using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Resource.Move.Contracts.Blip.Responses
{
    public class DefaultResponse
    {

        [JsonProperty("method")]
        public string Method { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("from")]
        public string From { get; set; }

        [JsonProperty("to")]
        public string To { get; set; }

        [JsonProperty("metadata")]
        public MetadataJson Metadata { get; set; }

        [JsonProperty("reason", NullValueHandling = NullValueHandling.Ignore)]
        public ReasonJson Reason { get; set; }


        public class MetadataJson
        {
            [JsonProperty("#command.uri")]
            public string CommandUri { get; set; }

            [JsonProperty("uber-trace-id")]
            public string UberTraceId { get; set; }
        }

        public class ReasonJson
        {
            [JsonProperty("code")]
            public int Code { get; set; }

            [JsonProperty("description")]
            public string Description { get; set; }
        }

        
    }
}
