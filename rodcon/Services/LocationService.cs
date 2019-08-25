using rodcon.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace rodcon.Services
{
    public class LocationService
    {
        private static readonly string GeocodeApiEndpoint = ConfigurationManager.AppSetting["Configurations:GoogleGeocodeEndpoint"];
        private static readonly string ApiKey = ConfigurationManager.AppSetting["Configurations:GoogleApiKey"];
        public static async Task<dynamic> GetAsync(string address)
        {
            // Variable to hold result
            var response = string.Empty;
            var success = true;

            var missingConfigurations = string.IsNullOrWhiteSpace(GeocodeApiEndpoint)
                || string.IsNullOrWhiteSpace(ApiKey);
            if (!missingConfigurations)
            {
                var url = $"{GeocodeApiEndpoint}{address}&key={ApiKey}";
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
