using System;
using System.Text;
using System.Reflection;

namespace TXCommons.LuceneSearch
{
    public class SearchNHouseInfo
    {
        /// <summary>
        /// 城市拼音  1
        /// </summary>
        public string CityPY { get; set; }
        /// <summary>
        ///房屋搜索类型  2
        /// </summary>
        public string SearchType { get; set; }
        /// <summary>
        /// 物业类型  3
        /// </summary>
        public string PropertyType { get; set; }
        /// <summary>
        ///区域搜索类型 quyu:区域 sub:地铁  4
        /// </summary>
        public string Type { get; set; }
        /// <summary>
        /// type=quyu:区域Id type=sub:地铁   5
        /// </summary>
        public string DistrictPY { get; set; }
        /// <summary>
        /// type=quyu:商圈Id type=sub:站点   6
        /// </summary>
        public string BusinessPY { get; set; }

        /// <summary>
        /// 出租类型 7
        /// </summary>
        public string RentType { get; set; }
        /// <summary>
        /// 价格区间 8 开始
        /// </summary>
        public string PriceBegin { get; set; }
        /// <summary>
        /// 价格区间 8 结束
        /// </summary>
        public string PriceEnd { get; set; }
        /// <summary>
        /// 面积 9 开始
        /// </summary>
        public string AreaBegin { get; set; }

        /// <summary>
        /// 面积 9 结束
        /// </summary>
        public string AreaEnd { get; set; }
        /// <summary>
        /// 居室 户型 10
        /// </summary>
        public string Room { get; set; }

        /// <summary>
        /// 配套设施 11
        /// </summary>
        public string Facilities { get; set; }

        /// <summary>
        /// 排序字段名称  12
        /// </summary>
        public string OrderByName { get; set; }

        /// <summary>
        /// 装修情况  13
        /// </summary>
        public string Renovation { get; set; }

        /// <summary>
        /// 出价状态  14  
        /// </summary>
        public string BidStatus { get; set; }

        /// <summary>
        /// 房源状态字段 15
        /// </summary>
        public string IsOnePrice { get; set; }

        /// <summary>
        /// 是否收中介费 16
        /// </summary>
        public string IsAgency { get; set; }

        /// <summary>
        /// 出价类型 17
        /// </summary>
        public string PType { get; set; }

        /// <summary>
        /// 用户类型 18
        /// </summary>
        public string UserType { get; set; }

     
        /// <summary>
        /// 当前页  19
        /// </summary>
        public string PageIndex { get; set; }

        /// <summary>
        /// 搜索关键字 20
        /// </summary>
        public string SearchKeyword { get; set; }
        /// <summary>
        /// 是否免税
        /// </summary>
        public string IsTxfree { get; set; }
        /// <summary>
        /// 是否满5年
        /// </summary>
        public string IsFullYears { get; set; }
        /// <summary>
        /// 是否唯一
        /// </summary>
        public string IsUnique { get; set; }
        /// <summary>
        /// 住宅类别
        /// </summary>
        public string HouseType { get; set; }
        /// <summary>
        /// 页数 21
        /// </summary>
        public string PageSize { get; set; }

        /// <summary>
        /// 读取URL，生成Info对象
        /// </summary>
        /// <param name="Condition">所有连接在一起的参数，例如0-0----------</param>
        /// <returns></returns>
        public static SearchNHouseInfo GetSearchCondiction(string Condition)
        {
            SearchNHouseInfo s = new SearchNHouseInfo();
            try
            {
                if (!string.IsNullOrEmpty(Condition))
                {
                    string[] u1 = Condition.Split("-".ToCharArray());
                    s.CityPY = u1[0];
                    s.SearchType = u1[1];
                    s.PropertyType = u1[2];
                    s.Type = u1[3];
                    
                    for (int i = 4; i < u1.Length; i++)
                    {
                        InitCondictionInfo(u1[i], s);
                    }

                }
                return s;
            }
            catch (Exception e)
            {
                Log4netService.RecordLog.RecordException("崔利国", string.Format("({0})", Condition), e);
                throw;
            }
        }

        public static  void InitCondictionInfo(string str,SearchNHouseInfo info)
        {
            char pa = str[0];
            if(Util .IsEnglish(str)&&pa!='k')
            {
                if (string.IsNullOrEmpty(info.DistrictPY))
                    info.DistrictPY = str;
                else
                    info.BusinessPY = str;
            }
            else
            {
                
                string strChar = str.Substring(1);
                switch (pa)
                {
                    case 'a': info.DistrictPY = strChar; break;
                    case 'b': info.BusinessPY = strChar; break;
                    case 'v': info.RentType = strChar; break;
                    case 't': info.PriceBegin = strChar; break;
                    case 'u': info.PriceEnd = strChar; break;
                    case 'm': info.AreaBegin = strChar; break;
                    case 'n': info.AreaEnd = strChar; break;
                    case 'r': info.Room = strChar; break;
                    case 'p': info.Facilities = strChar; break;
                    case 's': info.OrderByName = strChar; break;
                    case 'q': info.Renovation = strChar; break;
                    case 'h': info.BidStatus = strChar; break;
                    case 'o': info.IsOnePrice = strChar; break;
                    case 'y': info.IsAgency = strChar; break;
                    case 'z': info.PType= strChar; break;
                    case 'j': info.UserType = strChar; break;
                    case 'i': info.PageIndex= strChar; break;
                    case 'k': info.SearchKeyword = strChar; break;
                    case 'w': info.IsUnique = strChar; break;
                    case 'g': info.IsTxfree = strChar; break;
                    case 'e': info.IsFullYears = strChar; break;
                    case 'd': info.HouseType = strChar; break;
                    case 'c': info.PageSize = strChar; break;
                    default: break;
                }
            }
        }
        /// <summary>
        /// 根据Info 生成url
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string GetSearchUrl(SearchNHouseInfo s)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                PropertyInfo[] ProInfos = s.GetType().GetProperties();
                for (int i = 0; i < ProInfos.Length; i++)
                {
                    sb.Append(ProInfos[i].GetValue(s, null));
                    sb.Append("-");
                }
                if (sb.Length > 0)
                {
                    sb.Remove(sb.Length - 1, 1);
                }
                return sb.ToString();
            }
            catch (Exception e)
            {
                Log4netService.RecordLog.RecordException<SearchNHouseInfo>("崔利国", new SearchNHouseInfo(), e);
                throw;
            }

        }
    }
}
