using Confluent.Kafka;
using Microsoft.Extensions.Options;
using SystemMonitor.Agent.Producers;
using SystemMonitor.MessageBroker.Kafka;
using SystemMonitor.Models.Configs;
using SystemMonitor.Models.Dtos;

namespace SystemMonitor.Agent.Extantions;

public static class ProducerExtantion
{
    public static void AddProducers(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<KafkaProducerSettings>(configuration.GetSection("Kafka"));
        services.AddSingleton<KafkaProducer<string, ComputerDetailsDto>>(sp =>
        {
            var kafkaSettings = sp.GetRequiredService<IOptions<KafkaProducerSettings>>().Value;
            var config = new ProducerConfig
            {
                BootstrapServers = kafkaSettings.BootstrapServers
            };
            return new KafkaProducer<string, ComputerDetailsDto>(config);
        });
        services.AddHostedService<ComputerProducer>();
    }
}