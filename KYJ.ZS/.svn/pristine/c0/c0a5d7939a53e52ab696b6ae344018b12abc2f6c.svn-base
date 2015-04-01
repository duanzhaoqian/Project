using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KYJ.ZS.Models.LocalUsers
{
    /// <summary>
    /// Author：ningjd
    /// Time：2014-06-04
    /// Desc：普通用户管理
    /// </summary>
    public class LocalUserManageEntity
    {
        public int Id { get; set; }

        /// <summary>
        /// 登录名
        /// </summary>
        public string LoginName { get; set; }

        /// <summary>
        /// 真实姓名
        /// </summary>
        public string RealName { get; set; }

        /// <summary>
        /// 性别：0保密 1男 2女
        /// </summary>
        public int Sex { get; set; }

        /// <summary>
        /// 注册时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 用户最后登录时间
        /// </summary>
        public DateTime LastLoginTime { get; set; }

        /// <summary>
        /// 认证状态：0未知1未认证2认证中3认证未通过4认证通过
        /// </summary>
        public int PapersStatus { get; set; }

        /// <summary>
        /// 认证状态文字说明
        /// </summary>
        public string PapersStatusDesc {
            get
            {
                switch (this.PapersStatus)
                {
                    case 1: return "未认证";
                    case 2: return "认证中";
                    case 3: return "认证未通过";
                    case 4: return "认证通过";
                    default: return "";
                }
            }
        }

        /// <summary>
        /// 账户
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// 状态：0 正常，1锁定(默认值0)
        /// </summary>
        public int State { get; set; }

        /// <summary>
        /// GUID
        /// </summary>
        public string Guid { get; set; }

        /// <summary>
        /// 提交时间
        /// </summary>
        public DateTime PapersTime { get; set; }
    }
}
