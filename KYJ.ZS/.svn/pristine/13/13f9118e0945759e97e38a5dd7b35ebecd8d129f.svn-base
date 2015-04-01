using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace KYJ.ZS.Models.LocalUsers
{
    /// <summary>
    /// Author:zhuzh
    /// Date:2014-05-09
    /// Desc:用户后台基础资料实体
    /// </summary>
    public class UserInfoEntity
    {
        /// <summary>
        /// 主键不自增(公共平台用户ID) 
        /// </summary>
        public int UserId { get; set; }
        /// <summary>
        /// 邮箱
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// 手机号码
        /// </summary>
        public string Mobile { get; set; }
        /// <summary>
        /// 真实姓名
        /// </summary>
        public string RealName { get; set; }
        /// <summary>
        /// 用户昵称
        /// </summary>
        public string NickName { get; set; }
        /// <summary>
        /// 性别：0保密 1男 2女
        /// </summary>
        public int Sex { get; set; }
        /// <summary>
        /// 生日年份
        /// </summary>
        public string BirthdayYear { get; set; }
        /// <summary>
        /// 生日月份
        /// </summary>
        public string BirthdayMonth { get; set; }
        /// <summary>
        /// 生日天数
        /// </summary>
        public string BirthdayDay { get; set; }
        /// <summary>
        /// 星座
        /// </summary>
        public int Constellation { get; set; }
        /// <summary>
        /// 居住省份ID
        /// </summary>
        public int LiveProvinceId { get; set; }
        /// <summary>
        /// 居住省份名称
        /// </summary>
        public string LiveProvinceName { get; set; }
        /// <summary>
        /// 居住城市ID
        /// </summary>
        public int LiveCityId { get; set; }
        /// <summary>
        /// 居住城市名称
        /// </summary>
        public string LiveCityName { get; set; }
        /// <summary>
        /// 居住区域ID
        /// </summary>
        public int LiveDistrictId { get; set; }
        /// <summary>
        /// 居住区域名称
        /// </summary>
        public string LiveDistrictName { get; set; }
        /// <summary>
        /// 家乡省份ID
        /// </summary>
        public int HomeProvinceId { get; set; }
        /// <summary>
        /// 家乡省份名称
        /// </summary>
        public string HomeProvinceName { get; set; }
        /// <summary>
        /// 家乡城市ID
        /// </summary>
        public int HomeCityId { get; set; }
        /// <summary>
        /// 家乡城市名称
        /// </summary>
        public string HomeCityName { get; set; }
        /// <summary>
        /// 家乡区域ID
        /// </summary>
        public int HomeDistrictId { get; set; }
        /// <summary>
        /// 家乡区域名称
        /// </summary>
        public string HomeDistrictName { get; set; }
    }
    /// <summary>
    /// Author:zhuzh
    /// Date:2014-05-09
    /// Desc:用户后台教育情况数据库实体
    /// </summary>
    public class UserEducationEntity
    {
        /// <summary>
        /// 主键不自增(公共平台用户ID) 
        /// </summary>
        public int UserId { get; set; }
        /// <summary>
        /// 大学
        /// </summary>
        [Required(ErrorMessage = "大学不能为空")]
        public string College { get; set; }
        /// <summary>
        /// 高中
        /// </summary>
        [Required(ErrorMessage = "高中不能为空")]
        public string HighSchool { get; set; }
        /// <summary>
        /// 初中
        /// </summary>
        [Required(ErrorMessage = "初中不能为空")]
        public string MiddleSchool { get; set; }
        /// <summary>
        /// 小学
        /// </summary>
        [Required(ErrorMessage = "小学不能为空")]
        public string PrimarySchool { get; set; }
    }
    /// <summary>
    /// Author:zhuzh
    /// Date:2014-05-14
    /// Desc:用户后台修改密码数据实体
    /// </summary>
    public class UserPasswordEntity
    {
        public int UserId { get; set; }

        /// <summary>
        /// 原密码
        /// </summary>
        public string OldPassword { get; set; }

        /// <summary>
        /// 新密码
        /// </summary>
        [Required]
        [DataType(DataType.Password)]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "密码必须在{2} 和{1}之间")]
        public string NewPassword { get; set; }

        /// <summary>
        /// 确认密码
        /// </summary>
        [Required]
        [DataType(DataType.Password)]
        [System.Web.Mvc.Compare("NewPassword", ErrorMessage = "密码和确认密码不匹配。")]
        public string RePassword { get; set; }
    }
}
