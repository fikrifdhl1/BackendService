using BackendService.Models.Domain;
using BackendService.Repositories.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace BackendService.Repositories
{
    public class CartItemRepository : BaseRepository<CartItem>, ICartItemRepository
    {
        private readonly CartDbContext _context;
        public CartItemRepository(CartDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CartItem>> GetCartItemsByCartId(int cartId)
        {
            return await _context.Set<CartItem>().Where(x => x.CartId == cartId).ToListAsync();
        }

        public async Task<CartItem> GetCartItemsByCartIdAndProductId(int cartId, int productId)
        {
            return await _context.Set<CartItem>().Where(x => x.CartId == cartId && x.ProductId == productId).FirstOrDefaultAsync();
        }
    }
}
