// src/Infrastructure/DbSeeder.cs
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using TaskStatus = Domain.Entities.TaskStatus;

namespace Infrastructure;
public static class DbSeeder
{
    public static async Task SeedAsync(AppDbContext db, string adminEmail, string adminPasswordHash)
    {
        await db.Database.MigrateAsync();

        if (!await db.Users.AnyAsync())
        {
            db.Users.Add(new AppUser
            {
                Email = adminEmail,
                PasswordHash = adminPasswordHash,
                Role = Roles.Admin
            });
        }
        if (!await db.Projects.AnyAsync())
        {
            var p = new Project { Name = "Demo Project", Description = "Smart Task Tracker demo" };
            db.Projects.Add(p);
            db.Tasks.AddRange(
                new TaskItem { Project = p, Title = "Set up repo", Status = TaskStatus.Done },
                new TaskItem { Project = p, Title = "Add auth", Status = TaskStatus.InProgress },
                new TaskItem { Project = p, Title = "CRUD tasks", Status = TaskStatus.Todo }
            );
        }
        await db.SaveChangesAsync();
    }
}

