using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.XPath;
using System.IO;

namespace ServiceStack.TCP
{
    public class Tool
    {
        /// <summary>
        /// 加载存储redis的服务器数据 
        /// </summary>
        /// <returns></returns>
        public List<RedisHostEntity> LoadTCPRedisConfigureToRedis()
        {
            string s = AppDomain.CurrentDomain.SetupInformation.ConfigurationFile;
            string path = s.Substring(0, s.LastIndexOf(@"\")) + "\\TCPRedisConfigure.xml";
            var xml = new XPathDocument(path);
            XPathNavigator nav = xml.CreateNavigator();
            XPathNodeIterator iter = nav.Select("Root/RedisClientSection");
            List<RedisHostEntity> list = new List<RedisHostEntity>();
            while (iter.MoveNext())
            {
                var Connections = iter.Current.Select("Connection");
                while (Connections.MoveNext())
                {
                    string data = Connections.Current.GetAttribute("Host", "");
                    if (!data.Contains(":"))
                    {
                        continue;
                    }
                    RedisHostEntity e = new RedisHostEntity
                    {
                        Name = Connections.Current.GetAttribute("Name", ""),
                        Key=data,
                        Host = data.Split(':')[0],
                        Port = data.Split(':')[1]
                        //Connections = Connections.Current.GetAttribute("Connections", ""),
                        //DB = Convert.ToInt32(Connections.Current.GetAttribute("DB", ""))
                    };
                    list.Add(e);
                }
            }
            return list;
        }

        #region 将字符串转换为Int32

        public static Int32 ToInt32(object str)
        {
            return ToInt32(str, 0);
        }
        public static Int32 ToInt32(object str, Int32 defValue)
        {
            Int32 outValue;

            if (str != null && !String.IsNullOrEmpty(str.ToString()))
            {
                if (Int32.TryParse(str.ToString(), out outValue))
                {
                    return outValue;
                }
            }
            return defValue;
        }
        #endregion

        #region 将文本写成任意扩展名的文件
        public static void WriteTextToFile(string Text)
        {
            string FileFullPathAndName = AppDomain.CurrentDomain.BaseDirectory + "\\RedisLog\\" + DateTime.Now.ToString("yyyyMMdd") + ".txt";
            WriteTextToFile(Text + "|" + DateTime.Now.ToString() + "\r\n", FileFullPathAndName, true, "gb2312");
        }
        public static void WriteTextToFile(string Text, string FileFullPathAndName)
        {
            WriteTextToFile(Text, FileFullPathAndName, false, "gb2312");
        }
        public static void WriteTextToFile(string Text, string FileFullPathAndName, bool IsAppend)
        {
            WriteTextToFile(Text, FileFullPathAndName, IsAppend, "gb2312");
        }
        public static void WriteTextToFile(string Text, string FileFullPathAndName, bool IsAppend, string encoding)
        {
            if (Text == "" || Text == null)
            {
                return;
            }
            string DirName = GetDirByFileName(FileFullPathAndName);
            if (!Directory.Exists(DirName))
            {
                Directory.CreateDirectory(DirName);
            }
            if (!File.Exists(FileFullPathAndName))
            {
                using (File.Create(FileFullPathAndName)) { };
            }
            Encoding gbEncode = Encoding.GetEncoding(encoding);
            using (StreamWriter swFromFile = new StreamWriter(FileFullPathAndName, IsAppend, gbEncode))
            {
                swFromFile.Write(Text);
                swFromFile.Close();
            }
        }
        #endregion

        #region 根据文件名,取得目录名
        public static string GetDirByFileName(string FileName)
        {
            int position = FileName.LastIndexOf(@"\");
            if (position > 0)
            {
                return FileName.Substring(0, position);
            }
            else
            {
                return "";
            }
        }

        #endregion
    }
}
