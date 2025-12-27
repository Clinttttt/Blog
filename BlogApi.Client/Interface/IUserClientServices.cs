using BlogApi.Application.Dtos;
using BlogApi.Domain.Common;

namespace BlogApi.Client.Interface
{
    public interface IUserClientServices
    {
        Task<Result<UserProfileDto>?> GetCurrentUser();
        Task<Result<bool>> UnSubscribeToNewsletter(string command);
    }
}
