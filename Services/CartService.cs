using BackendService.Models.Domain;
using BackendService.Models.DTO;
using BackendService.Models.Enum;
using BackendService.Repositories;
using BackendService.Utils.Logger;

namespace BackendService.Services
{
    public class CartService : ICartService
    {
        private readonly ICartRepository _cartRepository;
        private readonly ICartItemRepository _cartItemRepository;
        private readonly IProductService _productService;
        private readonly ICustomeLogger _customeLogger;

        public CartService(
            ICartRepository cartRepository,
            ICartItemRepository cartItemRepository,
            IProductService productService,
            ICustomeLogger customeLogger)
        {
            _cartRepository = cartRepository;
            _cartItemRepository = cartItemRepository;
            _productService = productService;
            _customeLogger = customeLogger;
        }

        public async Task<bool> Create(CreateCartDTO cart)
        {
            _customeLogger.Log($"Starting {nameof(Create)}", LogLevel.Information);

            var newCart = new Cart
            {
                UserId = cart.UserId,
                Status = (int)WorkFlowCart.Active,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
            };

            await _cartRepository.AddAsync(newCart);
            await _cartRepository.SaveChangesAsync();

            return true;
        }

        public async Task<IEnumerable<Cart>> GetAll()
        {
            _customeLogger.Log($"Starting {nameof(GetAll)}", LogLevel.Information);
            var carts = await _cartRepository.GetAll();

            foreach (var cart in carts)
            {
                cart.Items = await _cartItemRepository.GetCartItemsByCartId(cart.Id);
            }

            return carts;
        }

        public async Task<Cart> GetById(int id)
        {
            _customeLogger.Log($"Starting {nameof(GetById)}", LogLevel.Information);
            return await GetCartById(id);
        }

        public async Task<bool> AddItemToCart(CreateCartItemDTO cartItem)
        {
            _customeLogger.Log($"Starting {nameof(AddItemToCart)}", LogLevel.Information);

            var product = await _productService.GetByIdAsync(cartItem.ProductId);
            var existingCartItem = await _cartItemRepository.GetCartItemsByCartIdAndProductId(cartItem.CartId, cartItem.ProductId);

            if (cartItem.Quantity > product.Stock)
            {
                throw new BadHttpRequestException($"Stock not enough");
            }

            if (existingCartItem != null)
            {
                return await UpdateCartItem(new UpdateCartItemDTO
                {
                    Id = existingCartItem.Id,
                    CartId = existingCartItem.CartId,
                    Quantity = cartItem.Quantity,
                });
            }

            var newItem = new CartItem
            {
                CartId = cartItem.CartId,
                ProductId = cartItem.ProductId,
                Quantity = cartItem.Quantity,
                PriceUnit = product.Price,
                TotalPrice = product.Price * cartItem.Quantity,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
            };

            var cart = await GetCartById(cartItem.CartId);
            cart.TotalAmount += newItem.TotalPrice;
            cart.UpdatedAt = DateTime.UtcNow;

            await _cartRepository.UpdateAsync(cart);
            await _cartItemRepository.AddAsync(newItem);
            await _cartItemRepository.SaveChangesAsync();

            return true;
        }

        public async Task<bool> CheckoutCart(CheckoutCartDTO cart)
        {
            _customeLogger.Log($"Starting {nameof(CheckoutCart)}", LogLevel.Information);
            var currentCart = await GetCartById(cart.CartId);
            if (currentCart.Status != (int)WorkFlowCart.Active)
            {
                throw new BadHttpRequestException($"Cart with id: {cart.CartId} already checked out");
            }
            if (cart.UserId != currentCart.UserId)
            {
                throw new BadHttpRequestException($"User with id: {cart.UserId} didn't have cart with id: {cart.CartId}");
            }

            var productStocks = currentCart.Items.Select(x => new UpdateStockDTO
            {
                Id = x.ProductId,
                Quantity = x.Quantity
            }).ToList();

            await _productService.UpdateStockBulkAsync(productStocks);

            currentCart.Status = (int)WorkFlowCart.Checkedout;
            currentCart.UpdatedAt = DateTime.UtcNow;
            await _cartRepository.UpdateAsync(currentCart);

            return true;
        }

        public async Task<bool> UpdateCartItem(UpdateCartItemDTO cartItem)
        {
            _customeLogger.Log($"Starting {nameof(UpdateCartItem)}", LogLevel.Information);

            var currentCartItem = await GetCartItemById(cartItem.Id);
            if (currentCartItem.CartId != cartItem.CartId)
            {
                throw new BadHttpRequestException($"Cart with id: {cartItem.CartId} didn't have cart item with id: {cartItem.Id}");
            }

            var cart = await GetCartById(cartItem.CartId);
            var product = await _productService.GetByIdAsync(currentCartItem.ProductId);

            if (cartItem.Quantity.HasValue)
            {
                if (cartItem.Quantity > product.Stock)
                {
                    throw new BadHttpRequestException($"Stock not enough");
                }

                var newQuantity = cartItem.Quantity.Value;
                var newTotalPrice = product.Price * newQuantity;

                cart.TotalAmount = (cart.TotalAmount - currentCartItem.TotalPrice) + newTotalPrice;

                if (newQuantity == 0)
                {
                    await _cartItemRepository.RemoveAsync(currentCartItem);
                }
                else
                {
                    currentCartItem.Quantity = newQuantity;
                    currentCartItem.TotalPrice = newTotalPrice;
                    currentCartItem.UpdatedAt = DateTime.UtcNow;
                    await _cartItemRepository.UpdateAsync(currentCartItem);
                }
            }

            await _cartRepository.UpdateAsync(cart);

            return true;
        }

        public async Task<bool> Delete(int id)
        {
            _customeLogger.Log($"Starting {nameof(Delete)}", LogLevel.Information);

            var cart = await GetCartById(id);
            await _cartRepository.RemoveAsync(cart);

            return true;
        }

        private async Task<Cart> GetCartById(int id)
        {
            var cart = await _cartRepository.GetByIdAsync(id);
            if (cart == null)
            {
                throw new BadHttpRequestException($"Cart with id: {id} not found");
            }
            cart.Items = await _cartItemRepository.GetCartItemsByCartId(cart.Id);
            return cart;
        }

        private async Task<CartItem> GetCartItemById(int id)
        {
            var cartItem = await _cartItemRepository.GetByIdAsync(id);
            if (cartItem == null)
            {
                throw new BadHttpRequestException($"CartItem with id: {id} not found");
            }
            return cartItem;
        }
    }
}
