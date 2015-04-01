using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace KYJ.ZS.Models.LocalUsers
{
    /// <summary>
    /// Author:zhuzh
    /// Date  :2014-05-29
    /// Desc  :用户注册实体
    /// </summary>
    public class RegisterUserEntity
    {
        /// <summary>
        /// 登录名/手机号码
        /// </summary>
        [Required(ErrorMessage = "请填写正确信息！")]
        [RegularExpression(@"(((13[0-9]{1})|(15[0-9]{1})|(18[0-9]{1})|(14[0-9]{1}))+\d{8})", ErrorMessage = "请填写正确的手机号码！")]
        //[RegularExpression(@"(13944444444)", ErrorMessage = "请填写正确的手机号码！")]
        public string LoginName { get; set; }
        /// <summary>
        /// 验证码
        /// </summary>
        [Required(ErrorMessage = "请填写正确信息！")]
        public string VerificationCode { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        [Required(ErrorMessage = "密码格式不正确！")]
        [DataType(DataType.Password)]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "密码必须在{2} 和{1}之间!")]
        [RegularExpression(@"(\w){6,20}", ErrorMessage = "请输入6-20位英文字母、下划线或数字")]
        public string Password { get; set; }
        /// <summary>
        /// 确认密码
        /// </summary>
        [Required(ErrorMessage = "确认密码与密码不一致！")]
        [DataType(DataType.Password)]
        [System.Web.Mvc.Compare("Password", ErrorMessage = "密码和确认密码不匹配!")]
        public string RePassword { get; set; }
    }

    /// <summary>
    /// Author:zhuzh
    /// Date  :2014-05-29
    /// Desc  :注册操作枚举
    /// </summary>
    public enum RegisterType
    {
        /// <summary>
        /// 成功，公共平台和本地添加帐号成功
        /// </summary>
        SUCCESS,
        /// <summary>
        /// 通过，公共平台添加成功，本地未添加成功（不妨碍登录）
        /// </summary>
        PASS,
        /// <summary>
        /// 失败，公共平台、本地添加失败
        /// </summary>
        FAILURE
    }

}
