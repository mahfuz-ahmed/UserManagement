using UserManagement.Application.DTOs;

namespace UserManagement.Application.Services
{
    public interface IUserService
    {
        Task<UserDto> CreateUserAsync(CreateUserDto createUserDto);
        Task CreateBulkUsersAsync(int count);
        Task<List<UserDto>> GetUsersAsync();
    }
}
