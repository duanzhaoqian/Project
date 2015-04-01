using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KYJ.ZS.Models.RentalGoodses
{
   public class Manager_RentalGoodsAduitEntity
    {
       /// <summary>
       /// 推广商品ID
       /// </summary>
       public int GeneralizeGoodsId { get;set;}
       /// <summary>
       /// 商品ID
       /// </summary>
        public int GoodsId { get; set; }
       /// <summary>
       /// 商品名称
       /// </summary>
        public string GoodsName { get; set; }
       /// <summary>
       /// 月租价格
       /// </summary>
        public double MonthPrice { get; set; }
       /// <summary>
       /// 商户名称
       /// </summary>
        public string MerchantName { get; set; }
       /// <summary>
       /// 商家编号
       /// </summary>
        public string GoodsCode { get; set; }
       /// <summary>
       /// 品牌
       /// </summary>
        public string Brand { get; set; }
       /// <summary>
       /// 排序
       /// </summary>
        public int Sort { get; set; }

 
    }
    /// <summary>
    /// 排序商品搜索条件
    /// </summary>
   public class GeneralizeRentalGoodsSearchEntity
   {
       /// <summary>
       /// 商品名称
       /// </summary>
       public string  GoodsName { get; set; }
       /// <summary>
       /// 商户ID
       /// </summary>
       public int MerchantId { get; set; }
       /// <summary>
       /// 商户名称
       /// </summary>
       public string MerchantName { get; set; }
       /// <summary>
       /// 商家编号
       /// </summary>
       public string GoodsCode { get; set; }
   }
}
