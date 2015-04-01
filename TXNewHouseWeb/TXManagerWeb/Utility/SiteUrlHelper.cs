using System;
using System.Web.Mvc;
using TXManagerWeb.Common;

namespace TXManagerWeb.Utility
{
    public partial class SiteUrlHelper
    {
        // ReSharper disable once InconsistentNaming
        private static readonly SiteUrlHelper instance = new SiteUrlHelper();

        internal static SiteUrlHelper Instance
        {
            get { return instance; }
        }

        private SiteUrlHelper()
        {
        }

        internal UrlHelper UrlHelper { get; private set; }

        internal SiteUrlHelper SetContext(UrlHelper context)
        {
            if (context == null)
                throw new ArgumentNullException("context");
            UrlHelper = context;
            return this;
        }

        #region Common-URL-Route

        /// <summary>
        /// 登录
        /// </summary>
        public string Login
        {
            get { return Auxiliary.Instance.ManagerUrl; }
        }

        public string Common(string action, string controller)
        {
            return UrlHelper.Action(action, controller);
        }

        public string Index
        {
            get { return UrlHelper.Action("index", "common"); }
        }

        /// <summary>
        /// 公用管理
        /// </summary>
        public string PIndex
        {
            get { return UrlHelper.Action("pindex", "common"); }
        }

        /// <summary>
        /// 资讯管理
        /// </summary>
        public string CIndex
        {
            get { return this.UrlHelper.Action("cindex", "common"); }
        }

        /// <summary>
        /// 经纪人
        /// </summary>
        public string BIndex
        {
            get { return UrlHelper.Action("bindex", "common"); }
        }

        /// <summary>
        /// 经济公司
        /// </summary>
        public string BCIndex
        {
            get { return UrlHelper.Action("bcindex", "common"); }
        }

        /// <summary>
        /// 房产客服管理
        /// </summary>
        public string RIndex
        {
            get { return UrlHelper.Action("rindex", "common"); }
        }

        /// <summary>
        /// 财务管理
        /// </summary>
        public string FIndex
        {
            get { return UrlHelper.Action("findex", "common"); }
        }

        /// <summary>
        /// 新房管理
        /// </summary>
        public string NHouseIndex
        {
            get { return UrlHelper.Action("nhouseindex", "common"); }
        }

        /// <summary>
        /// 退出
        /// </summary>
        public string SignOut
        {
            get { return UrlHelper.Action("signout", "common"); }
        }

        /// <summary>
        /// 下拉框联动
        /// </summary>
        /// <param name="actionName"></param>
        /// <returns></returns>
        public string GeographyLocation(string actionName)
        {
            return UrlHelper.Action(actionName, "geography");
        }

        #endregion
    }
}