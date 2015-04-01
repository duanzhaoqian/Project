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
    public partial class CtrMain : UserControl
    {
        public delegate void ReloadData(object obj);
        public static event FrMain.ShowMsg _ShowMsg;
        public CtrMain()
        {
            InitializeComponent();//FrmLogin
            FrMain._ReloadData += new ReloadData(FrMain__ReloadData);
            //FrmLogin._ReloadData += new ReloadData(FrMain__ReloadData);
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
        private void button1_Click(object sender, EventArgs e)
        {
            MonitorClient.AppUtility.ServerStateTimer.GetServertype(null);
        }
        private void FrMain__ReloadData(object obj)
        {
            //点击效果

            try
            {
                string sid = obj.ToString();
                this.CurrentID = Convert.ToInt32(sid);

                //TreeNode sNode = treeView1.SelectedNode;
                if (sid=="0")
                {

                    ////MessageBox.Show("该Node为根节点");
                    ////this.STRSCENEID =string.Empty;
                    ////this.groupBox3.Text = "";// +"    [ " + fnames + ">>" + snames + " ]";
                    //// string[] bb = sNode.Tag.ToString().Split('&');
                    //this.txttypefid.Text = sNode.Tag.ToString();
                    //this.SELECTID = Convert.ToInt32(sNode.Tag.ToString());

                    ////TreeNode fNode = sNode.Parent;
                    ////string fnames = fNode.Text;
                    ////string fid = fNode.Tag.ToString();
                    ////string snames = treeView1.SelectedNode.Text;
                    ////string sid = treeView1.SelectedNode.Tag.ToString();
                    ////////this.STRSCENEID = sid;
                    ////////this.groupBox3.Text = "" + "    [ " + fnames + ">>" + snames + " ]";
                    //////MessageBox.Show("" + "    [ " + fnames + ">>" + snames + " ]");
                    ////this.txttypefid.Text = sid;
                    foreach (Control ctr in panel3.Controls)
                    {
                        ctr.Visible = false;
                        //_MyTextBoxVisble(false, ctr);// = sid;
                    }
                    //foreach (Control ctr in panel3.Controls)
                    //{
                    //    ctr.Visible = false;
                    //}
                    //panel4.Visible = false;
                    _MyPanelVisble(false, this.panel4);// = sid;

                    
                    //this.textBox2.Visible = true;

                    _MyTextBoxVisble(true, this.textBox2);// = sid;
                }
                else
                {
                    foreach (Control ctr in panel3.Controls)
                    {
                        try
                        {
                            if (ctr.Name != "txtservertypename")
                            {
                                ctr.Visible = true;
                            }
                        }
                        catch (Exception ex)
                        { 

                        }
                    }
                    //foreach (Control ctr in panel3.Controls)
                    //{
                    //    ctr.Visible = false;
                    //}
                    _MyPanelVisble(true, this.panel4);// = sid;
                   // panel4.Visible = true;
                    //this.textBox2.Visible = false;
                    _MyTextBoxVisble(false, this.textBox2);// = sid;
                    //MessageBox.Show("该Node的父节点为：" + sNode.Parent.Text.ToString());
                    //TreeNode fNode = sNode.Parent;
                    //string fnames = fNode.Text;
                    //string fid = fNode.Tag.ToString();
                    //string snames = treeView1.SelectedNode.Text;
                    //string sid = treeView1.SelectedNode.Tag.ToString();
                    DataTable dt = MonitorClient.AppUtility.CommonList.getSingerServerTable(Convert.ToInt32(sid));
                    if (dt == null)
                        return;
                    if (dt.Rows.Count == 0)
                        return;
                    _MyTextBox(sid, this.txtid);// = sid;
                    _MyTextBox(dt.Rows[0]["SERVTYPE"].ToString(), this.txtservertypename);// = sid;
                    _MyTextBox(dt.Rows[0]["WANIP"].ToString(), this.txtserverip);// = sid;
                    _MyTextBox(dt.Rows[0]["WANPORT"].ToString(), this.txtserverport);// = sid;
                    _MyTextBox(dt.Rows[0]["NAME"].ToString(), this.txtservername);// = sid;
                    //this.txtservertypename.Text = dt.Rows[0]["SERVTYPE"].ToString();
                    //this.txtserverip.Text = dt.Rows[0]["WANIP"].ToString();
                    //this.txtserverport.Text = dt.Rows[0]["WANPORT"].ToString();
                    //this.txtservername.Text = dt.Rows[0]["NAME"].ToString();
                    //this.txtserverstate.Text = dt.Rows[0]["STATE"].ToString();
                    ////this.STRSCENEID = sid;
                    ////this.groupBox3.Text = "" + "    [ " + fnames + ">>" + snames + " ]";
                    //// MessageBox.Show("" + "    [ " + fnames + ">>" + snames + " ]");
                    ////string[] bb = sid.ToString().Split('&');
                    //this.txttypefid.Text = sid.ToString();
                    //this.SELECTID = Convert.ToInt32(sid.ToString());
                }
            }
            catch (Exception ex)
            {
                _ShowMsg(ex.Message);
            }
        }


        public void _MyTextBox(string strtext, TextBox boxtemp)
        {
            try
            {
                if (!boxtemp.InvokeRequired)//判断是否需要进行唤醒的请求，如果控件与主线程在一个线程内，可以写成if(!InvokeRequired)
                {
                    //MessageBox.Show("同一线程内");
                    boxtemp.Text = strtext;
                  
                }
                else
                {
                    //MessageBox.Show("不是同一个线程");
                    MyTextBox a1 = new MyTextBox(_MyTextBox);
                    Invoke(a1, new object[] { strtext, boxtemp });//执行唤醒操作
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



        public void _MyPanelVisble(bool isview, Panel boxtemp)
        {
            try
            {
                if (!boxtemp.InvokeRequired)//判断是否需要进行唤醒的请求，如果控件与主线程在一个线程内，可以写成if(!InvokeRequired)
                {
                    //MessageBox.Show("同一线程内");
                    boxtemp.Visible = isview;
                }
                else
                {
                    //MessageBox.Show("不是同一个线程");
                    MyPanelVisble a1 = new MyPanelVisble(_MyPanelVisble);
                    Invoke(a1, new object[] { isview, boxtemp });//执行唤醒操作
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
        public void _MyTextBoxVisble(bool isview, TextBox boxtemp)
        {
            try
            {
                if (!boxtemp.InvokeRequired)//判断是否需要进行唤醒的请求，如果控件与主线程在一个线程内，可以写成if(!InvokeRequired)
                {
                    //MessageBox.Show("同一线程内");
                    boxtemp.Visible = isview;
                }
                else
                {
                    //MessageBox.Show("不是同一个线程");
                    MyTextBoxVisble a1 = new MyTextBoxVisble(_MyTextBoxVisble);
                    Invoke(a1, new object[] { isview, boxtemp });//执行唤醒操作
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

        /// <summary>
        /// 启动
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.CurrentID == 0)
                    return;
                DataTable dt = MonitorClient.AppUtility.CommonList.getSingerServerTable(this.CurrentID);
                if (dt == null)
                {
                    dt = null;
                    return;
                }
                if (dt.Rows.Count == 0)
                {
                    dt = null;
                    return;
                }
//1：停止服务
//2：开启服务
//3：重启服务
                Model.ServerInfo model = new Model.ServerInfo();
                model.ip = dt.Rows[0]["WANIP"].ToString();
                model.port = MonitorClient.AppUtility.CommonList.CurrentServerInfo.port;// Convert.ToInt32(dt.Rows[0]["WANPORT"].ToString());
                //MonitorClient.AppUtility.CommonList.CurrentServerInfo;
               
                MonitorClient.AppUtility.Currentsocket _Currentsocket = new Currentsocket();
                _Currentsocket.Model = model;// MonitorClient.AppUtility.CommonList.CurrentServerInfo;
                Hashtable hs = new Hashtable();
                hs[1] = MonitorClient.AppUtility.KOMCmd.OperateServer;
                hs[2] = MonitorClient.AppUtility.KOMCmd.Succeed;
                List<CommClass.TTable> ListTtable = new List<AppUtility.CommClass.TTable>();
                //Table属性内容
                AppUtility.CommClass.TTable Ttable = new AppUtility.CommClass.TTable();
                Ttable.FieldName = "ID";
                Ttable.FieldValue = this.CurrentID.ToString();
                ListTtable.Add(Ttable);
                Ttable.FieldName = "SERVTYPE";
                Ttable.FieldValue = dt.Rows[0]["SERVTYPE"].ToString();
                ListTtable.Add(Ttable);
                Ttable.FieldName = "OPTTYPE";
                Ttable.FieldValue = "2";
                ListTtable.Add(Ttable);
                byte[] ByteResult = null;
                dt = null;
                AppUtility.CommClass.TableToByteArry(ListTtable, ref ByteResult);
                hs[3] = ByteResult;
                _Currentsocket.SetSendData(hs);
                //_Currentsocket.TcpClient = MonitorClient.AppUtility.CommonList.MonClient;
                ThreadPool.QueueUserWorkItem(new WaitCallback(_Currentsocket.do_Job), null);
                _ShowMsg("正在请求对服务器进行操作，请稍等……");
            }
            catch (Exception ex)
            {
                _ShowMsg(ex.Message);
            }
        }

        /// <summary>
        /// 停止
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.CurrentID == 0)
                    return;
                DataTable dt = MonitorClient.AppUtility.CommonList.getSingerServerTable(this.CurrentID);
                if (dt == null)
                {
                    dt = null;
                    return;
                }
                if (dt.Rows.Count == 0)
                {
                    dt = null;
                    return;
                }
                //1：停止服务
                //2：开启服务
                //3：重启服务
                Model.ServerInfo model = new Model.ServerInfo();
                model.ip = dt.Rows[0]["WANIP"].ToString();
                model.port = MonitorClient.AppUtility.CommonList.CurrentServerInfo.port;// Convert.ToInt32(dt.Rows[0]["WANPORT"].ToString());
               
                MonitorClient.AppUtility.Currentsocket _Currentsocket = new Currentsocket();
                _Currentsocket.Model = model;// MonitorClient.AppUtility.CommonList.CurrentServerInfo;
                Hashtable hs = new Hashtable();
                hs[1] = MonitorClient.AppUtility.KOMCmd.OperateServer;
                hs[2] = MonitorClient.AppUtility.KOMCmd.Succeed;
                List<CommClass.TTable> ListTtable = new List<AppUtility.CommClass.TTable>();
                //Table属性内容
                AppUtility.CommClass.TTable Ttable = new AppUtility.CommClass.TTable();
                Ttable.FieldName = "ID";
                Ttable.FieldValue = this.CurrentID.ToString();
                ListTtable.Add(Ttable);
                Ttable.FieldName = "SERVTYPE";
                Ttable.FieldValue = dt.Rows[0]["SERVTYPE"].ToString();
                ListTtable.Add(Ttable);
                Ttable.FieldName = "OPTTYPE";
                Ttable.FieldValue = "1";
                ListTtable.Add(Ttable);
                byte[] ByteResult = null;
                dt = null;
                AppUtility.CommClass.TableToByteArry(ListTtable, ref ByteResult);
                hs[3] = ByteResult;
                _Currentsocket.SetSendData(hs);
               // _Currentsocket.TcpClient = MonitorClient.AppUtility.CommonList.MonClient;
                ThreadPool.QueueUserWorkItem(new WaitCallback(_Currentsocket.do_Job), null);
                _ShowMsg("正在请求对服务器进行操作，请稍等……");
            }
            catch (Exception ex)
            {
                _ShowMsg(ex.Message);
            }
        }

        /// <summary>
        /// 重起
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.CurrentID == 0)
                    return;
                DataTable dt = MonitorClient.AppUtility.CommonList.getSingerServerTable(this.CurrentID);
                if (dt == null)
                {
                    dt = null;
                    return;
                }
                if (dt.Rows.Count == 0)
                {
                    dt = null;
                    return;
                }
                //1：停止服务
                //2：开启服务
                //3：重启服务
                Model.ServerInfo model = new Model.ServerInfo();
                model.ip = dt.Rows[0]["WANIP"].ToString();
                model.port = MonitorClient.AppUtility.CommonList.CurrentServerInfo.port;// Convert.ToInt32(dt.Rows[0]["WANPORT"].ToString());
               
                MonitorClient.AppUtility.Currentsocket _Currentsocket = new Currentsocket();
                _Currentsocket.Model = model;// MonitorClient.AppUtility.CommonList.CurrentServerInfo;
                Hashtable hs = new Hashtable();
                hs[1] = MonitorClient.AppUtility.KOMCmd.OperateServer;
                hs[2] = MonitorClient.AppUtility.KOMCmd.Succeed;
                List<CommClass.TTable> ListTtable = new List<AppUtility.CommClass.TTable>();
                //Table属性内容
                AppUtility.CommClass.TTable Ttable = new AppUtility.CommClass.TTable();
                Ttable.FieldName = "ID";
                Ttable.FieldValue = this.CurrentID.ToString();
                ListTtable.Add(Ttable);
                Ttable.FieldName = "SERVTYPE";
                Ttable.FieldValue = dt.Rows[0]["SERVTYPE"].ToString();
                ListTtable.Add(Ttable);
                Ttable.FieldName = "OPTTYPE";
                Ttable.FieldValue = "3";
                ListTtable.Add(Ttable);
                byte[] ByteResult = null;
                dt = null;
                AppUtility.CommClass.TableToByteArry(ListTtable, ref ByteResult);
                hs[3] = ByteResult;
                _Currentsocket.SetSendData(hs);
                //_Currentsocket.TcpClient = MonitorClient.AppUtility.CommonList.MonClient;
                ThreadPool.QueueUserWorkItem(new WaitCallback(_Currentsocket.do_Job), null);
                _ShowMsg("正在请求对服务器进行操作，请稍等……");
            }
            catch (Exception ex)
            {
                _ShowMsg(ex.Message);
            }
        }
    }
}
