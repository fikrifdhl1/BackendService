using BackendService.Models.Domain;
using BackendService.Models.DTO;
using BackendService.Repositories;
using BackendService.Utils.Logger;

namespace BackendService.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly ICustomeLogger _logger;

        public ProductService(IProductRepository productRepository, ICustomeLogger logger)
        {
            _productRepository = productRepository;
            _logger = logger;
        }

        public async Task<bool> Create(CreateProductDTO product)
        {
            _logger.Log($"Starting {this}.{nameof(Create)}", LogLevel.Information);

            await _productRepository.AddAsync(new Product
            {
                Name = product.Name,
                Price = product.Price,
                Description = product.Description,
                Stock = product.Stock,
            });

            await _productRepository.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Delete(int id)
        {
            _logger.Log($"Starting {this}.{nameof(Delete)}", LogLevel.Information);

            var product = await GetProductById(id);

            await _productRepository.RemoveAsync(product);
            return true;
        }

        public async Task<IEnumerable<Product>> GetAll()
        {
            _logger.Log($"Starting {this}.{nameof(GetAll)}", LogLevel.Information);
            return await _productRepository.GetAll();
        }

        public async Task<Product> GetById(int id)
        {
            _logger.Log($"Starting {this}.{nameof(GetById)}", LogLevel.Information);
            return await GetProductById(id);
        }

        public async Task<bool> Update(UpdateProductDTO product)
        {
            _logger.Log($"Starting {this}.{nameof(Update)}", LogLevel.Information);

            var currentProduct = await GetProductById(product.Id);

            if (!string.IsNullOrEmpty(product.Name))
            {
                currentProduct.Name = product.Name;
            }
            if (!string.IsNullOrEmpty(product.Description))
            {
                currentProduct.Description = product.Description;
            }
            if (product.Stock != null)
            {
                currentProduct.Stock = (int)product.Stock;
            }
            if (product.Price != null && product.Price > 0)
            {
                currentProduct.Price = (float)product.Price;
            }

            await _productRepository.UpdateAsync(currentProduct);

            return true;
        }

        private async Task<Product> GetProductById(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product == null)
            {
                throw new BadHttpRequestException($"Product with id: {id} not found");
            }
            return product;
        }
    }
}
