using Blog.Application.Commands.Notification.CreateNotification;
using BlogApi.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Application.Request.Notification
{
    public class CreateNotificationRequest
    {
        public int PostId { get; set; }
        public Guid RecipientUserId { get; set; }
        public EntityEnum.Type Type { get; set; }

        public CreateNotificationCommand CreateNotification(Guid? ActorUserId)
            => new(PostId, ActorUserId, RecipientUserId, Type);

    }
}
