using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net;

namespace TXCommons
{
    public class HttpMethod
    {
        /// <summary>
        /// 得到当前访问方式
        /// </summary>
        /// <returns></returns>
        public static string GetHttpMethod
        {
            get
            {
                return System.Web.HttpContext.Current.Request.HttpMethod;
            }
        }
        /// <summary>
        /// 获取Byte类型数据
        /// </summary>
        /// <param name="QueryKey"></param>
        /// <param name="DefaultValue"></param>
        /// <returns></returns>
        public static byte RequestByte(string QueryKey, byte DefaultValue)
        {
            byte result = DefaultValue;
            if (System.Web.HttpContext.Current.Request[QueryKey] != null && System.Web.HttpContext.Current.Request[QueryKey].Trim() != "")
            {
                byte.TryParse(System.Web.HttpContext.Current.Request[QueryKey].Trim(), out result);
            }
            return result;
        }
        public static byte RequestByte(string QueryKey, byte DefaultValue, bool ISQueryString)
        {
            byte result = DefaultValue;
            if (ISQueryString)
            {
                if (System.Web.HttpContext.Current.Request.QueryString[QueryKey] != null && System.Web.HttpContext.Current.Request.QueryString[QueryKey].Trim() != "")
                {
                    byte.TryParse(System.Web.HttpContext.Current.Request.QueryString[QueryKey].Trim(), out result);
                }
            }
            else
            {
                if (System.Web.HttpContext.Current.Request.Form[QueryKey] != null && System.Web.HttpContext.Current.Request.Form[QueryKey].Trim() != "")
                {
                    byte.TryParse(System.Web.HttpContext.Current.Request.Form[QueryKey].Trim(), out result);
                }
            }
            return result;
        }
        public static int RequestInt(string QueryKey)
        {
            int result = 0;
            if (System.Web.HttpContext.Current.Request[QueryKey] != null && System.Web.HttpContext.Current.Request[QueryKey].Trim() != "")
            {
                int.TryParse(System.Web.HttpContext.Current.Request[QueryKey].Trim(), out result);
            }
            return result;
        }
        public static int RequestInt(string QueryKey, int DefaultValue)
        {
            int result = DefaultValue;
            if (System.Web.HttpContext.Current.Request[QueryKey] != null && System.Web.HttpContext.Current.Request[QueryKey].Trim() != "")
            {
                int.TryParse(System.Web.HttpContext.Current.Request[QueryKey].Trim(), out result);
            }
            return result;
        }
        public static int RequestInt(string QueryKey, int DefaultValue, bool ISQueryString)
        {
            int result = DefaultValue;
            if (ISQueryString)
            {
                if (System.Web.HttpContext.Current.Request.QueryString[QueryKey] != null && System.Web.HttpContext.Current.Request.QueryString[QueryKey].Trim() != "")
                {
                    int.TryParse(System.Web.HttpContext.Current.Request.QueryString[QueryKey].Trim(), out result);
                }
            }
            else
            {
                if (System.Web.HttpContext.Current.Request.Form[QueryKey] != null && System.Web.HttpContext.Current.Request.Form[QueryKey].Trim() != "")
                {
                    int.TryParse(System.Web.HttpContext.Current.Request.Form[QueryKey].Trim(), out result);
                }
            }
            return result;
        }
        public static long RequestLong(string QueryKey, long DefaultValue)
        {
            long result = DefaultValue;
            if (System.Web.HttpContext.Current.Request[QueryKey] != null && System.Web.HttpContext.Current.Request[QueryKey].Trim() != "")
            {
                long.TryParse(System.Web.HttpContext.Current.Request[QueryKey].Trim(), out result);
            }
            return result;
        }
        public static decimal RequestDecimal(string QueryKey, decimal DefaultValue)
        {
            decimal result = DefaultValue;
            if (System.Web.HttpContext.Current.Request[QueryKey] != null && System.Web.HttpContext.Current.Request[QueryKey].Trim() != "")
            {
                decimal.TryParse(System.Web.HttpContext.Current.Request[QueryKey].Trim(), out result);
            }
            return result;
        }
        public static decimal RequestDecimal(string QueryKey, decimal DefaultValue, bool ISQueryString)
        {
            decimal result = DefaultValue;
            if (ISQueryString)
            {
                if (System.Web.HttpContext.Current.Request.QueryString[QueryKey] != null && System.Web.HttpContext.Current.Request.QueryString[QueryKey].Trim() != "")
                {
                    decimal.TryParse(System.Web.HttpContext.Current.Request.QueryString[QueryKey].Trim(), out result);
                }
            }
            else
            {
                if (System.Web.HttpContext.Current.Request.Form[QueryKey] != null && System.Web.HttpContext.Current.Request.Form[QueryKey].Trim() != "")
                {
                    decimal.TryParse(System.Web.HttpContext.Current.Request.Form[QueryKey].Trim(), out result);
                }
            }
            return result;
        }
        public static string RequestString(string QueryKey)
        {
            string result = "";
            if (System.Web.HttpContext.Current.Request[QueryKey] != null)
            {
                result = System.Web.HttpContext.Current.Request[QueryKey].Trim();

            }
            return result;
        }
        public static string RequestString(string QueryKey, string DefaultValue)
        {
            string result = DefaultValue;
            if (System.Web.HttpContext.Current.Request[QueryKey] != null)
            {
                result = System.Web.HttpContext.Current.Request[QueryKey].Trim();
            }
            return result;
        }
        public static string RequestString(string QueryKey, string DefaultValue, bool ISQueryString)
        {
            string result = DefaultValue;
            if (ISQueryString)
            {
                if (System.Web.HttpContext.Current.Request.QueryString[QueryKey] != null)
                {
                    result = System.Web.HttpContext.Current.Request.QueryString[QueryKey].Trim();
                }
            }
            else
            {
                if (System.Web.HttpContext.Current.Request.Form[QueryKey] != null)
                {
                    result = System.Web.HttpContext.Current.Request.Form[QueryKey].Trim();
                }
            }
            return result;
        }

