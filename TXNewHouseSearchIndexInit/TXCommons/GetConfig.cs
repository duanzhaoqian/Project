using System.Text;

namespace TXCommons
{
    public class GetConfig
    {
        #region Img\Css\Js

        /// <summary>
        /// 获得CSS/JS路径
        /// </summary>
        /// <param name="fileName">文件名称</param>
        /// <returns></returns>
        public static string GetFileUrl(string fileName)
        {
            StringBuilder str = new StringBuilder();
            // js/css文件根路径
            str.Append(PubConstant.GetConfigString("static.fileurl"));
            str.Append(fileName);
            str.Append("?v=");
            // js/css文件版本号
            str.Append(CurrVer);
            return str.ToString();
        }

        /// <summary>
        /// 获得CSS/JS路径
        /// author: liyuzhao
        /// </summary>
        /// <param name="fileName">文件名称</param>
        /// <param name="configKeyName">web.config中的key名称</param>
        /// <returns></returns>
        public static string GetFileUrl(string fileName, string configKeyName)
        {
            var str = new StringBuilder();
            // js/css文件根路径
            str.Append(PubConstant.GetConfigString(configKeyName));
            str.Append(fileName);
            str.Append("?v=");
            // js/css文件版本号
            str.Append(CurrVer);
            return str.ToString();
        }

        /// <summary>
        /// 获得IMG路径
        /// </summary>
        /// <returns></returns>
        public static string ImgUrl
        {
            get { return PubConstant.GetConfigString("static.imgurl"); }
        }

        /// <summary>
        /// 获取当前JS/CSS版本
        /// </summary>
        /// <returns></returns>
        public static string CurrVer
        {
            get { return PubConstant.GetConfigString("static.version"); }
        }

        #endregion

        #region 域名\域

        /// <summary>
        /// 当前平台域
        /// </summary>
        public static string Domain
        {
            get { return PubConstant.GetConfigString("domain"); }
        }

        /// <summary>
        /// 当前平台域名
        /// </summary>
        public static string BaseUrl
        {
            get
            {
                System.Web.HttpRequest Request = System.Web.HttpContext.Current.Request;
                string url = Request.Url.ToString();
                string cityPinyin = url.Replace("http://", "").Split('.')[0].ToLower();
                if (cityPinyin != "nhdevelopers" && cityPinyin != "dev" && cityPinyin != "manager" && cityPinyin != "nhmanager")
                    return "http://" + cityPinyin + PubConstant.GetConfigString("baseUrl");
                else
                    return PubConstant.GetConfigString("baseUrl");
            }
        }


        /// <summary>
        /// 前台域名
        /// </summary>
        public static string QTBaseUrl
        {
            get
            {
                System.Web.HttpRequest Request = System.Web.HttpContext.Current.Request;
                string url = Request.Url.ToString();
                string cityPinyin = url.Replace("http://", "").Split('.')[0].ToLower();
                if (cityPinyin != "nhdevelopers" && cityPinyin != "dev" && cityPinyin != "manager" && cityPinyin != "nhmanager")
                    return "http://" + cityPinyin + PubConstant.GetConfigString("baseUrl");
                else
                    return PubConstant.GetConfigString("baseUrl");
            }
        }

        /// <summary>
        /// 开发商后台域名
        /// </summary>
        public static string DevBaseUrl
        {
            get
            {
                return PubConstant.GetConfigString("baseUrl");
            }
        }

        /// <summary>
        /// 管理后台域名
        /// </summary>
        public static string ManageBaseUrl
        {
            get
            {
                return PubConstant.GetConfigString("baseUrl");
            }
        }
        #endregion

        #region 组件配置

        public static string SMSWebUrl
        {
            get { return PubConstant.GetConfigString("sms.baseUrl"); }
        }

        /// <summary>
        /// Email的Smtp连接地址
        /// </summary>
        public static string MailSmtp
        {
            get { return PubConstant.GetConfigString("MailSmtp"); }
        }

        public static string MQIPAddress
        {
            get { return PubConstant.GetConfigString("MQIPAddress"); }
        }

        public static string MQConnectionTimeOut
        {
            get { return PubConstant.GetConfigString("MQConnectionTimeOut"); }
        }

        public static string MQRetryCount
        {
            get { return PubConstant.GetConfigString("MQRetryCount"); }
        }

        public static string MQPremisesPicQueueName
        {
            get { return PubConstant.GetConfigString("MQPremisesPicQueueName"); }
        }

