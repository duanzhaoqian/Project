using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using log4net;
using log4net.Appender;

namespace Log4NetTest
{
    class Program
    {
        static void Main(string[] args)
        {
            FileInfo fileInfo = new FileInfo("log4net.config");
            log4net.Config.XmlConfigurator.Configure(fileInfo);
            log4net.ILog log = LogManager.GetLogger(typeof(Program));
       
             log.Info(DateTime.Now);
            int n = 68 & 255;
            UdpClient udpClient=new UdpClient();
            IPEndPoint ipEndPoint = new IPEndPoint(IPAddress.Parse("192.168.25.1"), 8889);
            byte[] bytes = Encoding.UTF8.GetBytes("test");
            udpClient.Send(bytes, bytes.Length,ipEndPoint);
            Console.ReadKey();
        }
    }
}
