using Blog.Client.Components;
using Blog.Infrastructure.SignalR.Comments;
using Blog.Infrastructure.SignalR.Notifications;
using Blog.Infrastructure.SignalR.Posts;
using BlogApi.Client;
using BlogApi.Client.Middleware;
using Microsoft.AspNetCore.Components.Server;
using Microsoft.AspNetCore.DataProtection;

var builder = WebApplication.CreateBuilder(args);

<<<<<<< HEAD
=======
/*var port = Environment.GetEnvironmentVariable("PORT");
if (!string.IsNullOrEmpty(port))
{
    builder.WebHost.UseUrls($"http://0.0.0.0:{port}");
}
*/


>>>>>>> c94bbccddaa70554ebd8ebe96938642f015d8e60
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddSignalR();

builder.Services.Configure<CircuitOptions>(options =>
{
    options.DetailedErrors = true;
});

builder.Services.AddServices(builder.Configuration);

builder.Services
    .AddDataProtection()
    .PersistKeysToFileSystem(
        new DirectoryInfo("/root/.aspnet/DataProtection-Keys"))
    .SetApplicationName("BlogApp");

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

// app.UseHttpsRedirection(); // ? keep OFF in Docker for now

app.UseStaticFiles();
app.Middleware();
app.UseAntiforgery();

app.UseAuthentication();
app.UseAuthorization();
app.UseWebSockets(); 
app.MapHub<PostHub>("/hubs/posts");
app.MapHub<CommentHub>("/hubs/comments");
app.MapHub<NotificatonHub>("/hubs/notifications");
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
