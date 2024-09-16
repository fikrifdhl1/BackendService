using BackendService.Models.DTO;
using BPKBBackend.Models;
using FluentValidation;

namespace BackendService.Validators.User
{
    public class UserValidator : IUserValidator
    {
        private readonly IValidator<UserToCreateDTO> _create;
        private readonly IValidator<UserToUpdateDTO> _update;
        private readonly IValidator<LoginRequestDTO> _login;

        public UserValidator(IValidator<UserToCreateDTO> create, IValidator<UserToUpdateDTO> update, IValidator<LoginRequestDTO> login)
        {
            _create = create;
            _update = update;
            _login = login;
        }

        public IValidator<UserToCreateDTO> CreateUser()
        {
            return _create;
        }

        public IValidator<LoginRequestDTO> Login()
        {
            return _login;
        }

        public IValidator<UserToUpdateDTO> UpdateUser()
        {
            return _update;
        }
    }
}
