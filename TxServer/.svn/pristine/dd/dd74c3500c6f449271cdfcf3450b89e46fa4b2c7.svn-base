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
namespace TXSearchService
{
    partial class TxVillageService : ServiceBase
    {
        

        protected string MQQueueName = string.Empty;
     
       
        protected int ThreadCount = 0;
        protected LuceneOpt luceneOpt = new LuceneOpt();
        private System.Timers.Timer timerArea = new System.Timers.Timer();
        private static object lockobject1 = new object();
        private System.Timers.Timer timerMinute = new System.Timers.Timer();
        protected string LogPath = string.Empty;
        string stop = "";
        public TxVillageService()
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
            luceneOpt.CityIndexPath = Profile.IniReadValue("TXVillageService", "CityIndexPath");

            luceneOpt.LogPath = Profile.IniReadValue("TXVillageService", "VillageLogPath");
            luceneOpt.HouseDal = new Dal.SearchServiceDal(luceneOpt.HouseConnectionString);
            luceneOpt.SearchDal = new Dal.SearchServiceDal(luceneOpt.BaseDataConnectionString);
            luceneOpt.UserDal = new Dal.SearchServiceDal(Profile.IniReadValue("public", "UserConnectionString"));
            luceneOpt.CityId = Profile.IniReadValue("TXLongHouseService", "CityId");
           

            LogPath = luceneOpt.LogPath;
        }

        protected override void OnStart(string[] args)
        {
            //while (true)
            //{
            //    Thread.Sleep(100);
            //    if (DateTime.Now.Second == 1)
            //    {

            //        break;
            //    }
            //}
            //timerMinute.Elapsed += new ElapsedEventHandler(timerMinute_Elapsed);
            //timerMinute.Interval = 1000 * 60 * 1;
            //timerMinute.AutoReset = true;
            //timerMinute.Enabled = true;
           
            DeleteDirectory();
            CreateIndex();
            
          
            timerArea.Elapsed += new ElapsedEventHandler(timerArea_Elapsed);
            timerArea.Interval = 1000 * 60 * 60;
            timerArea.AutoReset = true;
            timerArea.Enabled = true;

            //TXCommons.Refurbish.LogTool.Log("小区,区域，地铁索引服务启动_1", "", luceneOpt.LogPath);
            //ServerStart();

            //TXCommons.Refurbish.LogTool.Log("自动退还保证金:", "", luceneOpt.LogPath);
            //UpdateIsBack();
            //TXCommons.Refurbish.LogTool.Log("打开房租支付开口:", "", luceneOpt.LogPath);
            //UpdateIsOpen();
            //TXCommons.Refurbish.LogTool.Log("到期返还保证金:", "", luceneOpt.LogPath);
            //UpdateIsLock();
        }


        protected void timerArea_Elapsed(object sender, ElapsedEventArgs e)
        {
          
            try
            {
               
             
                    IndexStart();
                   
              
                    
            }
            catch (Exception ex)
            {
                TXCommons.Refurbish.LogTool.Log("定时服务错误",ex.Message, luceneOpt.LogPath);
            }
          
        }

        #region 分钟定时处理
        //protected void timerMinute_Elapsed(object sender, ElapsedEventArgs e)
        //{
        //    UpdateBidBegin();

        //    UpdateBidEnd();
        //}
        #endregion
        

        #region  索引相关服务
        /// <summary>
        /// 服务开始执行的函数，负责目录创建以及所有数据索引生成
        /// </summary>
        public void IndexStart()
        {
            
               
                if ( DateTime.Now.Hour == 2)
                {
                    TXCommons.Refurbish.LogTool.Log("小区,区域，地铁索引服务启动", "", luceneOpt.LogPath);
                    DeleteDirectory();
                    CreateIndex();
                }
                //ServerStart();

                //DeleteRefurbishRedis();

            
        }

