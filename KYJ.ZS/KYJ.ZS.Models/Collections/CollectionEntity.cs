using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KYJ.ZS.Models.Collections
{
    /// <summary>
    /// Author：baiyan
    /// Time：2014-05-07
    /// 收藏列表属性
    /// </summary>
    public class CollectionEntity
    {
        /// <summary>
        /// 收藏表ID
        /// </summary>
        public int CollId { get; set; }
        /// <summary>
        /// 用户ID
        /// </summary>
        public int UserId { get; set; }
        /// <summary>
        /// 商品ID
        /// </summary>
        public int GoodsId { get; set; }
        /// <summary>
        /// 收藏时间
        /// </summary>
        public DateTime Time { get; set; }
        /// <summary>
        /// 商品类型（0 未知，1 租，2 售）
        /// </summary>
        public int GoodsType { get; set; }
        /// <summary>
        /// 是否删除
        /// </summary>
        public bool IsDel { get; set; }
        /// <summary>
        /// 品牌名称
        /// </summary>
        public string BrandName { get; set; }
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 商品GUID
        /// </summary>
        public string Guid { get; set; }
        /// <summary>
        /// 市场价
        /// </summary>
        public decimal Price { get; set; }
        /// <summary>
        /// 商品月租金
        /// </summary>
        public decimal MonthPrice { get; set; }
        /// <summary>
        /// 出租数量
        /// </summary>
        public int Number { get; set; }
    }
}
