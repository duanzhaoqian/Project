using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MonitorClient.AppUtility;
using System.Threading;
using System.Collections;

namespace MonitorClient
{
    public partial class CtrServerinfo : UserControl
    {
        public static event FrMain.ShowMsg _ShowMsg;
        public delegate void ReloadServerType(DataTable dt);
        public delegate void DataGridViewDataBind(DataTable dt);
        public delegate void ReSetText();
        public CtrServerinfo()
        {
            InitializeComponent();
            MonitorClient.AppUtility.CommonList._ReloadServerType+=new ReloadServerType(CommonList__ReloadServerType);
            MonitorClient.AppUtility.CommonList._ServerinfoDataGridViewDataBind+=new DataGridViewDataBind(CommonList__ServerinfoDataGridViewDataBind);
            MonitorClient.NetClassLibrary._5814._ReSetText+=new ReSetText(_5814__ReSetText);
        }
        private int _CurrentID = 0;
        public int CurrentID
        {
            get { return _CurrentID; }
            set
            {
                this._CurrentID = value;
                //if (value == 0)
                //{
                //    this.txtTYPEID.Enabled = true;
                //    // this.button1.Text = "添加";
                //}
                //else
                //{
                //    this.txtTYPEID.Enabled = false;
                //    //this.button1.Text = "修改";
                //}
            }
        }

        private void _5814__ReSetText()
        {
            try
            {
                //192.168.0.110
                foreach (Control ctr in this.panel1.Controls)
                {
                    if (ctr is TextBox)
                    {
                        ctr.Text = "";
                    }
                }
                this.cmbServerTypeid.SelectedValue = "";
                //MonitorClient.AppUtility.CommonList.t_serverinfo;
                if (MonitorClient.AppUtility.CommonList.t_serverinfo == null)
                    return;
                if (MonitorClient.AppUtility.CommonList.t_serverinfo.Rows.Count == 0)
                    return;
                foreach (DataRow rows in MonitorClient.AppUtility.CommonList.t_serverinfo.Rows)
                {
                    if (rows["ID"].ToString() == this.CurrentID.ToString())
                    {
                        MonitorClient.AppUtility.CommonList.t_serverinfo.Rows.Remove(rows);
                        break;
                    }
                }

                CommonList__ServerinfoDataGridViewDataBind(MonitorClient.AppUtility.CommonList.t_serverinfo);
            }
            catch (Exception ex)
            {
                _ShowMsg(ex.StackTrace + ex.Message);
            }
        }
        private void CommonList__ServerinfoDataGridViewDataBind(DataTable dt)
        {
            try
            {
                _MyDataGridView(dt, this.dataGridView1);
            }
            catch (Exception ex)
            {
                _ShowMsg(ex.Message);
            }
        }
        private void CtrServerinfo_Load(object sender, EventArgs e)
        {
            //_MyDropListDataBind(dt, this.cmb_Server, "sname", "ID");  
            try
            {
                ThreadPool.QueueUserWorkItem(new WaitCallback(CommonList__ReloadServerTypeStart), null);
            }
            catch (Exception ex)
            {
                _ShowMsg(ex.Message);
            }
        }

        public void _MyDataGridView(DataTable dt, System.Windows.Forms.DataGridView dataGridView1)
        {
            try
            {
                if (!dataGridView1.InvokeRequired)
                {
                    dataGridView1.DataSource = dt;
                }
                else
                {
                    MyDataGridView a1 = new MyDataGridView(_MyDataGridView);
                    Invoke(a1, new object[] { dt, dataGridView1 });//执行唤醒操作
                }
            }
            catch (Exception ex)
            {
                _ShowMsg(ex.Message);
            }
            finally
            {
                //closegif();

            }
        }


