using Blog.Application.Queries.Posts.GetAdminRequest;
using Blog.Application.Queries.Posts.GetApprovalTotal;
using BlogApi.Application.Models;
using BlogApi.Application.Request.Posts;
using BlogApi.Domain.Common;
using static Blog.Client.Components.Pages.Public.PublicHome;

namespace BlogApi.Client.Interface
{
    public interface IPostModerationClientService
    {
        Task<Result<bool>> ApprovePost(int PostId);
        Task<Result<bool>> RejectPost(int PostId);
        Task<Result<PagedResult<PendingRequestDto>>> GetListAdminRequest(ListPaginatedRequest request);
    }
}