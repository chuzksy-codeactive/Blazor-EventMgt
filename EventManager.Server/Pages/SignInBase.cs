using System;
using System.Threading.Tasks;
using EventManager.Server.DTOs;
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
        protected Sotsera.Blazor.Toaster.IToaster Toaster { get; set; }

        public async Task HandleValidSubmit()
        {
            var user = await UserDataService.AuthenticateUserAsync(_user);

            if (user.Username != string.Empty)
            {
                Toaster.Success("New user added successfully");
            }
            else
            {
                Toaster.Error(user.Message);
            }

        }
    }
}
