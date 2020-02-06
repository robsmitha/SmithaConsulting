using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Utilities
{
    public static class DateTimeExtensions
    {
        public static DateTime Epoch = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
        public static DateTime ToDateTime(this decimal time)
        {
            return Epoch.AddSeconds(Convert.ToDouble(time)).ToLocalTime();
        }
        public static DateTime ToDateTime(this int time)
        {
            return Epoch.AddSeconds(Convert.ToDouble(time)).ToLocalTime();
        }
    }
}
