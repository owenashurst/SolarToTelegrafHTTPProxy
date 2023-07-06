using System.Text.Json.Serialization;

namespace SolarToTelegrafHTTPProxy.Services.Mqtt.Models.Sensors;

public class BatteryDischargeCurrent : IConfig
{
    [JsonIgnore]
    public string ConfigTopicName { get; set; } = "homeassistant/sensor/iconica_solar_battery_discharge_current/config";

    [JsonPropertyName("device_class")] 
    public string DeviceClass { get; set; } = "current";

    [JsonPropertyName("name")] 
    public string Name { get; set; } = "Battery Discharge Current";

    [JsonPropertyName("state_topic")] 
    public string StateTopic { get; set; } = Config.StateTopicName;

    [JsonPropertyName("unit_of_measurement")]
    public string UnitOfMeasurement { get; set; } = "A";

    [JsonPropertyName("value_template")] 
    public string ValueTemplate { get; set; } = "{{ value_json.batteryDischargeCurrent }}";

    [JsonPropertyName("unique_id")] 
    public string UniqueId { get; set; } = "batterydischargecurrent";

    [JsonPropertyName("device")] 
    public Device Device { get; init; } = new();
}