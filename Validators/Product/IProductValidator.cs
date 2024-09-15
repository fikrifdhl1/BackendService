using BackendService.Models.DTO;
using FluentValidation;

namespace BackendService.Validators.Product
{
    public interface IProductValidator
    {
        IValidator<CreateProductDTO> CreateProduct();
        IValidator<UpdateProductDTO> UpdateProduct();
    }
}
