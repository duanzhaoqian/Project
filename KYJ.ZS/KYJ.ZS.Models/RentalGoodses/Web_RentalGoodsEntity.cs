using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KYJ.ZS.Models.RentalGoodses
{
    /// <summary>
    /// 作者：邓福伟
    /// 时间：2014-04-22
    /// 描述：Web前端，租售产品详情
    /// </summary>
    public class Web_RentalGoodsEntity
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
        /// 商品所在地
        /// </summary>
        public string Location { get; set; }
        /// <summary>
        /// 商品名称
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 商品条形码
        /// </summary>
        public string BarCode { get; set; }
        /// <summary>
        /// 商品原价格
        /// </summary>
        public double Price { get; set; }
        /// <summary>
        /// 带纳金
        /// </summary>
        public double LateFees { get; set; }
        /// <summary>
        /// 押金
        /// </summary>
        public double Deposit { get; set; }
        /// <summary>
        /// 运费模版Id
        /// </summary>
        public int FtempId { get; set; }
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime BeginTime { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime EndTime { get; set; }
        /// <summary>
        /// 重量
        /// </summary>
        public double Weight { get; set; }
        /// <summary>
        /// 体积
        /// </summary>
        public double Volume { get; set; }
        /// <summary>
        /// 分类Id
        /// </summary>
        public int CategoryId { get; set; }
        /// <summary>
        /// 品牌Id
        /// </summary>
        public int BrandId { get; set; }
        /// <summary>
        /// 品牌名称
        /// </summary>
        public string BrandName { get; set; }


        /// <summary>
        /// 商户Id
        /// </summary>
        public int MerchantId { get; set; }
        /// <summary>
        /// 商铺GUId
        /// </summary>
        public string MerchantGuid { get; set; }
        /// <summary>
        /// 商铺名称
        /// </summary>
        public string MerchantName { get; set; }



        /// <summary>
        /// 商品月价格
        /// </summary>
        public string MothPrice { get; set; }
        /// <summary>
        /// 商品总数量
        /// </summary>
        public int Number { get; set; }
        /// <summary>
        /// 商品编码
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 租期模版
        /// </summary>
        public string TempMonths { get; set; }

        /// <summary>
        /// 商品属性扩展
        /// </summary>
        public string OtherAttrs { get; set; }
        /// <summary>
        /// 商品属性名称扩展
        /// </summary>
        public string OtherAttrName { get; set; }
        /// <summary>
        /// 商品描述扩展
        /// </summary>
        public string OtherDesc { get; set; }
        /// <summary>
        /// 商品颜色扩展
        /// </summary>
        public string OtherColors { get; set; }
        /// <summary>
        /// 商品规格扩展
        /// </summary>
        public string OtherNorms { get; set; }
        /// <summary>
        /// 商品价格和数量扩展
        /// </summary>
        public string OtherPrices { get; set; }
        
        

        /// <summary>
        /// 收藏数
        /// </summary>
        /// <returns></returns>
        public int CollectionCount { get; set; }
        /// <summary>
        /// 运费模版
        /// </summary>
        public string OtherFreightTemplate { get; set; }

        /// <summary>
        /// 浏览量
        /// </summary>
        public int BrowseAmount { get; set; }

    }
}
