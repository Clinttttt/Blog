using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Application.Queries.Posts.GetApproveRespond
{
    public class ApproveRespondDto
    {
   
        public int PostId { get; set; }
        public DateTime CreatedAt { get; set; }
        public string? Title { get; set; }
        public BlogApi.Domain.Enums.EntityEnum.Type Type { get; set; }
    }
}
