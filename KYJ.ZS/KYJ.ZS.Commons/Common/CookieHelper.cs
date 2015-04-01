using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace KYJ.ZS.Commons.Common
{
    /// <summary>
    /// Author:zhuzh
    /// Date:2014-05-04
    /// Desc:操作COOKIE的底层方法
    /// </summary>
    internal class CookieHelper
    {
        private static string CookieDomain = PubConstant.Domain;
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
                cookie.Expires = (DateTime)ExpireTime;
            }
            CookieValue = Auxiliary.Md5Encrypt(CookieValue);
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
                CookieValue = Auxiliary.Md5Decrypt(System.Web.HttpContext.Current.Request.Cookies[CookieName].Value);
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
            SetCookie(CookieName, "", DateTime.Now.AddYears(-1));
        }
        #endregion 
    }
}
