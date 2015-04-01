using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KYJ.ZS.Commons.Index
{
    public class IndexCondition
    {
        /// Author：wwang
        /// Time：2014-04-17
        /// Desc: 搜索对象操作类
        /// <summary>
        /// 读取URL，生成Info对象
        /// </summary>
        /// <param name="condition">所有连接在一起的参数，例如0-0----------</param>
        /// <returns></returns>

        public static IndexGoodsConditionInfo GetSearchCondiction(string condition)
        {
            IndexGoodsConditionInfo info = new IndexGoodsConditionInfo();


            if (!string.IsNullOrEmpty(condition))
            {
                string[] u1 = condition.Split("-".ToCharArray());
                    for (int i = 0; i < u1.Length; i++)
                    {
                        InitCondictionInfo(u1[i], info);
                    }
            }
            return info;

        }
        public static void InitCondictionInfo(string str, IndexGoodsConditionInfo info)
        {
            if (!string.IsNullOrEmpty(str))
            {
                char pa = str[0];

                var strChar = str.Substring(1);
                switch (pa)
                {
                    case 'f':
                        info.GoodsNormId = strChar;
                        break;
                    case 'o':
                        var attr = strChar.Split('_');
                        var attrid = attr[0];
                        var attrvalueid = attr[1];
                        SearchAttr temp = new SearchAttr { AttributeId = attrid, AttributeValueId = attrvalueid };
                        info.Attrs.Add(temp);
                        break;
                  
                    case 't':
                        info.GoodsType = strChar;
                        break;
                    case 'p':
                        var p = strChar;
                        if (p == "1")
                        {
                            info.PriceBegin = "0";
                            info.PriceEnd = "599";
                        }
                        if(p=="2")
                        {
                            info.PriceBegin = "600";
                            info.PriceEnd = "1099";
                        }
                        if (p == "3")
                        {
                            info.PriceBegin = "1100";
                            info.PriceEnd = "1699";
                        }
                        if (p == "4")
                        {
                            info.PriceBegin = "1700";
                            info.PriceEnd = "2599";
                        }
                        if (p == "5")
                        {
                            info.PriceBegin = "2600";
                            info.PriceEnd = "4199";
                        }
                        if (p == "6")
                        {
                            info.PriceBegin = "4220";
                            info.PriceEnd = "7999";
                        }
                        if (p == "7")
                        {
                            info.PriceBegin = "8000";
                            info.PriceEnd = "89999";
                        }

                        break;
                    case 'w':
                        info.KeyWord = strChar.Substring(1);
                        break;
                    case 'y':
                            info.Sort = strChar;
                        break;
                    case 'i':
                        info.PageIndex =Convert.ToInt32(strChar);
                        break;
                    case 's':
                        info.PageSize = Convert.ToInt32(strChar);
                        break;
                    case 'c':
                        info.FirstlyCatId = strChar;
                        break;
                    case 'd':
                        info.SecondaryCatId = strChar;
                        break;
                    case 'e':
                        info.ThirdlyCatId = strChar;
                        break;
                    case 'r':
                        info.BrandId = strChar;
                        break;
                    default:
                        break;
                }

            }
        }
    }
}
