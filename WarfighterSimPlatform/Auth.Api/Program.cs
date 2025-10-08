using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

var jwtKey = builder.Configuration["Jwt:Key"] ?? "dev-secret-key-change-me";
var issuer = builder.Configuration["Jwt:Issuer"] ?? "WarfighterSim";
var audience = builder.Configuration["Jwt:Audience"] ?? "WarfighterClients";

builder.Services
    .AddAuthentication().AddJwtBearer(o =>
    {
        o.TokenValidationParameters = new()
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = issuer,
            ValidAudience = audience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
        };
    });
builder.Services.AddAuthorization();

var app = builder.Build();

app.MapPost("/auth/token", ([FromBody] LoginDto login) =>
{
    // DEMO ONLY: accept any non-empty username/pw
    if (string.IsNullOrWhiteSpace(login.Username) || string.IsNullOrWhiteSpace(login.Password))
        return Results.BadRequest("Invalid credentials");

    var claims = new[]
    {
        new Claim(JwtRegisteredClaimNames.Sub, login.Username),
        new Claim("role","operator")
    };

    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
    var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
    var token = new JwtSecurityToken(issuer, audience, claims, expires: DateTime.UtcNow.AddHours(8), signingCredentials: creds);
    var jwt = new JwtSecurityTokenHandler().WriteToken(token);
    return Results.Ok(new { access_token = jwt });
});

app.MapGet("/auth/health", [AllowAnonymous] () => Results.Ok(new { status = "ok", service = "Auth.Api" }));

app.Run();

record LoginDto(string Username, string Password);
