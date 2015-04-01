using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace JiePan.Common.UrlBox
{
    public class AssUrlBox
    {
        #region 扩展

        private static readonly AssUrlBox instance = new AssUrlBox();

        private static string BaseUrl;
        internal static AssUrlBox Instance
        {
            get
            {
                return instance;
            }
        }

        private AssUrlBox()
        {
        }

        internal UrlHelper UrlHelper { get; private set; }

        internal AssUrlBox SetContext(UrlHelper context)
        {
            if (context == null)
                throw new ArgumentNullException("context");
            this.UrlHelper = context;
            return this;
        } 
        #endregion
    }
}
