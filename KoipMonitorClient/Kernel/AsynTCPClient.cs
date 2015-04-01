﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.IO;
using System.Reflection;
using System.Collections;

namespace Kernel
{
    public class AsynTCPClient:IDisposable
    {
        private ManualResetEvent connectDone = new ManualResetEvent(false);
        private ManualResetEvent sendDone = new ManualResetEvent(false);
        private ManualResetEvent receiveDone = new ManualResetEvent(false);
        private string _targetAddress;
        private int _targetPort;
        private int _pendingConnectionsQueue = 1000;
        public Socket tcpClient;
        private IPEndPoint remoteEP;
        private bool _connected;
        private int AVRecvDataMax = 2097152;//音视频接收最大Buffer


        /// <summary>
        /// 接收到封包後續處理
        /// </summary>
        public event AsynTCPClientReceiveEvent ReceiveEvent;
        public event AsynClientExceptionHandlingEvent ExceptionHandlingEvent;
        /// <summary>
        /// 音视频接收到封包後續處理
        /// </summary>
        public event AsynTCPClientReceiveEvent AudioVideoReceiveEvent;


        /// <summary>
        /// 操作系统类型值
        /// </summary>
        private int OSType = 0;
        /// <summary>
        /// 传入操作系统类型值
        /// </summary>
        /// <param name="OsType"></param>
        public AsynTCPClient(string OsType)
        {
            try
            {
                OSType = Convert.ToInt32(OsType);
            }
            catch
            {
                OSType = 0;
            }
        }
        public AsynTCPClient()
        {

        }
        ~AsynTCPClient()
        {
            this.Dispose();
        }

        public void Dispose()
        {

            tcpClient = null;
            connectDone = null;
            sendDone = null;
            receiveDone = null;
            tcpClient = null;
            remoteEP = null;
            ReceiveEvent = null;
            ExceptionHandlingEvent = null;
        }

        //...判斷Socket 是否連接
        public bool Connected
        {
            set
            {
                this._connected = value;
                
            }
            get
            {
                try
                {
                    return this.tcpClient.Connected;
                }
                catch
                {
                    return this._connected;
                }
                //
            }
        }

        /// <summary>
        /// 輸入目標位置
        /// </summary>        
        public string targetAddress
        {
            set
            {
                this._targetAddress = value;
            }
            get
            {
                return this._targetAddress;
            }
        }

        /// <summary>
        /// 設定傾聽端口
        /// </summary>
        public int targetPort
        {
            set
            {
                this._targetPort = value;
            }
            get
            {
                return this._targetPort;
            }
        }

        /// <summary>
        /// 設定TCP 等待連接佇列大小
        /// </summary>
        public int pendingConnectionsQueue
        {
            set
            {
                this._pendingConnectionsQueue = value;
            }
            get
            {
                return this._pendingConnectionsQueue;
            }
        }

        public void BeginConnect(string Address, int Port)
        {
            try
            {
                _targetAddress = Address;
                _targetPort = Port;
                remoteEP = new IPEndPoint(System.Net.IPAddress.Parse(_targetAddress), _targetPort);
                tcpClient = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                Thread listen = new Thread(new ThreadStart(Run));
                listen.Start();

                ////...等待連線成功
                //while (!this._connected)
                //{
                //    Thread.Sleep(500);
                //}
                
            }
            catch (Exception ex)
            {
                connectDone.Set();
                KConsole.Write(ErrorLevel.Serious, "Kernel>>AsynTCPClient>>Start", ex.Message);
                //GC.Collect();
                //throw new ArgumentNullException();
            }
        }

        private void Run()
        {

            try
            {
                connectDone.Reset();
                try
                {
                    tcpClient.BeginConnect(remoteEP, new AsyncCallback(ConnectCallback), null);
                }
                catch { }
                connectDone.WaitOne();
            }
            catch (Exception ex)
            {
 
            }
        }

       

