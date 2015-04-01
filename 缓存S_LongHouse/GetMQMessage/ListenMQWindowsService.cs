using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Web.Script.Serialization;
using ListenMQ;
using Module;

namespace GetMQMessage
{
    public partial class ListenMQWindowsService : ServiceBase
    {
        public ListenMQWindowsService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
          Process process=new Process();
            process.ProcessData();
        }

        protected override void OnStop()
        {
        }
    }
}
