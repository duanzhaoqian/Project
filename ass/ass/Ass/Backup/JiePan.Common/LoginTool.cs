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
// File:		LoginTool.cs
// CreateDate: 	2014-06-14 1:02
// ModifyDate:	2014-06-14 1:02
#endregion

using System;
using System.Web;
using JiePan.Common.Cookies;
using JiePan.Common.Web;

namespace JiePan.Common
{
    public class LoginTool
    {
        private static string ConfigKey
        {
            get { return ConfigHelper.GetConfig("JiePan.Ass.LoginInfo"); }
        }
        /// <summary>
        /// 添加登录信息
        /// </summary>
        /// <param name="guid"></param>
        /// <param name="loginUserInfo"></param>
        /// <param name="isRemeber"></param>
        /// <returns></returns>
        public static Boolean AddLoginInfo(string guid, LoginUserInfo loginUserInfo, Boolean isRemeber)
        {
            CookieTool.SetLoginCookie(Md5Helper.GetDoubleStringMd5Hash(guid), ConfigKey, isRemeber);
            HttpContext.Current.Session.Add(Md5Helper.GetDoubleStringMd5Hash(guid),loginUserInfo);
            TimeSpan timeSpan = new TimeSpan(1, 0, 0);
            if (isRemeber)
            {
                timeSpan = new TimeSpan(30, 0, 0, 0);
            }
            return true;
        }

        public static LoginUserInfo GetLoginUserInfo()
        {
            LoginUserInfo loginUserInfo =null;
            string key = CookieTool.GetCookie(ConfigHelper.GetConfig("JiePan.Ass.LoginInfo"));
            if (!string.IsNullOrEmpty(key))
            {
                loginUserInfo = HttpContext.Current.Session[key] as LoginUserInfo;
            }
            return loginUserInfo;
        }

    }
}