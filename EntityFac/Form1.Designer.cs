namespace EntityFac
{
    partial class Form1
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
            this.btnConnectSQL = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtConnection = new System.Windows.Forms.TextBox();
            this.btnCreateFile = new System.Windows.Forms.Button();
            this.txtAddress = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnQueryTable = new System.Windows.Forms.Button();
            this.cbxDataBase = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtAccount = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtServerAddress = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtPrefix = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtNameSpace = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.btnSelectPath = new System.Windows.Forms.Button();
            this.btnSelectAll = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.chkTables = new System.Windows.Forms.CheckedListBox();
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.cbxTable = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.cbxCodeType = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.btn_CreateCode = new System.Windows.Forms.Button();
            this.rtxtCode = new System.Windows.Forms.RichTextBox();
            this.chxCopy = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnConnectSQL
            // 
            this.btnConnectSQL.Location = new System.Drawing.Point(856, 14);
            this.btnConnectSQL.Name = "btnConnectSQL";
            this.btnConnectSQL.Size = new System.Drawing.Size(148, 26);
            this.btnConnectSQL.TabIndex = 0;
            this.btnConnectSQL.Text = "连接服务器";
            this.btnConnectSQL.UseVisualStyleBackColor = true;
            this.btnConnectSQL.Click += new System.EventHandler(this.btnConnectSQL_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(24, 93);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "数据库链接";
            // 
            // txtConnection
            // 
            this.txtConnection.Location = new System.Drawing.Point(95, 90);
            this.txtConnection.Name = "txtConnection";
            this.txtConnection.Size = new System.Drawing.Size(738, 21);
            this.txtConnection.TabIndex = 2;
            // 
            // btnCreateFile
            // 
            this.btnCreateFile.Location = new System.Drawing.Point(825, 36);
            this.btnCreateFile.Name = "btnCreateFile";
            this.btnCreateFile.Size = new System.Drawing.Size(183, 67);
            this.btnCreateFile.TabIndex = 3;
            this.btnCreateFile.Text = "生成文件";
            this.btnCreateFile.UseVisualStyleBackColor = true;
            this.btnCreateFile.Click += new System.EventHandler(this.btnCreateFile_Click);
            // 
            // txtAddress
            // 
            this.txtAddress.Location = new System.Drawing.Point(254, 82);
            this.txtAddress.Name = "txtAddress";
            this.txtAddress.Size = new System.Drawing.Size(428, 21);
            this.txtAddress.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(165, 85);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "文件存放地址";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnQueryTable);
            this.groupBox1.Controls.Add(this.cbxDataBase);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.txtPassword);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.txtAccount);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txtServerAddress);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtConnection);
            this.groupBox1.Controls.Add(this.btnConnectSQL);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1195, 125);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            // 
            // btnQueryTable
            // 
            this.btnQueryTable.Location = new System.Drawing.Point(856, 59);
            this.btnQueryTable.Name = "btnQueryTable";
            this.btnQueryTable.Size = new System.Drawing.Size(148, 52);
            this.btnQueryTable.TabIndex = 11;
            this.btnQueryTable.Text = "查询表格";
            this.btnQueryTable.UseVisualStyleBackColor = true;
            this.btnQueryTable.Click += new System.EventHandler(this.btnQueryTable_Click);
            // 
            // cbxDataBase
            // 
            this.cbxDataBase.FormattingEnabled = true;
            this.cbxDataBase.Location = new System.Drawing.Point(95, 61);
            this.cbxDataBase.Name = "cbxDataBase";
            this.cbxDataBase.Size = new System.Drawing.Size(198, 20);
            this.cbxDataBase.TabIndex = 10;
            this.cbxDataBase.SelectedIndexChanged += new System.EventHandler(this.cbxDataBase_SelectedIndexChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(48, 64);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(41, 12);
            this.label6.TabIndex = 9;
            this.label6.Text = "数据库";
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(635, 18);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(198, 21);
            this.txtPassword.TabIndex = 8;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(600, 23);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(29, 12);
            this.label5.TabIndex = 7;
            this.label5.Text = "密码";
            // 
            // txtAccount
            // 
            this.txtAccount.Location = new System.Drawing.Point(367, 19);
            this.txtAccount.Name = "txtAccount";
            this.txtAccount.Size = new System.Drawing.Size(198, 21);
            this.txtAccount.TabIndex = 6;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(332, 27);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(29, 12);
            this.label4.TabIndex = 5;
            this.label4.Text = "账号";
            // 
            // txtServerAddress
            // 
            this.txtServerAddress.Location = new System.Drawing.Point(95, 23);
            this.txtServerAddress.Name = "txtServerAddress";
            this.txtServerAddress.Size = new System.Drawing.Size(198, 21);
            this.txtServerAddress.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(24, 28);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 3;
            this.label3.Text = "服务器地址";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.button1);
            this.groupBox2.Controls.Add(this.txtPrefix);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.txtNameSpace);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.btnSelectPath);
            this.groupBox2.Controls.Add(this.txtAddress);
            this.groupBox2.Controls.Add(this.btnCreateFile);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox2.Location = new System.Drawing.Point(0, 519);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(1195, 138);
            this.groupBox2.TabIndex = 7;
            this.groupBox2.TabStop = false;
            // 
            // txtPrefix
            // 
            this.txtPrefix.Location = new System.Drawing.Point(575, 38);
            this.txtPrefix.Name = "txtPrefix";
            this.txtPrefix.Size = new System.Drawing.Size(198, 21);
            this.txtPrefix.TabIndex = 15;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(504, 43);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(53, 12);
            this.label8.TabIndex = 14;
            this.label8.Text = "表名前缀";
            // 
            // txtNameSpace
            // 
            this.txtNameSpace.Location = new System.Drawing.Point(254, 38);
            this.txtNameSpace.Name = "txtNameSpace";
            this.txtNameSpace.Size = new System.Drawing.Size(198, 21);
            this.txtNameSpace.TabIndex = 13;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(183, 43);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(59, 12);
            this.label7.TabIndex = 12;
            this.label7.Text = "NameSpace";
            // 
            // btnSelectPath
            // 
            this.btnSelectPath.Location = new System.Drawing.Point(698, 79);
            this.btnSelectPath.Name = "btnSelectPath";
            this.btnSelectPath.Size = new System.Drawing.Size(75, 23);
            this.btnSelectPath.TabIndex = 7;
            this.btnSelectPath.Text = "选择路径";
            this.btnSelectPath.UseVisualStyleBackColor = true;
            this.btnSelectPath.Click += new System.EventHandler(this.btnSelectPath_Click);
            // 
            // btnSelectAll
            // 
            this.btnSelectAll.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnSelectAll.Location = new System.Drawing.Point(3, 348);
            this.btnSelectAll.Name = "btnSelectAll";
            this.btnSelectAll.Size = new System.Drawing.Size(368, 23);
            this.btnSelectAll.TabIndex = 6;
            this.btnSelectAll.Text = "全选";
            this.btnSelectAll.UseVisualStyleBackColor = true;
            this.btnSelectAll.Click += new System.EventHandler(this.btnSelectAll_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.groupBox5);
            this.groupBox3.Controls.Add(this.groupBox4);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox3.Location = new System.Drawing.Point(0, 125);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(1195, 394);
            this.groupBox3.TabIndex = 8;
            this.groupBox3.TabStop = false;
            // 
            // chkTables
            // 
            this.chkTables.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chkTables.FormattingEnabled = true;
            this.chkTables.Location = new System.Drawing.Point(3, 17);
            this.chkTables.Name = "chkTables";
            this.chkTables.Size = new System.Drawing.Size(368, 354);
            this.chkTables.TabIndex = 0;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(19, 66);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 16;
            this.button1.Text = "保存配置";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.btnSelectAll);
            this.groupBox4.Controls.Add(this.chkTables);
            this.groupBox4.Dock = System.Windows.Forms.DockStyle.Left;
            this.groupBox4.Location = new System.Drawing.Point(3, 17);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(374, 374);
            this.groupBox4.TabIndex = 1;
            this.groupBox4.TabStop = false;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.rtxtCode);
            this.groupBox5.Controls.Add(this.groupBox6);
            this.groupBox5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox5.Location = new System.Drawing.Point(377, 17);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(815, 374);
            this.groupBox5.TabIndex = 2;
            this.groupBox5.TabStop = false;
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.chxCopy);
            this.groupBox6.Controls.Add(this.btn_CreateCode);
            this.groupBox6.Controls.Add(this.cbxCodeType);
            this.groupBox6.Controls.Add(this.label10);
            this.groupBox6.Controls.Add(this.cbxTable);
            this.groupBox6.Controls.Add(this.label9);
            this.groupBox6.Dock = System.Windows.Forms.DockStyle.Left;
            this.groupBox6.Location = new System.Drawing.Point(3, 17);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(214, 354);
            this.groupBox6.TabIndex = 0;
            this.groupBox6.TabStop = false;
            // 
            // cbxTable
            // 
            this.cbxTable.FormattingEnabled = true;
            this.cbxTable.Location = new System.Drawing.Point(68, 20);
            this.cbxTable.Name = "cbxTable";
            this.cbxTable.Size = new System.Drawing.Size(126, 20);
            this.cbxTable.TabIndex = 13;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(24, 23);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(29, 12);
            this.label9.TabIndex = 12;
            this.label9.Text = "表格";
            // 
            // cbxCodeType
            // 
            this.cbxCodeType.FormattingEnabled = true;
            this.cbxCodeType.Location = new System.Drawing.Point(68, 60);
            this.cbxCodeType.Name = "cbxCodeType";
            this.cbxCodeType.Size = new System.Drawing.Size(126, 20);
            this.cbxCodeType.TabIndex = 15;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(0, 63);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(53, 12);
            this.label10.TabIndex = 14;
            this.label10.Text = "代码类型";
            // 
            // btn_CreateCode
            // 
            this.btn_CreateCode.Location = new System.Drawing.Point(26, 149);
            this.btn_CreateCode.Name = "btn_CreateCode";
            this.btn_CreateCode.Size = new System.Drawing.Size(148, 52);
            this.btn_CreateCode.TabIndex = 12;
            this.btn_CreateCode.Text = "生成代码";
            this.btn_CreateCode.UseVisualStyleBackColor = true;
            this.btn_CreateCode.Click += new System.EventHandler(this.btn_CreateCode_Click);
            // 
            // rtxtCode
            // 
            this.rtxtCode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtxtCode.Location = new System.Drawing.Point(217, 17);
            this.rtxtCode.Name = "rtxtCode";
            this.rtxtCode.Size = new System.Drawing.Size(595, 354);
            this.rtxtCode.TabIndex = 1;
            this.rtxtCode.Text = "";
            // 
            // chxCopy
            // 
            this.chxCopy.AutoSize = true;
            this.chxCopy.Location = new System.Drawing.Point(74, 108);
            this.chxCopy.Name = "chxCopy";
            this.chxCopy.Size = new System.Drawing.Size(120, 16);
            this.chxCopy.TabIndex = 16;
            this.chxCopy.Text = "复制代码到剪贴板";
            this.chxCopy.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1195, 657);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "Form1";
            this.Text = "实体类生成";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnConnectSQL;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtConnection;
        private System.Windows.Forms.Button btnCreateFile;
        private System.Windows.Forms.TextBox txtAddress;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.CheckedListBox chkTables;
        private System.Windows.Forms.Button btnSelectAll;
        private System.Windows.Forms.Button btnSelectPath;
        private System.Windows.Forms.Button btnQueryTable;
        private System.Windows.Forms.ComboBox cbxDataBase;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtAccount;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtServerAddress;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtPrefix;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtNameSpace;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.RichTextBox rtxtCode;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.Button btn_CreateCode;
        private System.Windows.Forms.ComboBox cbxCodeType;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox cbxTable;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.CheckBox chxCopy;
    }
}

