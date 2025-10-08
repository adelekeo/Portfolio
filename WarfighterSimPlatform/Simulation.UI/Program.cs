var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

// UI talks to the gateway inside the Docker network
builder.Services.AddHttpClient("Gateway",
    c => c.BaseAddress = new Uri("http://gateway.api:8090"));

var app = builder.Build();

app.UseStaticFiles();
app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host"); // this exists in blazorserver template

app.Run();

