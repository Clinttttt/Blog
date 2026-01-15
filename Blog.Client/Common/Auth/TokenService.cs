using BlogApi.Application.Dtos;
using BlogApi.Application.Models;
using BlogApi.Client.Interface;
using BlogApi.Domain.Common;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.AspNetCore.Http;

namespace BlogApi.Client.Common.Auth
{
    public class TokenService : ITokenService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public TokenService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public Task<string?> GetAccessTokenAsync()
        {
            try
            {
                var httpContext = _httpContextAccessor.HttpContext;
                if (httpContext == null)
                    return Task.FromResult<string?>(null);

                if (!httpContext.Request.Cookies.TryGetValue("AccessToken", out var token) ||
                    string.IsNullOrWhiteSpace(token))
                    return Task.FromResult<string?>(null);

                token = token.Trim().Trim('"').Trim();
                return Task.FromResult<string?>(token);
            }
            catch
            {
                return Task.FromResult<string?>(null);
            }
        }
    }
}
