using System.Text.Json.Serialization;

namespace SolarToTelegrafHTTPProxy.Services.Mqtt.Models.Sensors;

public class BatteryChargingCurrent : IConfig
{
    [JsonIgnore]
    public string ConfigTopicName { get; set; } = "homeassistant/sensor/iconica_solar_battery_charging_current/config";

    [JsonPropertyName("device_class")] 
    public string DeviceClass { get; set; } = "current";

    [JsonPropertyName("name")] 
    public string Name { get; set; } = "Battery Charging Current";

    [JsonPropertyName("state_topic")] 
    public string StateTopic { get; set; } = Config.StateTopicName;
   
    [JsonPropertyName("state_class")] 
    public string StateClass { get; set; } = "measurement";

    [JsonPropertyName("unit_of_measurement")]
    public string UnitOfMeasurement { get; set; } = "A";

    [JsonPropertyName("value_template")] 
    public string ValueTemplate { get; set; } = "{{ value_json.batteryChargingCurrent }}";

    [JsonPropertyName("unique_id")] 
    public string UniqueId { get; set; } = "batterychargingcurrent";

    [JsonPropertyName("device")] 
    public Device Device { get; init; } = new();
}