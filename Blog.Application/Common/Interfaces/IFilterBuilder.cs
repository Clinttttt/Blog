using Blog.Application.Queries.Notification.GetListNotification;
using Blog.Domain.Entities;
using BlogApi.Application.Queries.Posts;
using BlogApi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Application.Common.Interfaces
{
    public interface IFilterBuilder
    {
        Expression<Func<Post, bool>>? PostBuildFilter(GetPagedPostsQuery request);
        Expression<Func<Notification, bool>>? NotificationBuilderFilter(GetListNotificationQuery request);
    }
}
