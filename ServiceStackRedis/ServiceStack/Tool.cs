using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.Web;
using System.IO.Compression;
using System.Text.RegularExpressions;

namespace KYJ.ServiceStack
{
    public class Tool
    {
        #region tool
        /// <summary>
        /// 读取html数据
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string GetHtml(string str)
        {
            return GetHtml(str, "utf-8");
        }
        /// <summary>
        /// 获取HTML
        /// </summary>
        /// <param name="str"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        public static string GetHtml(string url, string code)
        {
            try
            {
                WebRequest mrequest = WebRequest.Create(url);
                WebResponse mresponse = mrequest.GetResponse();
                StreamReader sr = new StreamReader(mresponse.GetResponseStream(), Encoding.GetEncoding(code));
                return sr.ReadToEnd().Replace("\n", "").Replace("\r", "");
            }
            catch// (Exception ex)
            {
                return "";
            }

        }
        /// <summary>
        /// 获取HTML
        /// </summary>
        /// <param name="url"></param>
        /// <param name="postData"></param>
        /// <returns></returns>
        public static string GetPostHtml(string url, string postData)
        {
            StreamReader streamReader = null;
            Stream responseStream = null;
            HttpWebResponse httpWebResponse = null;
            Stream stream = null;
            try
            {
                byte[] byteRequest = Encoding.Default.GetBytes(postData);
                HttpWebRequest httpWebRequest;
                httpWebRequest = (HttpWebRequest)HttpWebRequest.Create(url);
                httpWebRequest.ContentType = "application/x-www-form-urlencoded";
                httpWebRequest.Accept = "*/*";
                httpWebRequest.UserAgent = "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 6.1; Trident/4.0; SLCC2; .NET CLR 2.0.50727; .NET CLR 3.5.30729; .NET CLR 3.0.30729; Media Center PC 6.0)";
                httpWebRequest.Headers.Add("x-flash-version", "10,0,32,18");
                httpWebRequest.Headers.Add(HttpRequestHeader.AcceptEncoding, "*/*");
                httpWebRequest.Headers.Add(HttpRequestHeader.AcceptLanguage, "zh-CN");
                httpWebRequest.Headers.Add(HttpRequestHeader.CacheControl, "no-cache");
                ServicePointManager.Expect100Continue = false;
                httpWebRequest.Method = "Post";
                httpWebRequest.KeepAlive = false;
                httpWebRequest.AllowAutoRedirect = true;
                httpWebRequest.ContentLength = byteRequest.Length;
                httpWebRequest.Timeout = 99999999;
                stream = httpWebRequest.GetRequestStream();
                stream.Write(byteRequest, 0, byteRequest.Length);


                httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                responseStream = httpWebResponse.GetResponseStream();
                streamReader = new StreamReader(responseStream, Encoding.UTF8);
                string html = streamReader.ReadToEnd();

                return html.Replace("\n", "").Replace("\r", "");
            }
            catch// (Exception ex)
            {
                return string.Empty;
            }
            finally
            {
                if (stream != null)
                {
                    stream.Close();
                }
                if (httpWebResponse != null)
                {
                    httpWebResponse.Close();
                }
                if (responseStream != null)
                {
                    responseStream.Close();
                }
                if (streamReader != null)
                {
                    streamReader.Close();
                }
            }

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

        public static double ToDouble(object str)
        {
            return ToDouble(str, 0);
        }
        public static double ToDouble(object str, double defValue)
        {
            double outValue;

            if (str != null && !String.IsNullOrEmpty(str.ToString()))
            {
                if (Double.TryParse(str.ToString(), out outValue))
                {
                    return outValue;
                }
            }
            return defValue;
        }

        public static string SetReplaceValue(string data)
        {
            if (string.IsNullOrEmpty(data))
            {
                return string.Empty;
            }
            data = ZipWrapper.Compress(data);
            data = HttpUtility.UrlEncode(data);
            return data.Replace("+", "%20").Replace("%2b", "#2B#");
        }

        public static string GetReplaceValue(string data)
        {
            if (string.IsNullOrEmpty(data))
            {
                return string.Empty;
            }
            data = data.Replace("%20", "+").Replace("#2B#", "%2b").Replace("\\", "");
            data = HttpUtility.UrlDecode(data);
            return ZipWrapper.Decompress(data);
        }


        /// <summary>
        /// 通过获取到的html json进行数据转换
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string GetObjectToJson<T>(string html)
        {
            if (html.Contains("[\"") && html.Contains("\"]"))
            {
                html = html.Replace("[\"", string.Empty).Replace("\"]", string.Empty).Replace("\"", string.Empty);
            }

            var arr = html.Split(',');

            return GetJsonToString<T>(arr, html);

        }

        public static string GetJsonToString<T>(string[] arr, string html)
        {
            int executeType = 3;//默认执行的是JSON对象方式 ,1=List<string>,2=string
            if (typeof(T) == typeof(List<string>))
            {
                executeType = 1;
            }
            if (typeof(T) == typeof(string))
            {
                executeType = 2;
            }
            StringBuilder GetObj = new StringBuilder();
            for (int i = 0; i < arr.Length; i++)
            {
                string content = arr[i];
                content = Tool.GetReplaceValue(content);
                if (executeType == 1 || executeType == 2)
                {
                    GetObj.Append("\"").Append(content).Append("\",");
                }
                else
                {
                    GetObj.Append(content);
                }

            }
            string typeStr = typeof(T).ToString();
            StringBuilder sb = new StringBuilder();
            if (typeStr.Contains("System.Collections.Generic"))
            {
                sb.Append("[");
            }

            if (executeType == 1 || executeType == 2)
            {
                sb.Append(GetObj.ToString());
            }
            else
            {
                string str = GetObj.ToString();
                MatchCollection matchs = Regex.Matches(str, "{(.*?)}");

                foreach (Match item in matchs)
                {
                    sb.Append(item.Groups[0].Value).Append(",");
                }
            }

            string sbEndStr = sb.ToString().TrimEnd(',');
            if (typeStr.Contains("System.Collections.Generic"))
            {
                sbEndStr += "]";
            }
            return sbEndStr;
        }

        /// <summary>
        /// 通过获取到的html数据转换为json
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static List<string> GetJsonToObject<T>(T info)
        {
            if (typeof(T) == typeof(List<string>) || typeof(T) == typeof(int) || typeof(T) == typeof(double))
            {
                return info as List<string>;
            }
            List<string> list = new List<string>();
            if (typeof(T) == typeof(string))
            {
                list.Add(info as string);
                return list;
            }
            string content = Newtonsoft.Json.JsonConvert.SerializeObject(info);
            MatchCollection matchs = Regex.Matches(content, "{(.*?)}");
            foreach (Match item in matchs)
            {
                list.Add(item.Groups[0].Value);
            }
            return list;
        }

        #endregion


        public static System.Xml.XmlDocument LoadXml()
        {
            System.Xml.XmlDocument xml = new System.Xml.XmlDocument();
            string s = AppDomain.CurrentDomain.SetupInformation.ConfigurationFile;
            xml.Load(s.Substring(0, s.LastIndexOf(@"\")) + "\\RedisConfigure.xml");
            return xml;
        }
        public static string GetRedisPostUrl(Enum FunctionTypeEnum, Enum CityEnum)
        {
            if (FunctionTypeEnum == null)
            {
                throw new Exception("FunctionTypeEnum 不能为空");
            }
            string key = "RedisServerDefault";
            string City = CityEnum == null ? string.Empty : CityEnum.ToString();
            string Type = FunctionTypeEnum.ToString();
          
            if (Type.Contains("NewHouse"))
            {
                key = "NewHouse01";
            }
            else if (Type.Contains("KYD") || Type.Contains("BaseData"))
            {
                key = "KuaiYouDai01";
            }
            else if (Type == "HouseImage")
            {
                string ciName = City.ToString().ToUpper();
                if (ciName == "BEIJING" || ciName == "SHANGHAI")
                {
                    key = "HouseImage01";
                }
                else if (ciName == "OTHER")
                {
                    key = "BidPrice01";
                }
                else
                {
                    key = "HouseImage02";
                }
            }
            else if (Type == "UserHeadImage")
            {
                key = "UserHeadImage01";
            }
            else if (City == "Other" || Type == "BidPrice" || Type == "WebDBData" || Type == "HouseDBData")
            {
                //如果是other要存到出价服务器中
                key = "BidPrice01";
            }
            else if (Type == "ViewCount")
            {
                key = "ViewCount01";
            }

            //Console.WriteLine("读取到的城市:" + City);
            //Console.WriteLine("读取到的Type:" + Type);
            //Console.WriteLine("读取到的key:" + key);
            try
            {
                System.Xml.XmlDocument doc = LoadXml();
                System.Xml.XmlAttributeCollection child = doc.ChildNodes[0].Attributes;
                string value = child[key].Value;
                if (string.IsNullOrEmpty(value))
                {
                    return child["RedisServerDefault"].Value;
                }
                return value;
            }
            catch// (Exception ex)
            {
                return string.Empty;
            }
        }

        public static string GetMd5(string str)
        {
            return System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(str, "md5").ToLower();
        }

    }
}
