using iserra_api.Dto;
using iserra_api.Responses;
using Microsoft.AspNetCore.Mvc;

namespace iserra_api.Services.UserServices;

public interface IUserService
{
    Task<ResponseModel<List<UserDto>>> GetAllUsers();
    Task<UserDto> Me(Guid userId);
    Task<ResponseModel<UserDto>> CreateUser(UserCreateDto user);
    Task<ResponseModel<UserDto>> UpdateUser(string email, UserUpdateDto user);
}