using Blog.Application.Common.Interfaces.Repositories;
using Blog.Application.Queries.User.GetListAuthor;
using BlogApi.Application.Dtos;
using BlogApi.Application.Models;
using BlogApi.Domain.Common;
using BlogApi.Domain.Entities;
using BlogApi.Domain.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BlogApi.Infrastructure.Respository
{
    public class UserRespository(IAppDbContext context) : IUserRespository
    {
        public async Task<Result<PagedResult<AuthorStatDto>>> GetListing(int pageNumber = 1, int pageSize = 10, CancellationToken cancellationToken = default)
        {

            var users = await context.Users
              .AsNoTracking()
              .Where(u => u.Role == "Author")
              .OrderByDescending(u => u.Posts.Count)
              .Skip((pageNumber - 1) * pageSize)
              .Take(pageSize)
               .Select(u => new AuthorStatDto
               {
                   UserId = u.Id,
                   Name = u.UserName,
                   PostCount = u.Posts.Count,
                   CreatedAt = u.ExternalLogins
                     .OrderByDescending(l => l.LinkedAt)
                     .Select(l => l.LinkedAt)
                     .FirstOrDefault(),
                   ProfilePhoto = u.ExternalLogins
                            .FirstOrDefault(el => el.Provider == "Google" && el.ProfilePhotoBytes != null) != null
                        ? $"data:image/jpeg;base64,{Convert.ToBase64String(u.ExternalLogins.First(el => el.Provider == "Google").ProfilePhotoBytes!)}"
                        : string.Empty
               }).ToListAsync(cancellationToken);

            var totalcount = users.Count();

            return Result<PagedResult<AuthorStatDto>>.Success(new PagedResult<AuthorStatDto>
            {
                Items = users,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalCount = totalcount
            });
        }
        public async Task<List<AuthorDto>> Top5(CancellationToken cancellationToken = default)
        {
            return await context.Users
                .AsNoTracking()
                .OrderByDescending(s => s.Posts.Count)
                .Select(s => new AuthorDto
                {
                    UserId = s.Id,
                    Name = s.UserName,
                    PostCount = s.Posts.Count,
                }).Take(5)
                .ToListAsync(cancellationToken);          
        }
        public async Task<User> Get(Expression<Func<User, bool>> filter, CancellationToken cancellationToken = default)
        {
            IQueryable<User> user = context.Users.AsNoTracking();

            if (filter != null)
                user = user.Where(filter);

            return await user
                .AsNoTracking()
                .Include(s => s.Posts)
                .Include(s => s.UserInfo)
                .Include(s => s.ExternalLogins)
                .FirstOrDefaultAsync();
        }

    }
}
