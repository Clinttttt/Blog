using BlogApi.Application.Dtos;
using BlogApi.Domain.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Application.Queries.User.FilterBy.GetTop5
{
    public class GetTop5Query() : IRequest<Result<List<AuthorDto>>>;
   
}
