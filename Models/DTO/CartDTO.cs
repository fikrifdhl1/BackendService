using BackendService.Models.Domain;

namespace BackendService.Models.DTO
{
    public class CartDTO
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public float TotalAmount { get; set; }
        public CartItem[] Items { get; set; }
        public int Status { get; set; }
    }
}
