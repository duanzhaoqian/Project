using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using DrawingPictureHelp;
using SQLData;

namespace DrawTest
{
    class Program
    {
        static void Main(string[] args)
        {
            string dirPath = ConfigurationManager.AppSettings["DirPath"];
            DrawPic drawPic = new DrawPic();
            drawPic.DirPath = dirPath;

            SQLData.SQLRead sqlRead = new SQLRead();
            Stopwatch stopwatch1 = new Stopwatch();
            stopwatch1.Start();
            List<string> listGuids = sqlRead.GetList();
            stopwatch1.Stop();
            Console.WriteLine(string.Format("获取{0}条数据", listGuids.Count));
            Console.WriteLine(string.Format("消耗时间：{0}", stopwatch1.Elapsed));
            Console.WriteLine("开始画图");
            Stopwatch stopwatch2 = new Stopwatch();
            stopwatch2.Start();
            for (int i = 0; i < 2000000; i++)
            {
                if (listGuids.Count - i - 1 >= 0)
                {
                    drawPic.BeginDrawing(listGuids[listGuids.Count - i - 1]);
                }
            }
            stopwatch2.Stop();
            Console.WriteLine("画图结束");
            Console.WriteLine(string.Format("消耗时间：{0}", stopwatch2.Elapsed));

            //Stopwatch stopwatch2 = new Stopwatch();
            //stopwatch2.Start();
            //for (int i = 0; i < 500000; i++)
            //{
            //    drawPic.BeginDrawing(Guid.NewGuid().ToString());
            //}
            //stopwatch2.Stop();
            //Console.WriteLine("画图结束");
            //Console.WriteLine(string.Format("消耗时间：{0}", stopwatch2.Elapsed));
            Console.ReadKey();
        }
    }
}
