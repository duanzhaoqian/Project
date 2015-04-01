using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.IO;

namespace Commons
{
    public class OptLog
    {

        private static string LogPath = ConfigurationManager.AppSettings["LogPath"].ToString();

        /// <summary>
        /// 生成日志
        /// </summary>
        /// <param name="flag"></param>
        /// <param name="msg"></param>
        public static void Log(string flag, string type, string msg, bool isln)
        {
            try
            {
                if (!Directory.Exists(LogPath))
                {
                    Directory.CreateDirectory(LogPath);
                }
                var strPath = LogPath + (type + DateTime.Now.ToString("yyyyMMdd") + ".txt");
                FileStream fs = new FileStream(strPath, FileMode.OpenOrCreate, FileAccess.Write);
                StreamWriter m_streamWriter = new StreamWriter(fs);
                m_streamWriter.BaseStream.Seek(0, SeekOrigin.End);
                m_streamWriter.WriteLine(flag + msg + "：" + DateTime.Now + "\n");
                if (isln)
                    m_streamWriter.WriteLine("--------------------------------------------------------------------------------------------\n");
                m_streamWriter.Flush();
                m_streamWriter.Close();
                fs.Close();
            }
            catch { }

        }
    }
}
