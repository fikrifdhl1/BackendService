using BackendService.Models.Domain;
using BackendService.Models.DTO;

namespace BackendService.Utils.Mapper
{
    public class UserDTOMapper : ICustomeMapper<User, UserDTO>
    {
        public UserDTO Map(User source)
        {
            return new UserDTO
            {
                Id= source.Id,
                Email=source.Email,
                Role=source.Role,
                Username = source.Username,
            };
        }

        public User ReverseMap(UserDTO destination)
        {
            throw new NotImplementedException();
        }
    }
}
