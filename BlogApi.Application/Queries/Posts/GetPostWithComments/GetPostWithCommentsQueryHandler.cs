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

namespace BlogApi.Application.Queries.Posts.GetPostWithComments
{
    public class GetPostWithCommentsQueryHandler : IRequestHandler<GetPostWithCommentsQuery, Result<PostWithCommentsDto>>
    {
        private readonly IAppDbContext _context;
        public GetPostWithCommentsQueryHandler(IAppDbContext context)
        {
            _context = context;
        }
        public async Task<Result<PostWithCommentsDto>> Handle(GetPostWithCommentsQuery request, CancellationToken cancellationToken)
        {
          
            var likecomment = await _context.CommentLikes
                .Where(s=> s.CommentId == s.CommentId)
                .CountAsync(cancellationToken);

            var postwithcomment = await _context.Posts
                .Include(s=> s.Category)
                .Include(s=> s.PostLikes)
             
                .Include(s=> s.PostTags)         
                .ThenInclude(s=> s.tag)
                .Include(s=> s.Comments)
                .ThenInclude(s=> s.User.ExternalLogins)
                 .AsNoTracking()
                 .Where(s => s.Id == request.PostId)
                 .Select(s => new PostWithCommentsDto
                 {
                     PostLike = s.PostLikes.Where(s=> s.PostId == request.PostId).Count(),
                     Title = s.Title,
                     Content = s.Content,
                     PostCreatedAt = s.CreatedAt,
                     CategoryName = s.Category.Name,
                     Photo = s.Photo,
                     PhotoContent = s.PhotoContent,
                     Author = s.Author,
                     readingDuration = s.readingDuration,
                     Tags = s.PostTags.Select(s => new TagDto
                     {
                         Id = s.TagId,
                         Name = s.tag != null ? s.tag.Name : null,

                     }).ToList(),
                     Comments = s.Comments
                     .Select(c => new CommentDto
                     {
                         LikeCount = likecomment,
                         CommentId = c.Id,
                         PostId = c.PostId,
                         Content = c.Content,
                         CreatedAt = c.CreatedAt,
                         UserName  = c.User.UserName,                       
                         PhotoUrl  = c.User.ExternalLogins.OrderByDescending
                         (s=> s.LinkedAt)
                         .Select(s=> s.ProfilePhotoUrl)
                         .FirstOrDefault()

                     }).ToList()
                 }).FirstOrDefaultAsync(cancellationToken);
            if (postwithcomment is null)
                return Result<PostWithCommentsDto>.NotFound();
            return Result<PostWithCommentsDto>.Success(postwithcomment);
        }
    }
}
