using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KYJ.ZS.Models.LocalUsers
{
    /// <summary>
    /// Author:baiyan
    /// Time:2014-4-24
    /// Desc:个人用户基本信息
    /// </summary>
    public class LocalUserEntity
    {
        /// <summary>
        /// 主键不自增(公共平台用户ID) 
        /// </summary>
        public int UserId { get; set; }
        /// <summary>
        /// 登录名
        /// </summary>
        public string LoginName { get; set; }
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
        /// 状态：0 正常，1锁定(默认值0)
        /// </summary>
        public int State { get; set; }
        /// <summary>
        /// 用户等级：0 普通，1 高级(默认值0)
        /// </summary>
        public int Rank { get; set; }
        /// <summary>
        /// 用户积分(默认值0)
        /// </summary>
        public int Integral { get; set; }
        /// <summary>
        /// GUID唯一标识
        /// </summary>
        public string Guid { get; set; }
        /// <summary>
        /// 注册时间
        /// </summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime UpdateTime { get; set; }
        /// <summary>
        /// 用户最后登录时间
        /// </summary>
        public DateTime LastLoginTime { get; set; }
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
        /// <summary>
        /// 大学
        /// </summary>
        public string College { get; set; }
        /// <summary>
        /// 高中
        /// </summary>
        public string HighSchool { get; set; }
        /// <summary>
        /// 初中
        /// </summary>
        public string MiddleSchool { get; set; }
        /// <summary>
        /// 小学
        /// </summary>
        public string PrimarySchool { get; set; }
        /// <summary>
        /// 公司名称
        /// </summary>
        public string CompanyName { get; set; }
        /// <summary>
        /// 公司职位
        /// </summary>
        public string CompanyPosition { get; set; }
        /// <summary>
        /// 证件号码
        /// </summary>
        public string Papers { get; set; }
        /// <summary>
        /// 证件姓名
        /// </summary>
        public string PapersName { get; set; }
        /// <summary>
        /// 证件类型：0未知1身份证2护照
        /// </summary>
        public int PapersType { get; set; }
        /// <summary>
        /// 认证状态：0未知1未认证2认证中3认证未通过4认证通过
        /// </summary>
        public int PapersStatus { get; set; }
        /// <summary>
        /// 认证描述
        /// </summary>
        public string PapersRemark { get; set; }

    }
}
