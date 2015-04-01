namespace MQ消息发送工具
{
    partial class MQ消息发送工具
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbMQAddress = new System.Windows.Forms.ComboBox();
            this.cmbMQName = new System.Windows.Forms.ComboBox();
            this.btnSendArrayList = new System.Windows.Forms.Button();
            this.btnSendSingle = new System.Windows.Forms.Button();
            this.cmbMsg1 = new System.Windows.Forms.ComboBox();
            this.cmbMsg2 = new System.Windows.Forms.ComboBox();
            this.cmbMsg3 = new System.Windows.Forms.ComboBox();
            this.cmbMsg4 = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(39, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "MQIP地址+端口:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(39, 54);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "MQ名称:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(39, 89);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(71, 12);
            this.label3.TabIndex = 0;
            this.label3.Text = "MQ消息内容:";
            // 
            // cmbMQAddress
            // 
            this.cmbMQAddress.FormattingEnabled = true;
            this.cmbMQAddress.Location = new System.Drawing.Point(134, 22);
            this.cmbMQAddress.Name = "cmbMQAddress";
            this.cmbMQAddress.Size = new System.Drawing.Size(281, 20);
            this.cmbMQAddress.TabIndex = 1;
            // 
            // cmbMQName
            // 
            this.cmbMQName.FormattingEnabled = true;
            this.cmbMQName.Location = new System.Drawing.Point(134, 54);
            this.cmbMQName.Name = "cmbMQName";
            this.cmbMQName.Size = new System.Drawing.Size(281, 20);
            this.cmbMQName.TabIndex = 2;
            // 
            // btnSendArrayList
            // 
            this.btnSendArrayList.Location = new System.Drawing.Point(12, 264);
            this.btnSendArrayList.Name = "btnSendArrayList";
            this.btnSendArrayList.Size = new System.Drawing.Size(230, 23);
            this.btnSendArrayList.TabIndex = 7;
            this.btnSendArrayList.Text = "发送ArrayList消息（ObjectMessage）";
            this.btnSendArrayList.UseVisualStyleBackColor = true;
            this.btnSendArrayList.Click += new System.EventHandler(this.btnSendArrayList_Click);
            // 
            // btnSendSingle
            // 
            this.btnSendSingle.Location = new System.Drawing.Point(277, 264);
            this.btnSendSingle.Name = "btnSendSingle";
            this.btnSendSingle.Size = new System.Drawing.Size(184, 23);
            this.btnSendSingle.TabIndex = 8;
            this.btnSendSingle.Text = "发送单条消息（TextMessage）";
            this.btnSendSingle.UseVisualStyleBackColor = true;
            this.btnSendSingle.Click += new System.EventHandler(this.btnSendSingle_Click);
            // 
            // cmbMsg1
            // 
            this.cmbMsg1.FormattingEnabled = true;
            this.cmbMsg1.Location = new System.Drawing.Point(134, 86);
            this.cmbMsg1.Name = "cmbMsg1";
            this.cmbMsg1.Size = new System.Drawing.Size(278, 20);
            this.cmbMsg1.TabIndex = 3;
            // 
            // cmbMsg2
            // 
            this.cmbMsg2.FormattingEnabled = true;
            this.cmbMsg2.Location = new System.Drawing.Point(134, 122);
            this.cmbMsg2.Name = "cmbMsg2";
            this.cmbMsg2.Size = new System.Drawing.Size(278, 20);
            this.cmbMsg2.TabIndex = 4;
            // 
            // cmbMsg3
            // 
            this.cmbMsg3.FormattingEnabled = true;
            this.cmbMsg3.Location = new System.Drawing.Point(134, 164);
            this.cmbMsg3.Name = "cmbMsg3";
            this.cmbMsg3.Size = new System.Drawing.Size(278, 20);
            this.cmbMsg3.TabIndex = 5;
            // 
            // cmbMsg4
            // 
            this.cmbMsg4.FormattingEnabled = true;
            this.cmbMsg4.Location = new System.Drawing.Point(134, 210);
            this.cmbMsg4.Name = "cmbMsg4";
            this.cmbMsg4.Size = new System.Drawing.Size(278, 20);
            this.cmbMsg4.TabIndex = 6;
            // 
            // MQ消息发送工具
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(473, 351);
            this.Controls.Add(this.cmbMsg4);
            this.Controls.Add(this.cmbMsg3);
            this.Controls.Add(this.cmbMsg2);
            this.Controls.Add(this.cmbMsg1);
            this.Controls.Add(this.btnSendSingle);
            this.Controls.Add(this.btnSendArrayList);
            this.Controls.Add(this.cmbMQName);
            this.Controls.Add(this.cmbMQAddress);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "MQ消息发送工具";
            this.Text = "MQ消息发送工具";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmbMQAddress;
        private System.Windows.Forms.ComboBox cmbMQName;
        private System.Windows.Forms.Button btnSendArrayList;
        private System.Windows.Forms.Button btnSendSingle;
        private System.Windows.Forms.ComboBox cmbMsg1;
        private System.Windows.Forms.ComboBox cmbMsg2;
        private System.Windows.Forms.ComboBox cmbMsg3;
        private System.Windows.Forms.ComboBox cmbMsg4;
    }
}

