using BackendService.Models.Domain;
using BackendService.Models.DTO;

namespace BackendService.Services
{
    public interface ITransactionService
    {
        Task<IEnumerable<Transaction>> GetAllAsync();
        Task<Transaction> GetByIdAsync(int id);
        Task<bool> CreateAsync(CreateTransactionDTO transaction);
    }
}
