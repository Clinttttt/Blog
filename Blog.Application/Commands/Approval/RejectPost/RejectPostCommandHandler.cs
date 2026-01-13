using BlogApi.Domain.Common;
using BlogApi.Domain.Entities;
using BlogApi.Domain.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using static BlogApi.Domain.Enums.EntityEnum;

namespace Blog.Application.Commands.Approval.RejectPost
{
    public class RejectPostCommandHandler(IAppDbContext context)
        : IRequestHandler<RejectPostCommand, Result<bool>>
    {
        public async Task<Result<bool>> Handle(RejectPostCommand request, CancellationToken cancellationToken)
        {
            var post = await context.Posts
                .Include(p => p.PostTags)
                .FirstOrDefaultAsync(s => s.Id == request.PostId && s.Status == Status.Pending, cancellationToken);

            if (post is null)
                return Result<bool>.NotFound();

           await context.Notifications.AddAsync(new Blog.Domain.Entities.Notification
            {
               PostId = request.PostId,
               ActorUserId = post.UserId,
               RecipientUserId = post.UserId,
               Type = BlogApi.Domain.Enums.EntityEnum.Type.PostDecline,
               CreatedAt = DateTime.UtcNow.AddHours(8),
               IsRead = false,
            });
        
            var categoryId = post.CategoryId;
            var tagIds = post.PostTags?.Select(pt => pt.TagId).ToList() ?? new List<int>();

            context.Posts.Remove(post);

            var categoryInUse = await context.Posts
                .AnyAsync(p => p.CategoryId == categoryId && p.Id != request.PostId, cancellationToken);

            if (!categoryInUse)
            {
                var category = await context.Categories.FindAsync(categoryId, cancellationToken);
                if (category is not null)
                {
                    context.Categories.Remove(category);
                }
            }

            if (tagIds.Any())
            {
                foreach (var tagId in tagIds)
                {
                    var tagInUse = await context.PostTags
                        .AnyAsync(pt => pt.TagId == tagId && pt.PostId != request.PostId, cancellationToken);

                    if (!tagInUse)
                    {
                        var tag = await context.Tags.FindAsync(new object[] { tagId }, cancellationToken);
                        if (tag is not null)
                        {
                            context.Tags.Remove(tag);
                        }
                    }
                }
            }

            await context.SaveChangesAsync(cancellationToken);
            return Result<bool>.Success(true);
        }
    }
}