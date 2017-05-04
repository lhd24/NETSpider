namespace NETSpider.Controls
{
    partial class MySqlPanel
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
            this.panel2 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.comMySqlCode = new WinFormLib.Controls.ComboBoxExt();
            this.lbMySqlPort = new WinFormLib.Controls.LableTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.lbMySqlPwd = new WinFormLib.Controls.LableTextBox();
            this.cbDatabase = new WinFormLib.Controls.ComboBoxExt();
            this.lbMySqlUser = new WinFormLib.Controls.LableTextBox();
            this.lbMySql = new WinFormLib.Controls.LableTextBox();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.comMySqlCode);
            this.panel2.Controls.Add(this.lbMySqlPort);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.lbMySqlPwd);
            this.panel2.Controls.Add(this.cbDatabase);
            this.panel2.Controls.Add(this.lbMySqlUser);
            this.panel2.Controls.Add(this.lbMySql);
            this.panel2.Location = new System.Drawing.Point(1, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(363, 129);
            this.panel2.TabIndex = 44;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(186, 103);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 47;
            this.label3.Text = "编码：";
            // 
            // comMySqlCode
            // 
            this.comMySqlCode.DataControlName = "cbTaskCategory";
            this.comMySqlCode.DataFiled = "";
            this.comMySqlCode.DataFormater = WinFormLib.Core.DataCellType.None;
            this.comMySqlCode.DataViewValue = "";
            this.comMySqlCode.DefaultValue = null;
            this.comMySqlCode.DisenableBackColor = System.Drawing.SystemColors.Control;
            this.comMySqlCode.DisenableForeColor = System.Drawing.Color.Green;
            this.comMySqlCode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comMySqlCode.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.comMySqlCode.ForeColor = System.Drawing.Color.Black;
            this.comMySqlCode.FormattingEnabled = true;
            this.comMySqlCode.LineColor = System.Drawing.Color.DodgerBlue;
            this.comMySqlCode.Location = new System.Drawing.Point(260, 98);
            this.comMySqlCode.Name = "comMySqlCode";
            this.comMySqlCode.ReadOnlyValue = true;
            this.comMySqlCode.Size = new System.Drawing.Size(95, 20);
            this.comMySqlCode.TabIndex = 46;
            // 
            // lbMySqlPort
            // 
            this.lbMySqlPort.BackColor = System.Drawing.SystemColors.Control;
            this.lbMySqlPort.DataControlName = "端口：";
            this.lbMySqlPort.DataViewValue = "3306";
            this.lbMySqlPort.DefaultValue = null;
            this.lbMySqlPort.Location = new System.Drawing.Point(9, 38);
            this.lbMySqlPort.Name = "lbMySqlPort";
            this.lbMySqlPort.PasswordChar = '\0';
            this.lbMySqlPort.ReadOnlyValue = false;
            this.lbMySqlPort.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Both;
            this.lbMySqlPort.Size = new System.Drawing.Size(170, 22);
            this.lbMySqlPort.TabIndex = 44;
            this.lbMySqlPort.TextInfoVisible = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 103);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 6;
            this.label2.Text = "数据库：";
            // 
            // lbMySqlPwd
            // 
            this.lbMySqlPwd.BackColor = System.Drawing.SystemColors.Control;
            this.lbMySqlPwd.DataControlName = "密码：";
            this.lbMySqlPwd.DataViewValue = "****";
            this.lbMySqlPwd.DefaultValue = null;
            this.lbMySqlPwd.Location = new System.Drawing.Point(185, 66);
            this.lbMySqlPwd.Name = "lbMySqlPwd";
            this.lbMySqlPwd.PasswordChar = '*';
            this.lbMySqlPwd.ReadOnlyValue = false;
            this.lbMySqlPwd.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Both;
            this.lbMySqlPwd.Size = new System.Drawing.Size(170, 22);
            this.lbMySqlPwd.TabIndex = 2;
            this.lbMySqlPwd.TextInfoVisible = true;
            // 
            // cbDatabase
            // 
            this.cbDatabase.DataControlName = "cbTaskCategory";
            this.cbDatabase.DataFiled = "";
            this.cbDatabase.DataFormater = WinFormLib.Core.DataCellType.None;
            this.cbDatabase.DataViewValue = "";
            this.cbDatabase.DefaultValue = null;
            this.cbDatabase.DisenableBackColor = System.Drawing.SystemColors.Control;
            this.cbDatabase.DisenableForeColor = System.Drawing.Color.Green;
            this.cbDatabase.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbDatabase.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbDatabase.ForeColor = System.Drawing.Color.Black;
            this.cbDatabase.FormattingEnabled = true;
            this.cbDatabase.LineColor = System.Drawing.Color.DodgerBlue;
            this.cbDatabase.Location = new System.Drawing.Point(81, 98);
            this.cbDatabase.Name = "cbDatabase";
            this.cbDatabase.ReadOnlyValue = true;
            this.cbDatabase.Size = new System.Drawing.Size(98, 20);
            this.cbDatabase.TabIndex = 5;
            this.cbDatabase.DropDown += new System.EventHandler(this.cbDatabase_DropDown);
            // 
            // lbMySqlUser
            // 
            this.lbMySqlUser.BackColor = System.Drawing.SystemColors.Control;
            this.lbMySqlUser.DataControlName = "用户名：";
            this.lbMySqlUser.DataViewValue = "root";
            this.lbMySqlUser.DefaultValue = null;
            this.lbMySqlUser.Location = new System.Drawing.Point(9, 66);
            this.lbMySqlUser.Name = "lbMySqlUser";
            this.lbMySqlUser.PasswordChar = '\0';
            this.lbMySqlUser.ReadOnlyValue = false;
            this.lbMySqlUser.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Both;
            this.lbMySqlUser.Size = new System.Drawing.Size(170, 22);
            this.lbMySqlUser.TabIndex = 1;
            this.lbMySqlUser.TextInfoVisible = true;
            // 
            // lbMySql
            // 
            this.lbMySql.BackColor = System.Drawing.SystemColors.Control;
            this.lbMySql.DataControlName = "服务器：";
            this.lbMySql.DataViewValue = "localhost";
            this.lbMySql.DefaultValue = null;
            this.lbMySql.Location = new System.Drawing.Point(9, 12);
            this.lbMySql.Name = "lbMySql";
            this.lbMySql.PasswordChar = '\0';
            this.lbMySql.ReadOnlyValue = false;
            this.lbMySql.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Both;
            this.lbMySql.Size = new System.Drawing.Size(346, 22);
            this.lbMySql.TabIndex = 0;
            this.lbMySql.TextInfoVisible = true;
            // 
            // MySqlPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel2);
            this.Name = "MySqlPanel";
            this.Size = new System.Drawing.Size(365, 134);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label3;
        private WinFormLib.Controls.ComboBoxExt comMySqlCode;
        private WinFormLib.Controls.LableTextBox lbMySqlPort;
        private System.Windows.Forms.Label label2;
        private WinFormLib.Controls.LableTextBox lbMySqlPwd;
        private WinFormLib.Controls.ComboBoxExt cbDatabase;
        private WinFormLib.Controls.LableTextBox lbMySqlUser;
        private WinFormLib.Controls.LableTextBox lbMySql;
    }
}
