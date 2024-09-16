using BackendService.Models.DTO;
using FluentValidation;

namespace BackendService.Validators.Transaction
{
    public interface ITransactionValidator
    {
        IValidator<CreateTransactionDTO> CreateTransaction();
    }
}
