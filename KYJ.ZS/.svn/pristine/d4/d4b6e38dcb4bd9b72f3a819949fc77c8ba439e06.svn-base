using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KYJ.ZS.Models.Pages;
using KYJ.ZS.Models.RentalGoodses;

namespace KYJ.ZS.Models.View
{
    /// <summary>
    /// 作者：maq
    /// 时间：2014年5月7日13:57:12
    /// 描述：仓库中的商品和违规商品的viewmodel
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class RentalGoodsIndexView<T> where T:new ()
    {
        public RentalGoodsIndexView()
        {
            QueryPms = new RentalGoodsesQueryPms();
        }
        /// <summary>
        /// 页面数据
        /// </summary>
        public PageData<T> PageData { get; set; }
        /// <summary> 
        /// 查询时的参数
        /// </summary>
        public RentalGoodsesQueryPms QueryPms { get; set; }
    }
}
