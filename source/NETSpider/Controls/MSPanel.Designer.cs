namespace NETSpider.Controls
{
    partial class MSPanel
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.lbSqlServerPwd = new WinFormLib.Controls.LableTextBox();
            this.comSqlServerData = new WinFormLib.Controls.ComboBoxExt();
            this.lbSqlServerUser = new WinFormLib.Controls.LableTextBox();
            this.lbSqlserver = new WinFormLib.Controls.LableTextBox();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.radioButton2);
            this.panel1.Controls.Add(this.radioButton1);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.lbSqlServerPwd);
            this.panel1.Controls.Add(this.comSqlServerData);
            this.panel1.Controls.Add(this.lbSqlServerUser);
            this.panel1.Controls.Add(this.lbSqlserver);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(365, 134);
            this.panel1.TabIndex = 1;
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.radioButton2.Location = new System.Drawing.Point(170, 40);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(71, 16);
            this.radioButton2.TabIndex = 8;
            this.radioButton2.Text = "密码登录";
            this.radioButton2.UseVisualStyleBackColor = true;
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Checked = true;
            this.radioButton1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.radioButton1.Location = new System.Drawing.Point(28, 40);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(65, 16);
            this.radioButton1.TabIndex = 7;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "Windows";
            this.radioButton1.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 103);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 6;
            this.label1.Text = "数据库：";
            // 
            // lbSqlServerPwd
            // 
            this.lbSqlServerPwd.BackColor = System.Drawing.SystemColors.Control;
            this.lbSqlServerPwd.DataControlName = "密码：";
            this.lbSqlServerPwd.DataViewValue = "*****";
            this.lbSqlServerPwd.DefaultValue = null;
            this.lbSqlServerPwd.Location = new System.Drawing.Point(185, 66);
            this.lbSqlServerPwd.Name = "lbSqlServerPwd";
            this.lbSqlServerPwd.PasswordChar = '*';
            this.lbSqlServerPwd.ReadOnlyValue = false;
            this.lbSqlServerPwd.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Both;
            this.lbSqlServerPwd.Size = new System.Drawing.Size(170, 22);
            this.lbSqlServerPwd.TabIndex = 2;
            this.lbSqlServerPwd.TextInfoVisible = true;
            // 
            // comSqlServerData
            // 
            this.comSqlServerData.DataControlName = "cbDataSource";
            this.comSqlServerData.DataFiled = "";
            this.comSqlServerData.DataFormater = WinFormLib.Core.DataCellType.None;
            this.comSqlServerData.DataViewValue = "";
            this.comSqlServerData.DefaultValue = null;
            this.comSqlServerData.DisenableBackColor = System.Drawing.SystemColors.Control;
            this.comSqlServerData.DisenableForeColor = System.Drawing.Color.Green;
            this.comSqlServerData.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comSqlServerData.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.comSqlServerData.ForeColor = System.Drawing.Color.Black;
            this.comSqlServerData.FormattingEnabled = true;
            this.comSqlServerData.LineColor = System.Drawing.Color.DodgerBlue;
            this.comSqlServerData.Location = new System.Drawing.Point(81, 98);
            this.comSqlServerData.Name = "comSqlServerData";
            this.comSqlServerData.ReadOnlyValue = true;
            this.comSqlServerData.Size = new System.Drawing.Size(274, 20);
            this.comSqlServerData.TabIndex = 5;
            this.comSqlServerData.DropDown += new System.EventHandler(this.comSqlServerData_DropDown);
            this.comSqlServerData.SelectedIndexChanged += new System.EventHandler(this.comSqlServerData_SelectedIndexChanged);
            // 
            // lbSqlServerUser
            // 
            this.lbSqlServerUser.BackColor = System.Drawing.SystemColors.Control;
            this.lbSqlServerUser.DataControlName = "用户名：";
            this.lbSqlServerUser.DataViewValue = "admin";
            this.lbSqlServerUser.DefaultValue = null;
            this.lbSqlServerUser.Location = new System.Drawing.Point(9, 66);
            this.lbSqlServerUser.Name = "lbSqlServerUser";
            this.lbSqlServerUser.PasswordChar = '\0';
            this.lbSqlServerUser.ReadOnlyValue = false;
            this.lbSqlServerUser.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Both;
            this.lbSqlServerUser.Size = new System.Drawing.Size(170, 22);
            this.lbSqlServerUser.TabIndex = 1;
            this.lbSqlServerUser.TextInfoVisible = true;
            // 
            // lbSqlserver
            // 
            this.lbSqlserver.BackColor = System.Drawing.SystemColors.Control;
            this.lbSqlserver.DataControlName = "服务器：";
            this.lbSqlserver.DataViewValue = "localhost";
            this.lbSqlserver.DefaultValue = null;
            this.lbSqlserver.Location = new System.Drawing.Point(9, 12);
            this.lbSqlserver.Name = "lbSqlserver";
            this.lbSqlserver.PasswordChar = '\0';
            this.lbSqlserver.ReadOnlyValue = false;
            this.lbSqlserver.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Both;
            this.lbSqlserver.Size = new System.Drawing.Size(346, 22);
            this.lbSqlserver.TabIndex = 0;
            this.lbSqlserver.TextInfoVisible = true;
            // 
            // MSPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Name = "MSPanel";
            this.Size = new System.Drawing.Size(365, 134);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.Label label1;
        private WinFormLib.Controls.LableTextBox lbSqlServerPwd;
        private WinFormLib.Controls.ComboBoxExt comSqlServerData;
        private WinFormLib.Controls.LableTextBox lbSqlServerUser;
        private WinFormLib.Controls.LableTextBox lbSqlserver;
    }
}
