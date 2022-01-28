using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Resource.Move.Contracts.Api.Responses
{
    public class ResourcesChangeResponse
    {
        public int Total { get; set; }
        public int ErrorCount { get; set; }
        public int SuccessCount { get; set; }
        public int DeleteCount { get; set; }
        public int SetCount { get; set; }
        public List<DefaultError> ResourcesErrors { get; set; } = new List<DefaultError>();
    }

    public class DefaultError
    {
        public string ResourceId { get; set; }
        public string Reason { get; set; }
        public ResourceAction ResourceAction { get; set; }
    }

    public enum ResourceAction
    {
        Delete,
        Set,
    }
}
