using System;
using System.ComponentModel.DataAnnotations;

namespace EventManager.Server.DTOs
{
    public class UserDto
    {
        [Required]
        public string Username { get; set; }

        [Required]
        [Display(Name = "First name")] 
        public string Firstname { get; set; }

        [Required]
        [Display(Name = "Last name")]
        public string Lastname { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        [StringLength(30, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 8)]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        [Compare("Password", ErrorMessage = "The passwords do not match.")]
        [Display(Name = "Confirm password")]
        public string CnfPassword { get; set; }
        public string Token { get; set; }
    }
}
