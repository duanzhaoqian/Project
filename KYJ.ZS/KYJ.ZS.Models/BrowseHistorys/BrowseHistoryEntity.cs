using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KYJ.ZS.Models.BrowseHistorys
{
    /// <summary>
    /// 作者：邓福伟
    /// 时间：2014-05-27
    /// 描述：浏览历史
    /// </summary>
    public class BrowseHistoryEntity
    {
        /// <summary>
        /// 商品Id
        /// </summary>
        public int GoodsId{get;set;}
        /// <summary>
        /// 商品Guid
        /// </summary>
        public string GoodsGuid { get; set; }
        /// <summary>
        /// 商品价格
        /// </summary>
        public double GoodsPrice { get; set; }
        /// <summary>
        /// 商品浏览时间
        /// </summary>
        //public DateTime BrowseTime { get; set; }
    }
}
