using Assessment.Models;
using Assessment.Requests;
using Assessment.Response;
using Assessment.Services.IServices;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Assessment.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IEventsService _eventService;

        public EventsController(IMapper mapper, IEventsService eventService)
        {
            _mapper = mapper;
            _eventService = eventService;
        }

        // creating an event
        [HttpPost]
        [Authorize(Policy = "Admin")]
        public async Task<ActionResult<string>> CreateEvent(AddEvent addEvent)
        {
            var role = User.Claims.FirstOrDefault(x => x.Type == "Role").Value;
            if (!string.IsNullOrWhiteSpace(role) && role == "Admin")
            {
                var res = await _eventService.CreateEventAsync(_mapper.Map<Events>(addEvent));
                return CreatedAtAction(nameof(CreateEvent), new UserSuccess(res, 201));
            }
            return BadRequest("You are not authorized");
        }
        // updating an event
        [HttpPut("{id}")]
        [Authorize]
        public async Task<ActionResult<UserSuccess>> UpdateEvent(int id, UpdateEvent updateEvent)
        {
            var role = User.Claims.FirstOrDefault(x => x.Type == "Role").Value;
            if (!string.IsNullOrWhiteSpace(role) && role == "Admin")
            {
                var existingEvent = await _eventService.GetEventByIdAsync(id);
                if (existingEvent == null)
                {
                    return NotFound(new UserSuccess("Event not found", 404));
                }
                var updatedEvent = _mapper.Map<Events>(updateEvent);
                var response = await _eventService.UpdateEventAsync(updatedEvent);
                return Ok(new UserSuccess(response, 200));
            }
            return BadRequest("You are not authorized");
        }

        // deleting an event
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult<UserSuccess>> DeleteEvent(int id)
        {
            var role = User.Claims.FirstOrDefault(x => x.Type == "Role").Value;
            if (!string.IsNullOrWhiteSpace(role) && role == "Admin")
            {
                var existingEvent = await _eventService.GetEventByIdAsync(id);
                if (existingEvent == null)
                {
                    return NotFound(new UserSuccess("Event not found", 404));
                }
                var response = await _eventService.DeleteEventAsync(id);
                return Ok(new UserSuccess(response, 200));
            }
            return BadRequest("You are not authorized");
        }
        // getting all events and filtering by location
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EventsResponse>>> GetAllEvents(string? location)
        {
            var events = await _eventService.GetAllEventsAsync(location);
            var response = _mapper.Map<IEnumerable<EventsResponse>>(events);
            return Ok(response);
        }
        // getting an event by id
        [HttpGet("{id}")]
        public async Task<ActionResult<UserSuccess>> GetEventById(int id)
        {
            var events = await _eventService.GetEventByIdAsync(id);
            if (events == null)
            {
                return NotFound(new UserSuccess("Event not found", 404));
            }
            var response = _mapper.Map<EventsResponse>(events);
            return Ok(response);
        }

        // getting available slots
        [HttpGet("slots/{id}")]
        public async Task<ActionResult<int>> GetAvailableSlots(int id)
        {
            var events = await _eventService.GetEventByIdAsync(id);
            if (events == null)
            {
                return NotFound(new UserSuccess("Event not found", 404));
            }

            var slots = await _eventService.GetAvailableSlotsAsync(id);

            return Ok(slots);
        }

    }
}