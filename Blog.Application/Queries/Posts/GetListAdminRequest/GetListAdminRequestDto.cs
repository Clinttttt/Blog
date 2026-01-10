using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BlogApi.Domain.Enums.EntityEnum;

namespace Blog.Application.Queries.Posts.GetAdminRequest
{
    public class GetListAdminRequestDto
    {
        public int PostId { get; set; }
        public string? Title { get; set; }
        public string? Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public string? Name { get; set; }
        public string? CategoryName { get; set; }
        public ReadingDuration readingDuration { get; set; }
    }
}
