using BackendService.Models.DTO;
using FluentValidation;

namespace BackendService.Validators.Cart
{
    public class UpdateCartItemValidator : AbstractValidator<UpdateCartItemDTO>
    {
        public UpdateCartItemValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.CartId).NotEmpty();
            RuleFor(x => x.Quantity).GreaterThanOrEqualTo(0).When(x => x.Quantity != null);
        }
    }
}
