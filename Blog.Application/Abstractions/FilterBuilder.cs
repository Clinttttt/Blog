using Blog.Application.Common.Interfaces.Utilities;
using Blog.Application.Queries.Notification.GetListNotification;
using Blog.Domain.Entities;
using BlogApi.Application.Queries.Posts;
using BlogApi.Domain.Entities;
using BlogApi.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using static Blog.Application.Queries.Notification.GetListNotification.GetListNotificationQuery;
using static BlogApi.Domain.Enums.EntityEnum;

namespace Blog.Application.Abstractions
{
    public class FilterBuilder : IFilterBuilder
    {
        public Expression<Func<Post, bool>>? PostBuildFilter(GetPagedPostsQuery request)
        {
            return request.FilterType switch
            {
                PostFilterType.Published =>
                    p => p.Status == Status.Published,

                PostFilterType.PublishedByUser =>
                    p => p.UserId == request.UserId && p.Status == Status.Published,

                PostFilterType.Drafts =>
                    p => p.Status == Status.Draft,

                PostFilterType.DraftsByUser =>
                    p => p.UserId == request.UserId && p.Status == Status.Draft,

                PostFilterType.PendingByUser =>
                    p => p.UserId == request.UserId && p.Status == Status.Pending,

                PostFilterType.ByCategory =>
                    p => p.CategoryId == request.CategoryId && p.Status != Status.Pending,

                PostFilterType.ByTag =>
                    p => p.PostTags.Any(pt => pt.TagId == request.TagId) && p.Status != Status.Pending,

                PostFilterType.BookMark =>
                   p => p.BookMarks.Any(s => s.UserId == request.UserId),

                PostFilterType.MostViewed => p => p.UserId == request.UserId && p.Status == Status.Published,

                PostFilterType.MostLiked => p => p.UserId == request.UserId && p.Status == Status.Published,

                PostFilterType.MostRecent => p => p.UserId == request.UserId && p.Status == Status.Published,

                _ => null
            };
            }

        public static IQueryable<Post> ApplyingFilter( IQueryable<Post> query, PostFilterType type)
        {
            return type switch
            {
                PostFilterType.MostViewed => query.OrderByDescending(s => s.ViewCount),

                PostFilterType.MostLiked => query.OrderByDescending(s => s.PostLikes.Count()),

                PostFilterType.Published or PostFilterType.PublishedByUser or PostFilterType.DraftsByUser
                or PostFilterType.Drafts or PostFilterType.BookMark or PostFilterType.ByCategory
                or PostFilterType.ByTag or PostFilterType.PendingByUser => query.OrderByDescending(s=> s.CreatedAt),

                _ => query
            };
        }
       

        public Expression<Func<Notification, bool>>? NotificationBuilderFilter(GetListNotificationQuery request)
        {
            return request.NotifTypes switch
            {
                NotifType.All =>
                 s => s.RecipientUserId == request.UserId
                 && s.Type != EntityEnum.Type.PostApproval
                 && s.Type != EntityEnum.Type.PostDecline,

                NotifType.Comments =>
                  s => s.RecipientUserId == request.UserId && s.Type == EntityEnum.Type.Comment,

                NotifType.Unread =>
                 s => s.RecipientUserId == request.UserId && s.IsRead == false,

                NotifType.Posts =>
                 s => s.RecipientUserId == request.UserId && s.Type == EntityEnum.Type.LikePost,

                _ => s => s.RecipientUserId == request.UserId
            };
        }
    }
}






