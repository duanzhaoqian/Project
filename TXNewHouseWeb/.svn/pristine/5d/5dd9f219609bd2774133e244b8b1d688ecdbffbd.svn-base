using System;
using System.IO;
using System.Text;
using System.Xml;
using TXNewHouseServices.models;

namespace TXNewHouseServices
{
    public class ServiceCom
    {
        // Rank主体是否运行
        public string IS_RUN_BODY = "IsRunBody";
        // Rank开始时间
        public string BEGINTIME = "BeginTime";
        // Rank执行服务时间间隔
        public string TIME_INTERVAL = "TimeInterval";

        // 楼盘特色主体运行状态
        public string IS_RUN_BODY_CHARACTERISTIC = "IsRunBody_Characteristic";
        // 楼盘特色开始时间
        public string BEGINTIME_CHARACTERISTIC = "BeginTime_Characteristic";
        // 楼盘特色执行服务时间间隔
        public string TIME_INTERVAL_CHARACTERISTIC = "TimeInterval_Characteristic";

        private static ServiceCom _instance;

        private ServiceCom()
        {
        }

        public static ServiceCom Instance
        {
            get
            {
                if (null == _instance)
                {
                    _instance = new ServiceCom();
                }
                return _instance;
            }
        }

        /// <summary>
        /// 获取Settings配置文件
        /// </summary>
        /// <returns></returns>
        public SettingsEntity GetSettings()
        {
            var xml = LoadSettings();

            if (null == xml)
            {
                return null;
            }

            var root = GetRootNodeByXmlDoc(xml);

            if (null == root)
            {
                return null;
            }

            var settings = new SettingsEntity();

            #region Rank

            // 读取IsRunBody
            var node = GetNodeFromXml(root, IS_RUN_BODY);
            string value = GetNodeValue(node);
            int tmpInt;
            if (!int.TryParse(Convert.ToString(value), out tmpInt))
            {
                settings.IsRunBody = false;
            }
            else
            {
                settings.IsRunBody = (tmpInt == 1);
            }

            // 读取BeginTime
            node = GetNodeFromXml(root, BEGINTIME);
            value = GetNodeValue(node);
            DateTime tmpDateTime;
            if (!DateTime.TryParse(Convert.ToString(value), out tmpDateTime))
            {
                settings.BeginTime = DateTime.MaxValue;
            }
            else
            {
                settings.BeginTime = tmpDateTime;
            }

            // 读取TimeInterval
            node = GetNodeFromXml(root, TIME_INTERVAL);
            value = GetNodeValue(node);
            if (!int.TryParse(Convert.ToString(value), out tmpInt))
            {
                settings.TimeInterval = 1; // 默认为1小时
            }
            else
            {
                settings.TimeInterval = tmpInt;
            }

            #endregion



            #region Characteristic

            // 读取IsRunBody_Characteristic
            node = GetNodeFromXml(root, IS_RUN_BODY_CHARACTERISTIC);
            value = GetNodeValue(node);
            if (!int.TryParse(Convert.ToString(value), out tmpInt))
            {
                settings.IsRunBody_Characteristic = false;
            }
            else
            {
                settings.IsRunBody_Characteristic = (tmpInt == 1);
            }

            // 读取BeginTime_Characteristic
            node = GetNodeFromXml(root, BEGINTIME_CHARACTERISTIC);
            value = GetNodeValue(node);
            if (!DateTime.TryParse(Convert.ToString(value), out tmpDateTime))
            {
                settings.BeginTime_Characteristic = DateTime.MaxValue;
            }
            else
            {
                settings.BeginTime_Characteristic = tmpDateTime;
            }

            // 读取TimeInterval_Characteristic
            node = GetNodeFromXml(root, TIME_INTERVAL_CHARACTERISTIC);
            value = GetNodeValue(node);
            if (!int.TryParse(Convert.ToString(value), out tmpInt))
            {
                settings.TimeInterval_Characteristic = 1; // 默认为1小时
            }
            else
            {
                settings.TimeInterval_Characteristic = tmpInt;
            }

            #endregion

            return settings;
        }

