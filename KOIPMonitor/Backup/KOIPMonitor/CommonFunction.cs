using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Collections;
using Kernel;
using System.IO;
using System.Threading;

namespace KOIPMonitor
{
    class CommonFunction
    {
        public CommonFunction() { }
        ~CommonFunction() { }

        #region 发送数据

        public static void SendDatas(object _obj)
        {
            Hashtable _hashtable_Package = (Hashtable)_obj;
            StateObject state = (StateObject)_hashtable_Package["1"];
            try
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    using (BinaryWriter bw = new BinaryWriter(ms))
                    {
                        byte[] ResultByte = null;
                        ResultByte = (byte[])_hashtable_Package["4"];
                        byte[] GzipResultByte = null;   //Gzip压缩结果
                        Tools.GzipCompress(ref ResultByte, ref GzipResultByte);
                        byte ver = 1;

                        short cmd1 = (short)_hashtable_Package["2"];
                        short cmd2 = (short)_hashtable_Package["3"];

                        int packagelength = 0;
                        if (GzipResultByte != null)
                        {
                            packagelength = GzipResultByte.Length;
                        }
                        bw.Write(ver);
                        bw.Write(cmd1);
                        bw.Write(cmd2);
                        bw.Write(packagelength);
                        if (packagelength > 0)
                        {
                            bw.Write(GzipResultByte);
                        }
                        bw.Flush();

                        if (state != null)
                        {
                            if (state.workSocket.Connected)
                            {
                                state.Send(ms.ToArray());
                                Commonality.ConsoleManage.Write(Commonality.ErrorLevel.Response, "SendDatas", "CMD=" + cmd1.ToString() + " " + "BACKCODE=" + cmd2.ToString() + ">>IPaddress:" + state.wanIP.ToString() + ">>Port:" + state.wanPort.ToString());
                            }
                            else
                            {
                                Commonality.ConsoleManage.Write(Commonality.ErrorLevel.Response, "SendDatas", "state.workSocket is not Connected");

                            }

                        }
                        else
                        {
                            Commonality.ConsoleManage.Write(Commonality.ErrorLevel.Response, "SendDatas", "StateObject is null");

                        }


                    }
                }
            }
            catch
            {
                return;
            }
            finally
            {
                _hashtable_Package.Clear();
                //if (!string.IsNullOrEmpty(state.receiveFileTemporarily))
                //    DiskIO.Del(state.receiveFileTemporarily);
                ////GC.Collect();
            }
        }

        #endregion


        #region TCP客户端发送数据
        /// <summary>
        /// TCP客户端发送数据
        /// </summary>
        /// <param name="_obj"></param>
        public static void TcpClientSend(object _obj)
        {
            Hashtable _hashtable_Package = (Hashtable)_obj;
            Kernel.AsynTCPClient tcpClient = (Kernel.AsynTCPClient)_hashtable_Package["1"];

            try
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    using (BinaryWriter bw = new BinaryWriter(ms))
                    {
                        byte[] ResultByte = null;
                        ResultByte=(byte[])_hashtable_Package["4"];
                        byte[] GzipResultByte = null;   //Gzip压缩结果
                        Tools.GzipCompress(ref ResultByte, ref GzipResultByte);
                        byte ver = 1;

                        short cmd1 = (short)_hashtable_Package["2"];
                        short cmd2 = (short)_hashtable_Package["3"];

                        int packagelength = 0;
                        if (GzipResultByte != null)
                        {
                            packagelength = GzipResultByte.Length;
                        }
                        bw.Write(ver);
                        bw.Write(cmd1);
                        bw.Write(cmd2);
                        bw.Write(packagelength);

                        if (packagelength > 0)
                        {
                            bw.Write(GzipResultByte);
                        }
                        bw.Flush();

                        if (tcpClient.Connected)
                        {
                            tcpClient.BeginSend(ms.ToArray());
                            Commonality.ConsoleManage.Write(Commonality.ErrorLevel.Response, "Client Send Data Success", "CMD=" + cmd1.ToString() + " " + "BACKCODE=" + cmd2.ToString());
                        }
                        else
                        {
                            Commonality.ConsoleManage.Write(Commonality.ErrorLevel.Response, "CommonFunction>>TcpClientSend>>", "TCP Client Disconnect");

                        }

                    }
                }
            }
            catch
            {
                return;
            }
            finally
            {
                _hashtable_Package.Clear();
                ////GC.Collect();
            }
            
        }

        /// <summary>
        /// TCP客户端发送数据Gzip不压缩
        /// </summary>
        /// <param name="_obj"></param>
        public static void UnGzipTcpClientSend(object _obj)
        {
            Hashtable _hashtable_Package = (Hashtable)_obj;
            Kernel.AsynTCPClient tcpClient = (Kernel.AsynTCPClient)_hashtable_Package["1"];

            try
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    using (BinaryWriter bw = new BinaryWriter(ms))
                    {
                        byte[] ResultByte = null;
                        ResultByte = (byte[])_hashtable_Package["4"];
                        byte ver = 1;

                        short cmd1 = (short)_hashtable_Package["2"];
                        short cmd2 = (short)_hashtable_Package["3"];

                        int packagelength = 0;
                        if (ResultByte != null)
                        {
                            packagelength = ResultByte.Length;
                        }
                        bw.Write(ver);
                        bw.Write(cmd1);
                        bw.Write(cmd2);
                        bw.Write(packagelength);

                        if (packagelength > 0)
                        {
                            bw.Write(ResultByte);
                        }
                        bw.Flush();

                        if (tcpClient.Connected)
                        {
                            tcpClient.BeginSend(ms.ToArray());
                            Commonality.ConsoleManage.Write(Commonality.ErrorLevel.Response, "Client Send Data Success", "CMD=" + cmd1.ToString() + " " + "BACKCODE=" + cmd2.ToString());
                        }
                        else
                        {
                            Commonality.ConsoleManage.Write(Commonality.ErrorLevel.Response, "CommonFunction>>TcpClientSend>>", "TCP Client Disconnect");

                        }

                    }
                }
            }
            catch
            {
                return;
            }
            finally
            {
                _hashtable_Package.Clear();
                ////GC.Collect();
            }

        }

        #endregion

        #region 发送握手
        /// <summary>
        /// 发送数据(握手)
        /// </summary>
        /// <param name="ResultByte"></param>
        public static void HandshakeByte(ref byte[] ResultByte)
        {

            using (MemoryStream ms = new MemoryStream())
            {
                using (BinaryWriter bw = new BinaryWriter(ms))
                {
                    byte ver = 1;
                    short cmd1 = CommCmd.Handshake;
                    short cmd2 = ErrCommon.Success; //0;
                    int packagelength = 0;
                    bw.Write(ver);
                    bw.Write(cmd1);
                    bw.Write(cmd2);
                    bw.Write(packagelength);
                    bw.Flush();
                    ResultByte = ms.ToArray();
                }
            }

        }
        #endregion

    }
}
