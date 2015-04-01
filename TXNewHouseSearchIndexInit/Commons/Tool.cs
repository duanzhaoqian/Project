using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Commons
{
    public class Tool
    {

        public static Int32 ToInt32(object str)
        {
            return ToInt32(str, 0);
        }
        public static Int32 ToInt32(object str, Int32 defValue)
        {
            Int32 outValue;

            if (str != null && !String.IsNullOrEmpty(str.ToString()))
            {
                if (Int32.TryParse(str.ToString(), out outValue))
                {
                    return outValue;
                }
            }
            return defValue;
        }
        /// <summary>
        /// 取Web.Config值

        /// </summary>
        /// <param name="KeyName"></param>
        /// <returns></returns>
        public static string ConfigKey(string KeyName)
        {
            return System.Configuration.ConfigurationManager.AppSettings[KeyName];
        }
    }
}
