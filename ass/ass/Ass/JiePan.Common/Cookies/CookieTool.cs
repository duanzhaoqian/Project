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
// Description:	
// Copyirght:	Copyright (C) 2014 - CCINN All rights reserved
// Solution:		JiePan.CMS
// Project:		JiePan.Common
// File:		CookieTool.cs
// CreateDate: 	2014-06-14 0:18
// ModifyDate:	2014-06-14 0:18
#endregion

using System;

namespace JiePan.Common.Cookies
{
    public class CookieTool
    {
        /// <summary>
        /// 添加登陆Cookie
        /// </summary>
        /// <param name="userData"></param>
        /// <param name="cookieName"></param>
        public static void SetLoginCookie(string userData, string cookieName, bool isautologin)
        {
            if (!string.IsNullOrEmpty(userData))
            {
                var time = DateTime.Now.AddHours(1);
                if (isautologin)
                    time = DateTime.Now.AddDays(30);
                CookieHelper.SetCookie(cookieName, userData, time);
            }
        }

        /// <summary>
        /// 添加Cookie
        /// </summary>
        /// <param name="userDate"></param>
        /// <param name="cookieName"></param>
        public static void SetCookie(string userData, string cookieName, DateTime? expireTime)
        {
            if (!string.IsNullOrEmpty(userData))
            {
                CookieHelper.SetCookie(cookieName, userData, expireTime);
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
            CookieHelper.DelCookie(cookieName);
        } 
    }
}