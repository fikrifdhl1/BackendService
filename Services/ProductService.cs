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

        public async Task<bool> CreateAsync(CreateProductDTO product)
        {
            _logger.Log($"Starting {this}.{nameof(CreateAsync)}", LogLevel.Information);

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

        public async Task<bool> DeleteAsync(int id)
        {
            _logger.Log($"Starting {this}.{nameof(DeleteAsync)}", LogLevel.Information);

            var product = await GetProductByIdAsync(id);

            await _productRepository.RemoveAsync(product);
            return true;
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            _logger.Log($"Starting {this}.{nameof(GetAllAsync)}", LogLevel.Information);
            return await _productRepository.GetAll();
        }

        public async Task<Product> GetByIdAsync(int id)
        {
            _logger.Log($"Starting {this}.{nameof(GetByIdAsync)}", LogLevel.Information);
            return await GetProductByIdAsync(id);
        }


        public async Task<bool> UpdateAsync(UpdateProductDTO product)
        {
            _logger.Log($"Starting {this}.{nameof(UpdateAsync)}", LogLevel.Information);

            var currentProduct = await GetProductByIdAsync(product.Id);

            if (!string.IsNullOrEmpty(product.Name))
            {
                currentProduct.Name = product.Name;
            }
            if (!string.IsNullOrEmpty(product.Description))
            {
                currentProduct.Description = product.Description;
            }
            if (product.Stock.HasValue)
            {
                currentProduct.Stock = product.Stock.Value;
            }
            if (product.Price.HasValue && product.Price.Value > 0)
            {
                currentProduct.Price = product.Price.Value;
            }

            await _productRepository.UpdateAsync(currentProduct);

            return true;
        }

        public async Task<bool> UpdateStockBulkAsync(List<UpdateStockDTO> products)
        {
            _logger.Log($"Starting {this}.{nameof(UpdateStockBulkAsync)}", LogLevel.Information);
            foreach(var item in products)
            {
                var product = await GetProductByIdAsync(item.Id);
                if (product.Stock < item.Quantity)
                {
                    throw new BadHttpRequestException($"The stock of product with id: {item.Id} not enough");
                }
                product.Stock -= item.Quantity;
                _productRepository.Update(product);
            }
            await _productRepository.SaveChangesAsync();
            return true;
        }

        private async Task<Product> GetProductByIdAsync(int id)
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
