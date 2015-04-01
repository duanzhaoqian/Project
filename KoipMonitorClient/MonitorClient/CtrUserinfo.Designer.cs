namespace MonitorClient
{
    partial class CtrUserinfo
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.button6 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.listView_UserRoles = new System.Windows.Forms.ListView();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label_pages = new System.Windows.Forms.Label();
            this.button4 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.txtUSERIDsk = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.button7 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.btn_cencel = new System.Windows.Forms.Button();
            this.btn_update = new System.Windows.Forms.Button();
            this.cbxISMOBILE = new System.Windows.Forms.CheckBox();
            this.cbxISEMAIL = new System.Windows.Forms.CheckBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtMOBILE = new System.Windows.Forms.TextBox();
            this.txtEMAIL = new System.Windows.Forms.TextBox();
            this.txtPWD2 = new System.Windows.Forms.TextBox();
            this.txtPWD = new System.Windows.Forms.TextBox();
            this.txtUSERNAME = new System.Windows.Forms.TextBox();
            this.txtUSERID = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.CID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.USERID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.USERNAME = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EMAIL = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CISEMAIL = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MOBILE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CISMOBILE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CROLES = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CPWD = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.button6);
            this.groupBox1.Controls.Add(this.button5);
            this.groupBox1.Controls.Add(this.listView_UserRoles);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Right;
            this.groupBox1.Location = new System.Drawing.Point(770, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(134, 600);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "选择用户权限";
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(7, 400);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(75, 23);
            this.button6.TabIndex = 18;
            this.button6.Text = "全部取消";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(7, 371);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(75, 23);
            this.button5.TabIndex = 17;
            this.button5.Text = "全部选择";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // listView_UserRoles
            // 
            this.listView_UserRoles.Dock = System.Windows.Forms.DockStyle.Top;
            this.listView_UserRoles.Location = new System.Drawing.Point(3, 17);
            this.listView_UserRoles.Name = "listView_UserRoles";
            this.listView_UserRoles.Size = new System.Drawing.Size(128, 347);
            this.listView_UserRoles.TabIndex = 16;
            this.listView_UserRoles.UseCompatibleStateImageBehavior = false;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dataGridView1);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(770, 183);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.CID,
            this.USERID,
            this.USERNAME,
            this.EMAIL,
            this.CISEMAIL,
            this.MOBILE,
            this.CISMOBILE,
            this.CROLES,
            this.CPWD});
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(3, 17);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(764, 163);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellDoubleClick);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label_pages);
            this.groupBox3.Controls.Add(this.button4);
            this.groupBox3.Controls.Add(this.button3);
            this.groupBox3.Controls.Add(this.button1);
            this.groupBox3.Controls.Add(this.txtUSERIDsk);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox3.Location = new System.Drawing.Point(0, 183);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(770, 47);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "查询用户信息";
            // 
            // label_pages
            // 
            this.label_pages.AutoSize = true;
            this.label_pages.Location = new System.Drawing.Point(453, 23);
            this.label_pages.Name = "label_pages";
            this.label_pages.Size = new System.Drawing.Size(29, 12);
            this.label_pages.TabIndex = 5;
            this.label_pages.Text = "页码";
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(407, 18);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(40, 23);
            this.button4.TabIndex = 4;
            this.button4.Text = "下页";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(360, 18);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(40, 23);
            this.button3.TabIndex = 3;
            this.button3.Text = "上页";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(264, 18);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "查询";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // txtUSERIDsk
            // 
            this.txtUSERIDsk.Location = new System.Drawing.Point(97, 20);
            this.txtUSERIDsk.Name = "txtUSERIDsk";
            this.txtUSERIDsk.Size = new System.Drawing.Size(160, 21);
            this.txtUSERIDsk.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(40, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "用户ID";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.button7);
            this.groupBox4.Controls.Add(this.button2);
            this.groupBox4.Controls.Add(this.btn_cencel);
            this.groupBox4.Controls.Add(this.btn_update);
            this.groupBox4.Controls.Add(this.cbxISMOBILE);
            this.groupBox4.Controls.Add(this.cbxISEMAIL);
            this.groupBox4.Controls.Add(this.label7);
            this.groupBox4.Controls.Add(this.label6);
            this.groupBox4.Controls.Add(this.txtMOBILE);
            this.groupBox4.Controls.Add(this.txtEMAIL);
            this.groupBox4.Controls.Add(this.txtPWD2);
            this.groupBox4.Controls.Add(this.txtPWD);
            this.groupBox4.Controls.Add(this.txtUSERNAME);
            this.groupBox4.Controls.Add(this.txtUSERID);
            this.groupBox4.Controls.Add(this.label5);
            this.groupBox4.Controls.Add(this.label4);
            this.groupBox4.Controls.Add(this.label3);
            this.groupBox4.Controls.Add(this.label2);
            this.groupBox4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox4.Location = new System.Drawing.Point(0, 230);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(770, 370);
            this.groupBox4.TabIndex = 3;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "编辑用户信息";
            // 
            // button7
            // 
            this.button7.Location = new System.Drawing.Point(360, 202);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(75, 23);
            this.button7.TabIndex = 17;
            this.button7.Text = "重置";
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Click += new System.EventHandler(this.button7_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(254, 203);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 16;
            this.button2.Text = "修改";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // btn_cencel
            // 
            this.btn_cencel.Location = new System.Drawing.Point(151, 203);
            this.btn_cencel.Name = "btn_cencel";
            this.btn_cencel.Size = new System.Drawing.Size(75, 23);
            this.btn_cencel.TabIndex = 15;
            this.btn_cencel.Text = "删除";
            this.btn_cencel.UseVisualStyleBackColor = true;
            this.btn_cencel.Click += new System.EventHandler(this.btn_cencel_Click);
            // 
            // btn_update
            // 
            this.btn_update.Location = new System.Drawing.Point(48, 203);
            this.btn_update.Name = "btn_update";
            this.btn_update.Size = new System.Drawing.Size(75, 23);
            this.btn_update.TabIndex = 14;
            this.btn_update.Text = "添加";
            this.btn_update.UseVisualStyleBackColor = true;
            this.btn_update.Click += new System.EventHandler(this.btn_update_Click);
            // 
            // cbxISMOBILE
            // 
            this.cbxISMOBILE.AutoSize = true;
            this.cbxISMOBILE.Location = new System.Drawing.Point(264, 168);
            this.cbxISMOBILE.Name = "cbxISMOBILE";
            this.cbxISMOBILE.Size = new System.Drawing.Size(96, 16);
            this.cbxISMOBILE.TabIndex = 13;
            this.cbxISMOBILE.Text = "是否接收短信";
            this.cbxISMOBILE.UseVisualStyleBackColor = true;
            // 
            // cbxISEMAIL
            // 
            this.cbxISEMAIL.AutoSize = true;
            this.cbxISEMAIL.Location = new System.Drawing.Point(264, 140);
            this.cbxISEMAIL.Name = "cbxISEMAIL";
            this.cbxISEMAIL.Size = new System.Drawing.Size(96, 16);
            this.cbxISEMAIL.TabIndex = 12;
            this.cbxISEMAIL.Text = "是否接收邮件";
            this.cbxISEMAIL.UseVisualStyleBackColor = true;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(46, 174);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(35, 12);
            this.label7.TabIndex = 11;
            this.label7.Text = "手 机";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(46, 145);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(35, 12);
            this.label6.TabIndex = 10;
            this.label6.Text = "邮 箱";
            // 
            // txtMOBILE
            // 
            this.txtMOBILE.Location = new System.Drawing.Point(97, 166);
            this.txtMOBILE.Name = "txtMOBILE";
            this.txtMOBILE.Size = new System.Drawing.Size(160, 21);
            this.txtMOBILE.TabIndex = 9;
            // 
            // txtEMAIL
            // 
            this.txtEMAIL.Location = new System.Drawing.Point(97, 138);
            this.txtEMAIL.Name = "txtEMAIL";
            this.txtEMAIL.Size = new System.Drawing.Size(160, 21);
            this.txtEMAIL.TabIndex = 8;
            // 
            // txtPWD2
            // 
            this.txtPWD2.Location = new System.Drawing.Point(97, 110);
            this.txtPWD2.Name = "txtPWD2";
            this.txtPWD2.PasswordChar = '*';
            this.txtPWD2.Size = new System.Drawing.Size(160, 21);
            this.txtPWD2.TabIndex = 7;
            // 
            // txtPWD
            // 
            this.txtPWD.Location = new System.Drawing.Point(97, 82);
            this.txtPWD.Name = "txtPWD";
            this.txtPWD.PasswordChar = '*';
            this.txtPWD.Size = new System.Drawing.Size(160, 21);
            this.txtPWD.TabIndex = 6;
            // 
            // txtUSERNAME
            // 
            this.txtUSERNAME.Location = new System.Drawing.Point(97, 54);
            this.txtUSERNAME.Name = "txtUSERNAME";
            this.txtUSERNAME.Size = new System.Drawing.Size(160, 21);
            this.txtUSERNAME.TabIndex = 5;
            // 
            // txtUSERID
            // 
            this.txtUSERID.Location = new System.Drawing.Point(97, 26);
            this.txtUSERID.Name = "txtUSERID";
            this.txtUSERID.Size = new System.Drawing.Size(160, 21);
            this.txtUSERID.TabIndex = 4;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(28, 116);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 12);
            this.label5.TabIndex = 3;
            this.label5.Text = "确认密码";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(28, 87);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 2;
            this.label4.Text = "登录密码";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(40, 58);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 1;
            this.label3.Text = "用户名";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(40, 29);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "登录ID";
            // 
            // CID
            // 
            this.CID.DataPropertyName = "ID";
            this.CID.HeaderText = "用户ID";
            this.CID.Name = "CID";
            this.CID.ReadOnly = true;
            // 
            // USERID
            // 
            this.USERID.DataPropertyName = "USERID";
            this.USERID.HeaderText = "登录ID";
            this.USERID.Name = "USERID";
            this.USERID.ReadOnly = true;
            // 
            // USERNAME
            // 
            this.USERNAME.DataPropertyName = "USERNAME";
            this.USERNAME.HeaderText = "用户名";
            this.USERNAME.Name = "USERNAME";
            this.USERNAME.ReadOnly = true;
            // 
            // EMAIL
            // 
            this.EMAIL.DataPropertyName = "EMAIL";
            this.EMAIL.HeaderText = "邮箱";
            this.EMAIL.Name = "EMAIL";
            this.EMAIL.ReadOnly = true;
            // 
            // CISEMAIL
            // 
            this.CISEMAIL.DataPropertyName = "ISEMAIL";
            this.CISEMAIL.HeaderText = "是否接收邮件";
            this.CISEMAIL.Name = "CISEMAIL";
            this.CISEMAIL.ReadOnly = true;
            // 
            // MOBILE
            // 
            this.MOBILE.DataPropertyName = "MOBILE";
            this.MOBILE.HeaderText = "手机";
            this.MOBILE.Name = "MOBILE";
            this.MOBILE.ReadOnly = true;
            // 
            // CISMOBILE
            // 
            this.CISMOBILE.DataPropertyName = "ISMOBILE";
            this.CISMOBILE.HeaderText = "是否接收短信";
            this.CISMOBILE.Name = "CISMOBILE";
            this.CISMOBILE.ReadOnly = true;
            // 
            // CROLES
            // 
            this.CROLES.DataPropertyName = "ROLES";
            this.CROLES.HeaderText = "权限";
            this.CROLES.Name = "CROLES";
            this.CROLES.ReadOnly = true;
            // 
            // CPWD
            // 
            this.CPWD.DataPropertyName = "USERPWD";
            this.CPWD.HeaderText = "密码";
            this.CPWD.Name = "CPWD";
            this.CPWD.ReadOnly = true;
            // 
            // CtrUserinfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "CtrUserinfo";
            this.Size = new System.Drawing.Size(904, 600);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.ListView listView_UserRoles;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox txtUSERIDsk;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtMOBILE;
        private System.Windows.Forms.TextBox txtEMAIL;
        private System.Windows.Forms.TextBox txtPWD2;
        private System.Windows.Forms.TextBox txtPWD;
        private System.Windows.Forms.TextBox txtUSERNAME;
        private System.Windows.Forms.TextBox txtUSERID;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox cbxISMOBILE;
        private System.Windows.Forms.CheckBox cbxISEMAIL;
        private System.Windows.Forms.Button btn_cencel;
        private System.Windows.Forms.Button btn_update;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Label label_pages;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.DataGridViewTextBoxColumn CID;
        private System.Windows.Forms.DataGridViewTextBoxColumn USERID;
        private System.Windows.Forms.DataGridViewTextBoxColumn USERNAME;
        private System.Windows.Forms.DataGridViewTextBoxColumn EMAIL;
        private System.Windows.Forms.DataGridViewTextBoxColumn CISEMAIL;
        private System.Windows.Forms.DataGridViewTextBoxColumn MOBILE;
        private System.Windows.Forms.DataGridViewTextBoxColumn CISMOBILE;
        private System.Windows.Forms.DataGridViewTextBoxColumn CROLES;
        private System.Windows.Forms.DataGridViewTextBoxColumn CPWD;

    }
}
