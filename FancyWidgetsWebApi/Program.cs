using System.Reflection;
using FancyWidgets.Application;
using FancyWidgets.Application.Common.Mappings;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();

builder.Configuration.AddEnvironmentVariables()
    .AddUserSecrets(Assembly.GetExecutingAssembly(), true);
builder.Configuration
    .AddJsonFile("/etc/secrets/secrets.json", true);

builder.Services.AddApplication(builder.Configuration);
builder.Services.AddAutoMapper(config =>
{
    config.AddProfile(new AssemblyMappingProfile(Assembly.GetExecutingAssembly()));
});

var app = builder.Build();

if (!app.Environment.IsDevelopment())
    app.UseHsts();

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html");

app.Run();