using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assessment.Models;
using Assessment.Requests;
using Assessment.Response;
using Assessment.Services.IServices;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Assessment.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUserService _usersService;

        private readonly IConfiguration _configuration;

        public UserController(IMapper mapper, IUserService usersService)
        {
            _mapper = mapper;
            _usersService = usersService;
        }

        // add a user - register
        [HttpPost]
        public async Task<IActionResult> AddUser(AddUSer addUser)
        {
            try
            {
                var users = _mapper.Map<Users>(addUser);
                users.Role = "Admin";
                var response = await _usersService.AddUserAsync(users);
                return CreatedAtAction(nameof(AddUser), new UserSuccess(response, 201));
            }
            catch (Exception)
            {
                return BadRequest("Error adding user");
            }
        }

        // login a user
        [HttpPost("login")]
        public async Task<ActionResult<UserSuccess>> LoginUser(LoginUser loginUser)
        {
            // check if user exists using email
            var user = await _usersService.GetUserByEmailAsync(loginUser.Email);
            if (user == null)
            {
                return NotFound(new UserSuccess("User not found", 404));
            }
            // check if password is correct
            var password = BCrypt.Net.BCrypt.Verify(loginUser.Password, user.Password);
            if (!password)
            {
                return BadRequest(new UserSuccess("Invalid Credentials", 400));
            }
            // generate token
            // var token = CreateToken(user);
            // return Ok(new UserSuccess(token, 200));
            return Ok();
        }

        // create token
        // private string CreateToken(Users user)
        // {
        //     var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetValue<string>("TokenSecirity: SecretKey")));
        // }
        // update a user
        [HttpPut("{id}")]
        public async Task<ActionResult<UserSuccess>> UpdateUser(int id, AddUSer updateUser)
        {
            var existingUser = await _usersService.GetUserByIdAsync(id);
            if (existingUser == null)
            {
                return NotFound(new UserSuccess("User not found", 404));
            }
            var updatedUser = _mapper.Map(updateUser, existingUser);
            try
            {
                var success = await _usersService.UpdateUserAsync(updatedUser);
                return Ok(new UserSuccess(success, 200));
            }
            catch (Exception)
            {
                return BadRequest(new UserSuccess("Error updating user", 400));
            }

        }

        // delete a user
        [HttpDelete("{id}")]
        public async Task<ActionResult<UserSuccess>> DeleteUser(int id)
        {
            var existingUser = await _usersService.GetUserByIdAsync(id);
            if (existingUser == null)
            {
                return NotFound(new UserSuccess("User not found", 404));
            }
            try
            {
                var success = await _usersService.DeleteUserAsync(id);
                return Ok(new UserSuccess(success, 200));
            }
            catch (Exception)
            {
                return BadRequest(new UserSuccess("Error deleting user", 400));
            }
        }
        // get all users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UsersResponse>>> GetAllUsers(int? eventId)
        {
            var users = await _usersService.GetAllUsersAsync(eventId);
            var response = _mapper.Map<IEnumerable<UsersResponse>>(users);
            return Ok(response);
        }
        // get a user by id
        [HttpGet("{id}")]
        public async Task<ActionResult<UserSuccess>> GetUserById(int id)
        {
            var user = await _usersService.GetUserByIdAsync(id);
            if (user == null)
            {
                return NotFound(new UserSuccess("User not found", 404));
            }
            return Ok(user);
        }
        // endpoint to register for an event
        [HttpPost("register")]
        public async Task<ActionResult<UserSuccess>> RegisterForEvent(RegEvent registerForEvent)
        {
            try
            {
                var response = await _usersService.RegisterForEventAsync(registerForEvent.UserId, registerForEvent.EventId);
                return Ok(new UserSuccess(response, 200));
            }
            catch (Exception)
            {
                return BadRequest(new UserSuccess("Error registering for event", 400));
            }
        }
    }
}