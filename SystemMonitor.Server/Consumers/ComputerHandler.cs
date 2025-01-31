using SystemMonitor.MessageBroker;
using SystemMonitor.Models.Dtos;
using SystemMonitor.Server.Services;

namespace SystemMonitor.Server.Consumers;

public class ComputerHandler(
    ILogger<ComputerDetailsDto> logger,
    ComputerService computerService)
    : IKafkaHandler<string, ComputerDetailsDto>
{
    public async Task HandleAsync(string key, ComputerDetailsDto value)
    {
        logger.LogInformation("Received computer details: {0}", value.Name);

        await computerService.AddOrUpdate(value);
    }
}