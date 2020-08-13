using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace autobook.Controllers.Resources
{
    public class KeyValuePairResource
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}