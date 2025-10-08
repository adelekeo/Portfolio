using Microsoft.EntityFrameworkCore;

namespace MissionService;

public class Mission
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public string? Terrain { get; set; }
    public DateTimeOffset StartTime { get; set; } = DateTimeOffset.UtcNow;
    public DateTimeOffset? EndTime { get; set; }
}

public class MissionDb : DbContext
{
    public MissionDb(DbContextOptions<MissionDb> options) : base(options) {}
    public DbSet<Mission> Missions => Set<Mission>();
}
