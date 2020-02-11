using System;
using System.Threading.Tasks;
using EventManager.Server.DTOs;
using EventManager.Server.Exceptions;
using EventManager.Server.Services;
using Microsoft.AspNetCore.Components;

namespace EventManager.Server.Pages
{
    public class SignInBase : ComponentBase
    {
        public AuthenticateUserDto _user = new AuthenticateUserDto();

        [Inject]
        public IUserDataService UserDataService { get; set; }

        [Inject]
        public IAuthenticatedUserDto authenticatedUser { get; set; }

        [Inject]
        protected Sotsera.Blazor.Toaster.IToaster Toaster { get; set; }

        public async Task HandleValidSubmit()
        {
            try
            {
                var user = await UserDataService.AuthenticateUserAsync(_user);
                authenticatedUser.Email = user.Email;
                authenticatedUser.Username = user.Username;
                authenticatedUser.Id = user.Id;
                authenticatedUser.Token = user.Token;
                Toaster.Success("Logged in successfully");
            }
            catch (ApiException ex)
            {
                Toaster.Error(ex.Message);
            }
        }
    }
}
