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
    public partial class CtrServerRuning : UserControl
    {
        public static event FrMain.ShowMsg _ShowMsg;
        public delegate void TreeViewDataBind(DataTable dt);
        public delegate void UpdateTreeView(TreeView tree1, DataTable dt);
        public CtrServerRuning()
        {
            try
            {
                InitializeComponent();
               // MonitorClient.AppUtility.CommonList._TreeViewDataBind += new TreeViewDataBind(CommonList__TreeViewDataBind);

            }
            catch (Exception ex)
            {
                _ShowMsg(ex.Message);
            }
        }
        private void CtrServerRuning_Load(object sender, EventArgs e)
        {           
            try
            {
                //CommonList__TreeViewDataBind(null);
                MonitorClient.AppUtility.ServerStateTimer.GetServertype(null);
            }
            catch (Exception ex)
            {
                _ShowMsg(ex.Message);
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
                _ShowMsg(ex.Message);
            }
        }
        public void _MyTreeView(TreeView tree1, TreeNodeCollection nodes, DataTable dataSource, int parentid)
        {
            try
            {
                if (!tree1.InvokeRequired)//判断是否需要进行唤醒的请求，如果控件与主线程在一个线程内，可以写成if(!InvokeRequired)
                {
                    ////MessageBox.Show("同一线程内");
                    if (dataSource.Rows.Count > 0)
                    {
                        TreeNode node;
                        foreach (DataRow dr in dataSource.Rows)
                        {
                            node = new TreeNode();
                            node.Text = (string)dr["SERVTYPENAME"];
                            node.Tag = (string)dr["SERVTYPEID"];
                            nodes.Add(node);
                            if (parentid == 0)
                            {
                                //string fid = (string)dr["sceneid"];
                                //DataTable dataSource2 = getflashtype2(fid);
                                ////if (dataSource2.Rows.Count > 0)
                                ////{
                                ////    //CreateTreeViewRecursive2(node.Nodes, dataSource2, 1);
                                ////}
                                //_MyTreeView(tree1, node.Nodes, dataSource2, parentid + 1);
                                DataTable dataSource2=new DataTable ();
                                string StrMode = " sceneupid='" + node.Tag.ToString().Trim() + "' ";
                                DataRow[] dr2 = dataSource2.Select(StrMode);
                                DataTable dataSourceTempTow = dataSource.Clone();
                                foreach (DataRow rows in dr2)
                                {
                                    dataSourceTempTow.ImportRow(rows);
                                }
                                _MyTreeView(tree1, node.Nodes, dataSourceTempTow, parentid + 1);
                            }
                        }
                    }

                    //for (int i = 0; i < tree1.Nodes.Count; i++)
                    //{
                    //    //this.treeView1.Nodes[i].ExpandAll();
                    //    //if (this.treeView1.Nodes[i].Tag.ToString() == PID.ToString())
                    //    //{
                    //    tree1.Nodes[i].ExpandAll();
                    //    //    break;
                    //    //}
                    //}
                }
                else
                {
                    //MessageBox.Show("不是同一个线程");
                    MyTreeView a1 = new MyTreeView(_MyTreeView);
                    Invoke(a1, new object[] { tree1, nodes, dataSource, parentid});//执行唤醒操作
                }
            }
            catch (Exception ex)
            {
                //_config.MessageBox(ex.Message);
                //closegif();
            }
            finally
            {
               // closegif();
            }
        }

        private void MyUpdateTreeView(object obj)
        {
            try
            {
                _UpdateTreeView(this.treeView1, MonitorClient.AppUtility.CommonList.t_servertypeReal);
            }
            catch (Exception ex)
            {
                _ShowMsg(ex.Message);
            }

        }

        /// <summary>
        /// /
        /// </summary>
        /// <param name="tree1"></param>
        /// <param name="obj"></param>
        public void _UpdateTreeView(TreeView tree1, DataTable obj)
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
                    
                    TreeNode node;
                    foreach (DataRow row in obj.Rows)
                    {
                        bool notFind = true;
                        try
                        {

                            if (tree1.Nodes.Count == 0)
                            {
                                node = new TreeNode();
                                node.Text = (string)row["SERVTYPENAME"];
                                node.Tag = (string)row["SERVTYPEID"];
                                node.SelectedImageIndex = 0;
                                node.ImageIndex = 0;
                                node.ImageKey = "1.jpg";
                                node.SelectedImageKey = "1.jpg";
                                tree1.Nodes.Add(node);
                                //return;
                            }
                            else
                            {
                                foreach (TreeNode tn in tree1.Nodes)
                                {
                                    if (tn.Tag.ToString().Trim() == row["SERVTYPEID"].ToString().Trim())
                                    {
                                        tn.Text = (string)row["SERVTYPENAME"].ToString().Trim();
                                        tn.Tag = (string)row["SERVTYPEID"].ToString().Trim();
                                        notFind = false;
                                        tn.SelectedImageIndex = 1;
                                        tn.ImageIndex = 1;
                                        tn.ImageKey = "1.jpg";
                                        tn.SelectedImageKey = "1.jpg";
                                       // MessageBox.Show("已经存在对应节点，不能重复添加！");
                                        //return;
                                        break;
                                    }
                                }
                                if (notFind)
                                {
                                    node = new TreeNode();
                                    node.Text = (string)row["SERVTYPENAME"];
                                    node.Tag = (string)row["SERVTYPEID"];
                                    //node.SelectedImageIndex = 0;
                                    //node.ImageIndex = 0;
                                    node.ImageKey = "1.jpg";
                                    node.SelectedImageKey = "1.jpg";
                                    tree1.Nodes.Add(node);
                                }
                               
                                    //treeView2.Nodes.Add((TreeNode)e.Node.Clone());
                            }


                            //tree1.Nodes.Find(row["SERVTYPEID"].ToString(),false).Length>0;


                            //if (tree1.Nodes.Find(row["SERVTYPEID"].ToString(), false).Length > 0)
                            //{
                            //    node = tree1.Nodes[row["SERVTYPEID"].ToString()];
                            //    node.Text = (string)row["SERVTYPENAME"];
                            //    node.Tag = (string)row["SERVTYPEID"];
                            //}
                            //else
                            //{

                            //    node = new TreeNode();
                            //    node.Text = (string)row["SERVTYPENAME"];
                            //    node.Tag = (string)row["SERVTYPEID"];
                            //    tree1.Nodes.Add(node);
                            //}
                        }
                        catch (Exception ex)
                        {
                            _ShowMsg(ex.Message);
                        }
                    }
                    //Hashtable hs = (Hashtable)obj;
                    foreach (TreeNode tn in tree1.Nodes)
                    {
                        
                        //if (Convert.ToInt32(tn.Tag.ToString()) == Convert.ToInt32(obj.or[1].ToString().Trim()))
                        //{
                        //    int tempstate = Convert.ToInt32(hs[3].ToString().Trim());
                        //    //if (tempstate == 1)
                        //    //{
                        //    //    tn.ImageKey = "1.jpg";
                        //    //    tn.SelectedImageKey = "1.jpg";
                        //    //}
                        //    //else if (tempstate == 2)
                        //    //{
                        //    //    tn.ImageKey = "2.jpg";
                        //    //    tn.SelectedImageKey = "2.jpg";
                        //    //}
                        //    //else
                        //    //{
                        //    //    tn.ImageKey = "3.jpg";
                        //    //    tn.SelectedImageKey = "3.jpg";
                        //    //}
                        //    break;
                        //}
                      
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
                    Invoke(a1, new object[] { tree1, obj });//执行唤醒操作
                }
            }
            catch (Exception ex)
            {
                _ShowMsg(ex.Message);
            }
            finally
            {
                // closegif();
            }
        }

