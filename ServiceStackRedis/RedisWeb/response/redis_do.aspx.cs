using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using KYJ.ServiceStack;
//using ServiceStack.TCP;
using ServiceStack;

namespace RedisWeb.response
{
    public partial class redis_do : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string key = Request["key"].Trim();
            string PostUrl = Request["PostUrl"];
            if (!string.IsNullOrEmpty(Request["type"]) && Request["type"] == "tcp")
            {
                TcpGetData(key, PostUrl);
            }
            else
            {
                Response.Write(GetData(key, PostUrl));
            }
        }
        private string TcpGetData(string key, string PostUrl)
        {
            string functiontype = PostUrl.Split(';')[0];

            Enum functionEnum = (FunctionTypeEnum)Enum.Parse(typeof(FunctionTypeEnum), functiontype);
            string val = ServiceStack.TCP.RedisHelperv2.GetValue<string>(key, functionEnum, 253);
            if (!string.IsNullOrEmpty(val))
            {
                return val;
            }
            var vallist = ServiceStack.TCP.RedisHelperv2.GetAllItemsFromList<string>(key, functionEnum, 0);
            StringBuilder sb = new StringBuilder();
            if (vallist != null && vallist.Count > 0)
            {
                foreach (string item in vallist)
                {
                    sb.Append(item);
                }
                return sb.ToString();
            }
            var valset = ServiceStack.TCP.RedisHelperv2.GetAllItemsFromSortedSet<string>(key, functionEnum, 0);
            if (valset != null && valset.Count > 0)
            {
                foreach (string item in valset)
                {
                    sb.Append(item);
                }
                return sb.ToString();
            }
            return string.Empty;
        }

        private string GetData(string key, string PostUrl)
        {
            StringBuilder sb = new StringBuilder();
            string zrangeHtml = zrange(key, PostUrl);
            if (!string.IsNullOrEmpty(zrangeHtml))
            {
                sb.Append(zrangeHtml);

                return sb.ToString();
            }

            string getHtml = get(key, PostUrl);
            if (!string.IsNullOrEmpty(getHtml))
            {
                sb.Append(getHtml);
                return sb.ToString();
            }

            string lrangeHtml = lrange(key, PostUrl);
            if (!string.IsNullOrEmpty(lrangeHtml))
            {
                sb.Append(lrangeHtml);
                return sb.ToString();
            }
            return string.Empty;
        }
        private string get(string key, string PostUrl)
        {
            StringBuilder sb = new StringBuilder().Append("opt=get&key=");
            sb.Append(key);
            string html = Tool.GetPostHtml(PostUrl, sb.ToString()).Replace("\"", "");
            if (html == "error" || html.Contains("ERR Operation against a key holding the wrong kind of value"))
            {
                return string.Empty;
            }
            return Tool.GetReplaceValue(html);
        }
        private string zrange(string key, string PostUrl)
        {
            StringBuilder sb = new StringBuilder().Append("opt=zrange&n=1&key=");
            sb.Append(key);
            string html = Tool.GetPostHtml(PostUrl, sb.ToString());
            if (html == "{}" || html.Contains("ERR Operation against a key holding the wrong kind of value"))
            {
                return string.Empty;
            }
            return Tool.GetObjectToJson<List<string>>(html);
        }
        private string lrange(string key, string PostUrl)
        {
            StringBuilder sb = new StringBuilder().Append("opt=lrange&n=1&key=");
            sb.Append(key);
            string html = Tool.GetPostHtml(PostUrl, sb.ToString());
            if (html == "{}" || html.Contains("ERR Operation against a key holding the wrong kind of value"))
            {
                return string.Empty;
            }
            return Tool.GetObjectToJson<List<string>>(html);
        }

        /// <summary>
        /// GET Keys
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="FunctionTypeEnum">ServiceStack.RedisType枚举</param>
        /// <returns></returns>
        public static string GetKeys(string key, string PostUrl)
        {
            try
            {
                StringBuilder sb = new StringBuilder().Append("opt=keys&key=");
                sb.Append(key);
                string html = Tool.GetPostHtml(PostUrl, sb.ToString());
                if (html == "{}" || html.Contains("ERR Operation against a key holding the wrong kind of value"))
                {
                    return string.Empty;
                }
                return Tool.GetObjectToJson<List<string>>(html);
            }
            catch// (Exception ex)
            {
                return string.Empty;
            }
        }
    }
}