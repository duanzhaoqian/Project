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
using System.Transactions;
using TXCommons;
namespace TXSearchService
{
    partial class TxHouseBidStatusService : ServiceBase
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
        public TxHouseBidStatusService()
        {
            InitializeComponent();
            HouseConnectionString = Profile.IniReadValue("public", "HouseConnectionString");
            IndexPath = Profile.IniReadValue("TXLongHouseService", "HouseIndexPath");
            LogPath = Profile.IniReadValue("TXLongHouseService", "HouseLogPath");
            luceneOpt.HouseDal = new Dal.SearchServiceDal(HouseConnectionString);
            luceneOpt.UserDal = new Dal.SearchServiceDal(Profile.IniReadValue("public", "UserConnectionString"));
            luceneOpt.BaseDataConnectionString = Profile.IniReadValue("public", "BaseDataConnectionString");
            luceneOpt.SearchDal = new Dal.SearchServiceDal(luceneOpt.BaseDataConnectionString);
            luceneOpt.HouseIndexPath = IndexPath;
            luceneOpt.HouseActiveIndexPath = Profile.IniReadValue("TXLongHouseService", "HouseActiveIndexPath");
            luceneOpt.VillageSubWayIndexPath = Profile.IniReadValue("TXVillageService", "VillageSubWayIndexPath");
            luceneOpt.LogPath = LogPath;
            luceneOpt.Domain = Profile.IniReadValue("public", "Domain");
            luceneOpt.CDNUrl = Profile.IniReadValue("public", "CDNUrl");
            luceneOpt.SMSUrl = Profile.IniReadValue("public", "SMSUrl");
            luceneOpt.CityId = Profile.IniReadValue("TXLongHouseService", "CityId");
        }

        protected override void OnStart(string[] args)
        {
            
            // TODO: 在此处添加代码以启动服务。
           // HouseBIdStart();
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
          
            //TXCommons.Refurbish.LogTool.Log("分钟服务启动_1", DateTime.Now.ToString() + DateTime.Now.Millisecond.ToString(), LogPath);


            //while (true)
            //{
            //    Thread.Sleep(100);
            //    if (DateTime.Now.Second == 30)
            //    {

            //        break;
            //    }
            //}
            timerHour.Elapsed += new ElapsedEventHandler(timerHour_Elapsed);
            timerHour.Interval = 1000 * 60 * 60;
            timerHour.AutoReset = true;
            timerHour.Enabled = true;


            TXCommons.Refurbish.LogTool.Log("小时服务启动_1", DateTime.Now.ToString() + DateTime.Now.Millisecond.ToString(), LogPath);
            //TXCommons.Refurbish.LogTool.Log("房源推广服务:", "", luceneOpt.LogPath);
            //UpdateHouseIsRecommend();
            //TXCommons.Refurbish.LogTool.Log("到期下架:", "", luceneOpt.LogPath);
            //UpdateHouseBao();

            //TXCommons.Refurbish.LogTool.Log("房源省心租服务:", "", luceneOpt.LogPath);
            ////UpdateIsPlat();
            //TXCommons.Refurbish.LogTool.Log("最新服务:", "", luceneOpt.LogPath);
            //UpdateNew1();
        }

        #region  小时定时处理
        protected void timerHour_Elapsed(object sender, ElapsedEventArgs e)
        {
            
             try
             {
                    TXCommons.Refurbish.LogTool.Log("当前小时", DateTime .Now.Hour.ToString(), luceneOpt.LogPath);
                   
                     HouseBIdStart();
                     
                    
             }
             catch (Exception ex)
             {
                 TXCommons.Refurbish.LogTool.Log("房源出价，刷新定时服务错误", ex.Message, luceneOpt.LogPath);
             }
           
           
        }
        #endregion


        #region 分钟定时处理
        protected void timerMinute_Elapsed(object sender, ElapsedEventArgs e)
        {
            DateTime date = DateTime.Now;
            if (date.Minute % 2 == 0)
            {
                luceneOpt.UpdateLucene("0", luceneOpt.HouseIndexPath, "UpdateRefurbish",date);

            }
        }
        #endregion

        #region   处理房源出价处理状态


