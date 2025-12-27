using Azure;
using BlogApi.Application.Commands.Newsletter.UnSubscribeToNewsletter;
using BlogApi.Application.Dtos;
using BlogApi.Client.Interface;
using BlogApi.Domain.Common;
using System.Net;

namespace BlogApi.Client.Services
{
    public class UserClientServices : IUserClientServices
    { 
        private readonly HttpClient _httpClient;
        public UserClientServices(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<Result<UserProfileDto>?> GetCurrentUser()
        {
            var command = await _httpClient.GetAsync("api/User/GetCurrentUser");
            if (command.StatusCode == HttpStatusCode.NotFound) return null;
            command.EnsureSuccessStatusCode();
            var result = await command.Content.ReadFromJsonAsync<UserProfileDto>();
            if(result == null) return null;
            return Result<UserProfileDto>.Success(result);
        }
        public async Task<Result<bool>> UnSubscribeToNewsletter(string command)
        {
            var request = await _httpClient.GetAsync($"api/User/unsubscribe/{command}");
            request.EnsureSuccessStatusCode();
            var result = await request.Content.ReadFromJsonAsync<bool>();
            return Result<bool>.Success(result);
        }
    }
}
