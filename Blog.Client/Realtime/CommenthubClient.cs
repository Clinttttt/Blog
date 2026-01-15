using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Reflection.Metadata.Ecma335;

namespace Blog.Client.Realtime
{
    public class CommenthubClient(NavigationManager navigationManager, IConfiguration configuration) : IAsyncDisposable
    {
        private readonly NavigationManager _navigationManager = navigationManager;
        private readonly string _apiBaseUrl = configuration["ApiBaseUrl"] ?? "https://localhost:7096";
        private HubConnection? _hubConnection;

        public async Task CommentInitializeAsync()
        {
            if (_hubConnection != null)
                return;

            var hubUrl = new Uri($"{_apiBaseUrl.TrimEnd('/')}/hubs/comments");

            _hubConnection = new HubConnectionBuilder()
                .WithUrl(hubUrl)
                .WithAutomaticReconnect()
                .Build();

            await _hubConnection.StartAsync();

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