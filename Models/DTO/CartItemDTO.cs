namespace BackendService.Models.DTO
{
    public class CartItemDTO
    {
        public int Id { get; set; }
        public int CartId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public float PriceUnit { get; set; }
        public float TotalPrice { get; set; }
    }

    public class CreateCartItemDTO
    {
        public int CartId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }

    public class UpdateCartItemDTO
    {
        public int Id { get; set; }
        public int CartId { get; set; }
        public int? Quantity { get; set; }
    }
        
}
