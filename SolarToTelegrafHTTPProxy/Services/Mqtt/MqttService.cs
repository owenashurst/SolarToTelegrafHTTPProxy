using System;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MQTTnet;
using MQTTnet.Client;
using SolarToTelegrafHTTPProxy.Config;
using SolarToTelegrafHTTPProxy.Services.Mqtt.Models;

namespace SolarToTelegrafHTTPProxy.Services.Mqtt;

public class MqttService : IMqttService
{
    private readonly ILogger<MqttService> _logger;
    private readonly MqttSettings _mqttSettings;
    private readonly MqttFactory _mqttFactory;

    public MqttService(ILogger<MqttService> logger, IOptions<MqttSettings> mqttSettings)
    {
        _logger = logger;
        
        ArgumentNullException.ThrowIfNull(mqttSettings);
        _mqttSettings = mqttSettings.Value;
        
        _mqttFactory = new MqttFactory();
    }

    public async Task<bool> PublishMessageToBrokerAsync(Message mqttMessage)
    {
        using var mqttClient = _mqttFactory.CreateMqttClient();
        
        var mqttClientOptions = new MqttClientOptionsBuilder().WithTcpServer(_mqttSettings.Address, _mqttSettings.Port).Build();

        try
        {
            await mqttClient.ConnectAsync(mqttClientOptions, CancellationToken.None);

            _logger.LogInformation("Connected to MQTT broker.");
        
            var message = new MqttApplicationMessageBuilder()
                .WithTopic(_mqttSettings.Topic)
                .WithPayload(JsonSerializer.Serialize(mqttMessage))
                .Build();

            await mqttClient.PublishAsync(message, CancellationToken.None);
        
            var mqttClientDisconnectOptions = _mqttFactory.CreateClientDisconnectOptionsBuilder().Build();
        
            await mqttClient.DisconnectAsync(mqttClientDisconnectOptions, CancellationToken.None);

            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unable to send message to broker");

            return false;
        }
    }
}