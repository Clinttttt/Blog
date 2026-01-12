using BlogApi.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Application.Queries.Notification.GetListNotification
{
    public class GetListNotificationDto
    {
        public int Id { get; set; }
        public EntityEnum.Type Type { get; set; }
        public int? PostId { get; set; }
        public string? UserName { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsRead { get; set; }

        
       
    }
}
