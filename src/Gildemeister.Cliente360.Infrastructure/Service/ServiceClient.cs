using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Gildemeister.Cliente360.Infrastructure
{
    public class ConfigureSync
    {
        public  string Url { get; set; }
    }

    public class ServiceClient : IServiceClient
    {
        private readonly HttpClient _httpClient;

        private readonly IOptions<ConfigureSync> settings;

        public ServiceClient(IOptions<ConfigureSync> settings)
        {
            this.settings = settings;
            _httpClient = new HttpClient { BaseAddress = new Uri(settings.Value.Url) };
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<T> GetAsync<T>(string endpoint, string args = null)
        {
            var response = await _httpClient.GetAsync($"{endpoint}?{args}");
            if (!response.IsSuccessStatusCode)
                return default(T);

            var result = await response.Content.ReadAsStringAsync();


            return JsonConvert.DeserializeObject<T>(result);
        }

        public async Task<HttpResponseMessage> GetAsync(string endpoint, string args = null)
        {
            HttpResponseMessage httpResponse = new HttpResponseMessage();
            httpResponse = await _httpClient.GetAsync($"{endpoint}?{args}");
            return httpResponse;
        }

        public async Task PostAsync(string endpoint, object data, string args = null)
        {
            var payload = GetPayload(data);
            await _httpClient.PostAsync($"{endpoint}?{args}", payload);
        }

        public async Task PutAsync(string endpoint, object data, string args = null)
        {
            var payload = GetPayload(data);
            await _httpClient.PutAsync($"{endpoint}?{args}", payload);
        }


        public async Task<HttpResponseMessage> PostAsync(string endpoint, object content)
        {
            HttpResponseMessage httpResponse = new HttpResponseMessage();
            httpResponse =  await _httpClient.PostAsJsonAsync(endpoint, content);
            return httpResponse;
        }

        private static StringContent GetPayload(object data)
        {
            var json = JsonConvert.SerializeObject(data);

            return new StringContent(json, Encoding.UTF8, "application/json");
        }

      
    }
}
