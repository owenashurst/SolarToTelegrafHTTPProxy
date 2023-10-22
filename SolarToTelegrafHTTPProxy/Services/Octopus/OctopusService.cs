using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SolarToTelegrafHTTPProxy.Config;

namespace SolarToTelegrafHTTPProxy.Services.Octopus;

public class OctopusService : IOctopusService
{
    private readonly ILogger<OctopusService> _logger;
    private readonly IOctopusClient _octopusClient;
    private readonly OctopusSettings _octopusSettings;

    public OctopusService(ILogger<OctopusService> logger, IOctopusClient octopusClient, IOptions<OctopusSettings> octopusSettings)
    {
        _logger = logger;
        _octopusClient = octopusClient;
        
        ArgumentNullException.ThrowIfNull(octopusSettings.Value);
        _octopusSettings = octopusSettings.Value;
    }

    public async Task<double?> GetCurrentAgileRateAsync()
    {
        try
        {
            var agileRates = await _octopusClient.GetCurrentAgileRatesAsync(_octopusSettings.TariffCode, _octopusSettings.RegionalCode);

            var currentRate = agileRates.Results.OrderBy(x => x.ValidFrom).First().ValueIncVat;

            _logger.LogInformation("Current Agile rate: {CurrentAgileRate}", currentRate);
            
            return currentRate;
        }
        catch (HttpRequestException ex)
        {
            _logger.LogError(ex, "Unable to retrieve current Agile rate");
            return null;
        }
    }
}