        public void HouseBIdStart()
        {
            if (DateTime.Now.Hour == 0)
            {
                TXCommons.Refurbish.LogTool.Log("----------------------------房源出价中定时服务-----------------------", DateTime.Now.Hour.ToString(), luceneOpt.LogPath);
               
                try
                {

                    TXCommons.Refurbish.LogTool.Log("房源推广服务:", "", luceneOpt.LogPath);
                    UpdateHouseIsRecommend();

                    //string isState = Profile.IniReadValue("TXLongHouseService", "isState");
                    //if (isState == "1")
                    //{
                        TXCommons.Refurbish.LogTool.Log("到期下架:", "", luceneOpt.LogPath);
                        UpdateHouseBao();

                    //}

                    //else
                    //{
                    //    TXCommons.Refurbish.LogTool.Log("砍价房源出价结束定时服务已停止", "", luceneOpt.LogPath);
                    //}
                    //TXCommons.Refurbish.LogTool.Log("修改置顶过期:","", luceneOpt.LogPath);
                    //UpdateIsRecommend();
                    TXCommons.Refurbish.LogTool.Log("房源省心租服务:", "", luceneOpt.LogPath);
                    //UpdateIsPlat();
                    UpdateNew1();
                   
                }
                catch (Exception ex)
                {
                    TXCommons.Refurbish.LogTool.Log("砍价房源出价结束定时服务配置错误", ex.Message, luceneOpt.LogPath);
                }

                
                TXCommons.Refurbish.LogTool.Log("----------------------------砍价房源出价结束定时服务-----------------------", DateTime.Now.Hour.ToString(), luceneOpt.LogPath);
            }

        }

      


        /// <summary>
        /// 修改报价房过期
        /// </summary>
        public void UpdateHouseBao()
        {
           
            try
            {
               
                DataSet dataset = luceneOpt.HouseDal.GetHouseBao(luceneOpt .CityId);
                string houseid = "";
                bool b = true;
                if (dataset != null && dataset.Tables.Count > 0 && dataset.Tables[0].Rows.Count > 0)
                {
                    
                    TXCommons.Refurbish.LogTool.Log("----------修改报价房过期处理开始", "房源数量:" + dataset.Tables[0].Rows.Count + "CityId:" + luceneOpt.CityId, LogPath);
                    List<string> ids = new List<string>();
                    foreach (DataRow row in dataset.Tables[0].Rows)
                    {
                        try
                        {
                            houseid = row["Id"].ToString().Trim();
                            b = true;
                           
                            DataSet datasettime = luceneOpt.HouseDal.GetHouseAdvert(houseid);
                            if (datasettime != null && datasettime.Tables.Count > 0 && datasettime.Tables[0].Rows.Count > 0)
                            {
                                if (DateTime.Now <= DateTime .Parse (datasettime.Tables[0].Rows[0]["buytime"].ToString()))
                                {
                                    b = false;
                                }
                            }
                            if (b)
                            {
                                luceneOpt.HouseDal.UpdateHouseState(houseid, 0);
                                ids.Add(houseid);
                            }
                            else
                            {
                                TXCommons.Refurbish.LogTool.Log("修改报价房过期:houseid:"+houseid+":","正在推广中" , LogPath);
                            }
                        }
                        catch (Exception ex)
                        {
                            TXCommons.Refurbish.LogTool.Log("修改报价房过期服务错误", ex.Message, LogPath);
                        }
                    }
                    luceneOpt.UpdateLucene("0", luceneOpt.HouseIndexPath, "List", DateTime.Now, string.Empty, string.Empty, ids.ToArray());
                    TXCommons.Refurbish.LogTool.Log("----------修改报价房过期处理结束", "房源数量:" + dataset.Tables[0].Rows.Count + "CityId:" + luceneOpt.CityId, LogPath);
                }

            }
            catch (Exception ex)
            {
                TXCommons.Refurbish.LogTool.Log("修改报价房过期服务执行错误:", ex.Message, LogPath);
            }
            finally
            {
              
            }
        }




        /// <summary>
        /// 房源失去省心租服务
        /// </summary>
        //public void UpdateIsPlat()
        //{
           
        //    try
        //    {

        //        DataSet dataset = luceneOpt.HouseDal.GetHouseIsPlat(luceneOpt.CityId);
        //        string houseid = "";
        //        string id = "";
        //        if (dataset != null && dataset.Tables.Count > 0 && dataset.Tables[0].Rows.Count > 0)
        //        {
                  
