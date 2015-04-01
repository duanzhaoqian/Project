﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kernel;
using System.Data;
using MonitorClient.AppUtility;

namespace MonitorClient.NetClassLibrary
{
    class _5814
    {
       //public static event FrmLogin.HideThisForm _HideThisForm;
       //public static event FrmLogin.ShowMsgLog _ShowMsgLog;
       public static event FrMain.ShowMsg _ShowMsg;
       public static event CtrServerinfo.ReSetText _ReSetText;
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
                if (request.cmd2 == -112)
                {
                    _ShowMsg("删除设备信息不成功！");
                    return;
                }
                if (request.cmd2 != 0)
                {
                    _ShowMsg("删除设备信息不成功！");
                    return;
                }
                if (_ReSetText != null)
                {
                    _ReSetText();
                }
                //DataTable ReadBodyData = new DataTable();
                //MonitorClient.AppUtility.CommClass.ReadXML(request.receiveBuffer, ref ReadBodyData);
                //if (MonitorClient.AppUtility.CommonList.t_serverinfo == null)
                //{
                //    MonitorClient.AppUtility.CommonList.t_serverinfo = new DataTable();
                //}
                //MonitorClient.AppUtility.CommonList.Sett_serverinfo(ReadBodyData.Copy());
                //ReadBodyData.Clear();

                //_ShowMsg("设备添加成功");

              
                
            }
            catch (Exception ex)
            {
                _ShowMsg(ex.Message);             

            }
        }
    }
}
