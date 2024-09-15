using BackendService.Models.Domain;
using BackendService.Models.DTO;
using BackendService.Repositories;
using BackendService.Utils;
using BackendService.Utils.Logger;
using BackendService.Validators.User;
using FluentValidation;
using System.Net;

namespace BackendService.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userReposistory;
        private readonly ICustomeLogger _logger;
        private readonly ICustomeHash _hash;


        public UserService(IUserRepository userReposistory, ICustomeLogger logger, ICustomeHash hash)
        {
            _userReposistory = userReposistory;
            _logger = logger;
            _hash = hash;
        }

        public async Task<bool> Create(UserToCreateDTO user)
        {
            _logger.Log($"Starting {this}.{nameof(Create)}", LogLevel.Information);

            var userExists = await _userReposistory.GetByUsernameAsync(user.Username);
            if (userExists != null)
            {
                throw new BadHttpRequestException($"User with username: {user.Username} already exists");
            }

            await _userReposistory.AddAsync(new User
            {
                Email = user.Email,
                Username = user.Username,
                HashedPassword = _hash.Hash(user.Password),
                Role = user.Role
            });

            await _userReposistory.SaveChangesAsync();

            return true;
        }

        public async Task<bool> Delete(int id)
        {
            _logger.Log($"Starting {this}.{nameof(Delete)}", LogLevel.Information);
            var userExists = await GetUserByIdAsync(id);

            await _userReposistory.RemoveAsync(userExists);
            return true;
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            _logger.Log($"Starting {this}.{nameof(GetAll)}", LogLevel.Information);
            return await _userReposistory.GetAll();
        }

        public async Task<User> GetById(int id)
        {
            _logger.Log($"Starting {this}.{nameof(GetById)}", LogLevel.Information);

            return await GetUserByIdAsync(id);
        }

        public async Task<bool> Update(UserToUpdateDTO user)
        {
            _logger.Log($"Starting {this}.{nameof(Update)}", LogLevel.Information);
            var currentUser = await GetUserByIdAsync(user.Id);

            if (!string.IsNullOrEmpty(user.Email))
            {
                currentUser.Email = user.Email;
            }

            if (!string.IsNullOrEmpty(user.Password))
            {
                currentUser.HashedPassword = _hash.Hash(user.Password);
            }

            await _userReposistory.UpdateAsync(currentUser);
            return true;
        }

        private async Task<User> GetUserByIdAsync(int id)
        {
            var user = await _userReposistory.GetByIdAsync(id);
            if (user == null)
            {
                throw new BadHttpRequestException($"User with id: {id} not exists");
            }
            return user;
        }

    }
}
