using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KYJ.ZS.Models.Pages;
using KYJ.ZS.Models.RentalGoodses;
using OrderStateEnum = KYJ.ZS.Models.Orders.OrderStateEnum;

namespace KYJ.ZS.Models.OrderGoodses
{
    /// <summary>
    /// 作者:maq
    /// 时间：2014年5月26日10:28:33
    /// 描述：出租的订单查询参数
    /// </summary>
    public class RentOrderPms
    {
        public RentOrderPms()
        {

        }
        /// <summary>
        /// 分页参数
        /// </summary>
        public PagePms PagePms { get; set; }
        private int _pageIndex;
        /// <summary>
        /// 当前页码
        /// </summary>
        public int PageIndex
        {
            get
            {
                if (_pageIndex == 0)
                {
                    PageIndex = 1;
                }
                return _pageIndex;
            }
            set { _pageIndex = value; }
        }

        private int _pageSize;
        /// <summary>
        /// 分页大小
        /// </summary>
        public int PageSize
        {
            get
            {
                _pageSize = 10;
                return _pageSize;
            }
            set { _pageSize = value; }
        }
        /// <summary>
        /// 商户ID
        /// </summary>
        public int? MerchantId { get; set; }
        /// <summary>
        /// 商品名称
        /// </summary>
        public string GoodsName { get; set; }
        /// <summary>
        /// 订单编号
        /// </summary>
        public string OrderNum { get; set; }
        /// <summary>
        /// 买家昵称
        /// </summary>
        public string BuyersNickName { get; set; }
        /// <summary>
        /// 区域类型
        /// </summary>
        public OrderStateAreaType? OrderAreaType { get; set; }
        /// <summary>
        /// 订单状态
        /// </summary>
        public OrderState? OrderStateType { get; set; }
    }
}
