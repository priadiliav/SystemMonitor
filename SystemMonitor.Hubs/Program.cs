using Confluent.Kafka;
using Microsoft.Extensions.Options;
using SystemMonitor.Hubs.Hubs;
using SystemMonitor.Hubs.Services;
using SystemMonitor.MessageBroker;
using SystemMonitor.MessageBroker.Kafka;
using SystemMonitor.Models.Configs;
using SystemMonitor.Models.Dtos;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSignalR();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.WithOrigins("http://localhost:4200")
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials();
    });
});

// settings
builder.Services.Configure<KafkaConsumerSettings>(builder.Configuration.GetSection("Kafka"));
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
        kafkaSettings.Topic
    );
});
builder.Services.AddHostedService<ComputerConsumer>();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseAuthorization();
app.UseCors();
app.MapHub<ComputerHub>("/computerHub");

app.Run();

