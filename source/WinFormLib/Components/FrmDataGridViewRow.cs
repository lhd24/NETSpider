using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using WinFormLib.Core;
using WinFormLib.Controls;

namespace WinFormLib.Components
{
    public class FrmDataGridViewRow : FormBase
    {
        DataGridView _DataGridView;
        public FrmDataGridViewRow(DataGridView _DataGridView)
        {
            if (_DataGridView == null)
                this.Close();

            this._DataGridView = _DataGridView;
            this.DataGridViewRowIndex = -1;
            InitializeComponent();
            this.TopMost = true;
        }
        private int DataGridViewRowIndex = -1;
        void _DataGridView_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            if (_DataGridView.CurrentCell != null && DataGridViewRowIndex != -1 && this._DataGridView.Rows.Count > DataGridViewRowIndex)
            {
                if (_DataGridView.CurrentCell.RowIndex != DataGridViewRowIndex)
                {
                    _DataGridView.CurrentCell = _DataGridView[_DataGridView.CurrentCell.ColumnIndex, DataGridViewRowIndex];
                    Reload(this._DataGridView, this._DataGridView.Rows[DataGridViewRowIndex], false);
                }
            }
        }

        private void Reload(DataGridView dgvList, DataGridViewRow dgvr, bool NewControl)
        {

            if (dgvList == null || dgvr == null) return;
            if (NewControl)
                pnlMain.Controls.Clear();
            int StartY = 5;
            foreach (DataGridViewCell dgvc in dgvr.Cells)
            {
                LableTextBox lblTextBox;
                if (!NewControl)
                {
                    lblTextBox = pnlMain.Controls[dgvList.Columns[dgvc.ColumnIndex].Name] as LableTextBox;
                }
                else
                {
                    lblTextBox = new LableTextBox();
                    lblTextBox.Width = this.Width - 20;
                    lblTextBox.DataControlName = dgvList.Columns[dgvc.ColumnIndex].HeaderText;
                    lblTextBox.Name = dgvList.Columns[dgvc.ColumnIndex].Name;
                    lblTextBox.Location = new Point(10, StartY);
                    pnlMain.Controls.Add(lblTextBox);
                    StartY += (lblTextBox.Height + 5);
                }
                lblTextBox.DataViewValue = TryParse.ToString(dgvc.FormattedValue);
            }

            if (btnFirst != null) btnFirst.Enabled = true;
            if (btnPrev != null) btnPrev.Enabled = true;
            if (btnNext != null) btnNext.Enabled = true;
            if (btnLast != null) btnLast.Enabled = true;
            if (dgvList.SelectedRows[0].Index == 0)
            {
                if (btnFirst != null) btnFirst.Enabled = false;
                if (btnPrev != null) btnPrev.Enabled = false;
            }
            if (dgvList.SelectedRows[0].Index == dgvList.Rows.Count - 1)
            {
                if (btnNext != null) btnNext.Enabled = false;
                if (btnLast != null) btnLast.Enabled = false;
            }
        }

        private void btnFirst_Click(object sender, EventArgs e)
        {
            if (_DataGridView != null && _DataGridView.RowCount > 0)
            {
                DataGridViewRowIndex = 0;
                _DataGridView.CurrentCell = _DataGridView[_DataGridView.CurrentCell.ColumnIndex, 0];
                Reload(_DataGridView, _DataGridView.CurrentRow, false);
            }
        }

        private void FrmDataGridViewRow_Load(object sender, EventArgs e)
        {
            Reload(_DataGridView, _DataGridView.CurrentRow, true);
        }

