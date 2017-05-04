using WinFormLib.Components;
using WinFormLib.Components.Cell;
using WinFormLib.Core;
namespace WinFormLib.Controls
{
    partial class SearchTool
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.btnSelect = new ExoticControls.SplitButton();
            this.cbTF005 = new System.Windows.Forms.ComboBox();
            this.cbTF001 = new System.Windows.Forms.ComboBox();
            this.pnlTop = new System.Windows.Forms.Panel();
            this.pnlCenter = new System.Windows.Forms.Panel();
            this.cmsMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsMnuItemNormal = new System.Windows.Forms.ToolStripMenuItem();
            this.tsMnuItemHigh = new System.Windows.Forms.ToolStripMenuItem();
            this.dgvList = new WinFormLib.Components.DataGridViewEditor();
            this.CTF000 = new WinFormLib.Components.Cell.DataGridViewForzenColumn();
            this.CTF001 = new WinFormLib.Components.Cell.DataGridViewComboBoxButtonColumn();
            this.CTF005 = new WinFormLib.Components.Cell.DataGridViewComboBoxButtonColumn();
            this.CTF006 = new WinFormLib.Components.Cell.DataGridViewAutoFilterTextBoxColumn();
            this.CTF008 = new WinFormLib.Components.Cell.DataGridViewComboBoxButtonColumn();
            this.cbNum = new WinFormLib.Controls.LableComboBox();
            this.txtTF006 = new WinFormLib.Controls.TextBoxExt();
            this.pnlTop.SuspendLayout();
            this.pnlCenter.SuspendLayout();
            this.cmsMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvList)).BeginInit();
            this.SuspendLayout();
            // 
            // btnSelect
            // 
            this.btnSelect.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSelect.ImageKey = "Normal";
            this.btnSelect.Location = new System.Drawing.Point(617, 2);
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.Size = new System.Drawing.Size(80, 23);
            this.btnSelect.TabIndex = 6;
            this.btnSelect.Text = "重新查找";
            this.btnSelect.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSelect.UseVisualStyleBackColor = true;
            this.btnSelect.ButtonClick += new System.EventHandler(this.btnSelect_ButtonClick);
            // 
            // cbTF005
            // 
            this.cbTF005.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbTF005.FormattingEnabled = true;
            this.cbTF005.Location = new System.Drawing.Point(350, 4);
            this.cbTF005.Name = "cbTF005";
            this.cbTF005.Size = new System.Drawing.Size(78, 20);
            this.cbTF005.TabIndex = 8;
            // 
            // cbTF001
            // 
            this.cbTF001.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbTF001.FormattingEnabled = true;
            this.cbTF001.Location = new System.Drawing.Point(208, 4);
            this.cbTF001.Name = "cbTF001";
            this.cbTF001.Size = new System.Drawing.Size(140, 20);
            this.cbTF001.TabIndex = 7;
            // 
            // pnlTop
            // 
            this.pnlTop.Controls.Add(this.cbNum);
            this.pnlTop.Controls.Add(this.cbTF001);
            this.pnlTop.Controls.Add(this.txtTF006);
            this.pnlTop.Controls.Add(this.btnSelect);
            this.pnlTop.Controls.Add(this.cbTF005);
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTop.Location = new System.Drawing.Point(0, 0);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Size = new System.Drawing.Size(700, 31);
            this.pnlTop.TabIndex = 10;
            // 
            // pnlCenter
            // 
            this.pnlCenter.Controls.Add(this.dgvList);
            this.pnlCenter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlCenter.Location = new System.Drawing.Point(0, 31);
            this.pnlCenter.Name = "pnlCenter";
            this.pnlCenter.Size = new System.Drawing.Size(700, 113);
            this.pnlCenter.TabIndex = 11;
            // 
            // cmsMenu
            // 
            this.cmsMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsMnuItemNormal,
            this.tsMnuItemHigh});
            this.cmsMenu.Name = "cmsMenu";
            this.cmsMenu.Size = new System.Drawing.Size(119, 48);
            // 
            // tsMnuItemNormal
            // 
            this.tsMnuItemNormal.Checked = true;
            this.tsMnuItemNormal.CheckState = System.Windows.Forms.CheckState.Checked;
            this.tsMnuItemNormal.Name = "tsMnuItemNormal";
            this.tsMnuItemNormal.Size = new System.Drawing.Size(118, 22);
            this.tsMnuItemNormal.Text = "一般查询";
            this.tsMnuItemNormal.Click += new System.EventHandler(this.tsMnuItemNormal_Click);
            // 
            // tsMnuItemHigh
            // 
            this.tsMnuItemHigh.Name = "tsMnuItemHigh";
            this.tsMnuItemHigh.Size = new System.Drawing.Size(118, 22);
            this.tsMnuItemHigh.Text = "高级查询";
            this.tsMnuItemHigh.Click += new System.EventHandler(this.tsMnuItemHigh_Click);
            // 
            // dgvList
            // 
            this.dgvList.AllowUserToAddRows = false;
            this.dgvList.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(206)))), ((int)(((byte)(249)))), ((int)(((byte)(231)))));
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(183)))), ((int)(((byte)(220)))), ((int)(((byte)(242)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.Black;
            this.dgvList.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvList.AutoColumnWidth = true;
            this.dgvList.BackgroundColor = System.Drawing.Color.DarkGray;
            this.dgvList.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.dgvList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.CTF000,
            this.CTF001,
            this.CTF005,
            this.CTF006,
            this.CTF008});
            this.dgvList.DataControlName = "dgvList";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(183)))), ((int)(((byte)(220)))), ((int)(((byte)(242)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvList.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvList.EnabledEdit = true;
            this.dgvList.EnableHeadersVisualStyles = false;
            this.dgvList.EnterIsTab = true;
            this.dgvList.IsNoNullEmptyColumnIndex = null;
            this.dgvList.Location = new System.Drawing.Point(0, 0);
            this.dgvList.Margin = new System.Windows.Forms.Padding(0);
            this.dgvList.MultiSelect = true;
            this.dgvList.Name = "dgvList";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvList.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvList.RowHeadersVisible = false;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(183)))), ((int)(((byte)(220)))), ((int)(((byte)(242)))));
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.Black;
            this.dgvList.RowsDefaultCellStyle = dataGridViewCellStyle4;
            this.dgvList.RowTemplate.Height = 23;
            this.dgvList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvList.Size = new System.Drawing.Size(700, 113);
            this.dgvList.TabIndex = 1;
            // 
            // CTF000
            // 
            this.CTF000.Frozen = true;
            this.CTF000.HeaderText = "";
            this.CTF000.Name = "CTF000";
            this.CTF000.ReadOnly = true;
            this.CTF000.Width = 30;
            // 
            // CTF001
            // 
            this.CTF001.DataPropertyName = "TF001";
            this.CTF001.HeaderText = "查询字段";
            this.CTF001.Name = "CTF001";
            this.CTF001.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.CTF001.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.CTF001.Width = 140;
            // 
            // CTF005
            // 
            this.CTF005.DataPropertyName = "TF005";
            this.CTF005.HeaderText = "操作符";
            this.CTF005.Name = "CTF005";
            this.CTF005.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.CTF005.Width = 50;
            // 
            // CTF006
            // 
            this.CTF006.DataCellType = WinFormLib.Core.DataCellType.None;
            this.CTF006.DataPropertyName = "TF006";
            this.CTF006.FilteringEnabled = false;
            this.CTF006.HeaderText = "条件";
            this.CTF006.Name = "CTF006";
            this.CTF006.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.CTF006.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.CTF006.Width = 170;
            // 
            // CTF008
            // 
            this.CTF008.DataPropertyName = "TF008";
            this.CTF008.HeaderText = "";
            this.CTF008.Name = "CTF008";
            this.CTF008.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.CTF008.Width = 49;
            // 
            // cbNum
            // 
            this.cbNum.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(212)))), ((int)(((byte)(208)))), ((int)(((byte)(200)))));
            this.cbNum.DataControlName = "查找行数";
            this.cbNum.DataFiled = "";
            this.cbNum.DataFormater = WinFormLib.Core.DataCellType.None;
            this.cbNum.DataSource = null;
            this.cbNum.DataViewValue = "";
            this.cbNum.DefaultValue = "";
            this.cbNum.Location = new System.Drawing.Point(1, 2);
            this.cbNum.Name = "cbNum";
            this.cbNum.ReadOnlyValue = false;
            this.cbNum.Size = new System.Drawing.Size(159, 21);
            this.cbNum.TabIndex = 10;
            this.cbNum.TextInfoVisible = true;
            // 
            // txtTF006
            // 
            this.txtTF006.Location = new System.Drawing.Point(431, 4);
            this.txtTF006.Name = "txtTF006";
            this.txtTF006.Size = new System.Drawing.Size(180, 21);
            this.txtTF006.TabIndex = 9;
            // 
            // SearchTool
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnlCenter);
            this.Controls.Add(this.pnlTop);
            this.Name = "SearchTool";
            this.Size = new System.Drawing.Size(700, 144);
            this.pnlTop.ResumeLayout(false);
            this.pnlTop.PerformLayout();
            this.pnlCenter.ResumeLayout(false);
            this.cmsMenu.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvList)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private TextBoxExt txtTF006;
        private System.Windows.Forms.ComboBox cbTF005;
        private System.Windows.Forms.ComboBox cbTF001;
        private System.Windows.Forms.Panel pnlTop;
        private System.Windows.Forms.Panel pnlCenter;
        private System.Windows.Forms.ContextMenuStrip cmsMenu;
        private System.Windows.Forms.ToolStripMenuItem tsMnuItemNormal;
        private System.Windows.Forms.ToolStripMenuItem tsMnuItemHigh;
        private ExoticControls.SplitButton btnSelect;
        private LableComboBox cbNum;
        private DataGridViewEditor dgvList;
        private DataGridViewForzenColumn CTF000;
        private DataGridViewComboBoxButtonColumn CTF001;
        private DataGridViewComboBoxButtonColumn CTF005;
        private DataGridViewAutoFilterTextBoxColumn CTF006;
        private DataGridViewComboBoxButtonColumn CTF008;
    }
}
