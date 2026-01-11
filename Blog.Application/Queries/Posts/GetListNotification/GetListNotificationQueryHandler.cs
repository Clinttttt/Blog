using BlogApi.Application.Models;
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
    public class GetListNotificationQueryHandler(IAppDbContext context) : IRequestHandler<GetListNotificationQuery, Result<PagedResult<GetListNotificationDto>>>
    {
        public async Task<Result<PagedResult<GetListNotificationDto>>> Handle(GetListNotificationQuery request, CancellationToken cancellationToken)
        {
            var entity = await context.Notifications
                .Include(s => s.User)
                .Where(s=> s.RecipientUserId == request.UserId)
                .AsNoTracking().ToListAsync();

            var notifcount = entity.Count();
            var selected = entity             
                 .OrderByDescending(s => s.CreatedAt)
                 .OrderByDescending(s => s.CreatedAt)
                 .Skip((request.PageNumber - 1) * request.PageSze)
                 .Take(request.PageSze)
                 .Select(s => new GetListNotificationDto
                 {
                     Id = s.Id,
                     Type = s.Type,
                     PostId = s.PostId,
                     UserName = s.User!.UserName,
                     CreatedAt = s.CreatedAt,
                     IsRead = s.IsRead
                 }).ToList();
              
            if (!selected.Any())
                return Result<PagedResult<GetListNotificationDto>>.NoContent();
          

            var dto = new PagedResult<GetListNotificationDto>
            {
                Items = selected,
                TotalCount = notifcount,
                PageNumber = request.PageNumber,
                PageSize = request.PageSze
            };

            return Result<PagedResult<GetListNotificationDto>>.Success(dto);
        }
    }
}
