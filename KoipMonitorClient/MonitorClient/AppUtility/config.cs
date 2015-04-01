using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data;
using System.IO;

namespace MonitorClient.AppUtility
{
    public class config
    {
        static string strtitle = "爱唱服务管理系统                    ";
        public static void MessageBox(string Msg)
        {
            try
            {
                System.Windows.Forms.MessageBox.Show(Msg, strtitle, MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message, strtitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public static Model.ServerInfo getServerInfo(int id)
        {
            try
            {
                if (!File.Exists(localserverinfo.directoryName + "serverinfo.dll"))
                    return null;
                //MySqlHeader.MySqlCmdHeader cmd = MySqlHeader.MySqlCmdHeader.Instance;
                //string strsql = "select areasingno,areasing_name from d_areasinger";
                DataTable dt = new DataTable("serverinfo");
                dt.ReadXml(localserverinfo.directoryName + "serverinfo.dll");
                if (dt == null)
                    return null;
                if (dt.Rows.Count == 0)
                    return null;
                Model.ServerInfo _ServerInfo = new Model.ServerInfo();
                foreach (DataRow rows in dt.Rows)
                {
                    if (id == Convert.ToInt32(rows[0].ToString()))
                    {
                        _ServerInfo.ip = rows[2].ToString();
                        _ServerInfo.port = Convert.ToInt32(rows[3].ToString());
                        break;
                    }

                }


                return _ServerInfo;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
