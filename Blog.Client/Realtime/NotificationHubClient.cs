using Blog.Domain.Dtos;
using BlogApi.Client.Common.Auth;
using BlogApi.Client.Interface;
using BlogApi.Client.Security;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;

namespace Blog.Client.Realtime
{
    public class NotificationHubClient(
        NavigationManager navigationManager,
        IConfiguration configuration,
        ITokenService tokenService) : HubClientBase(navigationManager, configuration, tokenService)
    {
        public Task NotificationInitializeAsync() => InitializeAsync("hubs/notifications");

        public void OnNotification(Action<NotificationDto> handler) =>
            Subscribe("ReceiveNotification", handler);

        public void OnNotificationCount(Action<int, Guid?> handler) =>
            Subscribe("ReceiveNewNotification", handler);

        public void OnPendingCountAdmin(Action<int> handler) =>
         Subscribe("ReceiveNewPendingAdmin", handler);

        public void OnPendingCountAuthor(Action<int, Guid?> handler) =>
         Subscribe("ReceiveNewPendingAuthor", handler);
    }
}
