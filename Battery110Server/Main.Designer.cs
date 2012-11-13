namespace Battery110Server
{
    partial class Main
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lConNums = new System.Windows.Forms.Label();
            this.lRegNums = new System.Windows.Forms.Label();
            this.bDataLog = new System.Windows.Forms.Button();
            this.bSend = new System.Windows.Forms.Button();
            this.bCanSet = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lCnnectNums = new System.Windows.Forms.Label();
            this.lTermnalNums = new System.Windows.Forms.Label();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.tbPort = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tbIpAddress = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.bSave = new System.Windows.Forms.Button();
            this.pbListen = new System.Windows.Forms.ProgressBar();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.bActive = new System.Windows.Forms.Button();
            this.tbDbUser = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tbDatabaseAddress = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tbPass = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.tbDbPort = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.bDatabaseReset = new System.Windows.Forms.Button();
            this.pbDatabase = new System.Windows.Forms.ProgressBar();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.groupBox1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(711, 504);
            this.splitContainer1.SplitterDistance = 257;
            this.splitContainer1.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lConNums);
            this.groupBox1.Controls.Add(this.lRegNums);
            this.groupBox1.Controls.Add(this.bDataLog);
            this.groupBox1.Controls.Add(this.bSend);
            this.groupBox1.Controls.Add(this.bCanSet);
            this.groupBox1.Controls.Add(this.dataGridView1);
            this.groupBox1.Controls.Add(this.lCnnectNums);
            this.groupBox1.Controls.Add(this.lTermnalNums);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(711, 257);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "运行状态";
            // 
            // lConNums
            // 
            this.lConNums.AutoSize = true;
            this.lConNums.Location = new System.Drawing.Point(14, 90);
            this.lConNums.Name = "lConNums";
            this.lConNums.Size = new System.Drawing.Size(11, 12);
            this.lConNums.TabIndex = 5;
            this.lConNums.Text = "0";
            // 
            // lRegNums
            // 
            this.lRegNums.AutoSize = true;
            this.lRegNums.Location = new System.Drawing.Point(14, 44);
            this.lRegNums.Name = "lRegNums";
            this.lRegNums.Size = new System.Drawing.Size(11, 12);
            this.lRegNums.TabIndex = 4;
            this.lRegNums.Text = "0";
            // 
            // bDataLog
            // 
            this.bDataLog.Location = new System.Drawing.Point(604, 101);
            this.bDataLog.Name = "bDataLog";
            this.bDataLog.Size = new System.Drawing.Size(75, 23);
            this.bDataLog.TabIndex = 3;
            this.bDataLog.Text = "数据记录";
            this.bDataLog.UseVisualStyleBackColor = true;
            // 
            // bSend
            // 
            this.bSend.Location = new System.Drawing.Point(604, 61);
            this.bSend.Name = "bSend";
            this.bSend.Size = new System.Drawing.Size(75, 23);
            this.bSend.TabIndex = 3;
            this.bSend.Text = "发送信息";
            this.bSend.UseVisualStyleBackColor = true;
            this.bSend.Visible = false;
            // 
            // bCanSet
            // 
            this.bCanSet.Location = new System.Drawing.Point(604, 21);
            this.bCanSet.Name = "bCanSet";
            this.bCanSet.Size = new System.Drawing.Size(75, 23);
            this.bCanSet.TabIndex = 3;
            this.bCanSet.Text = "CAN设置";
            this.bCanSet.UseVisualStyleBackColor = true;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column4});
            this.dataGridView1.Location = new System.Drawing.Point(121, 12);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(453, 239);
            this.dataGridView1.TabIndex = 2;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "序号";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Width = 80;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "终端ID";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            // 
            // Column3
            // 
            this.Column3.HeaderText = "运行状态";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            // 
            // Column4
            // 
            this.Column4.HeaderText = "数据发送间隔（ms）";
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            this.Column4.Width = 150;
            // 
            // lCnnectNums
            // 
            this.lCnnectNums.AutoSize = true;
            this.lCnnectNums.Location = new System.Drawing.Point(13, 67);
            this.lCnnectNums.Name = "lCnnectNums";
            this.lCnnectNums.Size = new System.Drawing.Size(77, 12);
            this.lCnnectNums.TabIndex = 1;
            this.lCnnectNums.Text = "终端连接数量";
            // 
            // lTermnalNums
            // 
            this.lTermnalNums.AutoSize = true;
            this.lTermnalNums.Location = new System.Drawing.Point(13, 21);
            this.lTermnalNums.Name = "lTermnalNums";
            this.lTermnalNums.Size = new System.Drawing.Size(77, 12);
            this.lTermnalNums.TabIndex = 0;
            this.lTermnalNums.Text = "终端注册数量";
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.groupBox2);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.splitContainer3);
            this.splitContainer2.Size = new System.Drawing.Size(711, 243);
            this.splitContainer2.SplitterDistance = 101;
            this.splitContainer2.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.bActive);
            this.groupBox2.Controls.Add(this.checkBox2);
            this.groupBox2.Controls.Add(this.pbListen);
            this.groupBox2.Controls.Add(this.tbPort);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.tbIpAddress);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(711, 101);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "连接设置";
            // 
            // tbPort
            // 
            this.tbPort.Location = new System.Drawing.Point(121, 65);
            this.tbPort.Name = "tbPort";
            this.tbPort.Size = new System.Drawing.Size(176, 21);
            this.tbPort.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 70);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "端        口";
            // 
            // tbIpAddress
            // 
            this.tbIpAddress.Location = new System.Drawing.Point(121, 27);
            this.tbIpAddress.Name = "tbIpAddress";
            this.tbIpAddress.Size = new System.Drawing.Size(176, 21);
            this.tbIpAddress.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "绑定的IP地址";
            // 
            // splitContainer3
            // 
            this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer3.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer3.Location = new System.Drawing.Point(0, 0);
            this.splitContainer3.Name = "splitContainer3";
            this.splitContainer3.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.Controls.Add(this.groupBox3);
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.Controls.Add(this.checkBox1);
            this.splitContainer3.Panel2.Controls.Add(this.bSave);
            this.splitContainer3.Size = new System.Drawing.Size(711, 138);
            this.splitContainer3.SplitterDistance = 95;
            this.splitContainer3.TabIndex = 1;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.bDatabaseReset);
            this.groupBox3.Controls.Add(this.tbPass);
            this.groupBox3.Controls.Add(this.pbDatabase);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.tbDbPort);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.tbDbUser);
            this.groupBox3.Controls.Add(this.tbDatabaseAddress);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox3.Location = new System.Drawing.Point(0, 0);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(711, 95);
            this.groupBox3.TabIndex = 0;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "数据库设置";
            // 
            // checkBox1
            // 
            this.checkBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(561, 13);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(144, 16);
            this.checkBox1.TabIndex = 1;
            this.checkBox1.Text = "默认开机自动启动所有";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // bSave
            // 
            this.bSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.bSave.Location = new System.Drawing.Point(11, 9);
            this.bSave.Name = "bSave";
            this.bSave.Size = new System.Drawing.Size(113, 23);
            this.bSave.TabIndex = 0;
            this.bSave.Text = "保存配置（&Save）";
            this.bSave.UseVisualStyleBackColor = true;
            // 
            // pbListen
            // 
            this.pbListen.Location = new System.Drawing.Point(343, 65);
            this.pbListen.Name = "pbListen";
            this.pbListen.Size = new System.Drawing.Size(231, 23);
            this.pbListen.TabIndex = 4;
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Checked = true;
            this.checkBox2.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox2.Location = new System.Drawing.Point(343, 28);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(108, 16);
            this.checkBox2.TabIndex = 5;
            this.checkBox2.Text = "默认绑定所有IP";
            this.checkBox2.UseVisualStyleBackColor = true;
            // 
            // bActive
            // 
            this.bActive.Location = new System.Drawing.Point(604, 27);
            this.bActive.Name = "bActive";
            this.bActive.Size = new System.Drawing.Size(75, 23);
            this.bActive.TabIndex = 6;
            this.bActive.Text = "激  活";
            this.bActive.UseVisualStyleBackColor = true;
            // 
            // tbDbUser
            // 
            this.tbDbUser.Location = new System.Drawing.Point(121, 58);
            this.tbDbUser.Name = "tbDbUser";
            this.tbDbUser.Size = new System.Drawing.Size(176, 21);
            this.tbDbUser.TabIndex = 10;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(16, 63);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 9;
            this.label3.Text = "用  户  名";
            // 
            // tbDatabaseAddress
            // 
            this.tbDatabaseAddress.Location = new System.Drawing.Point(121, 20);
            this.tbDatabaseAddress.Name = "tbDatabaseAddress";
            this.tbDatabaseAddress.Size = new System.Drawing.Size(176, 21);
            this.tbDatabaseAddress.TabIndex = 8;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(16, 25);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 7;
            this.label4.Text = "数据库地址";
            // 
            // tbPass
            // 
            this.tbPass.Location = new System.Drawing.Point(440, 58);
            this.tbPass.Name = "tbPass";
            this.tbPass.PasswordChar = '*';
            this.tbPass.Size = new System.Drawing.Size(134, 21);
            this.tbPass.TabIndex = 14;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(335, 63);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 12);
            this.label5.TabIndex = 13;
            this.label5.Text = "密      码";
            // 
            // tbDbPort
            // 
            this.tbDbPort.Location = new System.Drawing.Point(440, 20);
            this.tbDbPort.Name = "tbDbPort";
            this.tbDbPort.Size = new System.Drawing.Size(86, 21);
            this.tbDbPort.TabIndex = 12;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(335, 25);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(65, 12);
            this.label6.TabIndex = 11;
            this.label6.Text = "数据库端口";
            // 
            // bDatabaseReset
            // 
            this.bDatabaseReset.Location = new System.Drawing.Point(604, 63);
            this.bDatabaseReset.Name = "bDatabaseReset";
            this.bDatabaseReset.Size = new System.Drawing.Size(75, 23);
            this.bDatabaseReset.TabIndex = 6;
            this.bDatabaseReset.Text = "重  置";
            this.bDatabaseReset.UseVisualStyleBackColor = true;
            // 
            // pbDatabase
            // 
            this.pbDatabase.Location = new System.Drawing.Point(561, 20);
            this.pbDatabase.Name = "pbDatabase";
            this.pbDatabase.Size = new System.Drawing.Size(118, 23);
            this.pbDatabase.TabIndex = 4;
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(711, 504);
            this.Controls.Add(this.splitContainer1);
            this.Name = "Main";
            this.Text = "Battery Server";
            this.Load += new System.EventHandler(this.Main_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel2.ResumeLayout(false);
            this.splitContainer3.Panel2.PerformLayout();
            this.splitContainer3.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.SplitContainer splitContainer3;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Button bSave;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label lConNums;
        private System.Windows.Forms.Label lRegNums;
        private System.Windows.Forms.Button bDataLog;
        private System.Windows.Forms.Button bSend;
        private System.Windows.Forms.Button bCanSet;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.Label lCnnectNums;
        private System.Windows.Forms.Label lTermnalNums;
        private System.Windows.Forms.TextBox tbPort;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbIpAddress;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button bActive;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.ProgressBar pbListen;
        private System.Windows.Forms.Button bDatabaseReset;
        private System.Windows.Forms.TextBox tbPass;
        private System.Windows.Forms.ProgressBar pbDatabase;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tbDbPort;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox tbDbUser;
        private System.Windows.Forms.TextBox tbDatabaseAddress;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
    }
}

