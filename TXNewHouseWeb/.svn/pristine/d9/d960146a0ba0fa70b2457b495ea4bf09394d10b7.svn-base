using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using TXGenerationRedisRankServices.BLL;
using System.Timers;
using System.Threading;

namespace TXGenerationRedisRankServices
{
    public partial class TXGenerationRedisRank : ServiceBase
    {
        private static bool isFirst = true;
        private int StartHour = 1;
        private System.Timers.Timer timerHour = new System.Timers.Timer();
        public TXGenerationRedisRank()
        {
            InitializeComponent();
            //new PremisesBLL().main();
            StartHour = Commons.Tool.ToInt32(Commons.Tool.ConfigKey("StartHour"), 1);
            timerHour.Elapsed += new ElapsedEventHandler(timerHour_Elapsed);
            timerHour.Interval = 1000 * 60 * 60;//每小时执行一次服务
            timerHour.AutoReset = true;
            timerHour.Enabled = true;
        }

        protected override void OnStart(string[] args)
        {
            if (isFirst)
            {
                isFirst = false;
                Thread t = new Thread(MyMain);
                t.Start();
            }
        }

        protected override void OnStop()
        {
        }

        protected void timerHour_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (DateTime.Now.Hour == StartHour)
            {
                Thread t = new Thread(MyMain);
                t.Start();
            }
        }
        private void MyMain()
        {
            new PremisesBLL().main();
        }

    }
}
