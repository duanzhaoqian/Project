using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.IO;
using KYJ.ZS.Commons;
using System.Threading;

namespace TXSearchService
{
   
    public partial class TxGoodsService : ServiceBase
    {
        private static object mylock = new object();
        protected LuceneOpt luceneOpt = new LuceneOpt();
        //商品lucene目录
        private string GoodsIndexPath = Auxiliary.ConfigKey("GoodsIndexPath");
        private string LogPath = Auxiliary.ConfigKey("LogPath");
        protected int ThreadCount = 0;
        //mq名称
        private string MQQueueName = Auxiliary.ConfigKey("HouseMQQueueName");
        public TxGoodsService()
        {
            InitializeComponent();
            luceneOpt.GoodsIndexPath = GoodsIndexPath;
        }

        protected override void OnStart(string[] args)
        {
            CreateThread();
            CreateUpdateMqThread();
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
                                KYJ.ZS.Commons.Tool.Log("商品消息接收异常", queueConsumer.ErrorMessage, LogPath);
                            }
                        }
                        else
                        {
                            string logMsg = string.Empty;
                            if (result.Trim() == "optimize")
                            {

                                KYJ.ZS.Commons.Tool.Log("Info", "优化成功", LogPath);
                            }
                            logMsg = "收到消息：" + result;
                            KYJ.ZS.Commons.Tool.Log("商品收到消息", logMsg, LogPath);
                        }
                    }
                    catch (Exception ex)
                    {
                        KYJ.ZS.Commons.Tool.Log("商品消息接收异常", ex.Message, LogPath);
                    }
                }
            }
            catch (Exception ex)
            {
                KYJ.ZS.Commons.Tool.Log("商品消息接收异常", ex.Message, LogPath);
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
                string goodsid = messages[1].Trim();

                switch (opt)
                {
                    case "update":
                        OperationList(goodsid);
                        break;
                    case "delete":
                        luceneOpt.DeleteHouseDoc(goodsid, "Id", GoodsIndexPath);
                        //luceneOpt.ClearCDN(houseid,cityid);
                        break;
                    default:
                        break;
                }


            }
            catch (Exception ex)
            {
                KYJ.ZS.Commons.Tool.Log("商品消息接收异常", "Start(string message)：" + ex.Message, LogPath);
            }

        }

        private string[] OperationList(string houseid)
        {
            Monitor.Enter(mylock);
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
        protected override void OnStop()
        {
        }
    }
}