        /// <summary>
        ///  删除索引目录
        /// </summary>
        public void DeleteDirectory()
        {
            try
            {

                DirectoryInfo directoryInfoVillage = new DirectoryInfo(luceneOpt.VillageIndexPath);
                if (directoryInfoVillage.Exists)
                {
                    directoryInfoVillage.Delete(true);
                }
              
                DirectoryInfo directoryInfoArea = new DirectoryInfo(luceneOpt.AreaIndexPath);
                if (directoryInfoArea.Exists)
                {
                    directoryInfoArea.Delete(true);
                }
                
                DirectoryInfo directoryInfoTraffic = new DirectoryInfo(luceneOpt.TrafficIndexPath);
                if (directoryInfoTraffic.Exists)
                {
                    directoryInfoTraffic.Delete(true);
                }


                DirectoryInfo SubWaydirectoryInfo = new DirectoryInfo(luceneOpt.VillageSubWayIndexPath);
                if (SubWaydirectoryInfo.Exists)
                {
                    SubWaydirectoryInfo.Delete(true);
                }
                DirectoryInfo CompanydirectoryInfo = new DirectoryInfo(luceneOpt.CompanyIndexPath);
                if (CompanydirectoryInfo.Exists)
                {
                    CompanydirectoryInfo.Delete(true);
                }
                DirectoryInfo AdvertdirectoryInfo = new DirectoryInfo(luceneOpt.AdvertIndexPath);
                if (AdvertdirectoryInfo.Exists)
                {
                    AdvertdirectoryInfo.Delete(true);
                }

                DirectoryInfo CitydirectoryInfo = new DirectoryInfo(luceneOpt.CityIndexPath);
                if (CitydirectoryInfo.Exists)
                {
                    CitydirectoryInfo.Delete(true);
                }
            }
            catch (Exception ex)
            {

                TXCommons.Refurbish.LogTool.Log("删除目录错误", ex.Message, luceneOpt.LogPath);
            }
        }
        /// <summary>
        /// 建立索引
        /// </summary>
        public void CreateIndex()
        {
            try
            {
                string stop = Profile.IniReadValue("TXLongHouseService", "Stop");
                if (stop == "Advert"||stop=="Full")
                {
                    luceneOpt.SetAdvertConfig();

                    DirectoryInfo directoryInfoVillage = new DirectoryInfo(luceneOpt.VillageIndexPath);
                    if (!directoryInfoVillage.Exists)
                    {
                        directoryInfoVillage.Create();
                    }
                    if (directoryInfoVillage.GetFiles().Length == 0)
                    {

                        int count = luceneOpt.AddDocForVillage(string.Empty);
                        TXCommons.Refurbish.LogTool.Log("小区索引生成成功", "共" + count + "条生成", luceneOpt.LogPath);


                    }
                    DirectoryInfo directoryInfoArea = new DirectoryInfo(luceneOpt.AreaIndexPath);
                    if (!directoryInfoArea.Exists)
                    {
                        directoryInfoArea.Create();
                    }
                    if (directoryInfoArea.GetFiles().Length == 0)
                    {



                        int count = luceneOpt.AddDocForArea(string.Empty);
                        TXCommons.Refurbish.LogTool.Log("区域商圈索引生成成功", "共" + count + "条生成", luceneOpt.LogPath);


                    }
                    DirectoryInfo directoryInfoTraffic = new DirectoryInfo(luceneOpt.TrafficIndexPath);
                    if (!directoryInfoTraffic.Exists)
                    {
                        directoryInfoTraffic.Create();
                    }
                    if (directoryInfoTraffic.GetFiles().Length == 0)
                    {


                        int count = luceneOpt.AddDocForTraffic(string.Empty);
                        TXCommons.Refurbish.LogTool.Log("地铁线路索引生成成功", "共" + count + "条生成", luceneOpt.LogPath);
                    }

                    //DirectoryInfo SubWaydirectoryInfo = new DirectoryInfo(luceneOpt.VillageSubWayIndexPath);
                    //if (!SubWaydirectoryInfo.Exists)
                    //{
                    //    SubWaydirectoryInfo.Create();
                    //}
                    //if (SubWaydirectoryInfo.GetFiles().Length == 0)
                    //{
                    //    int count = luceneOpt.AddDocForVillageSubWay(string.Empty);
                    //    TXCommons.Refurbish.LogTool.Log("小区地铁关系索引生成成功", "共" + count + "条生成", luceneOpt.LogPath);
                    //}

                    DirectoryInfo CompanydirectoryInfo = new DirectoryInfo(luceneOpt.CompanyIndexPath);
                    if (!CompanydirectoryInfo.Exists)
                    {
                        CompanydirectoryInfo.Create();
                    }
                    if (CompanydirectoryInfo.GetFiles().Length == 0)
                    {
                        int count = luceneOpt.AddDocForCompany();
                        TXCommons.Refurbish.LogTool.Log("经纪公司索引生成成功", "共" + count + "条生成", luceneOpt.LogPath);
                    }
                   

                    DirectoryInfo CitydirectoryInfo = new DirectoryInfo(luceneOpt.CityIndexPath);
                    if (!CitydirectoryInfo.Exists)
                    {
                        CitydirectoryInfo.Create();
                    }
                    if (CitydirectoryInfo.GetFiles().Length == 0)
                    {
                        TXCommons.Refurbish.LogTool.Log("城市索引处理开始:", "", luceneOpt.LogPath);
                        int count = luceneOpt.AddCityForArea("");
                        TXCommons.Refurbish.LogTool.Log("城市索引生成成功", "共" + count + "条生成", luceneOpt.LogPath);
                    }
                }
                if (stop == "Area" || stop == "Full")
                {
                    DirectoryInfo AdvertdirectoryInfo = new DirectoryInfo(luceneOpt.AdvertIndexPath);
                    if (!AdvertdirectoryInfo.Exists)
                    {
                        AdvertdirectoryInfo.Create();
                    }
                    if (AdvertdirectoryInfo.GetFiles().Length == 0)
                    {
                        TXCommons.Refurbish.LogTool.Log("广告索引处理开始:", "", luceneOpt.LogPath);
                        int count = luceneOpt.UpdateAdvertList("index", null);
                        TXCommons.Refurbish.LogTool.Log("广告索引生成成功", "共" + count + "条生成", luceneOpt.LogPath);
                    }
                }
                
            }
            catch (Exception ex)
            {

                TXCommons.Refurbish.LogTool.Log("索引目录错误", ex.Message, luceneOpt.LogPath);
            }
        }
        #endregion





