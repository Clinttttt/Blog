using Blog.Application.Common.Interfaces;
using BlogApi.Application.Common.Interfaces;
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
    public class GetApprovalTotalQueryHandler(IAppDbContext context) : IRequestHandler<GetApprovalTotalQuery, Result<GetApprovalTotalDto>>
    {
        

        public async Task<Result<GetApprovalTotalDto>> Handle(
            GetApprovalTotalQuery request,
            CancellationToken cancellationToken)
        {
       
            var postCount = await context.Posts
                .AsNoTracking()
                .Where(s => s.Status == Status.Pending)
                .CountAsync(cancellationToken);

            var notifcount = await context.Notifications
                .AsNoTracking()
                .Where(s => s.RecipientUserId == request.UserId && s.IsRead == false)
                .CountAsync(cancellationToken);

            var dto = new GetApprovalTotalDto
            {                
                ApprovalCount = postCount,
                NotificationCount = notifcount
            };  

            var result = Result<GetApprovalTotalDto>.Success(dto);          
            return result;
        }
    }
}
