using System;
using System.IO;
using System.Configuration;

namespace TXCommons.PictureModular
{
    public class PicLog
    {

        public static string LogPath = ConfigurationManager.AppSettings["LogPath"].ToString();

        /// <summary>
        /// 生成日志
        /// </summary>
        /// <param name="flag"></param>
        /// <param name="msg"></param>
        public static void Log(string LogPath, string msg)
        {
            try
            {

                var strPath = LogPath + ( DateTime.Now.ToString("yyyyMMdd") + ".txt");
                FileStream fs = new FileStream(strPath, FileMode.OpenOrCreate, FileAccess.Write);
                StreamWriter m_streamWriter = new StreamWriter(fs);
                m_streamWriter.BaseStream.Seek(0, SeekOrigin.End);
                m_streamWriter.WriteLine( msg + "\n");
                m_streamWriter.Flush();
                m_streamWriter.Close();
                fs.Close();
            }
            catch { }

        }
    }
}
