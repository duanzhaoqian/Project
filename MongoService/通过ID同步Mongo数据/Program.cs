using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using MongoProcess;

namespace 通过ID同步Mongo数据
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                string[] idStrings = File.ReadAllLines("IDS.txt");
                MongoProcess.MongoDBProcess mongoDbProcess = new MongoDBProcess();
                int i = 1;
                foreach (string s in idStrings)
                {
                    mongoDbProcess.Upsert(s);
                    Console.WriteLine("已处理ID：{0}，{1}/{2}", s, i++, idStrings.Length);
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message+Environment.NewLine+exception.StackTrace);
                Console.ReadLine();
            }
            Console.WriteLine("处理完成");
            Console.ReadKey();
        }
    }
}
