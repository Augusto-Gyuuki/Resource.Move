using Resource.Move.Contracts.Api.Requests;
using Resource.Move.Contracts.Api.Responses;
using Resource.Move.Contracts.Blip.Requests;
using Resource.Move.Contracts.Blip.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Resource.Move.Services.Interfaces
{
    public interface IResourcesService
    {
        public DefaultRequest ConvertResourceResponseToSetRequest(GetResourcesByIdResponse getResourcesByIdResponse);

        public DefaultRequest ConvertResourceResponseToDeleteRequest(GetResourcesByIdResponse getResourcesByIdResponse);

        public DefaultRequest CreateGetResourceByIdRequest(string resourceId);

        public DefaultRequest CreateGetAllResourcesRequest();

        public Task<GetAllResourcesResponse> GetAllBotResourcesAsync();

        public Task<GetResourcesByIdResponse> GetResourceByIdAsync(string resourceId);

        public Task<ResourcesChangeResponse> MoveBotResourcesAsync(ResourcesChangeRequest defaultRequest);

        public bool IsResourceEqual(object object1, object object2);

        public Task UpdateBotResourcesAsync(GetResourcesByIdResponse resourceResponse);

        public Task DeleteBotResourceAsync(GetResourcesByIdResponse resourceResponse);

        public void ValidateUpdateResourceResponse(DefaultResponse updateResourceResponse, string resourceId);

        public Task PopulateResourcesListAsync(GetAllResourcesResponse getAllResourcesResponse, List<GetResourcesByIdResponse> resourcesList);
    }
}
