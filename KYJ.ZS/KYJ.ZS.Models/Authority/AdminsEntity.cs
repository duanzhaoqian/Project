using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KYJ.ZS.Models.Authority
{
    /// <summary>
    /// 作者：孟国栋
    /// 时间：2014.05.22
    /// 描述：管理员展示实体类
    /// </summary>
    public class AdminsEntity
    {
        /// <summary>
        /// 管理员ID
        /// </summary>
        public int AdminId { get; set; }
        /// <summary>
        /// 角色ID
        /// </summary>
        public int RoleId { get; set; }
        /// <summary>
        /// 角色名字
        /// </summary>
        public string RoleName { get; set; }
        /// <summary>
        /// 用户名
        /// </summary>
        public string AdminName { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string PassWord { get; set; }
        /// <summary>
        /// 联系电话
        /// </summary>
        public string Mobile { get; set; }
        /// <summary>
        /// 真实姓名
        /// </summary>
        public string RealName { get; set; }
        /// <summary>
        /// 唯一标识
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
        public bool Isdel { get; set; }
        /// <summary>
        /// 操作管理员ID
        /// </summary>
        public int OperId { get; set; }
        /// <summary>
        /// 操作管理员名字
        /// </summary>
        public string OperName { get; set; }
    }
    /// <summary>
    /// 作者：孟国栋
    /// 时间：2014-05-22
    /// 描述：管理员查询条件
    /// </summary>
    public class AdminSearchEntity
    {
        /// <summary>
        /// 登录账号
        /// </summary>
        public string AdminName { get; set; }
        /// <summary>
        /// 角色ID
        /// </summary>
        public int RoleId { get; set; }
        /// <summary>
        /// 职员姓名
        /// </summary>
        public string RealName { get; set; }
        /// <summary>
        /// 联系电话
        /// </summary>
        public string Mobile { get; set; }
    }
}
