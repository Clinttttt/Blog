using BlogApi.Application.Models;
using BlogApi.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Application.Common.Interfaces.Utilities
{
    public interface IGoogleTokenValidator
    {
        Task<Result<GoogleUserInfo>?> ValidateAsync(string idToken);
    }
}
