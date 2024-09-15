using BackendService.Models.DTO;
using BackendService.Services;
using BackendService.Utils.Logger;
using BackendService.Validators.Product;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace BackendService.Controllers
{
    [ApiController]
    [Route("/api/v1/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly ICustomeLogger _logger;
        private readonly IProductValidator _validator;

        public ProductController(IProductService productService, ICustomeLogger logger, IProductValidator validator)
        {
            _productService = productService;
            _logger = logger;
            _validator = validator;
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            _logger.Log($"Starting {this}.{nameof(GetProducts)}", LogLevel.Information);
            var result = await _productService.GetAll();
            var productsDTO = new List<ProductDTO>();

            foreach (var product in result)
            {
                productsDTO.Add(new ProductDTO
                {
                    Id = product.Id,
                    Name = product.Name,
                    Description = product.Description,
                    Price = product.Price,
                    Stock = product.Stock,
                });
            }

            return Ok(new ApiResponse<List<ProductDTO>>
            {
                Code = (int)HttpStatusCode.OK,
                Message = "Success getting products",
                Data = productsDTO
            });
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] CreateProductDTO product)
        {
            _logger.Log($"Starting {this}.{nameof(CreateProduct)}", LogLevel.Information);
            var validate = _validator.CreateProduct().Validate(product);

            if (!validate.IsValid)
            {
                return BadRequest(new ApiResponse<object>
                {
                    Code = (int)HttpStatusCode.BadRequest,
                    Message = "Validation Error",
                    ErrorDetails = validate.ToString()
                });
            }

            var result = await _productService.Create(product);
            return Ok(new ApiResponse<object>
            {
                Code = (int)HttpStatusCode.OK,
                Message = "Success to create product"
            });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById([FromRoute] int id)
        {
            _logger.Log($"Starting {this}.{nameof(GetProductById)}", LogLevel.Information);
            var product = await _productService.GetById(id);

            var productDTO = new ProductDTO
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                Stock = product.Stock,
            };

            return Ok(new ApiResponse<ProductDTO>
            {
                Code = (int)HttpStatusCode.OK,
                Message = "Success get product",
                Data = productDTO
            });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct([FromRoute] int id, [FromBody] UpdateProductDTO product)
        {
            _logger.Log($"Starting {this}.{nameof(UpdateProduct)}", LogLevel.Information);
            product.Id = id;

            var validate = _validator.UpdateProduct().Validate(product);
            if (!validate.IsValid)
            {
                return BadRequest(new ApiResponse<object>
                {
                    Code = (int)HttpStatusCode.BadRequest,
                    Message = "Validation Error",
                    ErrorDetails = validate.ToString()
                });
            }

            var result = await _productService.Update(product);
            return Ok(new ApiResponse<object>
            {
                Code = (int)HttpStatusCode.OK,
                Message = "Success to update product"
            });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct([FromRoute] int id)
        {
            _logger.Log($"Starting {this}.{nameof(DeleteProduct)}", LogLevel.Information);
            var result = await _productService.Delete(id);

            return Ok(new ApiResponse<object>
            {
                Code = (int)HttpStatusCode.OK,
                Message = "Success to delete product"
            });
        }
    }
}
