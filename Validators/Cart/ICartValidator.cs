using BackendService.Models.DTO;
using FluentValidation;

namespace BackendService.Validators.Cart
{
    public interface ICartValidator
    {
        IValidator<CreateCartDTO> CreateCart();
        IValidator<CreateCartItemDTO> CreateCartItem();
        IValidator<UpdateCartItemDTO> UpdateCartItem();   
    }
}
