using BlogApi.Domain.Common;
using BlogApi.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Application.Queries.Posts.GetApprovalTotal
{
    public class GetUnreadTotalQuery : IRequest<Result<UnreadDto>>
    {
        public Guid? UserId { get; set; }
        public Expression<Func<Post, bool>>? filter { get; set; } = null;

    }

}
