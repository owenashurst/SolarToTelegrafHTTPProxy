using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using SolarToTelegrafHTTPProxy.Config;

namespace SolarToTelegrafHTTPProxy.Features.Telegraf
{
    public class TelegrafHttpService : ITelegrafHttpService
    {
        private readonly IOptions<TelegrafSettings> _telegrafSettings;
        private readonly IHttpClientFactory _httpClientFactory;

        public const string HttpClientName = "TelegrafHttpClient";

        public TelegrafHttpService(IOptions<TelegrafSettings> telegrafSettings, IHttpClientFactory httpClientFactory)
        {
            _telegrafSettings = telegrafSettings;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<bool> SubmitToTelegraf(Details.Query query)
        {
            using var httpClient = _httpClientFactory.CreateClient(HttpClientName);
            
            var stringContent = new StringContent(JsonSerializer.Serialize(query), Encoding.UTF8, "application/json");

            var response = await httpClient.PostAsync(_telegrafSettings.Value.Endpoint, stringContent);
            return response.IsSuccessStatusCode;
        }
    }
}
