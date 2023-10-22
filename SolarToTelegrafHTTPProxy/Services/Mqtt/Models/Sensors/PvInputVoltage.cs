using System.Text.Json.Serialization;

namespace SolarToTelegrafHTTPProxy.Services.Mqtt.Models.Sensors;

public class PvInputVoltage : IConfig
{
    [JsonIgnore]
    public string ConfigTopicName { get; set; } = "homeassistant/sensor/iconica_solar_pvinput_voltage/config";

    [JsonPropertyName("device_class")] 
    public string DeviceClass { get; set; } = "voltage";

    [JsonPropertyName("name")] 
    public string Name { get; set; } = "PV Input Voltage";

    [JsonPropertyName("state_topic")] 
    public string StateTopic { get; set; } = Config.StateTopicName;
    
    [JsonPropertyName("state_class")]
    public string StateClass { get; set; } = "measurement";

    [JsonPropertyName("unit_of_measurement")]
    public string UnitOfMeasurement { get; set; } = "V";

    [JsonPropertyName("value_template")] 
    public string ValueTemplate { get; set; } = "{{ value_json.pvInputVoltage }}";

    [JsonPropertyName("unique_id")] 
    public string UniqueId { get; set; } = "pvinputvoltage";
    
    [JsonPropertyName("device")] 
    public Device Device { get; init; } = new();
}