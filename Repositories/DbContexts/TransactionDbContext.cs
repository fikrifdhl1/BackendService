using BackendService.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace BackendService.Repositories.DbContexts
{
    public class TransactionDbContext : DbContext
    {
        public TransactionDbContext(DbContextOptions<TransactionDbContext> options) : base(options) { }

        public DbSet<Transaction> Transactions { get; set; }
    }
}
