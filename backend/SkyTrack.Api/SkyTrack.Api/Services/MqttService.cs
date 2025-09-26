using System.Text;
using MQTTnet;
using MQTTnet.Protocol;

namespace SkyTrack.Api.Services
{
    public class MqttService
    {
        private readonly ILogger<MqttClient> _logger;
        private readonly IMqttClient _client;
        private readonly MqttClientOptions _options;

        public MqttService(ILogger<MqttClient> logger, IConfiguration config)
        {
            this._logger = logger;
            var factory = new MqttClientFactory();
            _client = factory.CreateMqttClient();

            var host = config["MqttSettings:Host"];
            var portString = config["MqttSettings:Port"];

            if (string.IsNullOrWhiteSpace(host))
                throw new InvalidOperationException("MQTT Host is not configured.");

            if (!int.TryParse(portString, out int port))
                throw new InvalidOperationException("MQTT Port is not configured or invalid.");

            _logger.LogInformation($"Configuring MQTT client to connect to {host}:{port}");

            _options = new MqttClientOptionsBuilder()
                .WithTcpServer(host, port)
                .WithClientId("backend-service")
                .Build();

            _client.ApplicationMessageReceivedAsync += HandleMessage;
        }

        private Task HandleMessage(MqttApplicationMessageReceivedEventArgs e)
        {
            string topic = e.ApplicationMessage.Topic;
            string payload = Encoding.UTF8.GetString(e.ApplicationMessage.Payload);
            _logger.LogInformation($"Received on {topic}: {payload}");

            switch (topic)
            {
                case "motion/detected":
                    _logger.LogInformation("Motion detected event received.");
                    break;


                default:
                    _logger.LogWarning($"Unhandled topic: {topic}");
                    break;
            }

            return Task.CompletedTask;
        }

        public async Task StartAsync()
        {
            await _client.ConnectAsync(_options);

            await _client.SubscribeAsync(new MqttTopicFilterBuilder()
                .WithTopic("motion/detected")
                .WithQualityOfServiceLevel(MqttQualityOfServiceLevel.AtLeastOnce)
                .Build());

            await _client.SubscribeAsync(new MqttTopicFilterBuilder()
                .WithTopic("video/recorded")
                .WithQualityOfServiceLevel(MqttQualityOfServiceLevel.AtLeastOnce)
                .Build());

            await _client.SubscribeAsync(new MqttTopicFilterBuilder()
                .WithTopic("camera/error")
                .WithQualityOfServiceLevel(MqttQualityOfServiceLevel.AtLeastOnce)
                .Build());
        }


    }
}