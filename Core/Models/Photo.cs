using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace autobook.Core.Models
{
    public class Photo
    {
        public int Id { get; set; }
        [Required]
        [StringLength(255)]
        public string FileName { get; set; }
        public int VehiculeId { get; set; }
    }
}