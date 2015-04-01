using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Commons;
using System.Threading;
using Commons.Sources;
using System.Diagnostics;

namespace TXNewConsole
{
    class Program
    {
        protected static string MQIPAddress = string.Empty;
        protected static string MQQueueName = string.Empty;
        protected static string MQConnectionTimeOut = string.Empty;
        protected static string MQRetryCount = string.Empty;
        protected static string LogPath = string.Empty;
        protected static int ThreadCount = 1;
        /*
         #region Timing

         static void Main(string[] args)
         {
             Console.WriteLine("请选择需要执行的类型：");
             Console.Write("1 每分钟的定时服务  2 每小时的定时服务 3 每天的定时服务 4 保证金退还业务 ：");
             var type = Console.ReadLine();
             Console.WriteLine("----------------------------------------------------------------------");
             try
             {
                 var _opt = new TimingOpt();

                 Stopwatch sw = new Stopwatch();

                 int beginnum = 0;
                 int endnum = 0;
                 int num = 0;
                 while (true)
                 {
                     Console.WriteLine("正在执行... ...");
                     Console.WriteLine("----------------------------------------------------------------------");
                     switch (type)
                     {
                         case "1":
                             sw.Restart();
                             //执行定时服务
                             num = _opt.TimingSeckill();
                             sw.Stop();
                             Console.WriteLine("执行总行数：" + num + "　总耗时：" + sw.ElapsedMilliseconds);
                             break;
                         case "2":
                             sw.Restart();
                             //执行定时服务
                             beginnum = _opt.ActivitiesBeginTimingOperation(7);
                             endnum = _opt.ActivitiesEndTimingOperation(7);
                             sw.Stop();
                             Console.WriteLine("执行活动结束时间总行数：" + endnum + "，执行活动开始时间总行数：" + beginnum + "，总耗时：" + sw.ElapsedMilliseconds);
                             break;
                         case "3":
                             sw.Restart();
                             int[] types = { 5, 6, 8 };
                             //执行定时服务
                             foreach (var item in types)
                             {
                                 beginnum = beginnum + _opt.ActivitiesBeginTimingOperation(item);
                                 endnum = endnum + _opt.ActivitiesEndTimingOperation(item);
                             }
                             sw.Stop();
                             Console.WriteLine("执行活动结束时间总行数：" + endnum + "，执行活动开始时间总行数：" + beginnum + "，总耗时：" + sw.ElapsedMilliseconds);
                             break;
                         case "4":
                             //计时
                             sw.Restart();
                             //执行定时服务
                             num = _opt.AmountTimingOperation();
                             sw.Stop();
                             Console.WriteLine("执行总行数：" + num + "　总耗时：" + sw.ElapsedMilliseconds);
                             break;
                         default:
                             break;
                     }

                     type = Console.ReadLine();
                 }
             }
             catch (Exception ex)
             {
                 Console.WriteLine("执行异常：" + ex.Message);
             }
         }

         #endregion
        */
        #region Lucene

        static void Main(string[] args)
        {
            LogPath = Profile.IniReadValue("TXPremisesService", "LogPath"); //ConfigurationManager.AppSettings["LogPath"].ToString();
            ThreadCount = int.Parse(Profile.IniReadValue("TXPremisesService", "ThreadCount"));


            MQ.ConsumerConfiguration.IPAddress = Profile.IniReadValue("TXPremisesService", "MQIPAddress"); //ConfigurationManager.AppSettings["MQIPAddress"].ToString();
            MQ.ConsumerConfiguration.ConnectionTimeOut = new TimeSpan(0, 0, Convert.ToInt32(Profile.IniReadValue("TXPremisesService", "MQConnectionTimeOut")));
            MQ.ConsumerConfiguration.RetryCount = Convert.ToInt32(Profile.IniReadValue("TXPremisesService", "MQRetryCount"));

            //设置lucenexml内容
            MQHelper.SetDictionary();
            Console.WriteLine("请选择以下内容：");
            Console.WriteLine("              1:生成楼盘索引  2:生成排行榜索引");

            try
            {
                var num = Console.ReadLine();

                CreateThread(num);
            }
            catch (Exception ex)
            {

                Console.WriteLine("生成出错:" + ex);
            }

            

            Console.ReadLine();
        }

