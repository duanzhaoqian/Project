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
using Directory = Lucene.Net.Store.Directory;
using Analyzer = Lucene.Net.Analysis.Analyzer;
using StandardAnalyzer = Lucene.Net.Analysis.Standard.StandardAnalyzer;
using FSDirectory = Lucene.Net.Store.FSDirectory;
using IndexWriter = Lucene.Net.Index.IndexWriter;
using Field = Lucene.Net.Documents.Field;
using Lucene.Net.Index;
using Lucene.Net.Documents;


namespace WindowsServiceTest
{
    public partial class Service1 : ServiceBase
    {
            protected string HouseConnectionString = string.Empty;
        protected string IndexPath = string.Empty;
        protected string LogPath = string.Empty;
        protected string MQLongHouseSearchQueueName = string.Empty;

        protected LuceneOpt luceneOpt = new LuceneOpt();
        private System.Timers.Timer timerHour = new System.Timers.Timer();
        private System.Timers.Timer timerMinute = new System.Timers.Timer();
        private static object lockobject1 = new object();
        private static object lockobject2 = new object();
        public Service1()
        {
            InitializeComponent();
             HouseConnectionString = Profile.IniReadValue("public", "HouseConnectionString");
            IndexPath = Profile.IniReadValue("TXLongHouseService", "HouseIndexPath");
            LogPath = Profile.IniReadValue("TXLongHouseService", "HouseLogPath");
            luceneOpt.HouseDal = new Dal.testDal(HouseConnectionString);
            luceneOpt.UserDal = new Dal.testDal(Profile.IniReadValue("public", "UserConnectionString"));
            luceneOpt.HouseIndexPath = IndexPath;
            luceneOpt.VillageSubWayIndexPath = Profile.IniReadValue("TXVillageService", "VillageSubWayIndexPath");
            luceneOpt.LogPath = LogPath;
            luceneOpt.Domain = Profile.IniReadValue("public", "Domain");
            luceneOpt.CDNUrl = Profile.IniReadValue("public", "CDNUrl");
            luceneOpt.SMSUrl = Profile.IniReadValue("public", "SMSUrl");
        }

        protected override void OnStart(string[] args)
        {
            //timerMinute.Elapsed += new ElapsedEventHandler(timerMinute_Elapsed);
            //timerMinute.Interval = 500;
            //timerMinute.AutoReset = true;
            //timerMinute.Enabled = true;
            Analyzer analyzer = null;
            Directory directory = null;
            IndexWriter indexWriter = null;

            analyzer = new StandardAnalyzer(Lucene.Net.Util.Version.LUCENE_29);

            directory = FSDirectory.Open(new DirectoryInfo(luceneOpt.HouseIndexPath));
            indexWriter = new IndexWriter(directory, false, analyzer, false);
            TXCommons.Refurbish.LogTool.Log("服务启动", DateTime.Now.ToString() + DateTime.Now.Millisecond.ToString(), LogPath);
        }
        protected void timerMinute_Elapsed(object sender, ElapsedEventArgs e)
        {
           
               // luceneOpt.UpdateLucene("272", luceneOpt.HouseIndexPath,"Update");
               
           

        }
        protected override void OnStop()
        {

        }
    }
}
