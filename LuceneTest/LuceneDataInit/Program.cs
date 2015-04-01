using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LuceneOpt;

namespace LuceneDataInit
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("开始生成。。。");
            LuceneInitData luceneInit = new LuceneInitData();
            int num = luceneInit.InitData();
            Console.WriteLine("生成了{0}条数据", num);
            Console.ReadKey();
        }
    }
}
