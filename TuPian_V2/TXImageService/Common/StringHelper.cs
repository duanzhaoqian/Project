using System;
using System.Security.Cryptography;
using System.Text;

namespace Common
{
    /// <summary>
    /// string辅助类
    /// </summary>
    public class StringHelper
    {   
        /// <summary>
        /// 时间转换
        /// </summary>
        /// <param name="o"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        public static string ConvertTimeToString(object o,string format)
        {
            DateTime date;
            if (DateTime.TryParse(o.ToString(), out date))
                return date.ToString(format);
            return string.Empty;
        }
        /// <summary>
        /// 时间戳转为C#格式时间
        /// </summary>
        /// <param name="timeStamp">Unix时间戳格式</param>
        /// <returns>C#格式时间</returns>
        public static DateTime GetTime(string timeStamp)
        {
            DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            long lTime = long.Parse(timeStamp + "0000000");
            TimeSpan toNow = new TimeSpan(lTime);
            return dtStart.Add(toNow);
        }
        /// <summary>
        /// DateTime时间格式转换为Unix时间戳格式
        /// </summary>
        /// <param name="time"> DateTime时间格式</param>
        /// <returns>Unix时间戳格式</returns>
        public static int ConvertDateTimeInt(System.DateTime time)
        {
            System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1));
            return (int)(time - startTime).TotalSeconds;
        }
        /// <summary>
        /// 转换为linux时间戳
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public static long GetTimeStick(DateTime time)
        {
            DateTime date = time;
            date = date.ToUniversalTime();
            DateTime startDate = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            TimeSpan ts = date - startDate;
            long times = (long)ts.TotalSeconds;
            return times;
        }
        /// <summary>
        /// MD5　32位加密
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static  string UserMd5(string str)
        {
            string pwd = "";
            MD5 md5 = new MD5CryptoServiceProvider();//实例化一个md5对像
            // 加密后是一个字节类型的数组，这里要注意编码UTF8/Unicode等的选择　
            byte[] s = md5.ComputeHash(Encoding.UTF8.GetBytes(str));
            // 通过使用循环，将字节类型的数组转换为字符串，此字符串是常规字符格式化所得
            for (int i = 0; i < s.Length; i++)
            {
                // 将得到的字符串使用十六进制类型格式。格式后的字符是小写的字母，如果使用大写（X）则格式后的字符是大写字符 
                pwd = pwd + s[i].ToString("x2");
            }
            return pwd;
        }
        /// <summary>
        /// 截取字符串
        /// </summary>
        /// <param name="str">原字符串</param>
        /// <param name="size">截取长度</param>
        /// <returns>如果原字符串长度大于截取长度返回截取后的字符串不加...</returns>
        public static string SubString(string str, int size)
        {
            return SubString(str, size, false);
        }
        /// <summary>
        /// 截取字符串
        /// </summary>
        /// <param name="input">源字符</param>
        /// <param name="size">截取长度</param>
        /// <param name="patten">后缀</param>
        /// <returns></returns>
        public static string SubString(string input,int size,string patten)
        {
            if (string.IsNullOrWhiteSpace(input) || size < 1)
                return string.Empty;
            if (size > input.Length)
                return input;
            return input.Substring(0, size) + patten;
        }

        /// <summary>
        /// 截取字符串  重载方法
        /// </summary>
        /// <param name="str">源字符串</param>
        /// <param name="size">截取长度</param>
        /// <param name="Tag">截取后的新字符串后面是否加上...</param>
        /// <returns>如果原字符串长度大于截取长度返回截取后的新字符串</returns>
        public static string SubString(string str, int size, bool Tag)
        {
            if (string.IsNullOrWhiteSpace(str) || size < 1)
                return string.Empty;
            if (size > str.Length)
                return str;
            return Tag ? str.Substring(0, size) + "..." : str.Substring(0, size);
        }
        /// <summary>
        /// 去除字符串的HTML标签与脚本
        /// </summary>
        /// <param name="Htmlstring">待处理字符串</param>
        /// <returns>不含HTML标签和脚本的字符串</returns>
        public static string DelHTML(string Htmlstring)//将HTML去除
        {
            #region
            //删除脚本

            Htmlstring = System.Text.RegularExpressions.Regex.Replace(Htmlstring, @"<script[^>]*?>.*?</script>", "", System.Text.RegularExpressions.RegexOptions.IgnoreCase);

            //删除HTML

            Htmlstring = System.Text.RegularExpressions.Regex.Replace(Htmlstring, @"<(.[^>]*)>", "", System.Text.RegularExpressions.RegexOptions.IgnoreCase);

            Htmlstring = System.Text.RegularExpressions.Regex.Replace(Htmlstring, @"([/r/n])[/s]+", "", System.Text.RegularExpressions.RegexOptions.IgnoreCase);

            Htmlstring = System.Text.RegularExpressions.Regex.Replace(Htmlstring, @"-->", "", System.Text.RegularExpressions.RegexOptions.IgnoreCase);

            Htmlstring = System.Text.RegularExpressions.Regex.Replace(Htmlstring, @"<!--.*", "", System.Text.RegularExpressions.RegexOptions.IgnoreCase);

            //Htmlstring =System.Text.RegularExpressions. Regex.Replace(Htmlstring,@"<A>.*</A>","");

            //Htmlstring =System.Text.RegularExpressions. Regex.Replace(Htmlstring,@"<[a-zA-Z]*=/.[a-zA-Z]*/?[a-zA-Z]+=/d&/w=%[a-zA-Z]*|[A-Z0-9]","");

            Htmlstring = System.Text.RegularExpressions.Regex.Replace(Htmlstring, @"&(quot|#34);", "/", System.Text.RegularExpressions.RegexOptions.IgnoreCase);

            Htmlstring = System.Text.RegularExpressions.Regex.Replace(Htmlstring, @"&(amp|#38);", "&", System.Text.RegularExpressions.RegexOptions.IgnoreCase);

            Htmlstring = System.Text.RegularExpressions.Regex.Replace(Htmlstring, @"&(lt|#60);", "<", System.Text.RegularExpressions.RegexOptions.IgnoreCase);

            Htmlstring = System.Text.RegularExpressions.Regex.Replace(Htmlstring, @"&(gt|#62);", ">", System.Text.RegularExpressions.RegexOptions.IgnoreCase);

            Htmlstring = System.Text.RegularExpressions.Regex.Replace(Htmlstring, @"&(nbsp|#160);", " ", System.Text.RegularExpressions.RegexOptions.IgnoreCase);

            Htmlstring = System.Text.RegularExpressions.Regex.Replace(Htmlstring, @"&(iexcl|#161);", "/xa1", System.Text.RegularExpressions.RegexOptions.IgnoreCase);

            Htmlstring = System.Text.RegularExpressions.Regex.Replace(Htmlstring, @"&(cent|#162);", "/xa2", System.Text.RegularExpressions.RegexOptions.IgnoreCase);

            Htmlstring = System.Text.RegularExpressions.Regex.Replace(Htmlstring, @"&(pound|#163);", "/xa3", System.Text.RegularExpressions.RegexOptions.IgnoreCase);

            Htmlstring = System.Text.RegularExpressions.Regex.Replace(Htmlstring, @"&(copy|#169);", "/xa9", System.Text.RegularExpressions.RegexOptions.IgnoreCase);

            Htmlstring = System.Text.RegularExpressions.Regex.Replace(Htmlstring, @"&#(/d+);", "", System.Text.RegularExpressions.RegexOptions.IgnoreCase);


            Htmlstring.Replace("<", "");

            Htmlstring.Replace(">", "");

            Htmlstring.Replace("/r/n", "");

            //Htmlstring=HttpContext.Current.Server.HtmlEncode(Htmlstring).Trim();
            #endregion
            return Htmlstring;

        }
        /// <summary>
        /// </summary>
        /// <author>苏志刚</author>
        /// <param name="str">字符串</param>
        /// <param name="len">长度</param>
        /// <returns>返回最终字符串</returns>

        public static string Fix(string str, int len)
        {
            string s = str.ToString();
            //s = System.Text.RegularExpressions.Regex.Replace(s, "<[^>]*>", "");
            s = DelHTML(s);
            string result = ""; //最终返回的结果
            int byteLen = System.Text.Encoding.Default.GetByteCount(s);  //单字节字符长度

            int charLen = s.Length; //把字符平等对待时的字符串长度
            int byteCount = 0;  //记录读取进度{中文按两单位计算}
            int pos = 0;    //记录截取位置{中文按两单位计算}
            if (byteLen > len)
            {
                for (int i = 0; i < charLen; i++)
                {
                    if (Convert.ToInt32(s.ToCharArray()[i]) > 255)  //遇中文字符计数加2
                        byteCount += 2;
                    else         //按英文字符计算加1
                        byteCount += 1;
                    if (byteCount >= len)   //到达指定长度时，记录指针位置并停止
                    {
                        pos = i;
                        break;
                    }
                }
                result = s.Substring(0, pos) + "...";
            }
            else
                result = s;
            return result;
        }
        /// <summary>
        /// 判断是否是图片格式
        /// </summary>
        /// <param name="imgPath">图片地址</param>
        /// <returns>返回布尔值</returns>
        public static bool IsImgMode(string imgPath)
        {
            string SupportExtension = ".jpg|.gif|.jpeg|.bmp|.png";
            string mc = System.Text.RegularExpressions.Regex.Match(imgPath, "/.*(\\..*)").Groups[1].ToString();
            if (imgPath != "" && SupportExtension.Contains(mc))
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// 身份证隐藏
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string GetSizeString(string str)
        {
            if (str != "" && str.Length > 8)
            {
                int size = str.Length;
                string s = str.Substring(0, 6) + str.Substring(str.Length - 2, 2);
                return s.Insert(6, "**********");
            }
            return "";
        }
        /// <summary>
        /// 车辆识别代号的隐藏
        /// </summary>
        /// <param name="vin"></param>
        /// <returns></returns>
        public static string GetVIN(string vin)
        {
            string result = string.Empty;
            if (vin != "" && vin.Length >= 17)
            {
                int size = vin.Length;
                result = vin.Substring(0, 11);
                return result.Insert(11, "******");
            }
            else
            {
                result = vin.Insert(vin.Length, "******");
            }
            return result;
        }
        /// <summary>
        /// 身份证地址隐藏
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        public static string GetAddress(string address)
        {
            string result = string.Empty;
            if (address.Contains("县"))
            {
                int index = address.IndexOf("县", System.StringComparison.Ordinal);
                result = address.Substring(0, index + 1);
                return result.Insert(result.Length, "**********");
            }
            else if (address.Contains("区"))
            {
                int index = address.IndexOf("区", System.StringComparison.Ordinal);
                result = address.Substring(0, index + 1);
                return result.Insert(result.Length, "**********");
            }
            else if (address.Contains("市"))
            {
                int index = address.IndexOf("市", System.StringComparison.Ordinal);
                result = address.Substring(0, index + 1);
                return result.Insert(result.Length, "**********");
            }
            else if (address.Contains("省"))
            {
                int index = address.IndexOf("省", System.StringComparison.Ordinal);
                result = address.Substring(0, index + 1);
                return result.Insert(result.Length, "**********");
            }
            return result = address;
        }
    }
}
