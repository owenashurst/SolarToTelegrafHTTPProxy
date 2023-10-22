namespace SolarToTelegrafHTTPProxy.Config;

public class OctopusSettings
{
    public string TariffCode { get; set; }

    /// <summary>
    /// See: https://energy-stats.uk/dno-region-codes-explained/
    /// </summary>
    public string RegionalCode { get; set; }
}