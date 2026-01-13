using Blog.Application.Queries.Posts.GetAdminRequest;
using BlogApi.Application.Models;
using BlogApi.Domain.Common;
using BlogApi.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Application.Queries.Posts.GetListAuthorRequest
{
    public class GetListPendingRequestQuery() : IRequest<Result<PagedResult<PendingRequestDto>>>
    {
  
        public Expression<Func<Post, bool>>? filter { get; set; } = null;
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}

