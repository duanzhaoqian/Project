using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KYJ.ZS.Commons.Index
{
    /// <summary>
    /// 作者：wwang
    /// 时间：2014-5-14
    /// 描述：闲置物品条件操作类
    /// </summary>
    public class SaleGoodsCondition
    {
        /// Author：wwang
        /// Time：2014-05-14
        /// Desc: 搜索对象操作类
        /// <summary>
        /// 读取URL，生成Info对象
        /// </summary>
        /// <param name="condition">所有连接在一起的参数，例如0-0----------</param>
        /// <returns></returns>

        public static SaleGoodsConditionInfo GetSearchCondiction(string condition)
        {
            SaleGoodsConditionInfo info = new SaleGoodsConditionInfo();


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
        public static void InitCondictionInfo(string str, SaleGoodsConditionInfo info)
        {
            if (!string.IsNullOrEmpty(str))
            {
                char pa = str[0];

                var strChar = str.Substring(1);
                switch (pa)
                {
                    
                    case 'b':
                        info.TagId = strChar;
                        break;
                    case 'y':
                            info.Sort = strChar;
                        break;
                    case 'i':
                        info.PageIndex =Convert.ToInt32(strChar);
                        break;
                    case 's':
                        info.PageSize =Convert.ToInt32(strChar);
                        break;
                    case 'u':
                        info.Isbargain=strChar=="1"?true:false;
                        break;
                    case 'v':
                        info.IsNew = strChar == "1" ? true : false;
                        break;
                    default:
                        break;
                }

            }
        }
    }
}
