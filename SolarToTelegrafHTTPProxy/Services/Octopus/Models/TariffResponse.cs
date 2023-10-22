using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace SolarToTelegrafHTTPProxy.Services.Octopus.Models;

public class TariffResponse
{
    [JsonPropertyName("count")]
    public int Count { get; set; }
    
    [JsonPropertyName("next")]
    public string Next { get; set; }
    
    [JsonPropertyName("previous")]
    public string Previous { get; set; }
    
    [JsonPropertyName("results")]
    public IEnumerable<Tariff> Results { get; set; }
}