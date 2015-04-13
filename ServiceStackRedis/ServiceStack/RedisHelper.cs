using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KYJ.ServiceStack;

namespace ServiceStack
{
    public class RedisHelper
    {
        private static Dictionary<int, string> dicCity = null;

        static RedisHelper()
        {
            try
            {
                Init();
            }
            catch (Exception ex)
            {
            }
        }

        public static void Init()
        {
            #region 初始化98个城市

            dicCity = new Dictionary<int, string>()
                {
                    {304, "沈阳市"},
                    {119, "哈尔滨市"},
                    {257, "济南市"},
                    {224, "武汉市"},
                    {403, "重庆市"},
                    {212, "西安市"},
                    {385, "石家庄市"},
                    {232, "长春市"},
                    {146, "呼和浩特市"},
                    {374, "太原市"},
                    {102, "郑州市"},
                    {195, "合肥市"},
                    {248, "无锡市"},
                    {245, "苏州市"},
                    {312, "宁波市"},
                    {318, "福州市"},
                    {325, "厦门市"},
                    {135, "南昌市"},
                    {174, "长沙市"},
                    {352, "汕头市"},
                    {361, "珠海市"},
                    {114, "海口市"},
                    {115, "三亚市"},
                    {337, "南宁市"},
                    {107, "贵阳市"},
                    {78, "昆明市"},
                    {288, "拉萨市"},
                    {395, "兰州市"},
                    {410, "西宁市"},
                    {365, "银川市"},
                    {284, "乌鲁木齐"},
                    {386, "唐山市"},
                    {384, "秦皇岛市"},
                    {270, "淄博市"},
                    {268, "烟台市"},
                    {266, "威海市"},
                    {249, "徐州市"},
                    {242, "连云港市"},
                    {244, "南通市"},
                    {252, "镇江市"},
                    {240, "常州市"},
                    {309, "嘉兴市"},
                    {310, "金华市"},
                    {314, "绍兴市"},
                    {315, "台州市"},
                    {316, "温州市"},
                    {323, "泉州市"},
                    {342, "东莞市"},
                    {346, "惠州市"},
                    {343, "佛山市"},
                    {360, "中山市"},
                    {347, "江门市"},
                    {358, "湛江市"},
                    {328, "北海市"},
                    {332, "桂林市"},
                    {381, "邯郸市"},
                    {293, "鞍山市"},
                    {298, "抚顺市"},
                    {233, "吉林市"},
                    {126, "齐齐哈尔市"},
                    {117, "大庆市"},
                    {143, "包头市"},
                    {368, "大同市"},
                    {92, "洛阳市"},
                    {267, "潍坊市"},
                    {203, "芜湖市"},
                    {251, "扬州市"},
                    {308, "湖州市"},
                    {317, "舟山市"},
                    {326, "漳州市"},
                    {187, "株洲市"},
                    {341, "潮州市"},
                    {336, "柳州市"},
                    {380, "承德市"},
                    {378, "保定市"},
                    {297, "丹东市"},
                    {91, "开封市"},
                    {88, "安阳市"},
                    {265, "泰安市"},
                    {264, "日照市"},
                    {189, "蚌埠市"},
                    {198, "黄山市"},
                    {247, "泰州市"},
                    {322, "莆田市"},
                    {320, "南平市"},
                    {134, "九江市"},
                    {229, "宜昌市"},
                    {227, "襄樊市"},
                    {185, "岳阳市"},
                    {359, "肇庆市"},
                    {161, "乐山市"},
                    {165, "绵阳市"},
                    {79, "丽江市"},
                    {214, "延安市"},
                    {213, "咸阳市"},
                    {207, "宝鸡市"}
                };

            #endregion
        }

        public static bool AddItemToList<T>(T info, string key, DateTime? expireDateTime, Enum FunctionTypeEnum, int CityId)
        {
            return KYJ.ServiceStack.Redis.AddItemToList<T>(key, info, expireDateTime, FunctionTypeEnum, GetRedisTypeByCityId(CityId));
        }

        public static T GetAllItemsFromList<T>(string key, Enum FunctionTypeEnum, int CityId)
        {
            return KYJ.ServiceStack.Redis.GetAllItemsFromList<T>(key, FunctionTypeEnum, GetRedisTypeByCityId(CityId));
        }

        public static T GetValues<T>(Enum FunctionTypeEnum, int CityId, string keys)
        {
            return KYJ.ServiceStack.Redis.GetValues<T>(FunctionTypeEnum, GetRedisTypeByCityId(CityId), keys);
        }

        public static double GetNextSequence(Enum FunctionTypeEnum, int CityId, string key)
        {
            return KYJ.ServiceStack.Redis.GetNextSequence(FunctionTypeEnum, GetRedisTypeByCityId(CityId), key);
        }
        public static double GetNextSequence(Enum FunctionTypeEnum, int CityId, string key, DateTime expireDateTime)
        {
            return KYJ.ServiceStack.Redis.GetNextSequence(FunctionTypeEnum, GetRedisTypeByCityId(CityId), key, expireDateTime);
        }

