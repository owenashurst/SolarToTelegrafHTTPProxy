using System.Threading.Tasks;
using SolarToTelegrafHTTPProxy.Services.Octopus.Models;

namespace SolarToTelegrafHTTPProxy.Services.Octopus;

public interface IOctopusClient
{
    /// <summary>
    /// Returns a list of the current Agile rates
    /// </summary>
    /// <param name="regionalCode">The Regional Code. Find more here: </param>
    /// <returns><see cref="TariffResponse"/>The response</returns>
    Task<TariffResponse> GetCurrentAgileRatesAsync(string tariffCode, string regionalCode);
}