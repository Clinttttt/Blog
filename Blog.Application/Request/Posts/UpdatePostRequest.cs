using BlogApi.Application.Commands.Posts.UpdatePost;
using BlogApi.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BlogApi.Domain.Enums.EntityEnum;

namespace BlogApi.Application.Request.Posts
{
    public class UpdatePostRequest
    {
        public int? PostId { get; set; }
        public int? CategoryId { get; set; }
        public string? Title { get; set; }
        public string? Content { get; set; }
        public byte[]? Photo { get; set; }
        public string? PhotoContent { get; set; }
        public string? Author { get; set; }
        public ReadingDuration readingDuration { get; set; }

        public UpdatePostCommand ToCommand(Guid userId)
           => new(userId,
               PostId = PostId,
               CategoryId = CategoryId,
               Title = Title,
               Content = Content,
               Photo = Photo,
               PhotoContent = PhotoContent,
               Author = Author,
               readingDuration);
          
    }
}
