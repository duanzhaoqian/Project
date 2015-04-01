using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Net.Sockets;
using System.Net;
using System.Collections;
using System.IO;
using System.Reflection;

namespace Kernel
{
    public class AsynTCPSocket:IDisposable
    {
        private ManualResetEvent _allDone = new ManualResetEvent(false);
        private string _listenerAddress = null;
        private int _listenerPort = 0;
        private int _pendingConnectionsQueue = 10000;
        private Socket _listenerSocket = null;
        private int AVRecvDataMax = 2097152;//音视频接收最大Buffer


       
        /// <summary>
        /// 接收到封包後續處理
        /// </summary>
        public event AsynchronousSocketListenerReceiveEvent ReceiveEvent;

        /// <summary>
        /// 音视频接收到封包後續處理
        /// </summary>
        public event AsynchronousSocketListenerReceiveEvent AudioVideoReceiveEvent;

        /// <summary>
        /// 異常處理
        /// </summary>
        public event AsynchronousServerExceptionHandlingEvent ExceptionHandlingEvent;

        public event SystemMonitorSocketReceiveEvent SystemMonitorReceiveEvent;

        public event ConnectCountEvent ConnectEventCount;

        public AsynTCPSocket()
        {
           
        }

        public AsynTCPSocket(int ConnectionsProcess)
        {
           
        }

        /// <summary>
        /// 操作系统类型值
        /// </summary>
        private int OSType = 0;
        /// <summary>
        /// 传入操作系统类型值
        /// </summary>
        /// <param name="OsType"></param>
        public AsynTCPSocket(string OsType)
        {
            try
            {
                OSType = Convert.ToInt32(OsType);
            }
            catch
            {
                OSType = 0;
            }
            finally
            {
                Tools.OSTYPE = OSType;
            }
        }


        /// <summary>
        /// 輸入請聽位置
        /// </summary>        
        public string listenerAddress
        {
            set
            {
                this._listenerAddress = value;
            }
            get
            {
                return this._listenerAddress;
            }
        }