        private void ConnectCallback(IAsyncResult ar)
        {
            try
            {
                tcpClient.EndConnect(ar);
                KConsole.Write(ErrorLevel.Debug, "Kernel>>AsynTCPClient>>Run>>", "連接>>" + tcpClient.RemoteEndPoint.ToString());
                this.Connected = true;
                connectDone.Set();
                BeginReceive();
            }
            catch (Exception ex)
            {
                connectDone.Set();
                //connectDone.Reset();
                this._connected = false;
                KConsole.Write(ErrorLevel.Serious, "Kernel>>AsynTCPClient>>Run>>", ex.Message);
            }
        }


        public void Stop()
        {
            try
            {
                Dispose();
                if (tcpClient != null)
                {
                    try
                    {
                        tcpClient.Close();
                        tcpClient.Shutdown(SocketShutdown.Both);
                    }
                    catch
                    {
                        tcpClient.Shutdown(SocketShutdown.Both);
                    }

                }
            }
            catch (Exception ex)
            {
                KConsole.Write(ErrorLevel.Serious, "Kernel>>AsynTCPClient>>Stop", ex.Message);
            }
            finally
            {
                tcpClient = null;
                //GC.Collect();
            }
        }


        public void BeginSend(byte[] buffer)
        {
            ////sendDone.Reset();
            //tcpClient.BeginSend(buffer, 0, buffer.Length, 0, new AsyncCallback(SendCallback), null);
            ////sendDone.WaitOne();


            try
            {
                if (tcpClient == null) return;
                //KConsole.Write(ErrorLevel.Response, "", "KoIPRouter>>AsynTCPClient>>BeginSend>>Exception:" + tcpClient.Connected);

                if (!tcpClient.Connected) return;

                this.tcpClient.BeginSend(buffer, 0, buffer.Length, 0, new AsyncCallback(SendCallback), null);
            }
            catch (Exception ex)
            {
                KConsole.Write(ErrorLevel.Response, "", "KoIPRouter>>AsynTCPClient>>BeginSend>>Exception:" + ex.Message);
                //try
                //{
                //    tcpClient.Shutdown(SocketShutdown.Both);
                //}
                //catch
                //{
                //    tcpClient.Close();
                //}
                this._connected = false;
                tcpClient = null;
                // KConsole.Write(ErrorLevel.Serious, "Kernel>>StateObject>>Send", ex.Message);
            }
        }

        private void SendCallback(IAsyncResult ar)
        {
            //try
            //{
            //    int bytesSent = tcpClient.EndSend(ar);
            //    //sendDone.Set();
            //}
            //catch (Exception ex)
            //{
            //    //sendDone.Set();
            //    this._connected = false;
            //    KConsole.Write(ErrorLevel.Serious, "Kernel>>AsynTCPClient>>SendCallback>>", ex.Message);
            //}
            int bytesSent = 0;
            try
            {
                if (!tcpClient.Connected) return;
                if (tcpClient == null) return;
                bytesSent = tcpClient.EndSend(ar);
            }
            catch (Exception ex)
            {
                this._connected = false;
                tcpClient = null;
                KConsole.Write(ErrorLevel.Response, "", "KoIPRouter>>AsynTCPClient>>SendCallback>>Exception:" + ex.Message);
                //KConsole.Write(ErrorLevel.Serious, "Kernel>>StateObject>>SendCallback", ex.Message);
            }


        }