        //#region  经纪人服务到期相关服务
        ///// <summary>
        ///// 经纪人服务启动
        ///// </summary>
        //public void ServerStart()
        //{
        //    if (DateTime.Now.Hour == 1)
        //    {
        //        TXCommons.Refurbish.LogTool.Log("自动退还保证金:", "", luceneOpt.LogPath);
        //        UpdateIsBack();
        //        TXCommons.Refurbish.LogTool.Log("打开房租支付开口:", "", luceneOpt.LogPath);
        //        UpdateIsOpen();
        //        TXCommons.Refurbish.LogTool.Log("到期返还保证金:", "", luceneOpt.LogPath);
        //        UpdateIsLock();

        //        TXCommons.Refurbish.LogTool.Log("游戏处理开始:", "", luceneOpt.LogPath);
        //        UpdateGame();
               
        //    }

        //}


        ///// <summary>
        ///// 自动退还保证金
        ///// </summary>
        //public void UpdateIsBack()
        //{
           
        //    try
        //    {

        //        DataSet dataset = luceneOpt.HouseDal.GetHouseIsBack(luceneOpt.CityId);
        //        string id = "";
        //        if (dataset != null && dataset.Tables.Count > 0 && dataset.Tables[0].Rows.Count > 0)
        //        {
                   
        //            TXCommons.Refurbish.LogTool.Log("----------自动退还保证金服务开始", "数量:" + dataset.Tables[0].Rows.Count + "CityId:" + luceneOpt.CityId, LogPath);
        //            List<string> ids = new List<string>();
        //            foreach (DataRow row in dataset.Tables[0].Rows)
        //            {
        //                try
        //                {
        //                    id = row["id"].ToString().Trim();
        //                    int day = Convert.ToInt32(row["DayNum"]);
        //                    double  price = Convert.ToDouble (row["AllPrice"]);
        //                    double sprice = price / 365 * 0.07 * day;
        //                    luceneOpt.HouseDal.UpdateIsBack(id);
        //                    TXCommons.Refurbish.LogTool.Log("自动退还保证金Id:", id, LogPath);
        //                    luceneOpt.UserDal.UpdateU_AccountTransaction(Convert.ToInt32(row["UserId"]), price, 6, "返还保证金");
        //                    TXCommons.Refurbish.LogTool.Log("退还保证金：", "UserId:" + Convert.ToInt32(row["UserId"]) + "_AllPrice:" + price, LogPath);

