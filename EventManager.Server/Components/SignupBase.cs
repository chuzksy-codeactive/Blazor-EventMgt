using System;
using System.Text.Json;
using System.Threading.Tasks;
using EventManager.Server.DTOs;
using EventManager.Server.Services;
using Microsoft.AspNetCore.Components;

namespace EventManager.Server.Components
{
    public class SignupBase : ComponentBase
    {
        [Inject]
        public IUserDataService UserDataService { get; set; }

        [Inject]
        protected Sotsera.Blazor.Toaster.IToaster Toaster { get; set; }

        public UserDto _user = new UserDto();

        public async Task HandleValidSubmit()
        {
            Console.WriteLine("=========== OnValidSubmit ============");
            var user = await UserDataService.AddUserAsync(_user);

            if (user != null )
            {
                Toaster.Success("New user added successfully");
                _user = null;
                StateHasChanged();
            }
            else
            {
                Toaster.Error("Something went wrong! Try again");
                _user = null;
                StateHasChanged();
            }

        }
    }
}
