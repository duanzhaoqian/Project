using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Script.Serialization;

namespace TXCommons
{
    /// <summary>
    /// FileName: Util.cs
    /// CLRVersion: 4.0.30319.17929
    /// Author: 黄继华
    /// Corporation:
    /// Description:
    /// DateTime: 2012/12/12 18:40:12
    /// </summary>
    public static class Util
    {
        //静态常数定义
        #region DefaultDateTime
        public static DateTime DefaultDateTime { get { return default(DateTime); } }
        #endregion

        //字符截取

        #region 字符串截取 区分中英文

        /// <summary>
        /// 字符是否为汉字 
        /// author:黄继华   
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        public static bool IsChinese(char c)
        {
            return (int)c >= 0x4E00 && (int)c <= 0x9FA5;
        }
        /// <summary>
        /// 判断字符串中是否有中文字符
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static bool IsHasChinese(this string s)
        {
            if (s == null)
                throw new ArgumentNullException();
            Regex regex = new Regex("[\u4e00-\u9fa5]+", RegexOptions.IgnoreCase);
            return regex.IsMatch(s);
        }

        /// <summary>
        /// 获得字节长度 
        /// author:黄继华 
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static int getLengthb(string str)
        {
            return System.Text.Encoding.Default.GetByteCount(str);
        }
        /// <summary>
        /// c#的中英文混合字符串截取指定长度 
        /// author:黄继华    
        /// </summary>
        /// <param name="str"></param>
        /// <param name="startidx">从0开始</param>
        /// <param name="len"></param>
        /// <returns></returns>
        public static string getStrLenB(string str, int startidx, int len)
        {
            int Lengthb = getLengthb(str);
            if (startidx + 1 > Lengthb)
            {
                return "";
            }
            int j = 0;
            int l = 0;
            int strw = 0;//字符的宽度
            bool b = false;
            string rstr = "";
            for (int i = 0; i < str.Length; i++)
            {
                char c = str[i];
                if (j >= startidx)
                {
                    rstr = rstr + c;
                    b = true;
                }
                if (IsChinese(c))
                {
                    strw = 2;
                }
                else
                {
                    strw = 1;
                }
                j = j + strw;
                if (b)
                {
                    l = l + strw;
                    if ((l + 1) >= len) break;
                }

            }
            return rstr;

        }

        #endregion

        //检测判断

        #region IsNullOrDBNull
        /// <summary>
        /// 判断对象 是否为null或DBNull
        /// Author: 黄继华
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsNullOrDBNull(this object value)
        {
            return value == null || value is DBNull;
        }
        #endregion

        #region IsNumeric
        public static bool IsNumeric(String value)
        {
            return Regex.IsMatch(value, @"[-+]?\b[0-9]*\.?[0-9]+\b");
        }
        #endregion

        #region IsInteger
        public static bool IsInteger(String value)
        {
            return Regex.IsMatch(value, @"^[-+]?\d+$");
        }
        #endregion

        //大小写转换

        #region ToLowerCase
        /// <summary>
        ///  转为小写
        ///  Author: 黄继华
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static String ToLowerCase(this String value)
        {
            if (string.IsNullOrEmpty(ToStringTrim(value))) return value;
            return value.ToLower(System.Globalization.CultureInfo.CurrentCulture);
        }
        #endregion

        #region ToUpperCase
        /// <summary>
        /// 转为大写
        /// Author: 黄继华
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static String ToUpperCase(this String value)
        {
            if (string.IsNullOrEmpty(ToStringTrim(value))) return value;
            return value.ToUpper(System.Globalization.CultureInfo.CurrentCulture);
        }
        #endregion

        //类型或其它转换
        #region ToInt
        public static Int32 ToInt(object value, Int32 defaultValue)
        {
            try
            {
                if (value.IsNullOrDBNull()) return defaultValue;
                Int32 _value = 0;
                if (!Int32.TryParse(value.ToString(), out _value)) _value = defaultValue;
                return _value;
            }
            catch { return defaultValue; }
        }
        public static Int32 ToInt(object value)
        {
            return ToInt(value, 0);
        }
        public static Int32 ToInt(this bool value)
        {
            return value ? 1 : 0;
        }
        #endregion

        #region ToInt64
        public static Int64 ToInt64(object value, Int64 defaultValue)
        {
            try
            {
                if (value.IsNullOrDBNull()) return defaultValue;
                Int64 _value = 0;
                if (!Int64.TryParse(value.ToString(), out _value)) _value = defaultValue;
                return _value;
            }
            catch { return defaultValue; }
        }
        public static Int64 ToInt64(object value)
        {
            return ToInt64(value, 0);
        }
        #endregion

        #region ToDouble
        public static Double ToDouble(object value, Double defaultValue)
        {
            try
            {
                if (value.IsNullOrDBNull()) return defaultValue;
                Double _value = 0;
                if (!Double.TryParse(value.ToString(), out _value)) _value = defaultValue;
                return _value;
            }
            catch { return defaultValue; }
        }
        #endregion

