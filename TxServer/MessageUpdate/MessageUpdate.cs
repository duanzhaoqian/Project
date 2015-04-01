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


namespace MessageUpdate
{
    public partial class MessageUpdate : ServiceBase
    {
        protected string MQQueueName = string.Empty;


        protected int ThreadCount = 0;
        protected LuceneOpt luceneOpt = new LuceneOpt();
        private System.Timers.Timer timerArea = new System.Timers.Timer();
        private static object lockobject1 = new object();
        private System.Timers.Timer timerMinute = new System.Timers.Timer();
        protected string LogPath = string.Empty;
        public MessageUpdate()
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
            TXCommons.Refurbish.LogTool.Log("小区,区域，地铁索引服务启动_1", "", luceneOpt.LogPath);
            ServerStart();

            //TXCommons.Refurbish.LogTool.Log("自动退还保证金:", "", luceneOpt.LogPath);
            //UpdateIsBack();
            //TXCommons.Refurbish.LogTool.Log("打开房租支付开口:", "", luceneOpt.LogPath);
            //UpdateIsOpen();
            //TXCommons.Refurbish.LogTool.Log("到期返还保证金:", "", luceneOpt.LogPath);
            //UpdateIsLock();
            //TXCommons.Refurbish.LogTool.Log("游戏处理开始:", "", luceneOpt.LogPath);
            //UpdateGame();

            timerArea.Elapsed += new ElapsedEventHandler(timerArea_Elapsed);
            timerArea.Interval = 1000 * 60 * 60;
            timerArea.AutoReset = true;
            timerArea.Enabled = true;
        }

        protected void timerArea_Elapsed(object sender, ElapsedEventArgs e)
        {

            try
            {


                ServerStart();



            }
            catch (Exception ex)
            {
                TXCommons.Refurbish.LogTool.Log("定时服务错误", ex.Message, luceneOpt.LogPath);
            }

        }

        #region  经纪人服务到期相关服务
        /// <summary>
        /// 经纪人服务启动
        /// </summary>
        public void ServerStart()
        {
            if (DateTime.Now.Hour == 3)
            {
                //TXCommons.Refurbish.LogTool.Log("自动退还保证金:", "", luceneOpt.LogPath);
                //UpdateIsBack();
                TXCommons.Refurbish.LogTool.Log("打开房租支付开口:", "", luceneOpt.LogPath);
                UpdateIsOpen();
                TXCommons.Refurbish.LogTool.Log("到期返还保证金:", "", luceneOpt.LogPath);
                UpdateIsLock();

                //TXCommons.Refurbish.LogTool.Log("游戏处理开始:", "", luceneOpt.LogPath);
                //UpdateGame();

            }

        }


        /// <summary>
        /// 自动退还保证金
        /// </summary>
        public void UpdateIsBack()
        {

            try
            {

                DataSet dataset = luceneOpt.HouseDal.GetHouseIsBack(luceneOpt.CityId);
                string id = "";
                if (dataset != null && dataset.Tables.Count > 0 && dataset.Tables[0].Rows.Count > 0)
                {

                    TXCommons.Refurbish.LogTool.Log("----------自动退还保证金服务开始", "数量:" + dataset.Tables[0].Rows.Count + "CityId:" + luceneOpt.CityId, LogPath);
                    List<string> ids = new List<string>();
                    foreach (DataRow row in dataset.Tables[0].Rows)
                    {
                        try
                        {
                            id = row["id"].ToString().Trim();
                            int day = Convert.ToInt32(row["DayNum"]);
                            double price = Convert.ToDouble(row["AllPrice"]);
                            double sprice = price / 365 * 0.07 * day;
                            luceneOpt.HouseDal.UpdateIsBack(id);
                            TXCommons.Refurbish.LogTool.Log("自动退还保证金Id:", id, LogPath);
                            luceneOpt.UserDal.UpdateU_AccountTransaction(Convert.ToInt32(row["UserId"]), price, 6, "返还保证金");
                            TXCommons.Refurbish.LogTool.Log("退还保证金：", "UserId:" + Convert.ToInt32(row["UserId"]) + "_AllPrice:" + price, LogPath);

                            TXCommons.Refurbish.LogTool.Log("自动退还保证金返还收益Id:", id, LogPath);
                            luceneOpt.UserDal.UpdateU_AccountTransaction(Convert.ToInt32(row["UserId"]), sprice, 14, "保证金返还收益");
                            TXCommons.Refurbish.LogTool.Log("退还保证金返还收益：", "UserId:" + Convert.ToInt32(row["UserId"]) + "_AllSPrice:" + sprice, LogPath);
                            //ids.Add(houseid);
                        }
                        catch (Exception ex)
                        {
                            TXCommons.Refurbish.LogTool.Log("自动退还保证金服务错误", ex.Message, LogPath);
                        }
                    }
                    //luceneOpt.UpdateLucene("0", luceneOpt.HouseIndexPath, "List", DateTime.Now, string.Empty, string.Empty, ids.ToArray());
                    TXCommons.Refurbish.LogTool.Log("----------自动退还保证金处理结束", "数量:" + dataset.Tables[0].Rows.Count + "CityId:" + luceneOpt.CityId, LogPath);
                }

            }
            catch (Exception ex)
            {
                TXCommons.Refurbish.LogTool.Log("自动退还保证金执行错误:", ex.Message, LogPath);
            }
            finally
            {

            }
        }

