using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KYJ.ZS.Commons.Index
{
    /// Author：wwang
    /// Time：2014-04-17
    /// Desc: 搜索条件对象类
    /// <summary>
    /// 搜索商品条件对象
    /// </summary>
    public class IndexGoodsConditionInfo
    {
        private List<SearchAttr> _attrs;

        private int _pageSize = 10;
        /// <summary>
        /// 搜索对象属性值集合
        /// </summary>
        public List<SearchAttr> Attrs
        {

            get
            {
                if (_attrs != null)
                    return _attrs;
                else
                {
                    _attrs=new List<SearchAttr>();
                }
                return _attrs;
            }
            set
            {
                _attrs = value;

            }
        }

        /// <summary>
        /// 模糊搜索
        /// </summary>
        public string KeyWord { get; set; }

        /// <summary>
        /// 第几页
        /// </summary>
        public int PageIndex { get; set; }

        public int PageSize 
        {
            get
            {
                return _pageSize;
            }
            set
            {
                _pageSize=value;
            }
        }

        public string PriceBegin { get; set; }
      
        public string PriceEnd { get; set; }

        /// <summary>
        ///销量排序  0  售价排序  1 升序 2 降序 上架时间排序  3
        /// </summary>
        public string Sort { get; set; }

        /// <summary>
        /// 商品类型：租，售
        /// </summary>
        public string GoodsType { get; set; }

        /// <summary>
        /// 一级分类ID
        /// </summary>
        public string FirstlyCatId { get; set; }

        /// <summary>
        /// 二级分类ID
        /// </summary>
        public string SecondaryCatId { get; set; }

        /// <summary>
        /// 三级级分类ID
        /// </summary>
        public string ThirdlyCatId { get; set; }

        /// <summary>
        /// 品牌ID
        /// </summary>
        public string BrandId { get; set; }

        /// <summary>
        /// 商品规格ID
        /// </summary>
        public string GoodsNormId { get; set; }
        

    }
    public class SearchAttr
    {
        public string AttributeId { get; set; }

        public string AttributeValueId { get; set; }
    }
}
