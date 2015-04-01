using System;
using System.Collections.Generic;
using System.Text;
using Kernel;
using System.Threading;
using Commonality;
namespace KOIPMonitor
{
    class tcpClient_ReceiveEvent
    {
        public static void ReceiveEvent(TCPClientStateObject request)
        {
            try
            {
                if (request == null)
                    return;
                
                //Commonality.ConsoleManage.Write(Commonality.ErrorLevel.Response, 
                //    "TCPCLIENT RECEIVE", "CMD1=" + request.cmd1.ToString() + " " 
                //    + "CMD2=" + request.cmd2.ToString()+" from "+
                //    request.wanIP.ToString()+":"+request.wanPort.ToString());


                //KIS客户端指令以2000起始,方法集在KIS_C文件夹下面
                switch (request.cmd1)
                {
                    //case Commonality.SongCmd.RspGetSongDataVer:
                    //    IsSongServ.client.RspGetSongDataVer.process(request);
                    //    break;
                    //case Commonality.SongCmd.RspDownSongData:
                    //    IsSongServ.client.RspDownSongData.process(request);
                    //    break;
                    //case Commonality.SongCmd.SongFile:
                    //    IsSongServ.client.GetSongMIC.process(request);
                    //    break;

                    //case KNSCmd.UpdateUserLoginLoc:
                    //    BusinessDAL.KNS.client.UpdateUserLoginLoc.process(request);
                    //    Commonality.ConsoleManage.Write(Commonality.ErrorLevel.Response, "TCP Client Receive", "CMD=" + request.cmd1.ToString() + " " + "BACKCODE=" + request.cmd2.ToString());
                    //    break;

                    //case CommCmd.RegServer:
                    //    Commonality.ConsoleManage.Write(Commonality.ErrorLevel.Response, "TCP Client Receive", "CMD=" + request.cmd1.ToString() + " " + "BACKCODE=" + request.cmd2.ToString());
                    //    break;

                    //case CommCmd.Handshake:
                    //    Commonality.ConsoleManage.Write(Commonality.ErrorLevel.Response, "TCP Client Receive", "CMD=" + request.cmd1.ToString() + " " + "BACKCODE=" + request.cmd2.ToString());
                    //    break;

                    //case KISCmd.StopUser:
                    //    break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                //Commonality.ConsoleManage.Write(Commonality.ErrorLevel.Serious, "BusinessDAL.KNS>>tcpClient_ReceiveEvent>>ReceiveEvent>>", ex.Message);

            }
            finally
            {
                //if (!string.IsNullOrEmpty(request.receiveFileTemporarily))
                //    ThreadPool.QueueUserWorkItem(new WaitCallback(DiskIO.Del), request.receiveFileTemporarily);
            }
        }
    }
}


