using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KYJ.ZS.Models.DB
{
    /// <summary>
    /// 作者：wangyu
    /// 时间：2014/4/22 15:14:41
    /// 描述：商品颜色值实体类
    /// </summary>
    public class GoodsColor
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
        /// 分类颜色ID
        /// </summary>
        public int ColorId { get; set; }

        /// <summary>
        /// 分类颜色名称（可存自定义名称）
        /// </summary>
        public string ColorName { get; set; }

        /// <summary>
        /// 分类颜色代码(可存自定义)
        /// </summary>
        public string ColorCode { get; set; }

        /// <summary>
        /// 颜色类型：0 未知，1 租，2 售
        /// </summary>
        public int ColorType { get; set; }
    }
}
