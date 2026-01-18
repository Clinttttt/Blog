using Blog.Application.Queries.User.GetListAuthor;
using BlogApi.Application.Dtos;
using BlogApi.Application.Models;
using BlogApi.Domain.Common;
using BlogApi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Application.Common.Interfaces.Repositories
{
    public interface IUserRespository
    {
        Task<Result<PagedResult<AuthorStatDto>>> GetListing(int pageNumber = 1,
            int pageSize = 10,
            CancellationToken cancellationToken = default);

        Task<User> Get(Expression<Func<User, bool>> filter,
            CancellationToken cancellationToken = default);
        Task<List<AuthorDto>> Top5(CancellationToken cancellationToken = default);
    }
}
