using BackendService.Models.Domain;
using BackendService.Repositories.DbContexts;

namespace BackendService.Repositories
{
    public class TransactionRepository : BaseRepository<Transaction>,ITransactionRepository
    {
        private readonly TransactionDbContext _context;
        public TransactionRepository(TransactionDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
