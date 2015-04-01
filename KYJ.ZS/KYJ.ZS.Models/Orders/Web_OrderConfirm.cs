using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KYJ.ZS.Models.Orders
{
    public class Web_OrderConfirm
    {
        /// <summary>
        /// 商品Id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 商品GUId
        /// </summary>
        public string RentalGuid { get; set; }
        /// <summary>
        /// 商品名称
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 押金
        /// </summary>
        public double Deposit { get; set; }
        /// <summary>
        /// 运费模版Id
        /// </summary>
        public int FtempId { get; set; }
        /// <summary>
        /// 重量
        /// </summary>
        public double Weight { get; set; }
        /// <summary>
        /// 体积
        /// </summary>
        public double Volume { get; set; }

        /// <summary>
        /// 租期模版
        /// </summary>
        public string TempMonths { get; set; }

        /// <summary>
        /// 颜色名称
        /// </summary>
        public string ColorName  { get; set; }
        /// <summary>
        /// 颜色代码
        /// </summary>
        public string  ColorCode   { get; set; }
        /// <summary>
        /// 规格名称
        /// </summary>
        public string NormName { get; set; }

        /// <summary>
        /// 商品月价格
        /// </summary>
        public double MothPrice { get; set; }


        /// <summary>
        /// 商品数量
        /// </summary>
        public int GoodsNum { get; set; }
        /// <summary>
        /// 商品租期
        /// </summary>
        public int GoodsMonth { get; set; }
        /// <summary>
        /// 商品颜色Id
        /// </summary>
        public int GoodsColorId { get; set; }
        /// <summary>
        /// 商品规格Id
        /// </summary>
        public int GoodsNormId { get; set; }
        /// <summary>
        /// 商品最低租期
        /// </summary>
        public int GoodsLowestMonth { get; set; }
        /// <summary>
        /// 商品运费模版
        /// </summary>
        public string GoodsFreightTemplate { get; set; }






    }
}
