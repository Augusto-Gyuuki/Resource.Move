using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Resource.Move.Contracts.Blip.Responses
{
    public class GetAllResourcesResponse : DefaultResponse
    {
        public class ResourceJson
        {
            [JsonProperty("total", NullValueHandling = NullValueHandling.Ignore)]
            public int Total { get; set; }

            [JsonProperty("itemType", NullValueHandling = NullValueHandling.Ignore)]
            public string ItemType { get; set; }

            [JsonProperty("items", NullValueHandling = NullValueHandling.Ignore)]
            public List<string> Items { get; set; }
        }

        [JsonProperty("resource", NullValueHandling = NullValueHandling.Ignore)]
        public ResourceJson Resource { get; set; }
    }
}
