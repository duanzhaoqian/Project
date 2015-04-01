using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting;
using System.Text;
using KYJ.ZS.DAL.LocalUsers;
using KYJ.ZS.DAL.Orders;
using KYJ.ZS.Models.DB;
using KYJ.ZS.Models.OrderGoodses;
using KYJ.ZS.Models.Orders;
using KYJ.ZS.Commons;
using KYJ.ZS.Models.Pages;
using KYJ.ZS.Models.RentalGoodses;

namespace KYJ.ZS.BLL.Orders
{
    /// <summary>
    /// Author：ningjd
    /// Time：2014-04-24
    /// Desc：操作订单表
    /// </summary>
    public class OrderBll
    {
        private OrderDal dal = new OrderDal();

        #region 发货管理(租)—ningjd

        /// <summary>
        /// 获取订单列表(发货、未发货)
        /// </summary>
        /// <param name="id">订单ID</param>
        /// <param name="searchEntity">搜索条件Entity</param>
        /// <param name="index">当前页</param>
        /// <param name="pageSize">每页显示个数</param>
        /// <param name="totalRecord">总个数</param>
        /// <param name="totalPage">总页数</param>
        /// <returns></returns>
        public IList<OrderEntity> GetOrdersByState(int? id, OrderSearchEntity searchEntity, int index, int pageSize, out int totalRecord, out int totalPage)
        {
            return dal.GetOrdersByState(id, searchEntity, index, pageSize, out totalRecord, out totalPage);
        }

        /// <summary>
        /// 修改订单发货信息（管理后台）
        /// </summary>
        /// <param name="orderId">订单ID</param>
        /// <param name="expressCompany">快递/物流公司名称</param>
        /// <param name="expressNum">快递/物流单号</param>
        /// <returns></returns>
        public bool UpdateSendInfo(int orderId, string expressCompany, string expressNum)
        {
            return dal.UpdateSendInfo(orderId, expressCompany, expressNum) == 1;
        }

        /// <summary>
        /// 修改订单发货信息(商户判断)
        /// </summary>
        /// <param name="orderId">订单ID</param>
        /// <param name="expressCompany">快递/物流公司名称</param>
        /// <param name="expressNum">快递/物流单号</param>
        /// <param name="merchantId">当前商户ID</param>
        /// <returns></returns>
        public bool UpdateSendInfo(int orderId, string expressCompany, string expressNum, int merchantId)
        {
            return dal.UpdateSendInfo(orderId, expressCompany, expressNum, merchantId) == 1;
        }

        #endregion

        /// <summary>
        /// 作者：maq
        /// 时间：2014年5月26日10:11:31
        /// 描述：获取各个状态订单数量
        /// </summary>
        /// <param name="operId"></param>
        /// <returns></returns>
        public Dictionary<int, int> GetOrderSatteNum(int operId)
        {
            Dictionary<int, int> dicNow = dal.GetOrdersStateNum(operId);
            Dictionary<int, int> dicOld = dal.GetOldOrderSataeNum(operId);
            Dictionary<int, int> dic = new Dictionary<int, int>();
            foreach (KeyValuePair<int, int> valuePair in dicNow)
            {
                if (dic.ContainsKey(valuePair.Key))
                {
                    dic[valuePair.Key] += valuePair.Value;
                }
                else
                {
                    dic.Add(valuePair.Key, valuePair.Value);
                }
            }
            foreach (KeyValuePair<int, int> valuePair in dicOld)
            {
                if (dic.ContainsKey(valuePair.Key))
                {
                    dic[valuePair.Key] += valuePair.Value;
                }
                else
                {
                    dic.Add(valuePair.Key, valuePair.Value);
                }
            }
            return dic;
        }

