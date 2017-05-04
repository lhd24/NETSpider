namespace NETSpider
{
    partial class frmViewData
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmViewData));
            this.dgvList = new System.Windows.Forms.DataGridView();
            this.cnTask = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsExportTxt = new System.Windows.Forms.ToolStripMenuItem();
            this.tsExportExcel = new System.Windows.Forms.ToolStripMenuItem();
            this.tsPopDouble = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripProgressBar1 = new System.Windows.Forms.ToolStripProgressBar();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.dgvList)).BeginInit();
            this.cnTask.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvList
            // 
            this.dgvList.AllowUserToAddRows = false;
            this.dgvList.AllowUserToDeleteRows = false;
            this.dgvList.AllowUserToOrderColumns = true;
            this.dgvList.ContextMenuStrip = this.cnTask;
            this.dgvList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvList.Location = new System.Drawing.Point(0, 0);
            this.dgvList.Name = "dgvList";
            this.dgvList.RowTemplate.Height = 23;
            this.dgvList.Size = new System.Drawing.Size(784, 447);
            this.dgvList.TabIndex = 0;
            // 
            // cnTask
            // 
            this.cnTask.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsExportTxt,
            this.tsExportExcel,
            this.tsPopDouble});
            this.cnTask.Name = "cnTask";
            this.cnTask.Size = new System.Drawing.Size(149, 70);
            this.cnTask.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.cnTask_ItemClicked);
            // 
            // tsExportTxt
            // 
            this.tsExportTxt.Name = "tsExportTxt";
            this.tsExportTxt.Size = new System.Drawing.Size(148, 22);
            this.tsExportTxt.Text = "导出文本文件";
            // 
            // tsExportExcel
            // 
            this.tsExportExcel.Name = "tsExportExcel";
            this.tsExportExcel.Size = new System.Drawing.Size(148, 22);
            this.tsExportExcel.Text = "导出Excel";
            // 
            // tsPopDouble
            // 
            this.tsPopDouble.Name = "tsPopDouble";
            this.tsPopDouble.Size = new System.Drawing.Size(148, 22);
            this.tsPopDouble.Text = "去除重复数据";
            // 
            // statusStrip1
            // 
            this.statusStrip1.AutoSize = false;
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.toolStripProgressBar1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 447);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(784, 29);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(68, 24);
            this.toolStripStatusLabel1.Text = "当前数据：";
            // 
            // toolStripProgressBar1
            // 
            this.toolStripProgressBar1.AutoSize = false;
            this.toolStripProgressBar1.Name = "toolStripProgressBar1";
            this.toolStripProgressBar1.Size = new System.Drawing.Size(600, 23);
            // 
            // frmViewData
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 476);
            this.Controls.Add(this.dgvList);
            this.Controls.Add(this.statusStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmViewData";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "查看数据";
            ((System.ComponentModel.ISupportInitialize)(this.dgvList)).EndInit();
            this.cnTask.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvList;
        private System.Windows.Forms.ContextMenuStrip cnTask;
        private System.Windows.Forms.ToolStripMenuItem tsExportTxt;
        private System.Windows.Forms.ToolStripMenuItem tsExportExcel;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripMenuItem tsPopDouble;
    }
}