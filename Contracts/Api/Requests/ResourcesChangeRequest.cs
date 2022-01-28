using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Resource.Move.Contracts.Api.Requests
{
    public class ResourcesChangeRequest
    {
        public string AuthKeyOrigin { get; set; }
        public string AuthKeyDestination { get; set; }
    }
}