        private void btnPrev_Click(object sender, EventArgs e)
        {
            if (_DataGridView != null && _DataGridView.CurrentCell.RowIndex - 1 >= 0)
            {
                DataGridViewRowIndex = _DataGridView.CurrentCell.RowIndex - 1;
                _DataGridView.CurrentCell = _DataGridView[_DataGridView.CurrentCell.ColumnIndex, DataGridViewRowIndex];
                Reload(_DataGridView, _DataGridView.CurrentRow, false);
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (_DataGridView != null && _DataGridView.CurrentCell.RowIndex + 1 < this._DataGridView.RowCount)
            {
                DataGridViewRowIndex = _DataGridView.CurrentCell.RowIndex + 1;
                _DataGridView.CurrentCell = _DataGridView[_DataGridView.CurrentCell.ColumnIndex, _DataGridView.CurrentCell.RowIndex + 1];
                Reload(_DataGridView, _DataGridView.CurrentRow, false);
            }
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            if (_DataGridView != null && _DataGridView.RowCount > 0)
            {
                DataGridViewRowIndex = _DataGridView.RowCount - 1;
                _DataGridView.CurrentCell = _DataGridView[_DataGridView.CurrentCell.ColumnIndex, DataGridViewRowIndex];
                Reload(_DataGridView, _DataGridView.CurrentRow, false);
            }
        }



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
            if (this._DataGridView != null)
            {
                this._DataGridView.DataBindingComplete -= new DataGridViewBindingCompleteEventHandler(_DataGridView_DataBindingComplete);
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
            if (this._DataGridView != null)
            {
                this._DataGridView.DataBindingComplete += new DataGridViewBindingCompleteEventHandler(_DataGridView_DataBindingComplete);
            }
            this.pnlTop = new System.Windows.Forms.Panel();
            this.mnuMain = new System.Windows.Forms.ToolStrip();
            this.btnLast = new System.Windows.Forms.ToolStripButton();
            this.btnNext = new System.Windows.Forms.ToolStripButton();
            this.btnPrev = new System.Windows.Forms.ToolStripButton();
            this.btnFirst = new System.Windows.Forms.ToolStripButton();
            this.pnlMain = new System.Windows.Forms.Panel();
            this.pnlTop.SuspendLayout();
            this.mnuMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlTop
            // 
            this.pnlTop.Controls.Add(this.mnuMain);
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTop.Location = new System.Drawing.Point(0, 0);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Size = new System.Drawing.Size(624, 54);
            this.pnlTop.TabIndex = 0;
            // 
            // mnuMain
            // 
            this.mnuMain.ImageScalingSize = new System.Drawing.Size(25, 25);
            this.mnuMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnLast,
            this.btnNext,
            this.btnPrev,
            this.btnFirst});
            this.mnuMain.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.mnuMain.Location = new System.Drawing.Point(0, 0);
            this.mnuMain.Name = "mnuMain";
            this.mnuMain.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.mnuMain.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.mnuMain.Size = new System.Drawing.Size(624, 32);
            this.mnuMain.TabIndex = 3;
            this.mnuMain.Text = "FormMenu";
            // 
            // btnLast
            // 
            this.btnLast.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnLast.Image = global::WinFormLib.Properties.Resources.imgLast_min;
            this.btnLast.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnLast.Name = "btnLast";
            this.btnLast.Size = new System.Drawing.Size(29, 29);
            this.btnLast.Text = "最后一笔";
            this.btnLast.Click += new System.EventHandler(this.btnLast_Click);
            // 
            // btnNext
            // 
            this.btnNext.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnNext.Image = global::WinFormLib.Properties.Resources.imgNext_min;
            this.btnNext.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(29, 29);
            this.btnNext.Text = "下一笔";
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // btnPrev
            // 
            this.btnPrev.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnPrev.Image = global::WinFormLib.Properties.Resources.imgPrev_min;
            this.btnPrev.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnPrev.Name = "btnPrev";
            this.btnPrev.Size = new System.Drawing.Size(29, 29);
            this.btnPrev.Text = "上一笔";
            this.btnPrev.Click += new System.EventHandler(this.btnPrev_Click);
            // 
            // btnFirst
            // 
            this.btnFirst.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnFirst.Image = global::WinFormLib.Properties.Resources.imgFirst_min;
            this.btnFirst.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnFirst.Name = "btnFirst";
            this.btnFirst.Size = new System.Drawing.Size(29, 29);
            this.btnFirst.Text = "最先一笔";
            this.btnFirst.Click += new System.EventHandler(this.btnFirst_Click);
            // 
            // pnlMain
            // 
            this.pnlMain.AutoScroll = true;
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(0, 54);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(624, 444);
            this.pnlMain.TabIndex = 1;
            // 
            // FrmDataGridViewRow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(624, 498);
            this.Controls.Add(this.pnlMain);
            this.Controls.Add(this.pnlTop);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Name = "FrmDataGridViewRow";
            this.Text = "表格呈现";
            this.Load += new System.EventHandler(this.FrmDataGridViewRow_Load);
            this.pnlTop.ResumeLayout(false);
            this.pnlTop.PerformLayout();
            this.mnuMain.ResumeLayout(false);
            this.mnuMain.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlTop;
        private System.Windows.Forms.Panel pnlMain;
        protected System.Windows.Forms.ToolStrip mnuMain;
        private System.Windows.Forms.ToolStripButton btnLast;
        private System.Windows.Forms.ToolStripButton btnNext;
        private System.Windows.Forms.ToolStripButton btnPrev;
        private System.Windows.Forms.ToolStripButton btnFirst;
    }
}
