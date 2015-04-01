using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KYJ.ZS.Commons.Common
{
    /// <summary>
    /// Author:zhuzh
    /// Time:2014-4-17
    /// Desc:登录信息数据实体类
    /// </summary>
    public class LoginUserInfo
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public int UserID { get; set; }

        /// <summary>
        /// 登录名
        /// </summary>
        public string LoginName { get; set; }

        /// <summary>
        /// 用户邮箱
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// 用户手机号码
        /// </summary>
        public string Mobile { get; set; }

        /// <summary>
        /// 用户真实姓名
        /// </summary>
        public string RealName { get; set; }

        /// <summary>
        /// 用户昵称
        /// </summary>
        public string NickName { get; set; }

        /// <summary>
        /// 用户等级：0 普通，1 高级
        /// </summary>
        public int Rank { get; set; }

        /// <summary>
        /// 用户积分
        /// </summary>
        public int Integral { get; set; }

        /// <summary>
        /// GUID唯一标识
        /// </summary>
        public string _Guid { get; set; }
        /// <summary>
        /// 最后登录时间
        /// </summary>
        public string LastLoginTime { get; set; }
    }

    public enum UserInfoType
    {
        /// <summary>
        /// 前台、用户后台
        /// </summary>
        USERLOGIN,
        /// <summary>
        /// 商户后台
        /// </summary>
        MERCHANTLOGIN,
        /// <summary>
        /// 管理后台
        /// </summary>
        MANAGERLOGIN
    }
    /// <summary>
    /// Author:zhuzh
    /// Date:  2014=05-12
    /// Desc:  用户登录限制枚举
    /// </summary>
    public enum UserLoginType
    {
        /// <summary>
        /// 正常登录
        /// </summary>
        NORMAL = 0,
        /// <summary>
        /// 首次登录错误异常
        /// </summary>
        ERRORFIRSTLOGIN = 1,
        /// <summary>
        /// 本次登录错误 未限制
        /// </summary>
        ERROROTHERLOGIN = 2,
        /// <summary>
        /// 登录错误限制登录
        /// </summary>
        ERRORLIMITLOGIN = 10,
        /// <summary>
        /// 验证码错误
        /// </summary>
        ERRORCODE = 20
    }
}
