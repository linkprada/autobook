using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace autobook.Core.Models
{
    [Table("VehiculeFeatures")]
    public class VehiculeFeature
    {
        public int VehiculeId { get; set; }
        public int FeatureId { get; set; }
        public Vehicule Vehicule { get; set; }
        public Feature Feature { get; set; }
    }
}