        /// <summary>
        /// 加载Settings文件
        /// </summary>
        /// <returns></returns>
        private XmlDocument LoadSettings()
        {
            var s = AppDomain.CurrentDomain.SetupInformation.ConfigurationFile;
            var xml = new XmlDocument();
            var settings = new XmlReaderSettings { IgnoreComments = true };
            using (var reader = XmlReader.Create(s.Substring(0, s.LastIndexOf(@"\", StringComparison.Ordinal)) + "\\settings.xml", settings))
            {
                xml.Load(reader);
                return xml;
            }
        }

        /// <summary>
        /// 保存Settings文件
        /// </summary>
        /// <param name="xml"></param>
        private void SaveSettings(XmlDocument xml)
        {
            string s = AppDomain.CurrentDomain.SetupInformation.ConfigurationFile;
            using (var writer = new XmlTextWriter(s.Substring(0, s.LastIndexOf(@"\", StringComparison.Ordinal)) + "\\settings.xml", Encoding.Default))
            {
                writer.Formatting = Formatting.Indented;
                xml.Save(writer);    
            }
        }

        /// <summary>
        /// 获取xml根节点
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        private XmlNode GetRootNodeByXmlDoc(XmlDocument xml)
        {
            var root = xml.SelectSingleNode("settings");
            if (null == root || (!root.HasChildNodes))
            {
                return null;
            }

            return root;
        }

        /// <summary>
        /// 获取Xml中指定key的值
        /// </summary>
        /// <param name="root"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        private XmlNode GetNodeFromXml(XmlNode root, string key)
        {
            foreach (XmlNode node in root.ChildNodes)
            {
                var attr = node.Attributes;
                if (null == attr)
                {
                    continue;
                }
                if (key.Equals(attr["key"].Value))
                {
                    return node;
                }
            }

            return null;
        }

        /// <summary>
        /// 获取节点的value值
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        private string GetNodeValue(XmlNode node)
        {
            if (null == node)
            {
                return null;
            }

            var attr = node.Attributes;
            if (null != attr)
            {
                return attr["value"].Value;
            }

            return null;
        }

        /// <summary>
        /// 设置xml中配置节点的值
        /// </summary>
        /// <param name="settings"></param>
        /// <returns></returns>
        public void SaveSettingsForXml(SettingsEntity settings)
        {
            var xml = LoadSettings();

            if (null == xml)
            {
                return;
            }

            var root = GetRootNodeByXmlDoc(xml);

            if (root == xml)
            {
                return;
            }

            XmlNode node = GetNodeFromXml(root, IS_RUN_BODY);
            var attr = node.Attributes;
            if (null != attr)
            {
                attr["value"].Value = settings.IsRunBody ? "1" : "0";
            }

            node = GetNodeFromXml(root, BEGINTIME);
            attr = node.Attributes;
            if (null != attr)
            {
                attr["value"].Value = string.Format("{0:yyyy-MM-dd HH:mm:ss}", settings.BeginTime);
            }

            node = GetNodeFromXml(root, TIME_INTERVAL);
            attr = node.Attributes;
            if (null != attr)
            {
                attr["value"].Value = Convert.ToString(settings.TimeInterval);
            }

            node = GetNodeFromXml(root, IS_RUN_BODY_CHARACTERISTIC);
            attr = node.Attributes;
            if (null != attr)
            {
                attr["value"].Value = settings.IsRunBody_Characteristic ? "1" : "0";
            }

            node = GetNodeFromXml(root, BEGINTIME_CHARACTERISTIC);
            attr = node.Attributes;
            if (null != attr)
            {
                attr["value"].Value = string.Format("{0:yyyy-MM-dd HH:mm:ss}", settings.BeginTime_Characteristic);
            }

            node = GetNodeFromXml(root, TIME_INTERVAL_CHARACTERISTIC);
            attr = node.Attributes;
            if (null != attr)
            {
                attr["value"].Value = Convert.ToString(settings.TimeInterval_Characteristic);
            }

            SaveSettings(xml);
        }

        ///// <summary>
        ///// 写日志
        ///// </summary>
        ///// <param name="msg"></param>
        //public void WriteLog(string msg)
        //{
        //    string s = AppDomain.CurrentDomain.SetupInformation.ConfigurationFile;
        //    string filePath = s.Substring(0, s.LastIndexOf(@"\", StringComparison.Ordinal)) + "\\log.txt";

        //    StreamWriter sw;
        //    if (!File.Exists(filePath))
        //    {
        //        sw = File.CreateText(filePath);
        //    }
        //    else
        //    {
        //        sw = File.AppendText(filePath);
        //    }
        //    sw.Write(string.Format("{0:yyyy-MM-dd HH:mm:ss}\t", DateTime.Now) + msg + Environment.NewLine);
        //    sw.Close();
        //    sw.Dispose();
        //}

        /// <summary>
        /// 写日志
        /// </summary>
        /// <param name="filenamelast">文件名</param>
        /// <param name="msg">信息</param>
        public void WriteLog(string filenamelast, string msg)
        {
            string s = AppDomain.CurrentDomain.SetupInformation.ConfigurationFile;
            string filePath = s.Substring(0, s.LastIndexOf(@"\", StringComparison.Ordinal)) + string.Format("\\log_{0}.txt", filenamelast);

            StreamWriter sw;
            if (!File.Exists(filePath))
            {
                sw = File.CreateText(filePath);
            }
            else
            {
                sw = File.AppendText(filePath);
            }
            sw.Write(string.Format("{0:yyyy-MM-dd HH:mm:ss}\t", DateTime.Now) + msg + Environment.NewLine);
            sw.Close();
            sw.Dispose();
        }
    }
}