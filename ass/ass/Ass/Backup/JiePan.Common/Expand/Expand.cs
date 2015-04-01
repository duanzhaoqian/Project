using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.WebPages;
using JiePan.CMS.Common.UrlBox;
using JiePan.Common;
using JiePan.Common.UrlBox;
using JiePan.Common.Web;

namespace System.Web.Mvc
{
    public static class Expand
    {
        /// <summary>
        /// passporturl扩展
        /// </summary>
        /// <param name="helper"></param>
        /// <returns></returns>
        public static PassPortUrlBox PassPortUrl(this UrlHelper helper)
        {
            return PassPortUrlBox.Instance.SetContext(helper);
        }
        public static CmsPortUrlBox CmsPortUrlBox(this UrlHelper helper)
        {
            return JiePan.Common.UrlBox.CmsPortUrlBox.Instance.SetContext(helper);
        }
        public static AssUrlBox AssUrlBox(this UrlHelper helper)
        {
            return JiePan.Common.UrlBox.AssUrlBox.Instance.SetContext(helper);
        }
    }

    public class MyView<T> : WebViewPage<T>
    {
        public override void Execute()
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// ssss
        /// </summary>
        /// <param name="webViewPage"></param>
        /// <returns></returns>
        public static LoginUserInfo CurrentUserInfo
        {
            get
            {
                return LoginTool.GetLoginUserInfo();
            }
        }
    }
}
