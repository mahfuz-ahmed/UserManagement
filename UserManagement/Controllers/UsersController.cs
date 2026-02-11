using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UserManagement.Application.Services;
using UserManagement.Domain.Entities;

namespace UserManagement.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMemoryCache _cache;
        private const string UsersCacheKey = "UsersList";

        public UsersController(IUserService userService, IMemoryCache cache)
        {
            _userService = userService;
            _cache = cache;
        }

        [HttpPost("create-users")]
        public async Task<IActionResult> CreateUser([FromBody] User user)
        {
            try
            {
                await _userService.CreateUserAsync(user);
                _cache.Remove(UsersCacheKey); // Invalidate cache on change
                return Ok(user);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
            catch (DbUpdateException)
            {
                return BadRequest(new { Message = "A user with this email already exists in the database." });
            }
        }

        [HttpPost("create-bulk-users")]
        public async Task<IActionResult> CreateBulkUsers()
        {
            await _userService.CreateBulkUsersAsync(10000);
            _cache.Remove(UsersCacheKey); // Invalidate cache on change
            return Ok(new { Message = "10,000 users created successfully." });
        }

        [HttpGet("fetch-users")]
        public async Task<IActionResult> FetchUsers()
        {
            if (!_cache.TryGetValue(UsersCacheKey, out List<User>? users))
            {
                users = await _userService.GetUsersAsync();
                var cacheOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromMinutes(5));
                _cache.Set(UsersCacheKey, users, cacheOptions);
            }

            return Ok(users);
        }
    }
}
