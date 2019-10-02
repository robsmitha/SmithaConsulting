using System.Net.Http;
using System.Threading.Tasks;

namespace Architecture.Services.LanguageUnderstanding
{
    public class LanguageUnderstandingService
    {        
        public string AppKey { get; set; }
        public string ApiKey { get; set; }
        public string Endpoint { get; set; }
        public LanguageUnderstandingService(string appKey, string apiKey, string endpoint)
        {
            AppKey = appKey;
            ApiKey = apiKey;
            Endpoint = endpoint;
        }

        public async Task<dynamic> GetAsync(string query)
        {
            // Variable to hold result
            var response = string.Empty;
            var success = true;

            var missingConfigurations = string.IsNullOrWhiteSpace(Endpoint)
                || string.IsNullOrWhiteSpace(AppKey)
                || string.IsNullOrWhiteSpace(ApiKey);
            if (!missingConfigurations)
            {
                var url = $"{Endpoint}{AppKey}?timezoneOffset=-360&subscription-key={ApiKey}&q={query}";

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
