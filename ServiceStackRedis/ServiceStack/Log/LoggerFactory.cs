using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Net.Mime;
using System.Reflection;
using System.Text;

namespace ServiceStack.Log
{
    public static class LoggerFactory
    {
        private static Dictionary<string,ILog> dic=new Dictionary<string, ILog>(); 
        [Obsolete("不再使用",true)]
        public static ILog GetLog(string name)
        {
            if (!dic.ContainsKey(name))
            {

                ILog log = Assembly.GetExecutingAssembly().CreateInstance(LogConfig.XDocument.Element("Log").Elements("Logger").First(c => c.Attribute("name").Value == name).Attribute("type").Value, true, BindingFlags.Default, null, new object[] { name }, null, null) as ILog;
                dic.Add(name,log);
            }
            return dic[name];
        }

        public static List<ILog> GetAllLog()
        {
            return dic.Select(c=>c.Value).ToList();
        }
      /// <summary>
      /// 以文本形式记录的Logger
      /// </summary>
      /// <returns></returns>
        public static ILog GetFileLogger()
        {
            if (!dic.ContainsKey("FileLogger"))
            {
               // dic["FileLogger"]=new FileLogger();
                dic.Add("FileLogger", new FileLogger());
            }
            return dic["FileLogger"];
        }
        /// <summary>
        /// 发送异常信息邮件的Logger，仅实现了Info方法，未实现Error方法
        /// </summary>
        /// <param name="mailSubject"></param>
        /// <param name="mailTo"></param>
        /// <param name="mailChaoSong"></param>
        /// <returns></returns>
        public static ILog GetMailLogger(string mailSubject, string mailTo, string mailChaoSong)
        {
            if (!dic.ContainsKey(string.Join("", mailSubject, mailTo, mailChaoSong)))
            {
                dic.Add(string.Join("", mailSubject, mailTo, mailChaoSong), new MailLogger(mailSubject, mailTo, mailChaoSong));
                //dic[string.Join("", mailSubject, mailTo, mailChaoSong)] = new MailLogger(mailSubject, mailTo, mailChaoSong);
            }
            return dic[string.Join("", mailSubject, mailTo, mailChaoSong)];
        }
    }
}
