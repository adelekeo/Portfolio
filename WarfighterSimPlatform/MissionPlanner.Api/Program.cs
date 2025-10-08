using Microsoft.EntityFrameworkCore;
using MissionPlanner.Api.Domain;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("SqlServer"),
        sql =>
        {
            sql.EnableRetryOnFailure(
                maxRetryCount: 5,
                maxRetryDelay: TimeSpan.FromSeconds(10),
                errorNumbersToAdd: null);
        }));


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.MapGet("/health", () => Results.Ok(new { status = "ok", service = "MissionPlanner.Api" }));

app.MapGet("/api/missions", async (AppDbContext db) => await db.Missions.ToListAsync());
app.MapGet("/api/missions/{id:int}", async (int id, AppDbContext db) =>
    await db.Missions.FindAsync(id) is { } m ? Results.Ok(m) : Results.NotFound());

app.MapPost("/api/missions", async (Mission mission, AppDbContext db) =>
{
    db.Missions.Add(mission);
    await db.SaveChangesAsync();
    return Results.Created($"/api/missions/{mission.Id}", mission);
});

app.MapPut("/api/missions/{id:int}", async (int id, Mission updated, AppDbContext db) =>
{
    var existing = await db.Missions.FindAsync(id);
    if (existing is null) return Results.NotFound();
    existing.Name = updated.Name;
    existing.Theater = updated.Theater;
    existing.StartUtc = updated.StartUtc;
    existing.EndUtc = updated.EndUtc;
    existing.Classification = updated.Classification;
    await db.SaveChangesAsync();
    return Results.NoContent();
});

app.MapDelete("/api/missions/{id:int}", async (int id, AppDbContext db) =>
{
    var existing = await db.Missions.FindAsync(id);
    if (existing is null) return Results.NotFound();
    db.Missions.Remove(existing);
    await db.SaveChangesAsync();
    return Results.NoContent();
});

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    await db.Database.EnsureCreatedAsync();

    if (!await db.Missions.AnyAsync())
    {
        db.Missions.Add(new Mission
        {
            Name = "Pacific Shield",
            Theater = "Pacific",
            Classification = "U/FOUO",
            StartUtc = DateTime.UtcNow,
            EndUtc = DateTime.UtcNow.AddHours(2)
        });
        await db.SaveChangesAsync();
    }
}

app.Run();
