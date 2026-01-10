using Blog.Application.Queries.Posts.RecentActivity;
using BlogApi.Api.Shared;
using BlogApi.Application.Dtos;
using BlogApi.Application.Models;
using BlogApi.Application.Queries.Posts.GetPublicStatistics;
using BlogApi.Application.Queries.Posts.GetStatistics;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlogApi.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostAnalyticsController : ApiBaseController
    {
        public PostAnalyticsController(ISender sender) : base(sender) { }

        [AllowAnonymous]
        [HttpGet("GetPublicStatistics")]
        public async Task<ActionResult<StatisticsDto>> GetPublicStatistics()
        {
            var command = new GetPublicStatisticsQuery();
            var result = await Sender.Send(command);
            return HandleResponse(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("GetStatistics")]
        public async Task<ActionResult<StatisticsDto>> GetStatistics()
        {
            var command = new GetStatisticsQuery(UserId);
            var result = await Sender.Send(command);
            return HandleResponse(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("GetRecentActivity")]
        public async Task<ActionResult<List<RecentActivityItemDto>>> GetRecentActivity([FromQuery] int limit = 4, [FromQuery] int daysBack = 7)
        {
            var query = new RecentActivityQuery
            {
                Limit = limit,
                DaysBack = daysBack
            };
            var result = await Sender.Send(query);
            return Ok(result.Value);
        }
    }
}