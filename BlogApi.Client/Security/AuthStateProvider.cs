using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace BlogApi.Client.Security
{
    public class AuthStateProvider : AuthenticationStateProvider
    {
        private readonly ProtectedLocalStorage _localStorage;
        private bool _isInitialized;

        public AuthStateProvider(ProtectedLocalStorage localStorage)
        {
            _localStorage = localStorage;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            if (!_isInitialized)
                return CreateAnonymousState();

            try
            {
                var tokenResult = await _localStorage.GetAsync<string>("AccessToken");

                if (!tokenResult.Success || string.IsNullOrWhiteSpace(tokenResult.Value))
                    return CreateAnonymousState();

                var handler = new JwtSecurityTokenHandler();
                JwtSecurityToken jwt;

                try
                {
                    jwt = handler.ReadJwtToken(tokenResult.Value);
                }
                catch
                {
                    await ClearAuthDataAsync();
                    return CreateAnonymousState();
                }

                if (jwt.ValidTo < DateTime.UtcNow)
                {
                    await ClearAuthDataAsync();
                    return CreateAnonymousState();
                }

                var claims = jwt.Claims
                    .Select(c => c.Type == "role" ? new Claim(ClaimTypes.Role, c.Value) : c)
                    .ToList();

                var identity = new ClaimsIdentity(claims, "jwt");
                var user = new ClaimsPrincipal(identity);

                return new AuthenticationState(user);
            }
            catch
            {
                return CreateAnonymousState();
            }
        }



      /// <summary>
      /// ///////////////////////////////////////////////////////////////////////////////////////////
      /// </summary>
      /// <returns></returns>
      /// 

        private AuthenticationState CreateAnonymousState()
        {
            var anonymous = new ClaimsPrincipal(new ClaimsIdentity());
            return new AuthenticationState(anonymous);
        }

        private async Task ClearAuthDataAsync()
        {
            try
            {
                await _localStorage.DeleteAsync("AccessToken");
                await _localStorage.DeleteAsync("RefreshToken");
            }
            catch
            {
                // ignore
            }
        }

        private async Task NotifyUserChangedAsync()
        {
            var authState = await GetAuthenticationStateAsync();
            NotifyAuthenticationStateChanged(Task.FromResult(authState));
        }

        public async Task InitializeAsync()
        {
            _isInitialized = true;
            await NotifyUserChangedAsync();
        }

        public void MarkUserAsAuthenticated()
        {
            _isInitialized = true;
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }

        public async Task MarkUserAsLoggedOut()
        {
            await ClearAuthDataAsync();
            NotifyAuthenticationStateChanged(Task.FromResult(CreateAnonymousState()));
        }

        public async Task<string?> GetUserIdAsync()
        {
            var authState = await GetAuthenticationStateAsync();
            return authState.User.Identity?.IsAuthenticated == true
                ? authState.User.FindFirst(ClaimTypes.NameIdentifier)?.Value
                : null;
        }
        public async Task<string?> GetUserNameAsync()
        {
            var authState = await GetAuthenticationStateAsync();
            return authState.User.Identity?.IsAuthenticated == true
                ? authState.User.FindFirst(ClaimTypes.Name)?.Value
                : null;
        }
    }
}
