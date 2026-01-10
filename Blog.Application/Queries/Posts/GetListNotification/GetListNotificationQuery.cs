using Blog.Application.Queries.Posts.GetListNotification;
using BlogApi.Domain.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Application.Queries.Posts.GetListNotification
{
    public record GetListNotificationQuery(Guid? UserId) : IRequest<Result<List<GetListNotificationDto>>>;
   
}

