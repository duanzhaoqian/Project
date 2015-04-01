using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Net;
using System.IO;
using System.Drawing;
using System.Drawing.Text;
using System.Globalization;
using System.Web;
using System.Threading;
using System.Web.Caching;
using System.Security.Cryptography;
using System.Collections;
using System.Runtime.Serialization.Json;
using System.Runtime.Serialization;
using System.Xml;
using System.Xml.Serialization;

namespace KYJ.ZS.Commons
{
    public class Auxiliary
    {
        public static string Key
        {
            get
            {
                return "kuaiyoujia.zushou";
            }
        }
        public static Random random = new Random();

        #region 将字符串转换为Int32

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
        /// 作者：wangyu
        /// 时间：2014-04-17
        /// 描述：将字符串转换为bool，默认false（包括转换失败）
        /// 
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool ToBoolen(object str)
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
        public static bool ToBoolen(object str, bool defValue)
        {
            bool outValue = defValue;
            if (str == null || String.IsNullOrEmpty(str.ToString())) return outValue;

            bool result = Boolean.TryParse(str.ToString(), out outValue);
            if (result) return outValue;
            return defValue;
        }
        /// <summary>
        /// 将字符串转换为Float
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static float ToFloat(String str, float defValue)
        {
            float outValue;

            if (!String.IsNullOrEmpty(str))
            {
                if (float.TryParse(str, out outValue))
                {
                    return outValue;
                }
            }
            return defValue;
        }
        /// <summary>
        /// 将字符串转换为decimal
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static decimal ToDecimal(string str)
        {
            return ToDecimal(str, 0);
        }
        /// <summary>
        /// 将字符串转换为decimal
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static decimal ToDecimal(string str, decimal defValue)
        {
            decimal outValue;
            if (!String.IsNullOrEmpty(str))
            {
                if (decimal.TryParse(str, out outValue))
                {
                    return outValue;
                }
            }
            return defValue;
        }
        ///// <summary>
        ///// 转义XML非法字符
        ///// </summary>
        ///// <param name="xmlString"></param>
        ///// <returns></returns>
        //public static string SaftXml(string xmlString) 
        //{
        //    return xmlString.Replace("<","&lt;" ).Replace(">","&gt;" ).Replace( "&","&amp;").Replace( "'","&apos;").Replace("\"","&quot;"); 
        //}
        #endregion

        #region txt日志文件保存方法
        /// <summary>
        /// 将文本保存成UTF-8编码文件。
        /// </summary>
        /// <param name="FileBody">要保存的文本</param>
        /// <param name="Path">保存文件路径</param>
        /// <param name="IsAppend">
        /// 是否为追加。
        /// true:追加
        /// false:覆盖原有内容
        /// </param>
        /// <returns>是否成功</returns>
        public static Boolean SaveToFile(StringBuilder FileBody, string Path, Boolean IsAppend)
        {
            return SaveToFile(FileBody, Path, IsAppend, Encoding.GetEncoding("UTF-8"));
        }

        /// <summary>
        /// 将文本保存成UTF-8编码文件。
        /// </summary>
        /// <param name="FileBody">要保存的文本</param>
        /// <param name="Path">保存文件路径</param>
        /// <param name="IsAppend">
        /// 是否为追加。
        /// true:追加
        /// false:覆盖原有内容
        /// </param>
        /// <returns>是否成功</returns>
        public static Boolean SaveToFile(StringBuilder FileBody, string Path, Boolean IsAppend, Encoding encoder)
        {
            if (Path.Length < 1)
                return false;
            string FilePath = Path.Substring(0, Path.LastIndexOf('\\'));         //设置文件要保存的文件夹。
            if (!System.IO.Directory.Exists(FilePath))
                System.IO.Directory.CreateDirectory(FilePath);
            FilePath = Path;
            try
            {
                //进行文件保存。
                using (StreamWriter sw = new StreamWriter(FilePath, IsAppend, encoder))
                {
                    sw.Write(FileBody.ToString());
                    sw.Flush();
                    sw.Close();
                }
            }
            catch
            {
                return false;
            }
            return true;
        }
        #endregion

        #region 取Web.Config值

        /// <summary>
        /// 取Web.Config值

        /// </summary>
        /// <param name="KeyName"></param>
        /// <returns></returns>
        public static string ConfigKey(string KeyName)
        {
            return System.Configuration.ConfigurationManager.AppSettings[KeyName];
        }
        #endregion

        #region 精确截取
        /// <summary>
        /// 精确截取
        /// </summary>
        /// <param name="length"></param>
        /// <param name="src"></param>
        /// <returns></returns>
        public static string CutStr(int length, string src)
        {
            if (length > 0)
            {
                Font font = new Font("Arial", 10);
                Bitmap bmp = new Bitmap(1, 1);
                Graphics g = Graphics.FromImage(bmp);
                SizeF s = new SizeF();
                float len = length * float.Parse("15.10417") + float.Parse("4.4401");
                for (int i = 0; i < src.Length; i++)
                {
                    s = g.MeasureString(src.Substring(0, src.Length - i), font);
                    if (s.Width <= len)
                    {
                        if (i != 0)
                        {
                            return src.Substring(0, src.Length - i - 1) + "...";//切后字符串
                        }
                        else
                        {
                            return src;//原始字符串
                        }

                    }
                }
            }
            return "";
        }
        #endregion
        /// <summary>
        /// 检查数据库返回对象是否有效
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static object CheckDbObject(object obj, object defaultValue)
        {
            object temp = defaultValue;

            if (obj != null && obj != DBNull.Value && !string.IsNullOrEmpty(obj.ToString().Trim()))
            {
                temp = obj;
            }

            return temp;
        }
        #region 校验DataSet中是否有数据

