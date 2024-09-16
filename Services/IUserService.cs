using BackendService.Models.Domain;
using BackendService.Models.DTO;
using BPKBBackend.Models;

namespace BackendService.Services
{
    public interface IUserService
    {
        Task<string> Login(LoginRequestDTO login);
        Task<IEnumerable<User>> GetAll();
        Task<User> GetById(int id);
        Task<bool> Create(UserToCreateDTO user);
        Task<bool> Update(UserToUpdateDTO user);
        Task<bool> Delete(int id);
    }

}
