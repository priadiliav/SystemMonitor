using Microsoft.EntityFrameworkCore;
using SystemMonitor.DataService.Data;

namespace SystemMonitor.Server.Extentions;

public static class DatabaseExtantion
{
    public static void AddPostgresDb(this IServiceCollection services, IConfiguration configuration)
    {
        var dbConnectionString = configuration.GetConnectionString("DefaultConnection");
        services.AddDbContext<AppDbContext>(
            option => option.UseNpgsql(dbConnectionString));
    }

    public static void MigrateDatabase(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        dbContext.Database.Migrate();
    }
}