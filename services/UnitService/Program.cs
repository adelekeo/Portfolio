using Contracts;
using Microsoft.EntityFrameworkCore;
using UnitService;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<UnitDb>(opt =>
    opt.UseSqlServer(builder.Configuration.GetConnectionString("Default")));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var app = builder.Build();

// Auto-migrate & seed
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<UnitDb>();
    db.Database.Migrate();
    if (!db.Units.Any())
    {
        db.Units.AddRange(
            new Unit { Callsign = "Alpha-1", Side = "Blue", Strength = 100, MissionId = 1 },
            new Unit { Callsign = "Bravo-2", Side = "Blue", Strength = 95, MissionId = 1 },
            new Unit { Callsign = "Foxtrot", Side = "Red", Strength = 90, MissionId = 1 },
            new Unit { Callsign = "SAM-21", Side = "Red", Strength = 85, MissionId = 2 }
        );
        db.SaveChanges();
    }
}

app.UseSwagger().UseSwaggerUI();

app.MapGet("/units", async (UnitDb db) =>
    (await db.Units.AsNoTracking().ToListAsync())
        .Select(u => new UnitDto(u.Id, u.Callsign, u.Side, u.Strength, u.MissionId)));

app.MapPost("/units", async (UnitDb db, CreateUnitDto dto) =>
{
    var u = new Unit { Callsign = dto.Callsign, Side = dto.Side, Strength = dto.Strength, MissionId = dto.MissionId };
    db.Units.Add(u);
    await db.SaveChangesAsync();
    return Results.Created($"/units/{u.Id}", new UnitDto(u.Id, u.Callsign, u.Side, u.Strength, u.MissionId));
});

app.MapPut("/units/{id:int}", async (UnitDb db, int id, CreateUnitDto dto) =>
{
    var u = await db.Units.FindAsync(id);
    if (u is null) return Results.NotFound();
    u.Callsign = dto.Callsign; u.Side = dto.Side; u.Strength = dto.Strength; u.MissionId = dto.MissionId;
    await db.SaveChangesAsync();
    return Results.NoContent();
});

app.MapDelete("/units/{id:int}", async (UnitDb db, int id) =>
{
    var u = await db.Units.FindAsync(id);
    if (u is null) return Results.NotFound();
    db.Units.Remove(u);
    await db.SaveChangesAsync();
    return Results.NoContent();
});

app.Run();
