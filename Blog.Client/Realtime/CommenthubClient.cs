
using BlogApi.Client.Common.Auth;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Reflection.Metadata.Ecma335;

namespace Blog.Client.Realtime
{
    public class CommenthubClient(
          NavigationManager navigationManager,
        IConfiguration configuration,
        ITokenService tokenService) : HubClientBase(navigationManager, configuration, tokenService)

    {
        public Task CommentInitializeAsync() => InitializeAsync("hubs/comments");

        public void OnSentComment(Action<int, string> handler)
            => Subscribe("ReceiveSentMessage", handler);

    }
}
