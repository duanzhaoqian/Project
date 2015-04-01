using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kernel;
using System.Data;
using MonitorClient.AppUtility;

namespace MonitorClient.NetClassLibrary
{
    class _5809
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
                if (request.cmd2 == -110)
                {
                    _ShowMsg("相同数据已存在，请勿重复操作！");
                    return;
                }
                
                DataTable ReadBodyData = new DataTable();
                MonitorClient.AppUtility.CommClass.ReadXML(request.receiveBuffer, ref ReadBodyData);
                if (MonitorClient.AppUtility.CommonList.t_servertype == null)
                {
                    MonitorClient.AppUtility.CommonList.t_servertype = new DataTable();
                }
                MonitorClient.AppUtility.CommonList.Sett_servertype(ReadBodyData.Copy());
                ReadBodyData.Clear();



                //if (request.cmd2 == -116)
                //{
                //    _ShowMsgLog("登录验证失败，登录ID不存在，请输入正确的登录ID！");
                //    return;
                //}
                ////Tools.Decompress(request.receiveBuffer, ref ByteResultKIS);
                //if (ReadBodyData == null || ReadBodyData.Rows.Count == 0)
                //{
                //    _ShowMsgLog("获取登录验证数据失败，请联系管理员！");
                //    return;
                //}
                //CommonList.mySESSION = ReadBodyData.Rows[0]["SESSION"].ToString();
                ////if (_CloseFrmLogin != null)
                ////{
                ////    _CloseFrmLogin();
                ////}
                ////AppUtility.config.MessageBox("登录成功");
                //if (MonitorClient.AppUtility.CommonList.CurrentServerInfo == null)
                //    MonitorClient.AppUtility.CommonList.CurrentServerInfo = new Model.ServerInfo();
                //MonitorClient.AppUtility.CommonList.CurrentServerInfo.ip = request.wanIP;
                //MonitorClient.AppUtility.CommonList.CurrentServerInfo.port = request.wanPort;

                //if (_HideThisForm != null)
                //{
                //    _HideThisForm();
                //}
                //DataTable ReadBodyData = new DataTable();
                //MonitorClient.AppUtility.CommClass.ReadXML(request.receiveBuffer, ref ReadBodyData);
                _ShowMsg("");
            }
            catch (Exception ex)
            {
                _ShowMsg(ex.Message);
                //if (!string.IsNullOrEmpty(request.receiveBuffer))
                //    ThreadPool.QueueUserWorkItem(new WaitCallback(DiskIO.Del), request.receiveBuffer);
            }
        }
    }
}
