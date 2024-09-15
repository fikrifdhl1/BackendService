using Microsoft.EntityFrameworkCore;

namespace BackendService.Repositories.DbContexts
{
    public class CartDbContext : DbContext
    {
        public CartDbContext(DbContextOptions<CartDbContext> options) : base(options) { }

        public DbSet<CartDbContext> Carts{ get; set; }
    }
}
