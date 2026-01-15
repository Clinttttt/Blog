using Blog.Application.Common.Interfaces.SignalR;
using Blog.Domain.Dtos;
using Blog.Infrastructure.SignalR.Posts;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Infrastructure.SignalR.Notifications
{
    public class NotificatonHubService : INotificatonHubService
    {
        private readonly IHubContext<NotificatonHub> _hubContext;

        public NotificatonHubService(IHubContext<NotificatonHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public async Task BroadcastNotificationCount(int Count, Guid? UserId)
        {
            await _hubContext.Clients.All.SendAsync("ReceiveNewNotification", Count, UserId);

        }
        public async Task BroadcastNotification(NotificationDto request, Guid? recipientUserId)
        {
            if (recipientUserId.HasValue)
                await _hubContext.Clients.Group($"user-{recipientUserId.Value}").SendAsync("ReceiveNotification", request);
            else
                await _hubContext.Clients.All.SendAsync("ReceiveNotification", request);
        }

    }
}