        //                    TXCommons.Refurbish.LogTool.Log("自动退还保证金返还收益Id:", id, LogPath);
        //                    luceneOpt.UserDal.UpdateU_AccountTransaction(Convert.ToInt32(row["UserId"]), sprice, 6, "保证金返还收益");
        //                    TXCommons.Refurbish.LogTool.Log("退还保证金返还收益：", "UserId:" + Convert.ToInt32(row["UserId"]) + "_AllSPrice:" +sprice, LogPath);
        //                    //ids.Add(houseid);
        //                }
        //                catch (Exception ex)
        //                {
        //                    TXCommons.Refurbish.LogTool.Log("自动退还保证金服务错误", ex.Message, LogPath);
        //                }
        //            }
        //            //luceneOpt.UpdateLucene("0", luceneOpt.HouseIndexPath, "List", DateTime.Now, string.Empty, string.Empty, ids.ToArray());
        //            TXCommons.Refurbish.LogTool.Log("----------自动退还保证金处理结束", "数量:" + dataset.Tables[0].Rows.Count + "CityId:" + luceneOpt.CityId, LogPath);
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        TXCommons.Refurbish.LogTool.Log("自动退还保证金执行错误:", ex.Message, LogPath);
        //    }
        //    finally
        //    {
                
        //    }
        //}

        ///// <summary>
        ///// 打开房租支付开口
        ///// </summary>
        //public void UpdateIsOpen()
        //{
      
        //    try
        //    {


        //        DataSet datasethouse = luceneOpt.HouseDal.GetHouseIsOpenHouse(luceneOpt.CityId);
        //        if (datasethouse != null && datasethouse.Tables.Count > 0 && datasethouse.Tables[0].Rows.Count > 0)
        //        {
        //            TXCommons.Refurbish.LogTool.Log("----------打开房租支付开口服务开始", "房源数量:" + datasethouse.Tables[0].Rows.Count + "CityId:" + luceneOpt.CityId, LogPath);
        //            foreach (DataRow rowh in datasethouse.Tables[0].Rows)
        //            {
        //                string houseid = rowh["houseid"].ToString().Trim();
        //                DataSet dataset = luceneOpt.HouseDal.GetHouseIsOpen(houseid);
        //                string cid = "";
        //                string pid = "";
        //                string time = "";
        //                if (dataset != null && dataset.Tables.Count > 0 && dataset.Tables[0].Rows.Count > 0)
        //                {
                            
        //                    TXCommons.Refurbish.LogTool.Log("----------打开房租支付开口服务开始", "数量:" + dataset.Tables[0].Rows.Count + "CityId:" + luceneOpt.CityId, LogPath);
                         
        //                    DataRow row = dataset.Tables[0].Rows[0];
        //                        try
        //                        {
        //                            cid = row["cid"].ToString().Trim();
        //                            pid = row["pid"].ToString().Trim();
        //                            time = row["time"].ToString().Trim();
        //                            TXCommons.Refurbish.LogTool.Log("打开房租支付开口服务c","cid："+cid+"time:"+time , LogPath);
        //                            luceneOpt.HouseDal.UpdateIsOpenC(cid, time);
        //                            TXCommons.Refurbish.LogTool.Log("打开房租支付开口服务c", "pid：" + pid, LogPath);
        //                            luceneOpt.HouseDal.UpdateIsOpenP(pid);
                                   
        //                        }
        //                        catch (Exception ex)
        //                        {
        //                            TXCommons.Refurbish.LogTool.Log("打开房租支付开口服务错误", ex.Message, LogPath);
        //                        }
                          
                          
        //                    TXCommons.Refurbish.LogTool.Log("----------打开房租支付开口处理结束", "数量:" + dataset.Tables[0].Rows.Count + "CityId:" + luceneOpt.CityId, LogPath);
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        TXCommons.Refurbish.LogTool.Log("打开房租支付开口执行错误:", ex.Message, LogPath);
        //    }
        //    finally
        //    {
              
