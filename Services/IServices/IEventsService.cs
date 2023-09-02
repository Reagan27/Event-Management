using Assessment.Models;

namespace Assessment.Services.IServices
{
    public interface IEventsService
    {
        // creating an event
        Task<string> CreateEventAsync(Events events);
        // updating an event
        Task<string> UpdateEventAsync(Events events);
        // deleting an event
        Task<string> DeleteEventAsync(int id);
        // getting all events
        Task<IEnumerable<Events>> GetAllEventsAsync(string? location);
        // getting an event by id
        Task<Events> GetEventByIdAsync(int id);
        // getting available slots
        Task<int> GetAvailableSlotsAsync(int id);


    }
}