namespace NETSpider.Controls
{
    partial class AccessPanel
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
            this.chkValidate = new System.Windows.Forms.CheckBox();
            this.button1 = new System.Windows.Forms.Button();
            this.lbAccessPwd = new WinFormLib.Controls.LableTextBox();
            this.lbAccessUser = new WinFormLib.Controls.LableTextBox();
            this.lbAccessName = new WinFormLib.Controls.LableTextBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.chkValidate);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.lbAccessPwd);
            this.panel1.Controls.Add(this.lbAccessUser);
            this.panel1.Controls.Add(this.lbAccessName);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(365, 134);
            this.panel1.TabIndex = 45;
            // 
            // chkValidate
            // 
            this.chkValidate.AutoSize = true;
            this.chkValidate.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.chkValidate.Location = new System.Drawing.Point(203, 47);
            this.chkValidate.Name = "chkValidate";
            this.chkValidate.Size = new System.Drawing.Size(72, 16);
            this.chkValidate.TabIndex = 45;
            this.chkValidate.Text = "需要验证";
            this.chkValidate.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.button1.Location = new System.Drawing.Point(81, 40);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(116, 23);
            this.button1.TabIndex = 50;
            this.button1.Text = "浏览";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // lbAccessPwd
            // 
            this.lbAccessPwd.BackColor = System.Drawing.SystemColors.Control;
            this.lbAccessPwd.DataControlName = "密码：";
            this.lbAccessPwd.DataViewValue = "*****";
            this.lbAccessPwd.DefaultValue = null;
            this.lbAccessPwd.Location = new System.Drawing.Point(185, 83);
            this.lbAccessPwd.Name = "lbAccessPwd";
            this.lbAccessPwd.PasswordChar = '*';
            this.lbAccessPwd.ReadOnlyValue = false;
            this.lbAccessPwd.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Both;
            this.lbAccessPwd.Size = new System.Drawing.Size(170, 22);
            this.lbAccessPwd.TabIndex = 2;
            this.lbAccessPwd.TextInfoVisible = true;
            // 
            // lbAccessUser
            // 
            this.lbAccessUser.BackColor = System.Drawing.SystemColors.Control;
            this.lbAccessUser.DataControlName = "用户名：";
            this.lbAccessUser.DataViewValue = "root";
            this.lbAccessUser.DefaultValue = null;
            this.lbAccessUser.Location = new System.Drawing.Point(9, 83);
            this.lbAccessUser.Name = "lbAccessUser";
            this.lbAccessUser.PasswordChar = '\0';
            this.lbAccessUser.ReadOnlyValue = false;
            this.lbAccessUser.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Both;
            this.lbAccessUser.Size = new System.Drawing.Size(170, 22);
            this.lbAccessUser.TabIndex = 1;
            this.lbAccessUser.TextInfoVisible = true;
            // 
            // lbAccessName
            // 
            this.lbAccessName.BackColor = System.Drawing.SystemColors.Control;
            this.lbAccessName.DataControlName = "文件名：";
            this.lbAccessName.DataViewValue = "";
            this.lbAccessName.DefaultValue = null;
            this.lbAccessName.Location = new System.Drawing.Point(9, 12);
            this.lbAccessName.Name = "lbAccessName";
            this.lbAccessName.PasswordChar = '\0';
            this.lbAccessName.ReadOnlyValue = false;
            this.lbAccessName.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Both;
            this.lbAccessName.Size = new System.Drawing.Size(346, 22);
            this.lbAccessName.TabIndex = 0;
            this.lbAccessName.TextInfoVisible = true;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // AccessPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Name = "AccessPanel";
            this.Size = new System.Drawing.Size(365, 134);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.CheckBox chkValidate;
        private System.Windows.Forms.Button button1;
        private WinFormLib.Controls.LableTextBox lbAccessPwd;
        private WinFormLib.Controls.LableTextBox lbAccessUser;
        private WinFormLib.Controls.LableTextBox lbAccessName;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
    }
}
