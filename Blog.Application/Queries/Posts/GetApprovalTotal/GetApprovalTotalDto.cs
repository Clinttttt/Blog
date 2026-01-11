using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Application.Queries.Posts.GetApprovalTotal
{
    public class GetApprovalTotalDto
    {
        public int? ApprovalCount { get; set; }
        public int? NotificationCount { get; set; }

    }
}
