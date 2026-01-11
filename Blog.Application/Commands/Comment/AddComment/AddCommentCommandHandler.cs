using Blog.Application.Abstractions;
using Blog.Application.Common.Interfaces;
using Blog.Domain.Entities;
using BlogApi.Domain.Common;
using BlogApi.Domain.Entities;
using BlogApi.Domain.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApi.Application.Commands.Comment.AddComment
{
    public class AddCommentCommandHandler : IRequestHandler<AddCommentCommand, Result<int>>
    {
        private readonly IAppDbContext _context;
        private readonly IPostHubService _hubService;
        private readonly ICacheInvalidationService _cacheInvalidation;
        private readonly INotificationService _notification;

        public AddCommentCommandHandler(IAppDbContext context, IPostHubService hubService, ICacheInvalidationService cacheInvalidation, INotificationService notification)
        {
            _context = context;
            _hubService = hubService;
            _cacheInvalidation = cacheInvalidation;
            _notification = notification;
        }

        public async Task<Result<int>> Handle(AddCommentCommand request, CancellationToken cancellationToken)
        {

            var post = await _context.Posts.FirstOrDefaultAsync(s => s.Id == request.PostId, cancellationToken);

            if (post is null)
                return Result<int>.NotFound();

            var comment = new BlogApi.Domain.Entities.Comment
            {
                UserId = request.UserId,
                Content = request.Content,
                CreatedAt = DateTime.UtcNow.AddHours(8),
                PostId = request.PostId,
            };

            _context.Comments.Add(comment);

            if (post.UserId != request.UserId)
            {
                var notification = new Notification
                {
                    PostId = post.Id,
                    ActorUserId = request.UserId,
                    RecipientUserId = post.UserId,
                    Type = Domain.Enums.EntityEnum.Type.Comment,
                    IsRead = false,
                    CreatedAt = DateTime.UtcNow.AddHours(8),
                };

                await _notification.NotificationAsync(notification, cancellationToken);
            }

            await _context.SaveChangesAsync(cancellationToken);
            await _hubService.BroadcastSentComment(request.PostId, request.Content);
            await _cacheInvalidation.InvalidatePostCacheAsync(request.PostId);

            return Result<int>.Success(comment.Id);
        }
    }
}