using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KYJ.ZS.Models.DB
{
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    /// <summary>
    /// 作者：wangyu
    /// 时间：2014/4/22 16:05:26
    /// 描述：出售商品扩展实体
    /// </summary>
    public class SaleOther
    {
        /// <summary>
        /// 主键
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 出售商品ID
        /// </summary>
        public int SaleId { get; set; }

        /// <summary>
        /// 出售商品描述
        /// </summary>
        [Required]
        public string Descs { get; set; }
    }
}
