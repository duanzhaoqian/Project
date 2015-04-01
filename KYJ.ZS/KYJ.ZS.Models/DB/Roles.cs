using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KYJ.ZS.Models.DB
{
    /// <summary>
    /// 作者：孟国栋
    /// 时间：2014*05*19
    /// 描述：角色实体类
    /// </summary>
   public class Roles
    {
       /// <summary>
       /// 角色Id
       /// </summary>
        public int RoleId { get; set; }
       /// <summary>
       /// 角色名称
       /// </summary>
        public string RoleName { get; set; }
       /// <summary>
       /// 添加时间
       /// </summary>
        public DateTime CreateTime { get; set; }
       /// <summary>
       /// 修改时间
       /// </summary>
        public DateTime UpdateTime { get; set; }
       /// <summary>
       /// 是否已经删除
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
}
