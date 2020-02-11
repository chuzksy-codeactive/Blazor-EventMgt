using System;
namespace EventManager.Server.DTOs
{
    public interface IAuthenticatedUserDto
    {
        string Id { get; set; }
        string Username { get; set; }
        string Email { get; set; }
        string Token { get; set; }
    }
}
