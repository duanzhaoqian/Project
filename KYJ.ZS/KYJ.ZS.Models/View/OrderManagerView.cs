using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KYJ.ZS.Models.OrderGoodses;
using KYJ.ZS.Models.Orders;
using KYJ.ZS.Models.Pages;
using KYJ.ZS.Models.RentalGoodses;

namespace KYJ.ZS.Models.View
{
    /// <summary>
    /// 作者：maq
    /// 时间：2014年5月27日09:48:44
    /// 描述：租用中的订单viewmodel
    /// </summary>
    public class OrderManagerView
    {
        public OrderManagerView()
        {
            PageData = new PageData<OrderManageEntity>(1, 1, 1, 1);
            RentOrderPms = new RentOrderPms();
        }
        /// <summary>
        /// 分页数据
        /// </summary>
        public PageData<OrderManageEntity> PageData { get; set; }
        /// <summary>
        /// 查询参数
        /// </summary>
        public RentOrderPms RentOrderPms { get; set; }
        /// <summary>
        /// 得到当前订单状态的所在区域
        /// </summary>
        /// <returns></returns>
        public int GetCurrentArea()
        {
            if (RentOrderPms.OrderAreaType == 0)
            {
                return (int)new OrderStateEnumOper().GetAreaType((OrderState)RentOrderPms.OrderStateType);
            }
            else
            {
                return (int)RentOrderPms.OrderAreaType;
            }
        }
        /// <summary>
        /// 获取当前订单所在区域
        /// </summary>
        /// <returns></returns>
        public OrderStateAreaType GetCurrentAreaType()
        {
            if (RentOrderPms.OrderAreaType == 0)
            {
                return new OrderStateEnumOper().GetAreaType((OrderState)RentOrderPms.OrderStateType);
            }
            else
            {
                return (OrderStateAreaType)RentOrderPms.OrderAreaType;
            }
        }
    }
}
