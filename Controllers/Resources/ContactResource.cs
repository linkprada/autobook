using System;
using System.ComponentModel.DataAnnotations;

namespace autobook.Controllers.Resources
{
    public class ContactResource
    {
        [Required]
        [MaxLength(255)]
        public String Name { get; set; }
        [Required]
        [EmailAddress]
        public String Email { get; set; }
        [Required]
        [MaxLength(255)]
        public String Phone { get; set; }
    }
}