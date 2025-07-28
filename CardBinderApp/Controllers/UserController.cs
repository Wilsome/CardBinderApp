using CardInfrastructure.DTO;
using CardInfrastructure.Interfaces;
using CardInfrastructure.Services;
using CardLibrary.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CardBinderApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class UserController(IUserService userService) : ControllerBase
    {
        //need a userservice object
        private readonly IUserService _userService = userService;

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(string id)
        {
            //pull the user
            User user = await _userService.GetUserByIdAsync(id);

            //validate 
            if (user == null)
            {
                return NotFound($"User with id {id} not found.");
            }

            return Ok(new UserResponseDto 
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
            });
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            ////create a list
            List<User> users = await _userService.GetAllUsersAsync();

            //return Ok(users);

            List<UserResponseDto> userDtos = users.Select(u => new UserResponseDto
            {
                FirstName = u.FirstName,
                LastName = u.LastName,
                Email = u.Email
            }).ToList();

            return Ok(userDtos);

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserById(string id)
        {
            User user = await _userService.GetUserByIdAsync(id);

            if (user == null)
            {
                return NotFound($"User {id} not found.");
            }

            //remove user
            await _userService.DeleteUserAsync(user);

            return Ok($"User {user.FirstName} {user.LastName} has been removed.");

        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserDto userDto) 
        {
            //validate model state
            if (!ModelState.IsValid) 
            {
                return BadRequest(ModelState);
            }

            //create the user
            User user = new()
            {
                FirstName = userDto.FirstName,
                LastName = userDto.LastName,
                Email = userDto.Email,
            };

           await _userService.CreateNewUserAsync(user);

            return Ok(new UserResponseDto
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email
            });

        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateUserAsync(string id, [FromBody] PatchUserDto userDto) 
        {
            //pull user from Database
            User user = await _userService.GetUserByIdAsync(id);

            if(user == null) 
            {
                return NotFound($"User {id} not found.");
            }

            //update properties
            if (userDto.FirstName != null) 
            {
                user.FirstName = userDto.FirstName;
            }
            if (userDto.LastName != null) 
            {
                user.LastName = userDto.LastName;
            }
            if (userDto.Email != null) 
            {
                user.Email = userDto.Email;   
            }

            await _userService.UpdateUserAsync(user);

            return Ok(new UserResponseDto 
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email
            });
        }
    }
}
