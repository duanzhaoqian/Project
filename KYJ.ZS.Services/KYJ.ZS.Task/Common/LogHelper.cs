using System;

using log4net;

namespace KYJ.ZS.Task
{
    using System.IO;

    using log4net.Config;

    /// <summary>
    /// 作者：wangyu
    /// 时间：2014/5/27 10:20:21
    /// 描述：
    /// </summary>
    public class LogHelper
    {
        public static void WriteLog(string text)
        {
            WriteLog(AppDomain.CurrentDomain.BaseDirectory + string.Format("/{0:yyyy-MM-dd}.txt", DateTime.Now), "{0}：{1}{2}", DateTime.Now, text, Environment.NewLine);
        }
        public static void WriteLog(string logFile, string format, params object[] arg)
        {
            using (StreamWriter writer = new StreamWriter(logFile, true))
            {
                writer.Write(format, arg);
                writer.Close();
                //writer.Dispose();
            }
        }

        #region log4net
        /*
         * 应用程序启动时初始化log4net配置
         * LogHelper.Config()
         * 
         * 在程序中记录日志
         * LogHelper.Info("消息");
         * 或
         * LogHelper.Logger.Info("消息");
         * 
         * Debug|Error|Info，配置文件中不同的事件等级只记录该级别以上的日志，其它忽略，全部记录的级别为：ALL
         */
        public static void Config()
        {
            XmlConfigurator.Configure();
        }
        public static void Config(string customConfigName)
        {
            if (customConfigName.Length > 0)
            {
                XmlConfigurator.Configure(new FileInfo(customConfigName));
            }
            else
            {
                XmlConfigurator.Configure();
            }
        }

        public static void Debug(string msg)
        {
            LogManager.GetLogger("DebugLoger").Debug(msg);
        }
        public static void Debug(string msg, Exception ex)
        {
            LogManager.GetLogger("DebugLoger").Debug(msg, ex);
        }

        public static void Error(string msg)
        {
            LogManager.GetLogger("ErrorLoger").Error(msg);
        }
        public static void Error(string msg, Exception ex)
        {
            LogManager.GetLogger("ErrorLoger").Error(msg, ex);
        }

        public static void Info(string msg)
        {
            LogManager.GetLogger("InfoLogger").Info(msg);
        }
        public static void Info(string msg, Exception ex)
        {
            LogManager.GetLogger("InfoLogger").Info(msg, ex);
        }

        public static void Warn(string msg)
        {
            LogManager.GetLogger("WarnLogger").Warn(msg);
        }
        public static void Warn(string msg, Exception ex)
        {
            LogManager.GetLogger("WarnLogger").Warn(msg, ex);
        }
        
        #endregion
    }
}
