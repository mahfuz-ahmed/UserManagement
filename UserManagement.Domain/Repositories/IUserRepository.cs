using System.Collections.Generic;
using System.Threading.Tasks;
using UserManagement.Domain.Entities;

namespace UserManagement.Domain.Repositories
{
    public interface IUserRepository
    {
        Task AddAsync(User user);
        Task AddRangeAsync(IEnumerable<User> users);
        Task<List<User>> GetAllAsync();
        Task<User?> GetByEmailAsync(string email);
    }
}
