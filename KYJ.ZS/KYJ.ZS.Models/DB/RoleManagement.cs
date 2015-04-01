using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KYJ.ZS.Models.DB
{
    /// <summary>
    /// 作者：孟国栋
    /// 时间：2014-05-19
    /// 描述：角色管理实体类
    /// </summary>
   public class RoleManagement
    {
       /// <summary>
       /// 角色权限信息的ID
       /// </summary>
        public int Id { get; set; }
       /// <summary>
       /// 角色ID
       /// </summary>
        public int RoleId { get; set; }
       /// <summary>
       /// 模块ID
       /// </summary>
        public int ModuleId { get; set; }
       /// <summary>
       /// 所拥有权限（以1,2,3...格式存储）
       /// </summary>
        public string Permission { get; set; }
       /// <summary>
       /// 操作管理员Id
       /// </summary>
        public int OperId { get; set; }
       /// <summary>
       /// 操作管理员用户名
       /// </summary>
        public string OperName { get; set; }
    }
}
