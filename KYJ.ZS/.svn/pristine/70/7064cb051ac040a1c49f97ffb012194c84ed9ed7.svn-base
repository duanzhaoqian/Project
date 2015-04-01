using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KYJ.ZS.Models.DB;

namespace KYJ.ZS.Models.RentalGoodses
{
    /// <summary>
    ///  作者：wwang
    ///  时间：2014-4-30
    ///  描述：前台商品三级列表页
    /// </summary>
    public class RentalGoodsListEntity
    {
        /// <summary>
        /// 分类菜单模型
        /// </summary>
        public RentalGoodsCatMenuEntity CatMenu { get; set; }
        //商品列表集合
        public List<RentalGoodsListItemEntity> ItemList { get; set; }
        /// <summary>
        /// 搜索条件菜单模型
        /// </summary>
        public RentalGoodsSearchMenuEntity SearchMenu { get; set; }
        //当前页码
        public int CurrentPageIndex { get; set; }
        //每页条数
        public int PageSize { get; set; }
        //分页总条数
        public int TotalItemCount { get; set; }

    }

    /// <summary>
    /// 类别的属性类
    /// </summary>
    public class CategoryAttribute
    {
        /// <summary>
        /// 属性名称
        /// </summary>
        public AttributeNameAttribute AttributeName { get; set; }

        /// <summary>
        /// 属性值集合
        /// </summary>
        public List<AttributeValueAttribute> AttributeValues { get; set; }


    }
    /// <summary>
    /// 属性名称类
    /// </summary>
    public class AttributeNameAttribute
    {
        public int CategoryAttrId { get; set; }

        public string Name { get; set; }
    }
    /// <summary>
    /// 属性值类
    /// </summary>
    public class AttributeValueAttribute
    {
        public int AttributeValueId { get; set; }

        public string Name { get; set; }
    }
}
