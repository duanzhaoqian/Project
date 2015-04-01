using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.IO;
using System.Threading;
using System.Timers;
using TXCommons;
namespace TXSearchService
{
    partial class TxLongHouseService : ServiceBase
    {
        protected string HouseConnectionString = string.Empty;

        protected string MQQueueName = string.Empty;
        protected string IndexPath = string.Empty;
        protected string LogPath = string.Empty;
        protected int ThreadCount = 0;
        protected LuceneOpt luceneOpt = new LuceneOpt();
        private static object lockobject1 = new object();
        private static List<string> listHouse = new List<string>();
        private static int mqUpdateTime = 10000;
        //private static bool isRead = true;
        public TxLongHouseService()
        {
            InitializeComponent();

            MQ.ConsumerConfiguration.IPAddress = Profile.IniReadValue("public", "MQIPAddress");
            MQ.ConsumerConfiguration.ConnectionTimeOut = new TimeSpan(0, 0, Convert.ToInt32(Profile.IniReadValue("public", "MQConnectionTimeOut")));
            MQ.ConsumerConfiguration.RetryCount = Convert.ToInt32(Profile.IniReadValue("public", "MQRetryCount"));
            HouseConnectionString = Profile.IniReadValue("public", "HouseConnectionString");

            ThreadCount = Convert.ToInt32(Profile.IniReadValue("public", "ThreadCount"));

            MQQueueName = Profile.IniReadValue("TXLongHouseService", "HouseMQQueueName");
            IndexPath = Profile.IniReadValue("TXLongHouseService", "HouseIndexPath");
            LogPath = Profile.IniReadValue("TXLongHouseService", "HouseLogPath");
            luceneOpt.VillageSubWayIndexPath = Profile.IniReadValue("TXVillageService", "VillageSubWayIndexPath");
            luceneOpt.HouseConnectionString = HouseConnectionString;
            mqUpdateTime = int.Parse(Profile.IniReadValue("TXLongHouseService", "MqUpdateTime"));
            luceneOpt.HouseIndexPath = IndexPath;
            luceneOpt.HouseActiveIndexPath = Profile.IniReadValue("TXLongHouseService", "HouseActiveIndexPath");
            luceneOpt.LogPath = LogPath;
            luceneOpt.HouseDal = new Dal.SearchServiceDal(HouseConnectionString);
            luceneOpt.UserDal = new Dal.SearchServiceDal(Profile.IniReadValue("public", "UserConnectionString"));
            luceneOpt.BaseDataConnectionString = Profile.IniReadValue("public", "BaseDataConnectionString");
            luceneOpt.SearchDal = new Dal.SearchServiceDal(luceneOpt.BaseDataConnectionString);
            luceneOpt.Domain = Profile.IniReadValue("public", "Domain");
            luceneOpt.CDNUrl = Profile.IniReadValue("public", "CDNUrl");
            luceneOpt.CityId = Profile.IniReadValue("TXLongHouseService", "CityId");
            luceneOpt.AdvertIndexPath = Profile.IniReadValue("TXVillageService", "AdvertIndexPath");
        }

        protected override void OnStart(string[] args)
        {
            TXCommons.Refurbish.LogTool.Log("房源引服务启动_1", "", LogPath);
            Start();
            CreateThread();
            CreateUpdateMqThread();
        }

        private string[] OperationList(string houseid)
        {
            Monitor.Enter(lockobject1);
            string[] arr = null;
            try
            {

                if (!string.IsNullOrEmpty(houseid))
                {
                    if (!listHouse.Contains(houseid))
                    {
                        listHouse.Add(houseid);
                    }
                }
                else
                {
                    if (listHouse.Count > 0)
                    {
                        arr = new string[listHouse.Count];

                        listHouse.CopyTo(arr);
                        listHouse.Clear();
                    }
                }

            }
            catch (Exception ex)
            {
                TXCommons.Refurbish.LogTool.Log("批量接受消息错误:", ex.Message, LogPath);
            }
            finally
            {
                Monitor.Exit(lockobject1);
            }
            return arr;
        }
      
        /// <summary>
        /// 服务开始执行的函数，负责目录创建以及所有数据索引生成
        /// </summary>
        public void Start()
        {
            try
            {
                DirectoryInfo directoryInfo = new DirectoryInfo(luceneOpt.HouseIndexPath);
                if (!directoryInfo.Exists)
                {
                    directoryInfo.Create();
                }
                if (directoryInfo.GetFiles().Length == 0)
                {
                    int count = luceneOpt.AddDocForLongHouse(string.Empty);
                    TXCommons.Refurbish.LogTool.Log("房源索引生成成功", "共" + count + "条生成", LogPath);
                }
                // DirectoryInfo directoryActiveInfo = new DirectoryInfo(luceneOpt.HouseActiveIndexPath);
                // if (!directoryActiveInfo.Exists)
                //{
                //    directoryActiveInfo.Create();
                //}
                //if (directoryActiveInfo.GetFiles().Length == 0)
                //{
                //    int count = luceneOpt.AddActiveDocForLongHouse();
                //    TXCommons.Refurbish.LogTool.Log("活动房源索引生成成功", "共" + count + "条生成", LogPath);
                //}
                 
            }
            catch (Exception ex)
            {

                TXCommons.Refurbish.LogTool.Log("房源索引生成异常", "Start():" + ex.Message, LogPath);
            }
        }

