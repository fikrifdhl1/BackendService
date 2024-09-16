using BackendService.Models.DTO;
using FluentValidation;

namespace BackendService.Validators.Transaction
{
    public class TransactionValidator : ITransactionValidator
    {
        private readonly IValidator<CreateTransactionDTO> _createTransaction;

        public TransactionValidator(IValidator<CreateTransactionDTO> createTransaction)
        {
            _createTransaction = createTransaction;
        }

        public IValidator<CreateTransactionDTO> CreateTransaction()
        {
            return _createTransaction;
        }
    }
}
