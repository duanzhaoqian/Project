using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConsoleApplication2.ServiceReference1;

namespace ConsoleApplication2
{
    class Program
    {
        static void Main(string[] args)
        {
            ServiceReference1.ServiceDemo1ImplClient serviceDemo1ImplClient=new ServiceDemo1ImplClient();
            string str=serviceDemo1ImplClient.Test1("aaa");
            Console.ReadKey();
        }
    }
}
