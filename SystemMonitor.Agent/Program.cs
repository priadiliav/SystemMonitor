using SystemMonitor.Agent.Builders;
using SystemMonitor.Agent.Collectors;
using SystemMonitor.Agent.Configs;
using SystemMonitor.Agent.Extantions;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddSingleton<Globals>();

builder.Services.AddSingleton<IMetricCollector, CpuUsageCollector>();
builder.Services.AddSingleton<IMetricCollector, DiskUsageCollector>();
builder.Services.AddSingleton<IMetricCollector, NetworkUsageCollector>();
builder.Services.AddSingleton<IMetricCollector, RamUsageCollector>();

builder.Services.AddSingleton<ComputerBuilder>();

builder.Services.AddProducers(builder.Configuration);

var host = builder.Build();
host.Run();