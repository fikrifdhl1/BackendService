using BPKBBackend.Models;
using FluentValidation;

namespace BackendService.Validators.User
{
    public class UserLoginDTOValidator : AbstractValidator<LoginRequestDTO>
    {
        public UserLoginDTOValidator()
        {
            RuleFor(x => x.Username).NotEmpty();
            RuleFor(x => x.Password).NotEmpty();
        }
    }
}
