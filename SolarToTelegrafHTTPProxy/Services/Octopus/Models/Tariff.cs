using System;
using System.Text.Json.Serialization;

namespace SolarToTelegrafHTTPProxy.Services.Octopus.Models;

public class Tariff
{
    [JsonPropertyName("value_exc_vat")]
    public double ValueExcVat { get; set; }
    
    [JsonPropertyName("value_inc_vat")]
    public double ValueIncVat { get; set; }
    
    [JsonPropertyName("valid_from")]
    public DateTime ValidFrom { get; set; }
    
    [JsonPropertyName("valid_to")]
    public DateTime ValidTo { get; set; }
}