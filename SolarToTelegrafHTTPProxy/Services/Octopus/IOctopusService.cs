using System.Threading.Tasks;

namespace SolarToTelegrafHTTPProxy.Services.Octopus;

public interface IOctopusService
{
    /// <summary>
    /// Retrieves the current Agile rate
    /// </summary>
    /// <returns><see cref="double"/>The Agile rate in pence or null if there was an error.</returns>
    Task<double?> GetCurrentAgileRateAsync();
}