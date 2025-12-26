using BlogApi.Application.Queries.Posts.GetPostWithComments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApi.Application.Request.Posts
{
    public class GetPostWithCommentsRequest
    {
       public int PostId { get; set; }
        public GetPostWithCommentsQuery GetPostWithCommentsQuery(Guid? UserId)
            => new(PostId, UserId);
    }
}
