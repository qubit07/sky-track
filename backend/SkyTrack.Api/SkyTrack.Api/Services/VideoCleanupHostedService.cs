using Microsoft.Extensions.Options;
using SkyTrack.Api.Models;

namespace SkyTrack.Api.Services
{
    public class VideoCleanupHostedService : BackgroundService
    {

        private readonly ILogger<VideoCleanupHostedService> _logger;
        private readonly IServiceProvider _serviceProvider;
        private readonly IOptions<CleanupOptions> _options;

        public VideoCleanupHostedService(ILogger<VideoCleanupHostedService> logger, IServiceProvider serviceProvider, IOptions<CleanupOptions> options)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
            _options = options;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            if (!_options.Value.Enabled)
            {
                _logger.LogInformation("Video cleanup is disabled in the configuration.");
                return;
            }

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    // Perform video cleanup logic here
                    // For example, delete old video files or clean up the database
                    using var scope = _serviceProvider.CreateScope();
                    var videoService = scope.ServiceProvider.GetRequiredService<VideoService>();
                    await videoService.CleanupVideos();

                    _logger.LogInformation("Video cleanup operation completed successfully.");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "An error occurred during video cleanup.");
                }

                // Wait for a specified interval before the next cleanup operation
                var interval = TimeSpan.FromHours(_options.Value.IntervalHours);
                if (interval.TotalHours <= 0)
                {
                    _logger.LogWarning("Invalid cleanup interval configured. Defaulting to 12 hours.");
                    interval = TimeSpan.FromHours(12);
                }
                _logger.LogInformation($"Next video cleanup will occur in {interval.TotalHours} hours.");
                Task.Delay(interval, stoppingToken).Wait(stoppingToken);
            }
        }
    }
}
