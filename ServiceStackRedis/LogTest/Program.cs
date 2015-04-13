using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ServiceStack;

namespace LogTest
{
    class Program
    {
        static void Main(string[] args)
        {
            //ILog log = LoggerFactory.GetLog("fileAndMail");
            //log.Log("Hello world","duanzq");
            //log.LogError("Hello world", "duanzq",new Exception("aaaa"));
          //Person p=new Person();
          //  p.Name = "aaa";
          //  p.Age = 11;
           // bool a = p.Say(p);
            int i;
            Person p1 = new Person();
            p1.Name = "aaa";
            p1.Age = 11;
            bool a1 = p1.Say(p1,0,out i);
             Console.ReadKey();
        }
    }
}
