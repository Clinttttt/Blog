using Blog.Domain.Dtos;
using BlogApi.Client.Common.Auth;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Configuration;

namespace Blog.Client.Realtime
{
    public class PostHubClient(NavigationManager navigationManager,
        IConfiguration configuration,
        ITokenService tokenService) : HubClientBase(navigationManager, configuration, tokenService)
    {
        public Task PostInitializeAsync() => InitializeAsync("hubs/posts");


        public void OnViewCountUpdate(Action<int, int> handler)
            => Subscribe("ReceiveViewCountUpdate", handler);

    }
}
