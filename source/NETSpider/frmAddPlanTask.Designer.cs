namespace NETSpider
{
    partial class frmAddPlanTask
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAddPlanTask));
            this.raDataTask = new System.Windows.Forms.RadioButton();
            this.raOtherTask = new System.Windows.Forms.RadioButton();
            this.raSpiderTask = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.pnTask = new System.Windows.Forms.Panel();
            this.dgvTask = new WinFormLib.Components.DataGridViewer();
            this.comCategorys = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.pnlDataBase = new System.Windows.Forms.Panel();
            this.pnlPrograms = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.lbFileArgs = new WinFormLib.Controls.LableTextBox();
            this.lbFileName = new WinFormLib.Controls.LableTextBox();
            this.cbPubDataTable = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.button12 = new System.Windows.Forms.Button();
            this.lbConnectionString = new WinFormLib.Controls.LableTextBox();
            this.raExportMySql = new System.Windows.Forms.RadioButton();
            this.raExportMSSQL = new System.Windows.Forms.RadioButton();
            this.raExportAccess = new System.Windows.Forms.RadioButton();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSubmit = new System.Windows.Forms.Button();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTaskID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTaskName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1.SuspendLayout();
            this.pnTask.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTask)).BeginInit();
            this.pnlDataBase.SuspendLayout();
            this.pnlPrograms.SuspendLayout();
            this.SuspendLayout();
            // 
            // raDataTask
            // 
            this.raDataTask.AutoSize = true;
            this.raDataTask.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.raDataTask.Location = new System.Drawing.Point(143, 12);
            this.raDataTask.Name = "raDataTask";
            this.raDataTask.Size = new System.Drawing.Size(83, 16);
            this.raDataTask.TabIndex = 13;
            this.raDataTask.TabStop = true;
            this.raDataTask.Text = "数据库任务";
            this.raDataTask.UseVisualStyleBackColor = true;
            this.raDataTask.CheckedChanged += new System.EventHandler(this.ShowTaskType);
            // 
            // raOtherTask
            // 
            this.raOtherTask.AutoSize = true;
            this.raOtherTask.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.raOtherTask.Location = new System.Drawing.Point(252, 12);
            this.raOtherTask.Name = "raOtherTask";
            this.raOtherTask.Size = new System.Drawing.Size(71, 16);
            this.raOtherTask.TabIndex = 12;
            this.raOtherTask.Text = "其他任务";
            this.raOtherTask.UseVisualStyleBackColor = true;
            this.raOtherTask.CheckedChanged += new System.EventHandler(this.ShowTaskType);
            // 
            // raSpiderTask
            // 
            this.raSpiderTask.AutoSize = true;
            this.raSpiderTask.Checked = true;
            this.raSpiderTask.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.raSpiderTask.Location = new System.Drawing.Point(14, 12);
            this.raSpiderTask.Name = "raSpiderTask";
            this.raSpiderTask.Size = new System.Drawing.Size(107, 16);
            this.raSpiderTask.TabIndex = 11;
            this.raSpiderTask.TabStop = true;
            this.raSpiderTask.Text = "Spider采摘任务";
            this.raSpiderTask.UseVisualStyleBackColor = true;
            this.raSpiderTask.CheckedChanged += new System.EventHandler(this.ShowTaskType);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.pnlPrograms);
            this.groupBox1.Controls.Add(this.pnTask);
            this.groupBox1.Controls.Add(this.pnlDataBase);
            this.groupBox1.Location = new System.Drawing.Point(14, 34);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(513, 289);
            this.groupBox1.TabIndex = 14;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Spider采摘任务";
            // 
            // pnTask
            // 
            this.pnTask.Controls.Add(this.dgvTask);
            this.pnTask.Controls.Add(this.comCategorys);
            this.pnTask.Controls.Add(this.label1);
            this.pnTask.Location = new System.Drawing.Point(6, 12);
            this.pnTask.Name = "pnTask";
            this.pnTask.Size = new System.Drawing.Size(497, 271);
            this.pnTask.TabIndex = 17;
            // 
            // dgvTask
            // 
            this.dgvTask.AllowUserToAddRows = false;
            this.dgvTask.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(206)))), ((int)(((byte)(249)))), ((int)(((byte)(231)))));
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(183)))), ((int)(((byte)(220)))), ((int)(((byte)(242)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.Black;
            this.dgvTask.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvTask.AutoColumnWidth = true;
            this.dgvTask.BackgroundColor = System.Drawing.Color.DarkGray;
            this.dgvTask.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colTaskID,
            this.colTaskName,
            this.Column3,
            this.Column4});
            this.dgvTask.CurrentPageIndex = 0;
            this.dgvTask.DataGridPager = null;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(183)))), ((int)(((byte)(220)))), ((int)(((byte)(242)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvTask.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvTask.EnableHeadersVisualStyles = false;
            this.dgvTask.EnterIsTab = false;
            this.dgvTask.Location = new System.Drawing.Point(7, 32);
            this.dgvTask.Margin = new System.Windows.Forms.Padding(0);
            this.dgvTask.Name = "dgvTask";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvTask.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvTask.RowHeadersVisible = false;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(183)))), ((int)(((byte)(220)))), ((int)(((byte)(242)))));
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.Black;
            this.dgvTask.RowsDefaultCellStyle = dataGridViewCellStyle4;
            this.dgvTask.RowTemplate.Height = 23;
            this.dgvTask.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvTask.Size = new System.Drawing.Size(485, 227);
            this.dgvTask.TabIndex = 17;
            this.dgvTask.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvTask_CellDoubleClick);
            // 
            // comCategorys
            // 
            this.comCategorys.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comCategorys.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.comCategorys.FormattingEnabled = true;
            this.comCategorys.Location = new System.Drawing.Point(81, 9);
            this.comCategorys.Name = "comCategorys";
            this.comCategorys.Size = new System.Drawing.Size(344, 20);
            this.comCategorys.TabIndex = 15;
            this.comCategorys.SelectedIndexChanged += new System.EventHandler(this.comCategorys_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label1.Location = new System.Drawing.Point(10, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 16;
            this.label1.Text = "任务分类：";
            // 
            // pnlDataBase
            // 
            this.pnlDataBase.Controls.Add(this.cbPubDataTable);
            this.pnlDataBase.Controls.Add(this.label8);
            this.pnlDataBase.Controls.Add(this.button12);
            this.pnlDataBase.Controls.Add(this.lbConnectionString);
            this.pnlDataBase.Controls.Add(this.raExportMySql);
            this.pnlDataBase.Controls.Add(this.raExportMSSQL);
            this.pnlDataBase.Controls.Add(this.raExportAccess);
            this.pnlDataBase.Location = new System.Drawing.Point(6, 12);
            this.pnlDataBase.Name = "pnlDataBase";
            this.pnlDataBase.Size = new System.Drawing.Size(497, 271);
            this.pnlDataBase.TabIndex = 18;
            // 
            // pnlPrograms
            // 
            this.pnlPrograms.Controls.Add(this.button1);
            this.pnlPrograms.Controls.Add(this.lbFileArgs);
            this.pnlPrograms.Controls.Add(this.lbFileName);
            this.pnlPrograms.Location = new System.Drawing.Point(6, 12);
            this.pnlPrograms.Name = "pnlPrograms";
            this.pnlPrograms.Size = new System.Drawing.Size(497, 271);
            this.pnlPrograms.TabIndex = 19;
            // 
            // button1
            // 
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.button1.Location = new System.Drawing.Point(422, 10);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(60, 21);
            this.button1.TabIndex = 59;
            this.button1.Text = "浏览";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // lbFileArgs
            // 
            this.lbFileArgs.BackColor = System.Drawing.SystemColors.Control;
            this.lbFileArgs.DataControlName = "参数：";
            this.lbFileArgs.DataViewValue = "";
            this.lbFileArgs.DefaultValue = null;
            this.lbFileArgs.Location = new System.Drawing.Point(6, 38);
            this.lbFileArgs.Multiline = true;
            this.lbFileArgs.Name = "lbFileArgs";
            this.lbFileArgs.PasswordChar = '\0';
            this.lbFileArgs.ReadOnlyValue = false;
            this.lbFileArgs.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Both;
            this.lbFileArgs.Size = new System.Drawing.Size(415, 69);
            this.lbFileArgs.TabIndex = 58;
            this.lbFileArgs.TextInfoVisible = true;
            // 
            // lbFileName
            // 
            this.lbFileName.BackColor = System.Drawing.SystemColors.Control;
            this.lbFileName.DataControlName = "程序：";
            this.lbFileName.DataViewValue = "";
            this.lbFileName.DefaultValue = null;
            this.lbFileName.Location = new System.Drawing.Point(7, 8);
            this.lbFileName.Name = "lbFileName";
            this.lbFileName.PasswordChar = '\0';
            this.lbFileName.ReadOnlyValue = false;
            this.lbFileName.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Both;
            this.lbFileName.Size = new System.Drawing.Size(415, 24);
            this.lbFileName.TabIndex = 57;
            this.lbFileName.TextInfoVisible = true;
            // 
            // cbPubDataTable
            // 
            this.cbPubDataTable.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbPubDataTable.FormattingEnabled = true;
            this.cbPubDataTable.Location = new System.Drawing.Point(81, 127);
            this.cbPubDataTable.Name = "cbPubDataTable";
            this.cbPubDataTable.Size = new System.Drawing.Size(344, 20);
            this.cbPubDataTable.TabIndex = 59;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label8.Location = new System.Drawing.Point(8, 107);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(101, 12);
            this.label8.TabIndex = 58;
            this.label8.Text = "查询或存储过程：";
            // 
            // button12
            // 
            this.button12.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button12.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.button12.Location = new System.Drawing.Point(425, 35);
            this.button12.Name = "button12";
            this.button12.Size = new System.Drawing.Size(60, 21);
            this.button12.TabIndex = 57;
            this.button12.Text = "设 置";
            this.button12.UseVisualStyleBackColor = true;
            this.button12.Click += new System.EventHandler(this.button12_Click);
            // 
            // lbConnectionString
            // 
            this.lbConnectionString.BackColor = System.Drawing.SystemColors.Control;
            this.lbConnectionString.DataControlName = "数据库：";
            this.lbConnectionString.DataViewValue = "";
            this.lbConnectionString.DefaultValue = null;
            this.lbConnectionString.Location = new System.Drawing.Point(9, 35);
            this.lbConnectionString.Multiline = true;
            this.lbConnectionString.Name = "lbConnectionString";
            this.lbConnectionString.PasswordChar = '\0';
            this.lbConnectionString.ReadOnlyValue = false;
            this.lbConnectionString.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Both;
            this.lbConnectionString.Size = new System.Drawing.Size(415, 69);
            this.lbConnectionString.TabIndex = 56;
            this.lbConnectionString.TextInfoVisible = true;
            // 
            // raExportMySql
            // 
            this.raExportMySql.AutoSize = true;
            this.raExportMySql.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.raExportMySql.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.raExportMySql.Location = new System.Drawing.Point(241, 13);
            this.raExportMySql.Name = "raExportMySql";
            this.raExportMySql.Size = new System.Drawing.Size(88, 16);
            this.raExportMySql.TabIndex = 55;
            this.raExportMySql.Text = "MySql数据库";
            this.raExportMySql.UseVisualStyleBackColor = true;
            // 
            // raExportMSSQL
            // 
            this.raExportMSSQL.AutoSize = true;
            this.raExportMSSQL.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.raExportMSSQL.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.raExportMSSQL.Location = new System.Drawing.Point(110, 13);
            this.raExportMSSQL.Name = "raExportMSSQL";
            this.raExportMSSQL.Size = new System.Drawing.Size(130, 16);
            this.raExportMSSQL.TabIndex = 54;
            this.raExportMSSQL.Text = "MS SqlServer数据库";
            this.raExportMSSQL.UseVisualStyleBackColor = true;
            // 
            // raExportAccess
            // 
            this.raExportAccess.AutoSize = true;
            this.raExportAccess.Checked = true;
            this.raExportAccess.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.raExportAccess.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.raExportAccess.Location = new System.Drawing.Point(9, 13);
            this.raExportAccess.Name = "raExportAccess";
            this.raExportAccess.Size = new System.Drawing.Size(94, 16);
            this.raExportAccess.TabIndex = 53;
            this.raExportAccess.TabStop = true;
            this.raExportAccess.Text = "Access数据库";
            this.raExportAccess.UseVisualStyleBackColor = true;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // btnCancel
            // 
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCancel.ImageIndex = 9;
            this.btnCancel.Location = new System.Drawing.Point(451, 329);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(80, 23);
            this.btnCancel.TabIndex = 18;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSubmit
            // 
            this.btnSubmit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSubmit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSubmit.ImageIndex = 3;
            this.btnSubmit.Location = new System.Drawing.Point(365, 329);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(80, 23);
            this.btnSubmit.TabIndex = 17;
            this.btnSubmit.Text = "确定";
            this.btnSubmit.UseVisualStyleBackColor = true;
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.DataPropertyName = "TaskName";
            this.dataGridViewTextBoxColumn1.HeaderText = "任务名称";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.Width = 200;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.DataPropertyName = "TaskName";
            this.dataGridViewTextBoxColumn2.HeaderText = "任务类型";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.Width = 140;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.HeaderText = "执行类型";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.Width = 140;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.HeaderText = "执行类型";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.Width = 140;
            // 
            // colTaskID
            // 
            this.colTaskID.DataPropertyName = "TaskID";
            this.colTaskID.HeaderText = "TaskID";
            this.colTaskID.Name = "colTaskID";
            this.colTaskID.Width = 60;
            // 
            // colTaskName
            // 
            this.colTaskName.DataPropertyName = "TaskName";
            this.colTaskName.HeaderText = "任务名称";
            this.colTaskName.Name = "colTaskName";
            this.colTaskName.Width = 200;
            // 
            // Column3
            // 
            this.Column3.HeaderText = "任务类型";
            this.Column3.Name = "Column3";
            this.Column3.Width = 140;
            // 
            // Column4
            // 
            this.Column4.HeaderText = "执行类型";
            this.Column4.Name = "Column4";
            this.Column4.Width = 140;
            // 
            // frmAddPlanTask
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(545, 364);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSubmit);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.raDataTask);
            this.Controls.Add(this.raOtherTask);
            this.Controls.Add(this.raSpiderTask);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmAddPlanTask";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "添加计划任务";
            this.Load += new System.EventHandler(this.frmAddPlanTask_Load);
            this.groupBox1.ResumeLayout(false);
            this.pnTask.ResumeLayout(false);
            this.pnTask.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTask)).EndInit();
            this.pnlDataBase.ResumeLayout(false);
            this.pnlDataBase.PerformLayout();
            this.pnlPrograms.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton raDataTask;
        private System.Windows.Forms.RadioButton raOtherTask;
        private System.Windows.Forms.RadioButton raSpiderTask;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comCategorys;
        private System.Windows.Forms.Panel pnTask;
        private WinFormLib.Components.DataGridViewer dgvTask;
        private System.Windows.Forms.Panel pnlDataBase;
        private System.Windows.Forms.RadioButton raExportMySql;
        private System.Windows.Forms.RadioButton raExportMSSQL;
        private System.Windows.Forms.RadioButton raExportAccess;
        private WinFormLib.Controls.LableTextBox lbConnectionString;
        private System.Windows.Forms.Button button12;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox cbPubDataTable;
        private System.Windows.Forms.Panel pnlPrograms;
        private WinFormLib.Controls.LableTextBox lbFileName;
        private System.Windows.Forms.Button button1;
        private WinFormLib.Controls.LableTextBox lbFileArgs;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSubmit;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTaskID;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTaskName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
    }
}