        public static bool CheckDs(DataSet ds)
        {
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 功能：获取dataset的第一表，第一行，第一列。无效返回空字符串。
        /// 作者：wangchengzhi
        /// 时间：2010-11-27
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        public static string GetFirstString(DataSet ds)
        {
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0] != null && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0 && ds.Tables[0].Columns.Count > 0)
                return ds.Tables[0].Rows[0][0].ToString();
            return "";
        }
        #endregion

        #region 转换DateTime


        /// <summary>
        /// 将字符串转换为DateTime
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static DateTime ToDateTime(String str, DateTime defValue)
        {
            DateTime outValue;

            if (!String.IsNullOrEmpty(str))
            {
                if (DateTime.TryParse(str, out outValue))
                {
                    return outValue;
                }
            }

            return defValue;
        }

        /// <summary>
        /// 将对象转换为DateTime
        /// </summary>
        /// <param name="obj">The obj.</param>
        /// <param name="defValue">The def value.</param>
        /// <returns></returns>
        public static DateTime ToDateTime(object obj, DateTime defValue)
        {
            if (obj != null)
            {
                defValue = ToDateTime(obj.ToString(), defValue);
            }

            return defValue;
        }
        #endregion

        #region 获取时间间隔
        /// <summary>
        /// 作者：wwang
        /// 时间：2014-5-19
        /// 描述：计算时间差，按时间差显示时间格式
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string DateStringFromNow(DateTime dt)
        {
            TimeSpan span = DateTime.Now - dt;
            if (span.TotalDays > 60)
            {
                return dt.ToShortDateString();
            }
            else if (span.TotalDays > 30)
            {
                return "1个月前";
            }
            else
            {

            }
            if (span.TotalDays > 14)
            {
                return "2周前";
            }
            else if (span.TotalDays > 7)
            {
                return "1周前";
            }
            else if (span.TotalDays > 1)
            {
                return string.Format("{0}天前", (int)Math.Floor(span.TotalDays));
            }
            else if (span.TotalHours > 1)
            {
                return string.Format("{0}小时前", (int)Math.Floor(span.TotalHours));
            }
            else if (span.TotalMinutes > 1)
            {
                return string.Format("{0}分钟前", (int)Math.Floor(span.TotalMinutes));
            }
            else if (span.TotalSeconds >= 1)
            {
                return string.Format("{0}秒前", (int)Math.Floor(span.TotalSeconds));
            }
            else
            {
                return "1秒前";
            }
        }


        #endregion

        #region 过滤掉字符串中所有html元素
        public static string CutAllHtmlElement(string InputStr)
        {
            return Regex.Replace(InputStr, @"<[^>]+>", "");
        }
        public static string CutAllHtmlElement_All(string InputStr)
        {
            return Regex.Replace(InputStr, @"<([\s\S]*?)>", "");
        }
        #endregion

