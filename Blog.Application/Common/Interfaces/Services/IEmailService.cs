using BlogApi.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Application.Common.Interfaces.Services
{
    public interface IEmailService
    {
        Task<Result<bool>> SendNewsletterAsync(string Subject, string Body);
    }
}
