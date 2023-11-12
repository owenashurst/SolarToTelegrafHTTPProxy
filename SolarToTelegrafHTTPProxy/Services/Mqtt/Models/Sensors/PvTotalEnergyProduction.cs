using System.Text.Json.Serialization;

namespace SolarToTelegrafHTTPProxy.Services.Mqtt.Models.Sensors;

public class PvTotalEnergyProduction
{
    [JsonIgnore]
    public string ConfigTopicName { get; set; } = "homeassistant/sensor/iconica_solar_pv_total_energy_production/config";

    [JsonPropertyName("device_class")] 
    public string DeviceClass { get; set; } = "energy";

    [JsonPropertyName("name")] 
    public string Name { get; set; } = "PV Total Energy Production";

    [JsonPropertyName("state_topic")] 
    public string StateTopic { get; set; } = Config.StateTopicName;

    [JsonPropertyName("state_class")]
    public string StateClass { get; set; } = "total";

    [JsonPropertyName("unit_of_measurement")]
    public string UnitOfMeasurement { get; set; } = "kWh";

    [JsonPropertyName("value_template")] 
    public string ValueTemplate { get; set; } = "{{ value_json.pvTotalEnergyProduction }}";

    [JsonPropertyName("unique_id")] 
    public string UniqueId { get; set; } = "pvtotalenergyproduction";

    [JsonPropertyName("device")] 
    public Device Device { get; init; } = new();
}