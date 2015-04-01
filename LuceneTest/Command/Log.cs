using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Command
{
    public class Log
    {
        public static void WriteLog(string content)
        {
            string path = @"D:\log\LuceneTest";
            string fileName = DateTime.Now.ToString("yyyy-MM-dd-hh-mm")+".txt";
            string fullPath = Path.Combine(path, fileName);
            File.WriteAllText(fullPath,content);
        }
    }
}
