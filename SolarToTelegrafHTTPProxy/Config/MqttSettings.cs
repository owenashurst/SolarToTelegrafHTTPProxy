namespace SolarToTelegrafHTTPProxy.Config;

public class MqttSettings
{
    public string Address { get; set; }

    public int Port { get; set; } = 1883;

    public string ClientId { get; set; } = "iconica-solar";

    public string Topic { get; set; } = "iconica-solar";
}