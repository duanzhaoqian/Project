using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KYJ.ZS.Models.View
{
    /// <summary>
    /// 作者：maq
    /// 时间：2014年5月6日10:02:34
    /// 描述：商户主页viewmodel
    /// </summary>
    public class MerchantMainView
    {
        #region 订单相关
        private Dictionary<int, int> _orderStateNum;
        public Dictionary<int, int> OrderStateNum
        {
            set { _orderStateNum = value; }
        }
        /// <summary>
        /// 获取各个状态的订单量
        /// </summary>
        /// <param name="orderNum"></param>
        /// <returns></returns>
        public int GetOrderNum(int orderNum)
        {
            if (_orderStateNum.ContainsKey(orderNum))
            {
                return _orderStateNum[orderNum];
            }
            else
            {
                return 0;
            }
        } 
        #endregion
        #region 商品相关

        private Dictionary<int, int> _getRentalGoodsStateNum;

        public Dictionary<int, int> GetRentalGoodsStateNum
        {
            set { _getRentalGoodsStateNum = value; }
        }

        public int GetRentalGoodsNum(int type)
        {
            if (_getRentalGoodsStateNum.ContainsKey(type))
            {
                return _getRentalGoodsStateNum[type];
            }
            else
            {
                return 0;
            }
        } 
        #endregion
        #region 商户账户金额相关
        /// <summary>
        /// 账户总金额
        /// </summary>
        public decimal Money
        {
            set
            {
                decimal money = value;
                string[] arrStr = money.ToString("0.00").Split('.');
                LongMoney = arrStr[0];
                if (arrStr.Length == 1)
                {
                    PointMoney = "00";
                }
                else
                {
                    PointMoney = arrStr[1].ToString();
                }
            }
        }

        /// <summary>
        /// 获取金额整数部分
        /// </summary>
        public string LongMoney { get; set; }
        /// <summary>
        /// 获取金额小数部分
        /// </summary>
        public string PointMoney { get; set; } 
        #endregion

    }
}
