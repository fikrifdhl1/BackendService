using BackendService.Models.Domain;
using BackendService.Repositories.DbContexts;

namespace BackendService.Repositories
{
    public class CartRepository : BaseRepository<Cart>,ICartRepository
    {
        private readonly CartDbContext _context;

        public CartRepository(CartDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
