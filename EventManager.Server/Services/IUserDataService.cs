using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EventManager.Server.DTOs;

namespace EventManager.Server.Services
{
    public interface IUserDataService
    {
        Task<IEnumerable<User>> GetUsersAsync();
        Task<User> AddUserAsync(User user);
        Task<User> AuthenticateUserAsync(AuthenticateUser authenticateUser);
        Task<User> GetUserByIdAsync(Guid userId);
    }
}
