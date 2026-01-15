using Blog.Application.Common.Interfaces.Repositories;
using Blog.Application.Common.Interfaces.Utilities;
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

namespace Blog.Application.Queries.Notification.GetListNotification
{
    public class GetListNotificationQueryHandler(INotificationRespository respository, IFilterBuilder builder) : IRequestHandler<GetListNotificationQuery, Result<PagedResult<GetListNotificationDto>>>
    {
        public async Task<Result<PagedResult<GetListNotificationDto>>> Handle(GetListNotificationQuery request, CancellationToken cancellationToken)
        {

            var build = builder.NotificationBuilderFilter(request);

            var notification = await respository.GetPaginatedNotificationAsync(
                 request.PageNumber,
                 request.PageSze,
                 filter: build,
                 cancellationToken
                );

            return notification;
        }
    }
}
