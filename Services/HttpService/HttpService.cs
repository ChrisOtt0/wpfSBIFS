using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using wpfSBIFS.DataTransferObjects;
using wpfSBIFS.Extensions;

namespace wpfSBIFS.Services.HttpService
{
    public class HttpService : IHttpService
    {
        HttpClient client = new HttpClient();
        string baseUrl = "https://localhost:7120/api/";

        public async Task<HttpResponseMessage> Get(string url)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, baseUrl + url);
            return await client.GetAsync(baseUrl + url);
        }

        public async Task<HttpResponseMessage> Post(string url, IJson data)
        {
            var request = new HttpRequestMessage()
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri(baseUrl + url)
            };
            request.Content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
            return await client.SendAsync(request);
        }

        public async Task<HttpResponseMessage> Put(string url, IJson data)
        {
            var request = new HttpRequestMessage()
            {
                Method = HttpMethod.Put,
                RequestUri = new Uri(baseUrl + url)
            };
            request.Content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
            return await client.SendAsync(request);
        }

        public async Task<HttpResponseMessage> Delete(string url, IJson data)
        {
            return await HttpClientExtension.DeleteAsJsonAsync(client, baseUrl + url, data);
        }

        public void AddAuthentication(string jwt)
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwt);
        }
    }
}
