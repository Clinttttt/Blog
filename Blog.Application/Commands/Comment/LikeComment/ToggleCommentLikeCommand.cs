using BlogApi.Domain.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApi.Application.Commands.Comment.LikeComment
{
    public record ToggleCommentLikeCommand(int CommentId, int PostId, Guid UserId) :IRequest<Result<bool>>;
    
}
