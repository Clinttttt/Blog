using Blog.Application.Common.Interfaces;
using Blog.Domain.Entities;
using BlogApi.Domain.Common;
using BlogApi.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Application.Abstractions
{
    public class NotificationService(IAppDbContext context) : INotificationService
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

            return Result<bool>.Success(true);
        }

    }
}
