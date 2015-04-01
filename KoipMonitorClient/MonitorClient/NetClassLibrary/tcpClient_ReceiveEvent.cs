using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kernel;

namespace MonitorClient.NetClassLibrary
{
    public class tcpClient_ReceiveEvent
    {
        public static event FrMain.ShowMsg _ShowMsg;
        public static void ReceiveEvent(TCPClientStateObject request)
        {
            try
            {
                if (request == null)
                {
                    _ShowMsg("连接对像失效");
                    return;
                }
                switch (request.cmd1)
                {
                    case MonitorClient.AppUtility.KOMCmd.OperateServerbackup:
                        MonitorClient.NetClassLibrary._5806.Procedure(request);

                        break;
                    case MonitorClient.AppUtility.KOMCmd.CheckPwd_back:
                        MonitorClient.NetClassLibrary._5808.Procedure(request);

                        break;
                    case MonitorClient.AppUtility.KOMCmd.AddServertype:
                        MonitorClient.NetClassLibrary._5809.Procedure(request);

                        break;
                    case MonitorClient.AppUtility.KOMCmd.skServertype:
                        MonitorClient.NetClassLibrary._5812.Procedure(request);

                        break;
                    case MonitorClient.AppUtility.KOMCmd.delServertype:
                        MonitorClient.NetClassLibrary._5810.Procedure(request);

                        break;
                    case MonitorClient.AppUtility.KOMCmd.updateServertype:
                        MonitorClient.NetClassLibrary._5811.Procedure(request);

                        break;
                    case MonitorClient.AppUtility.KOMCmd.AddUser:
                        MonitorClient.NetClassLibrary._5817.Procedure(request);

                        break;
                    case MonitorClient.AppUtility.KOMCmd.skUser:
                        MonitorClient.NetClassLibrary._5821.Procedure(request);

                        break;
                    case MonitorClient.AppUtility.KOMCmd.updateUser:
                        MonitorClient.NetClassLibrary._5820.Procedure(request);
                        break;
                    case MonitorClient.AppUtility.KOMCmd.delUser:
                        MonitorClient.NetClassLibrary._5819.Procedure(request);
                        break;
                    case MonitorClient.AppUtility.KOMCmd.AddServer:
                        MonitorClient.NetClassLibrary._5813.Procedure(request);
                        break;
                    case MonitorClient.AppUtility.KOMCmd.skServer:
                        MonitorClient.NetClassLibrary._5816.Procedure(request);
                        break;
                    case MonitorClient.AppUtility.KOMCmd.DELServer:
                        MonitorClient.NetClassLibrary._5814.Procedure(request);
                        break;
                    case MonitorClient.AppUtility.KOMCmd.UpdateServer:
                        MonitorClient.NetClassLibrary._5815.Procedure(request);
                        break;
                    case MonitorClient.AppUtility.KOMCmd.skServertype2:
                        MonitorClient.NetClassLibrary._5827.Procedure(request);
                        break;
                    case MonitorClient.AppUtility.KOMCmd.skServer2_backup:
                        MonitorClient.NetClassLibrary._5802.Procedure(request);
                        break;
                    default:
                       // ClientDataClass.RecData(request);
                        break;
                }
            }
            catch (Exception ex)
            {
                _ShowMsg(ex.Message);
                // KoIp.Commonality.ConsoleManage.Write(KoIp.Commonality.ErrorLevel.Serious, "KoIp.BusinessDAL.KIS>>tcpClient_ReceiveEvent>>ReceiveEvent>>", ex.Message);
               // LogClassLibrary.ConsoleManage.Write(LogClassLibrary.ErrorLevel.Errors, request.cmd1.ToString() + "接收数据：" + request.cmd1.ToString(), ex.Message.ToString());

            }
            finally
            {
                //if (!string.IsNullOrEmpty(request.receiveBuffer))
                //    ThreadPool.QueueUserWorkItem(new WaitCallback(DiskIO.Del), request.receiveBuffer);
            }
        }
        /// <summary>
        /// 登录成功后生成注册KNS文件03.XML
        /// </summary>
        /// <param name="request"></param>


        //public static void RecData(TCPClientStateObject request)
        //{
        //    try
        //    {
        //        ///KIS客户端指令以2000起始,方法集在KIS_C文件夹下面
        //        //Commonality.ConsoleManage.Write(Commonality.ErrorLevel.Response,
        //        //"TCPCLIENT RECEIVE", "CMD1=" + request.cmd1.ToString() + " "
        //        //+ "CMD2=" + request.cmd2.ToString() + " from " +
        //        //request.wanIP.ToString() + ":" + request.wanPort.ToString());

        //        LogClassLibrary.ConsoleManage.Write(LogClassLibrary.ErrorLevel.Errors,
        //     request.cmd1.ToString() + "接收错误码 ", "CMD1=" + request.cmd1.ToString() + " "
        //                                 + "CMD2=" + request.cmd2.ToString() + " from " +
        //                                 request.wanIP.ToString() + ":" + request.wanPort.ToString());
        //        DataTable Readdt = new DataTable();
        //        new NetClassLibrary.CommClass().ReadGzipXML(request.receiveBuffer, ref Readdt);
        //        if (Readdt == null)
        //        {
        //            LogClassLibrary.ConsoleManage.Write(LogClassLibrary.ErrorLevel.Errors, request.cmd1.ToString() + "接收指令：" + request.cmd1.ToString(), "Readdt == null ");
        //            return;
        //        }
        //        if (Readdt.Rows.Count == 0)
        //        {
        //            LogClassLibrary.ConsoleManage.Write(LogClassLibrary.ErrorLevel.Errors, request.cmd1.ToString() + "接收指令：" + request.cmd1.ToString(), "Readdt.Rows.Count==0");
        //            return;
        //        }
        //        StringBuilder sb = new StringBuilder();
        //        foreach (DataRow rows in Readdt.Rows)
        //        {
        //            foreach (DataColumn clum in Readdt.Columns)
        //            {

        //                sb.Append(clum.ColumnName + "=" + rows[clum]);
        //                sb.Append("    ");
        //            }
        //            sb.AppendLine("");
        //        }
        //        //Console.WriteLine(sb.ToString());
        //        LogClassLibrary.ConsoleManage.Write(LogClassLibrary.ErrorLevel.Errors, request.cmd1.ToString() + "接收数据：" + request.cmd1.ToString(), sb.ToString());
        //    }
        //    catch (Exception ex)
        //    {
        //        LogClassLibrary.ConsoleManage.Write(LogClassLibrary.ErrorLevel.Errors, request.cmd1.ToString() + "接收数据：" + request.cmd1.ToString(), ex.Message.ToString());

        //    }
        //}
    }
}
