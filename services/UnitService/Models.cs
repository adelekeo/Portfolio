using Microsoft.EntityFrameworkCore;

namespace UnitService;

public class Unit
{
    public int Id { get; set; }
    public string Callsign { get; set; } = "";
    public string Side { get; set; } = "Blue"; // Blue/Red
    public int Strength { get; set; } = 100;
    public int? MissionId { get; set; }
}

public class UnitDb : DbContext
{
    public UnitDb(DbContextOptions<UnitDb> options) : base(options) {}
    public DbSet<Unit> Units => Set<Unit>();
}
