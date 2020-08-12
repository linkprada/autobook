using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace autobook.Models
{
    public class Vehicule
    {
        public int Id { get; set; }
        public int ModelId { get; set; }
        [Required]
        public Model Model { get; set; }
        public Boolean IsRegistred { get; set; }
        [Required]
        [MaxLength(255)]
        public String ContactName { get; set; }
        [Required]
        [EmailAddress]
        public String ContactEmail { get; set; }
        [Required]
        [MaxLength(255)]
        public String ContactPhone { get; set; }
        public DateTime LastUpdate { get; set; }
        public List<VehiculeFeature> Features { get; set; }
    }
}