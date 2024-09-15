using BackendService.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace BackendService.Repositories.DbContexts
{
    public class ProductDbContext : DbContext
    {
        public ProductDbContext(DbContextOptions<ProductDbContext> options) : base(options) { }

        public DbSet<Product> Products{ get; set; }
    }
}
