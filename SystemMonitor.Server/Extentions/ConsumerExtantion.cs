using Confluent.Kafka;
using Microsoft.Extensions.Options;
using SystemMonitor.MessageBroker;
using SystemMonitor.MessageBroker.Kafka;
using SystemMonitor.Models.Configs;
using SystemMonitor.Models.Dtos;
using SystemMonitor.Server.Consumers;

namespace SystemMonitor.Server.Extentions;

public static class ConsumerExtantion
{
    public static void AddKafkaComputerHandler(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<KafkaConsumerSettings>(configuration.GetSection("Kafka"));
        services.AddScoped<IKafkaHandler<string, ComputerDetailsDto>, ComputerHandler>();
        services.AddSingleton<IKafkaConsumer<string, ComputerDetailsDto>>(sp =>
        {
            var kafkaSettings = sp.GetRequiredService<IOptions<KafkaConsumerSettings>>().Value;
            var config = new ConsumerConfig
            {
                BootstrapServers = kafkaSettings.BootstrapServers,
                GroupId = kafkaSettings.GroupId,
                AutoOffsetReset = Enum.Parse<AutoOffsetReset>(kafkaSettings.AutoOffsetReset, true)
            };
            var serviceScopeFactory = sp.GetRequiredService<IServiceScopeFactory>();

            return new KafkaConsumer<string, ComputerDetailsDto>(
                config,
                serviceScopeFactory,
                null,
                kafkaSettings.Topic);
        });

        services.AddHostedService<ComputerConsumer>();
    }
}