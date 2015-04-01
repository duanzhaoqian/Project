using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.IO;



using System.Threading;

namespace MessageUpdate
{
    public class LuceneOpt
    {
        #region 属性

        private static object lockobject1 = new object();
        private static object lockobject2 = new object();
        private static object lockobject3 = new object();
        public string HouseConnectionString
        {
            get;
            set;
        }

        public string BaseDataConnectionString
        {
            get;
            set;
        }

        public string UserConnectionString
        {
            get;
            set;
        }

        public string HouseIndexPath
        {
            get;
            set;
        }
        public string HouseActiveIndexPath
        {
            get;
            set;
        }
        public string VillageIndexPath
        {
            get;
            set;
        }
        public string VillageSubWayIndexPath
        {
            get;
            set;
        }
        public string AreaIndexPath
        {
            get;
            set;
        }
        public string TrafficIndexPath
        {
            get;
            set;
        }

        public string CompanyIndexPath
        {
            get;
            set;
        }

        public string AdvertIndexPath
        {
            get;
            set;
        }
        public string LogPath
        {
            get;
            set;
        }
        public Dal.SearchServiceDal HouseDal
        {
            get;
            set;
        }
        public Dal.SearchServiceDal SearchDal
        {
            get;
            set;
        }
        public Dal.SearchServiceDal UserDal
        {
            get;
            set;
        }

        public string Domain
        {
            get;
            set;
        }
        public string CDNUrl
        {
            get;
            set;
        }
        public string SMSUrl
        {
            get;
            set;
        }
        public string CityId
        {
            get;
            set;
        }
        #endregion

    }
}
