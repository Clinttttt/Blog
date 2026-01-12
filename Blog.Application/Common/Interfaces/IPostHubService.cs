using Blog.Domain.Dtos;
using BlogApi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Application.Common.Interfaces
{
    public interface IPostHubService
    {
        Task BroadcastViewCountUpdate(int postId, int viewCount);
        Task BroadcastSentComment(int PostId, string Content);
        Task BroadcastNotification(NotificationDto request, Guid? recipientUserId);
        Task BroadcastNotificationCount(int Count, Guid? UserId);
    }
}
