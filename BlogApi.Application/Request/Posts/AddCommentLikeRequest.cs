using BlogApi.Application.Commands.Posts.AddCommentLike;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApi.Application.Request.Posts
{
    public class AddCommentLikeRequest
    {
        public int CommentId { get; set; }
     
        public AddCommentLikeCommand addCommentLikeCommand(Guid UserId)
            => new(CommentId, UserId);
    }
}
