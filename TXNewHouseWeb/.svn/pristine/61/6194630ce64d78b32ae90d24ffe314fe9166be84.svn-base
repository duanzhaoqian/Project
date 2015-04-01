using System;
using System.Collections.Generic;
using System.Linq;

namespace TXCommons.MsgQueue
{
    public class MQHelp
    {
        public static List<CityData> CityList = new List<CityData>();


        static MQHelp()
        {
            string s = AppDomain.CurrentDomain.SetupInformation.ConfigurationFile;
            System.Xml.XmlDocument xml = new System.Xml.XmlDocument();
            xml.Load(s.Substring(0, s.LastIndexOf(@"\")) + "\\LuceneConfigure.xml");
            foreach (System.Xml.XmlNode child in xml.ChildNodes[0].ChildNodes)
            {
                System.Xml.XmlAttributeCollection attributes = child.Attributes;
                CityData cd = new CityData();
                cd.Id = Convert.ToInt32(attributes["Id"].Value);
                cd.CityIds = attributes["CityIds"].Value;
                cd.Name = attributes["Name"].Value;

                cd.MqName = attributes["MqName"].Value;
                cd.RoomMqName = attributes["RoomMqName"].Value;

                cd.Path = attributes["Path"].Value;
                cd.RankPath = attributes["RankPath"].Value;
                cd.RoomPath = attributes["RoomPath"].Value;

                cd.SearchUrl = attributes["SearchUrl"].Value;
                cd.RankUrl = attributes["RankUrl"].Value;
                cd.RoomUrl = attributes["RoomUrl"].Value;
                CityList.Add(cd);
            }

        }

        public static int GetCityId(string cityName)
        {
            if (cityName.ToLower() == "beijing")
            {
                return 253;
            }
            if (cityName.ToLower() == "shanghai")
            {
                return 239;
            }
            if (cityName.ToLower() == "guangzhou")
            {
                return 344;
            }
            if (cityName.ToLower() == "shenzhen")
            {
                return 355;
            }
            if (cityName.ToLower() == "tianjin")
            {
                return 205;
            }
            if (cityName.ToLower() == "qingdao")
            {
                return 263;
            }
            if (cityName.ToLower() == "hangzhou")
            {
                return 307;
            }
            if (cityName.ToLower() == "chengdu")
            {
                return 155;
            }
            if (cityName.ToLower() == "nanjing")
            {
                return 243;
            }
            if (cityName.ToLower() == "dalian")
            {
                return 296;
            }
            return -1;
        }

        public static string GetCityName(int cityId)
        {
            if (cityId == 253)
            {
                return "beijing";
            }
            if (cityId == 239)
            {
                return "shanghai";
            }
            if (cityId == 344)
            {
                return "guangzhou";
            }
            if (cityId == 355)
            {
                return "shenzhen";
            }
            if (cityId == 205)
            {
                return "tianjin";
            }
            if (cityId == 263)
            {
                return "qingdao";
            }
            if (cityId == 307)
            {
                return "hangzhou";
            }
            if (cityId == 155)
            {
                return "chengdu";
            }
            if (cityId == 243)
            {
                return "nanjing";
            }
            if (cityId == 296)
            {
                return "dalian";
            }
            return "default";
        }

        public static string GetMqNameByCityId(string cityid)
        {
            var obj = CityList.Find(r => r.CityIds.Split(',').Any(it => it == cityid));
            if (obj == null)
            {
                return CityList.Find(r => r.CityIds == "-1").MqName;
            }
            return obj.MqName;
        }
        /// <summary>
        /// 根据城市ID获取Lucenexml户型图消息名称
        /// </summary>
        /// <param name="cityid">城市</param>
        /// <returns></returns>
        public static string GetRoomMqNameByCityId(string cityid)
        {
            var obj = CityList.Find(r => r.CityIds.Split(',').Any(it => it == cityid));
            if (obj == null)
            {
                return CityList.Find(r => r.CityIds == "-1").RoomMqName;
            }
            return obj.RoomMqName;
        }

        public static string GetLuceneSearchUrlByCityId(string cityid)
        {
            var obj = CityList.Find(r => r.CityIds.Split(',').Any(it => it == cityid));
            if (obj == null)
            {
                return CityList.Find(r => r.CityIds == "-1").SearchUrl;
            }
            return obj.SearchUrl;
        }
        /// <summary>
        /// 根据城市ID 获取Lucenexml排行URL
        /// </summary>
        /// <param name="cityid">城市ID</param>
        /// <returns></returns>
        public static string GetLuceneRankingUrlByCityId(string cityid)
        {
            var obj = CityList.Find(r => r.CityIds.Split(',').Any(it => it == cityid));
            if (obj == null)
            {
                return CityList.Find(r => r.CityIds == "-1").RankUrl;
            }
            return obj.RankUrl;
        }
        /// <summary>
        /// 根据城市ID 获取Lucenexml户型URL
        /// </summary>
        /// <param name="cityid">城市ID</param>
        /// <returns></returns>
        public static string GetLuceneRoomUrlByCityId(string cityid)
        {
            var obj = CityList.Find(r => r.CityIds.Split(',').Any(it => it == cityid));
            if (obj == null)
            {
                return CityList.Find(r => r.CityIds == "-1").RoomUrl;
            }
            return obj.RoomUrl;
        }
        /// <summary>
        /// 根据城市ID 获取lucenexml信息
        /// </summary>
        /// <param name="cityid">城市ID</param>
        /// <returns></returns>
        public static CityData GetLucenexmlInfomation(string cityid)
        {
            return CityList.Find(r => r.CityIds.Split(',').Any(it => it == cityid));
        }
        /// <summary>
        /// 根据lucenexml ID 获取lucenexml信息
        /// </summary>
        /// <param name="Id">lucenexml ID</param>
        /// <returns></returns>
        public static CityData GetLucenexmlInfomation(int Id)
        {
            return CityList.Find(r => r.Id == Id);
        }

        /// <summary>
        /// 获取所有已开通的MQ名称
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<string> GetQueueNamesInfomation()
        {
            return CityList.Select(it => it.MqName).Distinct();
        }
        /// <summary>
        /// 获取所有Lucenexml的ID
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<int> GetLucenexmlIds()
        {
            return CityList.Select(it => it.Id).Distinct();
        }
        /// <summary>
        /// 获取Lucenexml所有数据
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<CityData> GetLucenexmls()
        {
            return CityList;
        }
        /// <summary>
        /// 根据Lucenexml的ID获取相应的城市信息
        /// </summary>
        /// <param name="Id">Lucenexml ID</param>
        /// <returns></returns>
        public static string GetCityIdByLucenexml(int Id)
        {
            var obj = CityList.Find(it => it.Id == Id);
            if (obj == null)
            {
                return "-1";
            }
            return obj.CityIds;
        }
    }

    public class CityData
    {
        public int Id { get; set; }
        public string CityIds { set; get; }
        public string Name { set; get; }

        public string Py { set; get; }
        public string Pyone { set; get; }

        public string MqName { set; get; }
        public string RoomMqName { set; get; }

        public string Path { get; set; }
        public string RankPath { get; set; }
        public string RoomPath { get; set; }

        public string RankUrl { get; set; }
        public string SearchUrl { set; get; }
        public string RoomUrl { set; get; }
    }
}
