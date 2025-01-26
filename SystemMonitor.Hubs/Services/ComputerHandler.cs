using Microsoft.AspNetCore.SignalR;
using SystemMonitor.Hubs.Hubs;
using SystemMonitor.MessageBroker;
using SystemMonitor.Models.Dtos;

namespace SystemMonitor.Hubs.Services;

public class ComputerHandler(
    ILogger<ComputerHandler> logger, 
    IHubContext<ComputerHub> hubContext) 
    : IKafkaHandler<string, ComputerDetailsDto>
{
    public async Task HandleAsync(string key, ComputerDetailsDto value)
    {
        logger.LogInformation("Handling message with key: {Key}", key);

        await hubContext.Clients.All.SendAsync("ReceiveComputerDetails", value);
    }
}