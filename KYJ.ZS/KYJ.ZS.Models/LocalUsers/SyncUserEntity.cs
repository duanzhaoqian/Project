using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KYJ.ZS.Models.LocalUsers
{
    /// <summary>
    /// Author:zhuzh
    /// Date:2014-4-25
    /// Desc:公共平台同步数据实体
    /// </summary>
    public class SyncUserEntity
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
        /// 密码
        /// </summary>
        public string PWD { get; set; }
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
        /// GUID唯一标识
        /// </summary>
        public string _GUID { get; set; }
        /// <summary>
        /// 性别：0保密 1男 2女
        /// </summary>
        public int Sex { get; set; }
        /// <summary>
        /// 状态：0 正常，1锁定(默认值0)
        /// </summary>
        public int State { get; set; }
    }
}
