using System.Text.Json.Serialization;

namespace SolarToTelegrafHTTPProxy.Services.Mqtt.Models;

public class Message : SolarInfo
{
    [JsonPropertyName("unique_id")]
    public string UniqueId { get; set; } = "iconica_solar";

    [JsonPropertyName("name")] 
    public string Name { get; set; } = "Iconica Solar Stats";
}