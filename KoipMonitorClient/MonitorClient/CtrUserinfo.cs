using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MonitorClient.AppUtility;
using System.Collections;
using System.Threading;

namespace MonitorClient
{
    public partial class CtrUserinfo : UserControl
    {
        public static event FrMain.ShowMsg _ShowMsg;
        public delegate void DataGridViewDataBind(DataTable dt);
        public delegate void SetCurrentpage(int page);
        public delegate void SetTotalSetPage(int pagecount);
        public delegate void SetTotalCount(int pagecount);
        private int currpage = 1;
        private int pagecount = 60;
        private int totalcount;
        private int totalpage;
        public int CURRPAGE
        {
            set { 
                this.currpage = value;
                if (this.currpage < this.totalpage)
                {
                    this.button4.Enabled = true;
                }
                else
                {
                     this.button4.Enabled = false;
                }

                if (this.currpage >1)
                {
                    this.button3.Enabled = true;
                }
                else
                {
                    this.button3.Enabled = false;
                }
            
            }
            get { return this.currpage; }
        }
        public int PAGECOUNT
        {
            set { this.pagecount = value; }
            get { return this.pagecount; }
        }
        public int TOTALCOUNT
        {
            set { this.totalcount = value; }
            get { return this.totalcount; }
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
        public CtrUserinfo()
        {
            //InitializeComponent();
            Control.CheckForIllegalCrossThreadCalls = false;//线程访问控件设置
            InitializeComponent();
            ListViewInit();
            MonitorClient.AppUtility.CommonList._CtrUserinfoDataGridViewDataBind += new DataGridViewDataBind(CommonList__DataGridViewDataBind);
            MonitorClient.NetClassLibrary._5821._SetCurrentpage+=new SetCurrentpage(_5821__SetCurrentpage);
            MonitorClient.NetClassLibrary._5821._SetTotalCount+=new SetTotalCount(_5821__SetTotalCount);
            MonitorClient.NetClassLibrary._5821._SetTotalSetPage+=new SetTotalSetPage(_5821__SetTotalSetPage);
        }

        private void _5821__SetCurrentpage(int aa)
        {
            this.CURRPAGE = aa;
            this.label_pages.Text = "页码" + this.CURRPAGE + "/" + this.totalpage + " 总记录：" + this.TOTALCOUNT;
        }
        private void _5821__SetTotalCount(int aa)
        {
            this.TOTALCOUNT = aa;
        }
        private void _5821__SetTotalSetPage(int aa)
        {
            this.totalpage = aa;
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


        private void ListViewInit()
        {
            try
            {
                //this.listView_UserRoles.GridLines = true; //显示表格线
                //this.listView_UserRoles.View = View.Details;//显示表格细节
                //this.listView_UserRoles.LabelEdit = false; //是否可编辑,ListView只可编辑第一列。
                //this.listView_UserRoles.Scrollable = true;//有滚动条
                //this.listView_UserRoles.HeaderStyle = ColumnHeaderStyle.Clickable;//对表头进行设置
                //this.listView_UserRoles.FullRowSelect = true;//是否可以选择行
                ////添加表头
                //this.listView_UserRoles.Columns.Add("ID", 0);
                //this.listView_UserRoles.Columns.Add("用户ID", 80);
                //this.listView_UserRoles.Columns.Add("用户姓名", 160);
                //this.listView_UserRoles.Columns.Add("权限", 0);


                //           //添加各项
                //ListViewItem[] p = new ListViewItem[2];
                //p[0] = new ListViewItem(new string[] { "","aaaa","bbbb"});
                //p[1] = new ListViewItem(new string[] { "","cccc", "ggggg" });
                ////p[0].SubItems[0].BackColor = Color.Red; //用于设置某行的背景颜色

                //this.listView_User.Items.AddRange(p);
                //也可以用this.listView1.Items.Add();不过需要在使用的前后添加Begin... 和End...防止界面自动刷新
                // 添加分组
                //this.listView_User.Groups.Add(new ListViewGroup("tou"));
                //this.listView_User.Groups.Add(new ListViewGroup("wei"));

                //this.listView_User.Items[0].Group = this.listView_User.Groups[0];
                //this.listView_User.Items[1].Group = this.listView_User.Groups[1];





                this.listView_UserRoles.GridLines = true; //显示表格线
                this.listView_UserRoles.View = View.Details;//显示表格细节
                this.listView_UserRoles.LabelEdit = false; //是否可编辑,ListView只可编辑第一列。
                this.listView_UserRoles.Scrollable = true;//有滚动条
                this.listView_UserRoles.HeaderStyle = ColumnHeaderStyle.Clickable;//对表头进行设置
                this.listView_UserRoles.FullRowSelect = true;//是否可以选择行
                listView_UserRoles.CheckBoxes = true;
                //添加表头
                this.listView_UserRoles.Columns.Add("权限名称", 160);
                this.listView_UserRoles.Columns.Add("权限值", 0);

                //曲库信息管理
                //歌手信息管理
                //情境信息管理
                //生成歌本
                //上下架管理
                //新增用户
                //修改密码
                //生成入库脚本
                //字典信息
                //发布歌本
                //数据备份
                //数据恢复
                //歌曲排行检索

                //添加各项
                //ListViewItem[] p1 = new ListViewItem[14];
                //p1[0] = new ListViewItem(new string[] { "全选", "0" });
                //p1[1] = new ListViewItem(new string[] { "曲库信息管理", "1" });
                //p1[2] = new ListViewItem(new string[] { "歌手信息管理", "2" });
                //p1[3] = new ListViewItem(new string[] { "情境信息管理", "3" });
                //p1[4] = new ListViewItem(new string[] { "生成歌本", "4" });
                //p1[5] = new ListViewItem(new string[] { "上下架管理", "5" });
                //p1[6] = new ListViewItem(new string[] { "新增用户", "6" });
                //p1[7] = new ListViewItem(new string[] { "修改密码", "7" });
                //p1[8] = new ListViewItem(new string[] { "生成入库脚本", "8" });
                //p1[9] = new ListViewItem(new string[] { "字典信息", "9" });
                //p1[10] = new ListViewItem(new string[] { "发布歌本", "10" });
                //p1[11] = new ListViewItem(new string[] { "数据备份", "11" });
                //p1[12] = new ListViewItem(new string[] { "数据恢复", "12" });
                //p1[13] = new ListViewItem(new string[] { "歌曲排行检索", "13" });


                //添加各项
                ListViewItem[] p1 = new ListViewItem[5];

                p1[0] = new ListViewItem(new string[] { "用户管理", "1" });
                p1[1] = new ListViewItem(new string[] { "更改密码", "2" });
                p1[2] = new ListViewItem(new string[] { "设备类型", "3" });
                p1[3] = new ListViewItem(new string[] { "设备信息", "4" });
                p1[4] = new ListViewItem(new string[] { "服务状态监控显示", "5" });
                //p1[5] = new ListViewItem(new string[] { "新增用户", "6" });
                //p1[6] = new ListViewItem(new string[] { "修改密码", "7" });
                //p1[7] = new ListViewItem(new string[] { "生成入库脚本", "8" });
                //p1[8] = new ListViewItem(new string[] { "字典信息", "9" });
                //p1[9] = new ListViewItem(new string[] { "发布歌本", "10" });
                //p1[10] = new ListViewItem(new string[] { "数据备份", "11" });
                //p1[11] = new ListViewItem(new string[] { "数据恢复", "12" });
                //p1[12] = new ListViewItem(new string[] { "歌曲排行检索", "13" });

                this.listView_UserRoles.Items.AddRange(p1);
            }
            catch (Exception ex)
            {
                //MessageBox.Show("ListViewInit>>" + ex.Message, ClsCommon.SysMessage, MessageBoxButtons.OK, MessageBoxIcon.Error);
                AppUtility.config.MessageBox("ListViewInit！" + ex.Message);
                return;
            }


        }

        /// <summary>
        /// 查询用户
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                /*                 
                <?xml version="1.0" encoding="utf-8" ?> 
                <ROOT>
                <ITEM>
                <ID></ID>
                < USERID ></ USERID >
                < PAGECOUNT ></ PAGECOUNT >
                <CURRPAGE></ CURRPAGE >
                </ITEM>
                </ ROOT>                 
                 */
                _ShowMsg("查询用户");

                MonitorClient.AppUtility.CommonList.Usercurrpage = 1;
              
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
                    Ttable.FieldValue = this.txtUSERIDsk.Text;
                    ListTtable.Add(Ttable);
                    Ttable.FieldName = "PAGECOUNT";
                    Ttable.FieldValue = this.PAGECOUNT.ToString();
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
 
            }
        }


