using MyPortfolio.Application.Extensions;
using MyPortfolio.Infrastructure.Extensions;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
        options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
        options.JsonSerializerOptions.WriteIndented = true;
    });

builder.Services.AddInfrastructureServices();
builder.Services.AddApplicationServices();

// Register repositories

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

// Ensure plugins directory exists in all possible locations
// Main content root directory
var pluginsDirectory = Path.Combine(app.Environment.ContentRootPath, "Plugins");
if (!Directory.Exists(pluginsDirectory))
{
    Directory.CreateDirectory(pluginsDirectory);
}

// In bin directory where the assembly is executed
var binPluginsDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Plugins");
if (!Directory.Exists(binPluginsDir))
{
    Directory.CreateDirectory(binPluginsDir);
}

// Ensure images directories exist
var imagesDirectory = Path.Combine(app.Environment.WebRootPath, "images");
if (!Directory.Exists(imagesDirectory))
{
    Directory.CreateDirectory(imagesDirectory);
}

var projectsImageDirectory = Path.Combine(imagesDirectory, "projects");
if (!Directory.Exists(projectsImageDirectory))
{
    Directory.CreateDirectory(projectsImageDirectory);
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();