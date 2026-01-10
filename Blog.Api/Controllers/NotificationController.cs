using Blog.Application.Commands.Notification.DeleteNotification;
using Blog.Application.Commands.Notification.MarkNotificationAsRead;
using Blog.Application.Queries.Posts.GetListNotification;
using Blog.Application.Request.Notification;
using BlogApi.Api.Shared;
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
        [HttpPost("CreateNotification")]
        public async Task<ActionResult<bool>> CreateNotification([FromBody] CreateNotificationRequest request)
        {
            var command = request.CreateNotification(UserIdOrNull);
            var result = await Sender.Send(command);
            return HandleResponse(result);
        }

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
        public async Task<ActionResult<List<GetListNotificationDto>>> GetListNotificatio()
        {
            var query = new GetListNotificationQuery(UserIdOrNull);
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

    }
}