        private string getRoles()
        {
            try
            {
                string P_roles = "";
                foreach (ListViewItem vItem in listView_UserRoles.Items)
                {

                    if (vItem.Checked == false)
                    {
                        P_roles += "0";
                    }
                    else
                    {
                        P_roles += "1";
                    }

                }
                return P_roles;
            }
            catch (Exception ex)
            {
                _ShowMsg(ex.Message);
                throw ex;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (ListViewItem vItem in listView_UserRoles.Items)
                {

                    vItem.Checked = true;

                }
            }
            catch (Exception ex)
            {
                _ShowMsg(ex.Message);
                return;
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (ListViewItem vItem in listView_UserRoles.Items)
                {

                    vItem.Checked = false;

                }
            }
            catch (Exception ex)
            {
                _ShowMsg(ex.Message);
                return;
            }
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_update_Click(object sender, EventArgs e)
        {
            try
            {
                /*                 
                < SERVTYPEID></ SERVTYPEID>
<SERVTYPENAME></ SERVTYPENAME>

                 */
                //_ShowMsg("查询用户");

                if (string.IsNullOrEmpty(this.txtUSERID.Text))
                {
                    _ShowMsg("输入登录ID！");
                    this.txtUSERID.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(this.txtUSERNAME.Text))
                {
                    _ShowMsg("输入用户名称！");
                    this.txtUSERNAME.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(this.txtPWD.Text))
                {
                    _ShowMsg("输入登录密码！");
                    this.txtPWD.Focus();
                    return;
                }
                if (this.txtPWD2.Text != this.txtPWD.Text)
                {
                    _ShowMsg("两次密码输入不相同，请重新输入确认密码！");
                    this.txtPWD2.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(this.txtEMAIL.Text))
                {
                    _ShowMsg("输入邮箱！");
                    this.txtEMAIL.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(this.txtMOBILE.Text))
                {
                    _ShowMsg("输入手机号码！");
                    this.txtMOBILE.Focus();
                    return;
                }

                MonitorClient.AppUtility.Currentsocket _Currentsocket = new Currentsocket();
                _Currentsocket.Model = MonitorClient.AppUtility.CommonList.CurrentServerInfo;
                //Model.ServerInfo _ServerInfo = new Model.ServerInfo();                    //_Currentsocket.Model = _ServerInfo;
                Hashtable hs = new Hashtable();
                hs[1] = MonitorClient.AppUtility.KOMCmd.AddUser;
                hs[2] = MonitorClient.AppUtility.KOMCmd.Succeed;
                List<CommClass.TTable> ListTtable = new List<AppUtility.CommClass.TTable>();
                //Table属性内容
                AppUtility.CommClass.TTable Ttable = new AppUtility.CommClass.TTable();
                Ttable.FieldName = "USERID";
                Ttable.FieldValue = this.txtUSERID.Text;
                ListTtable.Add(Ttable);
                Ttable.FieldName = "USERNAME";
                Ttable.FieldValue = this.txtUSERNAME.Text.ToString();
                ListTtable.Add(Ttable);
                Ttable.FieldName = "EMAIL";
                Ttable.FieldValue = this.txtEMAIL.Text.ToString();
                ListTtable.Add(Ttable);
                Ttable.FieldName = "MOBILE";
                Ttable.FieldValue = this.txtMOBILE.Text.ToString();
                ListTtable.Add(Ttable);
                Ttable.FieldName = "USERPWD";
                Ttable.FieldValue = this.txtPWD.Text.ToString();
                ListTtable.Add(Ttable);
                Ttable.FieldName = "ROLES";
                Ttable.FieldValue = this.getRoles();
                ListTtable.Add(Ttable);
                Ttable.FieldName = "ISEMAIL";
                Ttable.FieldValue = this.cbxISEMAIL.Checked ? "1" : "0";
                ListTtable.Add(Ttable);
                Ttable.FieldName = "ISMOBILE";
                Ttable.FieldValue = this.cbxISMOBILE.Checked ? "1" : "0";
                ListTtable.Add(Ttable);

//                <USERID>1</ USERID >
//<USERNAME>1</ USERNAME >
//<EMAIL></ EMAIL >
//<ISEMAIL></IS EMAIL >
//<MOBILE></ MOBILE >
//<ISMOBILE></ ISMOBILE >
//<USERPWD></ USERPWD >
//<ROLES></ROLES>

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
                _ShowMsg("正在添加用户，请稍等……");
            }
            catch (Exception ex)
            {
                _ShowMsg(ex.Message);
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            try
            {
                this.CurrentID = 0;
                foreach (Control crl in this.groupBox4.Controls)
                {
                    if (crl is TextBox)
                    {
                        crl.Text = "";
                    }
                   
                }
                this.cbxISEMAIL.Checked = false;
                this.cbxISMOBILE.Checked = false;
                //foreach (RadioButton r in groupBox4.Controls)
                //{
                //    r.AutoCheck = false;//禁止自动选中！！！！！
                //    r.Checked = false;//全部不选中
                //}
                //foreach (Control r in groupBox4.Controls)
                //{
                //    try
                //    {
                //        if (r is CheckBox)
                //        {
                //            r.Checked = false;//全部不选中
                //        }
                //        //r.AutoCheck = false;//禁止自动选中！！！！！
                //        //r.Checked = false;//全部不选中
                //    }
                //    catch(Exception ex)
                //    {
                //        _ShowMsg(ex.Message);
                //    }
                //}
            }
            catch (Exception ex)
            {
                _ShowMsg(ex.Message);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                /*                 
                <?xml version="1.0" encoding="utf-8" ?> 
                <ROOT>
                <ITEM>
                <ID></ID>
                < USERID ></ USERID >
                < PAGECOUNT ></ PAGECOUNT >
                <CURRPAGE></ CURRPAGE >
                </ITEM>
                </ ROOT>                 
                 */
                _ShowMsg("查询用户");



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
                Ttable.FieldValue = this.txtUSERIDsk.Text;
                ListTtable.Add(Ttable);
                Ttable.FieldName = "PAGECOUNT";
                Ttable.FieldValue = this.PAGECOUNT.ToString();
                ListTtable.Add(Ttable);
                Ttable.FieldName = "CURRPAGE";
                Ttable.FieldValue = (this.CURRPAGE-1).ToString();
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

            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                /*                 
                <?xml version="1.0" encoding="utf-8" ?> 
                <ROOT>
                <ITEM>
                <ID></ID>
                < USERID ></ USERID >
                < PAGECOUNT ></ PAGECOUNT >
                <CURRPAGE></ CURRPAGE >
                </ITEM>
                </ ROOT>                 
                 */
                _ShowMsg("查询用户");



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
                Ttable.FieldValue = this.txtUSERIDsk.Text;
                ListTtable.Add(Ttable);
                Ttable.FieldName = "PAGECOUNT";
                Ttable.FieldValue = this.PAGECOUNT.ToString();
                ListTtable.Add(Ttable);
                Ttable.FieldName = "CURRPAGE";
                Ttable.FieldValue = (this.CURRPAGE + 1).ToString();
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

            }
        }

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
                    this.txtUSERID.Text = this.dataGridView1.Rows[e.RowIndex].Cells["USERID"].Value.ToString();
                    this.txtUSERNAME.Text = this.dataGridView1.Rows[e.RowIndex].Cells["USERNAME"].Value.ToString();
                    this.txtEMAIL.Text = this.dataGridView1.Rows[e.RowIndex].Cells["EMAIL"].Value.ToString();
                    this.cbxISEMAIL.Checked = this.dataGridView1.Rows[e.RowIndex].Cells["CISEMAIL"].Value.ToString() == "1" ? true : false;

                    this.txtMOBILE.Text = this.dataGridView1.Rows[e.RowIndex].Cells["MOBILE"].Value.ToString();
                    this.cbxISMOBILE.Checked = this.dataGridView1.Rows[e.RowIndex].Cells["CISMOBILE"].Value.ToString() == "1" ? true : false;
                    string str_roles = this.dataGridView1.Rows[e.RowIndex].Cells["CROLES"].Value.ToString();
                    if (String.IsNullOrEmpty(str_roles))
                    {
                        return;
                    }
                    else
                    {
                        for (int i = 0; i < str_roles.Length; i++)
                        {
                            string str_role = str_roles.Substring(i, 1);
                            if (str_role == "1")
                            {
                                listView_UserRoles.Items[i].Checked = true;
                            }
                            else
                            {
                                listView_UserRoles.Items[i].Checked = false;

                            }
                        }
                    }
                    //this.txtPWD.Text = this.dataGridView1.Rows[e.RowIndex].Cells["CPWD"].Value.ToString();
                    //this.txtPWD2.Text = this.dataGridView1.Rows[e.RowIndex].Cells["CPWD"].Value.ToString();
                    
                }
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
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                /*                 
                < SERVTYPEID></ SERVTYPEID>
<SERVTYPENAME></ SERVTYPENAME>

                 */
                //_ShowMsg("查询用户");

                if (this.CurrentID==0)
                {
                    _ShowMsg("先选择要修改的用户信息！");
                   
                    return;
                }
                if (string.IsNullOrEmpty(this.txtUSERID.Text))
                {
                    _ShowMsg("输入登录ID！");
                    this.txtUSERID.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(this.txtUSERNAME.Text))
                {
                    _ShowMsg("输入用户名称！");
                    this.txtUSERNAME.Focus();
                    return;
                }
                //if (string.IsNullOrEmpty(this.txtPWD.Text))
                //{
                //    _ShowMsg("输入登录密码！");
                //    this.txtPWD.Focus();
                //    return;
                //}
                if (this.txtPWD2.Text != this.txtPWD.Text)
                {
                    _ShowMsg("两次密码输入不相同，请重新输入确认密码！");
                    this.txtPWD2.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(this.txtEMAIL.Text))
                {
                    _ShowMsg("输入邮箱！");
                    this.txtEMAIL.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(this.txtMOBILE.Text))
                {
                    _ShowMsg("输入手机号码！");
                    this.txtMOBILE.Focus();
                    return;
                }

                MonitorClient.AppUtility.Currentsocket _Currentsocket = new Currentsocket();
                _Currentsocket.Model = MonitorClient.AppUtility.CommonList.CurrentServerInfo;
                //Model.ServerInfo _ServerInfo = new Model.ServerInfo();                    //_Currentsocket.Model = _ServerInfo;
                Hashtable hs = new Hashtable();
                hs[1] = MonitorClient.AppUtility.KOMCmd.updateUser;
                hs[2] = MonitorClient.AppUtility.KOMCmd.Succeed;
                List<CommClass.TTable> ListTtable = new List<AppUtility.CommClass.TTable>();
                //Table属性内容
                AppUtility.CommClass.TTable Ttable = new AppUtility.CommClass.TTable();
                Ttable.FieldName = "ID";
                Ttable.FieldValue = this.CurrentID.ToString();
                ListTtable.Add(Ttable);
                Ttable.FieldName = "USERID";
                Ttable.FieldValue = this.txtUSERID.Text;
                ListTtable.Add(Ttable);
                Ttable.FieldName = "USERNAME";
                Ttable.FieldValue = this.txtUSERNAME.Text.ToString();
                ListTtable.Add(Ttable);
                Ttable.FieldName = "EMAIL";
                Ttable.FieldValue = this.txtEMAIL.Text.ToString();
                ListTtable.Add(Ttable);
                Ttable.FieldName = "MOBILE";
                Ttable.FieldValue = this.txtMOBILE.Text.ToString();
                ListTtable.Add(Ttable);
                Ttable.FieldName = "USERPWD";
                Ttable.FieldValue = this.txtPWD.Text.ToString();
                ListTtable.Add(Ttable);
                Ttable.FieldName = "ROLES";
                Ttable.FieldValue = this.getRoles();
                ListTtable.Add(Ttable);
                Ttable.FieldName = "ISEMAIL";
                Ttable.FieldValue = this.cbxISEMAIL.Checked ? "1" : "0";
                ListTtable.Add(Ttable);
                Ttable.FieldName = "ISMOBILE";
                Ttable.FieldValue = this.cbxISMOBILE.Checked ? "1" : "0";
                ListTtable.Add(Ttable);

                //                <USERID>1</ USERID >
                //<USERNAME>1</ USERNAME >
                //<EMAIL></ EMAIL >
                //<ISEMAIL></IS EMAIL >
                //<MOBILE></ MOBILE >
                //<ISMOBILE></ ISMOBILE >
                //<USERPWD></ USERPWD >
                //<ROLES></ROLES>

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
                _ShowMsg("正在修改用户，请稍等……");
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
        private void btn_cencel_Click(object sender, EventArgs e)
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
                    _ShowMsg("先选择要删除的用户信息！");

                    return;
                }
             

