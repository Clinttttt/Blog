using Blog.Application.Queries.Posts.GetAdminRequest;
using BlogApi.Application.Common.Interfaces;
using BlogApi.Application.Models;
using BlogApi.Domain.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Application.Queries.Posts.GetListAdminRequestQueryHandler
{
    public class GetListAdminRequestQueryHandler(IPostRespository respository) : IRequestHandler<GetListAdminRequestQuery, Result<PagedResult<GetListAdminRequestDto>>>
    {
        public async Task<Result<PagedResult<GetListAdminRequestDto>>> Handle(GetListAdminRequestQuery request, CancellationToken cancellationToken)
        {
          return await respository.GetListAdminRequest(request.PageNumber, request.PageSize, cancellationToken);
        }
    }
}
