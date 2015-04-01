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
// File:		LoginUserInfo.cs
// CreateDate: 	2014-06-14 0:57
// ModifyDate:	2014-06-14 0:57
#endregion
namespace JiePan.Common.Web
{
    public class LoginUserInfo
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string PassWord { get; set; }
        public string CreateIp { get; set; }
        public System.DateTime CreateDateTime { get; set; }
        public string LastIp { get; set; }
        public System.DateTime LastDatetime { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }
        public string Email { get; set; }
        public bool EmailValidated { get; set; }
        public string Guid { get; set; }
    }
}