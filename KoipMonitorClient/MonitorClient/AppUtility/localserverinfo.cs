using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;

namespace MonitorClient.AppUtility
{
    public class localserverinfo
    {
        public static string directoryName = "./";
        public static string getlocalserverinfoPath()
        {
            try
            {
                //if (KoipManage.Program.OStype != 0)
                //{
                    directoryName = Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetName().CodeBase).Remove(0, 6) + "\\";
                //}
                return directoryName;
            }
            catch (Exception ex)
            {
                //MessageBox(ex.Message);
                return string.Empty;
            }
        }
    }
}
