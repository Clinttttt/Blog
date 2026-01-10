using BlogApi.Domain.Common;
using BlogApi.Domain.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Application.Queries.Posts.GetListNotification
{
    public class GetListNotificationQueryHandler(IAppDbContext context) : IRequestHandler<GetListNotificationQuery, Result<List<GetListNotificationDto>>>
    {
        public async Task<Result<List<GetListNotificationDto>>> Handle(GetListNotificationQuery request, CancellationToken cancellationToken)
        {
            var notif = await context.Notifications
                 .Where(s => s.IsRead == false && s.RecipientUserId == request.UserId)
                 .OrderByDescending(s=> s.CreatedAt)
                 .Include(s=> s.User)
                 .Select(s=> new GetListNotificationDto
                 {
                     Id = s.Id,
                     Type = s.Type,
                     PostId = s.PostId,
                     UserName = s.User!.UserName,
                     CreatedAt = s.CreatedAt
                 })
                 .ToListAsync();
            if (!notif.Any())
                return Result<List<GetListNotificationDto>>.NoContent();

            return Result<List<GetListNotificationDto>>.Success(notif);
        }
    }
}
