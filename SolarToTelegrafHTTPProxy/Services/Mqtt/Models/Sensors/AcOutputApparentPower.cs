using System.Text.Json.Serialization;

namespace SolarToTelegrafHTTPProxy.Services.Mqtt.Models.Sensors;

public class AcOutputApparentPower : IConfig
{
    [JsonIgnore]
    public string ConfigTopicName { get; set; } = "homeassistant/sensor/iconica_solar_ac_output_apparent_power/config";

    [JsonPropertyName("device_class")] 
    public string DeviceClass { get; set; } = "power";

    [JsonPropertyName("name")] 
    public string Name { get; set; } = "AC Output Apparent Power";

    [JsonPropertyName("state_topic")] 
    public string StateTopic { get; set; } = Config.StateTopicName;
    
    [JsonPropertyName("state_class")]
    public string StateClass { get; set; } = "measurement";

    [JsonPropertyName("unit_of_measurement")]
    public string UnitOfMeasurement { get; set; } = "W";

    [JsonPropertyName("value_template")] 
    public string ValueTemplate { get; set; } = "{{ value_json.acOutputApparentPower }}";

    [JsonPropertyName("unique_id")] 
    public string UniqueId { get; set; } = "acoutputapparentpower";

    [JsonPropertyName("device")] 
    public Device Device { get; init; } = new();
}