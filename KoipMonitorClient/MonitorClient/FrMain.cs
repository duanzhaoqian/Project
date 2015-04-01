using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace MonitorClient
{
    public partial class FrMain : Form
    {
        public delegate void ShowMsg(string Msg);
        public delegate void TreeViewDataBind(DataTable dt);
        public delegate void TreeViewDataBind2(DataTable dt);
        public delegate void UpdateTreeView(TreeView tree1, TreeNodeCollection nodes, DataTable dt);

        public static FrMain frmthis = null;
        public static event CtrMain.ReloadData _ReloadData;

        // MyCtrl myctrl = new MyCtrl();
        #region 变量定义
        CtrUserinfo CtrlUserinfo = new CtrUserinfo();//用户管理
        CtrUpdatePwd CtrlUpdatePwd = new CtrUpdatePwd();//更改密码
        CtrServerType CtrlServerType = new CtrServerType();//设备类型
        CtrServerinfo CtrlServerinfo = new CtrServerinfo();//设备信息
        CtrServerRuning CtrlServerRuning = new CtrServerRuning();//服务状态监控显示
        CtrMain CtrlMain = new CtrMain();//主面板
        #endregion
        enum MyCtrl
        {
            CtrlUserinfo = 1,//用户管理
            CtrlUpdatePwd = 2,//更改密码
            CtrlServerType = 3,//设备类型CtrServerType_ToolStripMenuItem
            CtrlServerinfo = 4,//设备信息CtrServerType_ToolStripMenuItem
            CtrlServerRuning = 5,//服务状态监控显示
            CtrlMain = 6,//主面板
            //CtrlAddUser = 6,
            //CtrlChangeUserPwd = 7,
            //CtrlChangeRole = 8,
            //CtrlCreateScript = 9,
            //CtrlReleaseSongBook = 10,
            //CtrlDataBackup = 11,
            //CtrlSongRanking = 12,
            //CtrlCopyright = 13,
            //CtrlSingerClass = 14,
            //CtrlAreaClass = 15,
            //CtrlLanguageClass = 16,
            //CtrlStyles = 17,
            //CtrlSongYears = 18,
            //CtrlD_Situation = 19,
            //CtrlD_Situation2 = 20,
            //CtrlDataResume = 21,
            //Ctr_AreaSinger = 22
        }

        MyCtrl myctrl = new MyCtrl();
        public FrMain()
        {            
            InitializeComponent();
            frmthis = this;            
        }

        private void FrMain_Load(object sender, EventArgs e)
        {
            //FrMain.frmthis.Dock = DockStyle.None;
            //this.Width = 0;
            //this.Height = 0;



            //this.toolStripStatusLabel1.Text = "dddddddddddddddd";

            //CtrlServerRuning

            try
            {
                //CtrServerRuning
                CtrServerRuning._ShowMsg += new ShowMsg(MyShowMsg);
                CtrServerinfo._ShowMsg += new ShowMsg(MyShowMsg);
                CtrUserinfo._ShowMsg += new ShowMsg(MyShowMsg);
                CtrServerType._ShowMsg += new ShowMsg(MyShowMsg);
                CtrUserinfo._ShowMsg += new ShowMsg(MyShowMsg);
                CtrMain._ShowMsg += new ShowMsg(MyShowMsg);
                MonitorClient.AppUtility.Currentsocket._ShowMsg += new ShowMsg(MyShowMsg);
                MonitorClient.AppUtility.CommonList._ShowMsg += new ShowMsg(MyShowMsg);
                MonitorClient.NetClassLibrary.tcpClient_ReceiveEvent._ShowMsg += new ShowMsg(MyShowMsg);
                MonitorClient.AppUtility.ServerStateTimer._ShowMsg += new ShowMsg(MyShowMsg);

                MonitorClient.NetClassLibrary._5806._ShowMsg += new ShowMsg(MyShowMsg);
                MonitorClient.NetClassLibrary._5809._ShowMsg += new ShowMsg(MyShowMsg);
                MonitorClient.NetClassLibrary._5802._ShowMsg += new ShowMsg(MyShowMsg);

                MonitorClient.NetClassLibrary._5810._ShowMsg += new ShowMsg(MyShowMsg);

                MonitorClient.NetClassLibrary._5811._ShowMsg += new ShowMsg(MyShowMsg);
                MonitorClient.NetClassLibrary._5812._ShowMsg += new ShowMsg(MyShowMsg);
                MonitorClient.NetClassLibrary._5813._ShowMsg += new ShowMsg(MyShowMsg);
                MonitorClient.NetClassLibrary._5814._ShowMsg += new ShowMsg(MyShowMsg);
                MonitorClient.NetClassLibrary._5815._ShowMsg += new ShowMsg(MyShowMsg);
                MonitorClient.NetClassLibrary._5816._ShowMsg += new ShowMsg(MyShowMsg);
                MonitorClient.NetClassLibrary._5817._ShowMsg += new ShowMsg(MyShowMsg);
                MonitorClient.NetClassLibrary._5819._ShowMsg += new ShowMsg(MyShowMsg);
                MonitorClient.NetClassLibrary._5820._ShowMsg += new ShowMsg(MyShowMsg);
                MonitorClient.NetClassLibrary._5821._ShowMsg += new ShowMsg(MyShowMsg);
                MonitorClient.NetClassLibrary._5827._ShowMsg += new ShowMsg(MyShowMsg);

                MonitorClient.AppUtility.CommonList._TreeViewDataBind += new TreeViewDataBind(CommonList__TreeViewDataBind);
                MonitorClient.AppUtility.CommonList._TreeViewDataBind2 += new TreeViewDataBind2(CommonList__TreeViewDataBind2);
                //ServerStateTimer
                FrmLogin _FrmLogin = new FrmLogin();
                _FrmLogin.Show();
                _FrmLogin.TopMost = true;
                this.Hide();
                this.ShowInTaskbar = false;
                this.TSSL_Local.Text = "爱唱服务产品管理工具";


              

                ////if (myctrl == MyCtrl.CtrlServerRuning)
                ////{
                ////    return;
                ////}
                ////myctrl = MyCtrl.CtrlServerRuning;
                //LoadCtr2(CtrlServerRuning);
                ////TSSL_Local.Text = "用户管理";
            }
            catch (Exception ex)
            {
                TSSL_Local.Text = "服务状态监控显示  " + ex.Message;
            }
        }
        private void CommonList__TreeViewDataBind(DataTable dt)
        {
            try
            {
                //this.treeView1.Nodes.Clear();
                ThreadPool.QueueUserWorkItem(new WaitCallback(MyUpdateTreeView), dt);
            }
            catch (Exception ex)
            {
                //_ShowMsg(ex.Message);
            }
        }
        private void CommonList__TreeViewDataBind2(DataTable dt)
        {
            try
            {
                //this.treeView1.Nodes.Clear();
                ThreadPool.QueueUserWorkItem(new WaitCallback(MyUpdateTreeView2), dt);
            }
            catch (Exception ex)
            {
                //_ShowMsg(ex.Message);
            }
        }
        private void MyUpdateTreeView(object obj)
        {
            try
            {
                _UpdateTreeView(this.treeView1,this.treeView1.Nodes, MonitorClient.AppUtility.CommonList.t_servertypeReal);
            }
            catch (Exception ex)
            {
                //_ShowMsg(ex.Message);
            }

        }
        private void MyUpdateTreeView2(object obj)
        {
            try
            {
                _UpdateTreeView2(this.treeView1,this.treeView1.Nodes, MonitorClient.AppUtility.CommonList.t_serverinfoReal);
            }
            catch (Exception ex)
            {
                //_ShowMsg(ex.Message);
            }

        }
        /// <summary>
        /// /
        /// </summary>
        /// <param name="tree1"></param>
        /// <param name="obj"></param>
        public void _UpdateTreeView(TreeView tree1, TreeNodeCollection nodes, DataTable obj)
        {
            try
            {
                //tree1.ImageList = this.imageList1;
                //if (parentid == 0)
                /*
                 
                 <ROOT>
	<ITEM>
<ID>1</ ID >
<TYPE></TYPE>
        <STATE></STATE>
  	</ITEM>
</ROOT>
                 */
                if (!tree1.InvokeRequired)
                {
                    int seSTATE = 0;
                   
                    foreach (DataRow row in obj.Rows)
                    {
                        TreeNode node;
                        #region aa
                        bool notFind = true;
                        try
                        {

                            if (tree1.Nodes.Count == 0)
                            {
                                node = new TreeNode();
                                node.Text = (string)row["SERVTYPENAME"];
                                node.Tag = (string)row["SERVTYPEID"];
                                node.SelectedImageIndex = seSTATE;
                                node.ImageIndex = seSTATE;
                                //node.ImageKey = "1.jpg";
                                //node.SelectedImageKey = "1.jpg";
                                tree1.Nodes.Add(node);
                                //return;
                            }
                            else
                            {
                                node = new TreeNode();
                                foreach (TreeNode tn in tree1.Nodes)
                                {
                                    if (tn.Tag.ToString().Trim() == row["SERVTYPEID"].ToString().Trim())
                                    {
                                        node = tn;
                                        tn.Text = (string)row["SERVTYPENAME"].ToString().Trim();
                                        tn.Tag = (string)row["SERVTYPEID"].ToString().Trim();
                                        notFind = false;
                                        tn.SelectedImageIndex = seSTATE;
                                        tn.ImageIndex = seSTATE;
                                        //tn.ImageKey = "1.jpg";
                                        //tn.SelectedImageKey = "1.jpg";
                                        // MessageBox.Show("已经存在对应节点，不能重复添加！");
                                        //return;
                                        break;
                                    }
                                }
                                if (notFind)
                                {
                                   
                                    node.Text = (string)row["SERVTYPENAME"];
                                    node.Tag = (string)row["SERVTYPEID"];
                                    node.SelectedImageIndex = seSTATE;
                                    node.ImageIndex = seSTATE;
                                    //node.ImageKey = "1.jpg";
                                    //node.SelectedImageKey = "1.jpg";
                                    tree1.Nodes.Add(node);
                                }
                               

                                //treeView2.Nodes.Add((TreeNode)e.Node.Clone());

                            }

                            #region 子类
                            int TYPEID = Convert.ToInt32(row["SERVTYPEID"].ToString().Trim());
                            DataTable dataSourceTempTow =MonitorClient.AppUtility.CommonList.getServerTable(TYPEID);
                            if (dataSourceTempTow != null && dataSourceTempTow.Rows.Count != 0)
                            {
                                //_MyTreeView(tree1, node.Nodes, dataSourceTempTow, parentid + 1);
                                _UpdateTreeView2(tree1, node.Nodes, dataSourceTempTow);
                            }

                            #endregion
                        }
                        catch (Exception ex)
                        {
                            MyShowMsg(ex.Message);
                        }
                        finally
                        {
 
                        }
                        #endregion
                    }
                 

                    for (int i = 0; i < tree1.Nodes.Count; i++)
                    {
                        //this.treeView1.Nodes[i].ExpandAll();
                        //if (this.treeView1.Nodes[i].Tag.ToString() == PID.ToString())
                        //{
                        tree1.Nodes[i].ExpandAll();
                        //    break;
                        //}
                    }
                }
                else
                {
                    //MessageBox.Show("不是同一个线程");
                    UpdateTreeView a1 = new UpdateTreeView(_UpdateTreeView);
                    Invoke(a1, new object[] { tree1,nodes, obj });//执行唤醒操作
                }
            }
            catch (Exception ex)
            {
                //_ShowMsg(ex.Message);
            }
            finally
            {
                // closegif();
            }
        }
        public void _UpdateTreeView2(TreeView tree1,TreeNodeCollection nodes, DataTable obj)
        {
            try
            {

                if (!tree1.InvokeRequired)
                {
                    TreeNode node;
                    foreach (DataRow row in obj.Rows)
                    {
                        int seSTATE = 0;
                        if (row["STATE"].ToString().Trim() == "0")
                        {
                            seSTATE = 2;
                        }
                        else
                        {
                            seSTATE = 1;
                        }
                        bool notFind = true;
                        try
                        {
                            if (nodes.Count == 0)
                            {
                                node = new TreeNode();
                                node.Text = (string)row["NAME"];
                                node.Tag = (string)row["ID"];
                                node.SelectedImageIndex = seSTATE;
                                node.ImageIndex = seSTATE;
                                //node.ImageKey = "1.jpg";
                                //node.SelectedImageKey = "1.jpg";
                                nodes.Add(node);
                                //return;
                            }
                            else
                            {


                                node = new TreeNode();
                                foreach (TreeNode tn in nodes)
                                {
                                    if (tn.Tag.ToString().Trim() == row["ID"].ToString().Trim())
                                    {
                                        node = tn;
                                        tn.Text = (string)row["NAME"].ToString().Trim();
                                        tn.Tag = (string)row["ID"].ToString().Trim();
                                        notFind = false;
                                        tn.SelectedImageIndex = seSTATE;
                                        tn.ImageIndex = seSTATE;
                                        //tn.ImageKey = "1.jpg";
                                        //tn.SelectedImageKey = "1.jpg";
                                        // MessageBox.Show("已经存在对应节点，不能重复添加！");
                                        //return;
                                        break;
                                    }
                                }
                                if (notFind)
                                {

                                    node.Text = (string)row["NAME"];
                                    node.Tag = (string)row["ID"];
                                    node.SelectedImageIndex = seSTATE;
                                    node.ImageIndex = seSTATE;
                                    //node.ImageKey = "1.jpg";
                                    //node.SelectedImageKey = "1.jpg";
                                    nodes.Add(node);
                                }
                               
                            }


                        }
                        catch (Exception ex)
                        {
                            //_ShowMsg(ex.Message);
                        }
                    }
                 
                    for (int i = 0; i < tree1.Nodes.Count; i++)
                    {
                        //this.treeView1.Nodes[i].ExpandAll();
                        //if (this.treeView1.Nodes[i].Tag.ToString() == PID.ToString())
                        //{
                        tree1.Nodes[i].ExpandAll();
                        //    break;
                        //}
                    }
                }
                else
                {
                    //MessageBox.Show("不是同一个线程");
                    UpdateTreeView a1 = new UpdateTreeView(_UpdateTreeView2);
                    Invoke(a1, new object[] { tree1,nodes, obj });//执行唤醒操作
                }
            }
            catch (Exception ex)
            {
                //_ShowMsg(ex.Message);
            }
            finally
            {
                // closegif();
            }
        }

        #region 动态装载控件
        /// <summary>
        /// 动态装载控件
        /// </summary>
        /// <param name="obj"></param>
        private void LoadCtr(object obj)
        {
            try
            {

                Control ctr = (Control)obj;
                this.panelMain.Controls.Clear();
                ctr.Dock = DockStyle.Fill;
                this.panelMain.Controls.Add(ctr);
                this.TSSL_Local.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return;
            }
            finally
            {

            }

        }


        private void LoadCtr2(object obj)
        {
            try
            {

                Control ctr = (Control)obj;
                this.panel_left.Controls.Clear();
                ctr.Dock = DockStyle.Fill;
                this.panel_left.Controls.Add(ctr);
                //this.TSSL_Local.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return;
            }
            finally
            {

            }

        }
        #endregion

        /// <summary>
        /// 用户管理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Mtr_ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            try
            {
                if (myctrl == MyCtrl.CtrlUserinfo)
                {
                    return;
                }
                myctrl = MyCtrl.CtrlUserinfo;
                LoadCtr(CtrlUserinfo);
                TSSL_Local.Text = "用户管理";
            }
            catch (Exception ex)
            {
                TSSL_Local.Text = "用户管理  " + ex.Message;
            }
        }

        private void ExistSystem_ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }


        public void loadMain(object obj)
        {
            try
            {
                //CtrlUserinfo.Visible = false;
                RolesInit();

                if (_ReloadData != null)
                {
                    _ReloadData("0");
                }
                try
                {
                    if (myctrl == MyCtrl.CtrlMain)
                    {
                        return;
                    }
                    myctrl = MyCtrl.CtrlMain;
                    LoadCtr(CtrlMain);
                    TSSL_Local.Text = "主窗体";
                }
                catch (Exception ex)
                {
                    TSSL_Local.Text = "主窗体  " + ex.Message;
                }
            }
            catch (Exception ex)
            {
                MyShowMsg(ex.Message);
            }
        }
        /*
    p1[0] = new ListViewItem(new string[] { "用户管理", "1" });
                p1[1] = new ListViewItem(new string[] { "更改密码", "2" });
                p1[2] = new ListViewItem(new string[] { "设备类型", "3" });
                p1[3] = new ListViewItem(new string[] { "设备信息", "4" });
                p1[4] = new ListViewItem(new string[] { "服务状态监控显示", "5" });        
         */
        public void RolesInit()
        {
            try
            {
                string str_role = "";
                string ROLES = MonitorClient.AppUtility.SessionMag.ToStringBySession(MonitorClient.AppUtility.CommonList.mySESSION, 2);
                if (string.IsNullOrEmpty(ROLES))
                {
                    UpdatePwd_ToolStripMenuItem.Enabled = false;//更新密码
                    CtrServerType_ToolStripMenuItem.Enabled = false;//设备类型
                    CtrlServerinfo_ToolStripMenuItem.Enabled = false;//设备信息
                    Mtr_ToolStripMenuItem1.Enabled = false;//用户管理
                    //ExistSystem_ToolStripMenuItem.Enabled = false;//退出
                    CtrLMain_ToolStripMenuItem.Enabled = false;//主面板
                    return;
                }
                for (int i = 0; i < ROLES.Length; i++)
                {
                    str_role = ROLES.Substring(i, 1);
                    if (i == 0)
                    {

                        if (str_role == "0")
                        {
                            Mtr_ToolStripMenuItem1.Enabled = false;
                            // UpdatePwd_ToolStripMenuItem.Enabled = false;
                        }
                    }
                    if (i == 1)
                    {
                        if (str_role == "0")
                        {
                            UpdatePwd_ToolStripMenuItem.Enabled = false;
                            //TSB_SingerInfo.Enabled = false;
                        }
                    }
                    if (i == 2)
                    {
                        if (str_role == "0")
                        {
                            CtrServerType_ToolStripMenuItem.Enabled = false;
                            // TSB_Situation.Enabled = false;
                        }
                    }
                    if (i == 3)
                    {
                        if (str_role == "0")
                        {
                            CtrlServerinfo_ToolStripMenuItem.Enabled = false;
                            //TSB_CreateSongBook.Enabled = false;

                        }
                    }
                    //if (i == 4)
                    //{
                    //    if (str_role == "0")
                    //    {
                    //        TSMI_UpDown.Enabled = false;
                    //        TSB_UpDown.Enabled = false;
                    //    }
                    //}
                    //if (i == 5)
                    //{
                    //    if (str_role == "0")
                    //    {
                    //        TSMI_AddUser.Enabled = false;
                    //    }
                    //}
                    //if (i == 6)
                    //{
                    //    if (str_role == "0")
                    //    {
                    //        TSMI_ChangePwd.Enabled = false;
                    //    }
                    //}
                    //if (i == 7)
                    //{
                    //    if (str_role == "0")
                    //    {
                    //        TSMI_CreateScript.Enabled = false;
                    //    }
                    //}
                    //if (i == 8)
                    //{
                    //    if (str_role == "0")
                    //    {
                    //        TSMI_Dictionary.Enabled = false;
                    //    }
                    //}
                    //if (i == 9)
                    //{
                    //    if (str_role == "0")
                    //    {
                    //        TSMI_ReleaseSongBook.Enabled = false;
                    //    }
                    //}
                    //if (i == 10)
                    //{
                    //    if (str_role == "0")
                    //    {
                    //        TSMI_DataBackup.Enabled = false;
                    //    }
                    //}
                    //if (i == 11)
                    //{
                    //    if (str_role == "0")
                    //    {
                    //        TSMI_DataResume.Enabled = false;
                    //    }
                    //}
                    //if (i == 12)
                    //{
                    //    if (str_role == "0")
                    //    {
                    //        TSMI_SongRanking.Enabled = false;
                    //        TSB_SongRanking.Enabled = false;
                    //    }
                    //}
                    //if (i == 14)
                    //{
                    //    if (str_role == "0")
                    //    {
                    //        TSMI_SongInfo.Enabled = false;
                    //    }
                    //}
                    //if (i == 15)
                    //{
                    //    if (str_role == "0")
                    //    {
                    //        TSMI_SongInfo.Enabled = false;
                    //    }
                    //}
                    //if (i == 0)
                    //{
                    //    if (str_role == "0")
                    //    {
                    //        TSMI_SongInfo.Enabled = false;
                    //    }
                    //}
                }
            }
            catch (Exception ex)
            {
                MyShowMsg(ex.Message);
            }
        }
        /// <summary>
        /// 更改密码
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UpdatePwd_ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (myctrl == MyCtrl.CtrlUpdatePwd)
                {
                    return;
                }
                myctrl = MyCtrl.CtrlUpdatePwd;
                LoadCtr(CtrlUpdatePwd);
                TSSL_Local.Text = "更改密码";
            }
            catch (Exception ex)
            {
                TSSL_Local.Text = "更改密码  " + ex.Message;
            }
        }

        private void CtrServerType_ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (myctrl == MyCtrl.CtrlServerType)
                {
                    return;
                }
                myctrl = MyCtrl.CtrlServerType;
                LoadCtr(CtrlServerType);
                TSSL_Local.Text = "设备类型";
            }
            catch (Exception ex)
            {
                TSSL_Local.Text = "设备类型  " + ex.Message;
            }
        }

        private void CtrlServerinfo_ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (myctrl == MyCtrl.CtrlServerinfo)
                {
                    return;
                }
                myctrl = MyCtrl.CtrlServerinfo;
                LoadCtr(CtrlServerinfo);
                TSSL_Local.Text = "设备信息";
            }
            catch (Exception ex)
            {
                TSSL_Local.Text = "设备信息  " + ex.Message;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //ThreadPool.QueueUserWorkItem(new WaitCallback(aa), null);
            try
            {
                MonitorClient.AppUtility.ServerStateTimer.GetServertype(null);
                //Thread.Sleep(100);
                //MonitorClient.AppUtility.ServerStateTimer.GetServerInfo(null);
            }
            catch (Exception ex)
            {
                MyShowMsg(ex.Message);
            }
        }
        private void aa(object obj)
        {
            Random rd = new Random();
           string cc= rd.Next(1, int.MaxValue).ToString();

           MyShowMsg("设备信息" + cc);
        }


        private void MyShowMsg(string Msg)
        {
            try
            {
                toolStripStatusLabel2.Text = Msg;
            }
            catch (Exception ex)
            {
                toolStripStatusLabel2.Text = ex.Message;
            }
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            //点击效果

            try
            {


                TreeNode sNode = treeView1.SelectedNode;
                if (sNode.Parent == null)
                {

                    //////MessageBox.Show("该Node为根节点");
                    //////this.STRSCENEID =string.Empty;
                    //////this.groupBox3.Text = "";// +"    [ " + fnames + ">>" + snames + " ]";
                    ////// string[] bb = sNode.Tag.ToString().Split('&');
                    ////this.txttypefid.Text = sNode.Tag.ToString();
                    ////this.SELECTID = Convert.ToInt32(sNode.Tag.ToString());

                    //////TreeNode fNode = sNode.Parent;
                    //////string fnames = fNode.Text;
                    //////string fid = fNode.Tag.ToString();
                    //////string snames = treeView1.SelectedNode.Text;
                    //////string sid = treeView1.SelectedNode.Tag.ToString();
                    //////////this.STRSCENEID = sid;
                    //////////this.groupBox3.Text = "" + "    [ " + fnames + ">>" + snames + " ]";
                    ////////MessageBox.Show("" + "    [ " + fnames + ">>" + snames + " ]");
                    //////this.txttypefid.Text = sid;
                    //foreach (Control ctr in panel3.Controls)
                    //{
                    //    ctr.Visible = false;
                    //}
                    ////foreach (Control ctr in panel3.Controls)
                    ////{
                    ////    ctr.Visible = false;
                    ////}
                    //panel4.Visible = false;
                    //this.textBox2.Visible = true;
                    if (_ReloadData != null)
                    {
                        _ReloadData("0");
                    }

                }
                else
                {
                    //foreach (Control ctr in panel3.Controls)
                    //{
                    //    ctr.Visible = true;
                    //}
                    ////foreach (Control ctr in panel3.Controls)
                    ////{
                    ////    ctr.Visible = false;
                    ////}
                    //panel4.Visible = true;
                    //this.textBox2.Visible = false;
                    ////MessageBox.Show("该Node的父节点为：" + sNode.Parent.Text.ToString());
                    ////TreeNode fNode = sNode.Parent;
                    ////string fnames = fNode.Text;
                    ////string fid = fNode.Tag.ToString();
                    string snames = treeView1.SelectedNode.Text;
                    string sid = treeView1.SelectedNode.Tag.ToString();
                    if (_ReloadData != null)
                    {
                        _ReloadData(sid);
                    }
                    //DataTable dt = MonitorClient.AppUtility.CommonList.getSingerServerTable(Convert.ToInt32(sid));
                    //if (dt == null)
                    //    return;
                    //if (dt.Rows.Count == 0)
                    //    return;
                    //this.txtid.Text = sid;
                    //this.txtservertypename.Text = dt.Rows[0]["SERVTYPE"].ToString();
                    //this.txtserverip.Text = dt.Rows[0]["WANIP"].ToString();
                    //this.txtserverport.Text = dt.Rows[0]["WANPORT"].ToString();
                    //this.txtservername.Text = dt.Rows[0]["NAME"].ToString();
                    //this.txtserverstate.Text = dt.Rows[0]["STATE"].ToString();
                    //////this.STRSCENEID = sid;
                    //////this.groupBox3.Text = "" + "    [ " + fnames + ">>" + snames + " ]";
                    ////// MessageBox.Show("" + "    [ " + fnames + ">>" + snames + " ]");
                    //////string[] bb = sid.ToString().Split('&');
                    ////this.txttypefid.Text = sid.ToString();
                    ////this.SELECTID = Convert.ToInt32(sid.ToString());
                }
                try
                {
                    if (myctrl == MyCtrl.CtrlMain)
                    {
                        return;
                    }
                    myctrl = MyCtrl.CtrlMain;
                    LoadCtr(CtrlMain);
                    TSSL_Local.Text = "主窗体";
                }
                catch (Exception ex)
                {
                    TSSL_Local.Text = "主窗体  " + ex.Message;
                }
            }
            catch (Exception ex)
            {
                MyShowMsg(ex.Message);
            }
            finally
            {
 
            }
        }

        private void CtrLMain_ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (myctrl == MyCtrl.CtrlMain)
                {
                    return;
                }
                myctrl = MyCtrl.CtrlMain;
                LoadCtr(CtrlMain);
                TSSL_Local.Text = "主窗体";
            }
            catch (Exception ex)
            {
                TSSL_Local.Text = "主窗体  " + ex.Message;
            }
        }

        
    }
}
