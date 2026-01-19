using BlogApi.Domain.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Application.Queries.User.Get
{
    public record GetQuery(Guid UserId ) : IRequest<Result<UserDashboardDto>>;
   
}
