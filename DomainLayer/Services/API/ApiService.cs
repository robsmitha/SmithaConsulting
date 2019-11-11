using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Services.API
{
    public class APIService
    {
        private string Endpoint { get; set; }
        private string ApiKey { get; set; }
        public APIService(string endpoint, string apiKey)
        {
            Endpoint = endpoint;
            ApiKey = apiKey;
        }
        public async Task<string> GetAsync(string function)
        {
            var response = string.Empty;   
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ApiKey);
                try
                {
                    response = await client.GetStringAsync($"{Endpoint}{function}");
                }
                catch (HttpRequestException)
                {

                }
            }
            return response;
        }

        public string Get(string function)
        {
            var response = string.Empty;
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ApiKey);
                try
                {
                    response = client.GetStringAsync($"{Endpoint}{function}").Result;
                }
                catch (HttpRequestException)
                {

                }
            }
            return response;
        }
        
        public string Post(string function, string data)
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ApiKey);
                try
                {
                    var content = new StringContent(data, Encoding.UTF8, "application/json");
                    var response = client.PostAsync($"{Endpoint}{function}", content).Result;
                    return response.Content.ReadAsStringAsync().Result;
                }
                catch (HttpRequestException)
                {

                }
            }
            return null;
        }
        public async Task<HttpResponseMessage> PostAsync(string function, string data)
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ApiKey);
                try
                {
                    return await client.PostAsync($"{Endpoint}{function}", new StringContent(data, Encoding.UTF8, "application/json"));
                }
                catch (HttpRequestException)
                {

                }
            }
            return null;
        }
        public async Task<HttpResponseMessage> PutAsync(string function, string data)
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ApiKey);
                try
                {
                    return await client.PutAsync($"{Endpoint}{function}", new StringContent(data, Encoding.UTF8, "application/json"));
                }
                catch (HttpRequestException)
                {

                }
            }
            return null;
        }
        public string Put(string function, string data)
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ApiKey);
                try
                {
                    var response = client.PutAsync($"{Endpoint}{function}", new StringContent(data, Encoding.UTF8, "application/json")).Result;
                    return response.Content.ReadAsStringAsync().Result;
                }
                catch (HttpRequestException)
                {

                }
            }
            return null;
        }
        public void Delete(string function)
        {
            HttpResponseMessage response;
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ApiKey);
                try
                {
                    response = client.DeleteAsync(($"{Endpoint}{function}")).Result;
                }
                catch (HttpRequestException)
                {

                }
            }
        }
    }
}
