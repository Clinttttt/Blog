using BlogApi.Application.Dtos;
using BlogApi.Application.Request.Posts;
using BlogApi.Client.Helper;
using BlogApi.Client.Interface;
using BlogApi.Domain.Common;

namespace BlogApi.Client.Services
{
    public class FeaturedPostsClientService(HttpClient httpClient) : HandleResponse(httpClient), IFeaturedPostsClientService
    {
        public async Task<Result<bool>> AddFeatured(AddFeaturedRequest dto)
            => await PostAsync<AddFeaturedRequest, bool>("api/FeaturedPosts/AddFeatured", dto);

        public async Task<Result<bool>> DeleteFeatured(DeletePostRequest request)
            => await DeleteAsync<DeletePostRequest, bool>("api/FeaturedPosts/DeleteFeatured", request);

        public async Task<Result<List<FeaturedPostDto>>> ListFeatured()
            => await GetAsync<List<FeaturedPostDto>>("api/FeaturedPosts/ListFeatured");
    }
}