        /// <summary>
        /// 创建运行线程
        /// </summary>
        private void CreateThread()
        {
            for (int i = 0; i < ThreadCount; i++)
            {
                Thread thread = new Thread(new ThreadStart(ReceiveMessage));
                thread.Start();
            }
        }

        /// <summary>
        /// 创建消息处理
        /// </summary>
        private void CreateUpdateMqThread()
        {
            Thread thread = new Thread(new ThreadStart(UpdateMq));
            thread.Start();
        }

        private void UpdateMq()
        {
            try
            {
                while (true)
                {

                    string[] arr = OperationList("");
                    if (arr != null && arr.Length > 0)
                    {
                        luceneOpt.UpdateLucene("0", luceneOpt.HouseIndexPath, "List",DateTime.Now, string.Empty, string.Empty, arr);
                        arr = null;
                    }
                    string stop = Profile.IniReadValue("TXLongHouseService", "Stop");
                  
                    Thread.Sleep(mqUpdateTime);
                }
            }
            catch (Exception ex)
            {
                TXCommons.Refurbish.LogTool.Log("批量处理消息异常:", ex.Message, LogPath);
            }
        }

        /// <summary>
        /// 接受消息
        /// </summary>
        private void ReceiveMessage()
        {
            try
            {
                MQ.IQueueConsumer queueConsumer = MQ.ConsumerFactory.CreateQueueConsumer(MQQueueName);
                while (true)
                {
                    try
                    {
                      
                        string result = queueConsumer.ReceiveTextMessage();
                        if (string.IsNullOrEmpty(result))
                        {
                            if (!string.IsNullOrEmpty(queueConsumer.ErrorMessage))
                            {
                                TXCommons.Refurbish.LogTool.Log("房源消息接收异常", queueConsumer.ErrorMessage, LogPath);
                            }
                        }
                        else
                        {
                            string logMsg = string.Empty;
                            if (result.Trim() == "optimize")
                            {

                                TXCommons.Refurbish.LogTool.Log("Info", "优化成功", LogPath);
                            }
                            else
                            {
                                this.Start(result);
                            }
                            logMsg = "收到消息：" + result;
                            TXCommons.Refurbish.LogTool.Log("房源收到消息", logMsg, LogPath);
                        }
                    }
                    catch (Exception ex)
                    {
                        TXCommons.Refurbish.LogTool.Log("房源消息接收异常", ex.Message, LogPath);
                    }
                }
            }
            catch (Exception ex)
            {
                TXCommons.Refurbish.LogTool.Log("房源消息接收异常", ex.Message, LogPath);
            }
        }

        
        public void Start(string message)
        {

            try
            {
                if (string.IsNullOrEmpty(message) || message.IndexOf(",") == -1)
                {
                    return;
                }
                string[] messages = message.Split(',');
                string opt = messages[0].Trim();
                string houseid = messages[1].Trim();
                string cityid = messages[2].Trim();



                switch (opt)
                {
                    case "insert":
                        luceneOpt.AddDocForLongHouse(houseid);
                        break;
                    case "update":
                            //luceneOpt.UpdateHouseDoc(houseid, IndexPath))
                            //luceneOpt.UpdateLucene(houseid, IndexPath, "Update");
                            OperationList(houseid);
                       
                        break;
                    case "delete":
                        luceneOpt.DeleteHouseDoc(houseid, "Id", IndexPath);
                        //luceneOpt.ClearCDN(houseid,cityid);
                        break;
                    case "User":
                        string userid = messages[1].Trim();
                        string usertype = messages[3].Trim();
                        luceneOpt.UpdateLucene(houseid, IndexPath, "User", DateTime.Now, userid, usertype);
                        //luceneOpt.ClearCDN(houseid,cityid);
                        break;
                    case "active":
                        //luceneOpt.UpdateHouseDoc(houseid, IndexPath))
                        //luceneOpt.UpdateActiveLucene(houseid, luceneOpt.HouseActiveIndexPath, "Update");

                        break;
                    case "advert":
                        //luceneOpt.UpdateHouseDoc(houseid, IndexPath))
                        luceneOpt.UpdateAdvertList("list", houseid);

                        break;
                    default:
                        break;
                }
                

            }
            catch (Exception ex)
            {
                TXCommons.Refurbish.LogTool.Log("房源消息接收异常", "Start(string message)：" + ex.Message, LogPath);
            }

        }

        protected override void OnStop()
        {

        }
    }
}
