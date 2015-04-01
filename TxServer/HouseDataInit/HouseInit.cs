using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HouseDataInit
{
    public  class HouseInit
    {
        protected string HouseConnectionString = string.Empty;

        protected string MQQueueName = string.Empty;
        protected string IndexPath = string.Empty;
        protected string LogPath = string.Empty;
        protected int ThreadCount = 0;
        protected LuceneOpt luceneOpt = new LuceneOpt();
        private static object lockobject3 = new object();
        
        public void Init()
        {
            try
            {
                HouseConnectionString = Profile.IniReadValue("public", "HouseConnectionString");

                ThreadCount = Convert.ToInt32(Profile.IniReadValue("public", "ThreadCount"));

                MQQueueName = Profile.IniReadValue("TXLongHouseService", "HouseMQQueueName");
                IndexPath = Profile.IniReadValue("TXLongHouseService", "HouseIndexPath");
                LogPath = Profile.IniReadValue("TXLongHouseService", "HouseLogPath");
                luceneOpt.VillageSubWayIndexPath = Profile.IniReadValue("TXVillageService", "VillageSubWayIndexPath");
                luceneOpt.HouseConnectionString = HouseConnectionString;

                luceneOpt.HouseIndexPath = IndexPath;
                luceneOpt.HouseActiveIndexPath = Profile.IniReadValue("TXLongHouseService", "HouseActiveIndexPath");
                luceneOpt.LogPath = LogPath;
                luceneOpt.HouseDal = new SearchServiceDal(HouseConnectionString);
                luceneOpt.UserDal = new SearchServiceDal(Profile.IniReadValue("public", "UserConnectionString"));
                luceneOpt.BaseDataConnectionString = Profile.IniReadValue("public", "BaseDataConnectionString");
                luceneOpt.SearchDal = new SearchServiceDal(luceneOpt.BaseDataConnectionString);
                luceneOpt.Domain = Profile.IniReadValue("public", "Domain");
                luceneOpt.CDNUrl = Profile.IniReadValue("public", "CDNUrl");
                luceneOpt.CityId = Profile.IniReadValue("TXLongHouseService", "CityId");
                luceneOpt.AddDocForLongHouse();

                //luceneOpt.AddActiveDocForLongHouse();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
    }
}
