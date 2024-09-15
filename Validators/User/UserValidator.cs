using BackendService.Models.DTO;
using FluentValidation;

namespace BackendService.Validators.User
{
    public class UserValidator : IUserValidator
    {
        private readonly IValidator<UserToCreateDTO> _create;
        private readonly IValidator<UserToUpdateDTO> _update;

        public UserValidator(IValidator<UserToCreateDTO> create, IValidator<UserToUpdateDTO> update)
        {
            _create = create;
            _update = update;
        }

        public IValidator<UserToCreateDTO> CreateUser()
        {
            return _create;
        }

        public IValidator<UserToUpdateDTO> UpdateUser()
        {
            return _update;
        }
    }
}
