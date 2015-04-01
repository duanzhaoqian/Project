using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kernel;
using System.Data;

namespace MonitorClient.AppUtility
{
    public class CommonList
    {
        public static string OSTYPE = "1";
        public static string mySESSION = "";
        public static Kernel.AsynTCPClient MonClient;//=new AsynTCPClient(CommonList.OSTYPE);
        public static Model.ServerInfo CurrentServerInfo;// = new Model.ServerInfo();
        public CommonList()
        {

        }

        static CommonList()
        {
            try
            {
                if (t_servertypeReal == null)
                {
                    t_servertypeReal = new DataTable();
                }
                if (t_serverinfoReal == null)
                {
                    t_serverinfoReal = new DataTable();
                }
                if (t_monitinfo == null)
                {
                    t_monitinfo = new DataTable();
                }
                if (t_servertype == null)
                {
                    t_servertype = new DataTable();
                }
                if (t_serverinfo == null)
                {
                    t_serverinfo = new DataTable();
                }
                if (t_userinfo == null)
                {
                    t_userinfo = new DataTable();
                }
            }
            catch (Exception ex)
            {
                _ShowMsg(ex.StackTrace+ex.Message);
            }
        }

        #region 实时监控
        public static DataTable t_servertypeReal;// = new DataTable();
        public static DataTable t_serverinfoReal;// = new DataTable();
        #endregion


        public static DataTable t_monitinfo;// = new DataTable();
        public static DataTable t_servertype;// = new DataTable();
        public static DataTable t_serverinfo;// = new DataTable();

        public static DataTable t_userinfo;// = new DataTable();
        public static int Usercurrpage = 1;
        public static int Usertotalcount;
        public static int Usertotalpage;

        #region event
        public static event MonitorClient.CtrServerType.DataGridViewDataBind _DataGridViewDataBind;
        public static event MonitorClient.CtrUserinfo.DataGridViewDataBind _CtrUserinfoDataGridViewDataBind;
        public static event MonitorClient.FrMain.ShowMsg _ShowMsg;
        public static event CtrServerinfo.ReloadServerType _ReloadServerType;

        public static event MonitorClient.FrMain.TreeViewDataBind _TreeViewDataBind;
        public static event MonitorClient.FrMain.TreeViewDataBind2 _TreeViewDataBind2;
        public static event MonitorClient.CtrServerinfo.DataGridViewDataBind _ServerinfoDataGridViewDataBind;

        #endregion


        
        public static void Sett_servertype(DataTable dt)
        {
            try
            {
                if (t_servertype == null)
                {
                    t_servertype = new DataTable();
                }
                if (dt != null)
                {
                    t_servertype = dt;
                    if (_DataGridViewDataBind != null)
                    {
                        _DataGridViewDataBind(dt);
                    }
                    if (_ReloadServerType != null)
                    {
                        _ReloadServerType(dt);
                    }
                }

            }
            catch (Exception ex)
            {
                _ShowMsg(ex.Message);
            }
        }

        public static void Sett_userinfo(DataTable dt)
        {
            try
            {
                if (t_userinfo == null)
                {
                    t_userinfo = new DataTable();
                }
                if (dt != null)
                {
                    t_userinfo = dt;
                    if (_CtrUserinfoDataGridViewDataBind != null)
                    {
                        _CtrUserinfoDataGridViewDataBind(dt);
                    }
                }
            }
            catch (Exception ex)
            {
                _ShowMsg(ex.Message);
            }
        }


        public static void Sett_serverinfo(DataTable dt)
        {
            try
            {
                if (t_serverinfo == null)
                {
                    t_serverinfo = new DataTable();
                }
                if (dt != null)
                {
                    t_serverinfo = dt;
                    if (_ServerinfoDataGridViewDataBind != null)
                    {
                        _ServerinfoDataGridViewDataBind(dt);
                    }
                }
            }
            catch (Exception ex)
            {
                _ShowMsg(ex.Message);
            }
        }


        public static void Sett_servertypeReal(DataTable dt)
        {
            try
            {
                if (t_servertypeReal == null)
                {
                    t_servertypeReal = new DataTable();
                }
                if (dt != null)
                {
                    t_servertypeReal = dt;
                    //if (_TreeViewDataBind != null)
                    //{
                    //    _TreeViewDataBind(dt);
                    //}
                    //if (_ReloadServerType != null)
                    //{
                    //    _ReloadServerType(dt);
                    //}
                }
            }
            catch (Exception ex)
            {
                _ShowMsg(ex.Message);
            }
        }

        public static void Sett_serverinfoReal(DataTable dt)
        {
            try
            {
                if (t_serverinfoReal == null)
                {
                    t_serverinfoReal = new DataTable();
                }
                if (dt != null)
                {
                    t_serverinfoReal = dt;
                    //if (_TreeViewDataBind2 != null)
                    //{
                    //    _TreeViewDataBind2(dt);
                    //}
                    if (_TreeViewDataBind != null)
                    {
                        _TreeViewDataBind(dt);
                    }                  
                }
            }
            catch (Exception ex)
            {
                _ShowMsg(ex.Message);
            }
        }

        public static  DataTable getServerTable(int ServerType)
        {
            try
            {
                DataTable dt = t_serverinfoReal.Clone();
                DataRow[] rows = t_serverinfoReal.Select("  SERVTYPE='" + ServerType + "'");
                if (rows == null)
                    return null;
                if (rows.Length == 0)
                    return null;
                foreach (DataRow nrwo in rows)
                {
                    //dt.Rows.Add(nrwo);
                    dt.Rows.Add(nrwo.ItemArray);
                }
                return dt;


            }
            catch (Exception ex)
            {
                return null;
            }
        }


        public static DataTable getSingerServerTable(int ID)
        {
            try
            {
                DataTable dt = t_serverinfoReal.Clone();
                DataRow[] rows = t_serverinfoReal.Select("  ID='" + ID + "'");
                if (rows == null)
                    return null;
                if (rows.Length == 0)
                    return null;
                foreach (DataRow nrwo in rows)
                {
                    //dt.Rows.Add(nrwo);
                    dt.Rows.Add(nrwo.ItemArray);
                }
                return dt;


            }
            catch (Exception ex)
            {
                _ShowMsg(ex.Message);
                return null;
            }
        }
       
    }
}