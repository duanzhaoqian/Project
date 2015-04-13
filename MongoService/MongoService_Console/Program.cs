using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Web;
using System.Text;
using MongoImpl;

namespace MongoService_Console
{
    class Program
    {
        static void Main(string[] args)
        {
            using (WebServiceHost host = new WebServiceHost(typeof(MongoHouseService)))
            {
                host.Open();
                Console.Read();
            }
        }
    }
}
