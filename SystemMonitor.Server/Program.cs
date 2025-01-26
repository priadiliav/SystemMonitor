using Confluent.Kafka;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using SystemMonitor.DataService.Contracts;
using SystemMonitor.DataService.Data;
using SystemMonitor.DataService.Repositories;
using SystemMonitor.MessageBroker;
using SystemMonitor.MessageBroker.Kafka;
using SystemMonitor.Models.Configs;
using SystemMonitor.Models.Dtos;
using SystemMonitor.Server.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddLogging(config =>
{
    config.AddDebug();
    config.AddConsole();
});

builder.Services.Configure<KafkaConsumerSettings>(builder.Configuration.GetSection("Kafka"));

var dbConnectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(
    option => option.UseNpgsql(dbConnectionString));

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddScoped<IComputerDetailsRepository, ComputerDetailsRepository>();
builder.Services.AddScoped<IComputerMetricsRepository, ComputerMetricsRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<ComputerService>();

builder.Services.AddScoped<IKafkaHandler<string, ComputerDetailsDto>, ComputerHandler>();
builder.Services.AddSingleton<IKafkaConsumer<string, ComputerDetailsDto>>(sp =>
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
builder.Services.AddHostedService<ComputerConsumer>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
