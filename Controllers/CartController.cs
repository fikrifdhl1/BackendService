using BackendService.Models.DTO;
using BackendService.Services;
using BackendService.Utils.Logger;
using BackendService.Validators.Cart;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace BackendService.Controllers
{
    [Authorize]
    [ApiController]
    [Route("/api/v1/[controller]")]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;
        private readonly ICustomeLogger _logger;
        private readonly ICartValidator _validator;

        public CartController(ICartService cartService, ICustomeLogger logger, ICartValidator validator)
        {
            _cartService = cartService;
            _logger = logger;
            _validator = validator;
        }

        [HttpGet]
        public async Task<IActionResult> GetCarts()
        {
            _logger.Log($"Starting {this}.{nameof(GetCarts)}", LogLevel.Information);
            var carts = await _cartService.GetAll();
            var cartsDTO = new List<CartDTO>();

            foreach (var cart in carts)
            {
                var cartDTO = new CartDTO
                {
                    Id = cart.Id,
                    UserId = cart.UserId,
                    TotalAmount = cart.TotalAmount,
                    Status = cart.Status,
                    Items = cart.Items.Select(item => new CartItemDTO
                    {
                        Id = item.Id,
                        CartId = item.CartId,
                        ProductId = item.ProductId,
                        Quantity = item.Quantity,
                        PriceUnit = item.PriceUnit,
                        TotalPrice = item.TotalPrice
                    }).ToList()
                };
                cartsDTO.Add(cartDTO);
            }

            return Ok(new ApiResponse<List<CartDTO>>
            {
                Code = (int)HttpStatusCode.OK,
                Message = "Success getting carts",
                Data = cartsDTO
            });
        }

        [HttpPost]
        public async Task<IActionResult> CreateCart([FromBody] CreateCartDTO cart)
        {
            _logger.Log($"Starting {this}.{nameof(CreateCart)}", LogLevel.Information);
            var validate = _validator.CreateCart().Validate(cart);

            if (!validate.IsValid)
            {
                return BadRequest(new ApiResponse<object>
                {
                    Code = (int)HttpStatusCode.BadRequest,
                    Message = "Validation Error",
                    ErrorDetails = validate.ToString()
                });
            }

            await _cartService.Create(cart);
            return Ok(new ApiResponse<object>
            {
                Code = (int)HttpStatusCode.OK,
                Message = "Success to create cart"
            });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCartById([FromRoute] int id)
        {
            _logger.Log($"Starting {this}.{nameof(GetCartById)}", LogLevel.Information);
            var cart = await _cartService.GetById(id);

            var cartDTO = new CartDTO
            {
                Id = cart.Id,
                UserId = cart.UserId,
                TotalAmount = cart.TotalAmount,
                Status = cart.Status,
                Items = cart.Items.Select(item => new CartItemDTO
                {
                    Id = item.Id,
                    CartId = item.CartId,
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    PriceUnit = item.PriceUnit,
                    TotalPrice = item.TotalPrice
                }).ToList()
            };

            return Ok(new ApiResponse<CartDTO>
            {
                Code = (int)HttpStatusCode.OK,
                Message = "Success getting cart",
                Data = cartDTO
            });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCart([FromRoute] int id)
        {
            _logger.Log($"Starting {this}.{nameof(DeleteCart)}", LogLevel.Information);
            await _cartService.Delete(id);

            return Ok(new ApiResponse<object>
            {
                Code = (int)HttpStatusCode.OK,
                Message = "Success to delete cart"
            });
        }

        [HttpPost("{cart_id}/items")]
        public async Task<IActionResult> AddItemToCart([FromRoute] int cart_id, [FromBody] CreateCartItemDTO cartItem)
        {
            _logger.Log($"Starting {this}.{nameof(AddItemToCart)}", LogLevel.Information);
            cartItem.CartId = cart_id;
            var validate = _validator.CreateCartItem().Validate(cartItem);

            if (!validate.IsValid)
            {
                return BadRequest(new ApiResponse<object>
                {
                    Code = (int)HttpStatusCode.BadRequest,
                    Message = "Validation Error",
                    ErrorDetails = validate.ToString()
                });
            }

            await _cartService.AddItemToCart(cartItem);
            return Ok(new ApiResponse<object>
            {
                Code = (int)HttpStatusCode.OK,
                Message = "Success to add item to cart"
            });
        }

        [HttpPut("{cart_id}/items/{cart_itemId}")]
        public async Task<IActionResult> UpdateCartItem([FromRoute] int cart_itemId, [FromRoute] int cart_id, [FromBody] UpdateCartItemDTO cartItem)
        {
            _logger.Log($"Starting {this}.{nameof(UpdateCartItem)}", LogLevel.Information);
            cartItem.Id = cart_itemId;
            cartItem.CartId = cart_id;

            var validate = _validator.UpdateCartItem().Validate(cartItem);
            if (!validate.IsValid)
            {
                return BadRequest(new ApiResponse<object>
                {
                    Code = (int)HttpStatusCode.BadRequest,
                    Message = "Validation Error",
                    ErrorDetails = validate.ToString()
                });
            }

            await _cartService.UpdateCartItem(cartItem);
            return Ok(new ApiResponse<object>
            {
                Code = (int)HttpStatusCode.OK,
                Message = "Success to update cart item"
            });
        }
    }
}
