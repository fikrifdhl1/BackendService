using BackendService.Models.DTO;
using FluentValidation;

namespace BackendService.Validators.Product
{
    public class CreateProductDTOValidator : AbstractValidator<CreateProductDTO>
    {
        public CreateProductDTOValidator()
        {
            RuleFor(x => x.Name).NotEmpty().Length(4,30);
            RuleFor(x => x.Description).NotEmpty().Length(10,100);
            RuleFor(x => x.Stock).NotEmpty().GreaterThan(0);
            RuleFor(x => x.Price).NotEmpty().GreaterThan(100);
        }
    }
}
