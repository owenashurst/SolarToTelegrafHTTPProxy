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
        private readonly HttpClient _httpClient;

        public TelegrafHttpService(IOptions<TelegrafSettings> telegrafSettings, IHttpClientFactory httpClientFactory)
        {
            _telegrafSettings = telegrafSettings;

            _httpClient = httpClientFactory.CreateClient();
            _httpClient.BaseAddress = new Uri(telegrafSettings.Value.HttpListenerApiURL);
        }

        public async Task<bool> SubmitToTelegraf(Details.Query query)
        {
            var stringContent = new StringContent(JsonSerializer.Serialize(query), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(_telegrafSettings.Value.Endpoint, stringContent);
            if (response.IsSuccessStatusCode)
            {
                return true;
            } 
            else
            {
                return false;
            }
        }
    }
}
