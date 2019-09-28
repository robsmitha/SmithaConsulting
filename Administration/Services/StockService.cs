using Administration.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Administration.Services
{
    public class StockService
    {
        private static readonly string AplhaAdvantageApiKey = ConfigurationManager.AppSetting["Configurations:AplhaAdvantageApiKey"];
        private static readonly string AplhaAdvantageApiEndPoint = ConfigurationManager.AppSetting["Configurations:AplhaAdvantageApiEndPoint"];
        private static readonly string TIME_SERIES_INTRADAY = ConfigurationManager.AppSetting["Configurations:AplhaAdvantageTIME_SERIES_INTRADAY"];
        public static async Task<dynamic> GetAsync(string symbol)
        {
            // Variable to hold result
            var response = string.Empty;
            var success = true;

            var missingConfigurations = string.IsNullOrWhiteSpace(AplhaAdvantageApiEndPoint)
                || string.IsNullOrWhiteSpace(AplhaAdvantageApiKey)
                || string.IsNullOrWhiteSpace(TIME_SERIES_INTRADAY);
            if (!missingConfigurations)
            {
                var url = $"{AplhaAdvantageApiEndPoint}function={TIME_SERIES_INTRADAY}&symbol={symbol}&interval=5min&apikey={AplhaAdvantageApiKey}";

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
