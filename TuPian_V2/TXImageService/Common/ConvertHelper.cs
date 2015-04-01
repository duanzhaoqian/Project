using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common
{
    /// <summary>
    /// 为了转换数据的正确性
    /// </summary>
    public class ConvertHelper
    {
        #region String
        public static string GetString(object values)
        {
            return GetString(values, string.Empty);
        }

        public static string GetString(object values, string argDefault)
        {
            if (values == null)
            {
                return argDefault;
            }

            if (values.GetType().IsValueType)
            {
                return values.ToString();
            }

            string strVal = values as string;
            if (string.IsNullOrEmpty(strVal))
            {
                return argDefault;
            }
            return strVal;
        }
        #endregion
        #region Decimal
        public static decimal GetDecimal(object values)
        {
            return GetDecimal(values, 0m);
        }

        public static decimal GetDecimal(object values, decimal defaultvalues)
        {
            if (string.IsNullOrEmpty(Convert.ToString(values)))
                return defaultvalues;
            else
                return Convert.ToDecimal(values, new System.Globalization.CultureInfo("zh-cn"));
        }

        public static decimal? GetCanNullDecimal(object values)
        {
            if (string.IsNullOrEmpty(Convert.ToString(values)))
            {
                return null;
            }
            return Convert.ToDecimal(values);
        }
        #endregion
        #region DateTime
        public static DateTime? GetCanNullDateTime(object values)
        {
            if (string.IsNullOrEmpty(Convert.ToString(values)))
            {
                return null;
            }
            return Convert.ToDateTime(values);
        }

        public static DateTime GetDateTime(object values)
        {
            return GetDateTime(values, DateTime.Now);
        }

        public static DateTime GetDateTime(object values, DateTime defaultvalues)
        {
            if (string.IsNullOrEmpty(Convert.ToString(values)))
                return defaultvalues;
            else
                return Convert.ToDateTime(values);
        }

        public static DateTime GetDateTime(object values, DateTime defaultvalues, string strFormat)
        {
            if (string.IsNullOrEmpty(Convert.ToString(values)))
                return defaultvalues;
            else
                return DateTime.ParseExact(Convert.ToString(values), strFormat, null);
        }
        #endregion
        #region Int32
        public static int GetInt32(object values)
        {
            return GetInt32(values, 0);
        }

        public static int GetInt32(object values, int defaultvalue)
        {
            if (string.IsNullOrEmpty(Convert.ToString(values)))
                return defaultvalue;
            else
                return Convert.ToInt32(values);
        }

        public static int? GetCanNullInt32(object values)
        {
            if (string.IsNullOrEmpty(Convert.ToString(values)))
                return null;
            else
                return Convert.ToInt32(values);
        }
        #endregion
        #region bool
        public static bool GetBool(object values, bool argDefault = false)
        {
            if (string.IsNullOrEmpty(values as string))
            {
                return argDefault;
            }

            return Convert.ToBoolean(values);
        }
        #endregion
    }
}
