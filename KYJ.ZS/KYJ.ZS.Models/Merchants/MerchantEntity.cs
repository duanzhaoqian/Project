using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace KYJ.ZS.Models.Merchants
{
    /// <summary>
    /// Author：ningjd
    /// Time：2014-05-21
    /// Desc：商户添加Entity
    /// </summary>
    public class MerchantCreateEntity
    {
        /// <summary>
        /// 账号
        /// </summary>
        [Required(ErrorMessage = "请输入账号")]
        [StringLength(50)]
        public string LoginName { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        [Required(ErrorMessage = "请输入密码")]
        [StringLength(50)]
        public string Pwd { get; set; }

        /// <summary>
        /// 企业名称
        /// </summary>
        [Required(ErrorMessage = "请输入企业名称")]
        [StringLength(50)]
        public string Name { get; set; }

        /// <summary>
        /// 企业简介
        /// </summary>
        [StringLength(1000)]
        public string Introduction { get; set; }

        /// <summary>
        /// 经营类目
        /// </summary>
        [StringLength(1000)]
        public string OperateCategory { get; set; }

        /// <summary>
        /// GUID
        /// </summary>
        public string Guid { get; set; }

        /// <summary>
        /// 管理员ID
        /// </summary>
        public int AdminId { get; set; }

        /// <summary>
        /// 管理员名称
        /// </summary>
        public string AdminName { get; set; }
    }

    /// <summary>
    /// Author：ningjd
    /// Time：2014-05-22
    /// Desc：商户管理列表Entity
    /// </summary>
    public class MerchantIndexEntity
    {
        /// <summary>
        /// 商户ID
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 企业账号
        /// </summary>
        public string LoginName { get; set; }

        /// <summary>
        /// 入驻时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// GUID
        /// </summary>
        public string Guid { get; set; }

        /// <summary>
        /// 企业名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 企业简介
        /// </summary>
        public string Introduction { get; set; }
    }
}