        #region ToDecimal
        public static Decimal ToDecimal(object value, Decimal defaultValue)
        {
            try
            {
                if (value.IsNullOrDBNull()) return defaultValue;
                Decimal _value = 0;
                if (!Decimal.TryParse(value.ToString(), out _value)) _value = defaultValue;
                return _value;
            }
            catch { return defaultValue; }
        }
        #endregion

        #region ToDateTime
        public static DateTime ToDateTime(object value, DateTime defaultValue)
        {
            try
            {
                if (value.IsNullOrDBNull()) return defaultValue;
                DateTime _value = default(DateTime);

                if (!DateTime.TryParse(value.ToString(), out _value)) _value = defaultValue;
                return _value;
            }
            catch { return defaultValue; }
        }
        public static DateTime ToDateTime(object value)
        {
            return ToDateTime(value, DefaultDateTime);
        }
        #endregion

        #region ToString
        public static String ToString(object value)
        {
            if (value.IsNullOrDBNull()) return String.Empty;
            return String.IsNullOrEmpty(value.ToString()) ? String.Empty : value.ToString();
        }
        #endregion

        #region ToBool
        public static bool ToBool(this object value)
        {
            try
            {
                if (value.IsNullOrDBNull()) return false;
                String _a = value.ToString().ToLowerCase();
                String[] _aa = { "1", "yes", "ok", "true", "on" };

                return _aa.Contains(_a);
            }
            catch { return false; }
        }
        #endregion
        #region 插入数据库的数据可能有英文的分号，替换这样的分号

        public static String ReplaceEngRednik(this object value)
        {
            return ToString(value).Trim().Replace("'", "''");
        }

        #endregion
        #region ToStringTrim
        public static String ToStringTrim(this object value)
        {
            return ToString(value).Trim();
        }
        #endregion

        #region ToEnumInt
        /// <summary>
        /// 转为int型枚举
        /// Author: 黄继华
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name"></param>
        /// <returns></returns>
        public static int ToEnumInt<T>(string name)
        {
            int i = -1;
            string[] t = Enum.GetNames(typeof(T));
            foreach (string f in t)
            {
                if (f.ToUpperCase() == name.ToUpperCase())
                {
                    i = (int)Enum.Parse(typeof(T), f);
                    break;
                }
            }
            return i;
        }
        #endregion

        #region 时间搓

