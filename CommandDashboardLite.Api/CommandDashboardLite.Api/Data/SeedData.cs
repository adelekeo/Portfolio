using CommandDashboardLite.Api.Auth;
using CommandDashboardLite.Api.Models;

namespace CommandDashboardLite.Api.Data;

public static class SeedData
{
    public static void Run(AppDbContext db)
    {
        if (!db.Roles.Any())
        {
            db.Roles.AddRange(new Role { Name = "Admin" }, new Role { Name = "Viewer" });
            db.SaveChanges();
        }

        if (!db.Users.Any())
        {
            var adminRole = db.Roles.Single(r => r.Name == "Admin");
            var viewerRole = db.Roles.Single(r => r.Name == "Viewer");

            var admin = new User
            {
                Username = "admin",
                PasswordHash = PasswordHasher.Hash("Password123!")
            };
            admin.Roles.Add(adminRole);

            var viewer = new User
            {
                Username = "viewer",
                PasswordHash = PasswordHasher.Hash("Password123!")
            };
            viewer.Roles.Add(viewerRole);

            db.Users.AddRange(admin, viewer);
            db.SaveChanges();
        }

        if (!db.Assets.Any())
        {
            db.Assets.AddRange(
                new Asset { Name = "Langley AFB", Type = "Base", Location = "VA", Status = "Operational" },
                new Asset { Name = "C2 Node Alpha", Type = "Node", Location = "CONUS", Status = "Operational" },
                new Asset { Name = "Tanker-01", Type = "Aircraft", Location = "In Transit", Status = "Maintenance" }
            );
            db.SaveChanges();
        }
    }
}
