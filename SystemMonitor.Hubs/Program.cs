using SystemMonitor.Hubs.Extantions;
using SystemMonitor.Hubs.Hubs;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddJwt(builder.Configuration);

builder.Services.CustomCors();
builder.Services.AddSignalR();

builder.Services.AddKafkaComputerHandler(builder.Configuration);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseRouting();
app.UseCors();

app.UseAuthentication();
app.UseAuthorization();

app.MapHub<ComputerHub>("/computerHub");

app.Run();