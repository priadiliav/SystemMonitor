using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace SystemMonitor.Hubs.Hubs;

[Authorize]
public class WelcomeHub (ILogger<WelcomeHub> logger) : Hub
{
    public async Task SendMessage(string user, string message)
    {
        logger.LogInformation("SendMessage called, user: {user}, message: {message}", user, message);
    }
}