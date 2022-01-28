using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Resource.Move.Contracts.Blip.Requests
{
    public class DefaultRequest
    {
        [JsonProperty("id")]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [JsonProperty("method")]
        public string Method { get; set; }
        
        [JsonProperty("uri")]
        public string Uri { get; set; }
        
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("resource")]
        public object Resource { get; set; }

        [JsonIgnore]
        public string ResourceId { get; set; }
    }
}
