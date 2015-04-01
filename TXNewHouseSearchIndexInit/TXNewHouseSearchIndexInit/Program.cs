using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Commons;

namespace TXNewHouseSearchIndexInit
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("开始生成索引");
                MQHelper.SetDictionary();
                var ids = MQHelper.GetLucenexmlIds();
                Commons.LuceneOpt opt = new LuceneOpt();
                foreach (int id in ids)
                {
                    opt.AddDocumentForPremiseses(id);
                    Console.WriteLine("添加索引ID:"+id);
                }
                Console.WriteLine("添加完成......");
            }
            catch(Exception exception)
            {
                
               Console.WriteLine("新房索引生成失败:"+exception.Message);
            }
            Console.ReadKey();
        }
    }
}