        private void CommonList__ReloadServerType(DataTable dt)
        {
            try
            {
                //comboBox1
                _MyDropListDataBind(dt, this.cmbServerTypeid, "SERVTYPENAME", "SERVTYPEID");
                _MyDropListDataBind(dt, this.comboBox1, "SERVTYPENAME", "SERVTYPEID");
            }
            catch (Exception ex)
            {
                _ShowMsg(ex.StackTrace+ex.Message);
            }
        }
        private void CommonList__ReloadServerTypeStart(object obj)
        {
            try
            {
                /*                 
                < SERVTYPEID></ SERVTYPEID>
<SERVTYPENAME></ SERVTYPENAME>

                 */
                //_ShowMsg("查询用户");



                MonitorClient.AppUtility.Currentsocket _Currentsocket = new Currentsocket();
                _Currentsocket.Model = MonitorClient.AppUtility.CommonList.CurrentServerInfo;
                //Model.ServerInfo _ServerInfo = new Model.ServerInfo();                    //_Currentsocket.Model = _ServerInfo;
                Hashtable hs = new Hashtable();
                hs[1] = MonitorClient.AppUtility.KOMCmd.skServertype;
                hs[2] = MonitorClient.AppUtility.KOMCmd.Succeed;
                List<CommClass.TTable> ListTtable = new List<AppUtility.CommClass.TTable>();
                //Table属性内容
                AppUtility.CommClass.TTable Ttable = new AppUtility.CommClass.TTable();
                Ttable.FieldName = "SERVTYPEID";
                Ttable.FieldValue = "";
                ListTtable.Add(Ttable);
                Ttable.FieldName = "SERVTYPENAME";
                Ttable.FieldValue = "";
                ListTtable.Add(Ttable);
                //Ttable.FieldName = "CURRPAGE";
                //Ttable.FieldValue = this.CURRPAGE.ToString();
                //ListTtable.Add(Ttable);
                byte[] ByteResult = null;
                AppUtility.CommClass.TableToByteArry(ListTtable, ref ByteResult);
                //AppUtility._config.MessageBox("开始登录");
                hs[3] = ByteResult;
                _Currentsocket.SetSendData(hs);
                _Currentsocket.TcpClient = MonitorClient.AppUtility.CommonList.MonClient;
                //_Currentsocket.do_Job(null);
                ThreadPool.QueueUserWorkItem(new WaitCallback(_Currentsocket.do_Job), null);
                _ShowMsg("正在查询设备类型，请稍等……");
            }
            catch (Exception ex)
            {
                _ShowMsg(ex.Message);
            }
        }
        public void _MyDropListDataBind(DataTable dt, ComboBox boxtemp, string strtext, string strvalue)
        {
            try
            {
                if (!boxtemp.InvokeRequired)//判断是否需要进行唤醒的请求，如果控件与主线程在一个线程内，可以写成if(!InvokeRequired)
                {
                    //boxtemp.Items.Add(new string[] { "0", "选择设备类型" });
                    DataTable temp = dt.Clone();
                    DataRow row = temp.NewRow();
                    row[strtext] = "选择设备类型";
                    row[strvalue] = "";
                    temp.Rows.Add(row);
                    foreach (DataRow row0 in dt.Rows)
                    {
                        temp.Rows.Add(row0.ItemArray);
                    }
                  
   //for (int i = 0; i < dt.Rows.Count; i++)
   //             {
   //                 //将数组元素加入表...
   //                // dt3.Rows.Add(dr[i]);//出错提示为:该行已经属于另一个表
   //                 temp.Rows.Add(dr[i].ItemArray);
   //             }
                    //MessageBox.Show("同一线程内");
                    boxtemp.DataSource = temp;
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

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                CommonList__ReloadServerTypeStart(null);
                //ThreadPool.QueueUserWorkItem(new WaitCallback(CommonList__ReloadServerTypeStart), null);
            }
            catch (Exception ex)
            {
                _ShowMsg(ex.Message);
            }
        }
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                _ShowMsg("开始添加设备……");
                if (string.IsNullOrEmpty(this.cmbServerTypeid.SelectedValue.ToString()))
                {
                    _ShowMsg("输入设备类型！");
                    //this.txtTYPEID.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(this.txtSERVERID.Text))
                {
                    _ShowMsg("输入设备编号！");
                    this.txtSERVERID.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(this.txtNAME.Text))
                {
                    _ShowMsg("输入设备名称！");
                    this.txtNAME.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(this.txtwanip.Text))
                {
                    _ShowMsg("输入WanIP！");
                    this.txtwanip.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(this.txtlanip.Text))
                {
                    _ShowMsg("输入LanIP！");
                    this.txtlanip.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(this.txtwanport.Text))
                {
                    _ShowMsg("输入服务侦听端口！");
                    this.txtwanport.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(this.txtpath.Text))
                {
                    _ShowMsg("输入设备路径！");
                    this.txtpath.Focus();
                    return;
                }

                MonitorClient.AppUtility.Currentsocket _Currentsocket = new Currentsocket();
                _Currentsocket.Model = MonitorClient.AppUtility.CommonList.CurrentServerInfo;
                Hashtable hs = new Hashtable();
                hs[1] = MonitorClient.AppUtility.KOMCmd.AddServer;
                hs[2] = MonitorClient.AppUtility.KOMCmd.Succeed;
                List<CommClass.TTable> ListTtable = new List<AppUtility.CommClass.TTable>();
                //Table属性内容
                AppUtility.CommClass.TTable Ttable = new AppUtility.CommClass.TTable();
                Ttable.FieldName = "SERVERID";
                Ttable.FieldValue = this.txtSERVERID.Text;
                ListTtable.Add(Ttable);
                Ttable.FieldName = "NAME";
                Ttable.FieldValue = this.txtNAME.Text;
                ListTtable.Add(Ttable);
                Ttable.FieldName = "SERVTYPE";
                Ttable.FieldValue = this.cmbServerTypeid.SelectedValue.ToString();
                ListTtable.Add(Ttable);
                Ttable.FieldName = "WANIP";
                Ttable.FieldValue = this.txtwanip.Text;
                ListTtable.Add(Ttable);
                Ttable.FieldName = "LANIP";
                Ttable.FieldValue = this.txtlanip.Text;
                ListTtable.Add(Ttable);
                Ttable.FieldName = "WANPORT";
                Ttable.FieldValue = this.txtwanport.Text;
                ListTtable.Add(Ttable);
                Ttable.FieldName = "UPSERVERID";
                Ttable.FieldValue = "";
                ListTtable.Add(Ttable);
                Ttable.FieldName = "PATH";
                Ttable.FieldValue = this.txtpath.Text;
                ListTtable.Add(Ttable);
                Ttable.FieldName = "BEIZHU";
                Ttable.FieldValue = this.txtDESCR.Text;
                ListTtable.Add(Ttable);

                byte[] ByteResult = null;
                AppUtility.CommClass.TableToByteArry(ListTtable, ref ByteResult);
                hs[3] = ByteResult;
                _Currentsocket.SetSendData(hs);
                _Currentsocket.TcpClient = MonitorClient.AppUtility.CommonList.MonClient;
                //_Currentsocket.do_Job(null);
                ThreadPool.QueueUserWorkItem(new WaitCallback(_Currentsocket.do_Job), null);
                _ShowMsg("正在添加设备信息，请稍等……");
            }
            catch (Exception ex)
            {
                _ShowMsg(ex.StackTrace + ex.Message);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                //192.168.0.110
                foreach (Control ctr in this.panel1.Controls)
                {
                    if (ctr is TextBox)
                    {
                        ctr.Text = "";
                    }
                }
                this.cmbServerTypeid.SelectedValue = "0";
            }
            catch (Exception ex)
            {
                _ShowMsg(ex.StackTrace + ex.Message);
            }
        }


