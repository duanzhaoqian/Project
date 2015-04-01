using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using Module;

namespace SyncData
{
    public partial class SyncDataWindowsService : ServiceBase
    {
        public SyncDataWindowsService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            Process process = new Process();
            process.ProcessData();
        }

        protected override void OnStop()
        {
        }
    }
}
