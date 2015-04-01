using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KYJ.ZS.Models.DB;

namespace KYJ.ZS.Models.SaleGoodses
{
    /// <summary>
    /// 作者:wwang
    /// 时间：2014-5-6
    /// 描述：闲置物品列表页面分类栏模型
    /// </summary>
    public class SaleGoodsCatMenuEntity
    {
        /// <summary>
        /// 标签集合
        /// </summary>
        public List<Tag> Tags { get; set; }

        /// <summary>
        /// 总条数
        /// </summary>
        public int TotalCount { get; set; }
    }
}
