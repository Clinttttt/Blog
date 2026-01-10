using BlogApi.Application.Request.Posts;
using BlogApi.Client.Helper;
using BlogApi.Client.Interface;
using BlogApi.Domain.Common;

namespace BlogApi.Client.Services
{
    public class CommentsClientService(HttpClient httpClient) : HandleResponse(httpClient), ICommentsClientService
    {
        public async Task<Result<int>> AddComment(AddCommentRequest dto)
            => await PostAsync<AddCommentRequest, int>("api/Comments/AddComment", dto);

        public async Task<Result<int>> UpdateComment(UpdateCommentRequest dto)
            => await UpdateAsync<UpdateCommentRequest, int>("api/Comments/UpdateComment", dto);

        public async Task<Result<bool>> ToggleLikeComment(ToggleCommentLikeRequest dto)
            => await PostAsync<ToggleCommentLikeRequest, bool>("api/Comments/ToggleLikeComment", dto);
    }
}