using Blog.Application.Common.Interfaces.Repositories;
using BlogApi.Domain.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Application.Queries.User.Get
{
    public class GetQueryHandler(IUserRespository respository) : IRequestHandler<GetQuery, Result<UserDashboardDto>>
    {
        public async Task<Result<UserDashboardDto>> Handle(GetQuery request, CancellationToken cancellationToken)
        {
            return await respository.Get(request.UserId, cancellationToken);
        }
    }
}