        /// <summary>
        /// 过滤了特殊字符，防止sql注入和Xss攻击
        /// </summary>
        /// <param name="QueryKey"></param>
        /// <returns></returns>
        public static string RequestSafeString(string QueryKey)
        {
            string result = "";
            if (System.Web.HttpContext.Current.Request[QueryKey] != null)
            {
                result = RemoveUnsafeText(System.Web.HttpContext.Current.Request[QueryKey].Trim());

            }
            return result;
        }
        /// <summary>
        /// 防注入函数
        /// </summary>
        /// <param name="str">要处理的字符串</param>
        /// <returns></returns>
        public static string RemoveUnsafeText(string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return "";
            }
            str = str.Replace("&", "");
            str = str.Replace("<", "");
            str = str.Replace(">", "");
            str = str.Replace("'", "‘");
            //str = str.Replace("*", "");
            str = str.Replace("select", "");
            str = str.Replace("insert", "");
            str = str.Replace("update", "");
            str = str.Replace("delete", "");
            str = str.Replace("create", "");
            str = str.Replace("drop", "");
            //str = str.Replace("or", "");
            //str = str.Replace("and", "");
            str = str.Replace("=", "");
            str = str.Replace("delcare", "");
            str = str.Trim();
            return str;
        }

        /// <summary>
        /// 过滤了特殊字符，防止sql注入和Xss攻击
        /// </summary>
        /// <param name="QueryKey"></param>
        /// <param name="DefaultValue"></param>
        /// <returns></returns>
        public static string RequestSafeString(string QueryKey, string DefaultValue)
        {
            string result = DefaultValue;
            if (System.Web.HttpContext.Current.Request[QueryKey] != null)
            {
                result = RemoveUnsafeText(System.Web.HttpContext.Current.Request[QueryKey].Trim());
            }
            return result;
        }
        /// <summary>
        /// 过滤了特殊字符，防止sql注入和Xss攻击
        /// </summary>
        /// <param name="QueryKey"></param>
        /// <param name="DefaultValue"></param>
        /// <param name="ISQueryString"></param>
        /// <returns></returns>
        public static string RequestSafeString(string QueryKey, string DefaultValue, bool ISQueryString)
        {
            string result = DefaultValue;
            if (ISQueryString)
            {
                if (System.Web.HttpContext.Current.Request.QueryString[QueryKey] != null)
                {
                    result = RemoveUnsafeText(System.Web.HttpContext.Current.Request.QueryString[QueryKey].Trim());
                }
            }
            else
            {
                if (System.Web.HttpContext.Current.Request.Form[QueryKey] != null)
                {
                    result = RemoveUnsafeText(System.Web.HttpContext.Current.Request.Form[QueryKey].Trim());
                }
            }
            return result;
        }
        public static DateTime RequestDate(string QueryKey)
        {
            DateTime result = DateTime.MinValue;
            if (System.Web.HttpContext.Current.Request[QueryKey] != null && System.Web.HttpContext.Current.Request[QueryKey].Trim() != "")
            {
                DateTime.TryParse(System.Web.HttpContext.Current.Request[QueryKey].Trim(), out result);
            }
            return result;
        }
        public static DateTime RequestDate(string QueryKey, DateTime DefaultValue)
        {
            DateTime result = DefaultValue;
            if (System.Web.HttpContext.Current.Request[QueryKey] != null && System.Web.HttpContext.Current.Request[QueryKey].Trim() != "")
            {
                DateTime.TryParse(System.Web.HttpContext.Current.Request[QueryKey].Trim(), out result);
            }
            return result;
        }
        public static short RequestShort(string QueryKey, short DefaultValue)
        {
            short result = DefaultValue;
            if (System.Web.HttpContext.Current.Request[QueryKey] != null && System.Web.HttpContext.Current.Request[QueryKey].Trim() != "")
            {
                short.TryParse(System.Web.HttpContext.Current.Request[QueryKey].Trim(), out result);
            }
            return result;
        }
        public static bool ContainKey(string QueryKey)
        {
            bool result = false;
            if (System.Web.HttpContext.Current.Request[QueryKey] == null)
            {
                result = false;
            }
            else
                result = true;
            return result;
        }
        //一般不能查找的流都是只能CanRead的，只能一个字节一个字节读读下去，找到结束标志
        public static byte[] StreamToBytes(string url)
        {
            List<byte> bytes = new List<byte>();
            try
            {
                string _contentType = "application/x-www-form-urlencoded";
                string _accept = "image/gif, image/x-xbitmap, image/jpeg, image/pjpeg, application/x-shockwave-flash, application/x-silverlight, application/vnd.ms-excel, application/vnd.ms-powerpoint, application/msword, application/x-ms-application, application/x-ms-xbap, application/vnd.ms-xpsdocument, application/xaml+xml, application/x-silverlight-2-b1, */*";
                string _userAgent = "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 5.1; .NET CLR 2.0.50727; .NET CLR 3.0.04506.648; .NET CLR 3.5.21022)";

                HttpWebRequest httpWebRequest;
                httpWebRequest = (HttpWebRequest)HttpWebRequest.Create(url);
                httpWebRequest.ContentType = _contentType;
                httpWebRequest.Referer = url;
                httpWebRequest.Accept = _accept;
                httpWebRequest.UserAgent = _userAgent;
                httpWebRequest.Method = "Get";
                httpWebRequest.Timeout = 30 * 1000;

                HttpWebResponse httpWebResponse;
                httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                Stream stream = httpWebResponse.GetResponseStream();
                int temp = stream.ReadByte();
                while (temp != -1)
                {
                    bytes.Add((byte)temp);
                    temp = stream.ReadByte();
                }
                stream.Dispose();
            }
            catch (Exception ex)
            {
                //#if DEBUG
                //                throw ex;
                //#endif
                return null;
            }
            return bytes.ToArray();
        }


