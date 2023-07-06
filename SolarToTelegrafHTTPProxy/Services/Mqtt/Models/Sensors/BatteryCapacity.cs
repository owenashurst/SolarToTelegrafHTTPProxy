using System.Text.Json.Serialization;

namespace SolarToTelegrafHTTPProxy.Services.Mqtt.Models.Sensors;

public class BatteryCapacity : IConfig
{
    [JsonIgnore]
    public string ConfigTopicName { get; set; } = "homeassistant/sensor/iconica_solar_battery_capacity/config";

    [JsonPropertyName("device_class")] 
    public string DeviceClass { get; set; } = "none";

    [JsonPropertyName("name")] 
    public string Name { get; set; } = "Battery Capacity";

    [JsonPropertyName("state_topic")] 
    public string StateTopic { get; set; } = Config.StateTopicName;

    [JsonPropertyName("unit_of_measurement")]
    public string UnitOfMeasurement { get; set; } = "";

    [JsonPropertyName("value_template")] 
    public string ValueTemplate { get; set; } = "{{ value_json.batteryCapacity }}";

    [JsonPropertyName("unique_id")] 
    public string UniqueId { get; set; } = "batterycapacity";

    [JsonPropertyName("device")] 
    public Device Device { get; init; } = new();
}