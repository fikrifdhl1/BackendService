using BackendService.Models.Domain;
using BackendService.Models.DTO;
using BackendService.Repositories;
using BackendService.Utils.Logger;

namespace BackendService.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly ICartService _cartService;
        private readonly ICustomeLogger _logger;

        public TransactionService(ITransactionRepository transactionRepository, ICustomeLogger logger, ICartService cartService)
        {
            _transactionRepository = transactionRepository;
            _logger = logger;
            _cartService = cartService;
        }

        public async Task<bool> CreateAsync(CreateTransactionDTO transaction)
        {
            _logger.Log($"Starting {nameof(CreateAsync)}", LogLevel.Information);
            var cart = await _cartService.GetById(transaction.CartId);

            var checkOutCart = new CheckoutCartDTO
            {
                CartId= transaction.CartId,
                UserId= transaction.UserId,
            };

            await _cartService.CheckoutCart(checkOutCart);

            await _transactionRepository.AddAsync(new Transaction
            {
                CartId= transaction.CartId,
                UserId= transaction.UserId,
                TotalPrice = cart.TotalAmount,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
            });

            await _transactionRepository.SaveChangesAsync();
            return true;
        }

        public Task<IEnumerable<Transaction>> GetAllAsync()
        {
            _logger.Log($"Starting {nameof(GetAllAsync)}", LogLevel.Information);

            return _transactionRepository.GetAll();
        }

        public async Task<Transaction> GetByIdAsync(int id)
        {
            _logger.Log($"Starting {nameof(GetByIdAsync)}", LogLevel.Information);

            var transaction = await _transactionRepository.GetByIdAsync(id);
            if (transaction == null)
            {
                throw new BadHttpRequestException($"Transaction with id: {id} not found");
            }
            return transaction;
        }
    }
}
