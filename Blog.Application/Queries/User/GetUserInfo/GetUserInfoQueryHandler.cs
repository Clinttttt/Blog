using Blog.Application.Common.Interfaces;
using Blog.Application.Common.Interfaces.Repositories;
using BlogApi.Application.Dtos;
using BlogApi.Domain.Common;
using BlogApi.Domain.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BlogApi.Application.Queries.User.GetUserInfo
{
    public class GetUserInfoQueryHandler(
        IAppDbContext context
   ) 
        : IRequestHandler<GetUserInfoQuery, Result<UserInfoDto>>
    {
        public async Task<Result<UserInfoDto>> Handle(
         GetUserInfoQuery request,
         CancellationToken cancellationToken)
        {
     
            var userData = await context.Users
                .AsNoTracking()
                .Where(u => u.Id == request.UserId)
                .Select(u => new
                {
                    u.UserName,
                    FullName = u.UserInfo != null ? u.UserInfo.FullName : null,
                    Photo = u.UserInfo != null ? u.UserInfo.Photo : null,
                    PhotoContentType = u.UserInfo != null ? u.UserInfo.PhotoContentType : null,
                    Bio = u.UserInfo != null ? u.UserInfo.Bio : null,
                    ProfilePhotoUrlFromExternal = u.ExternalLogins
                        .Where(el => !string.IsNullOrEmpty(el.ProfilePhotoUrl))
                        .Select(el => el.ProfilePhotoUrl)
                        .FirstOrDefault(),
                    LinkedAt = u.ExternalLogins
                        .Select(el => el.LinkedAt)
                        .FirstOrDefault()
                })
                .FirstOrDefaultAsync(cancellationToken);

          
            if (userData is null)
                return Result<UserInfoDto>.NoContent();
          
            var dto = new UserInfoDto
            {
                FullName = userData.FullName ?? userData.UserName,
                ProfilePhoto = userData.Photo != null && userData.Photo.Length > 0
                    ? $"data:{userData.PhotoContentType};base64,{Convert.ToBase64String(userData.Photo)}"
                    : userData.ProfilePhotoUrlFromExternal ?? string.Empty,
                Bio = userData.Bio ?? "Add a short bio",
                CreatedAt = userData.LinkedAt
            };

            return Result<UserInfoDto>.Success(dto);
        }

    }
}
