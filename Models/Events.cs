using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assessment.Models
{
    public class Events
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public int Capacity { get; set; }
        public double Price { get; set; }
        public DateTime EventDate { get; set; }
        // public List<Users> Users { get; set; }
        public List<Users> Users { get; set; } = new List<Users>();

    }
}