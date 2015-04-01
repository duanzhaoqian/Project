using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using log4net;

namespace KYJ.ZS.Log4net
{
    /// <summary>
    /// Author:zhuzh
    /// Date  :2014-05-29
    /// Desc  :日志工厂
    /// </summary>
    public class LogFactory
    {
        static LogFactory()
        {
            //FileInfo configFile = new FileInfo(AppDomain.CurrentDomain.BaseDirectory + @"Log.config");
            FileInfo configFile = new FileInfo(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Log.config"));
            log4net.Config.XmlConfigurator.Configure(configFile);
        }

        public static LogImplement GetLogger(Type type)
        {
            return new LogImplement(LogManager.GetLogger(type));
        }

        public static LogImplement GetLogger(string str)
        {
            return new LogImplement(LogManager.GetLogger(str));
        }
    }
}
