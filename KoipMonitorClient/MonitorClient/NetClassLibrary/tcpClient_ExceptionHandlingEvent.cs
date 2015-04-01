using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kernel;

namespace MonitorClient.NetClassLibrary
{
    public class tcpClient_ExceptionHandlingEvent
    {
        public static void ReceiveEvent(TCPClientStateObject request)
        {
            try
            {
                if (request == null)
                    return;

                ///KIS客户端指令以2000起始,方法集在KIS_C文件夹下面
                //Commonality.ConsoleManage.Write(Commonality.ErrorLevel.Response,
                //"TCPCLIENT RECEIVE", "CMD1=" + request.cmd1.ToString() + " "
                //+ "CMD2=" + request.cmd2.ToString() + " from " +
                //request.wanIP.ToString() + ":" + request.wanPort.ToString());

                ////KoIp.BusinessDAL.KMS._11.Dispose(KmsState);
                ///////KMS端指令以1000起始
                //switch (sender.cmd1)
                //{
                //    ///处理KIS连到到KMS将IPS存储在KMS
                //    case 11:
                //        //KoIpServerLibrary.KMS_S._1000.Procedure(KmsState);
                //        break;
                //    default:
                //        break;
                //}
            }
            catch (Exception ex)
            {

                //AppUtility.ConsoleManage.Write(AppUtility.ErrorLevel.Serious, "KoIpServerLibrary>>KMS_ReceiveEvent>>tcpServer_ReceiveEvent>>KMS  TCP 連接接收處理>>", ex.Message);
            }
            finally
            {
                //if (!string.IsNullOrEmpty(sender.receiveFileTemporarily))
                //    ThreadPool.QueueUserWorkItem(new WaitCallback(DiskIO.Del), sender.receiveFileTemporarily);
            }
        }
    }
}
