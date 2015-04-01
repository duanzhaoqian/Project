using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace MonitorClient.AppUtility
{
   public delegate void ReloadData();
    public delegate void ReloadDataSelect(int id);
    public delegate void MyDropListDataBind(DataTable dt, System.Windows.Forms.ComboBox boxtemp, string strtext, string strvalue);
    public delegate void MyTreeView(System.Windows.Forms.TreeView tree1, System.Windows.Forms.TreeNodeCollection nodes, DataTable dataSource, int parentid);
    public delegate void MySystemExits();

    public delegate void MyToolStripStatusLabel(System.Windows.Forms.ToolStripStatusLabel _ToolStripStatusLabel,string text);

    public delegate void MyDataGridView(DataTable dt, System.Windows.Forms.DataGridView dataGridView1);
    public delegate void MyTextBox(string strtext, System.Windows.Forms.TextBox mytext);
    public delegate void MyTextBoxVisble(bool isview, System.Windows.Forms.TextBox mytext);
    public delegate void MyPanelVisble(bool isview, System.Windows.Forms.Panel mytext);





}