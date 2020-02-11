using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

using EventManager.Server.DTOs;
using EventManager.Server.Exceptions;
using Microsoft.AspNetCore.Components;

namespace EventManager.Server.Services
{
    public class UserDataService : IUserDataService
    {
        private readonly HttpClient _httpClient;
        protected string errorResult = string.Empty;
        protected string successMessage = string.Empty;

        public UserDataService(HttpClient httpClient)
        {
            _httpClient = httpClient ?? throw new ArgumentException(nameof(httpClient));
        }

        public async Task<UserLoginDto> AddUserAsync(UserDto user)
        {
            HttpContent httpContent = new StringContent(JsonSerializer.Serialize(user), Encoding.UTF8, "application/json");
            httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await _httpClient.PostAsync("api/users", httpContent);
            var responseBody = await response.Content.ReadAsStreamAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new ApiException
                {
                    StatusCode = (int)response.StatusCode,
                    Content = await response.Content.ReadAsStringAsync()
                };
            }

            return await JsonSerializer.DeserializeAsync<UserLoginDto>(responseBody, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }

        public async Task<AuthenticatedUserDto> AuthenticateUserAsync(AuthenticateUserDto authenticateUser)
        {
            HttpContent httpContent = new StringContent(JsonSerializer.Serialize(authenticateUser), Encoding.UTF8, "application/json");
            httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await _httpClient.PostAsync("api/users/authenticate", httpContent);
            var responseBody = await response.Content.ReadAsStreamAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new ApiException
                {
                    StatusCode = (int)response.StatusCode,
                    Content = await response.Content.ReadAsStringAsync()
                };
            }
            return await JsonSerializer.DeserializeAsync<AuthenticatedUserDto>(responseBody, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }

        public async Task<UserDto> GetUserByIdAsync(Guid userId)
        {
            return await JsonSerializer.DeserializeAsync<UserDto>
                (await _httpClient.GetStreamAsync($"api/users/{userId}"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }

        public async Task<IEnumerable<UserDto>> GetUsersAsync()
        {
            return await JsonSerializer.DeserializeAsync<IEnumerable<UserDto>>
                (await _httpClient.GetStreamAsync($"api/users"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }
    }
}
