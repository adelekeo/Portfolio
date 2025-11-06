using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using SecureMissionOps.Client;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// Point this to your API base URL (local dev: API Kestrel)
var apiBase = builder.Configuration["ApiBaseUrl"] ?? "http://localhost:8080/";

builder.Services.AddBlazoredLocalStorage();
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(apiBase) });
builder.Services.AddScoped<TokenHandler>();
builder.Services.AddScoped(sp =>
{
    var handler = sp.GetRequiredService <TokenHandler>();
    return new HttpClient(handler) { BaseAddress = new Uri(apiBase) };
});

await builder.Build().RunAsync();

