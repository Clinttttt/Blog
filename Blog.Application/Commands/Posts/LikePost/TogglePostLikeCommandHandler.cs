using Blog.Application.Abstractions;
using Blog.Application.Common;
using Blog.Application.Common.Interfaces;
using Blog.Domain.Entities;
using BlogApi.Domain.Common;
using BlogApi.Domain.Entities;
using BlogApi.Domain.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace BlogApi.Application.Commands.Posts.LikePost
{
    public class TogglePostLikeCommandHandler : IRequestHandler<TogglePostLikeCommand, Result<bool>>
    {
        private readonly IAppDbContext _context;
        private readonly ICacheInvalidationService _cacheInvalidation;
        private readonly INotificationService _notificationService;

        public TogglePostLikeCommandHandler(
            IAppDbContext context,
            ICacheInvalidationService cacheInvalidation,
            INotificationService notificationService)
        {
            _context = context;
            _cacheInvalidation = cacheInvalidation;
            _notificationService = notificationService;
        }

        public async Task<Result<bool>> Handle(TogglePostLikeCommand request, CancellationToken cancellationToken)
        {

            var like = await _context.PostLikes
                .FirstOrDefaultAsync(
                s => s.PostId == request.PostId && s.UserId == request.UserId,
                cancellationToken);

            if (like is null)
            {
                var entity = new PostLike
                {
                    PostId = request.PostId,
                    UserId = request.UserId,
                    CreatedAt = DateTime.UtcNow.AddHours(8)
                };
                await _context.PostLikes.AddAsync(entity);
                await _context.SaveChangesAsync(cancellationToken);

                var post = await _context.Posts.FirstOrDefaultAsync(s => s.Id == request.PostId,cancellationToken);
                if (post != null && request.UserId != post.UserId)
                {
                    var notification = new Notification
                    {
                        PostId = post.Id,
                        ActorUserId = request.UserId,
                        RecipientUserId = post.UserId,
                        Type = Domain.Enums.EntityEnum.Type.LikePost,
                        IsRead = false,
                        CreatedAt = DateTime.UtcNow.AddHours(8),
                    };

                    await _notificationService.NotificationAsync(notification, cancellationToken);
                }
            }
            else
            {
                _context.PostLikes.Remove(like);
            }

            await _context.SaveChangesAsync(cancellationToken);
            await _cacheInvalidation.InvalidatePostListCachesAsync();
            await _cacheInvalidation.InvalidatePostCacheAsync(request.PostId);

            return Result<bool>.Success(true);
        }
    }
}
