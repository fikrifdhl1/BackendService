using BackendService.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace BackendService.Repositories.DbContexts
{
    public class UserDbContex : DbContext
    {
        public UserDbContex(DbContextOptions<UserDbContex> options) : base(options) { }

        public DbSet<User> Users{ get; set; }
    }
}
