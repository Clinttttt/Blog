using Blog.Application.Commands.Posts.TrackPostView;
using BlogApi.Api.Shared;
using BlogApi.Application.Dtos;
using BlogApi.Application.Models;
using BlogApi.Application.Queries.Posts;
using BlogApi.Application.Request.Posts;
using BlogApi.Domain.Common;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlogApi.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostInteractionsController : ApiBaseController
    {
        public PostInteractionsController(ISender sender) : base(sender) { }

        [Authorize(Roles = "Admin,Author")]
        [HttpPost("ToggleLikePost")]
        public async Task<ActionResult<bool>> ToggleLikePost([FromQuery] TogglePostLikeRequest request)
        {
            var command = request.ToCommand(UserId);
            var result = await Sender.Send(command);
            return HandleResponse(result);
        }

        [Authorize(Roles = "Admin,Author")]
        [HttpPost("AddBookMark")]
        public async Task<ActionResult<bool>> AddBookMark([FromQuery] AddBookMarkRequest request)
        {
            var command = request.ToCommand(UserId);
            var result = await Sender.Send(command);
            return HandleResponse(result);
        }

        [Authorize(Roles = "Admin,Author")]
        [HttpGet("ListBookMark")]
        public async Task<ActionResult<PagedResult<PostDto>>> ListBookMark([FromQuery] ListPaginatedRequest request)
        {
            var query = new GetPagedPostsQuery
            {
                UserId = UserIdOrNull,
                PageNumber = request.PageNumber,
                PageSize = request.PageSize,
                FilterType = PostFilterType.BookMark,
            };
            var result = await Sender.Send(query);
            return HandleResponse(result);
        }

        [AllowAnonymous]
        [HttpPost("TrackPostView")]
        public async Task<ActionResult<bool>> TrackPostView([FromQuery] int PostId)
        {
            var command = new TrackPostViewCommand(UserIdOrNull, PostId);
            var result = await Sender.Send(command);
            return HandleResponse(result);
        }
    }
}