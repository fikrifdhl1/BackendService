using BackendService.Utils;
using Microsoft.EntityFrameworkCore;

namespace BackendService.Repositories
{
    public abstract class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        private readonly DbContext _context;
        public BaseRepository(DbContext context) {
            _context = context;
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task AddAsync(T data)
        {
            await _context.Set<T>().AddAsync(data);
        }

        public async Task UpdateAsync(T data)
        {
            _context.Set<T>().Update(data);
            await SaveChangesAsync();
        }

        public async Task RemoveAsync(T data)
        {
            _context.Set<T>().Remove(data);
            await SaveChangesAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Update(T data)
        {
            _context.Set<T>().Update(data);
        }
    }
}
