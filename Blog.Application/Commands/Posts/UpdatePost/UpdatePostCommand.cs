using BlogApi.Application.Dtos;
using BlogApi.Domain.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BlogApi.Domain.Enums.EntityEnum;

namespace BlogApi.Application.Commands.Posts.UpdatePost
{
    public record UpdatePostCommand(
     Guid UserId,
     int? PostId,
     int? CategoryId,
     string? Title,
     string? Content,
     byte[]? Photo,
     string? PhotoContent,
     string? Author,
     ReadingDuration readingDuration,
     Status Status
 ) : IRequest<Result<int>>;

}
