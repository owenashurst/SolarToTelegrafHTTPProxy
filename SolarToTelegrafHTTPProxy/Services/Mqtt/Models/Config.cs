using System.Text.Json.Serialization;
using SolarToTelegrafHTTPProxy.Config;

namespace SolarToTelegrafHTTPProxy.Services.Mqtt.Models;

public class Config
{
    public const string StateTopicName = "homeassistant/sensor/iconica_solar/state";
    
    [JsonPropertyName("battery_voltage_t")]
    public string BatteryVoltage { get; set; } = MqttSettings.MessageTopic;
    [JsonPropertyName("battery_voltage_tpl")]
    public string BatteryVoltageTemplate { get; set; } = "{{ value_json.batteryVoltage }}";
    
    [JsonPropertyName("battery_charging_current_t")]
    public string BatteryChargingCurrent { get; set; } = MqttSettings.MessageTopic;
    [JsonPropertyName("battery_charging_current_tpl")]
    public string BatteryChargingCurrentTemplate { get; set; } = "{{ value_json.batteryChargingCurrent }}";
    
    [JsonPropertyName("battery_discharge_current_t")]
    public string BatteryDischargeCurrent { get; set; } = MqttSettings.MessageTopic;
    [JsonPropertyName("battery_discharge_current_tpl")]
    public string BatteryDischargeCurrentTemplate { get; set; } = "{{ value_json.batteryDischargeCurrent }}";
    
    [JsonPropertyName("battery_capacity_t")]
    public string BatteryCapacity { get; set; } = MqttSettings.MessageTopic;
    [JsonPropertyName("battery_capacity_tpl")]
    public string BatteryCapacityTemplate { get; set; } = "{{ value_json.batteryCapacity }}";
    
    [JsonPropertyName("ac_output_voltage_t")]
    public string ACOutputVoltage { get; set; } = MqttSettings.MessageTopic;
    [JsonPropertyName("ac_output_voltage_tpl")]
    public string ACOutputVoltageTemplate { get; set; } = "{{ value_json.acOutputVoltage }}";
    
    [JsonPropertyName("ac_output_frequency_t")]
    public string ACOutputFrequency { get; set; } = MqttSettings.MessageTopic;
    [JsonPropertyName("ac_output_frequency_tpl")]
    public string ACOutputFrequencyTemplate { get; set; } = "{{ value_json.acOutputFrequency }}";
    
    [JsonPropertyName("ac_output_power_t")]
    public string ACOutputPower { get; set; } = MqttSettings.MessageTopic;
    [JsonPropertyName("ac_output_power_tpl")]
    public string ACOutputPowerTemplate { get; set; } = "{{ value_json.acOutputPower }}";
    
    [JsonPropertyName("ac_output_apparent_power_t")]
    public string ACOutputApparentPower { get; set; } = MqttSettings.MessageTopic;
    [JsonPropertyName("ac_output_apparent_power_tpl")]
    public string ACOutputApparentPowerTemplate { get; set; } = "{{ value_json.acOutputApparentPower }}";
}