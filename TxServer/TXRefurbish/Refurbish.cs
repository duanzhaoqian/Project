using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
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

namespace TXRefurbish
{
    public partial class Refurbish : ServiceBase
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
        public Refurbish()
        {
            InitializeComponent();
            HouseConnectionString = Profile.IniReadValue("public", "HouseConnectionString");
            IndexPath = Profile.IniReadValue("TXLongHouseService", "HouseIndexPath");
            LogPath = Profile.IniReadValue("TXRefurbishService", "RefurbishLogPath");
            luceneOpt.HouseDal = new Dal.SearchServiceDal(HouseConnectionString);
            luceneOpt.UserDal = new Dal.SearchServiceDal(Profile.IniReadValue("public", "UserConnectionString"));
            luceneOpt.HouseIndexPath = IndexPath;
            luceneOpt.VillageSubWayIndexPath = Profile.IniReadValue("TXVillageService", "VillageSubWayIndexPath");
            luceneOpt.LogPath = LogPath;
            luceneOpt.Domain = Profile.IniReadValue("public", "Domain");
            luceneOpt.CDNUrl = Profile.IniReadValue("public", "CDNUrl");
            luceneOpt.SMSUrl = Profile.IniReadValue("public", "SMSUrl");
        }

        protected override void OnStart(string[] args)
        {
            while (true)
            {
                Thread.Sleep(100);
                if (DateTime.Now.Second == 1)
                {

                    break;
                }
            }
            timerMinute.Elapsed += new ElapsedEventHandler(timerMinute_Elapsed);
            timerMinute.Interval = 1000 * 60 * 1;
            timerMinute.AutoReset = true;
            timerMinute.Enabled = true;

            TXCommons.Refurbish.LogTool.Log("刷新定时服务启动", DateTime.Now.ToString() + DateTime.Now.Millisecond.ToString(), LogPath);
        }

        #region 分钟定时处理
        protected void timerMinute_Elapsed(object sender, ElapsedEventArgs e)
        {
            Monitor.Enter(lockobject2);
            try
            {

                UpdateRefurbish();
            }
            catch (Exception ex)
            {
                TXCommons.Refurbish.LogTool.Log("房源出价，刷新定时服务错误", ex.Message, luceneOpt.LogPath);
            }
            finally
            {
                Monitor.Exit(lockobject2);
            }

        }
        #endregion

        #region   预约刷新处理
        /// <summary>
        /// 更新刷新房源
        /// </summary>
        public void UpdateRefurbish()
        {
            DateTime date = DateTime.Now;
            if (date.Minute % 5 == 0)
            {
                Analyzer analyzer = null;
                Directory directory = null;
                IndexWriter indexWriter = null;

                try
                {


                    int userid = 0;
                    int houseid = 0;
                    int rcount = 0;
                    int acount = 0;

                    DataSet dataset = luceneOpt.HouseDal.GetTxRefurbishTimingData(date);
                    if (dataset != null && dataset.Tables.Count > 0 && dataset.Tables[0].Rows.Count > 0)
                    {
                        analyzer = new StandardAnalyzer(Lucene.Net.Util.Version.LUCENE_29);

                        directory = FSDirectory.Open(new DirectoryInfo(luceneOpt.HouseIndexPath));
                        indexWriter = new IndexWriter(directory, false, analyzer, false);
                        DataTable table = dataset.Tables[0];
                        foreach (DataRow row in table.Rows)
                        {
                            try
                            {
                                userid = int.Parse(row["userid"].ToString());
                                houseid = int.Parse(row["houseid"].ToString());
                                rcount = luceneOpt.HouseDal.GetRefurbishCount(userid);
                                acount = luceneOpt.HouseDal.GetServiceCount(userid);
                                if (rcount < acount)
                                {
                                    date = DateTime.Now;
                                    luceneOpt.HouseDal.RefurbishR(userid, houseid, date);
                                    if (luceneOpt.UpdateHouseDoc(houseid.ToString(), indexWriter, luceneOpt.HouseIndexPath, false))
                                    {
                                        TXCommons.Refurbish.LogTool.Log("房源预约刷新定时服务已刷新", "userid:" + userid + "-houseid:" + houseid + "-datetime:" + date.ToString() + "-rcount" + rcount + "-acount:" + acount, luceneOpt.LogPath);
                                    }
                                    else
                                    {
                                        TXCommons.Refurbish.LogTool.Log("房源预约刷新定时服务索引不存在", "userid:" + userid + "-houseid:" + houseid + "-datetime:" + date.ToString() + "-rcount" + rcount + "-acount:" + acount, luceneOpt.LogPath);
                                    }
                                }
                                else
                                {
                                    luceneOpt.HouseDal.RefurbishF(userid, houseid, date);
                                    TXCommons.Refurbish.LogTool.Log("房源预约刷新定时服务未刷新", "userid:" + userid + "-houseid:" + houseid + "-datetime:" + date.ToString() + "-rcount" + rcount + "-acount:" + acount, luceneOpt.LogPath);
                                }
                            }
                            catch (Exception ex)
                            {
                                TXCommons.Refurbish.LogTool.Log("房源刷新错误：", ex.Message, luceneOpt.LogPath);
                            }
                        }
                    }


                }
                catch (Exception ex)
                {
                    TXCommons.Refurbish.LogTool.Log("房源预约刷新定时服务", "房源预约刷新定时服务执行错误:" + ex.Message, LogPath);
                }
                finally
                {
                    if (analyzer != null)
                        analyzer.Close();
                    if (indexWriter != null)
                        indexWriter.Close();
                    if (directory != null)
                        directory.Close();
                }
            }
        }
        #endregion

        protected override void OnStop()
        {
        }
    }
}
