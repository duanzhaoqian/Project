using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Timers;
using System.IO;
using System.Threading;
using Directory = Lucene.Net.Store.Directory;
using Analyzer = Lucene.Net.Analysis.Analyzer;
using StandardAnalyzer = Lucene.Net.Analysis.Standard.StandardAnalyzer;
using FSDirectory = Lucene.Net.Store.FSDirectory;
using IndexWriter = Lucene.Net.Index.IndexWriter;
using Field = Lucene.Net.Documents.Field;
using Lucene.Net.Index;
using Lucene.Net.Documents;

namespace TXPublicData
{
    public partial class TxPublicData : ServiceBase
    {
        protected string MQQueueName = string.Empty;


        protected int ThreadCount = 0;
        protected LuceneOpt luceneOpt = new LuceneOpt();
        private System.Timers.Timer timerArea = new System.Timers.Timer();
        private static object lockobject1 = new object();
        private System.Timers.Timer timerMinute = new System.Timers.Timer();
        protected string LogPath = string.Empty;
        public TxPublicData()
        {
            InitializeComponent();
            luceneOpt.HouseActiveIndexPath = Profile.IniReadValue("TXLongHouseService", "HouseActiveIndexPath");
            luceneOpt.HouseConnectionString = Profile.IniReadValue("public", "HouseConnectionString");
            luceneOpt.BaseDataConnectionString = Profile.IniReadValue("public", "BaseDataConnectionString");
            ThreadCount = Convert.ToInt32(Profile.IniReadValue("public", "ThreadCount"));
            luceneOpt.VillageIndexPath = Profile.IniReadValue("TXVillageService", "VillageIndexPath"); ;
            luceneOpt.AreaIndexPath = Profile.IniReadValue("TXVillageService", "AreaIndexPath");
            luceneOpt.TrafficIndexPath = Profile.IniReadValue("TXVillageService", "TrafficIndexPath");
            luceneOpt.VillageSubWayIndexPath = Profile.IniReadValue("TXVillageService", "VillageSubWayIndexPath");
            luceneOpt.CompanyIndexPath = Profile.IniReadValue("TXVillageService", "CompanyIndexPath");
            luceneOpt.AdvertIndexPath = Profile.IniReadValue("TXVillageService", "AdvertIndexPath");
            luceneOpt.LogPath = Profile.IniReadValue("TXVillageService", "VillageLogPath");
            luceneOpt.HouseDal = new Dal.SearchServiceDal(luceneOpt.HouseConnectionString);
            luceneOpt.SearchDal = new Dal.SearchServiceDal(luceneOpt.BaseDataConnectionString);
            luceneOpt.UserDal = new Dal.SearchServiceDal(Profile.IniReadValue("public", "UserConnectionString"));
            luceneOpt.CityId = Profile.IniReadValue("TXLongHouseService", "CityId");
            LogPath = luceneOpt.LogPath;
        }

        protected override void OnStart(string[] args)
        {
        }

        protected override void OnStop()
        {

        }
    }
}
