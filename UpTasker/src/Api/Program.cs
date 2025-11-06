using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using UpTasker.Api.Data;
using UpTasker.Api.Entities;

var builder = WebApplication.CreateBuilder(args);

// EF Core (SQL Server)
builder.Services.AddDbContext<AppDbContext>(opt =>
    opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// JWT (dev)
var jwtKey = builder.Configuration["Jwt:Key"] ?? "super-secret-development-key-CHANGE-ME";
var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(o =>
    {
        o.TokenValidationParameters = new()
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = key
        };
    });

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "UpTasker API", Version = "v1" })
);

// CORS for UI
builder.Services.AddCors(o =>
    o.AddDefaultPolicy(p => p.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()));

var app = builder.Build();
/*
// Optional: seed a couple clients on first run
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    await db.Database.MigrateAsync();
    if (!db.Clients.Any())
    {
        db.Clients.AddRange(
            new Client(0, "Acme Corp", "ops@acme.test", "555-0100", null),
            new Client(0, "Blue Sky LLC", "contact@bluesky.test", "555-0111", "VIP")
        );
        await db.SaveChangesAsync();
    }
}
*/

app.UseCors();
app.UseSwagger();
app.UseSwaggerUI();
app.UseAuthentication();
//app.UseAuthorization();

// Health
app.MapGet("/health", () => Results.Ok(new { status = "Healthy", at = DateTimeOffset.UtcNow }));

// Minimal CRUD: Clients
app.MapGet("/api/clients", async (AppDbContext db) =>
    await db.Clients.OrderBy(c => c.Name).ToListAsync());

app.MapPost("/api/clients", async (AppDbContext db, ClientDto dto) =>
{
    var c = new Client(0, dto.Name, dto.ContactEmail, dto.Phone, dto.Notes);
    db.Add(c);
    await db.SaveChangesAsync();
    return Results.Created($"/api/clients/{c.Id}", c);
});

app.Run();

record ClientDto(string Name, string? ContactEmail, string? Phone, string? Notes);
