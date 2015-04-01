using System;
using System.Configuration;
using System.IO;

namespace KYJ.ZS.Commons.PictureModular
{
    public class PicLog
    {
        public static string LogPath = ConfigurationManager.AppSettings["LogPath"];

        /// <summary>
        /// 生成日志
        /// </summary>
        /// <param name="flag"></param>
        /// <param name="type"></param>
        /// <param name="msg"></param>
        public static void Log(string flag, string type, string msg)
        {
            try
            {
                var strPath = LogPath + (type + DateTime.Now.ToString("yyyyMMdd") + ".txt");
                FileStream fs = new FileStream(strPath, FileMode.OpenOrCreate, FileAccess.Write);
                StreamWriter mStreamWriter = new StreamWriter(fs);
                mStreamWriter.BaseStream.Seek(0, SeekOrigin.End);
                mStreamWriter.WriteLine(flag + msg + "\n");
                mStreamWriter.Flush();
                mStreamWriter.Close();
                fs.Close();
            }
            catch { }

        }
    }
}
