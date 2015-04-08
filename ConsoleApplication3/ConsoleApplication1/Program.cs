using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                string[] strings = File.ReadAllLines("picpath.txt").Where(c => !string.IsNullOrEmpty(c)).ToArray();
                List<FileInfo> list = new List<FileInfo>();
                List<string> liststring = strings.Where(c=>!string.IsNullOrEmpty(c)).Select(c => c.Replace(@"http://img.kuaiyoujia.com/", "")).ToList();
                foreach (string s in strings)
                {
                    WebClient webClient=new WebClient();
                    byte[] bytes=webClient.DownloadData(s);
                    //FileStream fileStream = new FileStream(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "DownLoad", Path.GetFileName(s)),FileMode.OpenOrCreate,FileAccess.Write);
                    //fileStream.Write(bytes,0,bytes.Length);
                    File.WriteAllBytes(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "DownLoad", Path.GetFileName(s)), bytes);
                }
                //foreach (string s in liststring)
                //{
                //    list.Add(new FileInfo(Path.Combine("Model", Path.GetFileName(s))));
                //    Console.WriteLine(s);
                //}
                //int i = 0;
                //foreach (FileInfo info in list)
                //{
                //    info.CopyTo(Path.Combine(@"F:\img", liststring.Where(c => c.Contains(info.Name)).First()), true);
                //    Console.WriteLine(info.FullName);
                //    Console.WriteLine(i++);
                //    Console.ReadKey();
                //}
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message + exception.StackTrace);
            }
            Console.WriteLine("OK");
            Console.ReadKey();
        }
    }
}
