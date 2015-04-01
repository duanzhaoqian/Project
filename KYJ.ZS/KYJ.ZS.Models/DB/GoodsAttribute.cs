using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KYJ.ZS.Models.DB
{
    /// <summary>
    /// 作者：wangyu
    /// 时间：2014/4/22 15:06:49
    /// 描述：商品属性值实体
    /// </summary>
    public class GoodsAttribute
    {
        /// <summary>
        /// 主键
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 商品ID
        /// </summary>
        public int GoodsId { get; set; }

        /// <summary>
        /// 分类属性ID
        /// </summary>
        public int CategoryAttrId { get; set; }

        /// <summary>
        /// 属性值ID（自定义属性值存0）
        /// </summary>
        public int AttrValId { get; set; }

        /// <summary>
        /// 商品属性值
        /// </summary>
        public string GoodsAttrVal { get; set; }

        /// <summary>
        /// 属性类型：0 未知，1 租，2 售
        /// </summary>
        public int GoodsAttrType { get; set; }
    }
}
