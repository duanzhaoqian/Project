﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Timers;
using Commons;
using System.Threading;

namespace TXNewTimingServices
{
    partial class TXActivitiesDayService : ServiceBase
    {

        protected static string LogPath = string.Empty;

        protected static int ThreadCount = 1;

        protected static int[] ActivityTypes = { 5, 6, 8 };

        private System.Timers.Timer timer = new System.Timers.Timer();

        private static bool IsCreateThread = true;

        public TXActivitiesDayService()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 每天执行一次服务（活动结束）
        /// </summary>
        /// <param name="args"></param>
        protected override void OnStart(string[] args)
        {
            //Thread.Sleep(10000);

            // TODO: 在此处添加代码以启动服务。
            OptLog.Log("Info：", "TXActivitiesDay", "服务启动", true);

            timer.Elapsed += new ElapsedEventHandler(CreateThread);
            //一分钟执行一次
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
                    //每天凌晨1分执行
                    if (DateTime.Now.Hour == 0 && DateTime.Now.Minute == 1)
                    {
                        //添加是否创建线程锁
                        IsCreateThread = false;
                        foreach (var item in ActivityTypes)
                        {
                            ParameterizedThreadStart ps = new ParameterizedThreadStart(ReceiveMessage);
                            Thread thread = new Thread(ps);
                            thread.Start(item);
                        }
                    }
                }
                else
                {
                    OptLog.Log("Info：", "TXActivitiesDay", "CreateThread():数据太多，线程暂用中。", true);
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
        static void ReceiveMessage(object type)
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
                var beginnum = _opt.ActivitiesBeginTimingOperation(Convert.ToInt32(type));
                var endnum = _opt.ActivitiesEndTimingOperation(Convert.ToInt32(type));
                sw.Stop();
                OptLog.Log("Info：", "TXActivitiesDay", "执行活动结束时间总行数：" + endnum + "，执行活动开始时间总行数：" + beginnum + "，总耗时：" + sw.ElapsedMilliseconds, true);
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