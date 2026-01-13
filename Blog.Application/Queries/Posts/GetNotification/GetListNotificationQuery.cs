using BlogApi.Application.Models;
using BlogApi.Domain.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Application.Queries.Notification.GetListNotification
{
    public class GetListNotificationQuery : IRequest<Result<PagedResult<GetListNotificationDto>>>
    {
        public Guid? UserId { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSze { get; set; } = 5;
        public NotifType NotifTypes { get; set; } = NotifType.All;
        public enum NotifType 
        {
            All,
            Comments,
            Posts,
            Unread
        }

    }




}
