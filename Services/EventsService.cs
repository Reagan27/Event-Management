using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Assessment.Context;
using Assessment.Models;
using Assessment.Requests;
using Assessment.Services.IServices;
using Microsoft.EntityFrameworkCore;

namespace Assessment.Services
{
    public class EventsService : IEventsService
    {
        private readonly AppDbContext _context;
        // private const int maxSlots = 10;
        public EventsService(AppDbContext context)
        {
            _context = context;

        }
        public async Task<string> CreateEventAsync(Events events)
        {
            _context.Events.Add(events);
            await _context.SaveChangesAsync();
            return "Event created successfully";
        }

        public async Task<string> DeleteEventAsync(int id)
        {
            var events = await _context.Events.Where(x => x.Id == id).FirstOrDefaultAsync();
            if (events == null)
            {
                return "Event not found";
            }
            _context.Events.Remove(events);
            await _context.SaveChangesAsync();
            return "Event deleted successfully";
        }

        // getting all events along with the location
        public async Task<IEnumerable<Events>> GetAllEventsAsync(string? location)
        {
            if (location == null)
            {
                var events = await _context.Events.ToListAsync();
                return events;
            }
            // var eventsByLocation = await _context.Events.Where(x => x.Location == location).ToListAsync();
            // return eventsByLocation;
            var eventLocation = await _context.Events.Where(x => x.Location.ToLower().Contains(location.ToLower())).ToListAsync();
            return eventLocation;
        }

        // getting an event by id
        public async Task<Events> GetEventByIdAsync(int id)
        {
            var events = await _context.Events.Where(x => x.Id == id).FirstOrDefaultAsync();
            return events;
        }

        public async Task<string> UpdateEventAsync(Events events)
        {
            _context.Events.Update(events);
            await _context.SaveChangesAsync();
            return "Event updated successfully";
        }

        // service to get the available slots for an event
        public async Task<int> GetAvailableSlotsAsync(int id)
        {
            var event2 = await _context.Events.Include(e => e.Users).FirstOrDefaultAsync(e => e.Id == id);

            if (event2 == null)
            {
                return -1;
            }

            int availableSlots = event2.Slots - event2.Users.Count;

            return availableSlots < 0 ? 0 : availableSlots;
        }

    }
}