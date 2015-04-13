using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace ServiceStack.Log
{
    [Obsolete("不再使用，以FileLogger代替",true)]
    internal class FileLog : LogBase, ILog
    {
        internal string FilePath { get; set; }
        public FileLog(string name)
            : base(name)
        {
            string configName = LogConfig.GetElement(LogConfig.XDocument.Element("Log"), "name", name).Attribute("config").Value;
            XElement xElement = LogConfig.GetElement(LogConfig.XDocument.Element("Log"), "name", configName);
            FilePath =
                xElement.Element("FilePath").Value;
        }

        public void Log(string content, string name)
        {
            string fileName = Path.Combine(FilePath,"INFO",string.Format("{0}-{1}-{2}", DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day), string.Format("{0}-{1}-{2} {3}.txt", DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour));
            if (!File.Exists(fileName))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(fileName));
                // File.Create(fileName);
            }
            File.AppendAllText(fileName, string.Format("===={3}Content：{0} {3}Name：{1} {3}Date：{2}{3}===={3}", content, name, DateTime.Now, Environment.NewLine));
        }

        public void LogError(string content, string name, Exception exception)
        {
            string fileName = Path.Combine(FilePath, "Exception", string.Format("{0}-{1}-{2}", DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day), string.Format("{0}-{1}-{2} {3}Exception.txt", DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour));
            if (!File.Exists(fileName))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(fileName));
                // File.Create(fileName);
            }
            File.AppendAllText(fileName, string.Format("===={3}Content：{0} {3}Name：{1} {3}Date：{2}{3}Exception：Message：{4}{3}StackTrace：{5}{3}===={3}", content, name, DateTime.Now, Environment.NewLine, exception.Message, exception.StackTrace));
        }
    }
}
