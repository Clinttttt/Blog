using BlogApi.Application.Dtos;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using System.Net.Http.Headers;

public class AuthorizationDelegatingHandler : DelegatingHandler
{
    private readonly ProtectedLocalStorage _localStorage;
    private readonly IHttpClientFactory _httpClientFactory;

    public AuthorizationDelegatingHandler(
        ProtectedLocalStorage localStorage,
        IHttpClientFactory httpClientFactory)
    {
        _localStorage = localStorage;
        _httpClientFactory = httpClientFactory;
    }

    protected override async Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        string? token = null;
        var tokenResult = await _localStorage.GetAsync<string>("AccessToken");
        token = tokenResult.Success ? tokenResult.Value : null;

        if (string.IsNullOrEmpty(token))
        {
          
            var authClient = _httpClientFactory.CreateClient("AuthClient");
            var refreshTokenResult = await _localStorage.GetAsync<string>("RefreshToken");

            if (refreshTokenResult.Success && !string.IsNullOrEmpty(refreshTokenResult.Value))
            {
                try
                {
                    var refreshResponse = await authClient.PostAsJsonAsync(
                        "api/Auth/RefreshToken",
                        new { RefreshToken = refreshTokenResult.Value },
                        cancellationToken);

                    if (refreshResponse.IsSuccessStatusCode)
                    {
                        var tokenResponse = await refreshResponse.Content.ReadFromJsonAsync<TokenResponseDto>(cancellationToken: cancellationToken);
                        if (tokenResponse != null)
                        {
                            await _localStorage.SetAsync("AccessToken", tokenResponse.AccessToken);
                            await _localStorage.SetAsync("RefreshToken", tokenResponse.RefreshToken);
                            token = tokenResponse.AccessToken;
                        }
                    }
                }
                catch
                {                
                }
            }
        }
        if (!string.IsNullOrEmpty(token))
        {
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }
        return await base.SendAsync(request, cancellationToken);
    }
}