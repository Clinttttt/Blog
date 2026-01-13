using BlogApi.Domain.Common;
using BlogApi.Domain.Enums;
using BlogApi.Domain.Interfaces;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Application.Commands.Notification.MarkAllNotificationAsRead
{
    public class MarkAllNotificationAsReadCommandHandler(IAppDbContext context) : IRequestHandler<MarkAllNotificationAsReadCommand, Result<bool>>
    {
        public async Task<Result<bool>> Handle(MarkAllNotificationAsReadCommand request, CancellationToken cancellationToken)
        {
            var list = await context.Notifications
                 .Where(s => s.RecipientUserId == request.UserId && s.IsRead == false && s.Type != EntityEnum.Type.PostApproval && s.Type != EntityEnum.Type.PostDecline).ToListAsync();

            foreach (var notif in list)
            {
                notif.IsRead = true;
            }
            await context.SaveChangesAsync(cancellationToken);
            return Result<bool>.Success(true);
        }
    }
}

