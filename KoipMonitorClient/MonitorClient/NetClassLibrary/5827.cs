using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kernel;
using System.Data;
using MonitorClient.AppUtility;

namespace MonitorClient.NetClassLibrary
{
    class _5827
    {
       //public static event FrmLogin.HideThisForm _HideThisForm;
       //public static event FrmLogin.ShowMsgLog _ShowMsgLog;
        public static event FrMain.ShowMsg _ShowMsg;
        
        /// <summary>
        /// 
        /// </summary>
        public static void Procedure(TCPClientStateObject request)
        {
            try
            {
                if (request == null)
                    return;

                if (request.cmd2 == -101)
                {
                    // AppUtility.config.MessageBox("错误提示解包失败");
                    _ShowMsg("错误提示解包失败");
                    return;
                }
                if (request.cmd2 == -500)
                {
                    _ShowMsg("服务端异常错误！");
                    return;
                }
                if (request.cmd2 == -119)
                {
                    _ShowMsg("查询无数据！");
                    return;
                }
                
                DataTable ReadBodyData = new DataTable();
                MonitorClient.AppUtility.CommClass.ReadXML(request.receiveBuffer, ref ReadBodyData);
                if (MonitorClient.AppUtility.CommonList.t_servertypeReal == null)
                {
                    MonitorClient.AppUtility.CommonList.t_servertypeReal = new DataTable();
                }
                MonitorClient.AppUtility.CommonList.Sett_servertypeReal(ReadBodyData.Copy());
                MonitorClient.AppUtility.ServerStateTimer.GetServerInfo(null);
                ReadBodyData.Clear();

                _ShowMsg("");

               
            }
            catch (Exception ex)
            {
                _ShowMsg(ex.Message);
            }
        }
    }
}
