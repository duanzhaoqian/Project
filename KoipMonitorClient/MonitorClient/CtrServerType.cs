using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using MonitorClient.AppUtility;
using System.Threading;

namespace MonitorClient
{
    public partial class CtrServerType : UserControl
    {
        public static event FrMain.ShowMsg _ShowMsg;
        public delegate void DataGridViewDataBind(DataTable dt);
        public CtrServerType()
        {
            InitializeComponent();
            MonitorClient.AppUtility.CommonList._DataGridViewDataBind += new DataGridViewDataBind(CommonList__DataGridViewDataBind);
        }
        private int _CurrentID = 0;
        public int CurrentID
        {
            get { return _CurrentID; }
            set
            {
                this._CurrentID = value;
                if (value == 0)
                {
                    this.txtTYPEID.Enabled = true;
                    // this.button1.Text = "添加";
                }
                else
                {
                    this.txtTYPEID.Enabled = false;
                    //this.button1.Text = "修改";
                }
            }
        }
        private void CommonList__DataGridViewDataBind(DataTable dt)
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


        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                /*                 
                < SERVTYPEID></ SERVTYPEID>
<SERVTYPENAME></ SERVTYPENAME>

                 */
                //_ShowMsg("查询用户");

                if (string.IsNullOrEmpty(this.txtTYPEID.Text))
                {
                    _ShowMsg("输入设备类型编号！");
                    this.txtTYPEID.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(this.txtTYPENAME.Text))
                {
                    _ShowMsg("输入设备名称！");
                    this.txtTYPENAME.Focus();
                    return;
                }

                MonitorClient.AppUtility.Currentsocket _Currentsocket = new Currentsocket();
                _Currentsocket.Model = MonitorClient.AppUtility.CommonList.CurrentServerInfo;
                //Model.ServerInfo _ServerInfo = new Model.ServerInfo();                    //_Currentsocket.Model = _ServerInfo;
                Hashtable hs = new Hashtable();
                hs[1] = MonitorClient.AppUtility.KOMCmd.AddServertype;
                hs[2] = MonitorClient.AppUtility.KOMCmd.Succeed;
                List<CommClass.TTable> ListTtable = new List<AppUtility.CommClass.TTable>();
                //Table属性内容
                AppUtility.CommClass.TTable Ttable = new AppUtility.CommClass.TTable();
                Ttable.FieldName = "SERVTYPEID";
                Ttable.FieldValue = this.txtTYPEID.Text;
                ListTtable.Add(Ttable);
                Ttable.FieldName = "SERVTYPENAME";
                Ttable.FieldValue = this.txtTYPENAME.Text.ToString();
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
                _ShowMsg("正在添加设备类型，请稍等……");
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
                /*                 
                < SERVTYPEID></ SERVTYPEID>
<SERVTYPENAME></ SERVTYPENAME>

                 */
                //_ShowMsg("查询用户");

                if (string.IsNullOrEmpty(this.txtTYPEID2.Text))
                {
                    _ShowMsg("精确查询，请输入设备类型编号！");
                    this.txtTYPEID2.Focus();
                    // return;
                }
                if (string.IsNullOrEmpty(this.txtTYPENAME2.Text))
                {
                    _ShowMsg("精确查询，请输入设备名称！");
                    this.txtTYPENAME2.Focus();
                    // return;
                }

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
                Ttable.FieldValue = this.txtTYPEID2.Text;
                ListTtable.Add(Ttable);
                Ttable.FieldName = "SERVTYPENAME";
                Ttable.FieldValue = this.txtTYPENAME2.Text.ToString();
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

        /// <summary>
        /// 
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
                    int id = Convert.ToInt32(this.dataGridView1.Rows[e.RowIndex].Cells[0].Value);
                    this.CurrentID = id;
                    this.txtTYPEID.Text = this.dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                    this.txtTYPENAME.Text = this.dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                    //this.txtport.Text = this.dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
                    //textBox2.Text = this.dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                _ShowMsg(ex.Message);
            }
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                /*                 
                < SERVTYPEID></ SERVTYPEID>
<SERVTYPENAME></ SERVTYPENAME>

                 */
                //_ShowMsg("查询用户");