        /// <summary>
        /// 双击修改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (this.dataGridView1.Rows.Count == 0)
                    return;
                if (e.RowIndex != -1)
                {
                    int id = Convert.ToInt32(this.dataGridView1.Rows[e.RowIndex].Cells["CID"].Value);
                    this.CurrentID = id;
                    _ShowMsg(this.CurrentID.ToString());
                    this.cmbServerTypeid.SelectedValue = this.dataGridView1.Rows[e.RowIndex].Cells["CSERVTYPE"].Value.ToString();
                    this.txtSERVERID.Text = this.dataGridView1.Rows[e.RowIndex].Cells["CSERVERID"].Value.ToString();
                    this.txtNAME.Text = this.dataGridView1.Rows[e.RowIndex].Cells["CNAME"].Value.ToString();
                    this.txtwanip.Text = this.dataGridView1.Rows[e.RowIndex].Cells["CWANIP"].Value.ToString();
                    this.txtlanip.Text = this.dataGridView1.Rows[e.RowIndex].Cells["LANIP"].Value.ToString();
                    this.txtwanport.Text = this.dataGridView1.Rows[e.RowIndex].Cells["CWANPORT"].Value.ToString();

                    this.txtpath.Text = this.dataGridView1.Rows[e.RowIndex].Cells["CPATH"].Value.ToString();
                    this.txtDESCR.Text = this.dataGridView1.Rows[e.RowIndex].Cells["CBEIZHU"].Value.ToString();                

                }
            }
            catch (Exception ex)
            {
                _ShowMsg(ex.Message);
            }
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
               // _ShowMsg("开始添加设备……");
                //if (string.IsNullOrEmpty(this.cmbServerTypeid.SelectedValue.ToString()))
                //{
                //    _ShowMsg("输入设备类型！");
                //    //this.txtTYPEID.Focus();
                //    return;
                //}
                //if (string.IsNullOrEmpty(this.txtSERVERID.Text))
                //{
                //    _ShowMsg("输入设备编号！");
                //    this.txtSERVERID.Focus();
                //    return;
                //}
                //if (string.IsNullOrEmpty(this.txtNAME.Text))
                //{
                //    _ShowMsg("输入设备名称！");
                //    this.txtNAME.Focus();
                //    return;
                //}
                //if (string.IsNullOrEmpty(this.txtwanip.Text))
                //{
                //    _ShowMsg("输入服务IP！");
                //    this.txtwanip.Focus();
                //    return;
                //}
                //if (string.IsNullOrEmpty(this.txtwanport.Text))
                //{
                //    _ShowMsg("输入服务侦听端口！");
                //    this.txtwanport.Focus();
                //    return;
                //}
                //if (string.IsNullOrEmpty(this.txtpath.Text))
                //{
                //    _ShowMsg("输入设备路径！");
                //    this.txtpath.Focus();
                //    return;
                //}

