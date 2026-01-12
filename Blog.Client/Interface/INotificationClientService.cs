using Blog.Application.Queries.Notification.GetListNotification;
using BlogApi.Application.Models;
using BlogApi.Application.Request.Posts;
using BlogApi.Domain.Common;

namespace Blog.Client.Interface
{
    public interface INotificationClientService
    {
   
        Task<Result<bool>> DeleteNotification(int notifId);
        Task<Result<PagedResult<GetListNotificationDto>>> GetListNotification(ListPaginatedRequest reques, GetListNotificationQuery.NotifType notiftype);
        Task<Result<PagedResult<GetListNotificationDto>>> GetCommentNotification(ListPaginatedRequest request);
        Task<Result<PagedResult<GetListNotificationDto>>> GetPostNotification(ListPaginatedRequest request);
        Task<Result<PagedResult<GetListNotificationDto>>> GetUnreadNotification(ListPaginatedRequest request);
        Task<Result<bool>> MarkNotificationAsRead(int notifId);
        Task<Result<bool>> MarkAllNotificationAsRead();
        Task<Result<bool>> DeleteAllNotification();
    }
}