        /// <summary>
        /// 获取远程接口函数 utf-8编码,GET时将参数写在URL里面
        /// </summary>
        /// <param name="Params">post参数，可以为空</param>
        /// <param name="Url">远程接口地址</param>
        /// <param name="tOut">超时时间</param>
        /// <param name="Method">值为POST或GET</param>
        /// <param name="ds">返回数据</param>
        /// <returns>执行结果返回</returns>
        public static bool SendData(string Params, string Url, string tOut, string encoding, string Method, ref string retVal)
        {
            try
            {
                #region
                //try
                //{
                //把sXmlMessage发送到指定的DsmpUrl地址上
                //动态设置超时时间
                int timeOut = String.IsNullOrEmpty(tOut) ? 8 : Int32.Parse(tOut);
                Encoding encode = System.Text.Encoding.GetEncoding(encoding);
                byte[] arrB = encode.GetBytes(Params);
                HttpWebRequest myReq = (HttpWebRequest)WebRequest.Create(Url);
                myReq.Method = Method;
                myReq.Timeout = 1000 * timeOut;
                myReq.Headers.Add("Accept-Language:zh-cn,en-us");
                myReq.Headers.Set("Pragma", "no-cache");
                myReq.ContentType = "application/x-www-form-urlencoded";
                myReq.ContentLength = arrB.Length;
                if (myReq.Method.ToLower() == "post")
                {
                    Stream outStream = myReq.GetRequestStream();
                    outStream.Write(arrB, 0, arrB.Length);
                    outStream.Close();
                }


                //接收HTTP做出的响应
                WebResponse myResp = myReq.GetResponse();
                Stream ReceiveStream = myResp.GetResponseStream();
                StreamReader readStream = new StreamReader(ReceiveStream, encode);

                retVal = readStream.ReadToEnd();

                if (!string.IsNullOrEmpty(retVal)) return true;
                else
                    return false;
                #endregion
            }
            catch (Exception ex)
            {
                string refurl = "";
                if (System.Web.HttpContext.Current.Request.UrlReferrer != null)
                    refurl = "<br>来源: " + System.Web.HttpContext.Current.Request.UrlReferrer.PathAndQuery;


                return false;
            }
        }
    }
}
