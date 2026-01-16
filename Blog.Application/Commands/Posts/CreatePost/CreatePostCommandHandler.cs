using Blog.Application.Abstractions;
using Blog.Application.Common;
using Blog.Application.Common.Interfaces.SignalR;
using Blog.Application.Common.Interfaces.Utilities;
using BlogApi.Domain.Common;
using BlogApi.Domain.Entities;
using BlogApi.Domain.Interfaces;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using static BlogApi.Domain.Enums.EntityEnum;

namespace BlogApi.Application.Commands.Posts.CreatePost
{
    public class CreatePostCommandHandler(IAppDbContext context, ICacheInvalidationService cacheInvalidation, INotificatonHubService notificatonHub) : IRequestHandler<CreatePostCommand, Result<int>>
    {
        public async Task<Result<int>> Handle(CreatePostCommand request, CancellationToken cancellationToken)
        {

            var user = await context.Users
                .FirstOrDefaultAsync(s => s.Id == request.UserId, cancellationToken);

            if (user is null)
                return Result<int>.NotFound();

            if (user.Role != "Author" && user.Role != "Admin")
                return Result<int>.Failure("Login first");

            var postStatus = user.Role == "Author"
                ? Status.Pending
                : request.Status;

            var post = new Post
            {
                Title = request.Title,
                Content = request.Content,
                UserId = request.UserId,
                CreatedAt = DateTime.UtcNow.AddHours(8),
                CategoryId = request.CategoryId,
                Photo = request.Photo,
                PhotoContent = request.PhotoContent,
                Author = request.Author,
                Status = postStatus,
                readingDuration = request.readingDuration
            };

            foreach (var tagId in request.TagIds.Distinct())
            {
                post.PostTags.Add(new PostTag
                {
                    TagId = tagId,
                    UserId = request.UserId
                });
            }
            context.Posts.Add(post);
            await context.SaveChangesAsync();

            var pendingCounts = await context.Posts
                .GroupBy(s => s.UserId)
                .Select(s => new
                {
                    UserId = s.Key,
                    Count = s.Where(s => s.Status == Status.Pending).Count(),

                }).ToListAsync(cancellationToken);

            var AdminCounts = pendingCounts.Sum(s => s.Count);
            var AuthorCounts = pendingCounts.FirstOrDefault(s => s.UserId == request.UserId)?.Count ?? 0;

            await notificatonHub.BroadcastPendingCountAuthor(AuthorCounts, request.UserId);
            await notificatonHub.BroadcastPendingCountAdmin(AdminCounts);

            await Task.WhenAll(
               cacheInvalidation.InvalidatePostListCachesAsync(),
               cacheInvalidation.InvalidateActivityCacheAsync(),
               cacheInvalidation.InvalidateTagsCacheAsync(),
               cacheInvalidation.InvalidateCategoryCacheAsync());

            return Result<int>.Success(post.Id);
        }
    }
}