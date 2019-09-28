using Administration.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Administration.Services
{
    public class WeatherService
    {
        private static readonly string WeatherApiEndpoint = ConfigurationManager.AppSetting["Configurations:DarkSkyEndpoint"];
        private static readonly string WeatherSecretKey = ConfigurationManager.AppSetting["Configurations:DarkSkySecretKey"];
        public static async Task<dynamic> GetAsync(double latitude, double longitude)
        {
            // Variable to hold result
            var response = string.Empty;
            var success = true;

            var missingConfigurations = string.IsNullOrWhiteSpace(WeatherApiEndpoint)
                || string.IsNullOrWhiteSpace(WeatherSecretKey);
            if (!missingConfigurations)
            {
                var url = $"{WeatherApiEndpoint}{WeatherSecretKey}/{latitude},{longitude}";
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
