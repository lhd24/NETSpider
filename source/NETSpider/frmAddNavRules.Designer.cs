namespace NETSpider
{
    partial class frmAddNavRules
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAddNavRules));
            this.lbMainUrl = new WinFormLib.Controls.LableTextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.label7 = new System.Windows.Forms.Label();
            this.lbResult = new WinFormLib.Controls.LableTextBox();
            this.txtNRule = new System.Windows.Forms.TextBox();
            this.cmdClear = new System.Windows.Forms.Button();
            this.cmdAdd = new System.Windows.Forms.Button();
            this.cmdTest = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.lbInfo = new WinFormLib.Controls.LableTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cbCmdType = new WinFormLib.Controls.ComboBoxExt();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.label6 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbMainUrl
            // 
            this.lbMainUrl.BackColor = System.Drawing.SystemColors.Control;
            this.lbMainUrl.DataControlName = "测试网址：";
            this.lbMainUrl.DataViewValue = "";
            this.lbMainUrl.DefaultValue = null;
            this.lbMainUrl.Location = new System.Drawing.Point(2, 3);
            this.lbMainUrl.Multiline = true;
            this.lbMainUrl.Name = "lbMainUrl";
            this.lbMainUrl.PasswordChar = '\0';
            this.lbMainUrl.ReadOnlyValue = false;
            this.lbMainUrl.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Both;
            this.lbMainUrl.Size = new System.Drawing.Size(554, 44);
            this.lbMainUrl.TabIndex = 0;
            this.lbMainUrl.TextInfoVisible = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lbMainUrl);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(568, 52);
            this.panel1.TabIndex = 2;
            // 
            // tabControl1
            // 
            this.tabControl1.Appearance = System.Windows.Forms.TabAppearance.FlatButtons;
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 52);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(568, 204);
            this.tabControl1.TabIndex = 3;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.label7);
            this.tabPage1.Controls.Add(this.lbResult);
            this.tabPage1.Controls.Add(this.txtNRule);
            this.tabPage1.Controls.Add(this.cmdClear);
            this.tabPage1.Controls.Add(this.cmdAdd);
            this.tabPage1.Controls.Add(this.cmdTest);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.lbInfo);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Controls.Add(this.cbCmdType);
            this.tabPage1.Location = new System.Drawing.Point(4, 26);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(560, 174);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "导航规则";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label7.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label7.Location = new System.Drawing.Point(79, 157);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(341, 12);
            this.label7.TabIndex = 34;
            this.label7.Text = "测试将返回所有结果，且不做任何加工，譬如：相对地址的转换";
            // 
            // lbResult
            // 
            this.lbResult.BackColor = System.Drawing.SystemColors.Control;
            this.lbResult.DataControlName = "测试结果：";
            this.lbResult.DataViewValue = "";
            this.lbResult.DefaultValue = null;
            this.lbResult.Location = new System.Drawing.Point(3, 88);
            this.lbResult.Multiline = true;
            this.lbResult.Name = "lbResult";
            this.lbResult.PasswordChar = '\0';
            this.lbResult.ReadOnlyValue = false;
            this.lbResult.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Both;
            this.lbResult.Size = new System.Drawing.Size(544, 66);
            this.lbResult.TabIndex = 33;
            this.lbResult.TextInfoVisible = true;
            // 
            // txtNRule
            // 
            this.txtNRule.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtNRule.Location = new System.Drawing.Point(81, 61);
            this.txtNRule.Multiline = true;
            this.txtNRule.Name = "txtNRule";
            this.txtNRule.Size = new System.Drawing.Size(466, 21);
            this.txtNRule.TabIndex = 32;
            // 
            // cmdClear
            // 
            this.cmdClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmdClear.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.cmdClear.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.cmdClear.Location = new System.Drawing.Point(481, 34);
            this.cmdClear.Name = "cmdClear";
            this.cmdClear.Size = new System.Drawing.Size(65, 21);
            this.cmdClear.TabIndex = 31;
            this.cmdClear.Text = "清  除";
            this.cmdClear.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cmdClear.UseVisualStyleBackColor = true;
            this.cmdClear.Click += new System.EventHandler(this.cmdClear_Click);
            // 
            // cmdAdd
            // 
            this.cmdAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmdAdd.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.cmdAdd.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.cmdAdd.Location = new System.Drawing.Point(412, 34);
            this.cmdAdd.Name = "cmdAdd";
            this.cmdAdd.Size = new System.Drawing.Size(65, 21);
            this.cmdAdd.TabIndex = 30;
            this.cmdAdd.Text = "增  加";
            this.cmdAdd.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cmdAdd.UseVisualStyleBackColor = true;
            this.cmdAdd.Click += new System.EventHandler(this.cmdAdd_Click);
            // 
            // cmdTest
            // 
            this.cmdTest.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmdTest.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.cmdTest.Location = new System.Drawing.Point(342, 34);
            this.cmdTest.Name = "cmdTest";
            this.cmdTest.Size = new System.Drawing.Size(65, 21);
            this.cmdTest.TabIndex = 29;
            this.cmdTest.Text = "测  试";
            this.cmdTest.UseVisualStyleBackColor = true;
            this.cmdTest.Click += new System.EventHandler(this.cmdTest_Click);
            // 
            // label1
            // 
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label1.Location = new System.Drawing.Point(79, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(239, 24);
            this.label1.TabIndex = 28;
            this.label1.Text = "系统采用正则进行导航规则的匹配，后缀可不填写，系统会自动识别";
            // 
            // lbInfo
            // 
            this.lbInfo.BackColor = System.Drawing.SystemColors.Control;
            this.lbInfo.DataControlName = "信息：";
            this.lbInfo.DataViewValue = "";
            this.lbInfo.DefaultValue = null;
            this.lbInfo.Location = new System.Drawing.Point(278, 6);
            this.lbInfo.Name = "lbInfo";
            this.lbInfo.PasswordChar = '\0';
            this.lbInfo.ReadOnlyValue = false;
            this.lbInfo.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Both;
            this.lbInfo.Size = new System.Drawing.Size(269, 22);
            this.lbInfo.TabIndex = 13;
            this.lbInfo.TextInfoVisible = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 11);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 12;
            this.label3.Text = "类型：";
            // 
            // cbCmdType
            // 
            this.cbCmdType.DataControlName = "comboBoxExt1";
            this.cbCmdType.DataFiled = "";
            this.cbCmdType.DataFormater = WinFormLib.Core.DataCellType.None;
            this.cbCmdType.DataViewValue = "";
            this.cbCmdType.DefaultValue = null;
            this.cbCmdType.DisenableBackColor = System.Drawing.SystemColors.Control;
            this.cbCmdType.DisenableForeColor = System.Drawing.Color.Green;
            this.cbCmdType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbCmdType.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbCmdType.ForeColor = System.Drawing.Color.Black;
            this.cbCmdType.FormattingEnabled = true;
            this.cbCmdType.LineColor = System.Drawing.Color.DodgerBlue;
            this.cbCmdType.Location = new System.Drawing.Point(81, 6);
            this.cbCmdType.Name = "cbCmdType";
            this.cbCmdType.ReadOnlyValue = true;
            this.cbCmdType.Size = new System.Drawing.Size(187, 20);
            this.cbCmdType.TabIndex = 11;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.label6);
            this.tabPage2.Location = new System.Drawing.Point(4, 26);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(560, 174);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "帮助";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            this.label6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label6.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label6.Location = new System.Drawing.Point(3, 3);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(554, 168);
            this.label6.TabIndex = 2;
            this.label6.Text = resources.GetString("label6.Text");
            // 
            // frmAddNavRules
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(568, 256);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmAddNavRules";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "添加导航规则";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmAddNavRules_FormClosing);
            this.Load += new System.EventHandler(this.frmAddNavRules_Load);
            this.panel1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private WinFormLib.Controls.LableTextBox lbMainUrl;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Label label3;
        private WinFormLib.Controls.ComboBoxExt cbCmdType;
        private WinFormLib.Controls.LableTextBox lbInfo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button cmdClear;
        private System.Windows.Forms.Button cmdAdd;
        private System.Windows.Forms.Button cmdTest;
        private System.Windows.Forms.TextBox txtNRule;
        private WinFormLib.Controls.LableTextBox lbResult;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
    }
}