using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Assessment.Requests
{
    public class AddUSer
    {
        [Required]
        [MinLength(3)]
        [MaxLength(50)]
        public string PhoneNumber { get; set; } = "";
        [Required]
        [MinLength(3)]
        [MaxLength(50)]
        public string Name { get; set; } = "";
        [Required]
        [MinLength(3)]
        [MaxLength(50)]
        [EmailAddress]
        public string Email { get; set; } = "";
    }
}