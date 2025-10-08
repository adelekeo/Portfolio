using Microsoft.EntityFrameworkCore;

namespace MissionPlanner.Api.Domain;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Mission> Missions => Set<Mission>();
}