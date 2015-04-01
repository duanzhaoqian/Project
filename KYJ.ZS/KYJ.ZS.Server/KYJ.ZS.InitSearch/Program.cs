using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KYJ.ZS.Commons;

namespace KYJ.ZS.InitSearch
{
    using KYJ.ZS.Search.Api;

    class Program
    {
        static void Main(string[] args)
        {
            var opt = new LuceneOpt();

            opt.AttributesIndexPath = Auxiliary.ConfigKey("AttributesIndexPath");
            opt.BrandsIndexPath = Auxiliary.ConfigKey("BrandsIndexPath");
            opt.CategoriesIndexPath = Auxiliary.ConfigKey("CategoriesIndexPath");
            opt.SaleGoodsesIndexPath = Auxiliary.ConfigKey("SaleGoodsesIndexPath");
            opt.RentalGoodsesIndexPath = Auxiliary.ConfigKey("RentalGoodsesIndexPath");

            opt.LogPath = Auxiliary.ConfigKey("LogPath");
            int count = 0;
            count= opt.AddDocForBrands("");
            Console.WriteLine("品牌数据生成" + count);
            count= opt.AddDocForAttributes("");
            Console.WriteLine("属性数据生成" + count);
            count = opt.AddDocForCategories("");
            Console.WriteLine("分类数据生成" + count);
            count = opt.AddDocForSaleGoodses("");
            Console.WriteLine("出售商品数据生成" + count);
            count = opt.AddDocForRentalGoodses("");
            Console.WriteLine("出租商品数据生成" + count);

            Console.Read();
        }
    }
}
