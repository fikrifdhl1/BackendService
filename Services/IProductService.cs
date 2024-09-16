using BackendService.Models.Domain;
using BackendService.Models.DTO;

namespace BackendService.Services
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetAllAsync();
        Task<Product> GetByIdAsync(int id);
        Task<bool> CreateAsync(CreateProductDTO product);
        Task<bool> UpdateAsync(UpdateProductDTO product);
        Task<bool> UpdateStockBulkAsync(List<UpdateStockDTO> products);
        Task<bool> DeleteAsync(int id);
    }
}
