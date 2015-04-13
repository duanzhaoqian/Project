using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mongo
{
    public static class Unity
    {
        // Methods
        public static DateTime? UtcTimeStringToLocalTime(this string time)
        {
            DateTime time2;
            if (!string.IsNullOrEmpty(time) && DateTime.TryParse(time, out time2))
            {
                return new DateTime?(time2);
            }
            return null;
        }

        public static DateTime UtcToLocalTime(this DateTime time)
        {
            TimeZoneInfo local = TimeZoneInfo.Local;
            return TimeZoneInfo.ConvertTimeFromUtc(time, local);
        }
    }

 

}
