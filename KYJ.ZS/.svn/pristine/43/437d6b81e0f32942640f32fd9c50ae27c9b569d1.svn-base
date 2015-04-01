using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace KYJ.ZS.Commons.Common
{
    /// <summary>
    /// Author:zhuzh
    /// Date:2014-4-25
    /// Desc:COOKIE存储，删除等操作集中营
    /// </summary>
    public class CookieTool
    {
        private static string _CookieDomain = PubConstant.Domain;

        /// <summary>
        /// 添加登陆Cookie
        /// </summary>
        /// <param name="userDate"></param>
        /// <param name="cookieName"></param>
        public static void SetLoginCookie(string userDate, string cookieName, bool isautologin)
        {
            if (!string.IsNullOrEmpty(userDate))
            {
                var time = DateTime.Now.AddHours(1);
                if (isautologin)
                    time = DateTime.Now.AddDays(30);
                CookieHelper.SetCookie(cookieName, userDate, time);
            }
        }

        /// <summary>
        /// 添加Cookie
        /// </summary>
        /// <param name="userDate"></param>
        /// <param name="cookieName"></param>
        public static void SetCookie(string userDate, string cookieName, DateTime? ExpireTime)
        {
            if (!string.IsNullOrEmpty(userDate))
            {
                CookieHelper.SetCookie(cookieName, userDate, ExpireTime);
            }
        }
        /// <summary>
        /// 获取指定CookieName的值
        /// </summary>
        /// <param name="cookieName">CookieName</param>
        /// <returns>指定Cookie的值</returns>
        public static string GetCookie(string cookieName)
        {
            if (!string.IsNullOrEmpty(cookieName))
            {
                return CookieHelper.GetCookie(cookieName);
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// 删除登陆Cookie
        /// </summary>
        /// <param name="cookieName"></param>
        public static void RemoveCookie(string cookieName)
        {
            //删除正常Cookie
            CookieHelper.DelCookie(cookieName);
        }

    }
}
