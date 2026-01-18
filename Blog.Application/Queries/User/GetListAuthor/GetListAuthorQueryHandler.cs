using Blog.Application.Common.Interfaces.Repositories;
using Blog.Application.Queries.User.GetListAuthor;
using BlogApi.Application.Dtos;
using BlogApi.Application.Models;
using BlogApi.Domain.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApi.Application.Queries.User.GetListAuthor
{
    public class GetListAuthorQueryHandler(IUserRespository  respository) : IRequestHandler<GetListAuthorQuery, Result<PagedResult<AuthorStatDto>>>
    {
        public async Task<Result<PagedResult<AuthorStatDto>>> Handle(GetListAuthorQuery request, CancellationToken cancellationToken)
        {
            return await respository.GetListing(request.pageNumber,request.pageSize, cancellationToken);     
        }
    }
}