        private void BeginReceive()
        {
            try
            {
                //receiveDone.Reset();
                TCPClientStateObject state = new TCPClientStateObject();
                state.headerBufferSize = 9;
                state.TempReceiveBuffer = new byte[state.headerBufferSize];
                state.workSocket = tcpClient;
                tcpClient.BeginReceive(state.TempReceiveBuffer, 0, state.headerBufferSize, 0, new AsyncCallback(ReceiveCallback), state);
                //receiveDone.WaitOne();
            }
            catch (Exception ex)
            {
                this._connected = false;
                KConsole.Write(ErrorLevel.Serious, "Kernel>>AsynTCPClient>>BeginReceive>>", ex.Message);
            }

        }
        public void ReceiveCallback(IAsyncResult ar)
        {
            TCPClientStateObject state = null;
            bool closeSocket = false;

            try
            {
                state = (TCPClientStateObject)ar.AsyncState;
                Socket userClient = state.workSocket;
                int bytesRead = 0;

                //...判斷socket連線是否斷掉
                if (userClient == null || !userClient.Connected)
                {
                    closeSocket = true;
                    return;
                }

                bytesRead = userClient.EndReceive(ar);


                //if (state.IsAVStream == true)
                //{
                //    //state.receiveBuffer = new byte[bytesRead];
                //    //if (userClient.Connected)
                //    //{
                //    //    if (state.TempReceiveBuffer != null)
                //    //    {
                //    //        Array.Copy(state.TempReceiveBuffer, state.receiveBuffer, bytesRead);
                //    //        state.TempReceiveBuffer = null;
                //    //    }
                //    //    else
                //    //    {
                //    //        Array.Copy(state.AVStream, state.receiveBuffer, bytesRead);
                //    //    }
                //    //    GoToProcessAV(state);
                //    //    userClient.BeginReceive(state.AVStream, 0, state.AVStream.Length, 0, new AsyncCallback(ReceiveCallback), state);

                //    //    return;
                //    //}
                //    //else
                //    //{
                //    //    closeSocket = true;
                //    //    return;
                //    //}


                //    state.receiveBuffer = new byte[bytesRead];
                //    if (userClient.Connected)
                //    {          
                //        Array.Copy(state.TempReceiveBuffer, state.receiveBuffer, bytesRead);   
                //        GoToProcessAV(state);
                //        userClient.BeginReceive(state.TempReceiveBuffer, 0, state.TempReceiveBuffer.Length, 0, new AsyncCallback(ReceiveCallback), state);
                //        return;
                //    }
                //    else
                //    {
                //        closeSocket = true;
                //        return;
                //    }

                //}


                if (bytesRead > 0)
                {
                    //#region//...是否讀過 Header
                    //if (!state.isReadHeader)
                    //{
                    //    //...建立接收封包的存放位置
                    //    //string directoryName = "/" + Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetName().CodeBase).Remove(0, 6) + "/Process/";

                    //    //if (OSType != 0)
                    //    //{
                    //    //    directoryName = Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetName().CodeBase).Remove(0, 6) + "\\Process\\";
                    //    //}
                    //    //DirectoryInfo Createdir = new DirectoryInfo(directoryName);
                    //    //if (!Createdir.Exists)
                    //    //{
                    //    //    Createdir.Create();
                    //    //}
                    //    ////state.receiveFileTemporarily = directoryName + RandomSession.createSession(64);
                    //    //state.receiveFileTemporarily = directoryName + "cli_" + Guid.NewGuid().ToString();

                    //    using (MemoryStream ms_readheader = new MemoryStream(state.TempReceiveBuffer, 0, bytesRead))
                    //    {
                    //        BinaryReader br = new BinaryReader(ms_readheader);
                    //        state.ver = br.ReadByte();//...封包版本號
                    //        state.cmd1 = br.ReadInt16();//...命令1
                    //        state.cmd2 = br.ReadInt16();//...命令2
                    //        state.receivePackageTotalSize = br.ReadInt32();//...封包內容總長度

                    //        if (state.IsAVStream)
                    //        {
                    //            state.ListReceiveBuffer.AddRange(state.TempReceiveBuffer);
                    //        }
                    //        if (state.receivePackageTotalSize <= 0)
                    //        {

                    //            //...沒有夾帶內容
                    //            state.isReadHeader = false; //...讀取完畢將標記改為false
                    //            state.receiveBuffer = null;
                    //            //state.receiveBuffer = new byte[state.headerBufferSize]; //...Buffer 清空
                    //            state.TempReceiveBuffer = new byte[state.headerBufferSize];
                    //            if (state.IsAVStream)
                    //            {
                    //                GoToProcessAV(state);
                    //            }
                    //            else
                    //            {
                    //                ReceiveEvent(state);//...接收完畢號 處理事件 
                    //                if (state.cmd1 == 6004)
                    //                {
                    //                    state.IsAVStream = true;
                    //                    //state.TempReceiveBuffer = new byte[AVRecvDataMax];

                    //                }
                    //            }
            
                    //        }
                    //        else
                    //        {

                    //            //...當有夾帶內容時
                    //            state.TempReceiveBuffer = null;
                    //            state.receiveBuffer = null;
                    //            state.TempReceiveBuffer = new byte[state.receivePackageTotalSize];//... 080609 by randy
                    //            state.isReadHeader = true;//,,,將標記設定為讀過標頭,可以繼續接收資料                               
                    //        }
                    //    }

                    //    //...接收buffer的資料
                    //    if (userClient.Connected)
                    //    {
                    //         userClient.BeginReceive(state.TempReceiveBuffer, 0, state.TempReceiveBuffer.Length, 0, new AsyncCallback(ReceiveCallback), state);
                    //    }
                    //    else
                    //    {
                    //        this._connected = false;
                    //        closeSocket = true;
                    //    }
                    //    return;

                    //}
                    //#endregion

                    #region//...是否讀過 Header
                    if (!state.isReadHeader)
                    {

                        byte[] HeadReceiveBuffer = new byte[bytesRead];
                        Array.Copy(state.TempReceiveBuffer, HeadReceiveBuffer, bytesRead);
                        state.ListHeadBuffer.AddRange(HeadReceiveBuffer);
                        state.headerBufferSize = state.headerBufferSize - bytesRead;

                        if (state.headerBufferSize == 0)
                        {
                            byte[] TempHeadReceiveBuffer = state.ListHeadBuffer.ToArray();
                            state.ListHeadBuffer.Clear();
                            state.headerBufferSize = 9;
                            using (MemoryStream ms_readheader = new MemoryStream(TempHeadReceiveBuffer, 0, TempHeadReceiveBuffer.Length))
                            {

                                BinaryReader br = new BinaryReader(ms_readheader);
                                state.ver = br.ReadByte();//...封包版本號
                                state.cmd1 = br.ReadInt16();//...命令1
                                state.cmd2 = br.ReadInt16();//...命令2
                                state.receivePackageTotalSize = br.ReadInt32();//...封包內容總長度
                                if (state.receivePackageTotalSize <= 0)
                                {
                                    state.receiveBuffer = null;
                                }

                            }
                            if (state.IsAVStream)
                            {
                                state.ListReceiveBuffer.AddRange(TempHeadReceiveBuffer);
                            }
                            if (state.receivePackageTotalSize <= 0)
                            {

                                //...沒有夾帶內容
                                state.isReadHeader = false; //...讀取完畢將標記改為false                                
                                state.TempReceiveBuffer = new byte[state.headerBufferSize]; //...Buffer 清空
                                //state.receiveFileTemporarily = null;
                                if (state.IsAVStream)
                                {
                                    GoToProcessAV(state);
                                }
                                else
                                {
                                    ReceiveEvent(state);//...接收完畢號 處理事件 
                                    if (state.cmd1 == 6001)
                                    {
                                        state.IsAVStream = true;
                                        //state.TempReceiveBuffer = new byte[AVRecvDataMax];

                                    }
                                }
                            }
                            else
                            {
                                //...當有夾帶內容時
                                state.receiveBuffer = null;
                                state.TempReceiveBuffer = new byte[state.receivePackageTotalSize];//... 080609 by randy
                                state.isReadHeader = true;//,,,將標記設定為讀過標頭,可以繼續接收資料 
                            }


                        }
                        else if (state.headerBufferSize > 0)
                        {
                            //...繼續接收
                            state.TempReceiveBuffer = new byte[state.headerBufferSize];//--randy,2008.06.09                     
                        }
                        //...接收buffer的資料
                        if (userClient == null || !userClient.Connected)
                        {
                            closeSocket = true;
                            return;
                        }
                        if (userClient.Connected)
                        {
                            userClient.BeginReceive(state.TempReceiveBuffer, 0, state.TempReceiveBuffer.Length, 0, new AsyncCallback(ReceiveCallback), state);
                            return;
                        }
                        else
                        {
                            closeSocket = true;
                            return;
                        }


                    }
                    #endregion

                    #region//...儲存數據

                    byte[] ReceiveBuffer = new byte[bytesRead];
                    Array.Copy(state.TempReceiveBuffer, ReceiveBuffer, bytesRead);
                    //state.ListReceiveBuffer.AddRange(state.TempReceiveBuffer);
                    state.ListReceiveBuffer.AddRange(ReceiveBuffer);

                    state.receivePackageTotalSize = state.receivePackageTotalSize - bytesRead;//--randy,2008.06.09                    
                    if (state.receivePackageTotalSize == 0)
                    {
                        state.headerBufferSize = 9;
                        //...接收完畢
                        state.isReadHeader = false;//...讀取Header reset                        
                        state.receiveBuffer = state.ListReceiveBuffer.ToArray();
                        state.TempReceiveBuffer = new byte[state.headerBufferSize];

                        state.ListReceiveBuffer = new List<byte>();
 
                        //ReceiveEvent(state);//...接收完畢號 處理事件 
                        //if (state.cmd1 == 6004)
                        //{
                        //    state.IsAVStream = true;
                        //    state.TempReceiveBuffer = new byte[AVRecvDataMax];

                        //}

                        if (state.IsAVStream)
                        {
                            GoToProcessAV(state);
                        }
                        else
                        {
                            if (ReceiveEvent != null)
                            {
                                ReceiveEvent(state);//...接收完畢號 處理事件 
                            }
                            if (state.cmd1 == 6004)
                            {
                                state.IsAVStream = true;
                                //state.TempReceiveBuffer = new byte[AVRecvDataMax];

                            }
                        }
                        //receiveDone.Set();
                    }
                    else if (state.receivePackageTotalSize > 0)
                    {
                        //...繼續接收
                        state.TempReceiveBuffer = new byte[state.receivePackageTotalSize];//--randy,2008.06.09                     
                    }
                    #endregion

                    #region//...接收buffer的資料

                    if (userClient.Connected)
                    {
                        userClient.BeginReceive(state.TempReceiveBuffer, 0, state.TempReceiveBuffer.Length, 0, new AsyncCallback(ReceiveCallback), state);

                    }
                    else
                    {
                        this._connected = false;
                        closeSocket = true;
                    }
                    
                    #endregion
                    return;
                }
                else
                {
                    KConsole.Write(ErrorLevel.Warn, "Kernel>>AsynTCPClient>>ReadCallback>>", "接收到的數據長度<=0");
                    closeSocket = true;
                }
            }
            catch (Exception ex)
            {
                KConsole.Write(ErrorLevel.Serious, "Kernel>>AsynTCPClient>>ReceiveCallback>>", ex.Message);
                this._connected = false;
                closeSocket = true;
            }
            finally
            {
                if (closeSocket)
                {
                    if (ExceptionHandlingEvent != null)
                    {
                        ExceptionHandlingEvent(state);
                    }
                    //receiveDone.Set();
                    state = null;
                    //GC.Collect();
                }
            }
        }

        /// <summary>
        /// 音视频流接收处理
        /// </summary>
        /// <param name="state"></param>
        private void GoToProcessAV(TCPClientStateObject state)
        {
            this.AudioVideoReceiveEvent(state);
        }

        /// <summary>
        /// 数据接收类型切换
        /// </summary>
        /// <param name="state">切换对像</param>
        /// <param name="IsState">切换状态：false[控制流]；true[音视频流]</param>
        public void DateTypeReceive(TCPClientStateObject state, bool IsState)
        {
            //Socket userClient = state.workSocket;
            //state.IsAVStream = IsState;
            //state.cmd1 = 0;
            //state.cmd2 = 0;
            //state.AVStream = new byte[AVRecvDataMax];
            //userClient.BeginReceive(state.AVStream, 0, state.AVStream.Length, 0, new AsyncCallback(ReceiveCallback), state);
        }


    }
}
