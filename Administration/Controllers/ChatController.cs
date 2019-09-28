using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Architecture.Data;
using Administration.Models;
using Administration.Services;

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
            if (HasSessionConversation)
            {
                model.Items = JsonConvert.DeserializeObject<List<ChatViewModel>>(ConversationStirng);
            }
            return PartialView(model);
        }
        public async Task<JsonResult> Speak(string query)
        {
            var result = await LanguageUnderstandingService.GetAsync(query);
            ChatViewModel luis = new ChatViewModel
            {
                query = query,
                timeStamp = DateTime.Now
            };

            if (result.success)
            {
                try
                {
                    //Deserialize response body into ChatViewModel object
                    luis = JsonConvert.DeserializeObject<ChatViewModel>(result.response);
                    if (luis != null)
                    {
                        await luis.Reply();
                    }
                }
                catch (Exception e)
                {
                    //An error occurred, output error message in conversation flow
                    luis.reply.message = $"Error: {e.Message} ";
                }
            }
            else
            {
                //The request was not successful, output response in conversation flow
                luis.reply.message = result.response;
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
                conversationList.Add(luis);
            }
            else
            {
                //Initialize new list with single item
                conversationList = new List<ChatViewModel>
                {
                    luis
                };
            }
            //Set Conversation session key with serialized list
            HttpContext.Session.SetString("Conversation", JsonConvert.SerializeObject(conversationList));
            #endregion

            return Json(luis);
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
    }
}