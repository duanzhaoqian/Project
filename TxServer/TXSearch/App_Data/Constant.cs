using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text.RegularExpressions;
using System.IO;

namespace TXSearch
{
    public class Constant
    {
       ///   <summary>   
       ///    去除HTML标记   
       ///   </summary>   
       ///   <param    name="NoHTML">包括HTML的源码   </param>   
       ///   <returns>已经去除后的文字</returns>   
       public static string NoHTML(string Htmlstring)
       {
           //删除脚本   
           Htmlstring = Regex.Replace(Htmlstring, @"<script[^>]*?>.*?</script>", "", RegexOptions.IgnoreCase);
           //删除HTML   
           Htmlstring = Regex.Replace(Htmlstring, @"<(.[^>]*)>", "", RegexOptions.IgnoreCase);
           Htmlstring = Regex.Replace(Htmlstring, @"([\r\n])[\s]+", "", RegexOptions.IgnoreCase);
           Htmlstring = Regex.Replace(Htmlstring, @"-->", "", RegexOptions.IgnoreCase);
           Htmlstring = Regex.Replace(Htmlstring, @"<!--.*", "", RegexOptions.IgnoreCase);

           Htmlstring = Regex.Replace(Htmlstring, @"&(quot|#34);", "\"", RegexOptions.IgnoreCase);
           Htmlstring = Regex.Replace(Htmlstring, @"&(amp|#38);", "&", RegexOptions.IgnoreCase);
           Htmlstring = Regex.Replace(Htmlstring, @"&(lt|#60);", "<", RegexOptions.IgnoreCase);
           Htmlstring = Regex.Replace(Htmlstring, @"&(gt|#62);", ">", RegexOptions.IgnoreCase);
           Htmlstring = Regex.Replace(Htmlstring, @"&(nbsp|#160);", "   ", RegexOptions.IgnoreCase);
           Htmlstring = Regex.Replace(Htmlstring, @"&(iexcl|#161);", "\xa1", RegexOptions.IgnoreCase);
           Htmlstring = Regex.Replace(Htmlstring, @"&(cent|#162);", "\xa2", RegexOptions.IgnoreCase);
           Htmlstring = Regex.Replace(Htmlstring, @"&(pound|#163);", "\xa3", RegexOptions.IgnoreCase);
           Htmlstring = Regex.Replace(Htmlstring, @"&(copy|#169);", "\xa9", RegexOptions.IgnoreCase);
           Htmlstring = Regex.Replace(Htmlstring, @"&#(\d+);", "", RegexOptions.IgnoreCase);
           Htmlstring.Replace("<", "");
           Htmlstring.Replace(">", "");
           Htmlstring.Replace("\r\n", "");
           Htmlstring = HttpContext.Current.Server.HtmlEncode(Htmlstring).Trim();
           return Htmlstring;
       }

       /// <summary>
       /// Unix时间戳转成时间
       /// </summary>
       /// <param name="timeStamp"></param>
       /// <returns></returns>
       public static DateTime FROM_UNIXTIME(long timeStamp)
       {
           return DateTime.Parse("1970-01-01 00:00:00").AddSeconds(timeStamp);
       }

       /// <summary>
       /// 时间转成Unix时间戳
       /// </summary>
       /// <param name="dateTime"></param>
       /// <returns></returns>
       public static long UNIX_TIMESTAMP(DateTime dateTime)
       {
           return (dateTime.Ticks - DateTime.Parse("2010-01-01 00:00:00").Ticks) / 10000000;//自己改的时间
       }

       public static string GetIntValue(object s, int bit)
       {
           try
           {
               if (bit == 0)
                   return s.ToString();
               else
               {
                   string ss = s.ToString();
                   if (ss.IndexOf(".") > -1)
                   {
                       ss = ss.Substring(0, ss.IndexOf("."));
                   }
                   return string.Format("{0:D" + bit + "}", Convert.ToInt32(ss));
               }
           }
           catch
           {
               return string.Format("{0:D" + bit + "}", 0);
           }

       }
       public static string GetDecimalValue(object s)
       {
           try
           {
               decimal d = Convert.ToDecimal(s);
               return string.Format("{0:000000.00}", d);

           }
           catch { return "0"; }
       }


    }
}