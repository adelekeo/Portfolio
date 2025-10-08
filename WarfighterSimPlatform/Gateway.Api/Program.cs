using Yarp.ReverseProxy;
using Yarp.ReverseProxy.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddReverseProxy()
    .LoadFromMemory(
        new[]
        {
            new RouteConfig
            {
                RouteId = "missions",
                ClusterId = "missionsCluster",
                Match = new RouteMatch { Path = "/api/missions/{**catch-all}" }
            },
            new RouteConfig
            {
                RouteId = "auth",
                ClusterId = "authCluster",
                Match = new RouteMatch { Path = "/auth/{**catch-all}" }
            },
            new RouteConfig
            {
                RouteId = "health",
                ClusterId = "missionsCluster",
                Match = new RouteMatch { Path = "/health" }
            }
        },
        new[]
        {
            new ClusterConfig
            {
                ClusterId = "missionsCluster",
                Destinations = new Dictionary<string, DestinationConfig>
                {
                    ["d1"] = new() { Address = "http://missionplanner.api:8080" }
                }
            },
            new ClusterConfig
            {
                ClusterId = "authCluster",
                Destinations = new Dictionary<string, DestinationConfig>
                {
                    ["d1"] = new() { Address = "http://auth.api:8081" }
                }
            }
        });

var app = builder.Build();
app.MapReverseProxy();
app.Run();
