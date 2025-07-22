var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddScoped<SkyTrack.Api.Services.SensorService>();
builder.Services.AddScoped<SkyTrack.Api.Services.NotificationService>();
builder.Services.AddScoped<SkyTrack.Api.Services.VideoService>();
builder.Services.AddScoped<SkyTrack.Api.Services.SimulationService>();

builder.Services.AddHostedService<SkyTrack.Api.Services.VideoCleanupHostedService>();

builder.Services.Configure<SkyTrack.Api.Models.CleanupOptions>(builder.Configuration.GetSection("CleanupOptions"));

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
