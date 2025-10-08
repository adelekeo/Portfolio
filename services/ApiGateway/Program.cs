var builder = WebApplication.CreateBuilder(args);

var missionUrl = builder.Configuration["MISSION_URL"] ?? "http://localhost:5001";
var unitUrl    = builder.Configuration["UNIT_URL"]    ?? "http://localhost:5002";
var simUrl     = builder.Configuration["SIM_URL"]     ?? "http://localhost:5003";

builder.Services.AddHttpClient("mission", c => c.BaseAddress = new Uri(missionUrl));
builder.Services.AddHttpClient("unit",    c => c.BaseAddress = new Uri(unitUrl));
builder.Services.AddHttpClient("sim",     c => c.BaseAddress = new Uri(simUrl));

var app = builder.Build();

app.MapGet("/api/missions", async (IHttpClientFactory f) =>
    await f.CreateClient("mission").GetStringAsync("/missions"));

app.MapPost("/api/missions", async (HttpContext ctx, IHttpClientFactory f) =>
    await f.CreateClient("mission").PostAsync("/missions", new StreamContent(ctx.Request.Body)));

app.MapGet("/api/units", async (IHttpClientFactory f) =>
    await f.CreateClient("unit").GetStringAsync("/units"));

app.MapPost("/api/units", async (HttpContext ctx, IHttpClientFactory f) =>
    await f.CreateClient("unit").PostAsync("/units", new StreamContent(ctx.Request.Body)));

app.MapPost("/api/simulate", async (HttpContext ctx, IHttpClientFactory f) =>
{
    var query = ctx.Request.QueryString.HasValue ? ctx.Request.QueryString.Value : "";
    return await f.CreateClient("sim").PostAsync($"/simulate{query}", new StringContent(""));
});

app.Run();
