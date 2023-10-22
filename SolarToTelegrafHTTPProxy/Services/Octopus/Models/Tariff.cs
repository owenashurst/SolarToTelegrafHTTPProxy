using System;

namespace SolarToTelegrafHTTPProxy.Services.Octopus.Models;

public class Tariff
{
    public double ValueExcVat { get; set; }
    public double ValueIncVat { get; set; }
    public DateTime ValidFrom { get; set; }
    public DateTime ValidTo { get; set; }
    public string PaymentMethod { get; set; }
}