        #region 邓福伟
        /// <summary>
        /// 作者：邓福伟
        /// 时间：2014-05-06
        /// 描述：订单生成参数判断--订单确认页
        /// </summary>
        /// <param name="entity">订单生成需要的参数</param>
        /// <returns></returns>
        public int Web_OrderParameterJudge(Web_OrderParameterEntity entity)
        {
            return dal.Web_OrderParameterJudge(entity);
        }
        /// <summary>
        /// 作者：邓福伟
        /// 时间：2014-05-06
        /// 描述：订单详情--订单确认页
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public Web_OrderConfirm Web_GetOrderConfirm(Web_OrderParameterEntity entity)
        {
            return dal.Web_GetOrderConfirm(entity);
        }
        /// <summary>
        /// 作者：邓福伟
        /// 时间：2014-05-09
        /// 描述：提交订单
        /// </summary>
        /// <param name="entity">订单生成需要的参数</param>
        /// <returns></returns>
        public int Web_SubmitOrder(Web_OrderParameterEntity entity)
        {
             #region 价格扩展
            //判断是否需要修改,价格扩展的数据
            if (entity.GoodsNormId > 0 && entity.GoodsColorId > 0)
            {
                //取价格扩展
                RentalGoodses.RentalGoodsBll rgBll = new RentalGoodses.RentalGoodsBll();
                var list = rgBll.Web_GetRentalOtherPrice(entity.GoodsId).Decompress().FromJson<List<Models.DB.GoodsPrice>>();
                //修改价格扩展的商品数量
                foreach (var model in list)
                {
                    if (entity.GoodsNormId == model.NormId && entity.GoodsColorId == model.ColorId)
                    {
                        model.Number = model.Number - entity.GoodsNum; //商品数量减一
                        break;
                    }
                }
                //最新的价格扩展,进行序列化和压缩
                entity.GoodsOtherPrices = list.ToJson().Compress();
            }
            else
            {
                entity.GoodsOtherPrices = DBNull.Value.ToString();
            }
             #endregion

            return dal.Web_SubmitOrder(entity);
        }
        #endregion
        #region 订单管理(租)—ningjd

        /// <summary>
        /// 获取订单列表(租)
        /// </summary>
        /// <param name="areaEnum">订单区域Enum</param>
        /// <param name="stateEnum">订单状态Enum</param>
        /// <param name="title">商品名称</param>
        /// <param name="number">订单编号</param>
        /// <param name="userNikeName">买家昵称</param>
        /// <param name="index">当前页</param>
        /// <param name="pageSize">每页显示个数</param>
        /// <param name="totalRecord">总个数</param>
        /// <param name="totalPage">总页数</param>
        /// <returns></returns>
        public IList<OrderManageEntity> GetRentalOrdersList(OrderStateAreaType areaEnum, OrderState? stateEnum, string title, string number, string userNikeName, int index, int pageSize, out int totalRecord, out int totalPage)
        {
            return dal.GetRentalOrdersList(areaEnum, stateEnum, title, number, userNikeName, index, pageSize, out totalRecord, out totalPage, null);
        }
        /// <summary>
        /// 作者：maq
        /// 时间：2014年5月26日16:45:55
        /// 描述：获取订单列表
        /// </summary>
        /// <param name="rentOrderPms"></param>
        /// <returns></returns>
        public PageData<OrderManageEntity> GetRentalOrdersList(RentOrderPms rentOrderPms)
        {
            return dal.GetRentalOrdersList(rentOrderPms);
        }

        /// <summary>
        /// 获取商户订单记录
        /// </summary>
        /// <param name="merchantId">商户ID</param>
        /// <param name="title">商品名称</param>
        /// <param name="number">订单编号</param>
        /// <param name="userNikeName">买家昵称</param>
        /// <param name="index">当前页</param>
        /// <param name="pageSize">每页显示个数</param>
        /// <param name="totalRecord">总个数</param>
        /// <param name="totalPage">总页数</param>
        /// <returns></returns>
        public IList<OrderManageEntity> GetOrderRecord(int merchantId, string title, string number, string userNikeName, int index, int pageSize, out int totalRecord, out int totalPage)
        {
            return dal.GetRentalOrdersList(OrderStateAreaType.All, null, title, number, userNikeName, index, pageSize, out totalRecord, out totalPage, merchantId);
        }
        #endregion


        #region  作者：maq  时间:2014年5月28日10:55:02   描述  订单状态相关操作
        /// <summary>
        /// 发送起租协议
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public Boolean SendAgreement(int orderId)
        {
            Boolean result = false;
            try
            {
                result = dal.SendAgreement(orderId);
            }
            catch (Exception ex)
            {
                Log4net.RecordLog.RecordException("maq", "(orderId)", ex);
            }
            return result;
        }
        /// <summary>
        /// 同意退租
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public Boolean BackRentAgree(int orderId)
        {
            Boolean result = false;
            try
            {
                result = dal.BackRentAgree(orderId);
            }
            catch (Exception ex)
            {
                Log4net.RecordLog.RecordException("maq", "(orderId)", ex);
            }
            return result;
        }
        /// <summary>
        /// 换货同意
        /// </summary>
        /// <returns></returns>
        public Boolean ExChangeGoodsAgree(int orderId, OrderState orderState)
        {
            Boolean result = false;
            try
            {
                result = dal.ExChangeGoodsAgree(orderId, orderState);
            }
            catch (Exception ex)
            {
                Log4net.RecordLog.RecordException("maq", "(orderId,orderState)", ex);
            }
            return result;
        }

