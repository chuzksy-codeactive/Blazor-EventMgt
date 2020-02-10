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

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        public UserDto _user = new UserDto();

        public async Task HandleValidSubmit()
        {
            var user = await UserDataService.AddUserAsync(_user);

            if (user.User != string.Empty )
            {
                Toaster.Success("New user added successfully");
                NavigateToOverView();
            }
            else
            {
                Toaster.Error(user.Message);
            }

        }

        protected void NavigateToOverView()
        {
            NavigationManager.NavigateTo("/login");
        }
    }
}
