using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace KYJ.ZS.Commons.LuceneSearch
{
    public class SearchGoodsConditionInfo
    {
        /// <summary>
        /// 搜索类型"s"为出售商品,"r"为出租商品
        /// </summary>
        public string SearchType { get; set; }

        /// <summary>
        /// 标签类别
        /// </summary>
        public string Tags { get; set; }

        /// <summary>
        /// 一级分类
        /// </summary>
        public string YiCategoryId { get; set; }

        /// <summary>
        /// 二级分类
        /// </summary>
        public string ErCategoryId { get; set; }

        /// <summary>
        /// 三级分类
        /// </summary>
        public string SanCategoryId { get; set; }

        /// <summary>
        /// 价格（租金）最小值
        /// </summary>
        public string PriceMin { get; set; }

        /// <summary>
        /// 价格（租金）最大值
        /// </summary>
        public string PriceMax { get; set; }

        /// <summary>
        /// 品牌Id
        /// </summary>
        public string BrandId { get; set; }

        /// <summary>
        /// 搜索关键字
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 商品属性Id集合
        /// </summary>
        public string AttributesId { get; set; }

        /// <summary>
        /// 是否可以讲价
        /// </summary>
        public string IsBargain { get; set; }

        /// <summary>
        /// 排序参数：0销量降序，1，价格升序，2价格降序，3上架时间排序
        /// </summary>
        public string Sort { get; set; }

        /// <summary>
        /// 翻页参数：每页要多少数据
        /// </summary>
        public string PageSize { get; set; }

        /// <summary>
        /// 当前页
        /// </summary>
        public string PageIndex { get; set; }


        public static SearchGoodsConditionInfo GetSearchCondiction(string condition)
        {
            var search = new SearchGoodsConditionInfo();
            try
            {
                if (!string.IsNullOrEmpty(condition))
                {
                    string[] u1 = condition.Split("-".ToCharArray());
                    search.SearchType = u1[0]; //添加判断搜索类型的参数
                    for (int i = 1; i < u1.Length; i++)
                    {
                        InitCondictionInfo(u1[i], search);
                    }
                }
                return search;
            }
            catch (Exception)
            {
                    
                throw;
            }

        }

        public static void InitCondictionInfo(string str, SearchGoodsConditionInfo info)
        {
            char pa = str[0];
            string strChar = str.Substring(1);
            switch (pa)
            {
                case 'b': 
                    info.Tags = strChar; break;
                case 'c':
                    info.YiCategoryId = strChar;break;
                case 'd':
                    info.ErCategoryId = strChar;break;
                case 'e':
                    info.SanCategoryId = strChar;break;
                case 'o':
                    info.AttributesId = strChar;break;
                case 'p':
                    var pl = strChar.Split("_".ToCharArray());
                    info.PriceMin = pl[0];
                    info.PriceMax = pl[1];
                    break;
                case 'r':
                    info.BrandId = strChar;break;
                case 'w':
                    info.Title = strChar;break;
                case 'u':
                    info.IsBargain = strChar;break;
                case 'y':
                    info.Sort = strChar;break;
                case 's':
                    info.PageSize = strChar;break;
                case 'i':
                    info.PageIndex = strChar;break;
                default: break;

            }
        }


        public class Summary
        {
            [DataMember]
            public int TotalRecords { get; set; }
            [DataMember]
            public int PageSize { get; set; }
            [DataMember]
            public int PageIndex { get; set; }
            [DataMember]
            public int TotalPage { get; set; }
            [DataMember]
            public int RealRecords { get; set; }

        }

        public class Relusts<RelustsSummary, SaleGoods>
        {
            [DataMember(Order = 1)]
            public RelustsSummary Summary { get; set; }
            [DataMember(Order = 2)]
            public IList<SaleGoods> Goods { get; set; }
        }

    
    }


    




}
