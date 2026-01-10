using BlogApi.Domain.Common;
using BlogApi.Domain.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Application.Commands.Notification.MarkNotificationAsRead
{
    public class MarkNotificationAsReadCommandHandler(IAppDbContext context) : IRequestHandler<MarkNotificationAsReadCommand, Result<bool>>
    {
        public async Task<Result<bool>> Handle(MarkNotificationAsReadCommand request, CancellationToken cancellationToken)
        {
            var notif = await context.Notifications.FirstOrDefaultAsync(s => s.Id == request.notifId && s.IsRead == false);
            if (notif is null)
                return Result<bool>.NotFound();

            notif.IsRead = true;
            await context.SaveChangesAsync(cancellationToken);
            return Result<bool>.Success(true);

        }
    }
}
