using System;
using System.Text;
using System.Reflection;

namespace TXCommons.Index
{
    public static class IndexConditionInfo
    {
        #region 楼盘搜索辅助工具
        /// <summary>
        /// 读取URL，生成Info对象
        /// </summary>
        /// <param name="condition">所有连接在一起的参数，例如0-0----------</param>
        /// <returns></returns>
        public static IndexPremisesConditionInfo GetSearchCondiction(string condition)
        {
            IndexPremisesConditionInfo info = new IndexPremisesConditionInfo();
            try
            {
                if (!string.IsNullOrEmpty(condition))
                {
                    string[] u1 = condition.Split("-".ToCharArray());
                    info.CityName = u1[0];
                    for (int i = 3; i < u1.Length; i++)
                    {
                        InitCondictionInfo(u1[i], info);
                    }

                }
                return info;
            }
            catch (Exception e)
            {
                Log4netService.RecordLog.RecordException("祝志华", string.Format("({0})", condition), e);
                throw;
            }
        }

        public static void InitCondictionInfo(string str, IndexPremisesConditionInfo info)
        {
            if (!string.IsNullOrEmpty(str))
            {
                char pa = str[0];
                if (Util.IsEnglish(str))
                {
                    if (string.IsNullOrEmpty(info.CityName))
                        info.CityName = str;
                    else if (string.IsNullOrEmpty(info.DistrictName))
                        info.DistrictName = str;
                    else
                        info.BusinessName = str;
                }
                else
                {
                    var strChar = str.Substring(1);
                    switch (pa)
                    {
                        case 'o':
                            info.SalesStatus = strChar;
                            break;
                        case 'l':
                            info.PropertyType = strChar;
                            break;
                        case 'p':
                            var ps = strChar.Split('_');
                            info.PriceBegin = ps[0]; info.PriceEnd = ps[1];
                            break;
                        case 'r':
                            info.Room = strChar;
                            break; 
                        case 'm':
                            info.HouseArea = strChar; //strChar.Split('_');
                            //info.AreaBegin = areas[0]; info.AreaEnd = areas[1];
                            break;
                        case 'x':
                            info.TrafficLine = strChar;
                            break;
                        case 'b':
                            info.TrafficStation = strChar;
                            break;
                        case 'h':
                            info.Ring = strChar;
                            break;
                        case 'd':
                            info.BuildingType = strChar;
                            break;
                        case 't':
                            info.Characteristic = strChar;//特色类型
                            break;
                        case 'c':
                            info.CharacteristicCount = strChar;//特色数量
                            break;
                        case 'k':
                            info.OpeningTime = strChar;
                            break;
                        case 'z':
                            info.Renovation = strChar;
                            break;
                        case 'w':
                            info.KeyWord = strChar.Substring(1);
                            break;
                        case 'y':
                            var num = Convert.ToInt32(strChar);
                            if (1 <= num && num <= 2)
                                info.PriceSort = strChar;
                            else if (3 <= num && num <= 4)
                                info.OpeningSort = strChar;
                            else if (5 <= num && num <= 6)
                                info.CheckInSort = strChar;
                            else if (7 <= num && num <= 8)
                                info.CharacteristicSort = strChar;
                            break;
                        case 'i':
                            info.PageIndex = strChar;
                            break;
                        case 'a':
                            info.PremisesID = strChar;
                            break;
                        case 's':
                            info.PageSize = strChar;
                            break;

                        default:
                            break;
                    }
                }
            }
        }

        /// <summary>
        /// 读取IndexPremisesConditionInfo对象，生成URL
        /// </summary>
        /// <param name="conditionInfo">IndexPremisesConditionInfo数据实体</param>
        /// <returns></returns>
        public static string GetSearchCondiction(IndexPremisesConditionInfo conditionInfo)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                PropertyInfo[] ProInfos = conditionInfo.GetType().GetProperties();
                for (int i = 0; i < ProInfos.Length; i++)
                {
                    sb.Append(ProInfos[i].GetValue(conditionInfo, null));
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
                Log4netService.RecordLog.RecordException<IndexPremisesConditionInfo>("祝志华", new IndexPremisesConditionInfo(), e);
                throw;
            }
        }

