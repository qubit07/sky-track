using SkyTrack.Api.Models;
using SkyTrack.Api.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddOpenApi();

builder.Services.AddScoped<SensorService>();
builder.Services.AddScoped<NotificationService>();
builder.Services.AddScoped<VideoService>();
builder.Services.AddScoped<SimulationService>();
builder.Services.AddScoped<IPhotoService, CloudinaryPhotoService>();

builder.Services.AddHostedService<VideoCleanupHostedService>();

builder.Services.Configure<CleanupOptions>(builder.Configuration.GetSection("CleanupOptions"));
builder.Services.Configure<CloudOptions>(builder.Configuration.GetSection("CloudOptions"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
