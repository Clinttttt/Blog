using Blog.Application.Common.Interfaces;
using Blog.Domain.Dtos;
using Blog.Domain.Entities;
using BlogApi.Domain.Common;
using BlogApi.Domain.Entities;
using BlogApi.Domain.Enums;
using BlogApi.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Application.Abstractions
{
    public class NotificationService(IAppDbContext context, IPostHubService hubService) : INotificationService
    {


        public async Task<Result<bool>> NotificationAsync(Notification request, CancellationToken cancellationToken = default)
        {

            bool exists = await context.Notifications.AnyAsync(n =>
                n.PostId == request.PostId &&
                n.ActorUserId == request.ActorUserId &&
                n.RecipientUserId == request.RecipientUserId &&
                n.Type == request.Type,
                cancellationToken);

            if (!exists)
            {
                await context.Notifications.AddAsync(request, cancellationToken);
                await context.SaveChangesAsync(cancellationToken);

            }

            var user = await context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(s => s.Id == request.ActorUserId, cancellationToken);

            await hubService.BroadcastNotification(
                      new NotificationDto
                      {
                          PostId = request.PostId,
                          ActorUserId = request.ActorUserId,
                          Type = request.Type,
                          CreatedAt = request.CreatedAt,
                          ActorName = user?.UserName,
                          RecipientUserId = request.RecipientUserId
                      },
                      request.RecipientUserId);


            var approval = new[] { EntityEnum.Type.PostApproval, EntityEnum.Type.PostDecline };
        
            var num = await context.Notifications
            .CountAsync(n => n.RecipientUserId == request.RecipientUserId && n.IsRead == false && !approval.Contains(n.Type), cancellationToken);


            await hubService.BroadcastNotificationCount(num, request.RecipientUserId);

            return Result<bool>.Success(true);
        }

    }
}
