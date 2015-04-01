using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;

namespace ServerTest
{
    class Program
    {
        static void Main(string[] args)
        {
            ServerTest.TestService server=new ServerTest.TestService();
            server.ServiceName = "serverTest";
            ServiceBase.Run(server);
        }
    }
}
