using Confluent.Kafka;
using Microsoft.Extensions.Options;
using SystemMonitor.Auth.Extentions;
using SystemMonitor.Hubs.Hubs;
using SystemMonitor.Hubs.Services;
using SystemMonitor.MessageBroker;
using SystemMonitor.MessageBroker.Kafka;
using SystemMonitor.Models.Configs;
using SystemMonitor.Models.Dtos;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddJwtAuthentication(builder.Configuration);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(corsPolicyBuilder =>
    {
        corsPolicyBuilder.WithOrigins("http://localhost:4200")
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials();
    });
});
builder.Services.AddSignalR();

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

// For now, we don't need HTTPS
//app.UseHttpsRedirection();

app.UseRouting();
app.UseCors(policyBuilder => policyBuilder
    .AllowAnyHeader()
    .AllowAnyMethod()
    .SetIsOriginAllowed(_ => true)
    .AllowCredentials()
);


app.UseAuthentication();
app.UseAuthorization();


app.MapHub<ComputerHub>("/computerHub");
app.MapHub<WelcomeHub>("/welcomeHub");

app.Run();

