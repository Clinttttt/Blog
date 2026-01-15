using Blog.Application.Common.Interfaces.Repositories;
using BlogApi.Domain.Common;
using BlogApi.Domain.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using static BlogApi.Domain.Enums.EntityEnum;

namespace Blog.Application.Queries.Posts.GetApprovalTotal
{
    public class GetUnreadTotalQueryHandler(INotificationRespository respository) : IRequestHandler<GetUnreadTotalQuery, Result<UnreadDto>>
    {
        public async Task<Result<UnreadDto>> Handle(GetUnreadTotalQuery request,CancellationToken cancellationToken)
        {

            return await respository.GetunreadAsync(request.UserId, request.filter, cancellationToken);

        }
    }
}
