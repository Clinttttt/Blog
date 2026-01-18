using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Application.Queries.User.GetListAuthor
{
    public class AuthorStatDto
    {
        public Guid UserId { get; set; }
        public string? Name { get; set; }
        public int? PostCount { get; set; }
        public DateTime CreatedAt { get; set; }
        public string? ProfilePhoto { get; set; }




    }

}