        #region 过滤html中危险标签
        public static string CutDangerousHtmlElement(string html)
        {
            System.Text.RegularExpressions.Regex regex1 = new System.Text.RegularExpressions.Regex(@"<script[^>]*>[^>]*<[^>]script[^>]*>", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            System.Text.RegularExpressions.Regex regex4 = new System.Text.RegularExpressions.Regex(@"<iframe[^>]*>[^>]*<[^>]iframe[^>]*>", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            System.Text.RegularExpressions.Regex regex5 = new System.Text.RegularExpressions.Regex(@"<frameset[^>]*>[^>]*<[^>]frameset[^>]*>", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            System.Text.RegularExpressions.Regex regex6 = new System.Text.RegularExpressions.Regex(@"<img (.+?) />", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            html = regex1.Replace(html, ""); //过滤<script></script>标记  
            html = regex4.Replace(html, ""); //过滤iframe  
            html = regex5.Replace(html, ""); //过滤frameset 

            html = regex6.Replace(html, "<img  " + "${1}" + " />");

            return html;
        }
        #endregion


        /// <summary>
        /// 该方法用来检测用户输入是否带有恶意
        /// </summary>
        /// <param name="text">用户输入的文字</param>
        /// <param name="maxlength">最大的长度</param>
        /// <returns>返回验证后的文字</returns>
        public static string InputText(string text, int maxlength)
        {
            text = text.ToLower().Trim();
            if (string.IsNullOrEmpty(text))
                return string.Empty;
            if (text.Length > maxlength)
                text = text.Substring(0, maxlength);

            text = Regex.Replace(text, "[\\s]{2,{", " ");
            text = Regex.Replace(text, "(<[b|B][r|R]/*>)+|(<[p|P](.|\\n)*?>)", "\n");	//<br>
            text = Regex.Replace(text, "(\\s*&[n|N][b|B][s|S][p|P];\\s*)+", " ");	//&nbsp;
            text = Regex.Replace(text, "<(.|\\n)*?>", string.Empty);	//any other tags
            text = Regex.Replace(text, "=", "");
            text = Regex.Replace(text, "%", "");
            text = Regex.Replace(text, "'", "");
            text = Regex.Replace(text, "select", "");
            text = Regex.Replace(text, "insert", "");
            text = Regex.Replace(text, "delete", "");
            text = Regex.Replace(text, "or", "");
            text = Regex.Replace(text, "exec", "");
            text = Regex.Replace(text, "--", "");
            text = Regex.Replace(text, "and", "");
            text = Regex.Replace(text, "where", "");
            text = Regex.Replace(text, "update", "");
            text = Regex.Replace(text, "script", "");
            text = Regex.Replace(text, "iframe", "");
            text = Regex.Replace(text, "master", "");
            text = Regex.Replace(text, "exec", "");
            text = Regex.Replace(text, "<", "");
            text = Regex.Replace(text, ">", "");
            text = Regex.Replace(text, "\r\n", "");

            return text;
        }


        #region 根据正则匹配，返回第一个结果
        /// <summary>
        /// 根据正则匹配，返回第一个结果
        /// </summary>
        /// <param name="regTxt"></param>
        /// <param name="html"></param>
        /// <returns></returns>
        public static string GetSingleValue(string regTxt, string html)
        {
            Regex regex = new Regex(regTxt);
            Match m = regex.Match(html);
            string result = string.Empty;
            if (m.Success)
            {
                if (m.Groups.Count == 2)
                {
                    result = m.Groups[1].Value;
                }
                else
                {
                    result = m.Groups[0].Value;
                }
            }

            return result;

        }

        #endregion

        #region 判断输入是否是数字
        public static bool IsNumber(object inputData)
        {
            if (inputData != null && !Convert.IsDBNull(inputData))
            {
                if (inputData.ToString() != String.Empty)
                {
                    Match m = Regex.Match(inputData.ToString(), @"^[0-9]+$");
                    if (m.Success)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        #endregion

        #region 统计字符串的长度intercept
        /// <summary>
        /// 统计字符串的长度intercept
        /// </summary>
        public static int StringLength(string StringObject)
        {
            if (StringObject == null)
            {
                return 0;
            }

            int digit = 0;
            for (int i = 0; i < StringObject.Length; i++)
            {
                if (Convert.ToInt32(Convert.ToChar(StringObject.Substring(i, 1))) < Convert.ToInt32(Convert.ToChar(128)))
                {
                    digit += 1;
                }
                else
                {
                    digit += 2;
                }
            }
            return digit;
        }
        #endregion

        #region 获取指定页面的HTML代码

        /// <summary>
        /// 获取指定页面的HTML代码
        /// </summary>
        /// <param name="url">指定页面的路径</param>
        /// <param name="cookieCollection">Cookie集合</param>
        /// <returns></returns>
        public static string GetHTML(string postData, string url)
        {
            try
            {
                byte[] byteRequest = Encoding.UTF8.GetBytes(postData);
                HttpWebRequest httpWebRequest;
                httpWebRequest = (HttpWebRequest)HttpWebRequest.Create(url);
                //  httpWebRequest.Headers.Add("Accept-Encoding", "deflate");
                httpWebRequest.ContentType = "application/x-www-form-urlencoded";
                ServicePointManager.Expect100Continue = false;
                //httpWebRequest.Referer = url;
                //httpWebRequest.Accept = accept;
                //httpWebRequest.UserAgent = userAgent;
                httpWebRequest.Method = "Post";
                httpWebRequest.KeepAlive = false;
                httpWebRequest.ContentLength = byteRequest.Length;
                httpWebRequest.Timeout = 99999999;
                Stream stream = httpWebRequest.GetRequestStream();
                stream.Write(byteRequest, 0, byteRequest.Length);
                stream.Close();
                HttpWebResponse httpWebResponse;
                httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                Stream responseStream = httpWebResponse.GetResponseStream();
                StreamReader streamReader = new StreamReader(responseStream, Encoding.UTF8);
                string html = streamReader.ReadToEnd();
                streamReader.Close();
                responseStream.Close();

                return html.Replace("\n", "").Replace("\r", "");
            }
            catch (Exception ex)
            {
                return "";
            }
        }
        #endregion

        #region 双英文 字符串连接加空格
        /// <summary>
        /// 双英文 字符串连接加空格
        /// </summary>
        /// <param name="strOne"></param>
        /// <param name="stringTwo"></param>
        /// <returns></returns>
        public static string GetTowEnglishString(string strOne, string stringTwo)
        {
            if (string.IsNullOrEmpty(strOne) || string.IsNullOrEmpty(stringTwo)) return string.Empty;
            string strMerge = string.Empty;
            if (string.IsNullOrEmpty(stringTwo)) return strOne;
            if (Auxiliary.IsNumber(stringTwo.Substring(0, 1))) return (strOne + stringTwo);//stringTwo字符串的首字母是是数字返回 strOne + stringTwo
            if (!Auxiliary.IsChineseLetter(strOne) && !Auxiliary.IsChineseLetter(stringTwo))
                strMerge = strOne + " " + stringTwo;
            else
                strMerge = strOne + stringTwo;
            return strMerge;
        }
        #endregion

        #region 判断是中文
        /// <summary>
        /// 判断是中文
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool IsChineseLetter(string input)
        {
            bool isChineseLetter = false;
            if (!string.IsNullOrEmpty(input))
            {
                int bC = 0;
                for (int i = 0; i < input.Length; i++)
                {
                    Regex rx = new Regex("^[\u4e00-\u9fa5]$");
                    if (rx.IsMatch(input[i].ToString()))
                    {
                        bC++;
                    }
                }
                if (bC == 0)
                    isChineseLetter = false;
                else
                    isChineseLetter = true;
            }
            else
            {
                isChineseLetter = false;
            }
            return isChineseLetter;
        }
        #endregion

        #region 判断是否是英文字母
        /// <summary>
        /// 作者：lj
        /// 时间：2010-07-28
        /// 功能：判断是否是英文字母
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool IsEnglishLetter(string input)
        {
            bool isEnglishLetter = false;
            if (!string.IsNullOrEmpty(input))
            {
                for (int i = 0; i < input.Length; i++)
                {
                    Regex rx = new Regex(@"^[a-zA-Z]$");
                    if (!rx.IsMatch(input[i].ToString()))
                    {
                        isEnglishLetter = false;
                        break;
                    }
                    else isEnglishLetter = true;
                }
            }
            else isEnglishLetter = false;

            return isEnglishLetter;
        }
        #endregion

        #region 返回符合正则式的结果集
        public static ArrayList GetLinks(string inputstr, string Regexstr)
        {
            return GetLinks(inputstr, Regexstr, false);
        }

        public static ArrayList GetLinks(string inputstr, string Regexstr, bool IgnoreRepeated)
        {
            ArrayList compArray = new ArrayList();
            ArrayList myArray = new ArrayList();
            Match mymatch = Regex.Match(inputstr, Regexstr, RegexOptions.IgnoreCase);
            while (mymatch.Success)
            {
                bool isAdd = true;
                if (IgnoreRepeated && compArray.IndexOf(mymatch.Value) > -1)
                {
                    isAdd = false;
                }
                if (isAdd)
                {
                    myArray.Add(mymatch.Value);
                }
                compArray.Add(mymatch.Value);
                mymatch = mymatch.NextMatch();
            }
            return myArray;
        }
        #endregion

        #region SearchAndReplace
        public static string SearchAndReplace(string HtmlStr, string RegexStr, string ReplaceRegex, bool CutHtml)
        {
            string ReturnStr = "";
            Match mymatch = Regex.Match(HtmlStr, RegexStr, RegexOptions.IgnoreCase);
            if (mymatch.Success)
            {
                ReturnStr = mymatch.Value;
            }
            else
            {
                ReturnStr = "";
            }
            ReturnStr = Regex.Replace(ReturnStr, ReplaceRegex, "", RegexOptions.IgnoreCase);
            if (CutHtml)
            {
                ReturnStr = Auxiliary.CutAllHtmlElement(ReturnStr);
            }
            return ReturnStr.Trim();
        }

        #endregion

        #region 将汉字转化为Uncode字符
        static Regex regex = new Regex("[a-zA-Z0-9]");
        /// <summary>
        /// 将汉字转换为Unicode
        /// </summary>
        /// <param name="text">要转换的字符串</param>
        /// <returns></returns>
        public static string StringToUnicode(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return "";
            }
            string result = string.Empty;

            foreach (char a in text)
            {
                string lowCode = "", temp = "";

                if (!regex.IsMatch(a.ToString()))
                {
                    byte[] bytes = System.Text.Encoding.Unicode.GetBytes(a.ToString());

                    for (int i = 0; i < bytes.Length; i++)
                    {
                        if (i % 2 == 0)
                        {
                            temp = System.Convert.ToString(bytes[i], 16);//取出元素4编码内容（两位16进制）
                            if (temp.Length < 2) temp = "0" + temp;
                        }
                        else
                        {
                            string mytemp = Convert.ToString(bytes[i], 16);
                            if (mytemp.Length < 2) mytemp = "0" + mytemp; lowCode = lowCode + @"\u" + mytemp + temp;//取出元素4编码内容（两位16进制）
                        }
                    }
                }
                else
                {
                    lowCode = a.ToString();
                }
                result += lowCode;
            }
            return result;

        }

        /// <summary>
        /// 将Unicode字串\u....\u....格式字串转换为原始字符串
        /// </summary>
        /// <param name="srcText"></param>
        /// <returns></returns>
        public static string UnicodeToString(string srcText)
        {
            string str = "";
            str = srcText.Remove(0, 2);
            byte[] bytes = new byte[2];
            bytes[1] = byte.Parse(int.Parse(str.Substring(0, 2), NumberStyles.HexNumber).ToString());
            bytes[0] = byte.Parse(int.Parse(str.Substring(2, 2), NumberStyles.HexNumber).ToString());
            return Encoding.Unicode.GetString(bytes);
        }

        public static string UnicodeToStrings(string str)
        {
            Regex regex = new Regex(@"\\u[a-zA-Z0-9]{4}");
            MatchCollection mc = regex.Matches(str);
            foreach (Match m in mc)
            {
                string oldValue = m.Value;
                string newValue = UnicodeToString(m.Value);
                str = str.Replace(oldValue, newValue);
            }
            return str;
        }

        #endregion

        #region 转换GB2312 的字符串为UTF8编码
        /// <summary>
        /// 转换GB2312 的字符串为UTF8编码
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string GB2312ToUTF8(string str)
        {
            try
            {
                Encoding uft8 = Encoding.GetEncoding(65001);
                Encoding gb2312 = Encoding.GetEncoding("gb2312");
                byte[] temp = gb2312.GetBytes(str);
                byte[] temp1 = Encoding.Convert(gb2312, uft8, temp);
                string result = uft8.GetString(temp1);
                return result;
            }
            catch
            {
                return string.Empty;
            }
        }
        #endregion

        #region 去除HTML
        /// <summary>
        /// 作者：yjq
        /// 时间：2010-03-05
        /// 去除 htmlCode 中所有的HTML标签(包括标签中的属性)
        /// </summary>
        /// <param name="htmlCode">包含 HTML 代码的字符串</param>
        /// <returns>返回一个不包含 HTML 代码的字符串</returns>
        public static string RemoveHtml(string htmlCode)
        {
            if (null == htmlCode || 0 == htmlCode.Length)
            {
                return string.Empty;
            }
            return Regex.Replace(htmlCode, @"<[^>]+>", string.Empty, RegexOptions.IgnoreCase | RegexOptions.Multiline).Replace("&nbsp;", " ");

        }
        #endregion

        #region 获取页面传递参数变量值
        /// <summary>
        /// 作者：yjq
        /// 时间：2011-01-05
        /// 功能：获取由Get方式传参的参数值
        /// </summary>
        /// <param name="queryName">要过滤的字符串</param>
        /// <returns></returns>
        public static string GetQueryString(string queryName)
        {
            string str = string.Empty;
            if (System.Web.HttpContext.Current.Request.QueryString[queryName] == null)
            {
                return str;
            }
            str = System.Web.HttpContext.Current.Request.QueryString[queryName];
            if (string.IsNullOrEmpty(str))
            {
                return str;
            }
            if (string.IsNullOrEmpty(str.Trim()))
            {
                return str;
            }
            str = SaftXml(str);
            return str;
        }

        /// <summary>
        /// 作者：yjq
        /// 时间：2011-04-08
        /// 功能：接受参数兼容From GET 形式
        /// </summary>
        /// <param name="queryName">参数名称</param>
        /// <returns></returns>
        public static string GetString(string queryName)
        {

            string str = string.Empty;
            if (System.Web.HttpContext.Current.Request[queryName] == null)
            {
                return str;
            }
            str = Convert.ToString(System.Web.HttpContext.Current.Request[queryName]);
            if (string.IsNullOrEmpty(str))
            {
                return str;
            }
            if (string.IsNullOrEmpty(str.Trim()))
            {
                return str;
            }
            str = SaftXml(str);
            return str;
        }

        /// <summary>
        /// 作者：yjq
        /// 时间：2011-01-05
        /// 功能：获取由From方式传参的参数值
        /// </summary>
        /// <param name="queryName">要过滤的字符串</param>
        /// <returns></returns>
        public static string FormQueryString(string queryName)
        {
            string str = string.Empty;
            if (System.Web.HttpContext.Current.Request.Form[queryName] == null)
            {
                return str;
            }
            str = Convert.ToString(System.Web.HttpContext.Current.Request.Form[queryName]);
            if (string.IsNullOrEmpty(str))
            {
                return str;
            }
            if (string.IsNullOrEmpty(str.Trim()))
            {
                return str;
            }
            str = SaftXml(str);
            return str;
        }
        /// <summary>
        /// 作者：yjq
        /// 时间：2011-04-01
        /// 功能：转义比较危险的字符
        /// </summary>
        /// <param name="str">要进行转义的字符串</param>
        /// <returns>转义后的字符串</returns>
        public static string SaftXml(string str)
        {
            string newstr = string.Empty;
            if (string.IsNullOrEmpty(str) || string.IsNullOrEmpty(str.Trim()))
            {
                return newstr;
            }
            newstr = str;
            newstr = newstr.Replace("&", "&amp;");
            newstr = newstr.Replace("<", "&lt;");
            newstr = newstr.Replace(">", "&gt;");
            newstr = newstr.Replace("'", "&apos;");
            newstr = newstr.Replace("\"", "&quot;");

            return newstr;
        }

        #endregion

        #region 适应ISAPI_UrlRewrite获取当前RawUrl
        public static string RawUrl()
        {
            string cUrl = System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_REWRITE_URL"];
            if (cUrl == null || cUrl == "")
            {
                cUrl = System.Web.HttpContext.Current.Request.RawUrl;
            }
            return cUrl;
        }
        #endregion

        #region 文件操作
        /// <summary>
        /// 获取一个文件里文本内容
        /// </summary>
        /// <param name="FileFullPath">文件完整路径</param>
        /// <returns></returns>
        public static string GetFileContent(string FileFullPath, Encoding en)
        {
            string Content = "";
            Encoding enCode = Encoding.UTF8;
            if (en != null)
            {
                enCode = en;
            }
            if (System.IO.File.Exists(FileFullPath))
            {
                StreamReader sr = new StreamReader(FileFullPath, enCode);
                Content = sr.ReadToEnd();
                sr.Close();
            }
            return Content;
        }

        /// <summary>
        /// 建立多层文件夹
        /// </summary>
        /// <param name="FolderPath"></param>
        public static void CreateFolder(string FolderPath)
        {
            string[] arrFolder = FolderPath.Split('\\');
            string floder = "";
            for (int i = 0; i < arrFolder.Length; i++)
            {
                if (i > 0)
                {
                    floder = floder + "/" + arrFolder[i];
                    if (arrFolder[i].IndexOf("$") < 0 && arrFolder[i].IndexOf(".") < 0 && arrFolder[i] != "" && arrFolder[i].IndexOf("\\") < 0)
                    {

                        if (!System.IO.Directory.Exists(floder))
                        {
                            System.IO.Directory.CreateDirectory(floder);
                        }
                    }
                }
            }
        }
        #region 将文本写成任意扩展名的文件
        public static void WriteTextToFile(string Text, string FileFullPathAndName)
        {
            WriteTextToFile(Text, FileFullPathAndName, false, "gb2312");
        }
        public static void WriteTextToFile(string Text, string FileFullPathAndName, bool IsAppend)
        {
            WriteTextToFile(Text, FileFullPathAndName, IsAppend, "gb2312");
        }
        public static void WriteTextToFile(string Text, string FileFullPathAndName, bool IsAppend, string encoding)
        {
            if (Text == "" || Text == null)
            {
                return;
            }
            string DirName = GetDirByFileName(FileFullPathAndName);
            if (!Directory.Exists(DirName))
            {
                Directory.CreateDirectory(DirName);
            }
            if (!File.Exists(FileFullPathAndName))
            {
                using (File.Create(FileFullPathAndName)) { };
            }
            Encoding gbEncode = Encoding.GetEncoding(encoding);
            using (StreamWriter swFromFile = new StreamWriter(FileFullPathAndName, IsAppend, gbEncode))
            {
                swFromFile.Write(Text);
                swFromFile.Close();
            }
        }
        #endregion

        #region 根据文件名,取得目录名
        public static string GetDirByFileName(string FileName)
        {
            int position = FileName.LastIndexOf(@"\");
            if (position > 0)
            {
                return FileName.Substring(0, position);
            }
            else
            {
                return "";
            }
        }

        #endregion
        #endregion

        #region 去除参数值后的 0
        /// <summary>
        /// 2.0 --> 2; 2.00 --> 2; 2.220 --> 2.22; 2.00222 --> 2.00222; 0.00000 --> 0
        /// <para>author:xuefeng</para>
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string FormatFloat(string value)
        {
            string temp = value;

            if (string.IsNullOrEmpty(temp))
                return value;

            if (!Regex.IsMatch(temp, @"^[+-]?\d+[.]?\d*$"))
                return value;

            if (!temp.Contains("."))
                return value;

            int len = temp.Split('.')[1].Length;

            string[] temps = temp.Split('.');

            for (int i = temps[1].Length - 1; i >= 0; i--)
            {
                if (temps[1].EndsWith("0"))
                    temps[1] = temps[1].Remove(i);
                else
                    i = -1;
            }
            if (temps[1].Length <= 0)
                return temps[0];
            else
                return temps[0] + "." + temps[1];
        }

        public static bool CheckInt32(string value)
        {
            if (!Regex.IsMatch(value, @"^[+-]?\d+[.]?\d*$"))
                return false;
            else
                return true;
        }
        #endregion

        #region CDN抓取
        /// <summary>
        /// 用于CDN缓存页面
        /// </summary>
        public static void SetPageCacheHead()
        {
            SetPageCacheHead(120);
        }

        public static void SetPageCacheHead(int minutes)
        {
            System.Web.HttpContext.Current.Response.Cache.SetCacheability(System.Web.HttpCacheability.Public);
            System.Web.HttpContext.Current.Response.Cache.SetLastModified(DateTime.Now);
            System.Web.HttpContext.Current.Response.Cache.SetExpires(DateTime.Now.AddMinutes(minutes));
            System.Web.HttpContext.Current.Response.Cache.SetMaxAge(new TimeSpan(0, minutes, 0));
            System.Web.HttpContext.Current.Response.Cache.SetOmitVaryStar(true);
        }
        #endregion

        #region 双英文 字符串连接加空格
        /// <summary>
        /// 双英文 字符串连接加空格
        /// </summary>
        /// <param name="strOne"></param>
        /// <param name="stringTwo"></param>
        /// <returns></returns>
        public static string GetNewTwoEnglishString(string strOne, string stringTwo)
        {
            string strMerge = string.Empty;
            if (Auxiliary.IsNumber(stringTwo))
                return strMerge = strOne + stringTwo;
            if ((strOne == null || strOne == "") && (stringTwo == null || stringTwo == ""))
                return "";
            if (strOne == null || strOne == "")
                return stringTwo;
            if (stringTwo == null || stringTwo == "")
                return strOne;
            if (Auxiliary.IsNumber(strOne.Substring(strOne.Length - 1, 1)) || Auxiliary.IsNumber(stringTwo.Substring(0, 1)))
                return (strOne.Trim() + stringTwo.Trim());

            if (!Auxiliary.IsChineseLetter(strOne.Substring(strOne.Length - 1, 1)) && !Auxiliary.IsChineseLetter(stringTwo.Substring(1)))
                strMerge = strOne + " " + stringTwo;
            else
                strMerge = strOne + stringTwo;
            return strMerge;
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
                if (HttpContext.Current.Request.ServerVariables["HTTP_VIA"] != null)
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

        #region 校验DataTable中是否有数据
        /// <summary>
        /// 功能：dt != null && dt.Rows.Count > 0    return true;
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static bool CheckDt(DataTable dt)
        {
            if (dt != null && dt.Rows.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion


        /// <summary>
        /// 作者：yjq
        /// 时间：2011-04-07
        /// 功能：验证字符串是否为空 true 不为空
        /// </summary>
        /// <param name="ob"></param>
        /// <returns></returns>
        public static bool CheckStr(Object ob)
        {
            if (ob == null)
            {
                return false;
            }
            string newstr = Convert.ToString(ob).Trim();
            if (string.IsNullOrEmpty(newstr))
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 作者：yjq
        /// 时间：2011-04-08
        /// 功能：关闭本页，刷新父页面
        /// </summary>
        /// <returns>js</returns>
        public static string Getscript()
        {
            string result = @"<script language=javascript> 
                              window.opener.parent.document.location.reload(); 
                              window.opener=null;window.open( '', '_self');
                              window.close();
                              </script>";
            return result;
        }
        /// <summary>
        /// 读取html数据
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string readonedata(string str)
        {
            return readonedata(str, "gb2312");
        }
        /// <summary>
        /// 获取HTML
        /// </summary>
        /// <param name="str"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        public static string readonedata(string str, string code)
        {
            try
            {
                WebRequest mrequest = WebRequest.Create(str);
                WebResponse mresponse = mrequest.GetResponse();
                StreamReader sr = new StreamReader(mresponse.GetResponseStream(), Encoding.GetEncoding(code));
                return sr.ReadToEnd();
            }
            catch (Exception ex)
            {
                return "";
            }
        }
        public static string DownloadString(string str)
        {
            //WebClient client = new WebClient();
            //client.Encoding = System.Text.Encoding.UTF8;
            //return client.DownloadString(str);
            return DownloadString(str, System.Text.Encoding.UTF8);
        }
        public static string DownloadString(string str, Encoding code)
        {
            WebClient client = new WebClient();
            client.Encoding = code;
            return client.DownloadString(str);
        }
        /// <summary>   
        /// 加密   
        /// </summary>   
        /// <param name="Text"></param>   
        /// <returns></returns>   
        public static string Md5Encrypt(string Text)
        {
            return Md5Encrypt(Text, Key);
        }
        /// <summary>    
        /// 加密数据    
        /// </summary>    
        /// <param name="Text"></param>    
        /// <param name="sKey">密钥</param>    
        /// <returns></returns>    
        public static string Md5Encrypt(string Text, string sKey)
        {
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            byte[] inputByteArray;
            inputByteArray = Encoding.Default.GetBytes(Text);
            des.Key = ASCIIEncoding.ASCII.GetBytes(System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(sKey, "md5").Substring(0, 8));
            des.IV = ASCIIEncoding.ASCII.GetBytes(System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(sKey, "md5").Substring(0, 8));
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();
            StringBuilder ret = new StringBuilder();
            foreach (byte b in ms.ToArray())
            {
                ret.AppendFormat("{0:X2}", b);
            }
            return ret.ToString();
        }

        /// <summary>   
        /// 解密   
        /// </summary>   
        /// <param name="Text"></param>   
        /// <returns></returns>   
        public static string Md5Decrypt(string Text)
        {
            return Md5Decrypt(Text, Key);

        }
        /// <summary>    
        /// 解密数据    
        /// </summary>    
        /// <param name="Text"></param>    
        /// <param name="sKey">密钥</param>    
        /// <returns></returns>    
        public static string Md5Decrypt(string Text, string sKey)
        {
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            int len;
            len = Text.Length / 2;
            byte[] inputByteArray = new byte[len];
            int x, i;
            for (x = 0; x < len; x++)
            {
                i = Convert.ToInt32(Text.Substring(x * 2, 2), 16);
                inputByteArray[x] = (byte)i;
            }
            des.Key = ASCIIEncoding.ASCII.GetBytes(System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(sKey, "md5").Substring(0, 8));
            des.IV = ASCIIEncoding.ASCII.GetBytes(System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(sKey, "md5").Substring(0, 8));
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();
            return Encoding.Default.GetString(ms.ToArray());
        }
        /// <summary>
        /// 生成图片 
        /// </summary>
        /// <param name="maimg"></param>
        /// <returns></returns>
        public static string getFictionImage(string maimg, string savePath)
        {
            try
            {
                WebRequest request = WebRequest.Create(maimg);
                WebResponse response = request.GetResponse();
                Stream sm = response.GetResponseStream();
                int len = (int)response.ContentLength;
                BinaryReader br = new BinaryReader(sm);
                string filmurl = maimg.Substring(maimg.LastIndexOf("/") + 1);
                //string url = Server.MapPath("../images/fictionimage/");
                string url = AppDomain.CurrentDomain.BaseDirectory + savePath;
                if (!Directory.Exists(url))
                {
                    Directory.CreateDirectory(url);
                }
                FileStream fs = File.Create(url + filmurl);
                fs.Write(br.ReadBytes(len), 0, len);
                br.Close();
                fs.Close();
                return filmurl;
            }
            catch (Exception ex)
            {
                return "";
            }
        }
        /// <summary>
        /// 生成图片 
        /// </summary>
        /// <param name="maimg"></param>
        /// <returns></returns>
        public static string getFictionImage(string maimg, string savePath, string id, bool isReSuffix = false)
        {
            try
            {
                WebRequest request = WebRequest.Create(maimg);
                //request.Headers.Add("Host", "gaoxiao.jokeji.cn");
                //request.Headers.Add("Referer", "://gaoxiao.jokeji.cn/GrapHtml/zhenrengaoxiao/20081229231344.htm");
                WebResponse response = request.GetResponse();
                Stream sm = response.GetResponseStream();
                int len = (int)response.ContentLength;
                BinaryReader br = new BinaryReader(sm);
                //string filmurl = maimg.Substring(maimg.LastIndexOf("/") + 1);
                string lastName = maimg.Substring(maimg.LastIndexOf(".") + 1);
                //string url = Server.MapPath("../images/fictionimage/");
                //string url = AppDomain.CurrentDomain.BaseDirectory + savePath;
                if (!Directory.Exists(savePath))
                {
                    Directory.CreateDirectory(savePath);
                }
                string filename = string.Empty;
                if (isReSuffix)
                {
                    filename = id + "." + lastName;// + 
                }
                else
                {
                    filename = id + ".jpg";// + lastName
                }
                FileStream fs = File.Create(savePath + "\\" + filename);
                fs.Write(br.ReadBytes(len), 0, len);
                br.Close();
                fs.Close();
                return filename;
            }
            catch (Exception ex)
            {
                return "";
            }
        }
        #region 过滤文章内容关键字
        /// <summary>
        /// 过滤文章内容关键字
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string MyReplace(string str)
        {
            str = Regex.Replace(str, "(&lt;)(?:.*?)(&gt;)", "", RegexOptions.IgnoreCase);
            str = Regex.Replace(str, "(<img)(?:.*?)(/>)", "", RegexOptions.IgnoreCase);
            str = Regex.Replace(str, "(<)(?:.*?)(>)", "", RegexOptions.IgnoreCase);
            str = Regex.Replace(str, "<[^{><}]*>", "", RegexOptions.IgnoreCase);
            str = str.Replace("<>", "");
            str = str.Replace("&lt;", "");
            str = str.Replace("&gt;", "");
            str = str.Replace("\r", "");
            str = str.Replace("\n", "");
            str = str.Replace("nbsp;", "");
            str = str.Replace("ldquo;", "");
            str = str.Replace("middot;", "");
            str = str.Replace("rdquo;", "");
            str = str.Replace("amp;", "");
            str = str.Replace("mdash;", "");
            str = str.Replace("hellip;", "");
            str = str.Replace("lsquo;", "");
            str = str.Replace("&", "");
            str = str.Replace("<br>", "");
            str = str.Replace("<br />", "");
            str = str.Replace("<p>", "");
            str = str.Replace("</p>", "");
            str = str.Replace("<img", "");
            str = str.Replace("/>", "");
            str = str.Replace(" ", "");
            str = str.Replace("　", "");
            str = str.Replace("??", "");
            str = str.Replace("rsquo;", "");
            str = str.Replace("&", " ");
            str = str.Replace(" ", "");
            str = str.Replace("lt;", "");
            str = str.Replace("gt;", "");
            str = str.Replace("amp;", "");
            str = str.Replace("apos;", "");
            str = str.Replace("quot;", "");
            str = str.Replace("\"", "“");
            str = str.Replace("'", "‘");
            str = str.Replace("<", "＜");
            str = str.Replace(">", "＞");
            str = str.Replace("<!--EndFragment-->", "");
            //str = str.Replace("这里是换行，请不要删除，一定要记住", "<br />");
            return str;
        }
        #endregion
        public static string RegexTest(string reg, int site)
        {
            string html = GetFileContent("C:\\1.txt", Encoding.Default);
            return Regex.Match(html, reg, RegexOptions.IgnoreCase).Groups[site].Value;
        }
        public static string ChangeTime(object o)
        {
            return Convert.ToDateTime(o).ToString("yyyy-MM-dd");
        }
        public static string ChangeTimeToMMdd(object o)
        {
            return Convert.ToDateTime(o).ToString("MM-dd");
        }
        /// <summary>
        /// 获取HTML
        /// </summary>
        /// <param name="path"></param>
        public static void isHTML(string path)
        {
            bool istrue = HttpContext.Current.Request["g"] != null ? false : true;
            if (istrue)
            {
                string html = Auxiliary.GetFileContent(AppDomain.CurrentDomain.BaseDirectory + path, Encoding.Default);
                if (!string.IsNullOrEmpty(html))
                {
                    HttpContext.Current.Response.Write(html);
                    HttpContext.Current.Response.End();
                }
            }
        }

        /// <summary>
        ///  编码
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string urlEeCode(string str)
        {
            return HttpUtility.UrlEncode(str);
        }
        /// <summary>
        /// 替换搜索关键字
        /// </summary>
        /// <param name="content">内容</param>
        /// <param name="key">关键字</param>
        /// <returns></returns>
        public static string ReplaceRedToTitle(string content, string key)
        {
            return content.Replace(key, "<SPAN style=\"COLOR: red; FONT-SIZE: 15px\">" + key + "</SPAN>");
        }
        public static string GetPageUrl(string page, int index)
        {
            return page + "_" + index + ".html";
        }
        /// <summary>
        /// 生成图片到物理路径 
        /// </summary>
        /// <param name="img"></param>
        /// <param name="id"></param>
        public static string CreateFilmImage(string img, int id, string path, bool isSuffix = false, int subName = 0, int BagCount = 1000)
        {
            string sName = string.Empty;
            if (subName > 0)
            {
                sName = "_" + subName;
            }
            string saveid = Auxiliary.getFictionImage(img, Auxiliary.ConfigKey("savePath") + path + "\\" + (id / BagCount), id + sName, isSuffix);
            if (string.IsNullOrEmpty(saveid))
            {
                Auxiliary.getFictionImage("http://www.xiats.com/content/images/notimg.jpg", Auxiliary.ConfigKey("savePath") + (id / 1000), id.ToString());
            }
            return saveid;
        }
        public static string CutString(int length, object src)
        {
            string str = src.ToString();
            if (str.Length <= length)
                return str;
            return str.Substring(0, length) + "...";
        }

        /// <summary>
        /// 获取随机名称
        /// </summary>
        /// <param name="num">位数</param>
        /// <returns>名称</returns>
        public static string GetRandomFileName(int num)
        {
            string result = string.Empty;
            char[] chars = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z' };
            Random random = new Random();
            for (int i = 0; i < num; i++)
            {
                int index = random.Next(0, chars.Length);
                result += chars[index].ToString();
            }
            return result;
        }

        #region 序列化

        /// <summary> 
        /// 反序列化 
        /// </summary> 
        /// <param name="type">类型</param> 
        /// <param name="xml">XML字符串</param> 
        /// <returns></returns> 
        public static object Deserialize(Type type, string xml)
        {
            try
            {
                using (var sr = new StringReader(xml))
                {
                    var xmldes = new XmlSerializer(type);

                    return xmldes.Deserialize(sr);
                }
            }
            catch (Exception e)
            {
                //Log4netService.RecordLog.RecordException("反序列化", xml, e); 
                return null;
            }
        }

        /// <summary> 
        /// 序列化XML文件 
        /// </summary> 
        /// <param name="type">类型</param> 
        /// <param name="obj">对象</param> 
        /// <returns></returns> 
        public static string Serializer(Type type, object obj)
        {
            var stream = new MemoryStream();
            //创建序列化对象 
            var xml = new XmlSerializer(type);
            try
            {
                //序列化对象 
                xml.Serialize(stream, obj);
            }
            catch (InvalidOperationException e)
            {
                //Log4netService.RecordLog.RecordException("序列化XML文件", obj.ToString(), e); return null;
            }
            stream.Position = 0;
            var sr = new StreamReader(stream);
            string str = sr.ReadToEnd();
            return str;
        }


        public static string SerializeWrite<T>(T instance)
        {
            DataContractSerializer serializer = new DataContractSerializer(typeof(T));


            System.IO.MemoryStream st = new System.IO.MemoryStream();

            serializer.WriteObject(st, instance);
            //st.Position = 0;
            // var sr = new StreamReader(st);

            // string _serializeString = sr.ReadToEnd(); ;

            //  sr.Close();
            byte[] array = st.ToArray();
            st.Close();
            string _serializeString = Encoding.UTF8.GetString(array, 0, array.Length);
            return _serializeString;

        }

        public static object SerializeRead<T>(string url)
        {
            DataContractSerializer serializer = new DataContractSerializer(typeof(T));

            XmlReader xml = XmlReader.Create(url);
            var o = serializer.ReadObject(xml);
            xml.Close();
            xml = null;

            return o;


        }

        //json 序列化  

        public static string ToJsJson(object item)
        {
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(item.GetType());
            using (MemoryStream ms = new MemoryStream())
            {
                serializer.WriteObject(ms, item);
                StringBuilder sb = new StringBuilder();
                sb.Append(Encoding.UTF8.GetString(ms.ToArray()));
                return sb.ToString();
            }
        }

        //反序列化  

        public static T FromJsonTo<T>(string jsonString)
        {
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(T));
            using (MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(jsonString)))
            {
                T jsonObject = (T)ser.ReadObject(ms);
                return jsonObject;
            }
        }

        public static string ToJson<T>(object customer)
        {

            DataContractJsonSerializer ds = new DataContractJsonSerializer(typeof(T));
            MemoryStream ms = new MemoryStream();

            ds.WriteObject(ms, customer);

            string strReturn = Encoding.UTF8.GetString(ms.ToArray());
            ms.Close();
            return strReturn;
        }

        public static object FromJson<T>(string strJson)
        {
            DataContractJsonSerializer ds = new DataContractJsonSerializer(typeof(T));
            MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(strJson));

            return ds.ReadObject(ms);
        }

        #endregion

        #region 正则表达式验证

        /// <summary>
        /// 正则验证
        /// </summary>
        /// <param name="regex">正则</param>
        /// <param name="strObj">需要验证的内容</param>
        /// <returns>满足：true，不满足：false</returns>
        public static bool KYJRegex(string regex, string strObj)
        {
            return Regex.IsMatch(strObj, @regex);
        }

        #endregion

    }
}