        /// <summary>
        /// 打开房租支付开口
        /// </summary>
        public void UpdateIsOpen()
        {

            try
            {


                DataSet datasethouse = luceneOpt.HouseDal.GetHouseIsOpenHouse(luceneOpt.CityId);
                if (datasethouse != null && datasethouse.Tables.Count > 0 && datasethouse.Tables[0].Rows.Count > 0)
                {
                    TXCommons.Refurbish.LogTool.Log("----------打开房租支付开口服务开始", "房源数量:" + datasethouse.Tables[0].Rows.Count + "CityId:" + luceneOpt.CityId, LogPath);
                    foreach (DataRow rowh in datasethouse.Tables[0].Rows)
                    {
                        string houseid = rowh["houseid"].ToString().Trim();
                        DataSet dataset = luceneOpt.HouseDal.GetHouseIsOpen(houseid);
                        string cid = "";
                        string pid = "";
                        string time = "";
                        if (dataset != null && dataset.Tables.Count > 0 && dataset.Tables[0].Rows.Count > 0)
                        {

                            TXCommons.Refurbish.LogTool.Log("----------打开房租支付开口服务开始", "数量:" + dataset.Tables[0].Rows.Count + "CityId:" + luceneOpt.CityId, LogPath);

                            DataRow row = dataset.Tables[0].Rows[0];
                            try
                            {
                                cid = row["cid"].ToString().Trim();
                                pid = row["pid"].ToString().Trim();
                                time = row["time"].ToString().Trim();
                                TXCommons.Refurbish.LogTool.Log("打开房租支付开口服务c", "cid：" + cid + "time:" + time, LogPath);
                                luceneOpt.HouseDal.UpdateIsOpenC(cid, time);
                                TXCommons.Refurbish.LogTool.Log("打开房租支付开口服务c", "pid：" + pid, LogPath);
                                luceneOpt.HouseDal.UpdateIsOpenP(pid);

                            }
                            catch (Exception ex)
                            {
                                TXCommons.Refurbish.LogTool.Log("打开房租支付开口服务错误", ex.Message, LogPath);
                            }


                            TXCommons.Refurbish.LogTool.Log("----------打开房租支付开口处理结束", "数量:" + dataset.Tables[0].Rows.Count + "CityId:" + luceneOpt.CityId, LogPath);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                TXCommons.Refurbish.LogTool.Log("打开房租支付开口执行错误:", ex.Message, LogPath);
            }
            finally
            {

            }
        }

        /// <summary>
        ///到期返回保证金
        /// </summary>
        public void UpdateIsLock()
        {

            try
            {

                DataSet dataset = luceneOpt.HouseDal.GetHouseIsLock(luceneOpt.CityId);
                string id = "";

                if (dataset != null && dataset.Tables.Count > 0 && dataset.Tables[0].Rows.Count > 0)
                {

                    TXCommons.Refurbish.LogTool.Log("----------到期返回保证金服务开始", "数量:" + dataset.Tables[0].Rows.Count + "CityId:" + luceneOpt.CityId, LogPath);
                    List<string> ids = new List<string>();
                    foreach (DataRow row in dataset.Tables[0].Rows)
                    {
                        try
                        {
                            id = row["id"].ToString().Trim();

                            luceneOpt.HouseDal.UpdateIsLock(id);
                            //ids.Add(houseid);
                        }
                        catch (Exception ex)
                        {
                            TXCommons.Refurbish.LogTool.Log("到期返回保证金服务错误", ex.Message, LogPath);
                        }
                    }
                    //luceneOpt.UpdateLucene("0", luceneOpt.HouseIndexPath, "List", DateTime.Now, string.Empty, string.Empty, ids.ToArray());
                    TXCommons.Refurbish.LogTool.Log("----------到期返回保证金处理结束", "数量:" + dataset.Tables[0].Rows.Count + "CityId:" + luceneOpt.CityId, LogPath);
                }

            }
            catch (Exception ex)
            {
                TXCommons.Refurbish.LogTool.Log("到期返回保证金执行错误:", ex.Message, LogPath);
            }
            finally
            {

            }
        }
        /// <summary>
        /// 游戏状态修改
        /// </summary>
        public void UpdateGame()
        {
            try
            {
                TXCommons.Refurbish.LogTool.Log("游戏开始数量:", luceneOpt.HouseDal.UpdateGameStateBegin().ToString(), LogPath);
                TXCommons.Refurbish.LogTool.Log("游戏结束数量:", luceneOpt.HouseDal.UpdateGameStateEnd().ToString(), LogPath);
            }
            catch (Exception ex)
            {
                TXCommons.Refurbish.LogTool.Log("游戏执行错误:", ex.Message, LogPath);
            }
            finally
            {

            }
        }

     
        #endregion
     
        protected override void OnStop()
        {
        }
    }
}
