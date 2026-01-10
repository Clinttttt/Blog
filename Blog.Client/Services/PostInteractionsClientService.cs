using BlogApi.Application.Dtos;
using BlogApi.Application.Models;
using BlogApi.Application.Request.Posts;
using BlogApi.Client.Helper;
using BlogApi.Client.Interface;
using BlogApi.Domain.Common;
using static Blog.Client.Components.Pages.Public.PublicHome;

namespace BlogApi.Client.Services
{
    public class PostInteractionsClientService(HttpClient httpClient) : HandleResponse(httpClient), IPostInteractionsClientService
    {
        public async Task<Result<bool>> ToggleLikePost(TogglePostLikeRequest request)
            => await PostAsync<bool>($"api/PostInteractions/ToggleLikePost?PostId={request.PostId}");

        public async Task<Result<bool>> AddBookMark(AddBookMarkRequest request)
            => await PostAsync<bool>($"api/PostInteractions/AddBookMark?PostId={request.PostId}");

        public async Task<Result<PagedResult<PostDto>>> ListBookMark(ListPaginatedRequest request)
            => await GetAsync<PagedResult<PostDto>>(
          $"api/PostInteractions/ListBookMark?PageNumber={request.PageNumber}&PageSize={request.PageSize}");

        public async Task<Result<bool>> TrackPostView(int? PostId)
            => await PostAsync<bool>($"api/PostInteractions/TrackPostView?PostId={PostId}");
    }
}