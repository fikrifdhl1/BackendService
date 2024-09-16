using BackendService.Models.Domain;
using BackendService.Repositories.DbContexts;

namespace BackendService.Repositories
{
    public class ProductRepository : BaseRepository<Product>,IProductRepository
    {
        private readonly ProductDbContext _context;
        public ProductRepository(ProductDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
