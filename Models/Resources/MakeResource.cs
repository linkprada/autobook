using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using autobook.Models;

namespace autobook.Resources
{
    public class MakeResource
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<ModelResource> Models { get; set; }

        public MakeResource()
        {
            Models = new List<ModelResource>();
        }
    }
}