        /// <summary>
        /// 订单撤销到上级状态
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="orderState"></param>
        /// <returns></returns>
        public Boolean OrderGoBack(int orderId, int orderState, string message)
        {
            Boolean result = false;
            try
            {
                result = dal.OrderGoBack(orderId, orderState, message);
            }
            catch (Exception ex)
            {
                Log4net.RecordLog.RecordException("maq", "(orderId,orderState)", ex);
            }
            return result;
        }
        /// <summary>
        /// 改变金额
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="money"></param>
        /// <param name="orderState"></param>
        /// <returns></returns>
        public Boolean ChangeMoney(int orderId, decimal money, OrderState orderState)
        {
            Boolean result = false;
            try
            {
                decimal userMoney = dal.GetOrderDeposit(orderId);
                result = dal.ChangeMoney(orderId, GetChangeState(orderState, money, userMoney), money,orderState);
            }
            catch (Exception ex)
            {
                Log4net.RecordLog.RecordException("maq", "(orderId,money)", ex);
            }
            return result;
        }
        /// <summary>
        /// 换货验收   撤销
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public Boolean CancleExchange(int orderId)
        {

            Boolean result = false;
            try
            {
                result = dal.CancleExchange(orderId);
            }
            catch (Exception ex)
            {
                Log4net.RecordLog.RecordException("maq", "(orderId)", ex);
            }
            return result;
        }
        /// <summary>
        /// 退货同意
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public Boolean BackGoodsAgree(int orderId)
        {
            Boolean result = false;
            try
            {
                result = dal.BackGoodsAgree(orderId);
            }
            catch (Exception ex)
            {
                Log4net.RecordLog.RecordException("maq", "(orderId)", ex);
            }
            return result;
        }

        /// <summary>
        /// 得到赔损之后的订单状态
        /// </summary>
        /// <param name="orderState"></param>
        /// <param name="money"></param>
        /// <param name="userMoney"></param>
        /// <returns></returns>
        public OrderState GetChangeState(OrderState orderState, decimal money, decimal userMoney)
        {
            OrderState result = 0;
            if (money > userMoney)//待支付
            {
                switch (orderState)
                {
                    case OrderState.BackRentCheck:
                        result = OrderState.BackRentWaitPay;
                        break;
                    case OrderState.BackGoodsCheck:
                        result = OrderState.BackGoodsWaitPay;
                        break;
                    case OrderState.ExchangeGoodsCheck://换货只能支付，不从余额里面扣
                        result = OrderState.ExchaneGoodsWaitPay;
                        break;
                }
            }
            else
            {
                switch (orderState)
                {
                    case OrderState.BackRentCheck:
                        result = OrderState.BackRentWaitConfirm;
                        break;
                    case OrderState.BackGoodsCheck:
                        result = OrderState.BackGoodsWaitConfirm;
                        break;
                    case OrderState.ExchangeGoodsCheck:
                        result = OrderState.ExchaneGoodsWaitPay;
                        break;
                }
            }


            return result;
        }
        /// <summary>
        /// 修改金额，不改变订单状态
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="money"></param>
        /// <returns></returns>
        public Boolean ChangeMoney(int orderId, decimal money)
        {
            Boolean result = false;
            try
            {
                result = dal.ChangeMoney(orderId, money);
            }
            catch (Exception ex)
            {
                Log4net.RecordLog.RecordException("maq", "(orderId,money)", ex);
            }
            return result;
        }

        /// <summary>
        /// 商户结算
        /// </summary>
        /// <param name="merId"></param>
        /// <param name="orderId"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public Boolean MerchantSettlement(int merId, int orderId, OrderState orderState, out string message)
        {
            Boolean result = false;
            try
            {
                result = dal.MerchantSettlement(merId, orderState, orderId, out message);
            }
            catch (Exception ex)
            {
                message = "未知异常";
                Log4net.RecordLog.RecordException("maq", "(orderId,orderId,OrderState)", ex);
            }
           
            return result;
        }
        /// <summary>
        /// 判断当前用户要操作的订单和
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="merId"></param>
        /// <returns></returns>
        public Boolean IsMerOrder(int orderId, int merId)
        {
            Boolean result = false;
            try
            {
                result = dal.IsMerOrder(orderId, merId);
            }
            catch (Exception ex)
            {
                Log4net.RecordLog.RecordException("maq", "(orderId,merId)", ex);
            }
            return result;
        }
        #endregion
    }
}
