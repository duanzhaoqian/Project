using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using Commons;
using System.Threading;
using System.Timers;

namespace TXRankServices
{
    partial class TXRankService : ServiceBase
    {
        protected static string LogPath = string.Empty;

        private System.Timers.Timer timer = new System.Timers.Timer();

        public TXRankService()
        {
            InitializeComponent();
        }
        protected override void OnStart(string[] args)
        {
            //设置lucenexml内容
            MQHelper.SetDictionary();

            OptLog.Log("Info：", "TXRanking", "服务启动", true);

            timer.Elapsed += new ElapsedEventHandler(CreateThread);
            //一小时执行一次，每天凌晨1点、12点执行一次
            timer.Interval = 1000 * 60 * 60;
            timer.AutoReset = true;
            timer.Enabled = true;
        }

        /// <summary>
        /// 创建运行线程
        /// </summary>
        private void CreateThread(object sender, ElapsedEventArgs e)
        {
            if (DateTime.Now.Hour == 12 || DateTime.Now.Hour == 1)
            {
                var mqthread = MQHelper.GetLucenexmlIds();
                foreach (var item in mqthread)
                {
                    ParameterizedThreadStart ps = new ParameterizedThreadStart(ReceiveMessage);
                    Thread thread = new Thread(ps);
                    thread.Start(item);
                }
            }

        }
        /// <summary>
        /// 生成楼盘排行索引
        /// </summary>
        /// <param name="Id">分组ID</param>
        private void ReceiveMessage(object Id)
        {
            try
            {
                LuceneOpt Opt = new LuceneOpt();
                var num = Opt.AddDocumentForRanking(Convert.ToInt32(Id));
                OptLog.Log("Info：", "TXRanking", "执行分组：" + Id + " 总行数：" + num, true);
            }
            catch (Exception ex)
            {
                OptLog.Log("Error：", "Error", ex.Message, false);
            }
        }

        protected override void OnStop()
        {
            // TODO: 在此处添加代码以执行停止服务所需的关闭操作。
        }
    }
}
