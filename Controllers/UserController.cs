using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Assessment.Models;
using Assessment.Requests;
using Assessment.Response;
using Assessment.Services.IServices;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
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

        public UserController(IMapper mapper, IUserService usersService, IConfiguration configuration)
        {
            _mapper = mapper;
            _usersService = usersService;
            _configuration = configuration;
        }

        // add a user - register
        [HttpPost]
        public async Task<IActionResult> AddUser(AddUSer addUser)
        {
            try
            {
                var users = _mapper.Map<Users>(addUser);
                users.Password = BCrypt.Net.BCrypt.HashPassword(addUser.Password);
                //users.Role = "Admin";
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
        public async Task<ActionResult<string>> LoginUser(LoginUser loginUser)
        {
            // check if user exists using email
            var client = await _usersService.GetUserByEmailAsync(loginUser.Email);
            if (client == null)
            {
                return NotFound("Incorrect Credentials");
            }
            // check if password is correct
            var password = BCrypt.Net.BCrypt.Verify(loginUser.Password, client.Password);
            if (!password)
            {
                return NotFound("Incorrect Credentials");
            }
            // generate token
            var token = CreateToken(client);
            return Ok(token);
            // return Ok();
        }

        // create token
        private string CreateToken(Users user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetValue<string>("TokenSecurity:SecretKey")));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // payload data
            List<Claim> claims = new List<Claim>();
            claims.Add(new Claim("Sub", user.Id.ToString()));
            claims.Add(new Claim("Email", user.Email));
            claims.Add(new Claim("Role", user.Role));
            claims.Add(new Claim("Name", user.Name));

            // create token
            var token = new JwtSecurityToken(
                _configuration["TokenSecurity:Issuer"],
                _configuration["TokenSecurity:Audience"],
                signingCredentials: creds,
                claims: claims,
                expires: DateTime.UtcNow.AddHours(2)
            );

            // return token
            var verifiedToken = new JwtSecurityTokenHandler().WriteToken(token);
            return verifiedToken;

        }
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
                return NotFound(" Error registering for event");
            }
        }
    }
}