using Assessment.Context;
using Assessment.Models;
using Assessment.Services.IServices;
using Microsoft.EntityFrameworkCore;

namespace Assessment.Services
{
    public class UserService : IUserService
    {
        private readonly AppDbContext _context;
        public UserService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<string> AddUserAsync(Users users)
        {
            _context.Users.Add(users);
            await _context.SaveChangesAsync();
            return "User added successfully";
        }

        public async Task<string> DeleteUserAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user != null)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
                return "User deleted successfully";
            }
            return "User not found";
        }

        public async Task<IEnumerable<Users>> GetAllUsersAsync(int? eventId)
        {
            if (eventId == null)
            {
                var users = await _context.Users.ToListAsync();
                return users;
            }
            var usersByEvent = await _context.Users.Where(x => x.RegisteredEvents.Any(x => x.Id == eventId)).ToListAsync();
            return usersByEvent;

        }

        public async Task<Users> GetUserByIdAsync(int id)
        {
            var user = await _context.Users.Where(x => x.Id == id).FirstOrDefaultAsync();
            return user;
        }

        public async Task<string> UpdateUserAsync(Users user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
            return "User updated successfully";
        }

        // register for an event
        public async Task<string> RegisterForEventAsync(int userId, int eventId)
        {
            var user = await _context.Users.Where(x => x.Id == userId).FirstOrDefaultAsync();
            var events = await _context.Events.Where(x => x.Id == eventId).FirstOrDefaultAsync();
            if (user == null || events == null)
            {
                return "User or Event not found";
            }
            events.Users.Add(user);
            await _context.SaveChangesAsync();
            return "User registered successfully";
        }

        public async Task<Users> GetUserByEmailAsync(string email)
        {
            var user = await _context.Users.Where(x => x.Email == email).FirstOrDefaultAsync();
            return user;
        }
    }
}