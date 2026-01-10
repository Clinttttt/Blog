using BlogApi.Domain.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Application.Commands.Notification.DeleteNotification
{
    public record DeleteNotificationCommand(int notifId) : IRequest<Result<bool>>;
   
}
