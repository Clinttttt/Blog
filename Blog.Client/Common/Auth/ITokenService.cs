namespace BlogApi.Client.Common.Auth
{
    public interface ITokenService
    {
        Task<string?> GetAccessTokenAsync();
    }
}
