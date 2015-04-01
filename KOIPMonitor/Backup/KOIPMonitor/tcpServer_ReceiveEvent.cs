using System;
using System.Collections.Generic;
using System.Text;
using Kernel;
using System.Threading;
using Commonality;
namespace KOIPMonitor
{
    public class tcpServer_ReceiveEvent
    {
        public static void ReceiveEvent(StateObject OmsState)
        {
            try
            {

                if (OmsState == null)
                    return;

                switch (OmsState.cmd1)
                {

                    case OMSCmd.ReqGetServList://启动服务请求
                        RspGetServList.process(OmsState);//启动服务回应
                        break;
                    case OMSCmd.ReqServOpt://启动服务请求
                        RspServOpt.process(OmsState);//启动服务回应
                        break;
                    case OMSCmd.ReqGetServState://获取服务器状态请求
                        RspGetServState.process(OmsState);//获取服务器状态回应
                        break;
                    case OMSCmd.ReqUserLogin://获取服务器状态请求
                        RspUserLogin.process(OmsState);//获取服务器状态回应
                        break;
                    case CommCmd.Handshake:
                        byte[] ResultByte=null;
                        CommonFunction.HandshakeByte(ref ResultByte);
                        OmsState.Send(ResultByte);
                        break;
                    default:
                        break;
                }
  
            }
            catch (Exception ex)
            {

                Commonality.ConsoleManage.Write(Commonality.ErrorLevel.Serious, "KOIPMonitor>>tcpServer_ReceiveEvent>>KOIPMonitor  TCP 連接接收處理>>", ex.Message);
            }
            finally
            {
                if (!string.IsNullOrEmpty(OmsState.receiveFileTemporarily))
                {
                    ThreadPool.QueueUserWorkItem(new WaitCallback(DiskIO.Del), OmsState.receiveFileTemporarily);
                    //KdsState.workSocket.Close();
                    //KdsState.workSocket.Shutdown(System.Net.Sockets.SocketShutdown.Both);
                }
                
            }
        }
    }
}
