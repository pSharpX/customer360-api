using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Gildemeister.Cliente360.Infrastructure
{
    public interface IServiceClient
    {
        Task<T> GetAsync<T>(string endpoint, string args = null);

        Task<HttpResponseMessage> GetAsync(string endpoint, string args = null);

        Task PostAsync(string endpoint, object data, string args = null);

        Task<HttpResponseMessage> PostAsync(string endpoint, object content);

        Task PutAsync(string endpoint, object data, string args = null);
    }
}
