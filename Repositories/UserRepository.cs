using BackendService.Models.Domain;
using BackendService.Repositories.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace BackendService.Repositories
{
    public class UserRepository : BaseRepository<User>,IUserRepository
    {
        private readonly UserDbContex _context;

        public UserRepository(UserDbContex context) : base(context)
        {
            _context = context;
        }

        public async Task<User> GetByUsernameAsync(string username)
        {
            var user = await _context.Users.Where(x => x.Username.Equals(username)).FirstOrDefaultAsync();
            return user;
        }
    }
}
