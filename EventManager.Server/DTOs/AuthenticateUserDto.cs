using System;
using System.ComponentModel.DataAnnotations;

namespace EventManager.Server.DTOs
{
    public class AuthenticateUserDto
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
