using BlogApi.Api.Shared;
using BlogApi.Application.Request.Posts;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlogApi.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ApiBaseController
    {
        public CommentsController(ISender sender) : base(sender) { }

        [Authorize(Roles = "Admin,Author")]
        [HttpPost("AddComment")]
        public async Task<ActionResult<int>> AddComment([FromBody] AddCommentRequest request)
        {
            var command = request.ToCommand(UserId);
            var result = await Sender.Send(command);
            return HandleResponse(result);
        }

        [Authorize(Roles = "Admin,Author")]
        [HttpPatch("UpdateComment")]
        public async Task<ActionResult<int>> UpdateComment([FromBody] UpdateCommentRequest request)
        {
            var command = request.ToCommand(UserId);
            var result = await Sender.Send(command);
            return HandleResponse(result);
        }

        [Authorize(Roles = "Admin,Author")]
        [HttpPost("ToggleLikeComment")]
        public async Task<ActionResult<bool>> ToggleLikeComment([FromBody] ToggleCommentLikeRequest request)
        {
            var command = request.ToCommand(UserId);
            var result = await Sender.Send(command);
            return HandleResponse(result);
        }
    }
}