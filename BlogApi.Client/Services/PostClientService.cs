using BlogApi.Application.Commands.Posts.CreatePost;
using BlogApi.Application.Dtos;
using BlogApi.Application.Queries.GetPostWithComments;
using BlogApi.Application.Request;
using BlogApi.Client.Dtos;
using BlogApi.Domain.Common;

namespace BlogApi.Client.Services
{
    public class PostClientService
    {
        private readonly HttpClient _httpClient;
        public PostClientService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        
        public async Task<Result<int>> Create(PostRequest dto)
        {
            var request = await _httpClient.PostAsJsonAsync("api/Posts/CreatePosts", dto);
            request.EnsureSuccessStatusCode();
            var result = await request.Content.ReadFromJsonAsync<int>();
            if (result == 0)
                return Result<int>.Failure();
            return Result<int>.Success(result);
        }
        public async Task<Result<PostWithCommentsDto>> Get(int PostId)
        {
            var command = new GetPostWithCommentsQuery(PostId);
            var request = await _httpClient.GetFromJsonAsync<PostWithCommentsDto>($"api/Posts/GetPost?PostId={command.PostId}");
            if (request is null)
                return Result<PostWithCommentsDto>.NotFound();
            return Result<PostWithCommentsDto>.Success(request);
        }
        public async Task<Result<int>> Update(UpdatePostRequest dto)
        {
            var request = await _httpClient.PatchAsJsonAsync("api/Posts/UpdatePost", dto);
            request.EnsureSuccessStatusCode();
            var result = await request.Content.ReadFromJsonAsync<int>();
            if (result is 0)
                return Result<int>.NotFound();
            return Result<int>.Success(result);
        }
        public async Task<Result<bool>> Archived(ArchivedRequest Id)
        {
            var request = await _httpClient.PatchAsJsonAsync("api/Posts/ArchivedPost", Id);
            request.EnsureSuccessStatusCode();
            var result = await request.Content.ReadFromJsonAsync<bool>();
            if (result is false)
                return Result<bool>.NotFound();
            return Result<bool>.Success(result);

        }



    }
}