        //            TXCommons.Refurbish.LogTool.Log("----------修改房源失去省心租服务开始", "房源数量:" + dataset.Tables[0].Rows.Count + "CityId:" + luceneOpt.CityId, LogPath);
        //            List<string> ids = new List<string>();
        //            foreach (DataRow row in dataset.Tables[0].Rows)
        //            {
        //                try
        //                {
        //                    houseid = row["houseid"].ToString().Trim();
        //                    id = row["id"].ToString().Trim();
        //                    int day = Convert.ToInt32(row["DayNum"]);
        //                    double price = Convert.ToDouble(row["AllPrice"]);
        //                    double sprice = price / 365 * 0.1 * day;
        //                    sprice = Math.Round(sprice, 2);
                            
        //                    luceneOpt.HouseDal.UpdateIsBack(id);
        //                    TXCommons.Refurbish.LogTool.Log("自动退还保证金Id:", id, LogPath);
        //                    luceneOpt.UserDal.UpdateU_AccountTransaction(Convert.ToInt32(row["UserId"]), price, 6, "返还保证金");
        //                    TXCommons.Refurbish.LogTool.Log("退还保证金：", "UserId:" + Convert.ToInt32(row["UserId"]) + "_AllPrice:" + price, LogPath);

        //                    //if (sprice > 0)
        //                    //{
        //                        TXCommons.Refurbish.LogTool.Log("自动退还保证金返还收益Id:", id, LogPath);
        //                        luceneOpt.UserDal.UpdateU_AccountTransaction(Convert.ToInt32(row["UserId"]), sprice, 6, "保证金返还收益");
        //                        TXCommons.Refurbish.LogTool.Log("退还保证金返还收益：", "UserId:" + Convert.ToInt32(row["UserId"]) + "_AllSPrice:" + sprice, LogPath);
        //                    //}
        //                    //else
        //                    //{
        //                    //    TXCommons.Refurbish.LogTool.Log("小于0，未退还,自动退还保证金返还收益Id:", id, LogPath);
        //                    //}
                            
        //                    luceneOpt.HouseDal.UpdateIsPlat(houseid);
                           
        //                    ids.Add(houseid);
        //                }
        //                catch (Exception ex)
        //                {
        //                    TXCommons.Refurbish.LogTool.Log("修改房源失去省心租服务服务错误", ex.Message, LogPath);
        //                }
        //            }
        //            luceneOpt.UpdateLucene("0", luceneOpt.HouseIndexPath, "List", DateTime.Now, string.Empty, string.Empty, ids.ToArray());
        //            TXCommons.Refurbish.LogTool.Log("----------修改房源失去省心租服务处理结束", "房源数量:" + dataset.Tables[0].Rows.Count + "CityId:" + luceneOpt.CityId, LogPath);
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        TXCommons.Refurbish.LogTool.Log("修改房源失去省心租服务服务执行错误:", ex.Message, LogPath);
        //    }
        //    finally
        //    {
              
        //    }
        //}

