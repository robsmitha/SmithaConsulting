using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Services
{
    public class WebApiService
    {
        private string Endpoint { get; set; }
        private string APIKey { get; set; }
        public WebApiService(string endpoint, string apiKey = null)
        {
            Endpoint = endpoint;
            APIKey = apiKey;
        }
        public async Task<T> GetAsync<T>(string function)
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", APIKey);
                client.DefaultRequestHeaders.Add("X-API-Key", APIKey);
                try
                {
                    var requestUri = FormatRequestUri(function);
                    var response = await client.GetStringAsync(requestUri);
                    return JsonConvert.DeserializeObject<T>(response); ;
                }
                catch (HttpRequestException e)
                {
                    throw e;
                }
            }
        }

        public T Get<T>(string function)
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", APIKey);
                client.DefaultRequestHeaders.Add("X-API-Key", APIKey);
                try
                {
                    var requestUri = FormatRequestUri(function);
                    var response = client.GetStringAsync(requestUri).Result;
                    return JsonConvert.DeserializeObject<T>(response);
                }
                catch (HttpRequestException e)
                {
                    throw e;
                }
            }
        }

        public T Post<T>(string function, T data)
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", APIKey);
                client.DefaultRequestHeaders.Add("X-API-Key", APIKey);
                try
                {
                    var requestUri = FormatRequestUri(function);
                    var content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
                    var response = client.PostAsync(requestUri, content).Result;
                    return JsonConvert.DeserializeObject<T>(response.Content.ReadAsStringAsync().Result);
                }
                catch (HttpRequestException e)
                {
                    throw e;
                }
            }
        }
        public async Task<T> PostAsync<T>(string function, T data)
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", APIKey);
                client.DefaultRequestHeaders.Add("X-API-Key", APIKey);
                try
                {
                    var requestUri = FormatRequestUri(function);
                    var content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
                    var response = await client.PostAsync(requestUri, content);
                    return JsonConvert.DeserializeObject<T>(response.Content.ReadAsStringAsync().Result);
                }
                catch (HttpRequestException e)
                {
                    throw e;
                }
            }
        }
        public async Task<T> PutAsync<T>(string function, T data)
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", APIKey);
                client.DefaultRequestHeaders.Add("X-API-Key", APIKey);
                try
                {
                    var requestUri = FormatRequestUri(function);
                    var content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
                    var response = await client.PutAsync(requestUri, content);
                    return JsonConvert.DeserializeObject<T>(response.Content.ReadAsStringAsync().Result);
                }
                catch (HttpRequestException e)
                {
                    throw e;
                }
            }
        }
        public T Put<T>(string function, T data)
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", APIKey);
                client.DefaultRequestHeaders.Add("X-API-Key", APIKey);
                try
                {
                    var requestUri = FormatRequestUri(function);
                    var content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
                    var response = client.PutAsync(requestUri, content).Result;
                    return JsonConvert.DeserializeObject<T>(response.Content.ReadAsStringAsync().Result);
                }
                catch (HttpRequestException e)
                {
                    throw e;
                }
            }
        }
        public void Delete(string function)
        {
            HttpResponseMessage response;
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", APIKey);
                client.DefaultRequestHeaders.Add("X-API-Key", APIKey);
                try
                {
                    var requestUri = FormatRequestUri(function);
                    response = client.DeleteAsync(requestUri).Result;
                }
                catch (HttpRequestException e)
                {
                    throw e;
                }
            }
        }

        #region Helper Methods
        private string FormatRequestUri(string function)
        {
            var requestUri = Endpoint;
            requestUri += !function.StartsWith("/") ? $"/{function}" : function;
            return requestUri;
        }
        #endregion
    }
}
