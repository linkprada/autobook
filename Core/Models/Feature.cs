using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace autobook.Core.Models
{
    public class Feature
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public List<VehiculeFeature> Vehicules { get; set; }

        public Feature()
        {
            Vehicules = new List<VehiculeFeature>();
        }
    }
}