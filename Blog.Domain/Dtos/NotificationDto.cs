using BlogApi.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Domain.Dtos
{
    public class NotificationDto
    {
        public int? PostId { get; set; }
        public Guid? ActorUserId { get; set; }
        public EntityEnum.Type Type { get; set; }
        public DateTime CreatedAt { get; set; }
        public Guid? RecipientUserId { get; set; }
        public string? ActorName { get; set; }
    }
}
