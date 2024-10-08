﻿using BackendService.Models.Domain;
using BackendService.Models.DTO;
using BackendService.Services;
using BackendService.Utils.Logger;
using BackendService.Validators.User;
using BPKBBackend.Models;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace BackendService.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ICustomeLogger _logger;
        private readonly IUserValidator _validator;
        
        public UserController(IUserService userService, ICustomeLogger logger,IUserValidator validator)
        {
            _userService = userService;
            _logger = logger;
            _validator = validator;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDTO request)
        {
            _logger.Log($"Starting {this}.{nameof(Login)}", LogLevel.Information);
            var validator = _validator.Login().Validate(request);
            if (!validator.IsValid)
            {
                return BadRequest(new ApiResponse<object>
                {
                    Code = (int)HttpStatusCode.BadRequest,
                    Message = "Validation Error",
                    ErrorDetails = validator.ToString()
                });
            }

            var token = await _userService.Login(request);

            return Ok(new ApiResponse<LoginResponseDTO>{
                Code = (int)HttpStatusCode.OK,
                Message = "Login success",
                Data = new LoginResponseDTO
                {
                    Token = token
                }
            });

        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            _logger.Log($"Starting {this}.{nameof(GetUsers)}", LogLevel.Information);
            var users = await _userService.GetAll();

            var result = new List<UserDTO>();
            foreach (var user in users)
            {
                result.Add(new UserDTO
                {
                    Id= user.Id,
                    Email= user.Email,
                    Role=user.Role,
                    Username = user.Username,
                });
            };

            var response = new ApiResponse<List<UserDTO>>
            {
                Code = (int)HttpStatusCode.OK,
                Data = result,
                Message = "Success getting users"
            };

            return Ok(response);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] UserToCreateDTO user)
        {
            _logger.Log($"Starting {this}.{nameof(CreateUser)}", LogLevel.Information);
            var validate = _validator.CreateUser().Validate(user);
            if (!validate.IsValid)
            {
                return BadRequest(new ApiResponse<object>
                {
                    Code = (int)HttpStatusCode.BadRequest,
                    Message = "Validation Error",
                    ErrorDetails = validate.ToString()
                });
            }

            var result = await _userService.Create(user);

            var response = new ApiResponse<object>
            {
                Code = (int)HttpStatusCode.OK,
                Message = "Success creating user"
            };
            return Ok(response);
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser([FromRoute] int id, [FromBody] UserToUpdateDTO user)
        {
            _logger.Log($"Starting {this}.{nameof(UpdateUser)}", LogLevel.Information);
            user.Id = id;
            var validate = _validator.UpdateUser().Validate(user);
            if (!validate.IsValid)
            {
                return BadRequest(new ApiResponse<object>
                {
                    Code = (int)HttpStatusCode.BadRequest,
                    Message = "Validation Error",
                    ErrorDetails = validate.ToString()
                });
            }

            var result = await _userService.Update(user);

            var response = new ApiResponse<object>
            {
                Code = (int)HttpStatusCode.OK,
                Message = "Success updating user"
            };
            return Ok(response);
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser([FromRoute] int id)
        {
            _logger.Log($"Starting {this}.{nameof(DeleteUser)}", LogLevel.Information);
            var result = await _userService.Delete(id);

            var response = new ApiResponse<object>
            {
                Code = (int)HttpStatusCode.OK,
                Message = "Success deleting user"
            };
            return Ok(response);
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> FindUserById([FromRoute] int id)
        {
            _logger.Log($"Starting {this}.{nameof(FindUserById)}", LogLevel.Information);

            var user = await _userService.GetById(id);

            var response = new ApiResponse<UserDTO>
            {
                Code = (int)HttpStatusCode.OK,
                Message = "Success getting user",
                Data = new UserDTO
                {
                    Id = user.Id,
                    Email = user.Email,
                    Role = user.Role,
                    Username = user.Username,
                },
            };

            return Ok(response);
        }

    }
}
