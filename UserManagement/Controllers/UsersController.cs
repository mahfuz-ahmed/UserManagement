using Microsoft.AspNetCore.Mvc;
using UserManagement.Application.Services;
using UserManagement.Application.DTOs;

namespace UserManagement.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("create-users")]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserDto userDto)
        {
            try
            {
                var result = await _userService.CreateUserAsync(userDto);
                return Ok(result);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
            catch (Exception)
            {
                return BadRequest(new { Message = "An error occurred while creating the user." });
            }
        }

        [HttpPost("create-bulk-users")]
        public async Task<IActionResult> CreateBulkUsers()
        {
            await _userService.CreateBulkUsersAsync(10000);
            return Ok(new { Message = "10,000 users created successfully." });
        }

        [HttpGet("fetch-users")]
        public async Task<IActionResult> FetchUsers()
        {
            var users = await _userService.GetUsersAsync();
            return Ok(users);
        }
    }
}
