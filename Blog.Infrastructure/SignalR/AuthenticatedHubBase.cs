using BlogApi.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Infrastructure.SignalR
{
    [Authorize]
    public abstract class AuthenticatedHubBase : Hub
    {
        protected Guid? CurrentUserId { get; private set; }
        public override async Task OnConnectedAsync()
        {
            var user = Context.User;
            if (user?.Identity?.IsAuthenticated == true)
            {
                var userIdClaim = user.FindFirst(ClaimTypes.NameIdentifier)?.Value
                    ?? user.FindFirst("sub")?.Value;

                if (Guid.TryParse(userIdClaim, out var UserId))
                {
                    CurrentUserId = UserId;
                    await Groups.AddToGroupAsync(Context.ConnectionId, $"user-{CurrentUserId}");

                }
            }
            await base.OnConnectedAsync();
        }
        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            if (CurrentUserId.HasValue)
            {
                await Groups.RemoveFromGroupAsync(Context.ConnectionId, $"user-{CurrentUserId.Value}");
            }
            await base.OnDisconnectedAsync(exception);
        }
    }
}
