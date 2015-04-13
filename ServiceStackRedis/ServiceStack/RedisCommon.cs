using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KYJ.ServiceStack;
using System.Text.RegularExpressions;

namespace KYJ.ServiceStack
{
    public class RedisCommon
    {
        public static string GetZADDData<T>(string key, T info, double scores, string LuaPostUrl)
        {
            List<string> list = Tool.GetJsonToObject<T>(info);
            string state = string.Empty;
            foreach (var item in list)
            {
                string url = "opt=zadd&n=2&cmd=" + scores + "&key=" + key + "&val=";
                string itemVal = Tool.SetReplaceValue(item);
                url += itemVal;
                state = Tool.GetPostHtml(LuaPostUrl, url);
            }
            return state;
        }
        public static string GetLPUSHData<T>(string key, T info, string LuaPostUrl)
        {
            //StringBuilder sb = new StringBuilder();

            //var list = Tool.GetJsonToObject<T>(info);
            //sb.Append("&cmd=");

            //foreach (var item in list)
            //{
            //    string itemVal = Tool.SetReplaceValue(item);
            //    sb.Append("LPUSH ").Append(key).Append(" ").Append(itemVal);
            //    sb.Append("%0D%0A");
            //}
            //return sb.ToString();
            List<string> list = Tool.GetJsonToObject<T>(info);
            string state = string.Empty;
            foreach (var item in list)
            {
                string url = "opt=lpush&n=2&cmd=3&key=" + key + "&val=";
                string itemVal = Tool.SetReplaceValue(item);
                url += itemVal;
                state = Tool.GetPostHtml(LuaPostUrl, url);
            }
            return state;
        }

        public static string GetZREMData<T>(string key, T info, string LuaPostUrl)
        {
            //StringBuilder sb = new StringBuilder();
            //List<string> list = Tool.GetJsonToObject<T>(info);
            //sb.Append("&cmd=");
            //foreach (var item in list)
            //{
            //    string itemVal = Tool.SetReplaceValue(item);
            //    sb.Append("zrem ").Append(key).Append(" ").Append(itemVal);
            //    sb.Append("%0D%0A");
            //}
            //return sb.ToString();
            List<string> list = Tool.GetJsonToObject<T>(info);
            string state = string.Empty;
            foreach (var item in list)
            {
                string url = "opt=zrem&key=" + key + "&val=";
                string itemVal = Tool.SetReplaceValue(item);
                url += itemVal;
                state = Tool.GetPostHtml(LuaPostUrl, url);
            }
            return state;
        }

        public static string GetLREMData<T>(string key, T info, string LuaPostUrl)
        {
            //StringBuilder sb = new StringBuilder();
            List<string> list = Tool.GetJsonToObject<T>(info);
            //sb.Append("&key=");
            string state = string.Empty;
            foreach (var item in list)
            {
                //string itemVal = Tool.SetReplaceValue(item);
                //sb.Append("LREM ").Append(key).Append(" 1 ").Append(itemVal);
                //sb.Append("%0D%0A");

                string url = "opt=lrem&key=" + key + "&val=";
                string itemVal = Tool.SetReplaceValue(item);
                url += itemVal;
                state = Tool.GetPostHtml(LuaPostUrl, url);
            }
            return state;
            //return sb.ToString();
        }

        public static IDictionary<T, double> GetWeightObject<T>(string html)
        {
            IDictionary<T, double> weight = new Dictionary<T, double>();

            html = html.Replace("[", string.Empty).Replace("]", string.Empty);

            MatchCollection matchs = Regex.Matches(html, "\"(.*?)\",\"(\\d+|\\d+.\\d+)\"");//
            foreach (Match item in matchs)
            {
                string strVal = item.Groups[0].Value;
                string strWeight = Regex.Match(strVal, "\"(?:.*?)\",\"(.*?)\"").Groups[1].Value;
                string strObj = Tool.GetReplaceValue(item.Groups[1].Value);
                weight.Add(Newtonsoft.Json.JsonConvert.DeserializeObject<T>(strObj), Tool.ToDouble(strWeight));
            }
            return weight;
        }

        ///// <summary>
        ///// 设置键的过期时间
        ///// </summary>
        ///// <param name="key"></param>
        ///// <param name="expireDateTime"></param>
        ///// <param name="sb"></param>
        //public static void SetExpireDateTime(string key, DateTime? expireDateTime, StringBuilder sb)
        //{
        //    if (expireDateTime != null)
        //    {
        //        TimeSpan span = (TimeSpan)(expireDateTime - DateTime.Now);
        //        sb.Append(" EXPIRE ").Append(key).Append(" ").Append(span.TotalSeconds.ToString());
        //        sb.Append("%0D%0A");
        //    }
        //}
    }
}
