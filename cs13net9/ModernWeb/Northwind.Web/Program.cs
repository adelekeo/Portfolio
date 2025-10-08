using Northwind.Web.Components;


#region Configure the web server host and services
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddRazorComponents();
var app = builder.Build();
#endregion


#region Configure the HTTP pipeline and routes
if (!app.Environment.IsDevelopment())
{
   // app.UseExceptionHandler("/Error");
    app.UseHsts();
}
app.UseHttpsRedirection();
app.UseAntiforgery();

app.UseDefaultFiles(); // index.html, default.html, and so on.
//app.MapStaticAssets(); // .NET 9 or later.
app.UseStaticFiles(); // wwwroot folder.
app.MapRazorComponents<App>();

app.MapGet("/env", () =>
  $"Environment is {app.Environment.EnvironmentName}");

#endregion
// Start the web server, host the website, and wait for requests
app.Run();
WriteLine("This executes after the web server has stopped!");
