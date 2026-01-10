using BlogApi.Api.Shared;
using BlogApi.Application.Dtos;
using BlogApi.Application.Queries.Posts.GetFeatured;
using BlogApi.Application.Request.Posts;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlogApi.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeaturedPostsController : ApiBaseController
    {
        public FeaturedPostsController(ISender sender) : base(sender) { }

        [Authorize(Roles = "Admin")]
        [HttpPost("AddFeatured")]
        public async Task<ActionResult<bool>> AddFeatured([FromBody] AddFeaturedRequest request)
        {
            var command = request.ToCommand(UserId);
            var result = await Sender.Send(command);
            return HandleResponse(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("DeleteFeatured")]
        public async Task<ActionResult<bool>> DeleteFeatured([FromBody] DeletePostRequest request)
        {
            var command = request.ToCommand(UserId);
            var result = await Sender.Send(command);
            return HandleResponse(result);
        }

        [AllowAnonymous]
        [HttpGet("ListFeatured")]
        public async Task<ActionResult<List<FeaturedPostDto>>> ListFeatured()
        {
            var query = new GetListFeaturedQuery();
            var result = await Sender.Send(query);
            return HandleResponse(result);
        }
    }
}