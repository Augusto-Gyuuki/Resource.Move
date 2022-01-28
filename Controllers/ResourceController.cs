using Microsoft.AspNetCore.Mvc;
using Polly;
using Polly.Retry;
using Polly.Timeout;
using Resource.Move.Clients;
using Resource.Move.Contracts.Api.Requests;
using Resource.Move.Services.Interfaces;
using RestEase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Resource.Move.Controllers
{
    [Route("api/[controller]")]
    public class ResourceController : ControllerBase
    {
        private readonly IResourcesService _resourcesService;
        private readonly AsyncRetryPolicy _asyncRetry;
        private readonly AsyncTimeoutPolicy _timeoutPolicy;
        public ResourceController(IResourcesService resourcesService)
        {
            _resourcesService = resourcesService;
            _asyncRetry = Policy.Handle<TimeoutRejectedException>().RetryAsync(3);
            _timeoutPolicy = Policy.TimeoutAsync(2, TimeoutStrategy.Pessimistic);
        }

        [HttpPost()]
        public async Task<IActionResult> Substituite([FromBody] ResourcesChangeRequest resourcesChangeRequest)
        {
            var a = await _resourcesService.MoveBotResourcesAsync(resourcesChangeRequest);

            return Ok(a);
        }
    }
}
