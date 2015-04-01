using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace KYJ.ZS.Commons
{
    public class PubConstant
    {

        public static string GetConfigString(string key)
        {
            return ConfigurationManager.AppSettings[key].ToString();
        }
        /// <summary>
        /// 域
        /// </summary>
        public static string Domain
        {
            get
            {
                return ConfigurationManager.AppSettings["domain"].ToString();
            }
        }
        /// <summary>
        /// 静态JS/CSS地址
        /// </summary>
        public static string StaticUrl
        {
            get
            {
                return Auxiliary.ConfigKey("static.baseurl");
            }
        }
        /// <summary>
        /// 前台地址
        /// </summary>
        public static string WebBaseUrl
        {
            get
            {
                return Auxiliary.ConfigKey("web.baseurl");
            }
        }
        /// <summary>
        /// 前台搜索地址
        /// </summary>
        public static string SearchBaseUrl
        {
            get
            {
                return Auxiliary.ConfigKey("search.baseurl");
            }
        }
        /// <summary>
        /// 前台地址
        /// </summary>
        public static string PayBaseUrl
        {
            get
            {
                return Auxiliary.ConfigKey("pay.baseurl");
            }
        }
        /// <summary>
        /// 用户后台地址
        /// </summary>
        public static string UserBaseUrl
        {
            get
            {
                return Auxiliary.ConfigKey("user.baseurl");
            }
        }
        /// <summary>
        /// 专题地址
        /// </summary>
        public static string SpecialBaseUrl
        {
            get
            {
                return Auxiliary.ConfigKey("special.baseurl");
            }
        }
        /// <summary>
        /// 帮助中心地址
        /// </summary>
        public static string HelpBaseUrl
        {
            get
            {
                return Auxiliary.ConfigKey("help.baseurl");
            }
        }
        /// <summary>
        /// 管理后台地址
        /// </summary>
        public static string ManagerBaseUrl
        {
            get
            {
                return Auxiliary.ConfigKey("manager.baseurl"); ;
            }
        }
        /// <summary>
        /// 商户后台地址
        /// </summary>
        public static string MerchantBaseUrl
        {
            get
            {
                return Auxiliary.ConfigKey("merchant.baseurl");
            }
        }
        /// <summary>
        /// 图片上传url
        /// </summary>
        public static string ImgUploadBaseUrl
        {
            get
            {
                return Auxiliary.ConfigKey("imgupload.baseurl");
            }
        }
        /// <summary>
        /// 短信接口地址
        /// </summary>
        public static string SMSWebUrl
        {
            get { return Auxiliary.ConfigKey("sms.baseurl"); }
        }

        /// <summary>
        /// 登录CookieName
        /// </summary>
        public static string COOKIE_NAME
        {
            get
            {
                return Auxiliary.ConfigKey("cookie_name");
            }
        }
        /// <summary>
        /// 登录次数CookieName
        /// </summary>
        public static string NUMBER_COOKIE
        {
            get
            {
                return Auxiliary.ConfigKey("number_cookie");
            }
        }
        /// <summary>
        /// 限制登录次数
        /// </summary>
        public static string LIMIT_LOGIN
        {
            get
            {
                return Auxiliary.ConfigKey("limit_login");
            }
        }
        /// <summary>
        /// 辅助COOKIE
        /// </summary>
        public static string COMMON_COOKIE
        {
            get
            {
                return Auxiliary.ConfigKey("common_cookie");
            }
        }


    }
}
