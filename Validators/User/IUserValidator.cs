using BackendService.Models.DTO;
using BPKBBackend.Models;
using FluentValidation;

namespace BackendService.Validators.User
{
    public interface IUserValidator
    {
        IValidator<UserToCreateDTO> CreateUser();
        IValidator<UserToUpdateDTO> UpdateUser();
        IValidator<LoginRequestDTO> Login();
    }
}