        /// <summary>
        /// 根据URL参数，获取INDEX所需要的参数
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public static string GetIndexSearchCondiction(string condition)
        {
            var info = GetSearchCondiction(condition);
            return GetSearchCondiction(info);
        }

        #endregion

        #region 排行搜索辅助工具

        /// <summary>
        /// 读取URL，生成Info对象
        /// </summary>
        /// <param name="condition">所有连接在一起的参数，例如0-0----------</param>
        /// <returns></returns>
        public static IndexRankingConditionInfo GetSearchRankingCondiction(string condition)
        {
            IndexRankingConditionInfo info = new IndexRankingConditionInfo();
            try
            {
                if (!string.IsNullOrEmpty(condition))
                {
                    string[] u1 = condition.Split("-".ToCharArray());
                    info.ProvinceID = u1[0];
                    info.CityID = u1[1];
                    info.DistrictID = u1[2];
                    info.BusinessID = u1[3];
                    info.PremisesID = u1[4];
                    info.Ranking = u1[5];
                    info.PageIndex = u1[6];
                    info.PageSize = u1[7];
                }
                return info;
            }
            catch (Exception e)
            {
                Log4netService.RecordLog.RecordException("祝志华", string.Format("({0})", condition), e);
                throw;
            }
        }

        /// <summary>
        /// 读取IndexPremisesConditionInfo对象，生成URL
        /// </summary>
        /// <param name="conditionInfo">IndexPremisesConditionInfo数据实体</param>
        /// <returns></returns>
        public static string GetSearchRankingCondiction(IndexRankingConditionInfo conditionInfo)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                PropertyInfo[] ProInfos = conditionInfo.GetType().GetProperties();
                for (int i = 0; i < ProInfos.Length; i++)
                {
                    sb.Append(ProInfos[i].GetValue(conditionInfo, null));
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
                Log4netService.RecordLog.RecordException<IndexPremisesConditionInfo>("祝志华", new IndexPremisesConditionInfo(), e);
                throw;
            }
        }
        #endregion

        #region 户型搜索辅助工具

        /// <summary>
        /// 读取URL，生成Info对象
        /// </summary>
        /// <param name="condition">所有连接在一起的参数，例如0-0----------</param>
        /// <returns></returns>
        public static IndexRoomConditionInfo GetSearchRoomCondiction(string condition)
        {
            IndexRoomConditionInfo info = new IndexRoomConditionInfo();
            try
            {
                if (!string.IsNullOrEmpty(condition))
                {
                    string[] u1 = condition.Split("-".ToCharArray());
                    info.CityId = u1[0];
                    info.PremisesID = u1[1];
                    info.BuildingId = u1[2];
                    info.Room = u1[3];
                    info.BeginArea = u1[4];
                    info.EndArea = u1[5];
                    info.RID = u1[6];
                    info.PageIndex = u1[7];
                    info.PageSize = u1[8];
                }
                return info;
            }
            catch (Exception e)
            {
                Log4netService.RecordLog.RecordException("祝志华", string.Format("({0})", condition), e);
                throw;
            }
        }

        /// <summary>
        /// 读取IndexRoomConditionInfo对象，生成URL
        /// </summary>
        /// <param name="conditionInfo">IndexRoomConditionInfo数据实体</param>
        /// <returns></returns>
        public static string GetSearchRoomCondiction(IndexRoomConditionInfo conditionInfo)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                PropertyInfo[] ProInfos = conditionInfo.GetType().GetProperties();
                for (int i = 0; i < ProInfos.Length; i++)
                {
                    sb.Append(ProInfos[i].GetValue(conditionInfo, null));
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
                Log4netService.RecordLog.RecordException<IndexPremisesConditionInfo>("祝志华", new IndexPremisesConditionInfo(), e);
                throw;
            }
        }

        #endregion
    }
}
