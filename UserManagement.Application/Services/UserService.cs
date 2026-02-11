using Bogus;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UserManagement.Domain.Entities;
using UserManagement.Domain.Repositories;

namespace UserManagement.Application.Services
{
    public interface IUserService
    {
        Task CreateUserAsync(User user);
        Task CreateBulkUsersAsync(int count);
        Task<List<User>> GetUsersAsync();
    }

    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task CreateUserAsync(User user)
        {
            var existingUser = await _userRepository.GetByEmailAsync(user.Email);
            if (existingUser != null)
            {
                throw new InvalidOperationException("User with this email already exists.");
            }

            user.Id = Guid.NewGuid();
            user.TimeStamp = DateTime.Now;
            await _userRepository.AddAsync(user);
        }

        public async Task CreateBulkUsersAsync(int count)
        {
            var userIndex = 0;
            var faker = new Faker<User>()
                .RuleFor(u => u.Id, f => Guid.NewGuid())
                .RuleFor(u => u.Name, f => f.Name.FullName())
                .RuleFor(u => u.Age, f => f.Random.Number(18, 80))
                .RuleFor(u => u.Email, f => 
                {
                    // Ensure unique emails within the batch and generally
                    return $"{f.Internet.UserName()}_{Guid.NewGuid().ToString().Substring(0, 8)}@example.com";
                })
                .RuleFor(u => u.TimeStamp, f => DateTime.Now);

            var users = faker.Generate(count);
            await _userRepository.AddRangeAsync(users);
        }

        public async Task<List<User>> GetUsersAsync()
        {
            return await _userRepository.GetAllAsync();
        }
    }
}
