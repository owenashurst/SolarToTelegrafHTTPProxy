using System.Text.Json.Serialization;

namespace SolarToTelegrafHTTPProxy.Services.Mqtt.Models.Sensors;

public class AcOutputPower : IConfig
{
    [JsonIgnore]
    public string ConfigTopicName { get; set; } = "homeassistant/sensor/iconica_solar_ac_output_power/config";

    [JsonPropertyName("device_class")] 
    public string DeviceClass { get; set; } = "power";

    [JsonPropertyName("name")] 
    public string Name { get; set; } = "AC Output Power";

    [JsonPropertyName("state_topic")] 
    public string StateTopic { get; set; } = Config.StateTopicName;

    [JsonPropertyName("state_class")] 
    public string StateClass { get; set; } = "measurement";

    [JsonPropertyName("unit_of_measurement")]
    public string UnitOfMeasurement { get; set; } = "W";

    [JsonPropertyName("value_template")] 
    public string ValueTemplate { get; set; } = "{{ value_json.acOutputPower }}";

    [JsonPropertyName("unique_id")] 
    public string UniqueId { get; set; } = "acoutputpower";

    [JsonPropertyName("device")] 
    public Device Device { get; init; } = new();
}