                MonitorClient.AppUtility.Currentsocket _Currentsocket = new Currentsocket();
                _Currentsocket.Model = MonitorClient.AppUtility.CommonList.CurrentServerInfo;
                Hashtable hs = new Hashtable();
                hs[1] = MonitorClient.AppUtility.KOMCmd.skServer;
                hs[2] = MonitorClient.AppUtility.KOMCmd.Succeed;
                List<CommClass.TTable> ListTtable = new List<AppUtility.CommClass.TTable>();
                //Table属性内容
                AppUtility.CommClass.TTable Ttable = new AppUtility.CommClass.TTable();
                Ttable.FieldName = "SERVERID";
                Ttable.FieldValue = this.txtSERVERID2.Text;
                ListTtable.Add(Ttable);
                Ttable.FieldName = "NAME";
                Ttable.FieldValue = this.txtNAME2.Text;
                ListTtable.Add(Ttable);
                Ttable.FieldName = "SERVTYPE";
                Ttable.FieldValue = this.comboBox1.SelectedValue.ToString();
                ListTtable.Add(Ttable);
                //Ttable.FieldName = "WANIP";
                //Ttable.FieldValue = this.txtwanip.Text;
                //ListTtable.Add(Ttable);
                //Ttable.FieldName = "WANPORT";
                //Ttable.FieldValue = this.txtwanport.Text;
                //ListTtable.Add(Ttable);
                //Ttable.FieldName = "UPSERVERID";
                //Ttable.FieldValue = "";
                //ListTtable.Add(Ttable);
                //Ttable.FieldName = "PATH";
                //Ttable.FieldValue = this.txtpath.Text;
                //ListTtable.Add(Ttable);
                //Ttable.FieldName = "BEIZHU";
                //Ttable.FieldValue = this.txtDESCR.Text;
                //ListTtable.Add(Ttable);

