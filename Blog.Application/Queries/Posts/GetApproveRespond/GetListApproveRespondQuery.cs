using BlogApi.Application.Models;
using BlogApi.Domain.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Application.Queries.Posts.GetApproveRespond
{
    public class GetListApproveRespondQuery : IRequest<Result<PagedResult<ApproveRespondDto>>>
    {
        public Guid UserId { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }


}
