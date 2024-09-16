namespace BackendService.Models.DTO
{
    public class TransactionDTO
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int CartId { get; set; }
        public float TotalPrice { get; set; }
    }

    public class CreateTransactionDTO
    {
        public int UserId { get; set; }
        public int CartId { get; set; }
    }
}