        /// <summary>
        /// 設定傾聽端口
        /// </summary>
        public int listenerPort
        {
            set
            {
                this._listenerPort = value;
            }
            get
            {
                return this._listenerPort;
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


        //...判斷Listen Socket 是否已經綁定端口位置
        public bool IsBound
        {
            get
            {
                if (_listenerSocket != null)
                {
                    return _listenerSocket.IsBound;
                }
                else
                {
                    return false;
                }
            }
        }

        public void Start()
        {            
            try
            {
                //if (string.IsNullOrEmpty(_listenerAddress))
                //{
                //    //KConsole.Write(ErrorLevel.Serious, "Kernel>>AsynTCPSocket>>Start", "_listenerAddress is null");
                //    KConsole.Write(ErrorLevel.Response, "", "Kernel>>AsynTCPSocket>>Start: _listenerAddress is null");
                //    throw new ArgumentNullException();
                //}

                if (_listenerPort <= 0)
                {
       
                    KConsole.Write(ErrorLevel.Response, "", "Kernel>>AsynTCPSocket>>Start: _listenerPort <=0");
                    throw new ArgumentNullException();
                }
                IPEndPoint localEndPoint = null;
                if (string.IsNullOrEmpty(_listenerAddress))
                {
    
                    localEndPoint = new IPEndPoint(System.Net.IPAddress.Any, listenerPort);
     
                }
                else
                {
                    localEndPoint = new IPEndPoint(System.Net.IPAddress.Parse(listenerAddress), listenerPort);
 

                }
                _listenerSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                
                #region 端口复用 modify by whr 20100911未启用
                /*端口复用 modify by whr 20100911*/
                //try
                //{
                //    _listenerSocket.Bind(localEndPoint);
                //}
                //catch
                //{
                //    _listenerSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                //    _listenerSocket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
                //    _listenerSocket.Bind(localEndPoint);
                //    //return;
                //}
                /**********************************/
                #endregion

                _listenerSocket.Bind(localEndPoint);
                KConsole.Write(ErrorLevel.Response, "", "Kernel>>AsynTCPSocket>>Start>>Bind  the socket  and Listen>>" + _listenerAddress + ":" + listenerPort.ToString());
                _listenerSocket.Listen(_pendingConnectionsQueue);
                KConsole.Write(ErrorLevel.Response, "", "pendingConnectionsQueue: " + _pendingConnectionsQueue.ToString());
                Thread listen = new Thread(new ParameterizedThreadStart(Run));
                listen.Start(_listenerSocket);




            }
            catch (Exception ex)
            {
                KConsole.Write(ErrorLevel.Response, "", "Kernel>>AsynTCPSocket>>Start>>Exception:" + ex.Message);
                //KConsole.Write(ErrorLevel.Serious, "Kernel>>AsynTCPSocket>>Start", ex.Message);
                _listenerSocket = null;                
                //GC.Collect();
                //throw new ArgumentNullException();
            }
        }

        private void Run(object obj)
        {
            try
            {
                Socket server = (Socket)obj;
                while (true)
                {
                    _allDone.Reset();
                    KConsole.Write(ErrorLevel.Response, "", "Kernel>>AsynTCPSocket>>Run>>Waiting Accept...");                   
                    server.BeginAccept(new AsyncCallback(AcceptCallback), server);

                    if (OSType > 0)
                    {
                        #region demo
                        //server.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.HeaderIncluded, 1);
                        //uint dummy = 0;
                        ////Console.WriteLine("Step 1");
                        //byte[] inOptionValues = new byte[System.Runtime.InteropServices.Marshal.SizeOf(dummy) * 3];
                        ////Console.WriteLine("Step 2");
                        //BitConverter.GetBytes((uint)1).CopyTo(inOptionValues, 0);
                        ////Console.WriteLine("Step 3");
                        //BitConverter.GetBytes((uint)1000).CopyTo(inOptionValues, System.Runtime.InteropServices.Marshal.SizeOf(dummy));
                        ////Console.WriteLine("Step 4");
                        //BitConverter.GetBytes((uint)1000).CopyTo(inOptionValues, System.Runtime.InteropServices.Marshal.SizeOf(dummy) * 2);
                        ////Console.WriteLine("Step 5");
                        //server.IOControl(IOControlCode.KeepAliveValues, inOptionValues, null);
                        ////Console.WriteLine("Step 6");

                        ////server.IOControl(System.Net.Sockets.IOControlCode.KeepAliveValues, inOptionValues, null);

                        ////                    server.SetSocketOption(SocketOptionLevel.IP,
                        ////SocketOptionName.HeaderIncluded, 1);
                        ////Console.WriteLine("Step 1");
                        ////byte[] IN = new byte[4] { 1, 0, 0, 0 };
                        ////Console.WriteLine("Step 2");
                        ////byte[] OUT = new byte[4];
                        ////Console.WriteLine("Step 3");
                        ////int SIO_RCVALL = unchecked((int)0x98000001);
                        ////Console.WriteLine("Step 4");
                        ////int ret_code = server.IOControl(IOControlCode.KeepAliveValues, inOptionValues, OUT);
                        ////Console.WriteLine("Step 5");
                        ////ret_code = OUT[0] + OUT[1] + OUT[2] + OUT[3];
                        ////Console.WriteLine("Step 6");
                        ////server.IOControl(unchecked((int)0x98000001), new byte[4] { 1, 0, 0, 0 }, new byte[4]);
                        ////server.IOControl(IOControlCode.KeepAliveValues, null, null);
                        ////Console.WriteLine("Step 6");
                        #endregion
                    }
                    _allDone.WaitOne();
                }
            }
            catch (Exception ex)
            {
                _allDone.Set();
                KConsole.Write(ErrorLevel.Response,"", "Kernel>>AsynTCPSocket>>Run>>Exception:"+ex.Message);
            }

        }

        public void Stop()
        {
            try
            {
                if (_listenerSocket != null)
                {
                    try
                    {
                        _listenerSocket.Shutdown(SocketShutdown.Both);
                    }
                    catch { }
                    _listenerSocket.Close(3000);

                }  

            }
            catch (Exception ex)
            {
                KConsole.Write(ErrorLevel.Response, "", "Kernel>>AsynTCPSocket>>Stop>>Exception:" + ex.Message);
                throw new ArgumentNullException();
            }
            finally
            {
                _listenerSocket = null;                
                //GC.Collect();
            }
        }

        public void AcceptCallback(IAsyncResult ar)
        {
            
            StateObject state = null;
            try
            {
                _allDone.Set();
                Socket server = (Socket)ar.AsyncState;
                Socket newUser = server.EndAccept(ar); //...建立一個對應的連接
                //ar.AsyncWaitHandle.Close();
                //...設定接收及傳送的緩衝區
                //newUser.ReceiveBufferSize = 32768;
                //newUser.SendBufferSize = 32768;
                //...建立一個保存連線狀態的物件
                state = new StateObject();
                state.headerBufferSize = 9;//...設定封包讀取大小
                state.workSocket = newUser;//...連接進來的線路
                state.TempReceiveBuffer = new byte[state.headerBufferSize];

                //state.IsAVStream = false;
                //string wanip = state.wanIP;
                //string wanport = state.wanPort.ToString();
                //KConsole.Write(ErrorLevel.Debug, "Kernel>>AsynTCPSocket>>AcceptCallback>>連接進來的連線>>", wanip + ":" + wanport);               

                // 每一條連接進來的請求都建立ConnectionSession
                string ConnectionSession = RandomSession.createSession(32);
                state.ConnectionSession = ConnectionSession;
                KConsole.Write(ErrorLevel.Response, "", "Kernel>>AsynTCPSocket>>AcceptCallback>>ConnectionSession:" + ConnectionSession);
                //KConsole.Write(ErrorLevel.Debug, "Kernel>>AsynTCPSocket>>AcceptCallback>>建立ConnectionSession>>", ConnectionSession);
                //_allDone.Set();
                //...開始接收請求 ,先接收封包的標頭
                newUser.BeginReceive(state.TempReceiveBuffer, 0, state.headerBufferSize, 0, new AsyncCallback(ReadCallback), state);
                //ThreadPool.QueueUserWorkItem(new WaitCallback(DisconnectEvent), state);
                ConnectCount(state);

               
            }
            catch (Exception ex)//13641132799
            {
                _allDone.Set();
                _allDone.Reset();
                KConsole.Write(ErrorLevel.Response,"", "Kernel>>AsynTCPSocket>>AcceptCallback>>Exception:"+ex.Message);
                //KConsole.Write(ErrorLevel.Serious, "Kernel>>AsynTCPSocket>>AcceptCallback>>", ex.Message);

                if (state != null)
                {
                    ExceptionHandlingEvent(state);
                    state = null;
                    //GC.Collect();
                }              
            }
        }
        public void ReadCallback(IAsyncResult ar)
        {
            StateObject state = null;
            bool closeSocket = false;

            try
            {
                state = (StateObject)ar.AsyncState;
                Socket userClient = state.workSocket;
                int bytesRead = 0;
                
                //...判斷socket連線是否斷掉
                if (userClient == null || !userClient.Connected)
                {
                    closeSocket = true;
                    return;

                }
                bytesRead = userClient.EndReceive(ar);

                if (bytesRead==0)
                {
                    closeSocket = true;
                    

                    return;
                }


                //if (state.IsAVStream==true)
                //{

                //    state.receiveBuffer = new byte[bytesRead];
                //    if (userClient.Connected)
                //    {
                //        Array.Copy(state.TempReceiveBuffer, state.receiveBuffer, bytesRead);
                //        GoToProcessAV(state);
                //        userClient.BeginReceive(state.TempReceiveBuffer, 0, state.TempReceiveBuffer.Length, 0, new AsyncCallback(ReadCallback), state);
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
                                        GoToProcess(state);//...接收完畢號 處理事件 
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
                                userClient.BeginReceive(state.TempReceiveBuffer, 0, state.TempReceiveBuffer.Length, 0, new AsyncCallback(ReadCallback), state);
                                return;
                            }
                            else
                            {
                                closeSocket = true;
                                return;
                            }
            

                    }
                    #endregion

                    #region //...儲存數據
                  
                    //DiskIO.Save(state.receiveFileTemporarily, state.receiveBuffer, bytesRead);
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
                        state.TempReceiveBuffer = new byte[state.headerBufferSize]; //...recevice buffer reset
                        //state.TempReceiveBuffer = null;
                        state.receiveBuffer = state.ListReceiveBuffer.ToArray();
                        //state.ListReceiveBuffer = new List<byte>();
                        state.ListReceiveBuffer.Clear();
                        if (state.IsAVStream)
                        {
                            GoToProcessAV(state);
                            //if (state.cmd1 == 6005)
                            //{
                            //    Console.WriteLine("state.cmd1=6001" + state.receivePackageTotalSize.ToString());
                            //}
                        }
                        else
                        {
                            GoToProcess(state);//...接收完畢號 處理事件
                            if (state.cmd1 == 6001)
                            {
                                state.IsAVStream = true;
                                //state.TempReceiveBuffer = new byte[AVRecvDataMax];

                            }
                        }
                    }
                    else if (state.receivePackageTotalSize > 0)
                    {
                        //...繼續接收
                        state.TempReceiveBuffer = new byte[state.receivePackageTotalSize];//--randy,2008.06.09                     
                    }
                    #endregion

                    #region//...接收buffer的資料
                    //...接收buffer的資料
                    if (userClient == null || !userClient.Connected)
                    {
                        closeSocket = true;
                        return;
                    }
 
                    userClient.BeginReceive(state.TempReceiveBuffer, 0, state.TempReceiveBuffer.Length, 0, new AsyncCallback(ReadCallback), state);

                    #endregion
                }
                else
                {
                    //KConsole.Write(ErrorLevel.Response, "", "Kernel>>AsynTCPSocket>>ReadCallback>>Exception: 接收到的數據長度<=0");
                    //KConsole.Write(ErrorLevel.Warn, "Kernel>>AsynTCPSocket>>ReadCallback>>", "接收到的數據長度<=0");
                    closeSocket = true;
                }
            }
            catch (Exception ex)
            {
                //KConsole.Write(ErrorLevel.Serious, "Kernel>>AsynTCPSocket>>ReadCallback>>", ex.Message);

                KConsole.Write(ErrorLevel.Response, "", "Kernel>>AsynTCPSocket>>ReadCallback>>Exception:" + ex.Message);
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
                    state = null;
                    //GC.Collect();

                }
                

            }
        }

        /// <summary>
        /// 数据接收类型切换
        /// </summary>
        /// <param name="state">切换对像</param>
        /// <param name="IsState">切换状态：false[控制流]；true[音视频流]</param>
        public void DateTypeReceive(StateObject state,bool IsState)
        {
            //Socket userClient = state.workSocket;
            //state.IsAVStream = IsState;
            //state.cmd1 = 0;
            //state.cmd2 = 0;
            //state.IsObjState = true;
            ////state.TempReceiveBuffer = null;
            ////state.AVStream = new byte[AVRecvDataMax];
            ////userClient.BeginReceive(state.AVStream, 0, state.AVStream.Length, 0, new AsyncCallback(ReadCallback), state);
            ////try
            ////{
            ////    IAsyncResult ar = (IAsyncResult)state;
            ////    ReadCallback(ar);
            ////}
            ////catch (Exception ex)
            ////{
            ////    Console.WriteLine(ex.ToString());
            ////}

            //userClient.BeginReceive(state.TempReceiveBuffer, 0, state.TempReceiveBuffer.Length, 0, new AsyncCallback(ReadCallback), state);

        }

        private void DisconnectEvent(object _obj)

        {
            StateObject state = (StateObject)_obj;
            if (state.workSocket.Poll(-1,SelectMode.SelectRead))
            {
                ExceptionHandlingEvent(state);
            }
        }
        /// <summary>
        /// 控制信息流处理
        /// </summary>
        /// <param name="state"></param>
        private void GoToProcess(StateObject state)
        {
            this.ReceiveEvent(state);
        }
        /// <summary>
        /// 音视频流接收处理
        /// </summary>
        /// <param name="state"></param>
        private void GoToProcessAV(StateObject state)
        {
            //Console.WriteLine("GoToProcessAV>>state.receiveBuffer===" + state.receiveBuffer[0]);
            this.AudioVideoReceiveEvent(state);
        }

        /// <summary>
        /// 连接数量
        /// </summary>
        /// <param name="state"></param>
        private void ConnectCount(StateObject state)
        {
            this.ConnectEventCount(state);
        }


        ~AsynTCPSocket()
        {
            this.Dispose();
        }
        public void Dispose()
        {
            Stop();
        }
    }
}
