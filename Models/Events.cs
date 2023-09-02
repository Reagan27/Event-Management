using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assessment.Models
{
    public class Events
    {
        public int Id { get; set; }
        public string Name { get; set; } = "New Event";
        public string Description { get; set; } = "";
        public string Location { get; set; } = "Nyeri";
        public int Slots { get; set; } = 30;
        public double Price { get; set; } = 1000;
        public DateTime EventDate { get; set; }
        // public List<Users> Users { get; set; }
        public List<Users> Users { get; set; } = new List<Users>();

    }
}