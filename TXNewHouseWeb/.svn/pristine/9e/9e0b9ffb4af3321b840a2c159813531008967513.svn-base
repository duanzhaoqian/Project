using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TXCommons.MsgQueue;

namespace Commons
{
    public class MQHelper
    {
        public static CityData GetLucenexmlInfomation(string cityid)
        {
            return DictionaryManager<IEnumerable<CityData>>.dictionary["lucenexml"].First(r => r.CityIds.Split(',').Any(it => it == cityid));
        }

        public static CityData GetLucenexmlInfomationByMqName(string mqname)
        {
            return DictionaryManager<IEnumerable<CityData>>.dictionary["lucenexml"].First(r => r.MqName == mqname);
        }
        public static CityData GetLucenexmlInfomationByRoomMqName(string mqname)
        {
            return DictionaryManager<IEnumerable<CityData>>.dictionary["lucenexml"].First(r => r.RoomMqName == mqname);
        }
        public static CityData GetLucenexmlInfomation(int Id)
        {
            return DictionaryManager<IEnumerable<CityData>>.dictionary["lucenexml"].First(r => r.Id == Id);
        }
        /// <summary>
        /// 获取所有已开通的MQ名称
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<string> GetQueueNamesInfomation()
        {
            return DictionaryManager<IEnumerable<CityData>>.dictionary["lucenexml"].Select(it => it.MqName).Distinct();
        }

        /// <summary>
        /// 获取户型图所有已开通的MQ名称
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<string> GetRoomQueueNamesInfomation()
        {
            return DictionaryManager<IEnumerable<CityData>>.dictionary["lucenexml"].Select(it => it.RoomMqName).Distinct();
        }

        public static IEnumerable<int> GetLucenexmlIds()
        {
            return DictionaryManager<IEnumerable<CityData>>.dictionary["lucenexml"].Select(it => it.Id).Distinct();
        }

        public static int GetCityId(string cityName)
        {
            return MQHelp.GetCityId(cityName);
        }

        public static string GetCityName(int cityId)
        {
            return MQHelp.GetCityName(cityId);
        }
        /// <summary>
        /// 根据Lucenexml的ID获取相应的城市信息
        /// </summary>
        /// <param name="Id">Lucenexml ID</param>
        /// <returns></returns>
        public static string GetCityIdByLucenexml(int Id)
        {
            return DictionaryManager<IEnumerable<CityData>>.dictionary["lucenexml"].First(it => it.Id == Id).CityIds;
        }
        /// <summary>
        /// 获取所有lucene分组
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<CityData> GetLucenexmls()
        {
            return DictionaryManager<IEnumerable<CityData>>.dictionary["lucenexml"];
        }
        /// <summary>
        /// 设置全局词典
        /// </summary>
        public static void SetDictionary()
        {
            if (!DictionaryManager<IEnumerable<CityData>>.dictionary.ContainsKey("lucenexml"))
            {
                DictionaryManager<IEnumerable<CityData>>.dictionary.Add("lucenexml", MQHelp.GetLucenexmls());
            }
        }
    }
}
