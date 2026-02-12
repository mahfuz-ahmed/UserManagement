using AutoMapper;
using Bogus;
using Microsoft.Extensions.Caching.Hybrid;
using UserManagement.Application.DTOs;
using UserManagement.Domain.Entities;
using UserManagement.Domain.Repositories;

namespace UserManagement.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly HybridCache _cache;
        private const string UsersCacheKey = "UsersList";

        public UserService(IUserRepository userRepository, IMapper mapper, HybridCache cache)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _cache = cache;
        }

        public async Task<UserDto> CreateUserAsync(CreateUserDto createUserDto)
        {
            var existingUser = await _userRepository.GetByEmailAsync(createUserDto.Email);
            if (existingUser != null)
            {
                throw new InvalidOperationException("User with this email already exists.");
            }

            var user = _mapper.Map<User>(createUserDto);
            user.Id = Guid.NewGuid();
            user.TimeStamp = DateTime.Now;

            await _userRepository.AddAsync(user);
            await _cache.RemoveAsync(UsersCacheKey);

            return _mapper.Map<UserDto>(user);
        }

        public async Task CreateBulkUsersAsync(int count)
        {
            var faker = new Faker<User>()
                .RuleFor(u => u.Id, f => Guid.NewGuid())
                .RuleFor(u => u.Name, f => f.Name.FullName())
                .RuleFor(u => u.Age, f => f.Random.Number(18, 80))
                .RuleFor(u => u.Email, f => 
                {
                    return $"{f.Internet.UserName()}_{Guid.NewGuid().ToString().Substring(0, 8)}@example.com";
                })
                .RuleFor(u => u.TimeStamp, f => DateTime.Now);

            var users = faker.Generate(count);
            await _userRepository.BulkInsertAsync(users);
            await _cache.RemoveAsync(UsersCacheKey);
        }

        public async Task<List<UserDto>> GetUsersAsync()
        {
            var users = await _cache.GetOrCreateAsync(
                UsersCacheKey,
                async cancel => await _userRepository.GetAllAsync(),
                cancellationToken: CancellationToken.None
            );

            return _mapper.Map<List<UserDto>>(users);
        }
    }
}
