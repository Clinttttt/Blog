using Blog.Application.Abstractions;
using Blog.Application.Common;
using Blog.Application.Common.Interfaces;
using Blog.Domain.Entities;
using BlogApi.Application.Common.Interfaces;
using BlogApi.Application.Queries.Posts;
using BlogApi.Domain.Common;
using BlogApi.Domain.Entities;
using BlogApi.Domain.Interfaces;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace BlogApi.Application.Commands.Posts.BookMark
{
    public class AddBookMarkCommandHandler : IRequestHandler<AddBookMarkCommand, Result<bool>>
    {
        private readonly IAppDbContext _context;
        private readonly ICacheInvalidationService _cacheInvalidation;
        private readonly INotificationService _notificationService;

        public AddBookMarkCommandHandler(
            IAppDbContext context,
            ICacheInvalidationService cacheInvalidation,
            INotificationService notificationService)
        {
            _context = context;
            _cacheInvalidation = cacheInvalidation;
            _notificationService = notificationService;
        }

        public async Task<Result<bool>> Handle(AddBookMarkCommand request, CancellationToken cancellationToken)
        {

            var bookmark = await _context.BookMarks.FirstOrDefaultAsync(s => s.PostId == request.PostId && s.UserId == request.UserId, cancellationToken);
            if (bookmark is null)
            {

                var entity = new Domain.Entities.BookMark
                {
                    PostId = request.PostId,
                    CreatedAt = DateTime.UtcNow.AddHours(8),
                    UserId = request.UserId
                };

                await _context.BookMarks.AddAsync(entity);
                await _context.SaveChangesAsync(cancellationToken);

                var post = await _context.Posts.FirstOrDefaultAsync(s => s.Id == request.PostId);
                if (post != null && request.UserId != post.UserId)
                {
                    var notification = new Notification
                    {
                        PostId = post.Id,
                        ActorUserId = request.UserId,
                        RecipientUserId = post.UserId,
                        Type = Domain.Enums.EntityEnum.Type.BookMark,
                        IsRead = false,
                        CreatedAt = DateTime.UtcNow.AddHours(8),
                    };
                    await _notificationService.NotificationAsync(notification, cancellationToken);
                }

            }
            else
            {
                _context.BookMarks.Remove(bookmark);
            }

            await _context.SaveChangesAsync(cancellationToken);
            await _cacheInvalidation.InvalidatePostListCachesAsync();
            await _cacheInvalidation.InvalidatePostCacheAsync(request.PostId);
            return Result<bool>.Success(true);
        }
    }
}
