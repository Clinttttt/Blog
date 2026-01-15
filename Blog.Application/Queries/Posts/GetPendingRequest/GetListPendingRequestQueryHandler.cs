using Blog.Application.Common.Interfaces.Repositories;
using Blog.Application.Queries.Posts.GetAdminRequest;
using BlogApi.Application.Models;
using BlogApi.Domain.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Application.Queries.Posts.GetListAuthorRequest
{    
    public class GetListPendingRequestQueryHandler(IPostRespository respository) : IRequestHandler<GetListPendingRequestQuery, Result<PagedResult<PendingRequestDto>>>
    {
        public async Task<Result<PagedResult<PendingRequestDto>>> Handle(GetListPendingRequestQuery request, CancellationToken cancellationToken)
        {
            return await respository.GetPaginatedPendingAsync(request.filter, request.PageNumber, request.PageSize, cancellationToken);
        }
    }
}
