using Microsoft.EntityFrameworkCore;
using SecureMissionOps.API.Models;

namespace SecureMissionOps.API.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Mission> Missions => Set<Mission>();

    protected override void OnModelCreating(ModelBuilder b)
    {
        b.Entity<Mission>()
            .Property(m => m.Title).HasMaxLength(120).IsRequired();
        b.Entity<Mission>()
            .Property(m => m.Priority).HasDefaultValue(3);
    }
}

