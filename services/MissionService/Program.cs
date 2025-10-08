using Contracts;
using Microsoft.EntityFrameworkCore;
using MissionService;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<MissionDb>(opt =>
    opt.UseSqlServer(builder.Configuration.GetConnectionString("Default")));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var app = builder.Build();

// Auto-migrate & seed
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<MissionDb>();
    db.Database.Migrate();
    if (!db.Missions.Any())
    {
        db.Missions.AddRange(
            new Mission { Name = "Convoy Escorts — Urban", Terrain = "Urban" },
            new Mission { Name = "Air Defense Suppression", Terrain = "Desert" }
        );
        db.SaveChanges();
    }
}

app.UseSwagger().UseSwaggerUI();

app.MapGet("/missions", async (MissionDb db) =>
    (await db.Missions.AsNoTracking().ToListAsync())
        .Select(m => new MissionDto(m.Id, m.Name, m.Terrain, m.StartTime, m.EndTime)));

app.MapPost("/missions", async (MissionDb db, CreateMissionDto dto) =>
{
    var m = new Mission { Name = dto.Name, Terrain = dto.Terrain, StartTime = dto.StartTime ?? DateTimeOffset.UtcNow, EndTime = dto.EndTime };
    db.Missions.Add(m);
    await db.SaveChangesAsync();
    return Results.Created($"/missions/{m.Id}", new MissionDto(m.Id, m.Name, m.Terrain, m.StartTime, m.EndTime));
});

app.MapPut("/missions/{id:int}", async (MissionDb db, int id, CreateMissionDto dto) =>
{
    var m = await db.Missions.FindAsync(id);
    if (m is null) return Results.NotFound();
    m.Name = dto.Name;
    m.Terrain = dto.Terrain;
    m.StartTime = dto.StartTime ?? m.StartTime;
    m.EndTime = dto.EndTime;
    await db.SaveChangesAsync();
    return Results.NoContent();
});

app.MapDelete("/missions/{id:int}", async (MissionDb db, int id) =>
{
    var m = await db.Missions.FindAsync(id);
    if (m is null) return Results.NotFound();
    db.Missions.Remove(m);
    await db.SaveChangesAsync();
    return Results.NoContent();
});

app.Run();
