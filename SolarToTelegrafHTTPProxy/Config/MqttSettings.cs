namespace SolarToTelegrafHTTPProxy.Config;

public class MqttSettings
{
    public string Address { get; set; }

    public int Port { get; set; } = 1883;

    public string Username { get; set; } = string.Empty;

    public string Password { get; set; } = string.Empty;

    public string ClientId { get; set; } = "iconica_solar";

    public string Topic { get; set; } = "homeassistant/sensor/iconica_solar/config";
}