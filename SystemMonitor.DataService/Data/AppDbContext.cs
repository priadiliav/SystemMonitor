using Microsoft.EntityFrameworkCore;
using SystemMonitor.Models.Entities;

namespace SystemMonitor.DataService.Data;
public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public virtual DbSet<ComputerDetails> ComputerDetails { get; set; }
    public virtual DbSet<ComputerMetrics> ComputerMetrics { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        //  The name should be unique 
        modelBuilder.Entity<ComputerDetails>()
            .HasIndex(c => c.Name)
            .IsUnique();

        // specifying the relationship between the entities
        modelBuilder.Entity<ComputerDetails>()
            .HasOne(c => c.Metrics)
            .WithOne(m => m.ComputerDetails)
            .HasForeignKey<ComputerMetrics>(m => m.ComputerDetailsId)
            .OnDelete(DeleteBehavior.Cascade);
    }

    public override int SaveChanges()
    {
        var entries = ChangeTracker.Entries<BaseEntity>();

        foreach (var entry in entries)
        {
            if (entry.State == EntityState.Added)
            {
                entry.Entity.CreatingDateTime = DateTime.UtcNow;
                entry.Entity.UpdatingDateTime = DateTime.UtcNow;
            }
            else if (entry.State == EntityState.Modified)
            {
                entry.Entity.UpdatingDateTime = DateTime.UtcNow;
            }
        }

        return base.SaveChanges();
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var entries = ChangeTracker.Entries<BaseEntity>();

        foreach (var entry in entries)
        {
            if (entry.State == EntityState.Added)
            {
                entry.Entity.CreatingDateTime = DateTime.UtcNow;
                entry.Entity.UpdatingDateTime = DateTime.UtcNow;
            }
            else if (entry.State == EntityState.Modified)
            {
                entry.Entity.UpdatingDateTime = DateTime.UtcNow;
            }
        }

        return await base.SaveChangesAsync(cancellationToken);
    }
}