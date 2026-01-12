using Blog.Domain.Dtos;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Configuration;

namespace Blog.Client.Realtime
{
    public class SignalRService(NavigationManager navigationManager, IConfiguration configuration) : IAsyncDisposable
    {

        private readonly NavigationManager _navigationManager = navigationManager;
        private readonly string _apiBaseUrl = configuration["ApiBaseUrl"] ?? "https://localhost:7096";
        private HubConnection? _hubConnection;


        public async Task InitializeAsync()
        {
            if (_hubConnection != null)
                return;

            var hubUrl = new Uri($"{_apiBaseUrl.TrimEnd('/')}/posthub");

            _hubConnection = new HubConnectionBuilder()
                .WithUrl(hubUrl)
                .WithAutomaticReconnect()
                .Build();

            await _hubConnection.StartAsync();
        }

        public void OnViewCountUpdate(Action<int, int> handler)
        {
            _hubConnection?.On("ReceiveViewCountUpdate", handler);
        }

        public void OnNotification(Action<NotificationDto> handler)
        {
            _hubConnection?.On<NotificationDto>("ReceiveNotification", handler);
        }

        public void OnNotificationCount(Action<int, Guid?> handler)
        {
            _hubConnection?.On("ReceiveNewNotification", handler);
        }

        public void OnSentComment(Action<int, string> handler)
        {
            _hubConnection?.On("ReceiveSentMessage", handler);
        }

        public HubConnectionState? ConnectionState => _hubConnection?.State;

        public async ValueTask DisposeAsync()
        {
            if (_hubConnection != null)
            {
                await _hubConnection.DisposeAsync();
                _hubConnection = null;
            }
        }
    }
}
