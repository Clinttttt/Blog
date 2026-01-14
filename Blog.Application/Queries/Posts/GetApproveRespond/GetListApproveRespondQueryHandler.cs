using BlogApi.Application.Models;
using BlogApi.Domain.Common;
using BlogApi.Domain.Enums;
using BlogApi.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using static BlogApi.Domain.Enums.EntityEnum;

namespace Blog.Application.Queries.Posts.GetApproveRespond
{
    public class GetListApproveRespondQueryHandler(IAppDbContext context) : IRequestHandler<GetListApproveRespondQuery, Result<PagedResult<ApproveRespondDto>>>
    {
        public async Task<Result<PagedResult<ApproveRespondDto>>> Handle(GetListApproveRespondQuery request, CancellationToken cancellationToken)
        {
            var approvalTypes = new[] { EntityEnum.Type.PostApproval, EntityEnum.Type.PostDecline };

            var notif = await context.Notifications
                .Include(s => s.User)
                .ThenInclude(s => s!.Posts)
                 .AsNoTracking()
                 .OrderByDescending(s => s.CreatedAt)
                 .Where(s => s.RecipientUserId == request.UserId && approvalTypes.Contains(s.Type))
                 .ToListAsync(cancellationToken);



            var dto = notif.Select(s => new ApproveRespondDto
            {
                PostId = s.Id,
                CreatedAt = s.CreatedAt,
                Title = s.User?.Posts != null ? s.User?.Posts.Select(p => p.Title).First() : "N/A",
                Type = s.Type

            }).ToList();

            var count = dto.Count;
            var result = new PagedResult<ApproveRespondDto>
            {
                Items = dto,
                PageNumber = request.PageNumber,
                PageSize = request.PageSize,
                TotalCount = count
            };

            return Result<PagedResult<ApproveRespondDto>>.Success(result);
        }
    }
}
