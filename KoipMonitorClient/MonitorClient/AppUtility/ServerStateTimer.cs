using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Threading;

namespace MonitorClient.AppUtility
{
    public class ServerStateTimer
    {
        static System.Timers.Timer aTimer = null;
        public static event FrMain.ShowMsg _ShowMsg;

        public ServerStateTimer()
        {
 
        }
        /// <summary>
        /// 初始化Timer对象
        /// </summary>
        public static void ObjectTime(object obj)
        {
            try
            {
                aTimer = new System.Timers.Timer();
                aTimer.Elapsed += new System.Timers.ElapsedEventHandler(Execute);
                aTimer.Interval = 10000;//相隔多长时间跑一次3600000
                aTimer.AutoReset = true;
                aTimer.Enabled = true;
            }
            catch (Exception ex)
            {
                _ShowMsg(ex.StackTrace + "\r\n" + ex.Message);
            }
        }

        public static void Execute(Object source, System.Timers.ElapsedEventArgs e)
        {
            try
            {
                GetServertype(null);
                //ThreadPool.QueueUserWorkItem(new WaitCallback(GetServertype), null);
                //ThreadPool.QueueUserWorkItem(new WaitCallback(GetServerInfo), null);
            }
            catch (Exception ex)
            {
                _ShowMsg(ex.StackTrace + "\r\n" + ex.Message);
            }

        }

        public static void GetServertype(object sender)
        {
            try
            {               
                MonitorClient.AppUtility.Currentsocket _Currentsocket = new Currentsocket();
                _Currentsocket.Model = MonitorClient.AppUtility.CommonList.CurrentServerInfo;
                Hashtable hs = new Hashtable();
                hs[1] = MonitorClient.AppUtility.KOMCmd.skServertype2;
                hs[2] = MonitorClient.AppUtility.KOMCmd.Succeed;
                //List<CommClass.TTable> ListTtable = new List<AppUtility.CommClass.TTable>();
                ////Table属性内容
                //AppUtility.CommClass.TTable Ttable = new AppUtility.CommClass.TTable();
                //Ttable.FieldName = "SERVTYPEID";
                //Ttable.FieldValue = "";
                //ListTtable.Add(Ttable);
                //Ttable.FieldName = "SERVTYPENAME";
                //Ttable.FieldValue = "";
                //ListTtable.Add(Ttable);
                byte[] ByteResult = null;
                //AppUtility.CommClass.TableToByteArry(ListTtable, ref ByteResult);
                hs[3] = ByteResult;
                _Currentsocket.SetSendData(hs);
                _Currentsocket.TcpClient = MonitorClient.AppUtility.CommonList.MonClient;
                ThreadPool.QueueUserWorkItem(new WaitCallback(_Currentsocket.do_Job), null);
                _ShowMsg("正在查询设备类型，请稍等……");
            }
            catch (Exception ex)
            {
                _ShowMsg(ex.Message);
            }
        }

        public static void GetServerInfo(object sender)
        {
            try
            {
                MonitorClient.AppUtility.Currentsocket _Currentsocket = new Currentsocket();
                _Currentsocket.Model = MonitorClient.AppUtility.CommonList.CurrentServerInfo;
                Hashtable hs = new Hashtable();
                hs[1] = MonitorClient.AppUtility.KOMCmd.skServer2;
                hs[2] = MonitorClient.AppUtility.KOMCmd.Succeed;
                //List<CommClass.TTable> ListTtable = new List<AppUtility.CommClass.TTable>();
                ////Table属性内容
                //AppUtility.CommClass.TTable Ttable = new AppUtility.CommClass.TTable();
                //Ttable.FieldName = "SERVTYPEID";
                //Ttable.FieldValue = "";
                //ListTtable.Add(Ttable);
                //Ttable.FieldName = "SERVTYPENAME";
                //Ttable.FieldValue = "";
                //ListTtable.Add(Ttable);
                byte[] ByteResult = null;
                //AppUtility.CommClass.TableToByteArry(ListTtable, ref ByteResult);
                hs[3] = ByteResult;
                _Currentsocket.SetSendData(hs);
                _Currentsocket.TcpClient = MonitorClient.AppUtility.CommonList.MonClient;
                ThreadPool.QueueUserWorkItem(new WaitCallback(_Currentsocket.do_Job), null);
                _ShowMsg("正在查询设备信息，请稍等……");
            }
            catch (Exception ex)
            {
                _ShowMsg(ex.Message);
            }
        }

    }
}
