using BlogApi.Domain.Common;
using BlogApi.Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Application.Commands.Notification.CreateNotification
{
    public record CreateNotificationCommand(int PostId, Guid? ActorUserId, Guid RecipientUserId, EntityEnum.Type Type) : IRequest<Result<bool>>;
  
   
}
