using BlogApi.Client.Interface;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;

public class GoogleAuthCallbackService
{
    private readonly IAuthClientService _authService;
    private readonly NavigationManager _navigation;
    private readonly ProtectedLocalStorage _localStorage;

    public GoogleAuthCallbackService(
        IAuthClientService authService,
        NavigationManager navigation,
        ProtectedLocalStorage localStorage)
    {
        _authService = authService;
        _navigation = navigation;
        _localStorage = localStorage;
    }

    public async Task ProcessLogin(string idToken)
    {
        try
        {        
            var result = await _authService.GoogleLogin(idToken);

            if (result.IsSuccess && result != null)
            {            
               
                await _localStorage.SetAsync("AccessToken", result.Value!.AccessToken);
                await _localStorage.SetAsync("RefreshToken", result.Value.RefreshToken);
           
                _navigation.NavigateTo("/", forceLoad: true);
            }
            else
            {
                Console.WriteLine($"Login failed: {result}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Exception in ProcessLogin: {ex.Message}");
            throw;
        }
    }
}