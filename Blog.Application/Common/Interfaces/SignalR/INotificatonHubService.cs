using Blog.Domain.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Application.Common.Interfaces.SignalR
{
    public interface INotificatonHubService
    {
        Task BroadcastNotificationCount(int Count, Guid? UserId);
        Task BroadcastNotification(NotificationDto request, Guid? recipientUserId);


    }
}
