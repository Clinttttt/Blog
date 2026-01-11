using BlogApi.Domain.Common;
using BlogApi.Domain.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Application.Commands.Notification.DeleteAllNotification
{
    public class DeleteAllNotificationCommandHandler(IAppDbContext context) : IRequestHandler<DeleteAllNotificationCommand, Result<bool>>
    {
        public async Task<Result<bool>> Handle(DeleteAllNotificationCommand request, CancellationToken cancellationToken)
        {
            var list = await context.Notifications
                .Where(s => s.RecipientUserId == request.UserId).ToListAsync();

            foreach (var notif in list)
            {
                 context.Notifications.Remove(notif);
            }
            await context.SaveChangesAsync(cancellationToken);
            return Result<bool>.Success(true);
        }
    }
}
