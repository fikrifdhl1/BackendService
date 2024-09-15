using BackendService.Models.Domain;
using BackendService.Models.DTO;

namespace BackendService.Utils.Mapper
{
    public class TransactionDTOMapper : ICustomeMapper<Transaction, TransactionDTO>
    {
        public TransactionDTO Map(Transaction source)
        {
            return new TransactionDTO
            {
                Id= source.Id,
                CartId= source.CartId,
                PaymentMethod= source.PaymentMethod,
                ShippingAddress= source.ShippingAddress,
                Status  = source.Status,
                UserId = source.UserId,
            };
        }

        public Transaction ReverseMap(TransactionDTO destination)
        {
            throw new NotImplementedException();
        }
    }
}
