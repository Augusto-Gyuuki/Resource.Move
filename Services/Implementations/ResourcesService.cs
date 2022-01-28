using Newtonsoft.Json;
using Resource.Move.Clients;
using Resource.Move.Contracts.Api.Requests;
using Resource.Move.Contracts.Api.Responses;
using Resource.Move.Contracts.Blip.Requests;
using Resource.Move.Contracts.Blip.Responses;
using Resource.Move.Services.Interfaces;
using RestEase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace Resource.Move.Services
{
    public class ResourcesService : IResourcesService
    {
        private readonly IBlipResources blipResourcesApi = RestClient.For<IBlipResources>("https://msging.net/commands");
        private ResourcesChangeResponse resourcesChangeResponse = null;

        public DefaultRequest ConvertResourceResponseToSetRequest(GetResourcesByIdResponse getResourcesByIdResponse)
        {
            var baseDefaultRequest = new DefaultRequest();
            baseDefaultRequest.Method = "set";
            baseDefaultRequest.Uri = $"/resources/{Uri.EscapeDataString(getResourcesByIdResponse.ResourceId)}";
            baseDefaultRequest.Type = getResourcesByIdResponse.Type;
            baseDefaultRequest.Resource = getResourcesByIdResponse.Resource;

            return baseDefaultRequest;
        }

        public DefaultRequest ConvertResourceResponseToDeleteRequest(GetResourcesByIdResponse getResourcesByIdResponse)
        {
            var baseDefaultRequest = new DefaultRequest();
            baseDefaultRequest.Method = "delete";
            baseDefaultRequest.Uri = $"/resources/{Uri.EscapeDataString(getResourcesByIdResponse.ResourceId)}";

            return baseDefaultRequest;
        }

        public DefaultRequest CreateGetResourceByIdRequest(string resourceId)
        {
            var baseDefaultRequest = new DefaultRequest();
            baseDefaultRequest.Method = "get";
            baseDefaultRequest.Uri = $"/resources/{Uri.EscapeDataString(resourceId)}";

            return baseDefaultRequest;
        }

        public DefaultRequest CreateGetAllResourcesRequest()
        {
            var baseDefaultRequest = new DefaultRequest();
            baseDefaultRequest.Method = "get";
            baseDefaultRequest.Uri = "/resources";
            return baseDefaultRequest;
        }

        public async Task<GetAllResourcesResponse> GetAllBotResourcesAsync()
        {
            return await blipResourcesApi.GetAllResources(CreateGetAllResourcesRequest());
        }

        public async Task<GetResourcesByIdResponse> GetResourceByIdAsync(string resourceId)
        {
            return await blipResourcesApi.GetResourcesById(CreateGetResourceByIdRequest(resourceId));
        }

        public async Task<ResourcesChangeResponse> MoveBotResourcesAsync(ResourcesChangeRequest defaultRequest)
        {
            try
            {
                GetAllResourcesResponse allResourcesResponse;
                List<GetResourcesByIdResponse> resourcesOriginList = new();
                List<GetResourcesByIdResponse> resourcesDestinationList = new();
                resourcesChangeResponse = new();

                blipResourcesApi.Authorization = defaultRequest.AuthKeyOrigin;
                allResourcesResponse = await GetAllBotResourcesAsync();
                await PopulateResourcesListAsync(allResourcesResponse, resourcesOriginList);

                blipResourcesApi.Authorization = defaultRequest.AuthKeyDestination;
                allResourcesResponse = await GetAllBotResourcesAsync();
                await PopulateResourcesListAsync(allResourcesResponse, resourcesDestinationList);

                foreach (var item in resourcesOriginList)
                {
                    var x = resourcesDestinationList.Find(x => x.ResourceId.Equals(item.ResourceId));
                    if (x is null)
                    {
                        await UpdateBotResourcesAsync(item);
                        continue;
                    }

                    resourcesDestinationList.Remove(x);
                    if (x is not null && !IsResourceEqual(x.Resource, item.Resource))
                    {
                        await UpdateBotResourcesAsync(item);
                    }
                }

                foreach (var item in resourcesDestinationList)
                {
                    await DeleteBotResourceAsync(item);
                }

                return resourcesChangeResponse;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public bool IsResourceEqual(object object1, object object2)
        {
            var serializedObject01 = JsonConvert.SerializeObject(object1);
            var serializedObject02 = JsonConvert.SerializeObject(object2);
            return serializedObject01 == serializedObject02;
        }

        public async Task UpdateBotResourcesAsync(GetResourcesByIdResponse resourceResponse)
        {
            var request = ConvertResourceResponseToSetRequest(resourceResponse);
            ValidateUpdateResourceResponse(await blipResourcesApi.UpdateResourceAsync(request), resourceResponse.ResourceId);
        }

        public async Task DeleteBotResourceAsync(GetResourcesByIdResponse resourceResponse)
        {
            var request = ConvertResourceResponseToDeleteRequest(resourceResponse);
            ValidateUpdateResourceResponse(await blipResourcesApi.DeleteResourceAsync(request), resourceResponse.ResourceId);
        }

        public void ValidateUpdateResourceResponse(DefaultResponse updateResourceResponse, string resourceId)
        {
            DefaultError defaultError = new();
            resourcesChangeResponse.Total++;

            if (updateResourceResponse.Status.Equals("failure"))
            {
                resourcesChangeResponse.ErrorCount++;
                defaultError.Reason = updateResourceResponse.Reason.Description;
                defaultError.ResourceId = resourceId;
                switch (updateResourceResponse.Method)
                {
                    case "set":
                        defaultError.ResourceAction = ResourceAction.Set;
                        break;
                    case "delete":
                        defaultError.ResourceAction = ResourceAction.Delete;
                        break;
                    default:
                        break;
                }
                resourcesChangeResponse.ResourcesErrors.Add(defaultError);
            }

            resourcesChangeResponse.SuccessCount++;

            switch (updateResourceResponse.Method)
            {
                case "set":
                    resourcesChangeResponse.SetCount++;
                    break;
                case "delete":
                    resourcesChangeResponse.DeleteCount++;
                    break;
                default:
                    break;
            }
        }

        public async Task PopulateResourcesListAsync(GetAllResourcesResponse getAllResourcesResponse, List<GetResourcesByIdResponse> resourcesList)
        {
            foreach (var item in getAllResourcesResponse.Resource.Items)
            {
                var response = await GetResourceByIdAsync(item);
                response.ResourceId = item;
                resourcesList.Add(response);
            }
        }
    }
}
