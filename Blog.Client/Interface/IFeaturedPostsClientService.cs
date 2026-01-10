using BlogApi.Application.Dtos;
using BlogApi.Application.Request.Posts;
using BlogApi.Domain.Common;

namespace BlogApi.Client.Interface
{
    public interface IFeaturedPostsClientService
    {
        Task<Result<bool>> AddFeatured(AddFeaturedRequest dto);
        Task<Result<bool>> DeleteFeatured(DeletePostRequest request);
        Task<Result<List<FeaturedPostDto>>> ListFeatured();
    }
}