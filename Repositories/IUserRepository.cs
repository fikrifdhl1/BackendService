using BackendService.Models.Domain;

namespace BackendService.Repositories
{
    public interface IUserRepository : IBaseRepository<User>
    {
        Task<User> GetByUsernameAsync(string username);
    }
}