        /// <summary>
        /// 创建运行线程
        /// </summary>
        private static void CreateThread(string step)
        {
            Console.WriteLine("服务启动：" + DateTime.Now.ToString());
            var Ids = MQHelper.GetLucenexmlIds();//MQQueueName.Split(',');
            foreach (var item in Ids)
            {
                //if (item == 1)
                //{
                var optThread = new OptThread() { Id = item, step = step };
                //ReceiveMessage(optThread);
                ParameterizedThreadStart ps = new ParameterizedThreadStart(ReceiveMessage);
                Thread thread = new Thread(ps);
                thread.Start(optThread);
                //}
            }

        }

        /// <summary>
        /// 接收生成索引委托
        /// </summary>
        private static void ReceiveMessage(object org)
        {
            try
            {
                LuceneOpt Opt = new LuceneOpt();
                var optThread = (OptThread)org;
                switch (optThread.step)
                {
                    case "1":
                        var p_num = Opt.AddDocumentForPremiseses(optThread.Id);
                        Console.WriteLine("索引分组：" + optThread.Id + " 执行总行数：" + p_num);
                        break;
                    case "2":
                        var r_num = Opt.AddDocumentForRankings(optThread.Id);
                        Console.WriteLine("排行分组：" + optThread.Id + " 执行总行数：" + r_num);
                        break;
                    default:
                        break;
                }

            }
            catch (Exception ex)
            {
                OptLog.Log("Error：", "Error", ex.Message, false);
            }
        }

        #endregion


        
         #region Room
        /*
        static void Main(string[] args)
        {
            LogPath = Profile.IniReadValue("TXPremisesService", "LogPath"); //ConfigurationManager.AppSettings["LogPath"].ToString();
            ThreadCount = int.Parse(Profile.IniReadValue("TXPremisesService", "ThreadCount"));


            MQ.ConsumerConfiguration.IPAddress = Profile.IniReadValue("TXPremisesService", "MQIPAddress"); //ConfigurationManager.AppSettings["MQIPAddress"].ToString();
            MQ.ConsumerConfiguration.ConnectionTimeOut = new TimeSpan(0, 0, Convert.ToInt32(Profile.IniReadValue("TXPremisesService", "MQConnectionTimeOut")));
            MQ.ConsumerConfiguration.RetryCount = Convert.ToInt32(Profile.IniReadValue("TXPremisesService", "MQRetryCount"));

            //设置lucenexml内容
            MQHelper.SetDictionary();
            Console.WriteLine("请选择以下内容：");
            Console.WriteLine("              1:生成户型图索引");
            MQHelper.SetDictionary();
            var num = Console.ReadLine();

            CreateThread(num);

            Console.ReadLine();
        }
       
        /// <summary>
        /// 创建运行线程
        /// </summary>
        private static void CreateThread(string step)
        {
            Console.WriteLine("服务启动：" + DateTime.Now.ToString());
            var Ids = MQHelper.GetLucenexmlIds();//MQQueueName.Split(',');
            foreach (var item in Ids)
            {
                    var optThread = new OptThread() {Id = item, step = step};

                    ParameterizedThreadStart ps = new ParameterizedThreadStart(ReceiveMessage);
                    Thread thread = new Thread(ps);
                    thread.Start(optThread);
                
            }

        }

        /// <summary>
        /// 接收生成索引委托
        /// </summary>
        private static void ReceiveMessage(object org)
        {
            try
            {
                LuceneOpt Opt = new LuceneOpt();
                var optThread = (OptThread)org;
                switch (optThread.step)
                {
                    case "1":
                        var p_num = Opt.AddDocumentForRooms(optThread.Id);
                        Console.WriteLine("索引分组：" + optThread.Id + " 执行总行数：" + p_num);
                        break;
                    default:
                        break;
                }

            }
            catch (Exception ex)
            {
                OptLog.Log("Error：", "Error", ex.Message, false);
            }
        }
        */
         #endregion



    }

    public class OptThread
    {
        public int Id { get; set; }

        public string step { get; set; }
    }
}