        public static string PremisesImgUploadBaseUrl
        {
            get { return PubConstant.GetConfigString("premisesimgupload.baseUrl"); }
        }
        public static string MQUserInfoCallQueueName { get { return PubConstant.GetConfigString("MQCallInfoQueueName"); } }
        /// <summary>
        /// 获取支付宝合作身份者ID
        /// </summary>
        /// <returns></returns>
        public static string GetAlipartner
        {
            get { return PubConstant.GetConfigString("Alipartner"); }
        }

        /// <summary>
        /// 获取支付宝key
        /// </summary>
        /// <returns></returns>
        public static string GetAlikey
        {
            get { return PubConstant.GetConfigString("Alikey"); }
        }

        #endregion

        #region 删除的代码

        ///// <summary>
        ///// kyj_HouseDBName【即将删除禁止使用】
        ///// </summary>
        //public static string Kyj_HouseDBName
        //{
        //    get { return PubConstant.GetConfigString("kyj_HouseDB"); }
        //}
        ///// <summary>
        ///// Kyj_WebDBName【即将删除禁止使用】
        ///// </summary>
        //public static string Kyj_WebDBName
        //{
        //    get { return PubConstant.GetConfigString("kyj_WebDB"); }
        //}


        ///// <summary>
        ///// 获取JS全路径【即将删除禁止使用】
        ///// </summary>
        ///// <param name="filePath"></param>
        ///// <returns></returns>
        //public static string GetJSPath(string filePath)
        //{
        //    return PubConstant.GetConfigString("static.baseUrl") + "/" + "js/";
        //}

        ///// <summary>
        ///// 获取CSS全路径【即将删除禁止使用】
        ///// </summary>
        ///// <param name="filePath"></param>
        ///// <returns></returns>
        //public static string GetCSSPath(string filePath)
        //{
        //    return PubConstant.GetConfigString("static.baseUrl") + "/" + "css/";
        //}

        ///// <summary>
        ///// 获取图片全路径【即将删除禁止使用】
        ///// </summary>
        ///// <param name="filePath"></param>
        ///// <returns></returns>
        //public static string GetImagesPath(string filePath)
        //{
        //    return PubConstant.GetConfigString("static.baseUrl") + "/" + "images/";
        //}

        /////<summary>
        /////房源列表页 每页显示条数 【即将删除禁止使用】
        /////Author:huangjihua 
        /////</summary>
        //public static int PageSize
        //{
        //    get { return PubConstant.ToInt32(PubConstant.GetConfigString("pageSize")); }
        //}

        #endregion

        #region 新房前台

        /// <summary>
        /// 取平台地址
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static string GetQTBaseUrl
        {
            get
            {
                System.Web.HttpRequest Request = System.Web.HttpContext.Current.Request;
                string url = Request.Url.ToString();
                string cityPinyin = url.Replace("http://", "").Split('.')[0].ToLower();
                if (cityPinyin != "nhdevelopers" && cityPinyin != "dev" && cityPinyin != "manager" && cityPinyin != "nhmanager")
                    return "http://" + cityPinyin + PubConstant.GetConfigString("baseUrl");
                else
                    return PubConstant.GetConfigString("baseUrl");
            }
        }

        /// <summary>
        /// 取得新房前台站点URL
        /// </summary>
        public static string GetSiteUrl
        {
            get { return PubConstant.GetConfigString("baseUrl.SiteUrl"); }
        }

        /// <summary>
        /// 取二手房地址
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static string GetEsfBaseUrl
        {
            get
            {
                System.Web.HttpRequest Request = System.Web.HttpContext.Current.Request;
                string url = Request.Url.ToString();
                string cityPinyin = url.Replace("http://", "").Split('.')[0].ToLower();
                return "http://" + cityPinyin + PubConstant.GetConfigString("esfUrl");
            }
        }

        /// <summary>
        /// 取租房地址
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static string GetZufangBaseUrl
        {
            get
            {
                System.Web.HttpRequest Request = System.Web.HttpContext.Current.Request;
                string url = Request.Url.ToString();
                string cityPinyin = url.Replace("http://", "").Split('.')[0].ToLower();
                return "http://" + cityPinyin + PubConstant.GetConfigString("zufangUrl");
            }
        }

        /// <summary>
        /// 取快来看地址
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static string GetKlkBaseUrl
        {
            get
            {
                System.Web.HttpRequest Request = System.Web.HttpContext.Current.Request;
                string url = Request.Url.ToString();
                string cityPinyin = url.Replace("http://", "").Split('.')[0].ToLower();
                return "http://" + cityPinyin + PubConstant.GetConfigString("klkUrl");
            }
        }

        /// <summary>
        /// 取快有贷地址
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static string GetKydBaseUrl
        {
            get
            {
                System.Web.HttpRequest Request = System.Web.HttpContext.Current.Request;
                string url = Request.Url.ToString();
                string cityPinyin = url.Replace("http://", "").Split('.')[0].ToLower();
                return "http://" + cityPinyin + PubConstant.GetConfigString("kydUrl");
            }
        }

