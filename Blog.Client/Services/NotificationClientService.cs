using Blog.Application.Queries.Notification.GetListNotification;
using Blog.Application.Queries.Posts.GetApproveRespond;
using Blog.Client.Interface;
using BlogApi.Application.Models;
using BlogApi.Application.Request.Posts;
using BlogApi.Client.Helper;
using BlogApi.Domain.Common;
using Microsoft.Identity.Client;

namespace Blog.Client.Services
{
    public class NotificationClientService(HttpClient httpClient) : HandleResponse(httpClient), INotificationClientService
    {
        public async Task<Result<bool>> DeleteNotification(int notifId)
            => await DeleteAsync<bool>($"api/Notification/DeleteNotification?notifId={notifId}");

        public async Task<Result<PagedResult<GetListNotificationDto>>> GetListNotification(ListPaginatedRequest request, GetListNotificationQuery.NotifType notiftype)
            => await GetAsync<PagedResult<GetListNotificationDto>>($"api/Notification/GetListNotification?PageNumber={request.PageNumber}&PageSize={request.PageSize}&notiftype={notiftype}");

        public async Task<Result<PagedResult<GetListNotificationDto>>> GetCommentNotification(ListPaginatedRequest request)
            => await GetAsync<PagedResult<GetListNotificationDto>>($"api/Notification/GetCommentNotification?PageNumber={request.PageNumber}&PageSize={request.PageSize}");

        public async Task<Result<PagedResult<GetListNotificationDto>>> GetPostNotification(ListPaginatedRequest request)
            => await GetAsync<PagedResult<GetListNotificationDto>>($"api/Notification/GetPostNotification?PageNumber={request.PageNumber}&PageSize={request.PageSize}");

        public async Task<Result<PagedResult<GetListNotificationDto>>> GetUnreadNotification(ListPaginatedRequest request)
            => await GetAsync<PagedResult<GetListNotificationDto>>($"api/Notification/GetUnreadNotification?PageNumber={request.PageNumber}&PageSize={request.PageSize}");

        public async Task<Result<PagedResult<ApproveRespondDto>>> GetListApproveRespond(ListPaginatedRequest request)
            => await GetAsync<PagedResult<ApproveRespondDto>>($"api/Notification/GetListApproveRespond?PageNumber={request.PageNumber}&PageSize={request.PageSize}");
        public async Task<Result<bool>> MarkNotificationAsRead(int notifId)
            => await UpdateAsync<bool>($"api/Notification/MarkNotificationAsRead?notifId={notifId}");

        public async Task<Result<bool>> MarkAllNotificationAsRead()
            => await UpdateAsync<bool>("api/Notification/MarkAllNotificationAsRead");

        public async Task<Result<bool>> DeleteAllNotification()
           => await DeleteAsync<bool>("api/Notification/DeleteAllNotification");
    }
}
