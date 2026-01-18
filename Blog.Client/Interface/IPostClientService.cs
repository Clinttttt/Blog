using Blog.Application.Queries.Posts.GetAdminRequest;
using BlogApi.Application.Dtos;
using BlogApi.Application.Models;
using BlogApi.Application.Request.Posts;
using BlogApi.Domain.Common;
using static Blog.Client.Components.Pages.Public.PublicHome;

namespace BlogApi.Client.Interface
{
    public interface IPostClientService
    {
        Task<Result<int>> Create(CreatePostRequest dto);
        Task<Result<PostDetailDto>> Get(GetPostRequest request);
        Task<Result<int>> Update(UpdatePostRequest dto);
        Task<Result<bool>> Delete(DeletePostRequest request);
        Task<Result<bool>> Archive(ArchivePostRequest request);
        Task<Result<PagedResult<PostDto>>> ListPublishedByUser(ListPaginatedRequest request);
        Task<Result<PagedResult<PostDto>>> ListDraftByUser(ListPaginatedRequest request);
        Task<Result<PagedResult<PostDto>>> ListPublished(ListPaginatedRequest request);
        Task<Result<PagedResult<PostDto>>> ListByTag(int tagId, ListPaginatedRequest request);
        Task<Result<PagedResult<PostDto>>> ListByCategory(int Id, ListPaginatedRequest request);
        Task<Result<PagedResult<PendingRequestDto>>> ListAuthorRequest(ListPaginatedRequest request);
    }
}