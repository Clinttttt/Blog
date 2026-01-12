using Blog.Application.Common.Interfaces;
using Blog.Domain.Dtos;
using Blog.Infrastructure.Hubs;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Infrastructure.Hubs.HubService
{
    public class PostHubService : IPostHubService
    {
        private readonly IHubContext<PostHub> _hubContext;

        public PostHubService(IHubContext<PostHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public async Task BroadcastViewCountUpdate(int postId, int viewCount)
        {
            await _hubContext.Clients.All.SendAsync("ReceiveViewCountUpdate", postId, viewCount);
        }
        public async Task BroadcastSentComment(int PostId, string Content)
        {
            await _hubContext.Clients.All.SendAsync("ReceiveSentMessage", PostId, Content);
        }

        public async Task BroadcastNotification(NotificationDto request, Guid? recipientUserId)
        {
            await _hubContext.Clients.All.SendAsync("ReceiveNotification", request);
            
        }

        public async Task BroadcastNotificationCount(int Count, Guid? UserId)
        {
            await _hubContext.Clients.All.SendAsync("ReceiveNewNotification", Count, UserId);

        }
    }
}
