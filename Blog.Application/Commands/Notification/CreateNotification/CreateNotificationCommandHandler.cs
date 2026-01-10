using BlogApi.Domain.Common;
using BlogApi.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Application.Commands.Notification.CreateNotification
{
    public class CreateNotificationCommandHandler(IAppDbContext context) : IRequestHandler<CreateNotificationCommand, Result<bool>>
    {
        public async Task<Result<bool>> Handle(CreateNotificationCommand request, CancellationToken cancellationToken)
        {
            var entity = new Domain.Entities.Notification
            {
                PostId = request.PostId,
                ActorUserId = request.ActorUserId,
                RecipientUserId = request.RecipientUserId,
                Type = request.Type,
                IsRead = false,
                CreatedAt = DateTime.UtcNow.AddHours(8),
            };
            context.Notifications.Add(entity);
            await context.SaveChangesAsync(cancellationToken);
            return Result<bool>.Success(true);
        }
    }
}
