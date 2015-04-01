#region head
//  ┏┓　　　┏┓
// ┏┛┻━━━┛┻┓
// ┃　　　　　　　┃ 　
// ┃　　　━　　　┃
// ┃　┳┛　┗┳　┃
// ┃　　　　　　　┃
// ┃　　　┻　　　┃
// ┃　　　　　　　┃
// ┗━┓　　　┏━┛
//     ┃　　　┃   神兽保佑　　　　　　　　　
//     ┃　　　┃   代码无BUG！
//     ┃　　　┗━━━┓
//     ┃　　　　　　　┣┓
//     ┃　　　　　　　┏┛
//     ┗┓┓┏━┳┓┏┛
//       ┃┫┫　┃┫┫
//       ┗┻┛　┗┻┛
// 
// 
// Author:		lianjiepan
// Blog:		blog.jiepansoft.com
// Description:	cookie 操作助手类
// Copyirght:	Copyright (C) 2014 - CCINN All rights reserved
// Solution:		JiePan.CMS
// Project:		JiePan.Common
// File:		CookieHelper.cs
// CreateDate: 	2014-06-14 0:14
// ModifyDate:	2014-06-14 0:14
#endregion

using System;
using System.Web;

namespace JiePan.Common.Cookies
{
    internal class CookieHelper
    {
        #region 操作Cookie
        /// <summary>
        /// 设置COOKIE
        /// </summary>
        /// <param name="cookieName"></param>
        /// <param name="cookieValue"></param>
        public static void SetCookie(string cookieName, string cookieValue)
        {
            SetCookie(cookieName, cookieValue, null);
        }
        /// <summary>
        /// 设置COOKIE
        /// </summary>
        /// <param name="cookieName"></param>
        /// <param name="cookieValue"></param>
        /// <param name="expireTime"></param>
        public static void SetCookie(string cookieName, string cookieValue, DateTime? expireTime)
        {
            HttpCookie cookie = new HttpCookie(cookieName);
            if (expireTime != null)
            {
                cookie.Expires = (DateTime)expireTime;
            }
            cookie.Value = cookieValue;
            System.Web.HttpContext.Current.Response.Cookies.Add(cookie);
        }
        /// <summary>
        /// 读取COOKIE
        /// </summary>
        /// <param name="cookieName"></param>
        /// <returns></returns>
        public static string GetCookie(string cookieName)
        {
            string cookieValue = string.Empty;
            HttpCookie cookie = HttpContext.Current.Request.Cookies[cookieName];
            if (cookie != null)
            {
                cookieValue = cookie.Value;
            }
            return cookieValue;
        }
        /// <summary>
        /// </summary>
        /// <param name="cookieName"></param>
        public static void DelCookie(string cookieName)
        {
            SetCookie(cookieName, "", DateTime.Now.AddYears(-1));
        }
        #endregion
    }
}