using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kernel;
using System.Data;
using MonitorClient.AppUtility;
using System.Threading;

namespace MonitorClient.NetClassLibrary
{
    class _5808
    {
       public static event FrmLogin.HideThisForm _HideThisForm;
       public static event FrmLogin.ShowMsgLog _ShowMsgLog;
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
                    _ShowMsgLog("错误提示解包失败");
                    return;
                }
                if (request.cmd2 == -107)
                {
                    _ShowMsgLog("登录验证失败，登录ID不存在，请输入正确的登录ID！");
                    return;
                }
                if (request.cmd2 == -136)
                {
                    _ShowMsgLog("登录密码错误，请输入正确的密码！");
                    return;
                }
                if (request.cmd2 == -116)
                {
                    _ShowMsgLog("登录验证失败，登录ID不存在，请输入正确的登录ID！");
                    return;
                }
                DataTable ReadBodyData = new DataTable();
                MonitorClient.AppUtility.CommClass.ReadXML(request.receiveBuffer, ref ReadBodyData);
                //Tools.Decompress(request.receiveBuffer, ref ByteResultKIS);
                if (ReadBodyData == null || ReadBodyData.Rows.Count == 0)
                {
                    _ShowMsgLog("获取登录验证数据失败，请联系管理员！");
                    return;
                }
                CommonList.mySESSION = ReadBodyData.Rows[0]["SESSION"].ToString();
                //if (_CloseFrmLogin != null)
                //{
                //    _CloseFrmLogin();
                //}
                //AppUtility.config.MessageBox("登录成功");
                if (MonitorClient.AppUtility.CommonList.CurrentServerInfo == null)
                    MonitorClient.AppUtility.CommonList.CurrentServerInfo = new Model.ServerInfo();
                MonitorClient.AppUtility.CommonList.CurrentServerInfo.ip = request.wanIP;
                MonitorClient.AppUtility.CommonList.CurrentServerInfo.port = request.wanPort;
               

                if (_HideThisForm != null)
                {
                    _HideThisForm();
                }
                try
                {
                   // //CommonList__TreeViewDataBind(null);
                   //  //MonitorClient.AppUtility.ServerStateTimer.ObjectTime();
                    ThreadPool.QueueUserWorkItem(new WaitCallback(MonitorClient.AppUtility.ServerStateTimer.ObjectTime), null);

                    MonitorClient.AppUtility.ServerStateTimer.GetServertype(null);
                    //Thread.Sleep(100);
                    //MonitorClient.AppUtility.ServerStateTimer.GetServerInfo(null);

                   // ThreadPool.QueueUserWorkItem(new WaitCallback(), null);
                }
                catch (Exception ex)
                {
                    _ShowMsgLog(ex.Message);
                }
                _ShowMsgLog("");
            }
            catch (Exception ex)
            {
                _ShowMsgLog(ex.Message);
                //if (!string.IsNullOrEmpty(request.receiveBuffer))
                //    ThreadPool.QueueUserWorkItem(new WaitCallback(DiskIO.Del), request.receiveBuffer);
            }
        }
    }
}
