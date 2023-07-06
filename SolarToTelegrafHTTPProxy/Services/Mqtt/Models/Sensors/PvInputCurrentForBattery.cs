using System.Text.Json.Serialization;

namespace SolarToTelegrafHTTPProxy.Services.Mqtt.Models.Sensors;

public class PvInputCurrentForBattery : IConfig
{
    [JsonIgnore]
    public string ConfigTopicName { get; set; } = "homeassistant/sensor/iconica_solar_pv_input_current_for_batteries/config";
    
    [JsonPropertyName("device_class")] 
    public string DeviceClass { get; set; } = "current";

    [JsonPropertyName("name")] 
    public string Name { get; set; } = "PV Input Current For Battery";

    [JsonPropertyName("state_topic")] 
    public string StateTopic { get; set; } = Config.StateTopicName;

    [JsonPropertyName("unit_of_measurement")]
    public string UnitOfMeasurement { get; set; } = "A";

    [JsonPropertyName("value_template")] 
    public string ValueTemplate { get; set; } = "{{ value_json.pvInputCurrentForBattery }}";

    [JsonPropertyName("unique_id")] 
    public string UniqueId { get; set; } = "pvinputcurrentforbatteries";

    [JsonPropertyName("device")]
    public Device Device { get; init; } = new()
    {
        Name = "IconicaSolar",
        Identifiers = new[] { "pvInputCurrent" }
    };
}