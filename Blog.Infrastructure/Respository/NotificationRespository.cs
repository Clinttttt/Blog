using Blog.Application.Common.Interfaces;
using Blog.Application.Queries.Notification.GetListNotification;
using Blog.Domain.Entities;
using BlogApi.Application.Models;
using BlogApi.Domain.Common;
using BlogApi.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Infrastructure.Respository
{
    public class NotificationRespository(IAppDbContext context) : INotificationRespository
    {

        public async Task<Result<PagedResult<GetListNotificationDto>>> GetPaginatedNotificationAsync(int PageNumber, int PageSze, Expression<Func<Notification, bool>>? filter = null, CancellationToken cancellationToken = default)
        {

            IQueryable<Notification> entities = context.Notifications.AsNoTracking();
     
            if (filter != null)
            {
                entities = entities.Where(filter);
            }
            var notifcount = entities.Count();

            var selected = await entities
                 .OrderByDescending(s => s.CreatedAt)
                 .Skip((PageNumber - 1) * PageSze)
                 .Take(PageSze)
                 .Select(s => new GetListNotificationDto
                 {
                     Id = s.Id,
                     Type = s.Type,
                     PostId = s.PostId,
                     UserName = s.User!.UserName,
                     CreatedAt = s.CreatedAt,
                     IsRead = s.IsRead
                 }).ToListAsync();

            if (!selected.Any())
                return Result<PagedResult<GetListNotificationDto>>.NoContent();

            var dto = new PagedResult<GetListNotificationDto>
            {
                Items = selected,
                TotalCount = notifcount,
                PageNumber = PageNumber,
                PageSize = PageSze
            };

            return Result<PagedResult<GetListNotificationDto>>.Success(dto);
        }
    }
}
