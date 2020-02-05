using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using EventManager.Server.Models;

namespace EventManager.Server.Services
{
    public class UserDataService : IUserDataService
    {
        private readonly HttpClient _httpClient;

        public UserDataService(HttpClient httpClient)
        {
            _httpClient = httpClient ?? throw new ArgumentException(nameof(httpClient));
        }

        public async Task<User> AddUserAsync(User user)
        {
            var userJson = new StringContent(JsonSerializer.Serialize(user), Encoding.UTF8, "application/json-patch+json");
            var response = await _httpClient.PostAsync("api/users", userJson);

            if (response.IsSuccessStatusCode)
            {
                return await JsonSerializer.DeserializeAsync<User>(await response.Content.ReadAsStreamAsync());
            }

            return null;
        }

        public async Task<User> AuthenticateUserAsync(AuthenticateUser authenticateUser)
        {
            var authenticateUserJson = new StringContent(JsonSerializer.Serialize(authenticateUser), Encoding.UTF8, "application/json-patch+json");
            var response = await _httpClient.PostAsync("api/users/authenticate", authenticateUserJson);

            if (response.IsSuccessStatusCode)
            {
                return await JsonSerializer.DeserializeAsync<User>(await response.Content.ReadAsStreamAsync());
            }

            return null;
        }

        public async Task<User> GetUserByIdAsync(Guid userId)
        {
            return await JsonSerializer.DeserializeAsync<User>
                (await _httpClient.GetStreamAsync($"api/users/{userId}"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }

        public async Task<IEnumerable<User>> GetUsersAsync()
        {
            return await JsonSerializer.DeserializeAsync<IEnumerable<User>>
                (await _httpClient.GetStreamAsync($"api/users"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }
    }
}
