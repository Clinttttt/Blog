using BlogApi.Domain.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Application.Commands.Notification.MarkNotificationAsRead
{
    public record MarkNotificationAsReadCommand(int notifId) : IRequest<Result<bool>>;
    
}
