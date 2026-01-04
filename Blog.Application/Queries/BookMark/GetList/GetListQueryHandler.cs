using BlogApi.Application.Common.Interfaces;
using BlogApi.Application.Dtos;
using BlogApi.Application.Models;
using BlogApi.Domain.Common;
using BlogApi.Domain.Enums;
using BlogApi.Domain.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace BlogApi.Application.Queries.BookMark.GetAllBookMark
{
    public class GetListQueryHandler(IPostRespository respository) : IRequestHandler<GetListQuery, Result<PagedResult<PostDto>>>
    {

        public async Task<Result<PagedResult<PostDto>>> Handle(GetListQuery request, CancellationToken cancellationToken)
        {
            var postpage = await respository.GetPaginatedPostDtoAsync(
               request.UserId,
               request.PageNumber,
               request.PageSize,
               filter: b => b.BookMarks.Any(s => s.UserId == request.UserId),
               cancellationToken);

            if (!postpage.Value!.Items.Any())
                return Result<PagedResult<PostDto>>.NoContent();
            return Result<PagedResult<PostDto>>.Success(postpage.Value);
        }
    }
}
