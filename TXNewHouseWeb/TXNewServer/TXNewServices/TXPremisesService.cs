using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Configuration;
using Commons;
using System.Threading;

namespace TXNewServices
{
    partial class TXPremisesService : ServiceBase
    {
        protected static string MQIPAddress = string.Empty;
        protected static string MQQueueName = string.Empty;
        protected static string MQConnectionTimeOut = string.Empty;
        protected static string MQRetryCount = string.Empty;
        protected static string LogPath = string.Empty;
        protected static int ThreadCount = 1;
        /// <summary>
        /// 静态词典，用作接收MQ消息操作
        /// </summary>
        protected static Dictionary<string, List<OperClass>> dictionary = new Dictionary<string, List<OperClass>>();

        public TXPremisesService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            //Thread.Sleep(1000 * 20);

            LogPath = Profile.IniReadValue("TXPremisesService", "LogPath");
            ThreadCount = int.Parse(Profile.IniReadValue("TXPremisesService", "ThreadCount"));


            MQ.ConsumerConfiguration.IPAddress = Profile.IniReadValue("TXPremisesService", "MQIPAddress");
            MQ.ConsumerConfiguration.ConnectionTimeOut = new TimeSpan(0, 0, Convert.ToInt32(Profile.IniReadValue("TXPremisesService", "MQConnectionTimeOut")));
            MQ.ConsumerConfiguration.RetryCount = Convert.ToInt32(Profile.IniReadValue("TXPremisesService", "MQRetryCount"));
            //设置lucenexml内容
            MQHelper.SetDictionary();


            OptLog.Log("Info：", "TXPremises", "楼盘服务启动：" + DateTime.Now, true);
        

            //Thread TCreateThread = new Thread(CreateThread);
            //TCreateThread.Start();//监听mq，并把mq数据加载到内存
            //Thread TCreateStart = new Thread(CreateStart);
            //TCreateStart.Start();//获取内存数据，并生成Lucene
            CreateThread();
            CreateStart();
           

        }
        private void CreateStart()
        {
            var mqnames = MQHelper.GetQueueNamesInfomation();
            foreach (var item in mqnames)
            {
                ParameterizedThreadStart ParStart = new ParameterizedThreadStart(Start);
                Thread myThread = new Thread(ParStart);
                object o = item;
                myThread.Start(o);
            }
        }
        /// <summary>
        /// 创建运行线程
        /// </summary>
        private void CreateThread()
        {
            var mqnames = MQHelper.GetQueueNamesInfomation();
            foreach (var item in mqnames)
            {
                //ReadMQ(item);
                ParameterizedThreadStart ParStart = new ParameterizedThreadStart(ReceiveMessage);
                Thread myThread = new Thread(ParStart);
                object o = item;
                myThread.Start(o);
            }
        }
        //private void ReadMQ(object MQName){
        //    ReceiveMessage(MQName);
        //  //  Start(MQName);

        //}
        /// <summary>
        /// 接受消息
        /// </summary>
        private void ReceiveMessage(object queueName)
        {
            try
            {
                var qname = Convert.ToString(queueName);

                MQ.IQueueConsumer queueConsumer = MQ.ConsumerFactory.CreateQueueConsumer(qname);
                while (true)
                {
                    try
                    {
                        //OptLog.Log("Info：", "TXPremises", "queueName：" + queueName + ":" + DateTime.Now, true);
                        string result = queueConsumer.ReceiveTextMessage();
                        if (string.IsNullOrEmpty(result))
                        {
                            if (!string.IsNullOrEmpty(queueConsumer.ErrorMessage))
                            {
                                OptLog.Log("Error：", "Error", queueConsumer.ErrorMessage, false);
                            }
                        }
                        else
                        {
                            //操作词典
                            OperDictionary(qname, result);
                            //this.Start(result);
                        }
                    }
                    catch (Exception ex)
                    {
                        OptLog.Log("Error：", "Error", ex.Message, false);
                    }
                }
            }
            catch (Exception ex)
            {
                OptLog.Log("Error：", "Error", ex.Message, false);
            }
        }

