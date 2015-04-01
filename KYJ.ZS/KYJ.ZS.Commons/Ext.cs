using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KYJ.ZS.Commons
{
    using System.IO;
    using System.IO.Compression;

    /// <summary>
    /// 作者：wangyu
    /// 时间：2014/4/21 14:39:48
    /// 描述：扩展类
    /// </summary>
    public static class Ext
    {
        /// <summary>
        /// 指示指定的字符串是 null 还是 System.String.Empty 字符串。
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsNullOrEmpty(this string str)
        {
            return string.IsNullOrEmpty(str);
        }

        /// <summary>
        /// 忽略大小写文本比对
        /// </summary>
        /// <param name="str"></param>
        /// <param name="compareString"></param>
        /// <returns></returns>
        public static bool IsEquals(this string str, string compareString)
        {
            return str.IsEquals(compareString, true);
        }

        public static bool IsEquals(this string str, string compareString, bool ignoreCase)
        {
            if (str == compareString)
                return true;

            return string.Compare(str, compareString, ignoreCase) == 0;
        }

        public static bool IsGuid(this string guidString)
        {
            Guid
                guid;

            return IsGuid(guidString, out guid);
        }

        public static bool IsGuid(this string guidString, out Guid guid)
        {
            guid = Guid.Empty;
            if (guidString.IsNullOrEmpty())
                return false;
            bool
                result = Guid.TryParse(guidString, out guid);
            return result;
        }

        /// <summary>
        /// 转为int,默认0
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static int ToInt(this string str)
        {
            return str.ToInt(0);
        }

        /// <summary>
        /// 转为int
        /// </summary>
        /// <param name="str"></param>
        /// <param name="def">转换失败返回的默认值</param>
        /// <returns></returns>
        public static int ToInt(this string str, int def)
        {
            if (string.IsNullOrEmpty(str))
                return def;

            int
                i;
            bool
                result = int.TryParse(str, out i);

            return result ? i : def;
        }

        public static string FormatString(this string format, params object[] args)
        {
            return string.Format(format, args);
        }

        /// <summary>
        /// 将数字内容的字符串转为小数类型,默认0.0
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static double ToDouble(this string str)
        {
            return str.ToDouble(0.0);
        }

        /// <summary>
        /// 将数字内容的字符串转为小数类型
        /// </summary>
        /// <param name="str"></param>
        /// <param name="def">转换错误时返回的默认值</param>
        /// <returns></returns>
        public static double ToDouble(this string str, double def)
        {
            if (str.IsNullOrEmpty())
                return def;
            double
                d;
            bool
                r = double.TryParse(str, out d);
            return r ? d : def;
        }

        /// <summary>
        /// 转为guid,默认Guid.Empty
        /// </summary>
        /// <param name="guidString"></param>
        /// <returns></returns>
        public static Guid ToGuid(this string guidString)
        {
            return ToGuid(guidString, Guid.Empty);
        }

        public static Guid ToGuid(this string guidString, Guid def)
        {
            Guid
                guid = def;

            if (!guidString.IsGuid(out guid))
                return def;

            return guid;
        }

        public static DateTime ToDateTime(this string str, DateTime def)
        {
            if (str.IsNullOrEmpty())
                return def;
            DateTime
                time;
            bool
                result = DateTime.TryParse(str, out time);

            return result ? time : def;
        }

        public static bool ToBoolen(this string str)
        {
            return ToBoolen(str, false);
        }
        /// <summary>
        /// 作者：wangyu
        /// 时间：2014-04-17
        /// 描述：将字符串转换为bool,转换失败返回defValue值
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool ToBoolen(this string str, bool defValue)
        {
            bool outValue = defValue;
            if (str == null || String.IsNullOrEmpty(str.ToString())) return outValue;

            bool result = Boolean.TryParse(str.ToString(), out outValue);
            if (result) return outValue;
            return defValue;
        }

        /// <summary>
        /// 对字符串进行 base64 编码。
        /// </summary>
        /// <param name="str">待编码的字符串</param>
        /// <returns></returns>
        public static string ToBase64(this string str)
        {
            return ToBase64(str, Encoding.Default);
        }

        /// <summary>
        /// 对字符串进行 base64 编码。
        /// </summary>
        /// <param name="str">待编码的字符串</param>
        /// <param name="encode">字符集</param>
        /// <returns></returns>
        public static string ToBase64(this string str, Encoding encode)
        {
            if (str.IsNullOrEmpty()) return string.Empty;
            byte[]
                b = encode.GetBytes(str);
            return Convert.ToBase64String(b);
        }

        /// <summary>
        /// 对字符串进行 base64 编码。
        /// </summary>
        /// <param name="str">待编码的字符串</param>
        /// <returns></returns>
        public static string FromBase64(this string str)
        {
            return FromBase64(str, Encoding.Default);
        }

        /// <summary>
        /// 对字符串进行 base64 编码。
        /// </summary>
        /// <param name="str">待编码的字符串</param>
        /// <param name="encode">字符集</param>
        /// <returns></returns>
        public static string FromBase64(this string str, Encoding encode)
        {
            if (str.IsNullOrEmpty()) return string.Empty;
            return encode.GetString(Convert.FromBase64String(str));
        }

        /// <summary>
        /// 字符串压缩 加base64编码
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string Compress(this string str)
        {
            return Compress(str, Encoding.UTF8);
        }
        public static string Compress(this string str, Encoding encode)
        {
            if (str.IsNullOrEmpty()) return string.Empty;

            byte[] buffer = encode.GetBytes(str);
            using (var ms = new MemoryStream())
            {
                using (var compressedzipStream = new GZipStream(ms, CompressionMode.Compress, true))
                {
                    compressedzipStream.Write(buffer, 0, buffer.Length);
                }
                return Convert.ToBase64String(ms.ToArray());
            }
            
        }

        /// <summary>
        /// 字符串解压缩 解base64编码
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string Decompress(this string str)
        {
            return Decompress(str, Encoding.UTF8);
        }
        public static string Decompress(this string str, Encoding encode)
        {
            if (str.IsNullOrEmpty()) return string.Empty;

            byte[] buffer = Convert.FromBase64String(str);
            
            using (var ms = new MemoryStream(buffer))
            {
                using (var zipStream = new GZipStream(ms, CompressionMode.Decompress))
                {
                    using (var reader = new StreamReader(zipStream))
                    {
                        return reader.ReadToEnd();
                    }
                    
                }
                
            }

        }

        
    }
}
