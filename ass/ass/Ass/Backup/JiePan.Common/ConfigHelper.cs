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
// File:		ConfigHelper.cs
// CreateDate: 	2014-06-14 1:06
// ModifyDate:	2014-06-14 1:06
#endregion

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Web.Configuration;

namespace JiePan.Common
{
    public class ConfigHelper
    {

        public static string GetConfig(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }

        #region 设置参数 老树添加
        /// <summary>设置配置文件参数</summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name"></param>
        /// <param name="defaultValue"></param>
        public static void SetConfig<T>(String name, T defaultValue)
        {
            //SetConfig(name, defaultValue.ToString());
            // 小心空引用
            SetConfig(name, "" + defaultValue);
        }
        /// <summary>
        /// 判断appSettings中是否有此项
        /// </summary>
        private static bool AppSettingsKeyExists(string strKey, System.Configuration.Configuration config)
        {
            foreach (string str in config.AppSettings.Settings.AllKeys)
            {
                if (str == strKey) return true;
            }
            return false;
        }
        /// <summary>设置配置文件参数</summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        public static void UpdateConfig(String name, String value)
        {
            var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            if (AppSettingsKeyExists(name, config))
            {
                config.AppSettings.Settings[name].Value = value;
                config.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection("appSettings");
            }
        }
        #endregion 
    }
}