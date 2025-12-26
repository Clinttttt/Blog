using BlogApi.Application.Dtos;
using BlogApi.Domain.Common;
using BlogApi.Domain.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApi.Application.Queries.Posts.GetAllBookMark
{
    public class GetAllBookMarkQueryHandler(IAppDbContext context) : IRequestHandler<GetAllBookMarkQuery, Result<List<PostDto>>>
    {

        public async Task<Result<List<PostDto>>> Handle(GetAllBookMarkQuery request, CancellationToken cancellationToken)
        {
            var bookmark = await context.BookMarks
                 .AsNoTracking()
                 .Where(s => s.UserId == request.UserId)
                 .Include(s => s.Post)
                 .ThenInclude(s => s.PostTags)
                 .Include(s => s.Post)
                 .ThenInclude(s => s.Category)
                 .Select(s => new PostDto
                 {
                     PostId = s.PostId,
                     IsBookMark = s.Post.BookMarks.Any(s=> s.UserId == request.UserId),
                     Title = s.Post.Title,
                     Content = s.Post.Content,
                     CreatedAt = s.Post.CreatedAt,
                     CategoryName = s.Post.Category.Name,
                     readingDuration = s.Post.readingDuration,
                     tags = s.Post.PostTags.Select(s => new TagDto
                     {
                         Id = s.TagId,
                         Name = s.tag != null ? s.tag.Name : "N/A",

                     }).ToList(),
                 }).ToListAsync();
            return Result<List<PostDto>>.Success(bookmark);
        }
    }

}
