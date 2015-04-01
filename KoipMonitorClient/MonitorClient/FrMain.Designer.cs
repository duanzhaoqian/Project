namespace MonitorClient
{
    partial class FrMain
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrMain));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.Mtr_ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Mtr_ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.UpdatePwd_ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.设备类型ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.CtrServerType_ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.设备管理ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.CtrlServerinfo_ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ExistSystem_ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.CtrLMain_ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel_left = new System.Windows.Forms.Panel();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.panelMain = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.TSSL_Local = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.menuStrip1.SuspendLayout();
            this.panel_left.SuspendLayout();
            this.panelMain.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Mtr_ToolStripMenuItem,
            this.设备类型ToolStripMenuItem,
            this.设备管理ToolStripMenuItem,
            this.CtrLMain_ToolStripMenuItem,
            this.ExistSystem_ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1018, 25);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // Mtr_ToolStripMenuItem
            // 
            this.Mtr_ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Mtr_ToolStripMenuItem1,
            this.UpdatePwd_ToolStripMenuItem});
            this.Mtr_ToolStripMenuItem.Name = "Mtr_ToolStripMenuItem";
            this.Mtr_ToolStripMenuItem.Size = new System.Drawing.Size(68, 21);
            this.Mtr_ToolStripMenuItem.Text = "用户管理";
            // 
            // Mtr_ToolStripMenuItem1
            // 
            this.Mtr_ToolStripMenuItem1.Name = "Mtr_ToolStripMenuItem1";
            this.Mtr_ToolStripMenuItem1.Size = new System.Drawing.Size(124, 22);
            this.Mtr_ToolStripMenuItem1.Text = "用户管理";
            this.Mtr_ToolStripMenuItem1.Click += new System.EventHandler(this.Mtr_ToolStripMenuItem1_Click);
            // 
            // UpdatePwd_ToolStripMenuItem
            // 
            this.UpdatePwd_ToolStripMenuItem.Name = "UpdatePwd_ToolStripMenuItem";
            this.UpdatePwd_ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.UpdatePwd_ToolStripMenuItem.Text = "更新密码";
            this.UpdatePwd_ToolStripMenuItem.Click += new System.EventHandler(this.UpdatePwd_ToolStripMenuItem_Click);
            // 
            // 设备类型ToolStripMenuItem
            // 
            this.设备类型ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.CtrServerType_ToolStripMenuItem});
            this.设备类型ToolStripMenuItem.Name = "设备类型ToolStripMenuItem";
            this.设备类型ToolStripMenuItem.Size = new System.Drawing.Size(68, 21);
            this.设备类型ToolStripMenuItem.Text = "设备类型";
            // 
            // CtrServerType_ToolStripMenuItem
            // 
            this.CtrServerType_ToolStripMenuItem.Name = "CtrServerType_ToolStripMenuItem";
            this.CtrServerType_ToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.CtrServerType_ToolStripMenuItem.Text = "设备类型信息管理";
            this.CtrServerType_ToolStripMenuItem.Click += new System.EventHandler(this.CtrServerType_ToolStripMenuItem_Click);
            // 
            // 设备管理ToolStripMenuItem
            // 
            this.设备管理ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.CtrlServerinfo_ToolStripMenuItem});
            this.设备管理ToolStripMenuItem.Name = "设备管理ToolStripMenuItem";
            this.设备管理ToolStripMenuItem.Size = new System.Drawing.Size(68, 21);
            this.设备管理ToolStripMenuItem.Text = "设备管理";
            // 
            // CtrlServerinfo_ToolStripMenuItem
            // 
            this.CtrlServerinfo_ToolStripMenuItem.Name = "CtrlServerinfo_ToolStripMenuItem";
            this.CtrlServerinfo_ToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.CtrlServerinfo_ToolStripMenuItem.Text = "设备信息管理";
            this.CtrlServerinfo_ToolStripMenuItem.Click += new System.EventHandler(this.CtrlServerinfo_ToolStripMenuItem_Click);
            // 
            // ExistSystem_ToolStripMenuItem
            // 
            this.ExistSystem_ToolStripMenuItem.Name = "ExistSystem_ToolStripMenuItem";
            this.ExistSystem_ToolStripMenuItem.Size = new System.Drawing.Size(68, 21);
            this.ExistSystem_ToolStripMenuItem.Text = "退出系统";
            this.ExistSystem_ToolStripMenuItem.Click += new System.EventHandler(this.ExistSystem_ToolStripMenuItem_Click);
            // 
            // CtrLMain_ToolStripMenuItem
            // 
            this.CtrLMain_ToolStripMenuItem.Name = "CtrLMain_ToolStripMenuItem";
            this.CtrLMain_ToolStripMenuItem.Size = new System.Drawing.Size(56, 21);
            this.CtrLMain_ToolStripMenuItem.Text = "主面板";
            this.CtrLMain_ToolStripMenuItem.Click += new System.EventHandler(this.CtrLMain_ToolStripMenuItem_Click);
            // 
            // panel_left
            // 
            this.panel_left.BackColor = System.Drawing.SystemColors.Control;
            this.panel_left.Controls.Add(this.treeView1);
            this.panel_left.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel_left.Location = new System.Drawing.Point(0, 25);
            this.panel_left.Name = "panel_left";
            this.panel_left.Size = new System.Drawing.Size(303, 615);
            this.panel_left.TabIndex = 1;
            // 
            // treeView1
            // 
            this.treeView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView1.ImageIndex = 0;
            this.treeView1.ImageList = this.imageList1;
            this.treeView1.Location = new System.Drawing.Point(0, 0);
            this.treeView1.Name = "treeView1";
            this.treeView1.SelectedImageIndex = 0;
            this.treeView1.Size = new System.Drawing.Size(303, 615);
            this.treeView1.TabIndex = 0;
            this.treeView1.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterSelect);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "7.png");
            this.imageList1.Images.SetKeyName(1, "1.jpg");
            this.imageList1.Images.SetKeyName(2, "2.jpg");
            // 
            // panelMain
            // 
            this.panelMain.Controls.Add(this.panel2);
            this.panelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMain.Location = new System.Drawing.Point(303, 25);
            this.panelMain.Name = "panelMain";
            this.panelMain.Size = new System.Drawing.Size(715, 615);
            this.panelMain.TabIndex = 2;
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(715, 1);
            this.panel2.TabIndex = 3;
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.TSSL_Local});
            this.statusStrip.Location = new System.Drawing.Point(0, 640);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(1018, 22);
            this.statusStrip.TabIndex = 3;
            this.statusStrip.Text = "曲库管理系统";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(56, 17);
            this.toolStripStatusLabel1.Text = "当前位置";
            // 
            // TSSL_Local
            // 
            this.TSSL_Local.Name = "TSSL_Local";
            this.TSSL_Local.Size = new System.Drawing.Size(0, 17);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel2});
            this.statusStrip1.Location = new System.Drawing.Point(303, 618);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(715, 22);
            this.statusStrip1.TabIndex = 4;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(0, 17);
            // 
            // FrMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1018, 662);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.panelMain);
            this.Controls.Add(this.panel_left);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FrMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "爱唱服务管理系统";
            this.Load += new System.EventHandler(this.FrMain_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.panel_left.ResumeLayout(false);
            this.panelMain.ResumeLayout(false);
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem Mtr_ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 设备类型ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 设备管理ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem UpdatePwd_ToolStripMenuItem;//更新密码
        private System.Windows.Forms.ToolStripMenuItem CtrServerType_ToolStripMenuItem;//设备类型
        private System.Windows.Forms.ToolStripMenuItem CtrlServerinfo_ToolStripMenuItem;//设备信息
        private System.Windows.Forms.ToolStripMenuItem Mtr_ToolStripMenuItem1;//用户管理
        private System.Windows.Forms.ToolStripMenuItem ExistSystem_ToolStripMenuItem;//退出
        private System.Windows.Forms.ToolStripMenuItem CtrLMain_ToolStripMenuItem;//主面板

        private System.Windows.Forms.Panel panel_left;
        private System.Windows.Forms.Panel panelMain;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel TSSL_Local;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        public System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.Panel panel2;
    }
}

