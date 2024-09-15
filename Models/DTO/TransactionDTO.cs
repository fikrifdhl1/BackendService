namespace BackendService.Models.DTO
{
    public class TransactionDTO
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int CartId { get; set; }
        public string ShippingAddress { get; set; }
        public string PaymentMethod { get; set; }
        public int Status { get; set; }
    }
}
