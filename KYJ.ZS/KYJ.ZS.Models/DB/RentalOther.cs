using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KYJ.ZS.Models.DB
{
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// 作者：wangyu
    /// 时间：2014/4/22 16:09:17
    /// 描述：出租商品扩展实体类
    /// </summary>
    public class RentalOther
    {
        /// <summary>
        /// 主键
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 出租商品ID
        /// </summary>
        public int RentalId { get; set; }

        /// <summary>
        /// 出租商品描述
        /// </summary>
        [Required]
        public string Descs { get; set; }

        /// <summary>
        /// 出租商品属性扩展
        /// </summary>
        [StringLength(4000)]
        public string Attrs { get; set; }

        /// <summary>
        /// 出租商品颜色扩展
        /// </summary>
        [StringLength(4000)]
        public string Colors { get; set; }

        /// <summary>
        /// 出租商品规格扩展
        /// </summary>
        [StringLength(4000)]
        public string Norms { get; set; }


        /// <summary>
        /// 出租商品价格扩展
        /// </summary>
        [StringLength(4000)]
        public string Prices { get; set; }
    }
}