                MonitorClient.AppUtility.Currentsocket _Currentsocket = new Currentsocket();
                _Currentsocket.Model = MonitorClient.AppUtility.CommonList.CurrentServerInfo;
                //Model.ServerInfo _ServerInfo = new Model.ServerInfo();                    //_Currentsocket.Model = _ServerInfo;
                Hashtable hs = new Hashtable();
                hs[1] = MonitorClient.AppUtility.KOMCmd.delUser;
                hs[2] = MonitorClient.AppUtility.KOMCmd.Succeed;
                List<CommClass.TTable> ListTtable = new List<AppUtility.CommClass.TTable>();
                //Table属性内容
                AppUtility.CommClass.TTable Ttable = new AppUtility.CommClass.TTable();
                Ttable.FieldName = "ID";
                Ttable.FieldValue = this.CurrentID.ToString();
                ListTtable.Add(Ttable);
              
                byte[] ByteResult = null;
                AppUtility.CommClass.TableToByteArry(ListTtable, ref ByteResult);
                //AppUtility._config.MessageBox("开始登录");
                hs[3] = ByteResult;
                _Currentsocket.SetSendData(hs);
                _Currentsocket.TcpClient = MonitorClient.AppUtility.CommonList.MonClient;
                //_Currentsocket.do_Job(null);
                ThreadPool.QueueUserWorkItem(new WaitCallback(_Currentsocket.do_Job), null);
                _ShowMsg("正在删除所选用户，请稍等……");
            }
            catch (Exception ex)
            {
                _ShowMsg(ex.Message);
            }
        }
    }
}
