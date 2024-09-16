using BackendService.Models.DTO;
using FluentValidation;

namespace BackendService.Validators.Cart
{
    public class CartValidator : ICartValidator
    {
        private readonly IValidator<CreateCartDTO> _createCartValidator;
        private readonly IValidator<CreateCartItemDTO> _createCartItemValidator;
        private readonly IValidator<UpdateCartItemDTO> _updateCartItemValidator;

        public CartValidator(
            IValidator<CreateCartDTO> createCartValidator,
            IValidator<CreateCartItemDTO> createCartItemValidator,
            IValidator<UpdateCartItemDTO> updateCartItemValidator)
        {
            _createCartValidator = createCartValidator;
            _createCartItemValidator = createCartItemValidator;
            _updateCartItemValidator = updateCartItemValidator;
        }

        public IValidator<CreateCartDTO> CreateCart()
        {
            return _createCartValidator;
        }

        public IValidator<CreateCartItemDTO> CreateCartItem()
        {
            return _createCartItemValidator;
        }

        public IValidator<UpdateCartItemDTO> UpdateCartItem()
        {
            return _updateCartItemValidator;
        }
    }

}
