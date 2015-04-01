using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KYJ.ZS.Models.View
{
    /// <summary>
    /// 作者:maq
    /// 时间：2014年4月29日16:02:31
    /// 描述:商户商品管理搜索查询参数
    /// </summary>
    public class RentalGoodsesQueryPms
    {
        public RentalGoodsesQueryPms()
        {
            _monthPriceMax = int.MaxValue;
            _monthPriceMin = 0;
            _rentalNumMax = int.MaxValue;
            _rentalNumMin = 0;
            _googsName = "";
            _merchantNumber = "";
        }
        private string _googsName;
        /// <summary>
        /// 商品名称
        /// </summary>
        public string GoodsName
        {
            get
            {
                if (_googsName == null)
                {
                    _googsName = "";
                }
                return _googsName;
            }
            set { _googsName = value; }
        }

        private string _merchantNumber;
        /// <summary>
        /// 商家编码
        /// </summary>
        public string MerchantNumber
        {
            get
            {
                if (_merchantNumber == null)
                {
                    _merchantNumber = "";
                }
                return _merchantNumber;
            }
            set { _merchantNumber = value; }
        }

        private decimal? _monthPriceMin;
        /// <summary>
        /// 月租下限
        /// </summary>
        public decimal? MonthPriceMin
        {
            get
            {
                if (_monthPriceMin == null||_monthPriceMin==int.MinValue)
                {
                    _monthPriceMin = decimal.MinValue;
                }
                return _monthPriceMin;
            }
            set { _monthPriceMin = value; }
        }

        private decimal? _monthPriceMax;
        /// <summary>
        /// 月租上限
        /// </summary>
        public decimal? MonthPriceMax
        {
            get
            {
                if (_monthPriceMax == null||_monthPriceMax==int.MaxValue)
                {
                    _monthPriceMax = decimal.MaxValue;
                }
                return _monthPriceMax;
            }
            set { _monthPriceMax = value; }
        }

        private int? _rentalNumMin;
        /// <summary>
        /// 租用量下限
        /// </summary>
        public int? RentalNumMin
        {
            get
            {
                if (_rentalNumMin == null)
                {
                    _rentalNumMin = int.MinValue;
                }
                return _rentalNumMin;
            }
            set { _rentalNumMin = value; }
        }

        private int? _rentalNumMax;
        /// <summary>
        /// 租用量上限
        /// </summary>
        public int? RentalNumMax
        {
            get
            {
                if (_rentalNumMax == null)
                {
                    _rentalNumMax = int.MaxValue;
                }
                return _rentalNumMax;
            }
            set { _rentalNumMax = value; }
        }
        /// <summary>
        /// 当前页码  必传
        /// </summary>
        public int? PageIndex { get; set; }
    }
}
