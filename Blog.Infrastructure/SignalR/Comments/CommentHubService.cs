using Blog.Application.Common.Interfaces.SignalR;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Infrastructure.SignalR.Comments
{
    public class CommentHubService : ICommentHubService
    {
        private readonly IHubContext<CommentHub> _hubContext;

        public CommentHubService(IHubContext<CommentHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public async Task BroadcastSentComment(int PostId, string Content)
        {
            await _hubContext.Clients.All.SendAsync("ReceiveSentMessage", PostId, Content);
        }
    }
}
