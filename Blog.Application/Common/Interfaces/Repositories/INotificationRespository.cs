using Blog.Application.Queries.Notification.GetListNotification;
using Blog.Application.Queries.Posts.GetApprovalTotal;
using Blog.Domain.Entities;
using BlogApi.Application.Models;
using BlogApi.Domain.Common;
using BlogApi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Application.Common.Interfaces.Repositories
{
    public interface INotificationRespository
    {
        Task<Result<PagedResult<GetListNotificationDto>>> GetPaginatedNotificationAsync
            (int PageNumber, 
            int PageSze,
            Expression<Func<Notification, bool>>? filter = null,
            CancellationToken cancellationToken = default);

        Task<Result<UnreadDto>> GetunreadAsync(Guid? UserId, 
            Expression<Func<Post, bool>>? filter = null,
            CancellationToken cancellationToken = default);


    }
}
