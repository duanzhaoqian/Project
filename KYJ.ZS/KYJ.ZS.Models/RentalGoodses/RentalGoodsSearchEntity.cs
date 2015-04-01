using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KYJ.ZS.Models.RentalGoodses
{
    /// <summary>
    /// 作者：wwang
    /// 时间：2014-5-5
    /// 描述：租售搜索页面模型
    /// </summary>
    public class RentalGoodsSearchEntity
    {
        //商品列表集合
        public List<RentalGoodsListItemEntity> ItemList { get; set; }
      
        //当前页码
        public int CurrentPageIndex { get; set; }
        //每页条数
        public int PageSize { get; set; }
        //分页总条数
        public int TotalItemCount { get; set; }
    }
}
