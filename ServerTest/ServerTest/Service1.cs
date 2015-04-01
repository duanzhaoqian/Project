using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;

namespace ServerTest
{
    partial class TestService : ServiceBase
    {
        public TestService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {

            // TODO: 在此处添加代码以启动服务。
            Thread thread = new Thread(ServiceTestLog);
            thread.IsBackground = true;
            thread.Start();
        }

        protected override void OnStop()
        {
            // TODO: 在此处添加代码以执行停止服务所需的关闭操作。
        }

        private void ServiceTestLog()
        {
            while (true)
            {
                using (StreamWriter streamWriter = new StreamWriter(@"D:\ServiceTestLog.txt", true, Encoding.Default, 1024 * 2))
                {
                    streamWriter.WriteLine(DateTime.Now);
                    Thread.Sleep(5000);
                }
            }
        }
    }
}
