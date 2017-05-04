namespace NETSpider
{
    partial class frmEncode
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmEncode));
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.comboBoxExt1 = new WinFormLib.Controls.ComboBoxExt();
            this.SuspendLayout();
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(1, 144);
            this.textBox2.Multiline = true;
            this.textBox2.Name = "textBox2";
            this.textBox2.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox2.Size = new System.Drawing.Size(522, 116);
            this.textBox2.TabIndex = 8;
            // 
            // button2
            // 
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.button2.Location = new System.Drawing.Point(463, 118);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(60, 21);
            this.button2.TabIndex = 7;
            this.button2.Text = "解 码";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.button1.Location = new System.Drawing.Point(388, 118);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(60, 21);
            this.button1.TabIndex = 6;
            this.button1.Text = "编 码";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(1, -3);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox1.Size = new System.Drawing.Size(522, 116);
            this.textBox1.TabIndex = 5;
            // 
            // comboBoxExt1
            // 
            this.comboBoxExt1.DataControlName = "comboBoxExt1";
            this.comboBoxExt1.DataFiled = "";
            this.comboBoxExt1.DataFormater = WinFormLib.Core.DataCellType.None;
            this.comboBoxExt1.DataViewValue = "";
            this.comboBoxExt1.DefaultValue = null;
            this.comboBoxExt1.DisenableBackColor = System.Drawing.SystemColors.Control;
            this.comboBoxExt1.DisenableForeColor = System.Drawing.Color.Green;
            this.comboBoxExt1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxExt1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.comboBoxExt1.ForeColor = System.Drawing.Color.Black;
            this.comboBoxExt1.FormattingEnabled = true;
            this.comboBoxExt1.LineColor = System.Drawing.Color.DodgerBlue;
            this.comboBoxExt1.Location = new System.Drawing.Point(1, 118);
            this.comboBoxExt1.Name = "comboBoxExt1";
            this.comboBoxExt1.ReadOnlyValue = true;
            this.comboBoxExt1.Size = new System.Drawing.Size(150, 20);
            this.comboBoxExt1.TabIndex = 10;
            // 
            // frmEncode
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(523, 262);
            this.Controls.Add(this.comboBoxExt1);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.textBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmEncode";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "解码/转码工具";
            this.Load += new System.EventHandler(this.frmEncode_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox textBox1;
        private WinFormLib.Controls.ComboBoxExt comboBoxExt1;
    }
}