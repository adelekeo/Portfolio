using System.Net.Http.Json;
using Contracts;

var builder = WebApplication.CreateBuilder(args);
builder.Services.Configure<HttpClientFactoryOptions>("mission", _ => { });
builder.Services.AddHttpClient("mission", (sp, client) =>
{
    client.BaseAddress = new Uri(builder.Configuration["Mission:BaseUrl"] ?? "http://localhost:5001");
});
builder.Services.AddHttpClient("unit", (sp, client) =>
{
    client.BaseAddress = new Uri(builder.Configuration["Unit:BaseUrl"] ?? "http://localhost:5002");
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var app = builder.Build();
app.UseSwagger().UseSwaggerUI();

app.MapPost("/simulate", async (int missionId, IHttpClientFactory factory) =>
{
    var mClient = factory.CreateClient("mission");
    var uClient = factory.CreateClient("unit");

    // naive fetches (could add DTOs but keep simple)
    var missions = await mClient.GetFromJsonAsync<List<MissionDto>>("/missions");
    var mission = missions?.FirstOrDefault(x => x.Id == missionId);
    if (mission is null) return Results.NotFound($"Mission {missionId} not found.");

    var units = await uClient.GetFromJsonAsync<List<UnitDto>>("/units");
    var forMission = units?.Where(u => u.MissionId == missionId).ToList() ?? [];
    if (forMission.Count == 0) return Results.BadRequest("No units assigned.");

    // toy computation
    var blue = forMission.Where(u => u.Side.Equals("Blue", StringComparison.OrdinalIgnoreCase)).Sum(u => u.Strength);
    var red  = forMission.Where(u => u.Side.Equals("Red",  StringComparison.OrdinalIgnoreCase)).Sum(u => u.Strength);
    var ratio = (blue + 1.0m) / (red + 1.0m);
    var success = Math.Clamp((ratio / 2m) * 100m, 5m, 95m);
    var result = new SimulationResultDto(missionId: missionId, SuccessProbability: decimal.Round(success, 2),
        FriendlyCasualties: (int)Math.Max(0, 100 - success), AdversaryCasualties: (int)Math.Min(100, success),
        Notes: "Toy calculation for demo.");

    return Results.Ok(result);
});

app.Run();
