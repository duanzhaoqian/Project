using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using Commons;
using System.Timers;
using System.Threading;

namespace TXNewTimingServices
{
    partial class TXAmountService : ServiceBase
    {
        protected static string LogPath = string.Empty;

        protected static int ThreadCount = 1;

        private System.Timers.Timer timer = new System.Timers.Timer();

        private static bool IsCreateThread = true;

        public TXAmountService()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 每天凌晨1点执行一次金额退还操作
        /// </summary>
        /// <param name="args"></param>
        protected override void OnStart(string[] args)
        {
            // TODO: 在此处添加代码以启动服务。
            OptLog.Log("Info：", "TXAmount", "服务启动", true);

            //Thread.Sleep(2000);

            timer.Elapsed += new ElapsedEventHandler(CreateThread);
            //一小时执行一次，每天凌晨1点执行一次金额退还操作
            timer.Interval = 1000 * 60 * 1;
            timer.AutoReset = true;
            timer.Enabled = true;
        }
        private static object lockobject = new object();
        /// <summary>
        /// 创建运行线程
        /// </summary>
        private void CreateThread(object sender, ElapsedEventArgs e)
        {
            try
            {
                if (IsCreateThread)
                {
                    //每天凌晨0点1分执行
                    if (DateTime.Now.Hour == 0 && DateTime.Now.Minute == 1)
                    {
                        //添加是否创建线程锁
                        IsCreateThread = false;
                        for (int i = 0; i < ThreadCount; i++)
                        {
                            Thread thread = new Thread(new ThreadStart(ReceiveMessage));
                            thread.Start();
                        }
                    }
                }
                else
                {
                    OptLog.Log("Info：", "TXAmount", "CreateThread():数据太多，线程暂用中。", true);
                }
            }
            catch (Exception ex)
            {
                //解除创建线程锁
                IsCreateThread = true;
                OptLog.Log("Error：", "Error", "CreateThread():" + ex.Message, true);
            }
        }

        /// <summary>
        /// 执行操作
        /// </summary>
        static void ReceiveMessage()
        {
            //线程加锁
            Monitor.Enter(lockobject);
            try
            {
                //计时
                var _opt = new TimingOpt();
                Stopwatch sw = new Stopwatch();
                sw.Start();
                //执行定时服务
                var num = _opt.AmountTimingOperation();
                sw.Stop();
                OptLog.Log("Info：", "TXAmount", "执行总行数：" + num + "　总耗时：" + sw.ElapsedMilliseconds, true);
            }
            catch (Exception ex)
            {
                OptLog.Log("Error：", "Error", "定时服务异常：" + ex.Message, true);
            }
            finally
            {
                //解除创建线程锁
                IsCreateThread = true;
                //解除线程锁
                Monitor.Exit(lockobject);
            }

        }

        protected override void OnStop()
        {
            // TODO: 在此处添加代码以执行停止服务所需的关闭操作。
        }
    }
}