//        public void _UpdateTreeView(TreeView tree1, object obj)
//        {
//            try
//            {
//                //tree1.ImageList = this.imageList1;
//                //if (parentid == 0)
//                /*
                 
//                 <ROOT>
//    <ITEM>
//<ID>1</ ID >
//<TYPE></TYPE>
//        <STATE></STATE>
//    </ITEM>
//</ROOT>
//                 */
//                if (!tree1.InvokeRequired)//判断是否需要进行唤醒的请求，如果控件与主线程在一个线程内，可以写成if(!InvokeRequired)
//                {
//                    Hashtable hs = (Hashtable)obj;
//                    foreach (TreeNode tn in tree1.Nodes)
//                    {
//                        if (Convert.ToInt32(tn.Tag.ToString()) == Convert.ToInt32(hs[1].ToString().Trim()))
//                        {
//                            int tempstate = Convert.ToInt32(hs[3].ToString().Trim());
//                            if (tempstate == 1)
//                            {
//                                tn.ImageKey = "1.jpg";
//                                tn.SelectedImageKey = "1.jpg";
//                            }
//                            else if (tempstate == 2)
//                            {
//                                tn.ImageKey = "2.jpg";
//                                tn.SelectedImageKey = "2.jpg";
//                            }
//                            else
//                            {
//                                tn.ImageKey = "3.jpg";
//                                tn.SelectedImageKey = "3.jpg";
//                            }
//                            break;
//                        }
//                        UpdateTreeView2(tn, obj);
//                        //tn.Tag
//                    }