        /// <summary>
        /// 取搜索联想接口地址
        /// </summary>
        public static string GetAutoSearchUrl
        {
            get
            {
                return PubConstant.GetConfigString("autoSearchUrl");
            }
        }

        /// <summary>
        /// 取平台服务地址
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static string GetPtfwBaseUrl
        {
            get
            {
                return PubConstant.GetConfigString("ptfwUrl");
            }
        }

        /// <summary>
        /// 取新房前台站点目录
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static string GetSiteRoot
        {
            get
            {
                return PubConstant.GetConfigString("siteRoot");
            }
        }
        /// <summary>
        /// 取新房前台站点目录
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static string GetSiteRootXF
        {
            get
            {
                return string.Format("{0}{1}/quyu", PubConstant.GetConfigString("baseUrl.SiteUrl"), PubConstant.GetConfigString("siteRoot"));
            }
        }
        /// <summary>
        /// 取新房前台站点目录帮助中心地址
        /// </summary>
        public static string GetXFhelpCenterUrl
        {
            get
            {
                return string.Format("{0}{1}/about/view1", PubConstant.GetConfigString("baseUrl.SiteUrl"), PubConstant.GetConfigString("siteRoot"));
            }
        }

        /// <summary>
        /// 取登陆Cookie名
        /// </summary>
        public static string PrivateCookie
        {
            get
            {
                return PubConstant.GetConfigString("privatecookie");
            }
        }

        /// <summary>
        /// 取区域 商圈 地铁 站点地址
        /// </summary>
        public static string SearchUrl
        {
            get
            {
                return PubConstant.GetConfigString("search.baseUrl");
            }
        }

        /// <summary>
        /// 秒杀操作处理时间（单位：分钟）
        /// </summary>
        public static int MSOperatingTime
        {
            get
            {
                return Util.ToInt(PubConstant.GetConfigString("MSOperatingTime"));
            }
        }

        /// <summary>
        /// 用户中心地址
        /// </summary>
        public static string UserCenterUrl
        {
            get
            {
                System.Web.HttpRequest Request = System.Web.HttpContext.Current.Request;
                string url = Request.Url.ToString();
                string cityPinyin = url.Replace("http://", "").Split('.')[0].ToLower();
                return "http://" + cityPinyin + PubConstant.GetConfigString("userCenterUrl");
            }
        }

        /// <summary>
        /// 金点子地址
        /// </summary>
        public static string JdzUrl
        {
            get
            {
                System.Web.HttpRequest Request = System.Web.HttpContext.Current.Request;
                string url = Request.Url.ToString();
                string cityPinyin = url.Replace("http://", "").Split('.')[0].ToLower();
                return "http://" + cityPinyin + PubConstant.GetConfigString("userCenterUrl") + "Comment/Message";
            }
        }

        /// <summary>
        /// 开发商地址
        /// </summary>
        public static string DevelopersUrl
        {
            get
            {
                return PubConstant.GetConfigString("developersUrl");
            }
        }

        /// <summary>
        /// 帮助中心地址
        /// </summary>
        public static string HelpCenterUrl
        {
            get
            {
                System.Web.HttpRequest Request = System.Web.HttpContext.Current.Request;
                string url = Request.Url.ToString();
                string cityPinyin = url.Replace("http://", "").Split('.')[0].ToLower();
                return "http://" + cityPinyin + PubConstant.GetConfigString("helpCenterUrl");
            }
        }

        /// <summary>
        /// 委托服务地址
        /// </summary>
        public static string EntrustUrl
        {
            get
            {
                return PubConstant.GetConfigString("entrustUrl");
            }
        }

        /// <summary>
        /// 登陆地址
        /// </summary>
        public static string LoginUrl
        {
            get
            {
                System.Web.HttpRequest Request = System.Web.HttpContext.Current.Request;
                string url = Request.Url.ToString();
                string cityPinyin = url.Replace("http://", "").Split('.')[0].ToLower();
                return "http://" + cityPinyin + PubConstant.GetConfigString("loginUrl");
            }
        }

        /// <summary>
        /// 注册地址
        /// </summary>
        public static string RegisterUrl
        {
            get
            {
                System.Web.HttpRequest Request = System.Web.HttpContext.Current.Request;
                string url = Request.Url.ToString();
                string cityPinyin = url.Replace("http://", "").Split('.')[0].ToLower();
                return "http://" + cityPinyin + PubConstant.GetConfigString("registerUrl");
            }
        }

        #endregion


    }
}