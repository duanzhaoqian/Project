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
using System.Data.SqlClient;

namespace TXTimingService
{
    public partial class TXTimingService : ServiceBase
    {
        protected string ConnectionString = string.Empty;
        protected string Logpath = string.Empty;
      
        protected string MQLongHouseSearchQueueName = string.Empty;
        MQ.IQueueProducer queueProducer = null;
        Dal.TimingServiceDal tsDal = null;
         private System.Timers.Timer timerHouseBid = new System.Timers.Timer();
        private static object lockobject1 = new object();
        public TXTimingService()
        {
            InitializeComponent();
            ConnectionString = Profile.IniReadValue("OrderTiming", "ConnectionString");
            Logpath = Profile.IniReadValue("OrderTiming", "Logpath");
             
            MQ.ProducerConfiguration.IPAddress = Profile.IniReadValue("OrderTiming", "MQIPAddress");
            MQ.ProducerConfiguration.ConnectionTimeOut = new TimeSpan(0, 0, Convert.ToInt32(Profile.IniReadValue("OrderTiming", "MQConnectionTimeOut")));
            MQ.ProducerConfiguration.RetryCount = Convert.ToInt32(Profile.IniReadValue("OrderTiming", "MQRetryCount"));

            MQLongHouseSearchQueueName = Profile.IniReadValue("OrderTiming", "MQLongHouseSearchQueueName");

            tsDal = new Dal.TimingServiceDal(ConnectionString);
        }

        protected override void OnStart(string[] args)
        {
           
                //处理订单房东待接受状态的timer初始化
            timerHouseBid.Elapsed += new ElapsedEventHandler(timerHouseBid_Elapsed);
            timerHouseBid.Interval = 1000 * 60 * 1;
            timerHouseBid.AutoReset = true;
            timerHouseBid.Enabled = true;
          


           TXCommons.Refurbish.LogTool.Log("Info", DateTime.Now.ToString() + DateTime.Now.Millisecond.ToString() + " : TXTimingService定时服务启动",Logpath);
        }

        #region  timerHouseBid 处理房源出价待处理状态
        protected void timerHouseBid_Elapsed(object sender, ElapsedEventArgs e)
        {
            Monitor.Enter(lockobject1);
            try
            {
              
                  DataSet dataset = tsDal.GetHouseBid();
                 string houseid = "";
                 string Status = "";
                 if (dataset != null && dataset.Tables.Count > 0 && dataset.Tables[0].Rows.Count > 0)
                 {
                    // TXBll.BidPrice.BidPriceBll bll = new TXBll.BidPrice.BidPriceBll();
                     foreach (DataRow row in dataset.Tables[0].Rows)
                     {
                         houseid = row["HouseId"].ToString().Trim();
                         Status = row["HouseBidStatus"].ToString().Trim();
                         string message =houseid  + "-" + Status  + "-" + row["Id"].ToString();
                         queueProducer.Send("LongTime," + message, Apache.NMS.MsgPriority.Normal);
                         
                         if (Status == "3")
                         {
                            // bll.UpdateBidStatus("1", houseid, null, 3);
                             message += "_投资状态已处理";
                         }
                         TXCommons.Refurbish.LogTool.Log("LongTime发送", message,Logpath);
                     }
                   
                 }
          
            }
            catch (Exception ex)
            {
                TXCommons.Refurbish.LogTool.Log("error", "timer1_Elapsed执行错误:" + ex.Message,Logpath);
            }
            finally
            {
                Monitor.Exit(lockobject1);
            }
        }
        #endregion

        protected override void OnStop()
        {

        }
    }
}