        //    }
        //}

        ///// <summary>
        /////到期返回保证金
        ///// </summary>
        //public void UpdateIsLock()
        //{
            
        //    try
        //    {

        //        DataSet dataset = luceneOpt.HouseDal.GetHouseIsLock(luceneOpt.CityId);
        //        string id = "";

        //        if (dataset != null && dataset.Tables.Count > 0 && dataset.Tables[0].Rows.Count > 0)
        //        {
                   
        //            TXCommons.Refurbish.LogTool.Log("----------到期返回保证金服务开始", "数量:" + dataset.Tables[0].Rows.Count + "CityId:" + luceneOpt.CityId, LogPath);
        //            List<string> ids = new List<string>();
        //            foreach (DataRow row in dataset.Tables[0].Rows)
        //            {
        //                try
        //                {
        //                    id = row["id"].ToString().Trim();

        //                    luceneOpt.HouseDal.UpdateIsLock(id);
        //                    //ids.Add(houseid);
        //                }
        //                catch (Exception ex)
        //                {
        //                    TXCommons.Refurbish.LogTool.Log("到期返回保证金服务错误", ex.Message, LogPath);
        //                }
        //            }
        //            //luceneOpt.UpdateLucene("0", luceneOpt.HouseIndexPath, "List", DateTime.Now, string.Empty, string.Empty, ids.ToArray());
        //            TXCommons.Refurbish.LogTool.Log("----------到期返回保证金处理结束", "数量:" + dataset.Tables[0].Rows.Count + "CityId:" + luceneOpt.CityId, LogPath);
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        TXCommons.Refurbish.LogTool.Log("到期返回保证金执行错误:", ex.Message, LogPath);
        //    }
        //    finally
        //    {
               
        //    }
        //}

        //public void UpdateGame()
        //{
        //    try
        //    {
        //        TXCommons.Refurbish.LogTool.Log("游戏开始数量:", luceneOpt.HouseDal.UpdateGameStateBegin().ToString(), LogPath);
        //        TXCommons.Refurbish.LogTool.Log("游戏结束数量:", luceneOpt.HouseDal.UpdateGameStateEnd().ToString(), LogPath);
        //    }
        //    catch (Exception ex)
        //    {
        //        TXCommons.Refurbish.LogTool.Log("游戏执行错误:", ex.Message, LogPath);
        //    }
        //    finally
        //    {
               
        //    }
        //}

        ///// <summary>
        ///// 跟新经纪人服务到期
        ///// </summary>
        ////public void UpdateAgentService()
        ////{

