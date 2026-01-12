using Blog.Application.Commands.Approval.ApprovePost;
using Blog.Application.Commands.Approval.RejectPost;

using Blog.Application.Queries.Posts.GetAdminRequest;
using Blog.Application.Queries.Posts.GetApprovalTotal;
using BlogApi.Api.Shared;
using BlogApi.Application.Models;
using BlogApi.Application.Request.Posts;
using BlogApi.Domain.Common;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlogApi.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class PostModerationController : ApiBaseController
    {
        public PostModerationController(ISender sender) : base(sender) { }

        [HttpPost("ApprovePost")]
        public async Task<ActionResult<bool>> ApprovePost([FromQuery] ApprovePostCommand command)
        {
            var result = await Sender.Send(command);
            return HandleResponse(result);
        }

        [HttpDelete("RejectPost")]
        public async Task<ActionResult<bool>> RejectPost([FromQuery] int PostId)
        {
            var query = new RejectPostCommand(PostId);
            var result = await Sender.Send(query);
            return HandleResponse(result);
        }

        [HttpGet("GetApprovalTotal")]
        public async Task<ActionResult<GetApprovalTotalDto>> GetApprovalTotal()
        {
            var request = new GetApprovalTotalQuery(UserIdOrNull);
            var result = await Sender.Send(request);
            return HandleResponse(result);
        }

        [HttpGet("GetListAdminRequest")]
        public async Task<ActionResult<PagedResult<GetListAdminRequestDto>>> GetListAdminRequest([FromQuery] ListPaginatedRequest request, CancellationToken cancellationToken)
        {
            var query = new GetListAdminRequestQuery
            {
                PageNumber = request.PageNumber,
                PageSize = request.PageSize
            };
            var result = await Sender.Send(query, cancellationToken);
            return HandleResponse(result);
        }
    }
}