        private static object MyLock = new object();
        /// <summary>
        /// 批量接收消息
        /// </summary>
        /// <param name="key">词典key</param>
        /// <param name="value">消息值</param>
        /// <returns></returns>
        private List<OperClass> OperDictionary(string key, string value)
        {
            Monitor.Enter(MyLock);
            List<OperClass> arr = null;
            try
            {
                if (!string.IsNullOrEmpty(value))
                {
                    string[] items = value.Split(',');
                    string type = items[0].Trim();
                    string premisesId = items[1].Trim();
                    string cityid = items[2].Trim();
                    //判断键值是否存在，如果不存在添加新的键值，如果存在判断是否存在相同的索引消息（无相同追加，有相同不做任何操作）。
                    if (dictionary.ContainsKey(key))
                    {
                        var list = dictionary[key];
                        if (!list.Any(it => it.ID == premisesId && it.Type == type && it.CityID == cityid))
                        {
                            var oper = new OperClass() { ID = premisesId, Type = type, CityID = cityid };

                            dictionary[key].Add(oper);
                        }
                    }
                    else
                    {
                        var nlist = new List<OperClass>() { new OperClass() { ID = premisesId, CityID = cityid, Type = type } };

                        dictionary.Add(key, nlist);
                    }

                    OptLog.Log("Info：", "TXPremises", "接收消息:处理类型：" + type + "，楼盘ID：" + premisesId + "　城市ID：" + cityid, false);

                }
                else
                {
                    //读取静态词典，并把指定key的键值移除
                    if (dictionary.ContainsKey(key))
                    {
                        var list = dictionary[key];

                        arr = dictionary[key];

                        dictionary.Remove(key);
                        //dictionary[key].Clear();
                    }
                }

            }
            catch (Exception ex)
            {
                OptLog.Log("Error：", "Error", "批量接受消息错误：" + ex.Message, false);
            }
            finally
            {
                Monitor.Exit(MyLock);
            }
            return arr;
        }
        public void Start(object queueName)
        {
            var qname = Convert.ToString(queueName);

            if (!string.IsNullOrEmpty(qname))
            {
                LuceneOpt Opt = new LuceneOpt();
                try
                {
                    while (true)
                    {
                        var opers = OperDictionary(qname, string.Empty);
                        if (opers != null && opers.Count > 0)
                        {
                            string logInsertMsg = string.Empty;
                            string logDelMsg = string.Empty;
                            string insertId = string.Empty;
                            string delId = string.Empty;

                            foreach (var item in opers)
                            {
                                string mtype = item.Type;
                                if (mtype.Contains("insert") || mtype.Contains("update"))
                                {
                                    logInsertMsg += "接收消息并执行方法AddDocumentForPremises，参数楼盘ID： " + item.ID + "，MQName:" + qname + "，城市ID：" + item.CityID;
                                    insertId += item.ID + ",";
                                    OptLog.Log("Info：", "TXPremises", logInsertMsg, true);
                                }
                                else if (mtype.Contains("delete"))
                                {
                                    logDelMsg += "接收消息并执行方法DeleteDocumentForPremises，参数楼盘ID： " + item.ID + "，MQName:" + qname + "，城市ID：" + item.CityID;
                                    delId += item.ID + ",";
                                    OptLog.Log("Info：", "TXPremises", logDelMsg, true);
                                }
                            }

                            if (!string.IsNullOrEmpty(insertId))
                            {
                                insertId = insertId.TrimEnd(',');
                                Opt.DeleteDocumentForPremises(new string[] { insertId }, qname);
                                Opt.AddDocumentForPremises(insertId, qname);
                            }
                            if (!string.IsNullOrEmpty(delId))
                            {
                                delId = delId.TrimEnd(',');
                                string[] ids = delId.Split(',');
                                Opt.DeleteDocumentForPremises(ids, qname);
                            }
                           
                        }
                        Thread.Sleep(10000);
                    }
                }
                catch (Exception ex)
                {
                    OptLog.Log("Error：", "Error", "Start(string message)：" + ex.Message, false);
                }

            }

        }
        protected override void OnStop()
        {
            // TODO: 在此处添加代码以执行停止服务所需的关闭操作。
        }
    }

    public class OperClass
    {
        public string ID { get; set; }

        public string Type { get; set; }

        public string CityID { get; set; }
    }
}
