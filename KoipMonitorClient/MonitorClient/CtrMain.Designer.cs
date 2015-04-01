namespace MonitorClient
{
    partial class CtrMain
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

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.panel4 = new System.Windows.Forms.Panel();
            this.button4 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.txtserverport = new System.Windows.Forms.TextBox();
            this.txtserverip = new System.Windows.Forms.TextBox();
            this.txtservername = new System.Windows.Forms.TextBox();
            this.txtservertypename = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtid = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.panel4.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel4
            // 
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel4.Controls.Add(this.button4);
            this.panel4.Controls.Add(this.button2);
            this.panel4.Controls.Add(this.button3);
            this.panel4.Location = new System.Drawing.Point(18, 475);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(633, 48);
            this.panel4.TabIndex = 11;
            this.panel4.Visible = false;
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(417, 12);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(75, 23);
            this.button4.TabIndex = 7;
            this.button4.Text = "重启服务";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(101, 12);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 5;
            this.button2.Text = "启动服务";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(259, 12);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 6;
            this.button3.Text = "停止服务";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // panel3
            // 
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.txtserverport);
            this.panel3.Controls.Add(this.txtserverip);
            this.panel3.Controls.Add(this.txtservername);
            this.panel3.Controls.Add(this.txtservertypename);
            this.panel3.Controls.Add(this.label4);
            this.panel3.Controls.Add(this.label3);
            this.panel3.Controls.Add(this.label2);
            this.panel3.Controls.Add(this.txtid);
            this.panel3.Controls.Add(this.label6);
            this.panel3.Controls.Add(this.textBox2);
            this.panel3.ForeColor = System.Drawing.SystemColors.WindowText;
            this.panel3.Location = new System.Drawing.Point(18, 125);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(633, 336);
            this.panel3.TabIndex = 10;
            // 
            // textBox2
            // 
            this.textBox2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.textBox2.BackColor = System.Drawing.SystemColors.Window;
            this.textBox2.Font = new System.Drawing.Font("宋体", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBox2.Location = new System.Drawing.Point(59, 40);
            this.textBox2.Multiline = true;
            this.textBox2.Name = "textBox2";
            this.textBox2.ReadOnly = true;
            this.textBox2.Size = new System.Drawing.Size(526, 247);
            this.textBox2.TabIndex = 10;
            this.textBox2.Text = "欢迎使用!";
            this.textBox2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtserverport
            // 
            this.txtserverport.BackColor = System.Drawing.SystemColors.Control;
            this.txtserverport.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtserverport.Enabled = false;
            this.txtserverport.Location = new System.Drawing.Point(187, 229);
            this.txtserverport.Name = "txtserverport";
            this.txtserverport.Size = new System.Drawing.Size(250, 21);
            this.txtserverport.TabIndex = 8;
            // 
            // txtserverip
            // 
            this.txtserverip.BackColor = System.Drawing.SystemColors.Control;
            this.txtserverip.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtserverip.Enabled = false;
            this.txtserverip.Location = new System.Drawing.Point(187, 182);
            this.txtserverip.Name = "txtserverip";
            this.txtserverip.Size = new System.Drawing.Size(250, 21);
            this.txtserverip.TabIndex = 7;
            // 
            // txtservername
            // 
            this.txtservername.BackColor = System.Drawing.SystemColors.Control;
            this.txtservername.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtservername.Enabled = false;
            this.txtservername.Location = new System.Drawing.Point(187, 135);
            this.txtservername.Name = "txtservername";
            this.txtservername.Size = new System.Drawing.Size(250, 21);
            this.txtservername.TabIndex = 6;
            // 
            // txtservertypename
            // 
            this.txtservertypename.BackColor = System.Drawing.SystemColors.Control;
            this.txtservertypename.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtservertypename.Enabled = false;
            this.txtservertypename.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtservertypename.Location = new System.Drawing.Point(187, 267);
            this.txtservertypename.Name = "txtservertypename";
            this.txtservertypename.Size = new System.Drawing.Size(250, 21);
            this.txtservertypename.TabIndex = 5;
            this.txtservertypename.Visible = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(100, 231);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 3;
            this.label4.Text = "服务器端口";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(112, 184);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 2;
            this.label3.Text = "服务器IP";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(100, 137);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "服务器名称";
            // 
            // txtid
            // 
            this.txtid.BackColor = System.Drawing.SystemColors.Control;
            this.txtid.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtid.Enabled = false;
            this.txtid.Location = new System.Drawing.Point(187, 88);
            this.txtid.Name = "txtid";
            this.txtid.Size = new System.Drawing.Size(250, 21);
            this.txtid.TabIndex = 12;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(112, 90);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 12);
            this.label6.TabIndex = 11;
            this.label6.Text = "服务编号";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.textBox1);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(673, 87);
            this.panel1.TabIndex = 9;
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.SystemColors.Control;
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox1.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBox1.Location = new System.Drawing.Point(200, 32);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(343, 45);
            this.textBox1.TabIndex = 0;
            this.textBox1.Text = "爱唱服务产品监控管理系统";
            this.textBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(16, 23);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(126, 45);
            this.button1.TabIndex = 0;
            this.button1.Text = "获取实时服务信息";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // CtrMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel1);
            this.Name = "CtrMain";
            this.Size = new System.Drawing.Size(673, 522);
            this.panel4.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.TextBox txtid;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox txtserverport;
        private System.Windows.Forms.TextBox txtserverip;
        private System.Windows.Forms.TextBox txtservername;
        private System.Windows.Forms.TextBox txtservertypename;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button button1;
    }
}
