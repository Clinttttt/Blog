using Blog.Application.Queries.User.GetListAuthor;
using BlogApi.Application.Dtos;
using BlogApi.Application.Models;
using BlogApi.Application.Request.Posts;
using BlogApi.Application.Request.User;
using BlogApi.Domain.Common;

namespace BlogApi.Client.Interface
{
    public interface IUserClientServices
    {
        Task<Result<UserProfileDto>> GetCurrentUser();
        Task<Result<bool>> UnSubscribeToNewsletter(string command);
        Task<Result<bool>> AddUserInfo(UserInfoRequest dto);
        Task<Result<bool>> UpdateUserInfo(UserInfoRequest dto);
        Task<Result<UserInfoDto>> GetUserInfo();
        Task<Result<List<AuthorDto>>> GetTopAuthors();
        Task<Result<PagedResult<AuthorStatDto>>> GetListing(ListPaginatedRequest request);
    }
}
