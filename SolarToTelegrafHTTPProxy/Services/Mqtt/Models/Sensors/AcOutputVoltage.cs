using System.Text.Json.Serialization;

namespace SolarToTelegrafHTTPProxy.Services.Mqtt.Models.Sensors;

public class AcOutputVoltage : IConfig
{
    [JsonIgnore]
    public string ConfigTopicName { get; set; } = "homeassistant/sensor/iconica_solar_ac_output_voltage/config";

    [JsonPropertyName("device_class")] 
    public string DeviceClass { get; set; } = "voltage";

    [JsonPropertyName("name")] 
    public string Name { get; set; } = "AC Output Voltage";

    [JsonPropertyName("state_topic")] 
    public string StateTopic { get; set; } = Config.StateTopicName;

    [JsonPropertyName("unit_of_measurement")]
    public string UnitOfMeasurement { get; set; } = "V";

    [JsonPropertyName("value_template")] 
    public string ValueTemplate { get; set; } = "{{ value_json.acOutputVoltage }}";

    [JsonPropertyName("unique_id")] 
    public string UniqueId { get; set; } = "acoutputvoltage";

    [JsonPropertyName("device")] 
    public Device Device { get; init; } = new();
}