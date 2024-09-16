using BackendService.Models.Domain;

namespace BackendService.Models.DTO
{
    public class CartDTO
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public float TotalAmount { get; set; }
        public List<CartItemDTO> Items { get; set; }
        public int Status { get; set; }
    }

    public class CreateCartDTO
    {
        public int UserId { get; set; }
    }

    public class CheckoutCartDTO
    {
        public int CartId { get; set; }
        public int UserId { get; set; }
    }
}