                //if (string.IsNullOrEmpty(this.txtTYPEID2.Text))
                //{
                //    _ShowMsg("精确查询，请输入设备类型编号！");
                //    this.txtTYPEID2.Focus();
                //    // return;
                //}
                //if (string.IsNullOrEmpty(this.txtTYPENAME2.Text))
                //{
                //    _ShowMsg("精确查询，请输入设备名称！");
                //    this.txtTYPENAME2.Focus();
                //    // return;
                //}
                if (this.CurrentID == 0)
                {
                    _ShowMsg("请选择需要删除的设备类型，双击选择类型！");
                    //this.txtTYPENAME2.Focus();
                    return;
                }

                MonitorClient.AppUtility.Currentsocket _Currentsocket = new Currentsocket();
                _Currentsocket.Model = MonitorClient.AppUtility.CommonList.CurrentServerInfo;
                //Model.ServerInfo _ServerInfo = new Model.ServerInfo();                    //_Currentsocket.Model = _ServerInfo;
                Hashtable hs = new Hashtable();
                hs[1] = MonitorClient.AppUtility.KOMCmd.delServertype;
                hs[2] = MonitorClient.AppUtility.KOMCmd.Succeed;
                List<CommClass.TTable> ListTtable = new List<AppUtility.CommClass.TTable>();
                //Table属性内容
                AppUtility.CommClass.TTable Ttable = new AppUtility.CommClass.TTable();
                Ttable.FieldName = "ID";
                Ttable.FieldValue = this.CurrentID.ToString();
                ListTtable.Add(Ttable);
                //Ttable.FieldName = "SERVTYPENAME";
                //Ttable.FieldValue = this.txtTYPENAME2.Text.ToString();
                //ListTtable.Add(Ttable);
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
                _ShowMsg("正在删除设备类型，请稍等……");
            }
            catch (Exception ex)
            {
                _ShowMsg(ex.Message);
            }
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                /*                 
                < SERVTYPEID></ SERVTYPEID>
<SERVTYPENAME></ SERVTYPENAME>

                 */
                //_ShowMsg("查询用户");
                if (this.CurrentID == 0)
                {
                    _ShowMsg("请选择需要修改的设备类型，双击选择类型！");
                    //this.txtTYPENAME2.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(this.txtTYPEID.Text))
                {
                    _ShowMsg("输入设备类型编号！");
                    this.txtTYPEID.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(this.txtTYPENAME.Text))
                {
                    _ShowMsg("输入设备名称！");
                    this.txtTYPENAME.Focus();
                    return;
                }

                MonitorClient.AppUtility.Currentsocket _Currentsocket = new Currentsocket();
                _Currentsocket.Model = MonitorClient.AppUtility.CommonList.CurrentServerInfo;
                //Model.ServerInfo _ServerInfo = new Model.ServerInfo();                    //_Currentsocket.Model = _ServerInfo;
                Hashtable hs = new Hashtable();
                hs[1] = MonitorClient.AppUtility.KOMCmd.updateServertype;
                hs[2] = MonitorClient.AppUtility.KOMCmd.Succeed;
                List<CommClass.TTable> ListTtable = new List<AppUtility.CommClass.TTable>();
                //Table属性内容

                AppUtility.CommClass.TTable Ttable = new AppUtility.CommClass.TTable();
                Ttable.FieldName = "ID";
                Ttable.FieldValue = this.CurrentID.ToString();
                ListTtable.Add(Ttable);
                Ttable.FieldName = "SERVTYPEID";
                Ttable.FieldValue = this.txtTYPEID.Text;
                ListTtable.Add(Ttable);
                Ttable.FieldName = "SERVTYPENAME";
                Ttable.FieldValue = this.txtTYPENAME.Text.ToString();
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
                _ShowMsg("正在修改设备类型，请稍等……");
            }
            catch (Exception ex)
            {
                _ShowMsg(ex.Message);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                this.CurrentID = 0;
                this.txtTYPEID.Text = "";
                this.txtTYPENAME.Text = "";
            }
            catch (Exception ex)
            {
                _ShowMsg(ex.Message);
            }
        }


    }
}
