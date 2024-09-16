using BackendService.Models.DTO;
using FluentValidation;

namespace BackendService.Validators.Cart
{
    public class CreateCartItemDTOValidator : AbstractValidator<CreateCartItemDTO>
    {
        public CreateCartItemDTOValidator()
        {
            RuleFor(x => x.CartId).NotEmpty();
            RuleFor(x => x.Quantity).NotEmpty().GreaterThan(0);
            RuleFor(x => x.ProductId).NotEmpty();
        }
    }
}
