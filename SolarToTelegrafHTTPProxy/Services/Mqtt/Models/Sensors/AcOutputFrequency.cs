using System.Text.Json.Serialization;

namespace SolarToTelegrafHTTPProxy.Services.Mqtt.Models.Sensors;

public class AcOutputFrequency : IConfig
{
    [JsonIgnore]
    public string ConfigTopicName { get; set; } = "homeassistant/sensor/iconica_solar_ac_output_frequency/config";

    [JsonPropertyName("device_class")] 
    public string DeviceClass { get; set; } = "frequency";

    [JsonPropertyName("name")] 
    public string Name { get; set; } = "AC Output Frequency";

    [JsonPropertyName("state_topic")] 
    public string StateTopic { get; set; } = Config.StateTopicName;

    [JsonPropertyName("unit_of_measurement")]
    public string UnitOfMeasurement { get; set; } = "Hz";

    [JsonPropertyName("value_template")] 
    public string ValueTemplate { get; set; } = "{{ value_json.acOutputFrequency }}";

    [JsonPropertyName("unique_id")] 
    public string UniqueId { get; set; } = "acoutputfrequency";

    [JsonPropertyName("device")] 
    public Device Device { get; init; } = new();
}