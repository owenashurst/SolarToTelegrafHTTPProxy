using System.Text.Json.Serialization;
using SolarToTelegrafHTTPProxy.Config;

namespace SolarToTelegrafHTTPProxy.Services.Mqtt.Models;

public class Config
{
    public const string StateTopicName = "homeassistant/sensor/iconica_solar/state";
    
    [JsonPropertyName("ac_output_apparent_power_t")]
    public string ACOutputApparentPower { get; set; } = MqttSettings.MessageTopic;
    [JsonPropertyName("ac_output_apparent_power_tpl")]
    public string ACOutputApparentPowerTemplate { get; set; } = "{{ value_json.acOutputApparentPower }}";
}