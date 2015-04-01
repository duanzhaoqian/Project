using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KYJ.ZS.Models.RolePermission
{
    /// <summary>
    /// Author：ningjd
    /// Time：2014-06-05
    /// Desc：角色权限Entity
    /// </summary>
    public class RolePermissionEntity
    {
        /// <summary>
        /// 角色ID
        /// </summary>
        public int? Id { get; set; }

        /// <summary>
        /// 角色名称
        /// </summary>
        public string RoleName { get; set; }

        /// <summary>
        /// 操作人Id
        /// </summary>
        public int OperId { get; set; }

        /// <summary>
        /// 操作人名称
        /// </summary>
        public string OperName { get; set; }

        /// <summary>
        /// 权限集合
        /// </summary>
        public IList<PermissionEntity> PermissionList { get; set; }
    }

    /// <summary>
    /// Author：ningjd
    /// Time：2014-06-05
    /// Desc：权限Entity
    /// </summary>
    public class PermissionEntity
    {
        /// <summary>
        /// 模块ID
        /// </summary>
        public int ModuleId { get; set; }

        /// <summary>
        /// 权限(格式：1,2,3,4,5)1增2删3改4查
        /// </summary>
        public string Permission { get; set; }
    }
}
