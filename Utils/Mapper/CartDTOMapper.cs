using BackendService.Models.Domain;
using BackendService.Models.DTO;

namespace BackendService.Utils.Mapper
{
    public class CartDTOMapper : ICustomeMapper<Cart, CartDTO>
    {
        public CartDTO Map(Cart source)
        {
            return new CartDTO
            {
                Id = source.Id,
                UserId = source.UserId,
                Status= source.Status,
                TotalAmount= source.TotalAmount,
            };
        }

        public Cart ReverseMap(CartDTO destination)
        {
            throw new NotImplementedException();
        }
    }
}
