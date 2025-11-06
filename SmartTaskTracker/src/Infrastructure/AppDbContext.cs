// src/Infrastructure/AppDbContext.cs
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure;
public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<AppUser> Users => Set<AppUser>();
    public DbSet<Project> Projects => Set<Project>();
    public DbSet<TaskItem> Tasks => Set<TaskItem>();

    protected override void OnModelCreating(ModelBuilder b)
    {
        b.Entity<AppUser>()
            .HasIndex(x => x.Email).IsUnique();

        b.Entity<Project>()
            .Property(p => p.Name).HasMaxLength(200).IsRequired();

        b.Entity<TaskItem>()
            .HasOne(t => t.Project)
            .WithMany(p => p.Tasks)
            .HasForeignKey(t => t.ProjectId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
