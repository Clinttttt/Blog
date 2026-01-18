using Blog.Application.Common.Interfaces.Repositories;
using BlogApi.Application.Dtos;
using BlogApi.Domain.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Application.Queries.User.FilterBy.GetTop5
{
    public class GetTop5QueryHandler(IUserRespository respository) : IRequestHandler<GetTop5Query, Result<List<AuthorDto>>>
    {
        public async Task<Result<List<AuthorDto>>> Handle(GetTop5Query request, CancellationToken cancellationToken)
        {
            var user = await respository.Top5(cancellationToken);
            if (!user.Any())
            {
                return Result<List<AuthorDto>>.NoContent();
            }
            return Result<List<AuthorDto>>.Success(user);
        }
    }
}
