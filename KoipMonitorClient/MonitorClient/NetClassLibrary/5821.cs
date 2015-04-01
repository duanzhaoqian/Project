using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kernel;
using System.Data;
using MonitorClient.AppUtility;
using System.IO;

namespace MonitorClient.NetClassLibrary
{
    class _5821
    {
       //public static event FrmLogin.HideThisForm _HideThisForm;
       //public static event FrmLogin.ShowMsgLog _ShowMsgLog;
       public static event FrMain.ShowMsg _ShowMsg;
       public static event CtrUserinfo.SetCurrentpage _SetCurrentpage;
       public static event CtrUserinfo.SetTotalSetPage _SetTotalSetPage;
       public static event CtrUserinfo.SetTotalCount _SetTotalCount;
       private static int currpage = 1;
       private static int totalcount;
       private static int totalpage;
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
                    // AppUtility.config.MessageBox("错误提示解包失败");
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
                byte[] xmldata = null;
                using (MemoryStream ms2 = new MemoryStream(request.receiveBuffer))
                {
                    using (BinaryReader br2 = new BinaryReader(ms2))
                    {
                        MonitorClient.AppUtility.CommonList.Usertotalpage = br2.ReadInt32();
                        MonitorClient.AppUtility.CommonList.Usercurrpage = br2.ReadInt32();
                        MonitorClient.AppUtility.CommonList.Usertotalcount = br2.ReadInt32();
                        //heads = br2.ReadBytes(heads.Length);
                        xmldata = br2.ReadBytes(request.receiveBuffer.Length-12);
                    }
                }

                _SetTotalCount(MonitorClient.AppUtility.CommonList.Usertotalcount);
                _SetTotalSetPage(MonitorClient.AppUtility.CommonList.Usertotalpage);
                _SetCurrentpage(MonitorClient.AppUtility.CommonList.Usercurrpage);
                
                
                //string str1 = System.Text.Encoding.UTF8.GetString(xmldata);
                //_ShowMsg(str1);
                DataTable ReadBodyData = new DataTable();
                MonitorClient.AppUtility.CommClass.ReadXML(xmldata, ref ReadBodyData);
                if (MonitorClient.AppUtility.CommonList.t_userinfo == null)
                {
                    MonitorClient.AppUtility.CommonList.t_userinfo = new DataTable();
                }
                MonitorClient.AppUtility.CommonList.Sett_userinfo(ReadBodyData.Copy());
                ReadBodyData.Clear();

                _ShowMsg("");

                //if (request.cmd2 == -116)
                //{
                //    _ShowMsgLog("登录验证失败，登录ID不存在，请输入正确的登录ID！");
                //    return;
                //}
                ////Tools.Decompress(request.receiveBuffer, ref ByteResultKIS);
                //if (ReadBodyData == null || ReadBodyData.Rows.Count == 0)
                //{
                //    _ShowMsgLog("获取登录验证数据失败，请联系管理员！");
                //    return;
                //}
                //CommonList.mySESSION = ReadBodyData.Rows[0]["SESSION"].ToString();
                ////if (_CloseFrmLogin != null)
                ////{
                ////    _CloseFrmLogin();
                ////}
                ////AppUtility.config.MessageBox("登录成功");
                //if (MonitorClient.AppUtility.CommonList.CurrentServerInfo == null)
                //    MonitorClient.AppUtility.CommonList.CurrentServerInfo = new Model.ServerInfo();
                //MonitorClient.AppUtility.CommonList.CurrentServerInfo.ip = request.wanIP;
                //MonitorClient.AppUtility.CommonList.CurrentServerInfo.port = request.wanPort;

                //if (_HideThisForm != null)
                //{
                //    _HideThisForm();
                //}
                //DataTable ReadBodyData = new DataTable();
                //MonitorClient.AppUtility.CommClass.ReadXML(request.receiveBuffer, ref ReadBodyData);
                
            }
            catch (Exception ex)
            {
                _ShowMsg(ex.Message);
                //if (!string.IsNullOrEmpty(request.receiveBuffer))
                //    ThreadPool.QueueUserWorkItem(new WaitCallback(DiskIO.Del), request.receiveBuffer);
            }
        }
    }
}
