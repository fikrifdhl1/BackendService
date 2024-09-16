using BackendService.Models.DTO;
using FluentValidation;

namespace BackendService.Validators.Cart
{
    public class CreateCartDTOValidator : AbstractValidator<CreateCartDTO>
    {
        public CreateCartDTOValidator()
        {
            RuleFor(x => x.UserId).NotEmpty();
        }
    }
}