        /// <summary>
        /// DateTime时间格式转换为Unix时间戳格式
        /// </summary>
        /// <param name=”time”></param>
        /// <returns></returns>
        public static int ConvertDateTimeInt(System.DateTime time)
        {
            System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1));
            return (int)(time - startTime).TotalSeconds;
        }

        ///// <summary>
        ///// 时间搓转换为时间
        ///// </summary>
        ///// <param name="timeStamp"></param>
        ///// <returns></returns>
        //public static DateTime FROM_UNIXTIME(long timeStamp)
        //{
        //    return DateTime.Parse("2010-01-01 00:00:00").AddSeconds(timeStamp);
        //}

        /// <summary>
        /// Unix时间戳转成时间
        /// </summary>
        /// <param name="timeStamp"></param>
        /// <returns></returns>
        public static DateTime FROM_UNIXTIME(long timeStamp)
        {
            return DateTime.Parse("2010-01-01 00:00:00.135").AddSeconds(timeStamp);
        }

        /// <summary>
        /// 时间转成Unix时间戳
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static long UNIX_TIMESTAMP(DateTime dateTime)
        {
            try
            {
                long i = (dateTime.Ticks - DateTime.Parse("2010-01-01 00:00:00.135").Ticks) / 10000000;//自己改的时间
                if (i == 0 || i == null || i < 1000)
                    i = 1000;
                return i;
            }
            catch { return 1000; }

        }

        #endregion

        #region 反序列化
        ///// <summary> 
        ///// 反序列化 
        ///// </summary> 
        ///// <param name="type">类型</param> 
        ///// <param name="xml">XML字符串</param> 
        ///// <returns></returns> 
        //public static object Deserialize(Type type, string xml)
        //{
        //    try
        //    {
        //        using (StringReader sr = new StringReader(xml))
        //        {
        //            XmlSerializer xmldes = new XmlSerializer(type);
        //            return xmldes.Deserialize(sr);
        //        }
        //    }

        //    catch (Exception e)
        //    {
        //        Log4netService.RecordLog.RecordException("反序列化", xml, e); return null;
        //    }

        //}      
        #endregion

        #region 序列化XML文件
        ///// <summary> 
        ///// 序列化XML文件 
        ///// </summary> 
        ///// <param name="type">类型</param> 
        ///// <param name="obj">对象</param> 
        ///// <returns></returns> 
        //public static string Serializer(Type type, object obj)
        //{
        //    MemoryStream Stream = new MemoryStream();
        //    //创建序列化对象 
        //    XmlSerializer xml = new XmlSerializer(type);
        //    try
        //    {
        //        //序列化对象 
        //        xml.Serialize(Stream, obj);
        //    }
        //    catch (InvalidOperationException e)
        //    {
        //        Log4netService.RecordLog.RecordException("反序列化", obj.ToString(), e); return null;
        //    }
        //    Stream.Position = 0;
        //    StreamReader sr = new StreamReader(Stream);
        //    string str = sr.ReadToEnd();
        //    return str;
        //}
        #endregion

        //Error提示信息
        #region MessageBox 黄继华 2013-6-13
        public static void MessageBox(String message)
        {
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.Write(message);
            HttpContext.Current.Response.End();
        }
        public static void MessageBox(String message, String url, String target)
        {
            String s = "<script type=\"text/javascript\">";
            if (!String.IsNullOrEmpty(message)) s += "alert('" + message + "');";
            if (!String.IsNullOrEmpty(url))
            {
                if (String.IsNullOrEmpty(target))
                {
                    target = "self.";
                }
                else
                {
                    target += ".";
                }
                s += target + "location.href='" + url + "';";
            }
            s += "</script>";
            HttpContext.Current.Response.Write(s);
            HttpContext.Current.Response.End();
        }
        public static void MessageBox(String message, bool tohistory)
        {
            String s = "<script type=\"text/javascript\">";
            if (!String.IsNullOrEmpty(message)) s += "alert('" + message + "');";
            if (tohistory)
            {
                s += "history.go(-1);";
            }
            s += "</script>";
            HttpContext.Current.Response.Write(s);
            HttpContext.Current.Response.End();
        }
        #endregion


        #region 判断是否英文
        /// <summary>
        /// 判断是否英文
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsEnglish(string str)
        {

            Regex regEnglish = new Regex("^[a-zA-Z]+$");

            return regEnglish.IsMatch(str);

        }
        #endregion



        #region 获得当前访问者的IP
        /// <summary>
        /// 获得当前访问者的IP
        /// </summary>
        /// <returns></returns>
        public static string GetIP()
        {
            string Ip = string.Empty;
            if (HttpContext.Current != null)
            {
                // 穿过代理服务器取远程用户真实IP地址

                if (HttpContext.Current.Request.ServerVariables["x-forward-for"] != null)
                {
                    Ip = HttpContext.Current.Request.ServerVariables["x-forward-for"];
                }

                else if (HttpContext.Current.Request.ServerVariables["HTTP_VIA"] != null)
                {
                    if (HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] == null)
                    {
                        if (HttpContext.Current.Request.ServerVariables["HTTP_CLIENT_IP"] != null)
                        {
                            Ip = HttpContext.Current.Request.ServerVariables["HTTP_CLIENT_IP"].ToString();
                        }
                        else
                        {
                            if (HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"] != null)
                            {
                                Ip = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"].ToString();
                            }
                            else
                            {
                                Ip = "";
                            }
                        }
                    }
                    else
                    {
                        Ip = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"].ToString();
                    }
                }
                else if (HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"] != null)
                {
                    Ip = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"].ToString();
                }
                else
                {
                    Ip = "";
                }
            }

            return Ip;

        }
        #endregion

        public static string NoHTML(string Htmlstring)
        {
            //删除脚本
            Htmlstring = Regex.Replace(Htmlstring, @"<script[^>]*?>.*?</script>", "",
              RegexOptions.IgnoreCase);
            //删除HTML
            Htmlstring = Regex.Replace(Htmlstring, @"<(.[^>]*)>", "",
              RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"([\r\n])[\s]+", "",
              RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"-->", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"<!--.*", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(quot|#34);", "\"",
              RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(amp|#38);", "&",
              RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(lt|#60);", "<",
              RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(gt|#62);", ">",
              RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(nbsp|#160);", "   ",
              RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(iexcl|#161);", "\xa1",
              RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(cent|#162);", "\xa2",
              RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(pound|#163);", "\xa3",
              RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(copy|#169);", "\xa9",
              RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&#(\d+);", "",
              RegexOptions.IgnoreCase);

            Htmlstring.Replace("<", "");
            Htmlstring.Replace(">", "");
            Htmlstring.Replace("\r\n", "");
            Htmlstring = HttpContext.Current.Server.HtmlEncode(Htmlstring).Trim();
            return Htmlstring;
        }


        /// <summary>
        /// 序列化
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string SerializeObject<T>(T value)
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(value);
        }

        /// <summary>
        /// 反序列化
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static object DeserializeObject<T>(string value)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject(value);
        }

        /// <summary>
        /// JSON格式数组转化为对应的List<T>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="JsonStr">JSON格式数组</param>
        /// <returns></returns>
        public static List<T> JSONStringToList<T>(string JsonStr)
        {
            JavaScriptSerializer Serializer = new JavaScriptSerializer();
            //设置转化JSON格式时字段长度
            List<T> objs = Serializer.Deserialize<List<T>>(JsonStr);
            return objs;
        }


    }
}
