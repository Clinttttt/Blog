using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Infrastructure.Hubs
{
   
    public class PostHub : Hub
    {
        /* [Authorize] 
         public async Task SendPersonalNotification(string message)
         {
             var userId = Context.UserIdentifier;
             await Clients.User(userId!).SendAsync("ReceiveNotification", message);
         }*/
    }
}



