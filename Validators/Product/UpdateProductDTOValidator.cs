using BackendService.Models.DTO;
using FluentValidation;

namespace BackendService.Validators.Product
{
    public class UpdateProductDTOValidator : AbstractValidator<UpdateProductDTO>
    {
        public UpdateProductDTOValidator()
        {
            RuleFor(x => x.Name).Length(4, 30).When(x => !string.IsNullOrEmpty(x.Name));
            RuleFor(x => x.Description).Length(10, 100).When(x => !string.IsNullOrEmpty(x.Description));
            RuleFor(x => x.Stock).GreaterThanOrEqualTo(0);
            RuleFor(x => x.Price).GreaterThan(100).When(x => x.Price != null);
        }
    }
}
