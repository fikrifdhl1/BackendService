using Microsoft.EntityFrameworkCore;

namespace BackendService.Repositories.DbContexts
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    }
}
