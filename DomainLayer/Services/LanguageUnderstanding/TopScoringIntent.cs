using System;
using System.Collections.Generic;
using System.Text;

namespace DomainLayer.Services.LanguageUnderstanding
{
    public class TopScoringIntent
    {
        public string intent { get; set; }
        public double score { get; set; }
    }
}
