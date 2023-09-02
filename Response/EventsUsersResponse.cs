using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assessment.Response
{
    public class EventsUsersResponse
    {
        public string Name { get; set; } = "";
        public string EventDate { get; set; } = "";
        public string Location { get; set; } = "";
        public int Capacity { get; set; }
        public double Price { get; set; }
        public List<UsersResponseDto> Users { get; set; } = new List<UsersResponseDto>();
    }

    public class UsersResponseDto
    {
        public string PhoneNumber { get; set; } = "";
        public string Name { get; set; } = "";
        public string Email { get; set; } = "";
    }
}