using Microsoft.EntityFrameworkCore;
using SystemMonitor.Auth.Services;
using SystemMonitor.DataService.Contracts;
using SystemMonitor.DataService.Data;
using SystemMonitor.DataService.Repositories;
using SystemMonitor.Security;
using SystemMonitor.Security.Impls;

var builder = WebApplication.CreateBuilder(args);

var dbConnectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(
    option => option.UseNpgsql(dbConnectionString));

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddSingleton<IDecryptor, Decryptor>();
builder.Services.AddSingleton<IEncryptor, Encryptor>();

builder.Services.AddScoped<JwtTokenService>();
builder.Services.AddScoped<AuthService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


// For now, we don't need HTTPS
//app.UseHttpsRedirection();

app.MapControllers();
app.Run();
