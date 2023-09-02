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
        public string Name { get; set; } = "New Event";
        [Required]
        [MinLength(3)]
        [MaxLength(500)]
        public string Description { get; set; } = "";
        [Required]
        [MinLength(3)]
        [MaxLength(50)]
        public string Location { get; set; } = "Nyeri";
        [Required]
        [Range(1, 100000)]
        public int Slots { get; set; } = 30;
        [Required]
        [Range(1, 100000)]
        public double Price { get; set; } = 1000;
        [Required]
        public DateTime EventDate { get; set; } = DateTime.Now;
    }
}