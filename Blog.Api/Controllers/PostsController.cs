using Blog.Application.Queries.Posts.GetAdminRequest;
using Blog.Application.Queries.Posts.GetListAuthorRequest;
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
    public class PostsController : ApiBaseController
    {
        public PostsController(ISender sender) : base(sender) { }

        [Authorize(Roles = "Admin,Author")]
        [HttpPost("Create")]
        public async Task<ActionResult<int>> Create([FromBody] CreatePostRequest request)
        {
            var command = request.ToCommand(UserId);
            var result = await Sender.Send(command);
            return HandleResponse(result);
        }

        [AllowAnonymous]
        [HttpGet("Get")]
        public async Task<ActionResult<PostDetailDto>> Get([FromQuery] GetPostRequest request)
        {
            var query = request.ToQuery(UserIdOrNull);
            var result = await Sender.Send(query);
            return HandleResponse(result);
        }

        [Authorize(Roles = "Admin,Author")]
        [HttpPatch("Update")]
        public async Task<ActionResult<int>> Update([FromBody] UpdatePostRequest request)
        {
            var command = request.ToCommand(UserId);
            var result = await Sender.Send(command);
            return HandleResponse(result);
        }

        [Authorize(Roles = "Admin,Author")]
        [HttpDelete("Delete")]
        public async Task<ActionResult<bool>> Delete([FromQuery] DeletePostRequest request)
        {
            var command = request.ToCommand(UserId);
            var result = await Sender.Send(command);
            return HandleResponse(result);
        }

        [Authorize(Roles = "Admin,Author")]
        [HttpPatch("Archived")]
        public async Task<ActionResult<bool>> Archived([FromQuery] ArchivePostRequest request)
        {
            var command = request.ToCommand(UserId);
            var result = await Sender.Send(command);
            return HandleResponse(result);
        }

        [Authorize(Roles = "Admin, Author")]
        [HttpGet("ListPublishedByUser")]
        public async Task<ActionResult<PagedResult<PostDto>>> ListPublishedByUser([FromQuery] ListPaginatedRequest request, CancellationToken cancellationToken)
        {
            var query = new GetPagedPostsQuery
            {
                UserId = UserId,
                PageNumber = request.PageNumber,
                PageSize = request.PageSize,
                FilterType = PostFilterType.PublishedByUser
            };
            var result = await Sender.Send(query, cancellationToken);
            return HandleResponse(result);
        }

        [AllowAnonymous]
        [HttpGet("ListPublished")]
        public async Task<ActionResult<PagedResult<PostDto>>> ListByPublished([FromQuery] ListPaginatedRequest request, CancellationToken cancellationToken)
        {
            var query = new GetPagedPostsQuery
            {
                UserId = UserIdOrNull,
                PageNumber = request.PageNumber,
                PageSize = request.PageSize,
                FilterType = PostFilterType.Published
            };
            var result = await Sender.Send(query, cancellationToken);
            return HandleResponse(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("ListDraftByUser")]
        public async Task<ActionResult<PagedResult<PostDto>>> ListDraftByUser([FromQuery] ListPaginatedRequest request, CancellationToken cancellationToken)
        {
            var query = new GetPagedPostsQuery
            {
                UserId = UserId,
                PageNumber = request.PageNumber,
                PageSize = request.PageSize,
                FilterType = PostFilterType.DraftsByUser
            };
            var result = await Sender.Send(query, cancellationToken);
            return HandleResponse(result);
        }

        [AllowAnonymous]
        [HttpGet("ListByTag/{id}")]
        public async Task<ActionResult<PagedResult<PostDto>>> ListByTag([FromRoute] int id, [FromQuery] ListPaginatedRequest request,
            CancellationToken cancellationToken)
        {
            var query = new GetPagedPostsQuery
            {
                UserId = UserIdOrNull,
                PageNumber = request.PageNumber,
                PageSize = request.PageSize,
                FilterType = PostFilterType.ByTag,
                TagId = id
            };
            var result = await Sender.Send(query, cancellationToken);
            return HandleResponse(result);
        }

        [AllowAnonymous]
        [HttpGet("ListByCategory/{id}")]
        public async Task<ActionResult<PagedResult<PostDto>>> ListByCategory([FromRoute] int id, [FromQuery] ListPaginatedRequest request,
            CancellationToken cancellationToken)
        {
            var query = new GetPagedPostsQuery
            {
                UserId = UserIdOrNull,
                PageNumber = request.PageNumber,
                PageSize = request.PageSize,
                FilterType = PostFilterType.ByCategory,
                CategoryId = id
            };
            var result = await Sender.Send(query, cancellationToken);
            return HandleResponse(result);
        }

        [HttpGet("Author")]
        [HttpGet("ListAuthorRequest")]
        public async Task<ActionResult<PagedResult<PendingRequestDto>>> ListAuthorRequest([FromQuery] ListPaginatedRequest request, CancellationToken cancellationToken)
        {
            var query = new GetListPendingRequestQuery
            {
                filter = s => s.UserId == UserId,
                PageNumber = request.PageNumber,
                PageSize = request.PageSize
            };
            var result = await Sender.Send(query, cancellationToken);
            return HandleResponse(result);
        }
    }
}