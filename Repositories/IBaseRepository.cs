namespace BackendService.Repositories
{
    public interface IBaseRepository<T>
    {
        Task<IEnumerable<T>> GetAll();
        Task<T> GetByIdAsync(int id);
        Task AddAsync(T data);
        Task UpdateAsync(T data);
        void Update(T data);
        Task RemoveAsync(T data);
        Task SaveChangesAsync();
    }
}
