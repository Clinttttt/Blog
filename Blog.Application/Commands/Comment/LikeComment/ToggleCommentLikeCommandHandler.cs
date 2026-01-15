using Blog.Application.Common.Interfaces.Services;
using Blog.Application.Common.Interfaces.Utilities;
using Blog.Domain.Entities;
using BlogApi.Domain.Common;
using BlogApi.Domain.Entities;
using BlogApi.Domain.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApi.Application.Commands.Comment.LikeComment
{
    public class ToggleCommentLikeCommandHandler : IRequestHandler<ToggleCommentLikeCommand, Result<bool>>
    {
        private readonly IAppDbContext _context;
        private readonly ICacheInvalidationService _cacheInvalidation;
        private readonly INotificationService _notificationService;

        public ToggleCommentLikeCommandHandler(
            IAppDbContext context,
            ICacheInvalidationService cacheInvalidation,
            INotificationService notificationService)
        {
            _context = context;
            _cacheInvalidation = cacheInvalidation;
            _notificationService = notificationService;
        }
        public async Task<Result<bool>> Handle(ToggleCommentLikeCommand request, CancellationToken cancellationToken)
        {
            var likecomment = await _context.CommentLikes
                .Include(s => s.Comments)
                .FirstOrDefaultAsync(s => s.CommentId == request.CommentId && s.UserId == request.UserId);

            if (likecomment is null)
            {
                var newLike = new CommentLike
                {
                    CommentId = request.CommentId,
                    UserId = request.UserId,
                    PostId = request.PostId
                };
                await _context.CommentLikes.AddAsync(newLike);
                await _context.SaveChangesAsync(cancellationToken);

                var comment = await _context.Comments.FirstOrDefaultAsync(c => c.Id == request.CommentId, cancellationToken);
                if (comment != null && comment.UserId != request.UserId)
                {
                    var notification = new Notification
                    {
                        PostId = comment.PostId,
                        ActorUserId = request.UserId,
                        RecipientUserId = comment.UserId,
                        Type = Domain.Enums.EntityEnum.Type.LikeComment,
                        IsRead = false,
                        CreatedAt = DateTime.UtcNow.AddHours(8),
                    };

                    await _notificationService.NotificationAsync(notification, cancellationToken);
                }
            }
            else
            {
                _context.CommentLikes.Remove(likecomment);
            }
            await _context.SaveChangesAsync(cancellationToken);
            await _cacheInvalidation.InvalidatePostCacheAsync(request.PostId);
            return Result<bool>.Success(true);
        }

    }
}
