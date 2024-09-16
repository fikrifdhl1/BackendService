using BackendService.Models.DTO;
using FluentValidation;

namespace BackendService.Validators.User
{
    public class UserUpdateDTOValidator : AbstractValidator<UserToUpdateDTO>
    {
        public UserUpdateDTOValidator()
        {
            RuleFor(x => x.Id).NotEmpty().GreaterThan(0);
            RuleFor(x => x.Email).EmailAddress().When(x => !string.IsNullOrEmpty(x.Email));
            RuleFor(x => x.Password).Length(8,20).When(x => !string.IsNullOrEmpty(x.Password));
        }
    }
}
