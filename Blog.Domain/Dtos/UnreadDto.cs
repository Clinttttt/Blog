using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Application.Queries.Posts.GetApprovalTotal
{
    public class UnreadDto
    {
        public int? PendingCount { get; set; }
        public int? NotificationCount { get; set; }

    }
}
