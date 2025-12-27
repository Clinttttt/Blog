using BlogApi.Domain.Common;
using BlogApi.Domain.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApi.Application.Commands.User.UpdateUserInfo
{
    public class UpdateUserInfoCommandHandler(IAppDbContext context) : IRequestHandler<UpdateUserInfoCommand, Result<bool>>
    {
        public async Task<Result<bool>> Handle(UpdateUserInfoCommand request, CancellationToken cancellationToken)
        {
           var user = await context.UserInfos.FirstOrDefaultAsync(s=> s.UserId == request.UserId);
            if (user is null)
                return Result<bool>.NotFound();
            else
                user.FullName = request.FullName;
                user.Bio =  request.Bio;
                context.UserInfos.Update(user);
            await context.SaveChangesAsync(cancellationToken);
            return Result<bool>.Success(true);
        }
    }
}
