using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EventManager.Server.DTOs;

namespace EventManager.Server.Services
{
    public interface IUserDataService
    {
        Task<IEnumerable<UserDto>> GetUsersAsync();
        Task<UserLoginDto> AddUserAsync(UserDto user);
        Task<UserDto> AuthenticateUserAsync(AuthenticateUserDto authenticateUser);
        Task<UserDto> GetUserByIdAsync(Guid userId);
    }
}
