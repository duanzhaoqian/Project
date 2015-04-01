using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KYJ.ZS.Models.DB;
using KYJ.ZS.DAL.OrderGoodses;
using KYJ.ZS.Models.OrderGoodses;

namespace KYJ.ZS.BLL.OrderGoodses
{
    /// <summary>
    ///作用：交易管理
    ///作者：李晓波
    ///QQ联系方式：935300596
    ///编写日期：2014.4.17
    ///修改时间：
    ///修改人：
    /// </summary>
    public class OrderGoodsBll
    {

        OrderGoodsDal dal = new OrderGoodsDal();
        #region 获取租用商品数据列表
        /// <summary>
        /// Auther:李晓波
        /// Time:2014-4-17
        /// Desc:获取租用商品数据列表
        /// </summary>          
        /// <param name="mdlOrderGoods"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalRecord"></param>
        /// <param name="TotalPage"></param>
        /// <returns></returns>
        public List<RentalOrderEntity> GetList(int id, RentalOrderEntity mdlRentalOrder, int pageIndex, int pageSize, out int totalRecord, out int totalPage)
        {
            //if (mdlRentalOrder.OrderDate != null)//根据订单日期查询
            //{
            //    return dal.GetList(id, mdlRentalOrder, pageIndex, pageSize, out  totalRecord, out  totalPage);
            //}
            //else if (mdlRentalOrder.State != null)//根据订单状态查询
            //{
            //    return dal.GetListForState(id, mdlRentalOrder, pageIndex, pageSize, out  totalRecord, out  totalPage);
            //}
            return dal.GetList(id, mdlRentalOrder, pageIndex, pageSize, out  totalRecord, out  totalPage);
        }
        #endregion
        /// <summary>
        /// Auther:李晓波
        /// Time:2014-5-8
        /// Desc:根据商户操作改变订单状态
        /// </summary>  
        /// <param name="tblType">订单所属表</param>
        /// <param name="operationType">订单操作类型</param>
        /// <param name="orderId">订单id</param>
        /// <returns></returns>
        public bool ChangeOrderStatus(int operationType, int orderId)
        {
            return dal.ChangeOrderStatus(operationType, orderId) > 0;
        }
        #region 用户后台获取出租订单分页
        /// <summary>
        /// Author:baiyan
        /// Time:2014-4-18
        /// Desc:用户后台获取出租订单分页
        /// </summary>
        /// <param name="index">当前页</param>
        /// <param name="pageSize">每页显示个数</param>
        /// <param name="totalRecord">总个数</param>
        /// <param name="totalPage">总页数</param>
        public List<OrderGoodsEntity> GetUserRentOrdersPage(OrderGoodsEntity _where, int index, int pageSize, out int totalRecord, out int totalPage)
        {
            return dal.GetUserRentOrdersPage(_where, index, pageSize, out totalRecord, out totalPage);
        }
        #endregion
        #region 用户后台获取出租订单详情页
        /// <summary>
        /// Author:baiyan
        /// Time:2014-4-21
        /// 用户后台获取出租订单详情页
        /// </summary>
        /// <param name="orderGoodsId">订单商品ID</param>
        /// <param name="userId">用户ID</param>
        /// <returns></returns>
        public OrderGoodsEntity GetUserRentOrdersInfo(int orderGoodsId, int userId)
        {
            return dal.GetUserRentOrdersInfo(orderGoodsId, userId);
        }
        #endregion
        /// <summary>
        /// Author:baiyan
        /// Time:2014-4-29
        /// 缴费历史
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public List<GoodsTenanci> GetPaymentRecord(int userId, int orderGoodsId)
        {
            return dal.GetPaymentRecord(userId, orderGoodsId);
        }
        /// <summary>
        /// Author:baiyan
        /// Time:2014-4-30
        /// 未缴费
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="orderGoodsId">订单商品ID</param>
        /// <returns></returns>
        public GoodsTenanci GetNotPayment(int userId, int orderGoodsId)
        {
            return dal.GetNotPayment(orderGoodsId);
            // dal.GetNotPayment(userId, orderGoodsId);
        }
        /// <summary>
        /// Author:baiyan
        /// Time:2014-4-28
        /// 确认收货
        /// </summary>
        /// <param name="orderId">订单ID</param>
        /// <param name="userId">用户ID</param>
        /// <param name="state">要更改的状态ID</param>
        /// <returns></returns>
        public int ConfirmReceipt(int orderId, int userId, int state)
        {
            return dal.ConfirmReceipt(orderId, userId, state);
        }
        /// <summary>
        /// Author:baiyan
        /// Time:2014-4-28
        /// 查询缴费单
        /// </summary>
        /// <param name="tenancyId">缴费单ID</param>
        /// <param name="userId">用户ID</param>
        /// <returns></returns>
        public GoodsTenanci GetPayment(int tenancyId, int userId)
        {
            return dal.GetPayment(tenancyId, userId);
        }
        /// <summary>
        /// 缴费
        /// Author:baiyan
        /// Time:2014-4-28
        /// </summary>
        /// <param name="tenanci">租期实体</param>
        /// <returns></returns>
        public int Payment(GoodsTenanci tenanci)
        {
            return dal.Payment(tenanci);
        }
        /// <summary>
        /// Author:baiyan
        /// Time:2014-5-4
        /// 查询买断商品的总余额
        /// </summary>
        /// <param name="orderGoodsId">订单商品ID</param>
        /// <returns></returns>
        public decimal GetBuyoutSum(int orderGoodsId)
        {
            return dal.GetBuyoutSum(orderGoodsId);
        }
        /// <summary>
        /// Author:baiyan
        /// Time:2014-5-4
        /// 买断
        /// </summary>
        /// <param name="orderGoodsId">订单商品ID</param>
        /// <param name="userId">用户ID</param>
        /// <returns></returns>
        public int Buyout(int orderGoodsId, int userId)
        {
            return dal.Buyout(orderGoodsId, userId);
        }
        /// <summary>
        /// 取消订单
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="orderGoodsId">订单商品ID</param>
        /// <returns></returns>
        public int CancelOrders(int userId, int orderId, int state)
        {
            return dal.CancelOrders(userId, orderId, state);
        }
        /// <summary>
        /// Author:baiyan
        /// Time:2014-5-23
        /// 删除订单
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="orderGoodsId">订单商品ID</param>
        /// <returns></returns>
        public int DelOrders(int userId, int orderGoodsId) {
            return dal.DelOrders(userId,orderGoodsId);
        }
        /// <summary>
        /// Author:baiyan
        /// Time:2014-5-7
        /// 申请换货
        /// </summary>
        /// <param name="orderId">订单ID</param>
        /// <param name="userId">用户ID</param>
        /// <param name="state">状态</param>
        /// <returns></returns>
        public int ReturnsGoods(int orderId, int userId, int state)
        {
            return dal.ReturnsGoods(orderId, userId, state);
        }
        /// <summary>
        /// Author:baiyan
        /// Time:2014-4-29
        /// 申请退租
        ///  <param name="orderId">订单ID</param>
        /// <param name="userId">用户ID</param>
        /// <param name="state">状态</param>
        /// </summary>
        /// <returns></returns>
        public int ApplicationSurrender(int orderId, int userId, int state)
        {
            return dal.ApplicationSurrender(orderId, userId, state);
        }
        /// <summary>
        /// Author:baiyan
        /// Time:2014-4-29
        /// 申请退货
        /// </summary>
        /// <param name="orderId">订单ID</param>
        /// <param name="userId">用户ID</param>
        /// <param name="state">状态</param>
        /// <returns></returns>
        public int ApplicationReturnOfGoods(int orderId, int userId, int state)
        {
            return dal.ApplicationReturnOfGoods(orderId, userId, state);
        }

