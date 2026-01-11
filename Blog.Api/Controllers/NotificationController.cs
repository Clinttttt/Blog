using Blog.Application.Commands.Notification.DeleteAllNotification;
using Blog.Application.Commands.Notification.DeleteNotification;
using Blog.Application.Commands.Notification.MarkAllNotificationAsRead;
using Blog.Application.Commands.Notification.MarkNotificationAsRead;
using Blog.Application.Queries.Posts.GetListNotification;
using BlogApi.Api.Shared;
using BlogApi.Application.Models;
using BlogApi.Application.Request.Posts;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController(ISender sender) : ApiBaseController(sender)
    {


        [Authorize(Roles = "Admin,Author")]
        [HttpDelete("DeleteNotification")]
        public async Task<ActionResult<bool>> DeleteNotification([FromQuery] int notifId)
        {
            var commnad = new DeleteNotificationCommand(notifId);
            var result = await Sender.Send(commnad);
            return HandleResponse(result);
        }

        [Authorize(Roles = "Admin,Author")]
        [HttpGet("GetListNotification")]
        public async Task<ActionResult<PagedResult<GetListNotificationDto>>> GetListNotification([FromQuery] ListPaginatedRequest request)
        {
            var query = new GetListNotificationQuery
            {
                UserId = UserIdOrNull,
                PageNumber = request.PageNumber,
                PageSze = request.PageSize,
            };
            var result = await Sender.Send(query);
            return HandleResponse(result);
        }

        [Authorize(Roles = "Admin,Author")]
        [HttpPatch("MarkNotificationAsRead")]
        public async Task<ActionResult<bool>> MarkNotificationAsRead(int notifId)
        {
            var command = new MarkNotificationAsReadCommand(notifId);
            var result = await Sender.Send(command);
            return HandleResponse(result);
        }

        [Authorize(Roles = "Admin,Author")]
        [HttpPatch("MarkAllNotificationAsRead")]
        public async Task<ActionResult<bool>> MarkAllNotificationAsRead()
        {
            var command = new MarkAllNotificationAsReadCommand(UserId);
            var result = await Sender.Send(command);
            return HandleResponse(result);
        }

        [Authorize(Roles = "Admin,Author")]
        [HttpDelete("DeleteAllNotification")]
        public async Task<ActionResult<bool>> DeleteAllNotification()
        {
            var command = new DeleteAllNotificationCommand(UserId);
            var result = await Sender.Send(command);
            return HandleResponse(result);
        }
    }
}
