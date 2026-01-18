using Blog.Application.Abstractions;
using Blog.Application.Common;
using Blog.Application.Common.Interfaces;
using Blog.Application.Common.Interfaces.Utilities;
using BlogApi.Domain.Common;
using BlogApi.Domain.Entities;
using BlogApi.Domain.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace BlogApi.Application.Commands.Posts.UpdatePost
{
    public class UpdatePostCommandHandler(IAppDbContext context, ICacheInvalidationService cacheInvalidation) : IRequestHandler<UpdatePostCommand, Result<int>>
    {
        public async Task<Result<int>> Handle(UpdatePostCommand request, CancellationToken cancellationToken)
        {

            var post = await context.Posts.FirstOrDefaultAsync(s => s.Id == request.PostId && s.UserId == request.UserId, cancellationToken);

            if (post is null)
                return Result<int>.NotFound();

            post.Title = request.Title;
            post.Content = request.Content;
            post.Photo = request.Photo;
            post.PhotoContent = request.PhotoContent;
            post.Author = request.Author;
            post.CategoryId = request.CategoryId;
            post.readingDuration = request.readingDuration;
            post.Status = request.Status;

            await context.SaveChangesAsync(cancellationToken);
            await Task.WhenAll(
             cacheInvalidation.InvalidatePostListCachesAsync(),
             cacheInvalidation.InvalidateActivityCacheAsync(),
             cacheInvalidation.InvalidateTagsCacheAsync(),
             cacheInvalidation.InvalidateCategoryCacheAsync(),
             cacheInvalidation.InvalidatePostCacheAsync(request.PostId));
            return Result<int>.Success(post.Id);
        }
    }
}
