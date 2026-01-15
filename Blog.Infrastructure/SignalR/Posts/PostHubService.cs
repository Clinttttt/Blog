using Blog.Application.Common.Interfaces.SignalR;
using Blog.Domain.Dtos;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Infrastructure.SignalR.Posts
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

      

      
    }
}
