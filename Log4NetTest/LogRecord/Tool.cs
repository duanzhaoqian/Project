using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.IO;
using System.Net;

namespace LogRecord
{
    public static class Tool
    {
        private static string _logPath;

        public static string LogPath
        {
            get
            {
                if (string.IsNullOrEmpty(_logPath))
                {
                    LogPath = ConfigurationManager.AppSettings["LogPath"];
                }
                return _logPath;
            }
            set { _logPath = value; }
        }

        /// <summary>
        /// 生成日志
        /// </summary>
        /// <param name="flag"></param>
        /// <param name="msg"></param>
        public static void Log(string flag, string msg)
        {
            try
            {
                //string strPath = LogPath + DateTime.Now.Year + "-" + DateTime.Now.Month + "-" + DateTime.Now.Day + "-" + DateTime.Now.Hour + ".txt";
                string dirPath = Path.Combine(LogPath,
   DateTime.Now.Year + "-" + DateTime.Now.Month + "-" + DateTime.Now.Day);
                if (!Directory.Exists(dirPath))
                {
                    Directory.CreateDirectory(dirPath);
                }
                string strPath = Path.Combine(dirPath, DateTime.Now.Hour + ".txt");

                FileStream fs = new FileStream(strPath, FileMode.OpenOrCreate, FileAccess.Write);
                StreamWriter m_streamWriter = new StreamWriter(fs);
                m_streamWriter.BaseStream.Seek(0, SeekOrigin.End);
                m_streamWriter.WriteLine(flag + ":  " + msg + " :" + DateTime.Now.ToString() + "\n");
                m_streamWriter.Flush();
                m_streamWriter.Close();
                fs.Close();
            }
            catch { }
        }
        /// <summary>
        /// 异常日志
        /// </summary>
        /// <param name="flag"></param>
        /// <param name="msg"></param>
        /// <param name="Logpath"></param>
        public static void LogException(string flag, string msg)
        {
            try
            {
                //string strPath = LogPath + DateTime.Now.Year + "-" + DateTime.Now.Month + "-" + DateTime.Now.Day + "-" + DateTime.Now.Hour + "Exception" + ".txt";
                string dirPath = Path.Combine(LogPath,
    DateTime.Now.Year + "-" + DateTime.Now.Month + "-" + DateTime.Now.Day);
                if (!Directory.Exists(dirPath))
                {
                    Directory.CreateDirectory(dirPath);
                }
                string strPath = Path.Combine(dirPath, DateTime.Now.Hour + "Exception" + ".txt");
                FileStream fs = new FileStream(strPath, FileMode.OpenOrCreate, FileAccess.Write);
                StreamWriter m_streamWriter = new StreamWriter(fs);
                m_streamWriter.BaseStream.Seek(0, SeekOrigin.End);
                m_streamWriter.WriteLine(flag + ":  " + msg + " :" + DateTime.Now.ToString() + "\n");
                m_streamWriter.Flush();
                m_streamWriter.Close();
                fs.Close();
            }
            catch { }
        }
        /// <summary>
        /// 获取一个文件里文本内容
        /// </summary>
        /// <param name="FileFullPath">文件完整路径</param>
        /// <returns></returns>
        public static string GetFileContent(string FileFullPath, Encoding en)
        {
            string Content = "";
            Encoding enCode = Encoding.UTF8;
            if (en != null)
            {
                enCode = en;
            }
            if (System.IO.File.Exists(FileFullPath))
            {
                StreamReader sr = new StreamReader(FileFullPath, enCode);
                Content = sr.ReadToEnd();
                sr.Close();
            }
            return Content;
        }
        #region 获取指定页面的HTML代码

        /// <summary>
        /// 获取指定页面的HTML代码
        /// </summary>
        /// <param name="url">指定页面的路径</param>
        /// <param name="cookieCollection">Cookie集合</param>
        /// <returns></returns>
        public static string GetHTML(string postData, string url)
        {
            try
            {
                byte[] byteRequest = Encoding.UTF8.GetBytes(postData);
                HttpWebRequest httpWebRequest;
                httpWebRequest = (HttpWebRequest)HttpWebRequest.Create(url);
                //  httpWebRequest.Headers.Add("Accept-Encoding", "deflate");
                httpWebRequest.ContentType = "application/x-www-form-urlencoded";
                ServicePointManager.Expect100Continue = false;
                //httpWebRequest.Referer = url;
                //httpWebRequest.Accept = accept;
                //httpWebRequest.UserAgent = userAgent;
                httpWebRequest.Method = "Post";
                httpWebRequest.KeepAlive = false;
                httpWebRequest.ContentLength = byteRequest.Length;
                httpWebRequest.Timeout = 99999999;
                Stream stream = httpWebRequest.GetRequestStream();
                stream.Write(byteRequest, 0, byteRequest.Length);
                stream.Close();
                HttpWebResponse httpWebResponse;
                httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                Stream responseStream = httpWebResponse.GetResponseStream();
                StreamReader streamReader = new StreamReader(responseStream, Encoding.UTF8);
                string html = streamReader.ReadToEnd();
                streamReader.Close();
                responseStream.Close();

                return html.Replace("\n", "").Replace("\r", "");
            }
            catch
            {
                throw;
            }
        }
        #endregion

    }
}
