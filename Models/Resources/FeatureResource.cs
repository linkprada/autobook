using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace autobook.Resources
{
    public class FeatureResource
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}