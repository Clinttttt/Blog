using BlogApi.Application.Dtos;
using BlogApi.Application.Models;
using BlogApi.Application.Request.Posts;
using BlogApi.Domain.Common;
using static Blog.Client.Components.Pages.Public.PublicHome;

namespace BlogApi.Client.Interface
{
    public interface IPostInteractionsClientService
    {
        Task<Result<bool>> ToggleLikePost(TogglePostLikeRequest request);
        Task<Result<bool>> AddBookMark(AddBookMarkRequest request);
        Task<Result<PagedResult<PostDto>>> ListBookMark(ListPaginatedRequest request);
        Task<Result<bool>> TrackPostView(int? PostId);
    }
}