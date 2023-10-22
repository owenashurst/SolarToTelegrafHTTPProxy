using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using SolarToTelegrafHTTPProxy.Services.Octopus.Models;

namespace SolarToTelegrafHTTPProxy.Services.Octopus;

public class OctopusClient : IOctopusClient
{
    private readonly IHttpClientFactory _httpClientFactory;
    
    public const string HttpClientName = "OctopusHttpClient";

    public OctopusClient(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<TariffResponse> GetCurrentAgileRatesAsync(string tariffCode, string regionalCode)
    {
        using var httpClient = _httpClientFactory.CreateClient(HttpClientName);

        var response = await httpClient.GetAsync(
            $"/v1/products/AGILE-18-02-21/electricity-tariffs/{tariffCode}-{regionalCode}/standard-unit-rates");

        response.EnsureSuccessStatusCode();

        var responseBody = await response.Content.ReadAsStringAsync();

        return JsonSerializer.Deserialize<TariffResponse>(responseBody);
    }
}