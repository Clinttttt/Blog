using BlogApi.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Application.Common.Interfaces.Services
{
    public interface INotificationService
    {
        Task<Result<bool>> NotificationAsync(Domain.Entities.Notification request, CancellationToken cancellationToken = default);
    }
}
