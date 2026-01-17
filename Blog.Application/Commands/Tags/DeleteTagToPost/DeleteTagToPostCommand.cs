using BlogApi.Domain.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Application.Commands.Tags.DeleteTagToPost
{
    public record DeleteTagToPostCommand(int? PostId,int? TagId, Guid UserId) : IRequest<Result<bool>>;
    
}
