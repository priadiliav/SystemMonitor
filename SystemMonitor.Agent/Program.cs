using Confluent.Kafka;
using SystemMonitor.Agent.Builders;
using SystemMonitor.Agent.Collectors;
using SystemMonitor.Agent.Configs;
using SystemMonitor.Agent.Services;
using SystemMonitor.MessageBroker.Kafka;
using SystemMonitor.Models.Dtos;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddSingleton<Globals>();

builder.Services.AddSingleton<IMetricCollector, CpuUsageCollector>();
builder.Services.AddSingleton<IMetricCollector, DiskUsageCollector>();
builder.Services.AddSingleton<IMetricCollector, NetworkUsageCollector>();
builder.Services.AddSingleton<IMetricCollector, RamUsageCollector>();
builder.Services.AddSingleton<ComputerBuilder>();

builder.Services.AddSingleton<KafkaProducer<string, ComputerDetailsDto>>(_ =>
{
    var config = new ProducerConfig
    {
        BootstrapServers = "localhost:9093"
    };
    return new KafkaProducer<string, ComputerDetailsDto>(config);
});

builder.Services.AddHostedService<ComputerProducer>();

var host = builder.Build();
host.Run();
