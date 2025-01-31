using SystemMonitor.DataService.Contracts;
using SystemMonitor.DataService.Repositories;
using SystemMonitor.Security;
using SystemMonitor.Server.Extentions;
using SystemMonitor.Server.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddLogging(config =>
{
    config.AddDebug();
    config.AddConsole();
});

builder.Services.CustomCors();
builder.Services.AddJwt(builder.Configuration);
builder.Services.AddPostgresDb(builder.Configuration);
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddScoped<IDecryptor, Decryptor>();
builder.Services.AddScoped<IEncryptor, Encryptor>();

builder.Services.AddScoped<IComputerDetailsRepository, ComputerDetailsRepository>();
builder.Services.AddScoped<IComputerMetricsRepository, ComputerMetricsRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<ComputerService>();

builder.Services.AddKafkaComputerHandler(builder.Configuration);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors();

app.UseAuthentication();
app.UseAuthorization();

app.MigrateDatabase();

app.MapControllers();
app.Run();