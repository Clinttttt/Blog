using Blog.Application.Commands.Tags.DeleteTagToPost;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Application.Request.Tag
{
    public class DeleteTagToPostRequest
    {
        public int? PostId { get; set; }
        public int? TagId { get; set; }

        public DeleteTagToPostCommand DeleteTagToPostCommand(Guid UserId)
            => new(PostId, TagId,UserId);
    }
}