//                    for (int i = 0; i < tree1.Nodes.Count; i++)
//                    {
//                        //this.treeView1.Nodes[i].ExpandAll();
//                        //if (this.treeView1.Nodes[i].Tag.ToString() == PID.ToString())
//                        //{
//                        tree1.Nodes[i].ExpandAll();
//                        //    break;
//                        //}
//                    }
//                }
//                else
//                {
//                    //MessageBox.Show("不是同一个线程");
//                    UpdateTreeView a1 = new UpdateTreeView(_UpdateTreeView);
//                    Invoke(a1, new object[] { tree1, obj });//执行唤醒操作
//                }
//            }
//            catch (Exception ex)
//            {
//                _ShowMsg(ex.Message);
//            }
//            finally
//            {
//                // closegif();
//            }
//        }

        public void UpdateTreeView2(TreeNode node, object obj)
        {
            Hashtable hs = (Hashtable)obj;
            foreach (TreeNode n in node.Nodes)
            {
                //在这里读取n的内容 
                //System.IO.FileInfo _FileInfo = new System.IO.FileInfo(@"C:\MIC\" + n.Tag.ToString() + ".ising99");
                //if (!_FileInfo.Exists)
                //    _FileInfo.Create();

                if (Convert.ToInt32(n.Tag.ToString()) == Convert.ToInt32(hs[1].ToString().Trim()))
                {
                    int tempstate = Convert.ToInt32(hs[3].ToString().Trim());
                    if (tempstate == 1)
                    {
                        n.ImageKey = "1.jpg";
                        n.SelectedImageKey = "1.jpg";
                    }
                    else if (tempstate == 2)
                    {
                        n.ImageKey = "2.jpg";
                        n.SelectedImageKey = "2.jpg";
                    }
                    else
                    {
                        n.ImageKey = "3.jpg";
                        n.SelectedImageKey = "3.jpg";
                    }
                    break;
                }
                UpdateTreeView2(n, obj);
            }
        }
       

        private void CreateTreeView(object obj)
        {
            if (MonitorClient.AppUtility.CommonList.t_servertypeReal == null || MonitorClient.AppUtility.CommonList.t_servertypeReal.Rows.Count == 0)
            {
                //return;
                MonitorClient.AppUtility.CommonList.t_servertypeReal = new DataTable();
            }
            DataSet ds = new DataSet();
            ds.ReadXml(MonitorClient.AppUtility.localserverinfo.getlocalserverinfoPath() + "xml//t_servertype.xml");
            MonitorClient.AppUtility.CommonList.t_servertypeReal = ds.Tables[0].Copy();

            DataTable dt = MonitorClient.AppUtility.CommonList.t_servertypeReal;//
            DataTable dt2 = null;// getflashtype2("");
            //首先把第一级的行政区划取出生成TreeView的节点
            //作为递归运算的入口
            //CreateTreeViewRecursive(treeView1.Nodes, dt, 0);

            _MyTreeView(this.treeView1, this.treeView1.Nodes, dt, 1);

            //for (int i = 0; i < this.treeView1.Nodes.Count; i++)
            //{
            //    //this.treeView1.Nodes[i].ExpandAll();
            //    if (this.treeView1.Nodes[i].Tag.ToString() == PID.ToString())
            //    {
            //        this.treeView1.Nodes[i].ExpandAll();
            //        break;
            //    }
            //}
            //this.treeView1.Nodes[0].ExpandAll();


        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            //点击效果
            try
            {
                TreeNode sNode = treeView1.SelectedNode;
                if (sNode.Parent == null)
                {
                    //MessageBox.Show("该Node为根节点");
                    //this.STRSCENEID =string.Empty;
                    //this.groupBox3.Text = "";// +"    [ " + fnames + ">>" + snames + " ]";



                    TreeNode fNode = sNode.Parent;
                    string fnames = fNode.Text;
                    string fid = fNode.Tag.ToString();
                    string snames = treeView1.SelectedNode.Text;
                    string sid = treeView1.SelectedNode.Tag.ToString();
                    //this.STRSCENEID = sid;
                    //this.groupBox3.Text = "" + "    [ " + fnames + ">>" + snames + " ]";
                    MessageBox.Show("" + "    [ " + fnames + ">>" + snames + " ]");


                }
                else
                {
                    //MessageBox.Show("该Node的父节点为：" + sNode.Parent.Text.ToString());
                    TreeNode fNode = sNode.Parent;
                    string fnames = fNode.Text;
                    string fid = fNode.Tag.ToString();
                    string snames = treeView1.SelectedNode.Text;
                    string sid = treeView1.SelectedNode.Tag.ToString();
                    //this.STRSCENEID = sid;
                    //this.groupBox3.Text = "" + "    [ " + fnames + ">>" + snames + " ]";
                    MessageBox.Show("" + "    [ " + fnames + ">>" + snames + " ]");
                }
            }            
            catch (Exception ex)
            {
                _ShowMsg(ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
           
        }
    }
}
