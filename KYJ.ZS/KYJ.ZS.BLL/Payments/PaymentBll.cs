using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KYJ.ZS.DAL.Payments;
using KYJ.ZS.Models.Payments;

namespace KYJ.ZS.BLL.Payments
{
    public class PaymentBll
    {
        private PaymentDal _dal = null;

        public PaymentBll()
        {
            if (_dal == null)
            {
                _dal = new PaymentDal();
            }
        }

        /// <summary>
        /// 获取指定订单编号的订单需支付金额信息
        /// </summary>
        /// <param name="ordernum">订单编号</param>
        /// <param name="userid">用户ID</param>
        /// <returns>PaymentEntity数据实体</returns>
        public PaymentEntity GetPaymentByOrderNum(string ordernum, int userid)
        {
            return _dal.GetPaymentByOrderNum(ordernum, userid);
        }

        /// <summary>
        /// 获取指定订单编号的订单续租金额信息
        /// </summary>
        /// <param name="ordernum">订单编号</param>
        /// <param name="userid">用户ID</param>
        /// <returns>PaymentEntity数据实体</returns>
        public PaymentEntity GetTenancyByOrderNum(string ordernum, int userid)
        {
            return _dal.GetTenancyByOrderNum(ordernum, userid);
        }
        /// <summary>
        /// 获取指定订单编号的订单购买金额信息
        /// </summary>
        /// <param name="ordernum">订单编号</param>
        /// <param name="userid">用户ID</param>
        /// <returns>PaymentEntity数据实体</returns>
        public PaymentEntity GetPayoutByOrderNum(string ordernum, int userid)
        {
            var endtity = _dal.GetTenancyByOrderNum(ordernum, userid);
            if (endtity != null)
            {
                var paymonth = _dal.GetPayoutByOrderGoodesID(endtity.OrderGoodesID);
                endtity.TotalPrice = endtity.MonthPrice * (endtity.Month - paymonth) + (endtity.Latefees * endtity.LatefeesDay);
            }
            return endtity;
        }
        /// <summary>
        /// 获取订单赔损信息
        /// </summary>
        /// <param name="ordernum">订单编号</param>
        /// <param name="userid">用户ID</param>
        /// <returns>PaymentEntity数据实体</returns>
        public PaymentEntity GetClaimByOrderNum(string ordernum, int userid)
        {
            return _dal.GetClaimByOrderNum(ordernum, userid);
        }
        /// <summary>
        /// 变更订单状态为已支付,其它状态不允许使用该方法，订单状态：3 支付订单完成后“待发货”
        /// </summary>
        /// <param name="ordernum">订单号</param>
        /// <param name="userid">用户ID</param>
        /// <returns></returns>
        public int UpdateOrderStatus(string ordernum, int userid)
        {
            return _dal.UpdateOrderStatus(ordernum, userid);
        }

        /// <summary>
        /// 验证是否需要续租
        /// </summary>
        /// <param name="ordernum">订单号</param>
        /// <param name="userid">用户ID</param>
        /// <returns></returns>
        public bool IsRelet(string ordernum, int userid)
        {
            return _dal.IsRelet(ordernum, userid);
        }

        /// <summary>
        /// 订单续租
        /// </summary>
        /// <param name="ordernum">订单号</param>
        /// <param name="userid">用户ID</param>
        /// <returns></returns>
        public int OrderRelet(string ordernum, int userid)
        {
            return _dal.OrderRelet(ordernum, userid);
        }

        /// <summary>
        /// 订单购买支付
        /// </summary>
        /// <param name="ordernum">订单号</param>
        /// <param name="userid">用户ID</param>
        /// <returns></returns>
        public int OrderBuyout(string ordernum, int userid)
        {
            return _dal.OrderBuyout(ordernum, userid);
        }
        /// <summary>
        /// 赔付
        /// </summary>
        /// <param name="ordernum">订单号</param>
        /// <param name="userid">用户ID</param>
        /// <param name="state">需变更订单ID</param>
        /// <returns></returns>
        public int OrderCliam(string ordernum, int userid)
        {
            return _dal.OrderCliam(ordernum, userid);
        }
        /// <summary>
        /// 商户帐户金额操作函数
        /// </summary>
        /// <param name="mid">商户ID</param>
        /// <param name="price">操作金额</param>
        /// <param name="mlog_type">日志类型 0 未知，1 充值，2 提现，3 租金4驳回</param>
        /// <param name="mlog_state">日志状态 0 未知，1 出账，2 入账</param>
        /// <param name="mlog_desc">日志说明</param>
        /// <returns></returns>
        public int MerhantAmount(int mid, decimal price, int mlog_type, int mlog_state, string mlog_desc)
        {
            return _dal.MerhantAmount(mid, price, mlog_type, mlog_state, mlog_desc);
        }
    }
}
