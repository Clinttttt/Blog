using Blog.Application.Common.Interfaces;
using Blog.Application.Common.Interfaces.Repositories;
using BlogApi.Application.Dtos;
using BlogApi.Application.Queries.User.CurrentUser;
using BlogApi.Domain.Common;
using BlogApi.Domain.Interfaces;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BlogApi.Application.Queries.User.GetCurrentUser
{
    public class GetPreviewQueryHandler(
        IUserRespository repository
       ) 
        : IRequestHandler<GetPreviewQuery, Result<UserProfileDto>>
    {
        private static readonly TimeSpan DefaultCacheDuration = TimeSpan.FromMinutes(10);

        public async Task<Result<UserProfileDto>> Handle(
            GetPreviewQuery request,
            CancellationToken cancellationToken)
        {
                                  
            var user = await repository.GetUserProfileAsync(request.UserId, cancellationToken);

            if (user is null)
                return Result<UserProfileDto>.NotFound();              
               
            var result = Result<UserProfileDto>.Success(user);     
            return result;
        }
    }
}
