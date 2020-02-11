using System;
using System.Text.Json;
using System.Threading.Tasks;
using EventManager.Server.DTOs;
using EventManager.Server.Exceptions;
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
            try
            {
                var user = await UserDataService.AddUserAsync(_user);
                Toaster.Success("New user added successfully");
                NavigateToOverView();
            }
            catch (ApiException ex)
            {
                Toaster.Error(ex.Content);
            }
        }

        protected void NavigateToOverView()
        {
            NavigationManager.NavigateTo("/login");
        }
    }
}
