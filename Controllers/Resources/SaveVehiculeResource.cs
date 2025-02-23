using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace autobook.Controllers.Resources
{
    public class SaveVehiculeResource 
    {
        public int Id { get; set; }
        public int ModelId { get; set; }
        public Boolean IsRegistred { get; set; }
        [Required]
        public ContactResource Contact { get; set; }
        public List<int> Features { get; set; }

        public SaveVehiculeResource()
        {
            Features = new List<int>();
        }
    }
}