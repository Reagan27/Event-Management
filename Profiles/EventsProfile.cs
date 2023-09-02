using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Assessment.Models;
using Assessment.Requests;
using Assessment.Response;
using AutoMapper;

namespace Assessment.Profiles
{
    public class EventsProfile : Profile
    {
        public EventsProfile()
        {
            // create a user profile
            CreateMap<AddUSer, Users>().ReverseMap();
            CreateMap<UsersResponse, Users>().ReverseMap();

            // create an event profile
            CreateMap<AddEvent, Events>().ReverseMap();
            CreateMap<EventsResponse, Events>().ReverseMap();
            CreateMap<EventsUsersResponse, Events>().ReverseMap();
            CreateMap<Events, EventsUsersResponse>().ReverseMap();
            CreateMap<UpdateEvent, Events>().ReverseMap();

            CreateMap<EventsUsersResponse, Users>().ReverseMap();

            // map for the event registration
            CreateMap<RegEvent, EventsUsersResponse>().ReverseMap();

            // Mapping from EventsUsersResponse to Events
            // CreateMap<EventsUsersResponse, Events>();

        }
    }
}