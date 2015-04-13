using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using KYJ.ServiceStack;
using ServiceStack;

namespace RedisWeb.response
{
    public partial class HandleRedis : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string key = string.Empty;
            string PostUrl = string.Empty;
            if (Request["key"]!=null)
                key = Request["key"].Trim();
            if (Request["PostUrl"]!=null)
                PostUrl = Request["PostUrl"];
            string action = Request["action"];
            switch (action)
            {
                case "like":
                    Response.Write(GetAllKeys(key, PostUrl));
                    break;
                case "remove":
                    Response.Write(RemoveItemToRedis(key,PostUrl));
                    break;
                case "add":
                    Response.Write(AddItemToRedis());
                    break;
                case "removeLike":
                    Response.Write(RemoveItemToLikeRedis(key,PostUrl));
                    break;
                default:
                    break;
            }
        }

        //添加
        private string AddItemToRedis()
        { 
            List<string> list=new List<string>(){ "zhangsan","lisi","wangwu","zhaoliu" };
            bool isResult= Redis.AddItemToList<List<string>>("MyTestRedis", list, null, FunctionTypeEnum.Activities, CityEnum.BeiJing);
            return isResult == true ? "OK" : "NO";
        }

        //移除
        private bool RemoveItemToRedis(string Key,string PostUrl)
        {
            //bool isResult= Redis.Remove(Key, FunctionTypeEnum.Activities, CityEnum.BeiJing);
            StringBuilder sb = new StringBuilder().Append("opt=del&key=");
            sb.Append(Key);
            string html = Tool.GetPostHtml(PostUrl, sb.ToString());
            return Tool.ToInt32(html) == 1 ? true : false;
        }

        //模糊键移除
        private bool RemoveItemToLikeRedis(string Key,string PostUrl)
        {
            StringBuilder sb = new StringBuilder().Append("opt=del&key=");
            sb.Append(Key);
            string html = Tool.GetPostHtml(PostUrl, sb.ToString());
            return Tool.ToInt32(html) == 1 ? true : false;
        }

        /// <summary>
        /// 获取请求Redis数据
        /// </summary>
        /// <param name="key">键值</param>
        /// <param name="PostUrl">请求地址</param>
        /// <returns></returns>
        private string GetData(string key, string PostUrl)
        {
            StringBuilder _sbResult = new StringBuilder();
            RedisPost redisPost = RedisPostFactory.createPost((int)DataTypeEnum.KEY);
            redisPost.Key = key;
            redisPost.PostUrl = PostUrl;

            string strResult = string.Empty;
            if (!string.IsNullOrEmpty(redisPost.GetResult()))
            {
                return strResult = redisPost.GetResult();                
            }
            if (string.IsNullOrEmpty(strResult))
            {
                redisPost = RedisPostFactory.createPost((int)DataTypeEnum.SET);
                return strResult = redisPost.GetResult();    
            }
            if (string.IsNullOrEmpty(strResult))
            {
                redisPost = RedisPostFactory.createPost((int)DataTypeEnum.LIST);
                return strResult = redisPost.GetResult();
            }
            return "";
        }

        /// <summary>
        /// 获取所有的键值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="PostUrl"></param>
        /// <returns></returns>
        private string GetAllKeys(string key,string PostUrl)
        {
            RedisPost redisPost = RedisPostFactory.createPost((int)DataTypeEnum.ALLKEYS);
            redisPost.Key = key;
            redisPost.PostUrl = PostUrl;
            return redisPost.GetResult();
        }
    }

    //Redis 请求
    public abstract class RedisPost
    {
        public string Key { get; set; }
        public string PostUrl { get; set; }
        public RedisPost() { }
        public RedisPost(string Key,string UrlPost) 
        {
            this.Key = Key;
            this.PostUrl = UrlPost;
        }
        public abstract string GetResult();
    }

    //Key-Value 数据类型请求
    public class KeyValuePost:RedisPost
    {
        public override string GetResult()
        {
            StringBuilder _sb = new StringBuilder().Append("opt=get&key=");
            _sb.Append(this.Key);
            string html = Tool.GetPostHtml(PostUrl, _sb.ToString()).Replace("\"", "");
            if (html == "error" || html.Contains("ERR Operation against a key holding the wrong kind of value"))
            {
                return string.Empty;
            }
            return Tool.GetReplaceValue(html);
        }
    }

    //Set数据类型请求
    public class SetPost:RedisPost
    {
        public override string GetResult()
        {
            StringBuilder _sb = new StringBuilder().Append("opt=zrange&n=1&key=");
            _sb.Append(this.Key);
            string html = Tool.GetPostHtml(PostUrl, _sb.ToString());
            if (html == "{}" || html.Contains("ERR Operation against a key holding the wrong kind of value"))
            {
                return string.Empty;
            }
            return Tool.GetObjectToJson<List<string>>(html);
        }
    }

    //List 数据类型请求
    public class ListPost:RedisPost
    {
        public override string GetResult()
        {
            StringBuilder _sb = new StringBuilder().Append("opt=lrange&n=1&key=");
            _sb.Append(this.Key);
            string html = Tool.GetPostHtml(PostUrl, _sb.ToString());
            if (html == "{}" || html.Contains("ERR Operation against a key holding the wrong kind of value"))
            {
                return string.Empty;
            }
            return Tool.GetObjectToJson<List<string>>(html);
        }
    }

    //所有键值的请求
    public class AllKeysPost:RedisPost
    {
        public override string GetResult()
        {
            StringBuilder sb = new StringBuilder().Append("opt=keys&key=");
            sb.Append(this.Key);
            sb.Append("*");
            string html = Tool.GetPostHtml(this.PostUrl, sb.ToString()).Replace("\"", "");
            if (html == "error" || html.Contains("ERR Operation against a key holding the wrong kind of value"))
            {
                return string.Empty;
            }
            //return html = Tool.GetReplaceValue(html);
            return html;
        }
    }

    //Redis请求工厂
    public class RedisPostFactory
    {
        public static RedisPost createPost(int strPostWay)
        {
            RedisPost redisPost = null;
            switch (strPostWay)
            {
                case (int)DataTypeEnum.KEY:
                    redisPost = new KeyValuePost();
                    break;
                case (int)DataTypeEnum.SET:
                    redisPost = new SetPost();
                    break;
                case (int)DataTypeEnum.LIST:
                    redisPost = new ListPost();
                    break;
                case (int)DataTypeEnum.ALLKEYS:
                    redisPost=new AllKeysPost();
                    break;
                default:
                    break;
            }
            return redisPost;
        }
    }

    //数据类型枚举
    public enum DataTypeEnum
    { 
        KEY,
        SET,
        LIST,
        ALLKEYS
    }

}