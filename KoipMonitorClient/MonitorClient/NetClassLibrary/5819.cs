using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kernel;
using System.Data;
using MonitorClient.AppUtility;
using System.Collections;
using System.Threading;

namespace MonitorClient.NetClassLibrary
{
    class _5819
    {
       //public static event FrmLogin.HideThisForm _HideThisForm;
       //public static event FrmLogin.ShowMsgLog _ShowMsgLog;
       public static event FrMain.ShowMsg _ShowMsg;
        /// <summary>
        /// 
        /// </summary>
        public static void Procedure(TCPClientStateObject request)
        {
            try
            {
                if (request == null)
                    return;

                if (request.cmd2 == -101)
                {
                    _ShowMsg("错误提示解包失败");
                    return;
                }
                if (request.cmd2 == -500)
                {
                    _ShowMsg("服务端异常错误！");
                    return;
                }
                if (request.cmd2 == -119)
                {
                    _ShowMsg("查询无数据！");
                    return;
                }
                if (request.cmd2 == -112)
                {
                    _ShowMsg("用户删除失败！");
                    return;
                }
                
                //DataTable ReadBodyData = new DataTable();
                //MonitorClient.AppUtility.CommClass.ReadXML(request.receiveBuffer, ref ReadBodyData);
                //if (MonitorClient.AppUtility.CommonList.t_userinfo == null)
                //{
                //    MonitorClient.AppUtility.CommonList.t_userinfo = new DataTable();
                //}
                //MonitorClient.AppUtility.CommonList.Sett_userinfo(ReadBodyData.Copy());
                //ReadBodyData.Clear();

                _ShowMsg("删除用户成功");
                MonitorClient.AppUtility.Currentsocket _Currentsocket = new Currentsocket();
                //_Currentsocket.Model = null;
                _Currentsocket.Model = MonitorClient.AppUtility.CommonList.CurrentServerInfo;
                Hashtable hs = new Hashtable();
                hs[1] = MonitorClient.AppUtility.KOMCmd.skUser;
                hs[2] = MonitorClient.AppUtility.KOMCmd.Succeed;
                List<CommClass.TTable> ListTtable = new List<AppUtility.CommClass.TTable>();
                //Table属性内容
                AppUtility.CommClass.TTable Ttable = new AppUtility.CommClass.TTable();
                Ttable.FieldName = "USERID";
                Ttable.FieldValue = "";
                ListTtable.Add(Ttable);
                Ttable.FieldName = "PAGECOUNT";
                Ttable.FieldValue = "60";
                ListTtable.Add(Ttable);
                Ttable.FieldName = "CURRPAGE";
                Ttable.FieldValue = MonitorClient.AppUtility.CommonList.Usercurrpage.ToString();
                ListTtable.Add(Ttable);
                byte[] ByteResult = null;
                AppUtility.CommClass.TableToByteArry(ListTtable, ref ByteResult);
                //AppUtility._config.MessageBox("开始登录");
                hs[3] = ByteResult;
                _Currentsocket.SetSendData(hs);
                _Currentsocket.TcpClient = MonitorClient.AppUtility.CommonList.MonClient;
                //_Currentsocket.do_Job(null);
                ThreadPool.QueueUserWorkItem(new WaitCallback(_Currentsocket.do_Job), null);
                _ShowMsg("正在请求查询用户，请稍等……");


                
            }
            catch (Exception ex)
            {
                _ShowMsg(ex.Message);
                
            }
        }
    }
}
