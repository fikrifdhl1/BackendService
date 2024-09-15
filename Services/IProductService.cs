using BackendService.Models.Domain;
using BackendService.Models.DTO;

namespace BackendService.Services
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetAll();
        Task<Product> GetById(int id);
        Task<bool> Create(CreateProductDTO product);
        Task<bool> Update(UpdateProductDTO product);
        Task<bool> Delete(int id);
    }
}
