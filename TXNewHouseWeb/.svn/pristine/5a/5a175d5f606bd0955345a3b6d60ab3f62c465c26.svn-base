using System;
using System.Web;
using TXCommons.ForKuaiYouJiaCookie;

namespace TXCommons
{
    public class CookieHelper
    {
        private static string CookieDomain = GetConfig.Domain;
        #region 操作Cookie
        /// <summary>
        /// 设置加密过的COOKIE
        /// by:douhaichao
        /// date:2013年11月12日 11:09:16
        /// </summary>
        /// <param name="CookieName"></param>
        /// <param name="CookieValue"></param>
        public static void SetCookie(string CookieName, string CookieValue)
        {
            SetCookie(CookieName, CookieValue, null);
        }
        /// <summary>
        /// 设置加密过的COOKIE
        /// by:douhaichao
        /// date:2013年11月12日 11:09:16
        /// </summary>
        /// <param name="CookieName"></param>
        /// <param name="CookieValue"></param>
        /// <param name="ExpireTime"></param>
        public static void SetCookie(string CookieName, string CookieValue, DateTime? ExpireTime)
        {
            HttpCookie cookie = new HttpCookie(CookieName);
            cookie.Domain = CookieDomain;
            if (ExpireTime != null)
            {
                System.Web.HttpContext.Current.Response.Cookies[CookieName].Expires = (DateTime)ExpireTime;
            }
            CookieValue = BaseRC4.RC4Encrypt(CookieValue);
            cookie.Value = CookieValue;
            System.Web.HttpContext.Current.Response.Cookies.Add(cookie);
            //SetCookie(CookieName, CookieValue);
        }
        /// <summary>
        /// 读取加密过的COOKIE
        /// by:douhaichao
        /// date:2013年11月12日 11:09:16
        /// </summary>
        /// <param name="CookieName"></param>
        /// <returns></returns>
        public static string GetCookie(string CookieName)
        {
            string CookieValue = string.Empty;
            HttpCookie cookie = HttpContext.Current.Request.Cookies[CookieName];
            if (cookie != null)
            {
                cookie.Domain = CookieDomain;
                CookieValue = BaseRC4.RC4Decrypt(System.Web.HttpContext.Current.Request.Cookies[CookieName].Value);
            }
            return CookieValue;
        }
        /// <summary>
        /// 删除COOKIE
        /// by:douhaichao
        /// date:2013年11月12日 11:09:16
        /// </summary>
        /// <param name="CookieName"></param>
        public static void DelCookie(string CookieName)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies[CookieName];
            //删除正常Cookie
            if (cookie != null)
            {
                cookie.Values.Remove(CookieName);
                cookie.Expires = DateTime.Now.AddDays(-20);
                cookie.Domain = CookieDomain;
                HttpContext.Current.Response.AppendCookie(cookie);
            }
        }
        #endregion 
    }
}
