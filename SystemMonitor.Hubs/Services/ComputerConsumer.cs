using SystemMonitor.MessageBroker;
using SystemMonitor.Models.Dtos;

namespace SystemMonitor.Hubs.Services;

public class ComputerConsumer(
    IKafkaConsumer<string, ComputerDetailsDto> kafkaConsumer) 
    : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await kafkaConsumer.Consume(stoppingToken);
    }
    
    public override async Task StopAsync(CancellationToken stoppingToken)
    {
        kafkaConsumer.Close();
        await base.StopAsync(stoppingToken);
    }

    public override void Dispose()
    {
        kafkaConsumer.Dispose();
        base.Dispose();
    }
}