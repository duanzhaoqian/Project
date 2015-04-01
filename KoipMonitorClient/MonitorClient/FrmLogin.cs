using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using MonitorClient.AppUtility;
using System.IO;
using System.Collections;

namespace MonitorClient
{
    public partial class FrmLogin : Form
    {
        public delegate void HideThisForm();
        public delegate void ShowMsgLog(string Msg);
        public static event CtrMain.ReloadData _ReloadData;
        public static bool isexist=true;
        public FrmLogin()
        {
            InitializeComponent();
            FrmServerInfo._ReloadData += new ReloadData(FormServerInfo__ReloadData);
            FrmServerInfo._ReloadDataSelect += new ReloadDataSelect(FormServerInfo__ReloadDataSelect);
             MonitorClient.NetClassLibrary._5808._HideThisForm+=new HideThisForm(_5808__HideThisForm);
            MonitorClient.NetClassLibrary._5808._ShowMsgLog+=new ShowMsgLog(_5808__ShowMsgLog);
            MonitorClient.AppUtility.Currentsocket._ShowMsgLog += new ShowMsgLog(_5808__ShowMsgLog);

        }

        private void FrmLogin_Load(object sender, EventArgs e)
        {
            try
            {
                FormServerInfo__ReloadData();
                //this.toolStripStatusLabel1.Text = "dddddddddddd";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void _5808__ShowMsgLog(string msg)
        {
            this.toolStripStatusLabel1.Text = msg;// "dddddddddddd";
        }
        private void FormServerInfo__ReloadData()
        {
            ThreadPool.QueueUserWorkItem(new WaitCallback(CmbServerDropListDataBind), null);
        }
        private void CmbServerDropListDataBind(object obj)
        {
            try
            {
                if (!File.Exists(localserverinfo.directoryName + "serverinfo.dll"))
                    return;
                DataTable dt = new DataTable("serverinfo");
                dt.ReadXml(localserverinfo.directoryName + "serverinfo.dll");
                _MyDropListDataBind(dt, this.cmb_Server, "sname", "ID");               
            }
            catch (Exception ex)
            {
                AppUtility.config.MessageBox(ex.Message);
            }
        }

        private void FormServerInfo__ReloadDataSelect(int id)
        {
            this.cmb_Server.SelectedValue = id;
        }
        public void _MyDropListDataBind(DataTable dt, ComboBox boxtemp, string strtext, string strvalue)
        {
            try
            {
                if (!boxtemp.InvokeRequired)//判断是否需要进行唤醒的请求，如果控件与主线程在一个线程内，可以写成if(!InvokeRequired)
                {
                    //MessageBox.Show("同一线程内");
                    boxtemp.DataSource = dt;
                    boxtemp.DisplayMember = strtext;// "areasing_name";
                    boxtemp.ValueMember = strvalue;// "areasingno";
                }
                else
                {
                    //MessageBox.Show("不是同一个线程");
                    MyDropListDataBind a1 = new MyDropListDataBind(_MyDropListDataBind);
                    Invoke(a1, new object[] { dt, boxtemp, strtext, strvalue });//执行唤醒操作
                }
            }
            catch (Exception ex)
            {
                AppUtility.config.MessageBox(ex.Message);
            }
            finally
            {
                //closegif();

            }
        }

       
        private void FrmLogin_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (FrmLogin.isexist)
            {
                Application.Exit();
            }
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            this.login(null);//csc关闭窗体
            //try
            //{
               

            //    if (string.IsNullOrEmpty(this.tb_UserID.Text))
            //    {
            //        AppUtility.config.MessageBox("输入用户ID！");
            //        this.tb_UserID.Focus();
            //        return;
            //    }
            //    //_ServerInfo.ip = this.tb_UserID.Text;
            //    if (string.IsNullOrEmpty(this.tb_Pwd.Text))
            //    {
            //        AppUtility.config.MessageBox("输入密码！");
            //        this.tb_Pwd.Focus();
            //        return;
            //    }
            //    if (string.IsNullOrEmpty(this.cmb_Server.SelectedValue.ToString()))
            //    {
            //        AppUtility.config.MessageBox("请选择登录服务器！");
            //        this.cmb_Server.Focus();
            //        return;
            //    }
            //    MonitorClient.AppUtility.Currentsocket _Currentsocket = new Currentsocket();
            //    //_Currentsocket.Model = null;
            //    Model.ServerInfo _ServerInfo = new Model.ServerInfo();


            //    _ServerInfo = MonitorClient.AppUtility.config.getServerInfo(Convert.ToInt32(this.cmb_Server.SelectedValue.ToString()));
            //    if (_ServerInfo == null)
            //    {
            //        AppUtility.config.MessageBox("请选择登录服务器！");
            //        this.cmb_Server.Focus();
            //        return;
            //    } 
            //    _Currentsocket.Model = _ServerInfo;
            //    Hashtable hs = new Hashtable();
            //    hs[1] = MonitorClient.AppUtility.KOMCmd.CheckPwd;
            //    hs[2] = MonitorClient.AppUtility.KOMCmd.Succeed;

            //    List<CommClass.TTable> ListTtable = new List<AppUtility.CommClass.TTable>();
            //    //Table属性内容
            //    AppUtility.CommClass.TTable Ttable = new AppUtility.CommClass.TTable();
            //    Ttable.FieldName = "USERID";
            //    Ttable.FieldValue = this.tb_UserID.Text;
            //    ListTtable.Add(Ttable);
            //    Ttable.FieldName = "USERPWD";
            //    Ttable.FieldValue = this.tb_Pwd.Text;
            //    ListTtable.Add(Ttable);
            //    byte[] ByteResult = null;
            //    AppUtility.CommClass.TableToByteArry(ListTtable, ref ByteResult);
            //    //AppUtility._config.MessageBox("开始登录");
            //    hs[3] = ByteResult;
            //    _Currentsocket.SetSendData(hs);
            //    _Currentsocket.TcpClient = MonitorClient.AppUtility.CommonList.MonClient;
            //    //_Currentsocket.do_Job(null);
            //    ThreadPool.QueueUserWorkItem(new WaitCallback(_Currentsocket.do_Job), null);
            //    _5808__ShowMsgLog("正在进行登录验证，请稍等……");


            //}
            //catch (Exception ex)
            //{
            //    AppUtility.config.MessageBox(ex.Message);
            //}


          
          
        }

        public void _5808__HideThisForm()
        {
            try
            {
                if (!InvokeRequired)
                {
                    //aTimer.AutoReset = false;
                    //aTimer.Enabled = false;

                    //ClsCommon.UserID =this.tb_UserID.Text.ToString().Trim();
                    //KoipManage.ClsCommon.UserID = Convert.ToInt32(dt.Rows[0]["ID"].ToString().Trim());
                    //FrmLogin.isexist = false;
                    //this.Close();

                    FrmLogin.isexist = false;
                    this.Close();
                    FrMain.frmthis.loadMain(null);
                    FrMain.frmthis.Show();
                    //FrMain.frmthis.Dock = DockStyle.None;
                    //FrMain.frmthis.ShowInTaskbar = true;
                    FrMain.frmthis.TopMost = true;
                    //FrMain.frmthis.RolesInit();

                    //if (_ReloadData != null)
                    //{
                    //    _ReloadData("0");
                    //}
                   


                }
                else
                {
                    //MessageBox.Show("不是同一个线程");
                    HideThisForm a1 = new HideThisForm(_5808__HideThisForm);
                    Invoke(a1, new object[] { });//执行唤醒操作
                }
            }
            catch (Exception ex)
            {
                AppUtility.config.MessageBox("11111111111111111111111" + ex.Message);
            }

        }
        private void _5807__CloseFrmLogin()
        {
            try
            {
                //_CloseFrmLogin();
                //FrmLogin.isexist = false;
                //this.Close();
                //FrMain.frmthis.Show();
                ////FrMain.frmthis.Dock = DockStyle.None;
                ////FrMain.frmthis.ShowInTaskbar = true;
                //FrMain.frmthis.TopMost = true;
                ////FrMain.frmthis.RolesInit();
            }
            catch (Exception ex)
            {
                AppUtility.config.MessageBox(ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                Application.Exit();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                FrmServerInfo _FrmServerInfo = new FrmServerInfo();
                _FrmServerInfo.Show();
                _FrmServerInfo.TopMost = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        protected override bool ProcessCmdKey(ref System.Windows.Forms.Message msg, System.Windows.Forms.Keys keyData) //激活回车键
        {
            int WM_KEYDOWN = 256;
            int WM_SYSKEYDOWN = 260;
            if (msg.Msg == WM_KEYDOWN | msg.Msg == WM_SYSKEYDOWN)
            {
                switch (keyData)
                {
                    case Keys.Enter:
                        this.login(null);//csc关闭窗体
                        break;
                }

            }
            return false;
        }


        protected void login(object obj)
        {
            try
            {


                if (string.IsNullOrEmpty(this.tb_UserID.Text))
                {
                    AppUtility.config.MessageBox("输入用户ID！");
                    this.tb_UserID.Focus();
                    return;
                }
                //_ServerInfo.ip = this.tb_UserID.Text;
                if (string.IsNullOrEmpty(this.tb_Pwd.Text))
                {
                    AppUtility.config.MessageBox("输入密码！");
                    this.tb_Pwd.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(this.cmb_Server.SelectedValue.ToString()))
                {
                    AppUtility.config.MessageBox("请选择登录服务器！");
                    this.cmb_Server.Focus();
                    return;
                }
                MonitorClient.AppUtility.Currentsocket _Currentsocket = new Currentsocket();
                //_Currentsocket.Model = null;
                Model.ServerInfo _ServerInfo = new Model.ServerInfo();


                _ServerInfo = MonitorClient.AppUtility.config.getServerInfo(Convert.ToInt32(this.cmb_Server.SelectedValue.ToString()));
                if (_ServerInfo == null)
                {
                    AppUtility.config.MessageBox("请选择登录服务器！");
                    this.cmb_Server.Focus();
                    return;
                }
                _Currentsocket.Model = _ServerInfo;
                Hashtable hs = new Hashtable();
                hs[1] = MonitorClient.AppUtility.KOMCmd.CheckPwd;
                hs[2] = MonitorClient.AppUtility.KOMCmd.Succeed;

                List<CommClass.TTable> ListTtable = new List<AppUtility.CommClass.TTable>();
                //Table属性内容
                AppUtility.CommClass.TTable Ttable = new AppUtility.CommClass.TTable();
                Ttable.FieldName = "USERID";
                Ttable.FieldValue = this.tb_UserID.Text;
                ListTtable.Add(Ttable);
                Ttable.FieldName = "USERPWD";
                Ttable.FieldValue = this.tb_Pwd.Text;
                ListTtable.Add(Ttable);
                byte[] ByteResult = null;
                AppUtility.CommClass.TableToByteArry(ListTtable, ref ByteResult);
                //AppUtility._config.MessageBox("开始登录");
                hs[3] = ByteResult;
                _Currentsocket.SetSendData(hs);
                _Currentsocket.TcpClient = MonitorClient.AppUtility.CommonList.MonClient;
                //_Currentsocket.do_Job(null);
                ThreadPool.QueueUserWorkItem(new WaitCallback(_Currentsocket.do_Job), null);
                _5808__ShowMsgLog("正在进行登录验证，请稍等……");


            }
            catch (Exception ex)
            {
                AppUtility.config.MessageBox(ex.Message);
            }
        }
    }
}
