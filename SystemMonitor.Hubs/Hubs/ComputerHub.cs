using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace SystemMonitor.Hubs.Hubs;

[Authorize]
public class ComputerHub : Hub;