                byte[] ByteResult = null;
                AppUtility.CommClass.TableToByteArry(ListTtable, ref ByteResult);
                hs[3] = ByteResult;
                _Currentsocket.SetSendData(hs);
                _Currentsocket.TcpClient = MonitorClient.AppUtility.CommonList.MonClient;
                //_Currentsocket.do_Job(null);
                ThreadPool.QueueUserWorkItem(new WaitCallback(_Currentsocket.do_Job), null);
                _ShowMsg("正在查询设备信息，请稍等……");
            }
            catch (Exception ex)
            {
                _ShowMsg(ex.StackTrace + ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.CurrentID == 0)
                {
                    _ShowMsg("选择需要删除的数据，双击列表中的行即可选中！");
                    return;
                }

                MonitorClient.AppUtility.Currentsocket _Currentsocket = new Currentsocket();
                _Currentsocket.Model = MonitorClient.AppUtility.CommonList.CurrentServerInfo;
                Hashtable hs = new Hashtable();
                hs[1] = MonitorClient.AppUtility.KOMCmd.DELServer;
                hs[2] = MonitorClient.AppUtility.KOMCmd.Succeed;
                List<CommClass.TTable> ListTtable = new List<AppUtility.CommClass.TTable>();
                //Table属性内容
                AppUtility.CommClass.TTable Ttable = new AppUtility.CommClass.TTable();
                Ttable.FieldName = "ID";
                Ttable.FieldValue = this.CurrentID.ToString();
                ListTtable.Add(Ttable);
                //Ttable.FieldName = "NAME";
                //Ttable.FieldValue = this.txtNAME2.Text;
                //ListTtable.Add(Ttable);
                //Ttable.FieldName = "SERVTYPE";
                //Ttable.FieldValue = this.comboBox1.SelectedValue.ToString();
                //ListTtable.Add(Ttable);
             

                byte[] ByteResult = null;
                AppUtility.CommClass.TableToByteArry(ListTtable, ref ByteResult);
                hs[3] = ByteResult;
                _Currentsocket.SetSendData(hs);
                _Currentsocket.TcpClient = MonitorClient.AppUtility.CommonList.MonClient;
                //_Currentsocket.do_Job(null);
                ThreadPool.QueueUserWorkItem(new WaitCallback(_Currentsocket.do_Job), null);
                _ShowMsg("正在提交删除设备信息，请稍等……");

            }
            catch (Exception ex)
            {
                _ShowMsg(ex.StackTrace+ex.Message);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                //_ShowMsg("开始添加设备……");
                if (this.CurrentID == 0)
                {
                    _ShowMsg("选择需要修改的数据，双击列表中的行即可选中！");
                    return;
                }
                if (string.IsNullOrEmpty(this.cmbServerTypeid.SelectedValue.ToString()))
                {
                    _ShowMsg("输入设备类型！");
                    //this.txtTYPEID.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(this.txtSERVERID.Text))
                {
                    _ShowMsg("输入设备编号！");
                    this.txtSERVERID.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(this.txtNAME.Text))
                {
                    _ShowMsg("输入设备名称！");
                    this.txtNAME.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(this.txtwanip.Text))
                {
                    _ShowMsg("输入WanIP！");
                    this.txtwanip.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(this.txtlanip.Text))
                {
                    _ShowMsg("输入LanIP！");
                    this.txtlanip.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(this.txtwanport.Text))
                {
                    _ShowMsg("输入服务侦听端口！");
                    this.txtwanport.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(this.txtpath.Text))
                {
                    _ShowMsg("输入设备路径！");
                    this.txtpath.Focus();
                    return;
                }

                MonitorClient.AppUtility.Currentsocket _Currentsocket = new Currentsocket();
                _Currentsocket.Model = MonitorClient.AppUtility.CommonList.CurrentServerInfo;
                Hashtable hs = new Hashtable();
                hs[1] = MonitorClient.AppUtility.KOMCmd.UpdateServer;
                hs[2] = MonitorClient.AppUtility.KOMCmd.Succeed;
                List<CommClass.TTable> ListTtable = new List<AppUtility.CommClass.TTable>();
                //Table属性内容
                AppUtility.CommClass.TTable Ttable = new AppUtility.CommClass.TTable();
                Ttable.FieldName = "ID";
                Ttable.FieldValue = this.CurrentID.ToString();
                ListTtable.Add(Ttable);
                Ttable.FieldName = "SERVERID";
                Ttable.FieldValue = this.txtSERVERID.Text;
                ListTtable.Add(Ttable);
                Ttable.FieldName = "NAME";
                Ttable.FieldValue = this.txtNAME.Text;
                ListTtable.Add(Ttable);
                Ttable.FieldName = "SERVTYPE";
                Ttable.FieldValue = this.cmbServerTypeid.SelectedValue.ToString();
                ListTtable.Add(Ttable);
                Ttable.FieldName = "WANIP";
                Ttable.FieldValue = this.txtwanip.Text;
                ListTtable.Add(Ttable);
                Ttable.FieldName = "LANIP";
                Ttable.FieldValue = this.txtlanip.Text;
                ListTtable.Add(Ttable);
                Ttable.FieldName = "WANPORT";
                Ttable.FieldValue = this.txtwanport.Text;
                ListTtable.Add(Ttable);
                Ttable.FieldName = "UPSERVERID";
                Ttable.FieldValue = "";
                ListTtable.Add(Ttable);
                Ttable.FieldName = "PATH";
                Ttable.FieldValue = this.txtpath.Text;
                ListTtable.Add(Ttable);
                Ttable.FieldName = "BEIZHU";
                Ttable.FieldValue = this.txtDESCR.Text;
                ListTtable.Add(Ttable);

                byte[] ByteResult = null;
                AppUtility.CommClass.TableToByteArry(ListTtable, ref ByteResult);
                hs[3] = ByteResult;
                _Currentsocket.SetSendData(hs);
                _Currentsocket.TcpClient = MonitorClient.AppUtility.CommonList.MonClient;
                //_Currentsocket.do_Job(null);
                ThreadPool.QueueUserWorkItem(new WaitCallback(_Currentsocket.do_Job), null);
                _ShowMsg("正在添加设备信息，请稍等……");
            }
            catch (Exception ex)
            {
                _ShowMsg(ex.StackTrace + ex.Message);
            }
        }
    }
}