        /// <summary>
        /// 修改房源推广状态
        /// </summary>
        public void UpdateHouseIsRecommend()
        {

            try
            {

                DataSet dataset = luceneOpt.HouseDal.GetHouseAdvertIsRecommend(luceneOpt.CityId);
                string houseid = "";
                List<string> ids = new List<string>();
                if (dataset != null && dataset.Tables.Count > 0 && dataset.Tables[0].Rows.Count > 0)
                {

                    TXCommons.Refurbish.LogTool.Log("----------修改房源失去推广服务开始", "房源数量:" + dataset.Tables[0].Rows.Count + "CityId:" + luceneOpt.CityId, LogPath);
                  
                    foreach (DataRow row in dataset.Tables[0].Rows)
                    {
                        try
                        {
                            houseid = row["Id"].ToString().Trim();
                            //if (luceneOpt.HouseDal.GetAdvertCount(houseid) > 0)
                            //{
                                luceneOpt.HouseDal.UpdateIsRecommend(houseid, 0);

                                ids.Add(houseid);

                                if (row["UserType"].ToString().Trim() == "1")
                                {
                                    //luceneOpt.HouseDal.UpdateHouseState(houseid, 0);
                                    //SMSTool smsTool = new SMSTool();

                                    //smsTool.sendSms(SMSOptionType.SEND_SMS, row["Mobile"].ToString(), "您的房源推广到期，房源已经下架");
                                    //TXCommons.Refurbish.LogTool.Log("修改房源失去推广服务：", "经纪人下架", LogPath);
                                }
                            //}
                            //else
                            //{
                            //    TXCommons.Refurbish.LogTool.Log("修改房源", "房源还在推广中", LogPath);
                            //}
                        }
                        catch (Exception ex)
                        {
                            TXCommons.Refurbish.LogTool.Log("修改房源失去推广服务错误", ex.Message, LogPath);
                        }
                    }
                    TXCommons.Refurbish.LogTool.Log("----------修改房源失去推广服务处理结束", "房源数量:" + dataset.Tables[0].Rows.Count + "CityId:" + luceneOpt.CityId, LogPath);
                }



                DataSet dataseth = luceneOpt.HouseDal.GetHouseAdvertIsRecommendUserType(luceneOpt.CityId);

                if (dataseth != null && dataseth.Tables.Count > 0 && dataseth.Tables[0].Rows.Count > 0)
                {

                    TXCommons.Refurbish.LogTool.Log("----------修改经纪人推广上架服务开始", "房源数量:" + dataseth.Tables[0].Rows.Count + "CityId:" + luceneOpt.CityId, LogPath);
                    foreach (DataRow row in dataseth.Tables[0].Rows)
                    {
                        try
                        {
                            houseid = row["Id"].ToString().Trim();
                            luceneOpt.HouseDal.UpdateHouseStateUserType(houseid, 1);
                            if (!ids.Contains(houseid))
                                ids.Add(houseid);
                        }
                        catch (Exception ex)
                        {
                            TXCommons.Refurbish.LogTool.Log("修改经纪人推广上架服务服务错误", ex.Message, LogPath);
                        }
                    }

                    TXCommons.Refurbish.LogTool.Log("----------修改经纪人推广上架服务处理结束", "房源数量:" + dataseth.Tables[0].Rows.Count + "CityId:" + luceneOpt.CityId, LogPath);
                }

                TXCommons.Refurbish.LogTool.Log("----------修改经纪人推广上架索引开始", "房源数量:" + ids.Count + "CityId:" + luceneOpt.CityId, LogPath);
                luceneOpt.UpdateLucene("0", luceneOpt.HouseIndexPath, "List", DateTime.Now, string.Empty, string.Empty, ids.ToArray());
            }
            catch (Exception ex)
            {
                TXCommons.Refurbish.LogTool.Log("修改房源推广服务服务执行错误:", ex.Message, LogPath);
            }
            finally
            {

            }
        }


        /// <summary>
        /// 最新服务
        /// </summary>
        public void UpdateNew1()
        {

            try
            {

                DataSet dataset = luceneOpt.HouseDal.GetHouseNew1(luceneOpt.CityId);
                string houseid = "";
                List<string> ids = new List<string>();
                if (dataset != null && dataset.Tables.Count > 0 && dataset.Tables[0].Rows.Count > 0)
                {

                    TXCommons.Refurbish.LogTool.Log("----------最新服务", "房源数量:"+ dataset.Tables[0].Rows.Count + "CityId:" + luceneOpt.CityId, LogPath);

                    foreach (DataRow row in dataset.Tables[0].Rows)
                    {
                        try
                        {
                            houseid = row["HouseId"].ToString().Trim();
                            luceneOpt.HouseDal.UpdateNew1(houseid);

                            ids.Add(houseid);
                            TXCommons.Refurbish.LogTool.Log("最新服务:", houseid, LogPath);
                           
                        }
                        catch (Exception ex)
                        {
                            TXCommons.Refurbish.LogTool.Log("最新服务错误", ex.Message, LogPath);
                        }
                    }
                    TXCommons.Refurbish.LogTool.Log("----------最新服务处理结束", "房源数量:"  + dataset.Tables[0].Rows.Count + "CityId:" + luceneOpt.CityId, LogPath);
                }





                TXCommons.Refurbish.LogTool.Log("----------最新服务索引开始", "房源数量:" + ids.Count + "CityId:" + luceneOpt.CityId, LogPath);
                luceneOpt.UpdateLucene("0", luceneOpt.HouseIndexPath, "List", DateTime.Now, string.Empty, string.Empty, ids.ToArray());
            }
            catch (Exception ex)
            {
                TXCommons.Refurbish.LogTool.Log("修改房源推广服务服务执行错误:", ex.Message, LogPath);
            }
            finally
            {

            }
        }
        #endregion


       
        protected override void OnStop()
        {
            // TODO: 在此处添加代码以执行停止服务所需的关闭操作。
        }
    }
}
