

using SkyTrack.Api.Services;

public class MqttHostedService : BackgroundService
{
    private readonly MqttService _mqttService;

    public MqttHostedService(MqttService mqttService)
    {
        _mqttService = mqttService;
    }
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await _mqttService.StartAsync();
        while (!stoppingToken.IsCancellationRequested)
        {
            await Task.Delay(1000, stoppingToken);
        }
    }
}