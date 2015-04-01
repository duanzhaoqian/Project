using System.Web.Mvc;

namespace TXManagerWeb.Utility
{
    public static class UtilityExtension
    {
        public static SiteUrlHelper SiteUrl(this UrlHelper helper)
        {
            return SiteUrlHelper.Instance.SetContext(helper);
        }
    }
}