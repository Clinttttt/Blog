using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Application.Common.Interfaces.SignalR
{
    public interface ICommentHubService
    {
        Task BroadcastSentComment(int PostId, string Content);
    }
}
