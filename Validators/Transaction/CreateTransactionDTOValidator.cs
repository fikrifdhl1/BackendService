using BackendService.Models.DTO;
using FluentValidation;

namespace BackendService.Validators.Transaction
{
    public class CreateTransactionDTOValidator : AbstractValidator<CreateTransactionDTO>
    {
        public CreateTransactionDTOValidator() { 
            RuleFor(x => x.UserId).NotEmpty();
            RuleFor(x => x.CartId).NotEmpty();
        }
    }
}
