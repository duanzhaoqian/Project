using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KYJ.ZS.Models.RentalGoodses
{
    /// <summary>
    /// 作者：maq
    /// 时间：2014年4月25日
    /// 描述：商户后台仓库中的商品
    /// </summary>
    public class WareHouseGoodsEntity
    {
        private string _title;
        /// <summary>
        /// 标题
        /// </summary>
        public string Title
        {
            get { return _title; }
            set { _title = value; }
        }

        private int _status;
        /// <summary>
        /// 商品显示状态
        /// </summary>
        public RentalStatus Status
        {
            get
            {
                switch (_status)
                {
                    case 0:
                        return RentalStatus.UnKnown;
                        break;
                    case 1:
                        return RentalStatus.Shelves;
                        break;
                    case 2:
                        return RentalStatus.ShelvesOff;
                        break;
                    default:
                        return RentalStatus.UnKnown;
                        break;
                }
            }
        }

        public int SetStatus
        {
            set { _status = value; }
        }

        private decimal _monthprice;
        /// <summary>
        /// 月租金
        /// </summary>
        public decimal MonthPrice
        {
            get { return _monthprice; }
            set { _monthprice = value; }
        }

        private int _stock;
        /// <summary>
        /// 库存
        /// </summary>
        public int Stock
        {
            get { return _stock; }
            set { _stock = value; }
        }

        private int _rentNum;
        /// <summary>
        /// 租用量
        /// </summary>
        public int RentNum
        {
            get { return _rentNum; }
            set { _rentNum = value; }
        }

        private int _salesVolume;
        /// <summary>
        /// 销量
        /// </summary>
        public int SalesVolume
        {
            get { return _salesVolume; }
            set { _salesVolume = value; }
        }

        private DateTime _createTime;
        /// <summary>
        /// 
        /// </summary>
        public DateTime CreateTime
        {
            get { return _createTime; }
            set { _createTime = value; }
        }

        private int _id;
        /// <summary>
        /// 主键
        /// </summary>
        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        private string _guid;
        /// <summary>
        /// 唯一标识
        /// </summary>
        public string Guid
        {
            get { return _guid; }
            set { _guid = value; }
        }

    }
    /// <summary>
    /// 商品显示状态
    /// </summary>
    public enum RentalStatus
    {
        /// <summary>
        /// 未知
        /// </summary>
        UnKnown = 0,
        /// <summary>
        /// 上架
        /// </summary>
        Shelves = 1,
        /// <summary>
        /// 下架
        /// </summary>
        ShelvesOff = 2
    }
}
