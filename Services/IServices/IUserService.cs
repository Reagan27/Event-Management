using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Assessment.Models;

namespace Assessment.Services.IServices
{
    public interface IUserService
    {
        // add a user
        Task<string> AddUserAsync(Users users);
        // update a user
        Task<string> UpdateUserAsync(Users users);
        // delete a user
        Task<string> DeleteUserAsync(int id);
        // get all users
        Task<IEnumerable<Users>> GetAllUsersAsync(int? eventId);
        // get a user by id
        Task<Users> GetUserByIdAsync(int id);
        // register for an event
        Task<string> RegisterForEventAsync(int userId, int eventId);
        // get user by email
        Task<Users> GetUserByEmailAsync(string email);
    }
}