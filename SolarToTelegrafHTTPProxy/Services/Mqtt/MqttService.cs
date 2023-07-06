using System;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Protocol;
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

    public async Task<bool> PublishMessageToBrokerAsync(InverterInformation inverterInformation)
    {
        using var mqttClient = _mqttFactory.CreateMqttClient();
        
        var mqttClientOptions = new MqttClientOptionsBuilder()
            .WithTcpServer(_mqttSettings.Address, _mqttSettings.Port)
            .WithCredentials(_mqttSettings.Username, _mqttSettings.Password).Build();

        try
        {
            await mqttClient.ConnectAsync(mqttClientOptions, CancellationToken.None);

            _logger.LogInformation("Connected to MQTT broker");

            var serialisedMqttMessage = JsonSerializer.Serialize(inverterInformation, new JsonSerializerOptions(JsonSerializerDefaults.Web));
            
            _logger.LogInformation("Sending MQTT message: {Message}", serialisedMqttMessage);
            
            var message = new MqttApplicationMessageBuilder()
                .WithTopic(Models.Config.StateTopicName)
                .WithPayload(serialisedMqttMessage)
                .WithQualityOfServiceLevel(MqttQualityOfServiceLevel.AtMostOnce)
                .WithRetainFlag(true)
                .Build();

            var result = await mqttClient.PublishAsync(message, CancellationToken.None);

            if (result.IsSuccess)
            {
                _logger.LogInformation("Successfully sent MQTT message");
            }
            else
            {
                _logger.LogError("Error when sending MQTT message");
            }
        
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
    
    public async Task<bool> PublishConfigMessageToBrokerAsync(IConfig config)
    {
        using var mqttClient = _mqttFactory.CreateMqttClient();
        
        var mqttClientOptions = new MqttClientOptionsBuilder()
            .WithTcpServer(_mqttSettings.Address, _mqttSettings.Port)
            .WithCredentials(_mqttSettings.Username, _mqttSettings.Password).Build();

        try
        {
            await mqttClient.ConnectAsync(mqttClientOptions, CancellationToken.None);

            _logger.LogInformation("Connected to MQTT broker");
            
            var serialisedMqttMessage = JsonSerializer.Serialize(config, config.GetType(), new JsonSerializerOptions(JsonSerializerDefaults.Web));

            _logger.LogInformation("Sending Config MQTT message: {Message}", serialisedMqttMessage);
            
            var message = new MqttApplicationMessageBuilder()
                .WithTopic(config.ConfigTopicName)
                .WithPayload(serialisedMqttMessage)
                .WithQualityOfServiceLevel(MqttQualityOfServiceLevel.AtMostOnce)
                .WithRetainFlag(true)
                .Build();

            var result = await mqttClient.PublishAsync(message, CancellationToken.None);

            if (result.IsSuccess)
            {
                _logger.LogInformation("Successfully sent Config MQTT message");
            }
            else
            {
                _logger.LogError("Error when sending Config MQTT message");
            }
        
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