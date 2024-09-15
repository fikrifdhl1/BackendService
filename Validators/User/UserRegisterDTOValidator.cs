using BackendService.Models.DTO;
using FluentValidation;

namespace BackendService.Validators.User
{
    public class UserRegisterDTOValidator : AbstractValidator<UserToCreateDTO>
    {
        public UserRegisterDTOValidator()
        {
            RuleFor(x => x.Username).NotEmpty().Length(5,20);
            RuleFor(x => x.Password).NotEmpty().Length(8,20);
            RuleFor(x => x.Email).EmailAddress().MaximumLength(30);
            RuleFor(x => x.Role).NotEmpty();
        }
    }
}