        ////    try
        ////    {
        ////        DataSet dataset = luceneOpt.HouseDal.GetAgentService();
        ////        if (dataset.Tables.Count > 0 && dataset.Tables[0].Rows.Count > 0)
        ////        {
        ////            TXCommons.Refurbish.LogTool.Log("更新经纪人服务数量:", dataset.Tables[0].Rows.Count.ToString(), luceneOpt.LogPath);
        ////            foreach (DataRow row in dataset.Tables[0].Rows)
        ////            {
        ////                try
        ////                {
        ////                    luceneOpt.HouseDal.UpdateAgentService(int.Parse(row["Id"].ToString()));
        ////                    TXCommons.Refurbish.LogTool.Log("更新经纪人服务到期:", "Id:" + row["Id"].ToString() + "_用户:" + row["A_Id"].ToString() + "_状态:" + row["State"].ToString(), luceneOpt.LogPath);
        ////                    luceneOpt.HouseDal.UpdateRefurbishEndCountMonth(row["A_Id"].ToString());
        ////                    TXCommons.Refurbish.LogTool.Log("经纪人发布报价房数归零:", "Id:" + row["Id"].ToString() + "_开始时间:" + row["BeginTime"].ToString() + "_结束时间:" + row["EndTime"].ToString(), luceneOpt.LogPath);
        ////                }
        ////                catch (Exception ex)
        ////                {
        ////                    TXCommons.Refurbish.LogTool.Log("更新经纪人服务到期错误", ex.Message, luceneOpt.LogPath);
        ////                }
        ////            }
        ////        }
        ////    }
        ////    catch (Exception ex)
        ////    {
        ////        TXCommons.Refurbish.LogTool.Log("更新经纪人服务到期错误", ex.Message, luceneOpt.LogPath);
        ////    }
        ////}
        ///// <summary>
        ///// 替换经纪人本月服务
        ///// </summary>
        ////public void UpdateWillAgentService()
        ////{
        ////    try
        ////    {
        ////        DataSet dataset = luceneOpt.HouseDal.GetWillAgentService();
        ////        if (dataset.Tables.Count > 0 && dataset.Tables[0].Rows.Count > 0)
        ////        {
        ////            TXCommons.Refurbish.LogTool.Log("替换经纪人服务数量:", dataset.Tables[0].Rows.Count.ToString(), luceneOpt.LogPath);
        ////            foreach (DataRow row in dataset.Tables[0].Rows)
        ////            {
        ////                try
        ////                {
        ////                    luceneOpt.HouseDal.UpdateWillAgentService(int.Parse(row["Id"].ToString()), int.Parse(row["A_Id"].ToString()));
        ////                    TXCommons.Refurbish.LogTool.Log("替换经纪人服务:", "Id:" + row["Id"].ToString() + "_用户:" + row["A_Id"].ToString() + "_状态:" + row["State"].ToString(), luceneOpt.LogPath);
        ////                    luceneOpt.HouseDal.UpdateRefurbishEndCountMonth(row["A_Id"].ToString());
        ////                    TXCommons.Refurbish.LogTool.Log("经纪人发布报价房数归零:", "Id:" + row["A_Id"].ToString() + "_开始时间:" + row["BeginTime"].ToString() + "_结束时间:" + row["EndTime"].ToString(), luceneOpt.LogPath);
        ////                }
        ////                catch (Exception ex)
        ////                {
        ////                    TXCommons.Refurbish.LogTool.Log("替换经纪人服务替换错误", ex.Message, luceneOpt.LogPath);
        ////                }
        ////            }
        ////        }
        ////    }
        ////    catch (Exception ex)
        ////    {
        ////        TXCommons.Refurbish.LogTool.Log("替换经纪人服务替换错误", ex.Message, luceneOpt.LogPath);
        ////    }
        ////}

        ///// <summary>
        ///// 经纪人发布标签房数归零
        ///// </summary>
        ////public void UpdateRefurbishEndCountMonth()
        ////{

        ////    try
        ////    {
        ////        if (DateTime.Now.Day == 1)
        ////        {
        ////            int count = luceneOpt.HouseDal.UpdateRefurbishEndCountMonth();
        ////            TXCommons.Refurbish.LogTool.Log("经纪人发布标签房数归零数量", count.ToString(), luceneOpt.LogPath);
        ////        }
        ////    }
        ////    catch (Exception ex)
        ////    {
        ////        TXCommons.Refurbish.LogTool.Log("经纪人发布标签房数归零错误", ex.Message, luceneOpt.LogPath);
        ////    }
        ////}
        ///// <summary>
        ///// 经纪人发布报价房数归零
        ///// </summary>
        ////public void GetEndCountMonth()
        ////{
        ////    try
        ////    {
        ////        if (DateTime.Now.Day == 1)
        ////        {
        ////            DataSet dataset = luceneOpt.HouseDal.GetEndCountMonth();
        ////            if (dataset.Tables.Count > 0 && dataset.Tables[0].Rows.Count > 0)
        ////            {
        ////                TXCommons.Refurbish.LogTool.Log("本月未到期经纪人数量:", dataset.Tables[0].Rows.Count.ToString(), luceneOpt.LogPath);
        ////                foreach (DataRow row in dataset.Tables[0].Rows)
        ////                {
        ////                    try
        ////                    {
        ////                        luceneOpt.HouseDal.UpdateRefurbishEndCountMonth(row["A_Id"].ToString());
        ////                        TXCommons.Refurbish.LogTool.Log("经纪人发布报价房数归零:", "Id:" + row["A_Id"].ToString() + "_开始时间:" + row["BeginTime"].ToString() + "_结束时间:" + row["EndTime"].ToString(), luceneOpt.LogPath);

