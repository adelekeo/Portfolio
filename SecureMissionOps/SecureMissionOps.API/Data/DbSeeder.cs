using SecureMissionOps.API.Data;
using SecureMissionOps.API.Models;

namespace SecureMissionOps.API.Data;

public static class DbSeeder
{
    public static async Task SeedAsync(AppDbContext db)
    {
        if (db.Missions.Any()) return;
        db.Missions.AddRange(
            new Mission { Title = "Initial Recon", Description = "Spin up baseline", Priority = 2, OwnerUserId = "admin", Status = MissionStatus.Active },
            new Mission { Title = "Harden Perimeter", Description = "Apply controls", Priority = 1, OwnerUserId = "admin", Status = MissionStatus.Planned }
        );
        await db.SaveChangesAsync();
    }
}

