using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kernel;
using System.Threading;
using System.Timers;
using System.IO;
using System.Collections;

namespace MonitorClient.AppUtility
{
    public class Currentsocket
    {
        public static event FrmLogin.ShowMsgLog _ShowMsgLog;
        public static event FrMain.ShowMsg _ShowMsg;
        private int ConnectNum = 0;
        private int waitNum = 0;
        private bool isTcpClient = false;

        //private int DelayTime = 0;
        private AsynTCPClient tcpClient;
        private Model.ServerInfo model;
        //StateObject _state = null;
        //DateTime startime = DateTime.Now;
       // private System.Timers.Timer _timer_job1;
       // private int Interval = 10000;
        private byte[] sendData = null;

        public byte[] SendData
        {
            set { this.sendData = value; }
            get { return this.sendData; }
        }
        public int WaitNum
        {
            set
            {

                this.waitNum = value;
            }
            get
            {
                return this.waitNum;
            }
        }
        public AsynTCPClient TcpClient
        {
            set
            {
                //if (value == null)
                //{
                //    value = new AsynTCPClient("1");
                //}
                this.tcpClient = value;
                this.isTcpClient = true;
            }
            get
            {
                return this.tcpClient;
            }
        }
        public Model.ServerInfo Model
        {
            set
            {
                this.model = new Model.ServerInfo();
                this.model = value;
            }
            get
            {
                return this.model;
            }
        }
        /// <summary>
        /// 停止
        /// </summary>
        public void StopTcpClient()
        {
            if (this.TcpClient.Connected)
            {
                try
                {
                    this.TcpClient.tcpClient.Close();
                    this.TcpClient.Stop();
                }
                catch (Exception ex)
                {
                    this.TcpClient.Stop();
                }
                finally
                {
                    //tcpClient = null;
                }

            }
            else
            {
                //tcpClient = null;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public Currentsocket()
        {
            if (tcpClient == null)
            {
                tcpClient = new AsynTCPClient(CommonList.OSTYPE);
                
                //HandshakeByte(ref this.sendData);
            }
            ReceiveEventReload(null);
        }

        public void SetSendData(object obj)
        {
            try
            {
                Hashtable hs = (Hashtable)obj;
                byte[] ByteResult = (byte[])hs[3];
                using (MemoryStream ms = new MemoryStream())
                {
                    using (BinaryWriter bw = new BinaryWriter(ms))
                    {
                        //string StrSend = "|6|1|1|11|";
                        //char[] charBuffer = StrSend.ToCharArray();
                        //byte[] bytearry = null;
                        //string CmdPath = Application.StartupPath + "\\Cmd\\";
                        //bytearry = File.ReadAllBytes(CmdPath + "SongFile.xml");
                        byte ver = 1;// KoipManage.Cmdinfo.CommCmd.MsgVer;
                        short cmd1 = (short)hs[1];//KoipManage.Cmdinfo.OMSCmd.ReqUserLogin;
                        short cmd2 = (short)hs[2];
                        int packagelength;
                        if (ByteResult == null)
                        {
                            packagelength = 0;
                        }
                        else
                        {

                            packagelength=ByteResult.Length;
                        }
                        bw.Write(ver);
                        bw.Write(cmd1);
                        bw.Write(cmd2);
                        bw.Write(packagelength);
                        if (packagelength != 0)
                        {
                            bw.Write(ByteResult);
                        }
                        bw.Flush();
                        this.SendData = ms.ToArray();
                        //while (true)
                        //{
                        //tcpClient.BeginSend(ms.ToArray());
                        //}
                        //Hashtable _hashtable_Package = new Hashtable();
                        //_hashtable_Package.Add("1", ms.ToArray());//...连結位置
                        //ThreadPool.QueueUserWorkItem(new WaitCallback(SongFile), _hashtable_Package);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        private void ReceiveEventReload(object obj)
        {
            try
            {
                tcpClient.ReceiveEvent -= new AsynTCPClientReceiveEvent(MonitorClient.NetClassLibrary.tcpClient_ReceiveEvent.ReceiveEvent);

                tcpClient.ReceiveEvent += new AsynTCPClientReceiveEvent(MonitorClient.NetClassLibrary.tcpClient_ReceiveEvent.ReceiveEvent);


                tcpClient.ExceptionHandlingEvent -= new AsynClientExceptionHandlingEvent(MonitorClient.NetClassLibrary.tcpClient_ExceptionHandlingEvent.ReceiveEvent);                
                tcpClient.ExceptionHandlingEvent += new AsynClientExceptionHandlingEvent(MonitorClient.NetClassLibrary.tcpClient_ExceptionHandlingEvent.ReceiveEvent);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        //public DateTime STARTIME
        //{
        //    set { this.startime = value; }
        //    get { return this.startime; }
        //}


        public void ConectServer(object obj)
        {
            try
            {
                //ConnectNum = 0; 
                //_ShowMsgLog("正在进行连接，请稍等……" + ConnectNum);
                if (tcpClient == null)
                {
                    tcpClient = new AsynTCPClient(CommonList.OSTYPE);
                    ReceiveEventReload(null);
                    tcpClient.targetAddress = Model.ip;
                    tcpClient.targetPort = Model.port;
                }
                if (!tcpClient.Connected)
                {
                    //tcpClient.Stop();
                    tcpClient.targetAddress = Model.ip;
                    tcpClient.targetPort = Model.port;
                    tcpClient.BeginConnect(tcpClient.targetAddress, tcpClient.targetPort);
                    //LogClassLibrary.ConsoleManage.Write(LogClassLibrary.ErrorLevel.Errors, "NetClassLibrary>ConectServer", "tcpClient.BeginConnect" + ConnectNum);
                }
                Thread.Sleep(500);
                while (!tcpClient.Connected)
                {
                    Thread.Sleep(500);
                    ConnectNum++;
                    if (ConnectNum > 5)
                    {
                        //
                        if (_ShowMsgLog != null)
                        _ShowMsgLog("连接服务器超时，请重试！" + ConnectNum + tcpClient.Connected);
                        if (_ShowMsg != null)
                        _ShowMsg("连接服务器超时，请重试！" + ConnectNum + tcpClient.Connected);
                        ConnectNum = 0;
                        break;
                    }
                    else
                    {
                        tcpClient.BeginConnect(tcpClient.targetAddress, tcpClient.targetPort);
                        if (_ShowMsgLog != null)
                        _ShowMsgLog("正在进行连接，请稍等…… " + ConnectNum + " 次");
                        if (_ShowMsg != null)
                        _ShowMsg("正在进行连接，请稍等…… " + ConnectNum + " 次");
                        ////ConnectNum++;      
                        //if (ConnectNum <= 3)
                        //{
                        //    ConectServer(obj);
                        //}
                    }
                   
                }
            }
            catch (Exception ex)
            {
                _ShowMsg(ex.Message);
               // LogClassLibrary.ConsoleManage.Write(LogClassLibrary.ErrorLevel.Errors, "NetClassLibrary>ConectServer", "" + ex.Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Data"></param>
        public void do_Job(object Data)
        {
            try
            {
                #region aa
               
                //List<CommClass.TTable> ListTtable = new List<AppUtility.CommClass.TTable>();
                ////Table属性内容
                //AppUtility.CommClass.TTable Ttable = new AppUtility.CommClass.TTable();
                //Ttable.FieldName = "USERID";
                //Ttable.FieldValue = "207154";
                //ListTtable.Add(Ttable);
                //Ttable.FieldName = "USERPWD";
                //Ttable.FieldValue = "xw";
                //ListTtable.Add(Ttable);
                //byte[] ByteResult = null;
                //AppUtility.CommClass.TableToByteArry(ListTtable, ref ByteResult);

                //using (MemoryStream ms = new MemoryStream())
                //{
                //    using (BinaryWriter bw = new BinaryWriter(ms))
                //    {
                //        //string StrSend = "|6|1|1|11|";
                //        //char[] charBuffer = StrSend.ToCharArray();
                //        //byte[] ByteResult = null;
                //        //string CmdPath = Application.StartupPath + "\\Cmd\\";
                //        //bytearry = File.ReadAllBytes(CmdPath + "SongFile.xml");
                //        byte ver = 1;// KoipManage.Cmdinfo.CommCmd.MsgVer;
                //        short cmd1 = (short)5807;//KoipManage.Cmdinfo.OMSCmd.ReqUserLogin;
                //        short cmd2 = (short)0;
                //        int packagelength = ByteResult.Length;
                //        bw.Write(ver);
                //        bw.Write(cmd1);
                //        bw.Write(cmd2);
                //        bw.Write(packagelength);
                //        bw.Write(ByteResult);
                //        bw.Flush();
                //        //this.SendData = ms.ToArray();
                //         if (tcpClient.Connected)
                //{
                //        //{
                //        tcpClient.BeginSend(ms.ToArray());
                //}
                //        //Hashtable _hashtable_Package = new Hashtable();
                //        //_hashtable_Package.Add("1", ms.ToArray());//...连結位置
                //        //ThreadPool.QueueUserWorkItem(new WaitCallback(SongFile), _hashtable_Package);
                //    }
                //}
                #endregion

                ConectServer(null);
                if (tcpClient.Connected)
                {
                    #region aa

                    //List<CommClass.TTable> ListTtable = new List<AppUtility.CommClass.TTable>();
                    ////Table属性内容
                    //AppUtility.CommClass.TTable Ttable = new AppUtility.CommClass.TTable();
                    //Ttable.FieldName = "USERID";
                    //Ttable.FieldValue = "207154";
                    //ListTtable.Add(Ttable);
                    //Ttable.FieldName = "USERPWD";
                    //Ttable.FieldValue = "xw";
                    //ListTtable.Add(Ttable);
                    //byte[] ByteResult = null;
                    //AppUtility.CommClass.TableToByteArry(ListTtable, ref ByteResult);

                    //using (MemoryStream ms = new MemoryStream())
                    //{
                    //    using (BinaryWriter bw = new BinaryWriter(ms))
                    //    {
                    //        //string StrSend = "|6|1|1|11|";
                    //        //char[] charBuffer = StrSend.ToCharArray();
                    //        //byte[] ByteResult = null;
                    //        //string CmdPath = Application.StartupPath + "\\Cmd\\";
                    //        //bytearry = File.ReadAllBytes(CmdPath + "SongFile.xml");
                    //        byte ver = 1;// KoipManage.Cmdinfo.CommCmd.MsgVer;
                    //        short cmd1 = (short)5807;//KoipManage.Cmdinfo.OMSCmd.ReqUserLogin;
                    //        short cmd2 = (short)0;
                    //        int packagelength = ByteResult.Length;
                    //        bw.Write(ver);
                    //        bw.Write(cmd1);
                    //        bw.Write(cmd2);
                    //        bw.Write(packagelength);
                    //        bw.Write(ByteResult);
                    //        bw.Flush();
                    //        //this.SendData = ms.ToArray();
                    //        if (tcpClient.Connected)
                    //        {
                    //            //{
                    //            tcpClient.BeginSend(ms.ToArray());
                    //        }
                    //        //Hashtable _hashtable_Package = new Hashtable();
                    //        //_hashtable_Package.Add("1", ms.ToArray());//...连結位置
                    //        //ThreadPool.QueueUserWorkItem(new WaitCallback(SongFile), _hashtable_Package);
                    //    }
                    //}
                    #endregion
                    if (this.isTcpClient)
                    {
                        MonitorClient.AppUtility.CommonList.MonClient = tcpClient;
                    }
                    tcpClient.BeginSend(this.SendData);
                    //if(_ShowMsg!=null)
                    //    _ShowMsg("数据已发送" + System.Text.Encoding.UTF8.GetString(this.SendData));
                    //WaitNum

                    //Thread.Sleep(500);
                    //KOIPMON.DALMySqlData.t_serverinfo.spu_t_serverinfo_update_state(this.T_serverinfo);
                    
                    //Console.WriteLine(model.NAME + "成功发送握手监控 DelayTime=" + DelayTime + "....IP=" + tcpClient.targetAddress.ToString() + ":" + tcpClient.targetPort.ToString() + " " + DateTime.Now);
                }
                //else
                //{
                //    ConectServer(null);
                //    if (tcpClient.Connected)
                //    {
                //        #region aa

                //        //List<CommClass.TTable> ListTtable = new List<AppUtility.CommClass.TTable>();
                //        ////Table属性内容
                //        //AppUtility.CommClass.TTable Ttable = new AppUtility.CommClass.TTable();
                //        //Ttable.FieldName = "USERID";
                //        //Ttable.FieldValue = "207154";
                //        //ListTtable.Add(Ttable);
                //        //Ttable.FieldName = "USERPWD";
                //        //Ttable.FieldValue = "xw";
                //        //ListTtable.Add(Ttable);
                //        //byte[] ByteResult = null;
                //        //AppUtility.CommClass.TableToByteArry(ListTtable, ref ByteResult);

                //        //using (MemoryStream ms = new MemoryStream())
                //        //{
                //        //    using (BinaryWriter bw = new BinaryWriter(ms))
                //        //    {
                //        //        //string StrSend = "|6|1|1|11|";
                //        //        //char[] charBuffer = StrSend.ToCharArray();
                //        //        //byte[] ByteResult = null;
                //        //        //string CmdPath = Application.StartupPath + "\\Cmd\\";
                //        //        //bytearry = File.ReadAllBytes(CmdPath + "SongFile.xml");
                //        //        byte ver = 1;// KoipManage.Cmdinfo.CommCmd.MsgVer;
                //        //        short cmd1 = (short)5807;//KoipManage.Cmdinfo.OMSCmd.ReqUserLogin;
                //        //        short cmd2 = (short)0;
                //        //        int packagelength = ByteResult.Length;
                //        //        bw.Write(ver);
                //        //        bw.Write(cmd1);
                //        //        bw.Write(cmd2);
                //        //        bw.Write(packagelength);
                //        //        bw.Write(ByteResult);
                //        //        bw.Flush();
                //        //        //this.SendData = ms.ToArray();
                //        //        if (tcpClient.Connected)
                //        //        {
                //        //            //{
                //        //            tcpClient.BeginSend(ms.ToArray());
                //        //        }
                //        //        //Hashtable _hashtable_Package = new Hashtable();
                //        //        //_hashtable_Package.Add("1", ms.ToArray());//...连結位置
                //        //        //ThreadPool.QueueUserWorkItem(new WaitCallback(SongFile), _hashtable_Package);
                //        //    }
                //        //}
                //        #endregion
                //        tcpClient.BeginSend(this.SendData);

                //    }
                //    else
                //    {


                //    }
                //}

              
            }
            catch (Exception ex)
            {
                Console.WriteLine("Kernel.SocketManager.SocketInfo.do_Job1" + ex.Message);
                throw ex;
            }



        }


        //public void Stop()
        //{
        //    try
        //    {
        //        if (_timer_job1 != null)
        //        {
        //            _timer_job1.Stop();
        //            _timer_job1 = null;
        //        }

        //    }
        //    catch (Exception ex)
        //    {

        //    }

        //}
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
                    short cmd1 = 1018;// CommCmd.Handshake;
                    short cmd2 = 0;// ErrCommon.Success; //0;
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


        #region 计算日期时间差
        /// <summary>
        /// 计算日期时间差
        /// </summary>
        /// <param name="DateTime1"></param>
        /// <param name="DateTime2"></param>
        /// <returns></returns>
        public static TimeSpan DateDiff(DateTime DateTime1, DateTime DateTime2)
        {
            //string dateDiff = null;

            TimeSpan ts1 = new TimeSpan(DateTime1.Ticks);
            TimeSpan ts2 = new TimeSpan(DateTime2.Ticks);
            TimeSpan ts = ts1.Subtract(ts2).Duration();
            return ts;
            //dateDiff = ts.Days.ToString() + "天"
            //    + ts.Hours.ToString() + "小时"
            //    + ts.Minutes.ToString() + "分钟"
            //    + ts.Seconds.ToString() + "秒"
            //    + ts.Milliseconds.ToString() + "毫秒";

            //return dateDiff;
        }
        #endregion


        /// <summary>
        /// 
        /// </summary>
        /// <param name="ts"></param>
        private void killSocket(TimeSpan ts)
        {
            //try
            //{

            //    if (ts.Minutes > 1)
            //    {
            //        Stop();
            //        new SocketInfo().del(this.STATE);
            //    }
            //    else
            //    {
            //        if (ts.Seconds > 40)
            //        {
            //            Stop();
            //            new SocketInfo().del(this.STATE);
            //        }

            //    }
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine("Kernel.SocketManager.SocketInfo.killSocket" + ex.Message);
            //}
        }




    }
}
