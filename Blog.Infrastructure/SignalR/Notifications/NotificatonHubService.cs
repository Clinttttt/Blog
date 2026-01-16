using Blog.Application.Common.Interfaces.SignalR;
using Blog.Domain.Dtos;
using Blog.Infrastructure.SignalR.Posts;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
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

        public async Task BroadcastPendingCountAuthor(int Count, Guid? authorUserId)
        {
            await _hubContext.Clients.All.SendAsync("ReceiveNewPendingAuthor", Count, authorUserId);
        }
        public async Task BroadcastPendingCountAdmin(int Count)
        {
           
                await _hubContext.Clients.All
                    .SendAsync("ReceiveNewPendingAdmin", Count);
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
