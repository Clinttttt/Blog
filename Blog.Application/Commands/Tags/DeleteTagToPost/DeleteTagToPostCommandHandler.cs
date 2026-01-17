using Blog.Application.Common.Interfaces.Utilities;
using BlogApi.Domain.Common;
using BlogApi.Domain.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Application.Commands.Tags.DeleteTagToPost
{
    public class DeleteTagToPostCommandHandler(IAppDbContext context, ICacheInvalidationService cacheInvalidation) : IRequestHandler<DeleteTagToPostCommand, Result<bool>>
    {
        public async Task<Result<bool>> Handle(DeleteTagToPostCommand request, CancellationToken cancellationToken)
        {
            var tagpost = await context.PostTags.FirstOrDefaultAsync(s => s.PostId == request.PostId && s.TagId == request.TagId && s.UserId == request.UserId);
            if (tagpost is null)
            {
                return Result<bool>.NotFound();
            }
            context.PostTags.Remove(tagpost);
            await context.SaveChangesAsync(cancellationToken);

            await cacheInvalidation.InvalidatePostCacheAsync(request.PostId);
            return Result<bool>.Success(true);
        }
    }
}
