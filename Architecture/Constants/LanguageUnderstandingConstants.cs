using System;
using System.Collections.Generic;
using System.Text;

namespace Architecture.Constants
{
    public class LanguageUnderstandingConstants
    {
        public const string INTENT_NONE = "None";
        public const string INTENT_GREETING = "Greeting";
        public const string INTENT_CHECK_WEATHER = "CheckWeather";
        public const string INTENT_CHECK_STOCK = "CheckStock";
        public const string ENTITY_STOCK_SYMBOL = "StockSymbol";
        public const string ENTITY_CITY = "builtin.geographyV2.city";
        public const string ENTITY_COUNTRY = "builtin.geographyV2.countryRegion";
        public const string ENTITY_STATE = "builtin.geographyV2.state";
        public const string ENTITY_POI = "builtin.geographyV2.poi";
    }
}
