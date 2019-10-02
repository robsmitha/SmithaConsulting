using Architecture.Services.LanguageUnderstanding;
using System;
using System.Collections.Generic;

namespace Administration.Models
{
    public class ChatViewModel
    {
        public string query { get; set; } = string.Empty;
        public TopScoringIntent topScoringIntent { get; set; } = new TopScoringIntent();
        public List<Entity> entities { get; set; } = new List<Entity>();
        public Reply reply { get; set; } = new Reply(string.Empty);
        public DateTime timeStamp { get; set; } = DateTime.Now;
        
    }
}
