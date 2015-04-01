using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace JiePan.CMS.Common.UrlBox
{
    public  partial  class PassPortUrlBox
    {
        #region 扩展
        private static readonly PassPortUrlBox instance = new PassPortUrlBox();

        private static string BaseUrl;
        internal static PassPortUrlBox Instance
        {
            get
            {
                return instance;
            }
        }

        private PassPortUrlBox()
        {
        }

        internal UrlHelper UrlHelper { get; private set; }

        internal PassPortUrlBox SetContext(UrlHelper context)
        {
            if (context == null)
                throw new ArgumentNullException("context");
            this.UrlHelper = context;
            return this;
        }

        #endregion
        /// <summary>
        /// 添加工资单
        /// </summary>
        public  string Salary_Add
        {
            get { return this.UrlHelper.Action("add", "salary"); }
        }
        /// <summary>
        /// 工资单列表
        /// </summary>
        public string Salary_Index
        {
            get { return this.UrlHelper.Action("index", "salary"); }
        }
        /// <summary>
        /// passport登录
        /// </summary>
        public string Login_Login
        {
            get { return this.UrlHelper.Action("login", "login"); }
        }

        public string Log_Index
        {
            get { return this.UrlHelper.Action("index", "log"); }
        }

        public string MyInfo_Index {
            get { return this.UrlHelper.Action("index", "myinfo"); }
        }
        public string MyInfo_Add
        {
            get { return this.UrlHelper.Action("add", "myinfo"); }
        }
    }
}
