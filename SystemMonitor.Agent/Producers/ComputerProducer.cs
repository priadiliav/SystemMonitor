using SystemMonitor.Agent.Builders;
using SystemMonitor.Agent.Configs;
using SystemMonitor.MessageBroker.Kafka;
using SystemMonitor.Models.Dtos;

namespace SystemMonitor.Agent.Producers;

public class ComputerProducer(
    ILogger<ComputerProducer> logger,
    ComputerBuilder computerBuilder,
    KafkaProducer<string, ComputerDetailsDto> kafkaProducer)
    : BackgroundService
{
    private const string Topic = "computers-info-topic";

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            var computerDetails = await computerBuilder.BuildAsync();

            await kafkaProducer.ProduceAsync(Topic, Globals.GetUserName(), computerDetails);

            logger.LogInformation("Metrics sent to Kafka");

            await Task.Delay(TimeSpan.FromSeconds(30), stoppingToken);
        }
    }
}