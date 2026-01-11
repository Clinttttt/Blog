using Blog.Application.Queries.Posts.GetListNotification;
using BlogApi.Application.Models;
using BlogApi.Application.Request.Posts;
using BlogApi.Domain.Common;

namespace Blog.Client.Interface
{
    public interface INotificationClientService
    {
   
        Task<Result<bool>> DeleteNotification(int notifId);
        Task<Result<PagedResult<GetListNotificationDto>>> GetListNotification(ListPaginatedRequest request);
        Task<Result<bool>> MarkNotificationAsRead(int notifId);
        Task<Result<bool>> MarkAllNotificationAsRead();
        Task<Result<bool>> DeleteAllNotification();
    }
}
