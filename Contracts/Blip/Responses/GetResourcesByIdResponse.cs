using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Resource.Move.Contracts.Blip.Responses
{
    public class GetResourcesByIdResponse : DefaultResponse
    {
        [JsonProperty("resource", NullValueHandling = NullValueHandling.Ignore)]
        public object Resource { get; set; }

        [JsonProperty("type", NullValueHandling = NullValueHandling.Ignore)]
        public string Type { get; set; }

        public string ResourceId { get; set; }

    }
}
