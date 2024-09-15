using BackendService.Models.Domain;
using BackendService.Models.DTO;

namespace BackendService.Utils.Mapper
{
    public class ProductDTOMapper : ICustomeMapper<Product, ProductDTO>
    {
        public ProductDTO Map(Product source)
        {
            return new ProductDTO
            {
                Id= source.Id,
                Name= source.Name,
                Description= source.Description,
                Price= source.Price,
                Stock = source.Stock,
            };
        }

        public Product ReverseMap(ProductDTO destination)
        {
            throw new NotImplementedException();
        }
    }
}