        /// <summary>
        /// 从订单扩展表中根据订单id取得订单状态
        /// </summary>
        /// <param name="orderId">订单id</param>
        /// <returns></returns>
        public int getOderState(int orderId)
        {
            return dal.getOderState(orderId);
        }
        /// <summary>
        /// 从订单扩展表中根据订单状态取得订单时间
        /// </summary>
        /// <param name="orderId">订单id</param>
        /// <returns></returns>
        public DateTime getOderTime(int orderState)
        {

            return dal.getOderTime(orderState);

        }
        /// <summary>
        /// 更新订单表的状态和时间
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="orderState"></param>
        /// <param name="orderTime"></param>
        public bool updateOrder(int orderId, int orderState, DateTime orderTime)
        {
            return dal.updateOrder(orderId, orderState, orderTime);
        }

        public bool updateRes(int orderId, string res)
        {
            return dal.updateRes(orderId, res);
        }




        /// <summary>
        /// Author:baiyan
        /// Time:2014-5-21
        /// Desc:撤消订单返回上一级操作
        /// </summary>
        /// <returns></returns>
        public int CancelOrderOperating(int orderId, int userId)
        {
            return dal.CancelOrderOperating(orderId, userId);
        }
        /// <summary>
        /// Author:baiyan
        /// Time:2014-5-21
        /// 确认租用
        /// </summary>
        /// <param name="orderId">订单ID</param>
        /// <returns></returns>
        public int ConfirmHire(int orderId, int userId, int state)
        {
            return dal.ConfirmHire(orderId, userId, state);
        }
         /// <summary>
        /// Author:baiyan
        /// Time:2014-5-27
        /// 根据orderid获取订单扩展表
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public OrderOther GetOrderOtherByOrderId(int orderId) {
            return dal.GetOrderOtherByOrderId(orderId);
        }
        /// <summary>
        /// Author:baiyan
        /// Time:2014-4-29
        /// 扣除赔损金额
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public int ConfirmPayout(int orderId, int userId, int state)
        {
            return dal.ConfirmPayout(orderId, userId, state);
        }
    }
}
