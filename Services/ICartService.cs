using BackendService.Models.Domain;
using BackendService.Models.DTO;

namespace BackendService.Services
{
    public interface ICartService
    {
        Task<IEnumerable<Cart>> GetAll();
        Task<Cart> GetById(int id);
        Task<bool> Create(CreateCartDTO cart);
        Task<bool> Delete(int id);
        Task<bool> CheckoutCart(CheckoutCartDTO cart);

        Task<bool> AddItemToCart(CreateCartItemDTO cartItem);
        Task<bool> UpdateCartItem(UpdateCartItemDTO cartItem);
    }
}
