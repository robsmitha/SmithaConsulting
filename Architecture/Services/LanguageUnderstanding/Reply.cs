using System;
using System.Collections.Generic;
using System.Text;

namespace Architecture.Services.LanguageUnderstanding
{
    public class Reply
    {
        public string message { get; set; }
        public Reply(string msg)
        {
            message = msg;
        }
    }
}
