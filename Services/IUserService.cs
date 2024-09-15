using BackendService.Models.Domain;
using BackendService.Models.DTO;

namespace BackendService.Services
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetAll();
        Task<User> GetById(int id);
        Task<bool> Create(UserToCreateDTO user);
        Task<bool> Update(UserToUpdateDTO user);
        Task<bool> Delete(int id);
    }

}
