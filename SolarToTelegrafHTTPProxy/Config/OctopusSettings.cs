namespace SolarToTelegrafHTTPProxy.Config;

public class OctopusSettings
{
    public string BaseApiUrl { get; set; } = "https://api.octopus.energy";
    
    /// <summary>
    /// Enable or disable looking up the current Agile Rate in your region.<br /><br />
    /// Useful for calculating cost savings within Grafana.
    /// </summary>
    public bool EnableAgileRateLookup { get; set; } = false;
    
    /// <summary>
    /// See: https://energy-stats.uk/dno-region-codes-explained/
    /// </summary>
    public string TariffCode { get; set; }

    /// <summary>
    /// See: https://energy-stats.uk/dno-region-codes-explained/
    /// </summary>
    public string RegionalCode { get; set; }
}