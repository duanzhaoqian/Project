using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace XiaoXiDingYue
{
    class Program
    {
        static void Main(string[] args)
        {
            //创建流程对象
            Module.Module module = new Module.Module();
            //启动服务
            module.ServerStart();
            Console.WriteLine("服务启动成功");
            //处理消息
            module.ProcessRequest();
            Console.WriteLine("开始处理消息");
            Console.ReadKey();
        }
    }
}
