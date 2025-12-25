using BlogApi.Domain.Common;
using BlogApi.Domain.Entities;
using BlogApi.Domain.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApi.Application.Commands.Posts.AddCommentLike
{
    public class AddCommentLikeCommandHandler(IAppDbContext context) : IRequestHandler<AddCommentLikeCommand, Result<bool>>
    {
        public async Task<Result<bool>> Handle(AddCommentLikeCommand request, CancellationToken cancellationToken)
        {
            var likecomment = await context.CommentLikes
                .FirstOrDefaultAsync(s => s.CommentId == request.CommentId && s.UserId == request.UserId);
            if (likecomment is null)
            {
               
                context.CommentLikes.Add(new CommentLike
                {
                    CommentId = request.CommentId,                
                    UserId = request.UserId,
                });
            }
            else
            {
                context.CommentLikes.Remove(likecomment);
            }
            await context.SaveChangesAsync(cancellationToken);
            return Result<bool>.Success(true);
        }
    }
}
