using BackendService.Models.Domain;

namespace BackendService.Repositories
{
    public interface ICartItemRepository : IBaseRepository<CartItem>
    {
        Task<IEnumerable<CartItem>> GetCartItemsByCartId(int cartId);

        Task<CartItem> GetCartItemsByCartIdAndProductId(int cartId,int productId);
    }
}
