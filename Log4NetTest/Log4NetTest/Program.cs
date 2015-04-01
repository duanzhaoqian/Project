using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using log4net;

namespace Log4NetTest
{
    class Program
    {
        static void Main(string[] args)
        {
            FileInfo fileInfo = new FileInfo("log4net.config");
            log4net.Config.XmlConfigurator.Configure(fileInfo);
            log4net.ILog log = LogManager.GetLogger("loginfo");
       
             log.Info(DateTime.Now);
            Console.ReadKey();
        }
    }
}
