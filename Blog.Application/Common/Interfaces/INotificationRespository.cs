using Blog.Application.Queries.Notification.GetListNotification;
using Blog.Domain.Entities;
using BlogApi.Application.Models;
using BlogApi.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Application.Common.Interfaces
{
    public interface INotificationRespository
    {
        Task<Result<PagedResult<GetListNotificationDto>>> GetPaginatedNotificationAsync
            (int PageNumber, 
            int PageSze,
            Expression<Func<Notification, bool>>? filter = null,
            CancellationToken cancellationToken = default);


    }
}
