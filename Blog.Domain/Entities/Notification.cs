using BlogApi.Domain.Entities;
using BlogApi.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Domain.Entities
{
    public class Notification
    {
        public int Id { get; set; }
        public int? PostId { get; set; }
        public Guid? ActorUserId { get; set; }
        public Guid RecipientUserId { get; set; }
        public EntityEnum.Type Type { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsRead { get; set; }
        public User? User {get;set;}
        public Post? Post { get; set; }

    }

}
