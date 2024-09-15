using BackendService.Models.DTO;
using FluentValidation;

namespace BackendService.Validators.Product
{
    public class ProductValidator : IProductValidator
    {
        private readonly IValidator<CreateProductDTO> _create;
        private readonly IValidator<UpdateProductDTO> _update;

        public ProductValidator(IValidator<CreateProductDTO> create, IValidator<UpdateProductDTO> update)
        {
            _create = create;
            _update = update;
        }

        public IValidator<CreateProductDTO> CreateProduct()
        {
            return _create;
        }

        public IValidator<UpdateProductDTO> UpdateProduct()
        {
            return _update;
        }
    }
}
