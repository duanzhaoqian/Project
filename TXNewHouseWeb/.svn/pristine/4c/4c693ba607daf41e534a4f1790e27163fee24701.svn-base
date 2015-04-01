using System;
using TXCommons.Index;

namespace TXCommons.SEO
{
    public class SeoCommon
    {

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
                    info.Type = u1[2];
                    for (int i = 3; i < u1.Length; i++)
                    {
                        InitCondictionInfo(u1[i], info);
                    }
                }
                return info;
            }
            catch (Exception e)
            {
                Log4netService.RecordLog.RecordException("sunlin", string.Format("({0})", condition), e);
                throw;
            }
        }

        /// <summary>
        /// 生成搜索对象
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static void InitCondictionInfo(string str, IndexPremisesConditionInfo info)
        {
            if (!string.IsNullOrEmpty(str))
            {
                char pa = str[0];
                if (Util.IsEnglish(str))
                {
                    //if (string.IsNullOrEmpty(info.CityName))
                    //    info.CityName = str == "beijing" ? "北京" : "上海";
                    //else if (string.IsNullOrEmpty(info.DistrictPY))
                    //    info.DistrictPY = str;
                    //else
                    //    info.BusinessPY = str;

                    if (string.IsNullOrEmpty(info.DistrictPY))
                        info.DistrictPY = str;
                    else
                        info.BusinessPY = str;
                }
                else
                {
                    var strChar = str.Substring(1);
                    switch (pa)
                    {
                        case 'o':
                            info.SalesStatus = PremisesType.GetSeoSalesStatus(strChar);
                            break;
                        case 'l':
                            info.PropertyType = PremisesType.GetSeoPropertyType(strChar);
                            break;
                        case 'p':
                            var ps = PremisesType.GetSeoPrice(strChar).Split('-');
                            info.PriceBegin = ps[0]; info.PriceEnd = ps[1];
                            break;
                        case 'r':
                            info.Room = PremisesType.GetSeoHouseTypes(strChar);
                            break;
                        case 'm':
                            var areas = PremisesType.GetSeoArea(strChar).Split('-');
                            info.AreaBegin = areas[0]; info.AreaEnd = areas[1];
                            break;
                        case 'x':
                            info.DistrictPY = strChar;
                            break;
                        case 'b':
                            info.BusinessPY = strChar;
                            break;
                        case 'h':
                            info.Ring = PremisesType.GetSeoRing(strChar);
                            break;
                        case 'd':
                            info.BuildingType = PremisesType.GetSeoBuildingType(strChar);
                            break;
                        case 't'://项目特色
                            info.Characteristic = PremisesType.GetSeoCharacteristic(strChar);
                            break;
                        case 'k':
                            info.OpeningTime = PremisesType.GetSeoOpeningTime(strChar);
                            break;
                        case 'z':
                            info.Renovation = PremisesType.GetSeoRenovation(strChar);
                            break;
                        case 'w':
                            info.KeyWord = strChar.Substring(1);
                            break;
                        default:
                            break;
                    }
                }
            }
        }

    }
}
