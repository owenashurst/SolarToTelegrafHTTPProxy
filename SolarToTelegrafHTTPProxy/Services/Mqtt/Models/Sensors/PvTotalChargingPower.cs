using System.Text.Json.Serialization;

namespace SolarToTelegrafHTTPProxy.Services.Mqtt.Models.Sensors;

public class PvTotalChargingPower : IConfig
{
    public string ConfigTopicName { get; set; } = "homeassistant/sensor/iconica_solar_pv_total_charging_power/config";

    [JsonPropertyName("device_class")] 
    public string DeviceClass { get; set; } = "number";

    [JsonPropertyName("name")] 
    public string Name { get; set; } = "PV Total Charging Power";

    [JsonPropertyName("state_topic")] 
    public string StateTopic { get; set; } = Config.StateTopicName;

    [JsonPropertyName("unit_of_measurement")]
    public string UnitOfMeasurement { get; set; } = "power";

    [JsonPropertyName("value_template")] 
    public string ValueTemplate { get; set; } = "{{ value_json.pvTotalChargingPower }}";

    [JsonPropertyName("unique_id")] 
    public string UniqueId { get; set; } = "pvtotalchargingpower";

    [JsonPropertyName("device")]
    public Device Device { get; init; } = new()
    {
        Name = "IconicaSolar",
        Identifiers = new[] { "pvTotalChargingPower" }
    };
}