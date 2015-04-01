using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace JiePan.Common.UrlBox
{
    public class CmsPortUrlBox
    {

        #region 扩展
        private static readonly CmsPortUrlBox instance = new CmsPortUrlBox();

        private static string BaseUrl;
        internal static CmsPortUrlBox Instance
        {
            get
            {
                return instance;
            }
        }

        private CmsPortUrlBox()
        {
        }

        internal UrlHelper UrlHelper { get; private set; }

        internal CmsPortUrlBox SetContext(UrlHelper context)
        {
            if (context == null)
                throw new ArgumentNullException("context");
            this.UrlHelper = context;
            return this;
        }

        #endregion
    }
}
