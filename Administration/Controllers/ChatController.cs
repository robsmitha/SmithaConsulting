using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Architecture.Data;
using Administration.Models;
using Architecture.Constants;
using Architecture.Services.LanguageUnderstanding;
using Architecture.Services.GoogleGeocode;
using Architecture.Services.Weather;
using Architecture.Services.Stock;

namespace Administration.Controllers
{
    public class ChatController : BaseController
    {
        private readonly DbArchitecture _context;

        public ChatController(DbArchitecture context) : base(context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            //for jquery ui autocomplete script include
            ViewBag.CDN = !string.IsNullOrWhiteSpace(CDNLocation) && !string.IsNullOrWhiteSpace(BucketName)
                ? $"{CDNLocation}{BucketName}"
                : string.Empty;
            return View();
        }
        public IActionResult List()
        {
            var model = new ChatListViewModel();
            if (!string.IsNullOrEmpty(ConversationStirng))
            {
                model.Items = JsonConvert.DeserializeObject<List<ChatViewModel>>(ConversationStirng);
            }
            return PartialView(model);
        }
        public async Task<JsonResult> Speak(string query)
        {
            var languageUnderstanding = new LanguageUnderstandingService(appKey: LanguageUnderstandingAppKey, apiKey: LanguageUnderstandingAppKey, endpoint: LanguageUnderstandingEndpoint);
            var result = await languageUnderstanding.GetAsync(query);
            var chat = new ChatViewModel
            {
                query = query,
                timeStamp = DateTime.Now
            };

            if (result.success)
            {
                try
                {
                    //Deserialize response body into ChatViewModel object
                    chat = JsonConvert.DeserializeObject<ChatViewModel>(result.response);
                    if (chat != null)
                    {
                        var message = string.Empty;
                        //Determine what the query was intenting to do
                        switch (chat.topScoringIntent.intent)
                        {
                            case LanguageUnderstandingConstants.INTENT_GREETING:
                                //Check degree of certainty that the intent is a correct assumption
                                if (chat.topScoringIntent.score > .9700001)
                                {
                                    message = Greeting();
                                }
                                else
                                {
                                    message = "I didn't quite catch that.";
                                }
                                break;
                            case LanguageUnderstandingConstants.INTENT_CHECK_STOCK:
                                if (chat.topScoringIntent.score > .5)
                                {
                                    message = await Stock(chat.entities);
                                }
                                else
                                {
                                    message = "I didn't quite catch that.";
                                }
                                break;
                            case LanguageUnderstandingConstants.INTENT_CHECK_WEATHER:
                                if (chat.topScoringIntent.score > .5)
                                {
                                    message = await Weather(chat.entities);
                                }
                                else
                                {
                                    message = "I didn't quite catch that.";
                                }
                                break;
                            case LanguageUnderstandingConstants.INTENT_NONE:
                            default:
                                message = "Pump the brakes, I didn't understand any of that.";
                                break;
                        }
                        chat.reply = new Reply(message);

                    }
                }
                catch (Exception e)
                {
                    //An error occurred, output error message in conversation flow
                    chat.reply.message = $"Error: {e.Message} ";
                }
            }
            else
            {
                //The request was not successful, output response in conversation flow
                chat.reply.message = result.response;
            }

            #region Holding conversation in session
            //variable to hold conversation
            List<ChatViewModel> conversationList;
            //try to get Conversation session key   
            var conversationString = HttpContext.Session.GetString("Conversation");
            if (!string.IsNullOrEmpty(conversationString))
            {
                //Deserialize string into conversation list
                conversationList = JsonConvert.DeserializeObject<List<ChatViewModel>>(conversationString);
                //Add to conversation list
                conversationList.Add(chat);
            }
            else
            {
                //Initialize new list with single item
                conversationList = new List<ChatViewModel>
                {
                    chat
                };
            }
            //Set Conversation session key with serialized list
            HttpContext.Session.SetString("Conversation", JsonConvert.SerializeObject(conversationList));
            #endregion

            return Json(chat);
        }

        public JsonResult Source(string value)
        {
            var weatherIntents = new List<string>
            {
                "What is the weather in Florida",
                "What is the weather in Paris",
                "What is the weather in New York",
                "What is the weather in Las Vegas",
                "What is the weather in California"
            };

            var stockIntents = new List<string>
            {
                "What is MSFT trading for",
                "What is MSFT",
                "What is the price of MSFT"
            };

            var list = weatherIntents.Where(x => x.ToLower().Contains(value)).ToList();
            list.AddRange(stockIntents.Where(x => x.ToLower().Contains(value)));

            return Json(list);
        }

        private string Greeting()
        {
            var message = "Hey!";
            return message;
        }
        private async Task<string> Stock(List<Entity> entities)
        {
            var symbol = string.Empty;
            foreach (var e in entities)
            {
                switch (e.type)
                {
                    case LanguageUnderstandingConstants.ENTITY_STOCK_SYMBOL:
                        symbol = e.entity;
                        break;
                    default:
                        break;
                }
            }
            string message;
            if (!string.IsNullOrWhiteSpace(symbol))
            {
                var stockService = new StockService(AplhaAdvantageApiKey, AplhaAdvantageApiEndPoint);
                var stockPayload = await stockService.GetAsync(symbol, StockConstants.TIME_SERIES_INTRADAY);
                if (stockPayload.success)
                {
                    var stock = new Stock(stockPayload.response);
                    message = $"I checked the stock {stock.Symbol}. Open: {stock.Open}. High: {stock.High}. Low: {stock.Low}. Close: {stock.Close}. Volume: {stock.Volume}";
                }
                else
                {
                    message = stockPayload.response;
                }
            }
            else
            {
                message = "I did not understand that stock symbol.";
            }
            return message;
        }

        private async Task<string> Weather(List<Entity> entities)
        {
            var city = string.Empty;
            var street = string.Empty;
            var state = string.Empty;
            var country = string.Empty;
            foreach (var e in entities)
            {
                switch (e.type)
                {
                    case LanguageUnderstandingConstants.ENTITY_CITY:
                        city = e.entity;
                        break;
                    case LanguageUnderstandingConstants.ENTITY_COUNTRY:
                        country = e.entity;
                        break;
                    case LanguageUnderstandingConstants.ENTITY_STATE:
                        state = e.entity;
                        break;
                    case LanguageUnderstandingConstants.ENTITY_POI:
                        street = e.entity;
                        break;
                }
            }
            var address = $"{street} {city} {state} {country}".Trim();
            string message;
            if (!string.IsNullOrWhiteSpace(address))
            {
                var googleGeocode = new GoogleGeocodeService(apiKey: GoogleApiKey, endpoint: GoogleGeocodeEndpoint);
                var locationPayload = await googleGeocode.GetAsync(address);
                if (locationPayload.success)
                {
                    var location = new Location(locationPayload.response);
                    var weatherService = new WeatherService(WeatherSecretKey, WeatherApiEndpoint);
                    var weatherPayload = await weatherService.GetAsync(latitude: location.Latitude, longitude: location.Longitude);
                    if (weatherPayload.success)
                    {
                        var weather = new Weather(weatherPayload.response);
                        message = $"I checked the weather in {location.FormattedAddress}. Here is the summary: {weather.Summary}. Current temperature is {weather.Temperature}";
                    }
                    else
                    {
                        message = locationPayload.response;
                    }
                }
                else
                {
                    message = locationPayload.response;
                }
            }
            else
            {
                message = "I did not understand that location.";
            }
            return message;
        }
    }
}