using BlogApi.Domain.Common;
using BlogApi.Domain.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Application.Commands.Notification.DeleteNotification
{
    public class DeleteNotificationCommandHandler(IAppDbContext context) : IRequestHandler<DeleteNotificationCommand, Result<bool>>
    {
        public async Task<Result<bool>> Handle(DeleteNotificationCommand request, CancellationToken cancellationToken)
        {
            var notif = await context.Notifications
                .FirstOrDefaultAsync(s => s.Id == request.notifId, cancellationToken);
            if (notif is null)
            {
                return Result<bool>.NotFound();
            }
            context.Notifications.Remove(notif);
            await context.SaveChangesAsync(cancellationToken);
            return Result<bool>.Success(true);
        }
    }
}
