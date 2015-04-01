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
// Project:		JiePan.PassPort.Web
// File:		AutoFac.cs
// CreateDate: 	2014-06-18 21:44
// ModifyDate:	2014-06-18 21:44
#endregion

using System.Reflection;
using System.Web.Mvc;
using Autofac;
using Autofac.Configuration;

namespace System
{
    /// <summary>
    /// 依赖注入类
    /// </summary>
    public static class AutoFacTool
    {
        /// <summary>
        /// PassPort对象容器
        /// </summary>
        public static IContainer PassPortContainer { get; set; }
        /// <summary>
        /// Cms对象容器
        /// </summary>
        public static IContainer CmsContainer { get; set; }
        /// <summary>
        /// ass对象容器
        /// </summary>
        public static IContainer AssContainer { get; set; }

    }
        
}