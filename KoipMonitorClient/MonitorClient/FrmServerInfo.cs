using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using MonitorClient.AppUtility;

namespace MonitorClient
{
    public partial class FrmServerInfo : Form
    {
       private DataTable dt = new DataTable("serverinfo");
       private string directoryName = MonitorClient.AppUtility.localserverinfo.getlocalserverinfoPath();
       public static event ReloadData _ReloadData;
       public static event ReloadDataSelect _ReloadDataSelect;
       private int _CurrentID = 0;
       public int CurrentID
       {
           get { return _CurrentID; }
           set
           {
               this._CurrentID = value;
               if (value == 0)
               {
                   this.button3.Enabled = false;
                   this.button1.Text = "添加";
               }
               else
               {
                   this.button3.Enabled = true;
                   this.button1.Text = "修改";
               }
           }
       }
               
        public FrmServerInfo()
        {
            InitializeComponent();
        }

        private void FrmServerInfo_Load(object sender, EventArgs e)
        {
            try
            {                 
                System.IO.FileInfo _FileInfo = new System.IO.FileInfo(directoryName + "serverinfo.dll");
                if (_FileInfo.Exists)
                {
                    dt.ReadXml(directoryName + "serverinfo.dll");
                    //this.dataGridView1.DataSource = dt;
                }
                else
                {
                    dt.Columns.Add("ID", System.Type.GetType("System.Int32"));
                    dt.Columns["ID"].AutoIncrement = true;
                    dt.Columns["ID"].AutoIncrementSeed = 1;
                    dt.Columns["ID"].AutoIncrementStep = 1;
                    dt.Columns.Add("sname", System.Type.GetType("System.String"));
                    dt.Columns.Add("ip", System.Type.GetType("System.String"));
                    dt.Columns.Add("port", System.Type.GetType("System.String"));
                    dt.WriteXml(directoryName + "serverinfo.dll", XmlWriteMode.WriteSchema);
                    //dt.WriteXml(directoryName + "serverinfo.dll");
                   // ds.Tables.Add(dt);
                    //ds.WriteXml(directoryName + "serverinfo.dll", XmlWriteMode.WriteSchema);
                }
                _FileInfo = null;
                this.dataGridView1.DataSource = dt;
                //    _FileInfo.Create();

            }
            catch (Exception ex)
            {
                AppUtility.config.MessageBox(ex.Message);
            }
        }

      

        private void dataGridView1_UserAddedRow(object sender, DataGridViewRowEventArgs e)
        {
            MessageBox.Show("aaaaa");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(this.txtNames.Text))
                {
                    AppUtility.config.MessageBox("输入服务器名称！");
                    this.txtNames.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(this.txtip.Text))
                {

                    AppUtility.config.MessageBox("输入服务器 IP 地址 ！");
                    this.txtip.Focus();
                    return;
                }
                else
                {
                    IPAddress ip;
                    if (IPAddress.TryParse(this.txtip.Text, out ip) && this.txtip.Text.Split('.').Length == 4)
                    {
                        //MessageBox.Show("合法！");
                    }
                    else
                    {
                        //MessageBox.Show("不合法！");
                        AppUtility.config.MessageBox("输入服务器 IP 地址不合法，请重新输入 ！");
                        this.txtip.Focus();
                        return;
                    }
                }

                if (string.IsNullOrEmpty(this.txtport.Text))
                {
                    AppUtility.config.MessageBox("输入服务器端口号 ！");
                    this.txtport.Focus();
                    return;
                }
                else
                {
                    try
                    {
                        int c = Convert.ToInt32(this.txtport.Text);
                    }
                    catch (Exception ex)
                    {
                        AppUtility.config.MessageBox("输入服务器端口号不合法，请重新输入 ！");
                        this.txtport.Focus();
                        return;
                    }
                }
                if (this.CurrentID == 0)
                {
                    DataRow newrow = dt.NewRow();
                    newrow["sname"] = this.txtNames.Text;
                    newrow["ip"] = this.txtip.Text;
                    newrow["port"] = this.txtport.Text;
                    dt.Rows.Add(newrow);
                    dt.WriteXml(directoryName + "serverinfo.dll", XmlWriteMode.WriteSchema);
                    _ReloadData();
                }
                else
                {
                    foreach (DataRow rows in dt.Rows)
                    {
                        if (this.CurrentID == Convert.ToInt32(rows[0].ToString().Trim()))
                        {
                            //DataRow newrow2 = dt.NewRow();
                            rows["sname"] = this.txtNames.Text;
                            rows["ip"] = this.txtip.Text;
                            rows["port"] = this.txtport.Text;
                            break;
                        }
                    }
                    dt.WriteXml(directoryName + "serverinfo.dll", XmlWriteMode.WriteSchema);
                    _ReloadData();
                }
                foreach (Control ctr in this.groupBox1.Controls)
                {
                    if (ctr is TextBox)
                    {
                        ctr.Text = "";
                    }
                }
                this.CurrentID = 0;
            }
            catch (Exception ex)
            {
                AppUtility.config.MessageBox(ex.Message);
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
                    this.txtNames.Text = this.dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                    this.txtip.Text = this.dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                    this.txtport.Text = this.dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
                    //textBox2.Text = this.dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                AppUtility.config.MessageBox(ex.Message);
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (this.dataGridView1.Rows.Count == 0)
                    return;
                if (e.RowIndex != -1)
                {
                    int id = Convert.ToInt32(this.dataGridView1.Rows[e.RowIndex].Cells[0].Value);
                    //this.CurrentID = id;
                    _ReloadDataSelect(id);
                }
            }
            catch (Exception ex)
            {
                AppUtility.config.MessageBox(ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                // 删除前的用户确认。 
                if (MessageBox.Show("确认要删除该行数据吗？", "删除确认", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK)
                {
                    // 如果不是 OK，则取消。 
                    //e.Cancel = true;
                    return;
                }// e.Cancel = true;
                int id = Convert.ToInt32(this.CurrentID);
                foreach (DataRow rows in dt.Rows)
                {
                    if (id == Convert.ToInt32(rows[0].ToString().Trim()))
                    {
                        dt.Rows.Remove(rows);
                        break;
                    }
                }
              
                dt.WriteXml(directoryName + "serverinfo.dll", XmlWriteMode.WriteSchema);
                _ReloadData();
                foreach (Control ctr in this.groupBox1.Controls)
                {
                    if (ctr is TextBox)
                    {
                        ctr.Text = "";
                    }
                }
                this.CurrentID = 0;
               
            }
            catch (Exception ex)
            {
                AppUtility.config.MessageBox(ex.Message);
            }
        }
    }
}
