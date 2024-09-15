using BackendService.Models.DTO;
using FluentValidation;

namespace BackendService.Validators.User
{
    public interface IUserValidator
    {
        IValidator<UserToCreateDTO> CreateUser();
        IValidator<UserToUpdateDTO> UpdateUser();
    }
}
