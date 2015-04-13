using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace ServiceStack.Log
{
    public class FileLogger : ILog
    {
        string FilePath { get; set; }
        public FileLogger()
        {
            FilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Log_ServiceStack");
        }

        public void Log(string content, string name)
        {
            string fileName = Path.Combine(FilePath, "INFO", name, string.Format("{0}-{1}-{2}", DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day), string.Format("{0}-{1}-{2} {3}.txt", DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour));
            if (!File.Exists(fileName))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(fileName));
                // File.Create(fileName);
            }
            File.AppendAllText(fileName, string.Format("===={3}Content：{0} {3}Name：{1} {3}Date：{2}{3}===={3}", content, name, DateTime.Now, Environment.NewLine),new UTF8Encoding(false));
        }

        public void LogError(string content, string name, Exception exception)
        {
            string fileName = Path.Combine(FilePath, "Exception", name, string.Format("{0}-{1}-{2}", DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day), string.Format("{0}-{1}-{2} {3}_{4}Exception.txt", DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour,Process.GetCurrentProcess().Id));
            if (!File.Exists(fileName))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(fileName));
                // File.Create(fileName);
            }
            File.AppendAllText(fileName, string.Format("===={3}Content：{0} {3}Name：{1} {3}Date：{2}{3}Exception：Message：{4}{3}StackTrace：{5}{3}===={3}", content, name, DateTime.Now, Environment.NewLine, exception.Message, exception.StackTrace),new UTF8Encoding(false));
        }
    }
}
