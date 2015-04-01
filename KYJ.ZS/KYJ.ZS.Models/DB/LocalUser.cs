using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KYJ.ZS.Models.DB
{
    /// <summary>
    /// Author:baiyan
    /// Time:2014-4-17
    /// Desc:用户实体类
    /// </summary>
    public class LocalUser
    {
        /// <summary>
        /// 主键 不自增
        /// </summary>
        public int UserID { get; set; }
        /// <summary>
        /// 本地用户登录名(和公共平台同步)
        /// </summary>
        public string LoginName { get; set; }
        /// <summary>
        /// 用户邮箱
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// 本地用户手机号码
        /// </summary>
        public string Mobile { get; set; }
        /// <summary>
        /// 本地用户登录密码
        /// </summary>
        public string Pwd { get; set; }
        /// <summary>
        /// 支付密码
        /// </summary>
        public string PayPwd { get; set; }
        /// <summary>
        /// 用户真实姓名
        /// </summary>
        public string RealName { get; set; }
        /// <summary>
        /// 用户昵称
        /// </summary>
        public string NickName { get; set; }
        /// <summary>
        /// 性别：0 保密，1 男，2 女
        /// </summary>
        public int Sex { get; set; }
        /// <summary>
        /// 用户生日
        /// </summary>
        public DateTime Birthday { get; set; }
        /// <summary>
        /// 用户身份证号
        /// </summary>
        public string IdCard { get; set; }
        /// <summary>
        /// 用户状态 0正常 1锁定 (默认值0)
        /// </summary>
        public int State { get; set; }
        /// <summary>
        /// 用户等级 0普通 1高级 (默认值0)
        /// </summary>
        public int Rank { get; set; }
        /// <summary>
        /// 用户积分(默认值0)
        /// </summary>
        public int InteGral { get; set; }
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
        /// 最后登录时间
        /// </summary>
        public DateTime LastLoginTime { get; set; }
        /// <summary>
        /// 是否删除
        /// </summary>
        public bool IsDel { get; set; }
    }
}
