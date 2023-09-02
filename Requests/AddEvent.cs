using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Assessment.Requests
{
    public class AddEvent
    {

        [Required]
        [MinLength(3)]
        [MaxLength(50)]
        public string Name { get; set; } = "";
        [Required]
        [MinLength(3)]
        [MaxLength(500)]
        public string Description { get; set; } = "";
        [Required]
        [MinLength(3)]
        [MaxLength(50)]
        public string Location { get; set; } = "";
        [Required]
        [Range(1, 100000)]
        public double Price { get; set; }
        [Required]
        public DateTime EventDate { get; set; } = DateTime.Now;
    }
}