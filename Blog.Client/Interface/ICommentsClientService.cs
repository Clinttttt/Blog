using BlogApi.Application.Request.Posts;
using BlogApi.Domain.Common;

namespace BlogApi.Client.Interface
{
    public interface ICommentsClientService
    {
        Task<Result<int>> AddComment(AddCommentRequest dto);
        Task<Result<int>> UpdateComment(UpdateCommentRequest dto);
        Task<Result<bool>> ToggleLikeComment(ToggleCommentLikeRequest dto);
    }
}