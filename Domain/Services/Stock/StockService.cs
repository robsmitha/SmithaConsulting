using System.Net.Http;
using System.Threading.Tasks;

namespace Domain.Services.Stock
{
    public class StockService
    {
        public string ApiKey { get; set; }
        public string Endpoint { get; set; }
        public StockService(string apiKey, string endpoint)
        {
            ApiKey = apiKey;
            Endpoint = endpoint;
        }
        public async Task<dynamic> GetAsync(string symbol, string function)
        {
            // Variable to hold result
            var response = string.Empty;
            var success = true;

            var missingConfigurations = string.IsNullOrWhiteSpace(Endpoint)
                || string.IsNullOrWhiteSpace(ApiKey);
            if (!missingConfigurations)
            {
                var url = $"{Endpoint}function={function}&symbol={symbol}&interval=5min&apikey={ApiKey}";

                // Create a New HttpClient object and dispose it when done, so the app doesn't leak resources
                using (HttpClient client = new HttpClient())
                {
                    // Call asynchronous network methods in a try/catch block to handle exceptions
                    try
                    {
                        response = await client.GetStringAsync(url);
                    }
                    catch (HttpRequestException e)
                    {
                        response = $"Error: {e.Message}";
                        success = false;
                    }
                }
            }
            else
            {
                response = $"Missing Configurations. Please review setup documentation on the about page.";
                success = false;
            }
            return new { response, success };
        }
    }
}
