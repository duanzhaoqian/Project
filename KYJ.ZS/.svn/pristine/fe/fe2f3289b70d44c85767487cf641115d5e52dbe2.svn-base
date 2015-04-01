using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KYJ.ZS.Models.RentalGoodses
{
    /// <summary>
    /// 作者：wwang
    /// 时间：2014-5-5
    /// 描述：二级页面模型
    /// </summary>
    public class RentalGoodsChannelEntity
    {
        /// <summary>
        /// 分类菜单模型
        /// </summary>
        public RentalGoodsCatMenuEntity CatMenu { get; set; }

        /// <summary>
        /// 分类商品列表集合
        /// </summary>
        public List<GoodsList> ItemList { get; set; }

    }
    public class GoodsList
    {
        public string Name { get; set; }

        public List<RentalGoodsListItemEntity> ItemList { get; set; }

        public int Sort { get; set; }
    }
}
