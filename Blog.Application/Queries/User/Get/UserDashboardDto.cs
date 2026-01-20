using BlogApi.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Application.Queries.User.Get
{
    public class UserDashboardDto
    {
        public string? Name { get; set; }
        public DateTime CreatedAt { get; set; }
        public string? ProfilePhoto { get; set; }


        public int? TotalPost { get; set; }
        public int? TotalViews { get; set; }
        public int? TotalLikes { get; set; }
        public int? TotalBookMarks { get; set; }

   


    }
}
