using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;
using System;
using System.Threading.Tasks;

namespace Blog.Infrastructure.SignalR.Notifications
{
    [Authorize(Roles = "Admin,Author")]
    public class NotificatonHub : AuthenticatedHubBase
    {
           
    }
}
