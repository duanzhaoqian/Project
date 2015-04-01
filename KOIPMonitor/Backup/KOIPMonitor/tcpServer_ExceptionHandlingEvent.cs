using System;
using System.Collections.Generic;
using System.Text;
using Kernel;
using System.Threading;
using Commonality;
namespace KOIPMonitor
{
    public class tcpServer_ExceptionHandlingEvent
    {
        public static void ReceiveEvent(StateObject requset)
        {
            try
            {
                if (requset == null)
                    return;
                CommClass.RemoveClientConnList(requset);
            }
            catch
            {
                return;
            }
            
            //    KdsState.workSocket.Shutdown(System.Net.Sockets.SocketShutdown.Both);
            //    //Console.WriteLine("KIS服务异常" + "  KisState.workSocket.Shutdown(System.Net.Sockets.SocketShutdown.Both);");
            //    Commonality.ConsoleManage.Write(Commonality.ErrorLevel.Serious, "", "KisState.workSocket.Shutdown(System.Net.Sockets.SocketShutdown.Both);");
            //    //KdsState.workSocket.Close();

            //    Commonality.ConsoleManage.Write(Commonality.ErrorLevel.Safe, "Link Down", "USER:" + KdsState.wanIP + ":" + KdsState.wanPort.ToString());

            //}
            //catch (Exception ex)
            //{
            //    Commonality.ConsoleManage.Write(Commonality.ErrorLevel.Serious, "KoIpServerLibrary>>KMS_ReceiveEvent>>tcpServer_ReceiveEvent>>KMS  TCP 連接接收處理>>", ex.Message);
            //}
            //finally
            //{
            //    if (!string.IsNullOrEmpty(KdsState.receiveFileTemporarily))
            //        ThreadPool.QueueUserWorkItem(new WaitCallback(DiskIO.Del), KdsState.receiveFileTemporarily);
            //    try
            //    {
            //        KdsState.workSocket.Shutdown(System.Net.Sockets.SocketShutdown.Both);
            //        KdsState.workSocket.Close();

            //    }
            //    catch (Exception exnr)
            //    {
            //        Commonality.ConsoleManage.Write(Commonality.ErrorLevel.Serious, "KoIpServerLibrary>>KMS_ReceiveEvent>>tcpServer_ReceiveEvent>>KMS  TCP 連接接收處理>>", exnr.Message);
            //    }
            //}





        }
    }

}
