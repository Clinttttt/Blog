using Blog.Application.Queries.Posts.GetAdminRequest;
using BlogApi.Application.Dtos;
using BlogApi.Application.Models;
using BlogApi.Application.Request.Posts;
using BlogApi.Client.Helper;
using BlogApi.Client.Interface;
using BlogApi.Domain.Common;
using static Blog.Client.Components.Pages.Public.PublicHome;

namespace BlogApi.Client.Services
{
    public class PostClientService(HttpClient httpClient) : HandleResponse(httpClient), IPostClientService
    {
        public async Task<Result<int>> Create(CreatePostRequest dto)
            => await PostAsync<CreatePostRequest, int>("api/Posts/Create", dto);

        public async Task<Result<PostDetailDto>> Get(GetPostRequest request)
            => await GetAsync<PostDetailDto>($"api/Posts/Get?PostId={request.PostId}");

        public async Task<Result<int>> Update(UpdatePostRequest dto)
            => await UpdateAsync<UpdatePostRequest, int>("api/Posts/Update", dto);

        public async Task<Result<bool>> Archive(ArchivePostRequest request)
            => await UpdateAsync<bool>($"api/Posts/Archived?PostId={request.PostId}");

        public async Task<Result<bool>> Delete(DeletePostRequest request)
            => await DeleteAsync<bool>($"api/Posts/Delete?PostId={request.PostId}");

        public async Task<Result<PagedResult<PostDto>>> ListPublishedByUser(ListPaginatedRequest request)
           => await GetAsync<PagedResult<PostDto>>(
               $"api/Posts/ListPublishedByUser?PageNumber={request.PageNumber}&PageSize={request.PageSize}");

        public async Task<Result<PagedResult<PostDto>>> ListDraftByUser(ListPaginatedRequest request)
           => await GetAsync<PagedResult<PostDto>>(
               $"api/Posts/ListDraftByUser?PageNumber={request.PageNumber}&PageSize={request.PageSize}");

        public async Task<Result<PagedResult<PostDto>>> ListPublished(ListPaginatedRequest request)
            => await GetAsync<PagedResult<PostDto>>(
                $"api/Posts/ListPublished?PageNumber={request.PageNumber}&PageSize={request.PageSize}");

        public async Task<Result<PagedResult<PostDto>>> ListByTag(int tagId, ListPaginatedRequest request)
            => await GetAsync<PagedResult<PostDto>>(
                $"api/Posts/ListByTag/{tagId}?PageNumber={request.PageNumber}&PageSize={request.PageSize}");

        public async Task<Result<PagedResult<PostDto>>> ListByCategory(int categoryId, ListPaginatedRequest request)
            => await GetAsync<PagedResult<PostDto>>(
                $"api/Posts/ListByCategory/{categoryId}?PageNumber={request.PageNumber}&PageSize={request.PageSize}");

        public async Task<Result<PagedResult<PendingRequestDto>>> ListAuthorRequest(ListPaginatedRequest request)
            => await GetAsync<PagedResult<PendingRequestDto>>(
                $"api/Posts/ListAuthorRequest?PageNumber={request.PageNumber}&PageSize={request.PageSize}");
    }
}