using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Logging;
using BlogApi.Client.Security;
using BlogApi.Client.Common.Auth;

namespace Blog.Client.Realtime
{
    public abstract class HubClientBase : IAsyncDisposable
    {
        protected readonly NavigationManager _navigationManager;
        protected readonly string _apiBaseUrl;
        protected readonly ITokenService _tokenService;
        protected readonly ILogger<HubClientBase> _logger;

        private HubConnection? _hubConnection;
        private readonly SemaphoreSlim _initLock = new(1, 1);
        private bool _disposed;

        public event Action? OnReconnecting;
        public event Action<string?>? OnReconnected;
        public event Action<Exception?>? OnClosed;

        public HubConnectionState? ConnectionState => _hubConnection?.State;
        public bool IsConnected => _hubConnection?.State == HubConnectionState.Connected;

        protected HubClientBase(
            NavigationManager navigationManager,
            IConfiguration configuration,
            ITokenService tokenService,
            ILogger<HubClientBase> logger)
        {
            _navigationManager = navigationManager;
            _apiBaseUrl = configuration["ApiBaseUrl"] ?? "https://localhost:7096";
            _tokenService = tokenService;
            _logger = logger;
        }

        protected async Task InitializeAsync(string hubRelativePath)
        {
            // Check if disposed before acquiring lock
            if (_disposed)
            {
                _logger?.LogWarning("Attempted to initialize already disposed hub");
                return;
            }

            await _initLock.WaitAsync();
            try
            {
                // Double-check after acquiring lock
                if (_disposed)
                {
                    _logger?.LogWarning("Hub was disposed while waiting for lock");
                    return;
                }

                if (_hubConnection != null)
                {
                    _logger?.LogDebug("Hub already initialized");
                    return;
                }

                var hubUrl = $"{_apiBaseUrl.TrimEnd('/')}/{hubRelativePath.TrimStart('/')}";
                _logger?.LogInformation("Initializing SignalR hub: {HubUrl}", hubUrl);

                _hubConnection = new HubConnectionBuilder()
                    .WithUrl(hubUrl, options =>
                    {
                        options.AccessTokenProvider = () => _tokenService.GetAccessTokenAsync();
                    })
                    .WithAutomaticReconnect(new[] { TimeSpan.Zero, TimeSpan.FromSeconds(2), TimeSpan.FromSeconds(10) })
                    .Build();

                RegisterEventHandlers();

                await _hubConnection.StartAsync();
                _logger?.LogInformation("SignalR hub connected successfully");
            }
            catch (ObjectDisposedException)
            {
                _logger?.LogWarning("Hub connection was disposed during initialization");
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "Failed to initialize SignalR hub");
                throw;
            }
            finally
            {
                if (!_disposed)
                {
                    _initLock.Release();
                }
            }
        }

        private void RegisterEventHandlers()
        {
            if (_hubConnection == null) return;

            _hubConnection.Reconnecting += error =>
            {
                _logger?.LogWarning(error, "SignalR reconnecting...");
                OnReconnecting?.Invoke();
                return Task.CompletedTask;
            };

            _hubConnection.Reconnected += connectionId =>
            {
                _logger?.LogInformation("SignalR reconnected. ConnectionId: {ConnectionId}", connectionId);
                OnReconnected?.Invoke(connectionId);
                return Task.CompletedTask;
            };

            _hubConnection.Closed += error =>
            {
                _logger?.LogWarning(error, "SignalR connection closed");
                OnClosed?.Invoke(error);
                return Task.CompletedTask;
            };
        }

        protected void Subscribe(string method, Action handler)
        {
            EnsureConnected();
            _hubConnection!.On(method, handler);
            _logger?.LogDebug("Subscribed to method: {Method}", method);
        }

        protected void Subscribe<T>(string method, Action<T> handler)
        {
            EnsureConnected();
            _hubConnection!.On(method, handler);
            _logger?.LogDebug("Subscribed to method: {Method}", method);
        }

        protected void Subscribe<T1, T2>(string method, Action<T1, T2> handler)
        {
            EnsureConnected();
            _hubConnection!.On(method, handler);
            _logger?.LogDebug("Subscribed to method: {Method}", method);
        }

        protected void Subscribe<T1, T2, T3>(string method, Action<T1, T2, T3> handler)
        {
            EnsureConnected();
            _hubConnection!.On(method, handler);
            _logger?.LogDebug("Subscribed to method: {Method}", method);
        }

        protected async Task InvokeAsync(string method, params object?[] args)
        {
            EnsureConnected();
            await _hubConnection!.InvokeAsync(method, args);
            _logger?.LogDebug("Invoked method: {Method}", method);
        }

        protected async Task<T> InvokeAsync<T>(string method, params object?[] args)
        {
            EnsureConnected();
            var result = await _hubConnection!.InvokeAsync<T>(method, args);
            _logger?.LogDebug("Invoked method: {Method}", method);
            return result;
        }

        private void EnsureConnected()
        {
            if (_disposed)
                throw new ObjectDisposedException(nameof(HubClientBase));

            if (_hubConnection == null)
                throw new InvalidOperationException("Hub not initialized. Call InitializeAsync first.");
        }

        public async ValueTask DisposeAsync()
        {
            if (_disposed) return;

            _disposed = true;

            _logger?.LogDebug("Disposing hub connection");

            OnReconnecting = null;
            OnReconnected = null;
            OnClosed = null;

            if (_hubConnection != null)
            {
                try
                {
                    await _hubConnection.DisposeAsync();
                }
                catch (Exception ex)
                {
                    _logger?.LogWarning(ex, "Error disposing hub connection");
                }
                _hubConnection = null;
            }

            try
            {
                _initLock.Dispose();
            }
            catch (ObjectDisposedException)
            {
                // Already disposed, ignore
            }

            GC.SuppressFinalize(this);
        }
    }
}