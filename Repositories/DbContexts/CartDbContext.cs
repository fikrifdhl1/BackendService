using BackendService.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace BackendService.Repositories.DbContexts
{
    public class CartDbContext : DbContext
    {
        public CartDbContext(DbContextOptions<CartDbContext> options) : base(options) { }

        public DbSet<Cart> Carts{ get; set; }

        public DbSet<CartItem> CartItesm { get; set; }

    }
}
