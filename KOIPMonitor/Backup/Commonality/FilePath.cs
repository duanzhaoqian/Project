#define win
using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.IO;
using System.Data;

namespace KoIp.Commonality
{
    public class FilePath
    {
        /// <summary>
        /// 目录
        /// </summary>
        /// 
//        private static string StrbigPath = System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
//#if LINUX
//        private static string strDataPath = StrbigPath + "/".Trim();
//#else
//        private static string strDataPath = StrbigPath + "\\".Trim();
//#endif
        /// <summary>
        /// 服务器类型值{0:Linux；1:Windows}默认为Linux
        /// </summary>
        public static int OStype = 0;
        /// <summary>
        /// 配置文件路径
        /// </summary>        
        public static string ConfigFilePath = "/" + Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetName().CodeBase).Remove(0, 6) + "/server.xml";
        public static string wirespeedpath = "/" + Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetName().CodeBase).Remove(0, 6) + "/21788.mic";

        /// <summary>
        /// 设置全局变量路径
        /// </summary>
        public static void SetPubPath()
        {
            if (OStype != 0)
            {
                ConfigFilePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetName().CodeBase).Remove(0, 6) + "\\server.xml";
                wirespeedpath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetName().CodeBase).Remove(0, 6) + "\\21788.mic";
            }
        }
        //public static string kmsPath = ConfigFilePath + "kmsinfo.xml";
        //public static string kisPath = ConfigFilePath + "kisinfo.xml";
        //public static string knsPath = ConfigFilePath + "knsinfo.xml";
        //public static string ServerInfo = ConfigFilePath + "server.xml";
        //public static string ServerModePath = ConfigFilePath + "ServerMode.xml";
        //public static string wirespeedpath = ConfigFilePath + "21788.mic";

        #region 创建Server.xml文件
        /// <summary>
        /// 创建Server.xml文件
        /// </summary>
        public static void CreateConfig()
        {
            try
            {
                FileInfo file = new FileInfo(ConfigFilePath);
                if (file.Exists)
                {
                    return;
                }
                DataTable dt = new DataTable();
                string[] aryField = { "servermode", 
                                  "nodeid", 
                                  "kmsip", 
                                  "kmswanip", 
                                  "kmsport", 
                                  "kisip", 
                                  "kiswanip", 
                                  "kisport",
                                  "knsip", 
                                  "knswanip", 
                                  "knsport",
                                  "DBCONN", 
                                  "DATATYPE", 
                                  "AREAID",
                                  "HANDTIME",
                                  "HANDCOUNT"
                                };
                string[] aryVale = { "kns", 
                                  "1", 
                                  "192.168.0.90", 
                                  "192.168.0.90",
                                  "20001",
                                  "192.168.0.90", 
                                  "192.168.0.90",
                                  "30001",
                                  "192.168.0.90", 
                                  "192.168.0.90",
                                  "40001",
                                  "Server=192.168.8.119;Database=db_koip;Uid=root;Pwd=ising99;",
                                  "1",//1=MySQL;0=SQL SERVER
                                  "1",//区域
                                  "3",
                                  "30"
                               };
                for (int i = 0; i < aryField.Length; i++)
                {
                    dt.Columns.Add(new DataColumn(aryField[i], typeof(string)));//设置DataTable的ColumnName，根据不同的字段类型需要设计不同的typeof,最好分开写不要用for循环。
                }
                DataRow dr = dt.NewRow();
                for (int i = 0; i < aryField.Length; i++)
                {
                    dr[aryField[i]] = aryVale[i];//设置DataTable的行内容
                }
                dt.TableName = "Server";
                dt.Rows.Add(dr);
                CreateXML(ConfigFilePath, dt);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return;
            }

        }

        #region 创建XML文件
        /// <summary>
        /// 创建XML文件
        /// </summary>
        /// <param name="strXMLPath"></param>
        /// <param name="dt"></param>
        public static void CreateXML(string strXMLPath, DataTable dt)
        {
            dt.WriteXml(strXMLPath);
        }
        #endregion
        #endregion

       


























        /// <summary>
        /// 目录路径
        /// </summary>
        /// <returns></returns>
        private static string getdirpath()
        {
            try
            {
                //static string strDataPath = System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "data\\";
                string strDataPath = System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
                string[] temp = strDataPath.Split('\\');
                string strReturnPath = string.Empty;
                strReturnPath = strDataPath.Replace(temp[temp.Length - 2], "").Replace("\\\\", "");//+ "\\KoIpService\\" +KoIP.AppUtility.Encrypt.MD5Encrypt("kms.dll");
                return strReturnPath;
            }
            catch (Exception ex)
            {
                return string.Empty;
            }
        }
    }
}
