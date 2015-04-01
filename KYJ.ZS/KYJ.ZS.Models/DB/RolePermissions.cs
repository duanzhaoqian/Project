using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KYJ.ZS.Models.DB
{
    /// <summary>
    /// 权限实体类
    /// </summary>
   public class RolePermissions
    {
       //权限ID
        public int RolePermissionId { get; set; }
       //角色ID
        public int RoleId { get; set; }
       //模块ID
        public int ModuleId { get; set; }
       //权限
        public string Permission { get; set; }
       //操作人ID
        public int OperId { get; set; }
       //操作人姓名
        public string OperName { get; set; }
    }
}