        ////                    }
        ////                    catch (Exception ex)
        ////                    {
        ////                        TXCommons.Refurbish.LogTool.Log("经纪人发布报价房数归零错误", ex.Message, luceneOpt.LogPath);
        ////                    }
        ////                }
        ////            }
        ////        }
        ////    }
        ////    catch (Exception ex)
        ////    {
        ////        TXCommons.Refurbish.LogTool.Log("替换经纪人服务替换错误", ex.Message, luceneOpt.LogPath);
        ////    }
        ////}

        ///// <summary>
        ///// 赠送vip经纪人标签房
        ///// </summary>
        ////public void UpdateBargainingCountData()
        ////{
        ////    try
        ////    {
        ////        if (DateTime.Now.Day == 1)
        ////        {
        ////            DataSet dataset = luceneOpt.HouseDal.GetBargainingCountData();
        ////            if (dataset.Tables.Count > 0 && dataset.Tables[0].Rows.Count > 0)
        ////            {
        ////                TXCommons.Refurbish.LogTool.Log("本月未到期赠送vip经纪人标签房数量:", dataset.Tables[0].Rows.Count.ToString(), luceneOpt.LogPath);
        ////                foreach (DataRow row in dataset.Tables[0].Rows)
        ////                {
        ////                    try
        ////                    {
        ////                        luceneOpt.HouseDal.UpdateBargainingCountData(row["A_Id"].ToString(), Convert.ToInt16(row["BargainingCount"]));
        ////                        TXCommons.Refurbish.LogTool.Log("赠送vip经纪人标签房:", "Id:" + row["A_Id"].ToString() + "标签房数量:" + row["BargainingCount"].ToString(), luceneOpt.LogPath);

        ////                    }
        ////                    catch (Exception ex)
        ////                    {
        ////                        TXCommons.Refurbish.LogTool.Log("赠送vip经纪人标签房错误", ex.Message, luceneOpt.LogPath);
        ////                    }
        ////                }
        ////            }
        ////        }
        ////    }
        ////    catch (Exception ex)
        ////    {
        ////        TXCommons.Refurbish.LogTool.Log("赠送vip经纪人标签房错误", ex.Message, luceneOpt.LogPath);
        ////    }
        ////}
        ///// <summary>
        ///// 用户操作数据归零
        ///// </summary>
        ////public void UpdateRefurbishEndCountDay()
        ////{
        ////    try
        ////    {
        ////        int count = luceneOpt.HouseDal.UpdateRefurbishEndCountDay();
        ////        int count2 = luceneOpt.HouseDal.UpdateS_LongHouseOtherRefurbishEndCount();
        ////        TXCommons.Refurbish.LogTool.Log("用户操作数据归零数量", count.ToString(), luceneOpt.LogPath);
        ////        TXCommons.Refurbish.LogTool.Log("房源已刷新数据归零数量", count2.ToString(), luceneOpt.LogPath);
        ////    }
        ////    catch (Exception ex)
        ////    {
        ////        TXCommons.Refurbish.LogTool.Log("用户房源操作数据归零错误", ex.Message, luceneOpt.LogPath);
        ////    }
        ////}
        //#endregion

       


        #region   删除预约刷新Redis数据
        public void DeleteRefurbishRedis()
        {
            try
            {
                if (DateTime.Now.Hour == 0)
                {
                    TXCommons.Refurbish.Redis r = new TXCommons.Refurbish.Redis();
                    string str = r.DelKey(7);
                    TXCommons.Refurbish.LogTool.Log("删除预约刷新Redis数据", str, luceneOpt.LogPath);
                }
            }
            catch (Exception ex)
            {
                TXCommons.Refurbish.LogTool.Log("删除预约刷新Redis数据错误", ex.Message, luceneOpt.LogPath);
            }


        }
        #endregion

       

        protected override void OnStop()
        {
            // TODO: 在此处添加代码以执行停止服务所需的关闭操作。
        }
    }
}
