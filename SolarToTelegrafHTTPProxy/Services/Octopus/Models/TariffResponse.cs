using System.Collections.Generic;

namespace SolarToTelegrafHTTPProxy.Services.Octopus.Models;

public class TariffResponse
{
    public int Count { get; set; }
    public string Next { get; set; }
    public string Previous { get; set; }
    public IEnumerable<Tariff> Results { get; set; }
}