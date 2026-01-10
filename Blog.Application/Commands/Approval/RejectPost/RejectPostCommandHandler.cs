using BlogApi.Domain.Common;
using BlogApi.Domain.Entities;
using BlogApi.Domain.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using static BlogApi.Domain.Enums.EntityEnum;

namespace Blog.Application.Commands.Approval.RejectPost
{
    public class RejectPostCommandHandler(IAppDbContext context) : IRequestHandler<RejectPostCommand, Result<bool>>
    {
        public async Task<Result<bool>> Handle(RejectPostCommand request, CancellationToken cancellationToken)
        {
            var post = await context.Posts
                 .FirstOrDefaultAsync(s => s.Id == request.PostId && s.Status == Status.Pending);

            if (post is null)
                return Result<bool>.NotFound();

            context.Posts.Remove(post);
            await context.SaveChangesAsync();
            return Result<bool>.Success(true);
        }
    }
}
