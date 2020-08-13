using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using autobook.Core.Models;

namespace autobook.Controllers.Resources
{
    public class MakeResource : KeyValuePairResource
    {
        public List<KeyValuePairResource> Models { get; set; }

        public MakeResource()
        {
            Models = new List<KeyValuePairResource>();
        }
    }
}