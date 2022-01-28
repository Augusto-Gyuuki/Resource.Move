using Resource.Move.Contracts.Blip.Requests;
using Resource.Move.Contracts.Blip.Responses;
using RestEase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Resource.Move.Clients
{
    [Header("Content-Type", "application/json")]
    public interface IBlipResources
    {
        [Header("Authorization")]
        string Authorization { get; set; }


        [Post()]
        public Task<DefaultResponse> DeleteResourceAsync([Body] DefaultRequest defaultRequest);

        [Post()]
        public Task<DefaultResponse> UpdateResourceAsync([Body] DefaultRequest defaultRequest);

        [Post()]
        public Task<DefaultResponse> CreateResourceAsync([Body] DefaultRequest defaultRequest);

        [Post()]
        public Task<GetAllResourcesResponse> GetAllResources([Body] DefaultRequest defaultRequest);

        [Post()]
        public Task<GetResourcesByIdResponse> GetResourcesById([Body] DefaultRequest defaultRequest);
    }
}
