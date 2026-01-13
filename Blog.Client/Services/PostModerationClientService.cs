using Blog.Application.Queries.Posts.GetAdminRequest;
using Blog.Application.Queries.Posts.GetApprovalTotal;
using BlogApi.Application.Models;
using BlogApi.Application.Request.Posts;
using BlogApi.Client.Helper;
using BlogApi.Client.Interface;
using BlogApi.Domain.Common;
using static Blog.Client.Components.Pages.Public.PublicHome;

namespace BlogApi.Client.Services
{
    public class PostModerationClientService(HttpClient httpClient) : HandleResponse(httpClient), IPostModerationClientService
    {
        public async Task<Result<bool>> ApprovePost(int PostId)
            => await PostAsync<bool>($"api/PostModeration/ApprovePost?PostId={PostId}");

        public async Task<Result<bool>> RejectPost(int PostId)
            => await DeleteAsync<bool>($"api/PostModeration/RejectPost?PostId={PostId}");
     
        public async Task<Result<PagedResult<PendingRequestDto>>> GetListAdminRequest(ListPaginatedRequest request)
            => await GetAsync<PagedResult<PendingRequestDto>>(
                $"api/PostModeration/GetListAdminRequest?PageNumber={request.PageNumber}&PageSize={request.PageSize}");
    }
}