using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace KYJ.ZS.Models.Admin
{
    /// <summary>
    /// Author：ningjd
    /// Time：2014-06-06
    /// Desc: 职员添加Entity
    /// </summary>
    public class AdminCreateEntity
    {
        /// <summary>
        /// 管理员ID
        /// </summary>
        public int? AdminId { get; set; }
        /// <summary>
        /// 角色ID
        /// </summary>
        [Required(ErrorMessage = "")]
        [Range(1, Int32.MaxValue)]
        public int RoleId { get; set; }
        /// <summary>
        /// 用户名
        /// </summary>
        [Required(ErrorMessage = "")]
        [StringLength(50)]
        public string AdminName { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        [Required(ErrorMessage = "")]
        [StringLength(20)]
        public string PassWord { get; set; }
        /// <summary>
        /// 联系电话
        /// </summary>
        [Required(ErrorMessage = "")]
        [StringLength(50)]
        public string Mobile { get; set; }
        /// <summary>
        /// 真实姓名
        /// </summary>
        [Required(ErrorMessage = "")]
        [StringLength(20)]
        public string RealName { get; set; }
        /// <summary>
        /// 操作管理员ID
        /// </summary>
        [Required(ErrorMessage = "")]
        public int OperId { get; set; }
        /// <summary>
        /// 操作管理员名字
        /// </summary>
        [Required(ErrorMessage = "")]
        [StringLength(20)]
        public string OperName { get; set; }
    }
}