        public static int GetNextSequence<T>(Enum FunctionTypeEnum, int CityId)
        {
            return KYJ.ServiceStack.Redis.GetNextSequence<T>(FunctionTypeEnum, GetRedisTypeByCityId(CityId));
        }

        public static void RemoveAllFromList(string key, Enum FunctionTypeEnum, int CityId)
        {
            KYJ.ServiceStack.Redis.Remove(key, FunctionTypeEnum, GetRedisTypeByCityId(CityId));
        }

        public static void RemoveItemFromList<T>(string key, T info, Enum FunctionTypeEnum, int CityId)
        {
            KYJ.ServiceStack.Redis.RemoveItemFromList<T>(key, info, FunctionTypeEnum, GetRedisTypeByCityId(CityId));
        }

        public static void AddItemToSortedSet<T>(T info, string key, double scores, DateTime? expireDateTime, Enum FunctionTypeEnum, int CityId)
        {
            KYJ.ServiceStack.Redis.AddItemToSortedSet<T>(key, info, scores, expireDateTime, FunctionTypeEnum, GetRedisTypeByCityId(CityId));
        }

        public static bool RemoveItemFromSortedSet<T>(string key, T info, Enum FunctionTypeEnum, int CityId)
        {
            return KYJ.ServiceStack.Redis.RemoveItemFromSortedSet<T>(key, info, FunctionTypeEnum, GetRedisTypeByCityId(CityId));
        }

        public static T GetAllItemsFromSortedSet<T>(string key, Enum FunctionTypeEnum, int CityId)
        {
            return KYJ.ServiceStack.Redis.GetAllItemsFromSortedSet<T>(key, FunctionTypeEnum, GetRedisTypeByCityId(CityId));
        }

        public static IDictionary<T, double> GetAllWithScoresFromSortedSet<T>(string key, Enum FunctionTypeEnum, int CityId)
        {
            return KYJ.ServiceStack.Redis.GetAllWithScoresFromSortedSet<T>(key, FunctionTypeEnum, GetRedisTypeByCityId(CityId));
        }


        public static double GetItemScoreInSortedSet<T>(string key, T info, Enum FunctionTypeEnum, int CityId)
        {
            return KYJ.ServiceStack.Redis.GetItemScoreInSortedSet<T>(key, info, FunctionTypeEnum, GetRedisTypeByCityId(CityId));
        }

        public static int GetSortedSetCount<T>(string key, Enum FunctionTypeEnum, int CityId)
        {
            return KYJ.ServiceStack.Redis.GetSortedSetCount(key, FunctionTypeEnum, GetRedisTypeByCityId(CityId));
        }

        public static IDictionary<T, double> GetRangeWithScoresFromSortedSetByHighestScore<T>(string key, Enum FunctionTypeEnum, int CityId)
        {
            return KYJ.ServiceStack.Redis.GetRangeWithScoresFromSortedSetByHighestScore<T>(key, FunctionTypeEnum, GetRedisTypeByCityId(CityId));
        }

        public static IDictionary<T, double> GetRangeWithScoresFromSortedSetByLowestScore<T>(string key, Enum FunctionTypeEnum, int CityId)
        {
            return KYJ.ServiceStack.Redis.GetRangeWithScoresFromSortedSetByLowestScore<T>(key, FunctionTypeEnum, GetRedisTypeByCityId(CityId));
        }

        public static bool SetValue<T>(string key, T info, DateTime? expireDateTime, Enum FunctionTypeEnum, int CityId)
        {
            return KYJ.ServiceStack.Redis.Set<T>(key, info, expireDateTime, FunctionTypeEnum, GetRedisTypeByCityId(CityId));
        }

        public static T GetValue<T>(string key, Enum FunctionTypeEnum, int CityId)
        {
            return KYJ.ServiceStack.Redis.Get<T>(key, FunctionTypeEnum, GetRedisTypeByCityId(CityId));
        }
        public static Enum GetRedisTypeByCityId(int CityId)
        {
            if (CityId == 0)
            {
                return null;
            }
            if (dicCity.ContainsKey(CityId))
            {
                //如果是新开通的98个城市就放到other服务器
                return CityEnum.Other;
            }
            switch (CityId)
            {
                case 253:
                    return CityEnum.BeiJing;
                case 239:
                    return CityEnum.ShangHai;
                case 344:
                    return CityEnum.GuangZhou;
                case 355:
                    return CityEnum.ShenZhen;
                case 205:
                    return CityEnum.TianJin;
                case 263:
                    return CityEnum.QingDao;
                case 307:
                    return CityEnum.HangZhou;
                case 155:
                    return CityEnum.ChengDu;
                case 243:
                    return CityEnum.NanJing;
                case 296:
                    return CityEnum.Dalian;
                default:
                    return null;
            }
        }
    }
}
