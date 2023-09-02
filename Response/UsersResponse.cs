using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Assessment.Models;

namespace Assessment.Response
{
    public class UsersResponse
    {
        public int Id { get; set; }
        public string PhoneNumber { get; set; } = "";
        public string Name { get; set; } = "";
        public string Email { get; set; } = "";
        public List<Events> Events { get; set; } = new List<Events>();
    }
}