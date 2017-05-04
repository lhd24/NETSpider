using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WinFormLib.Core;
using System.Windows.Forms;

namespace WinFormLib.Components
{
    public class DataGridPager : UserControl
    {
        string defaultInfo = "共有{0}项   每页{1}项  共有{2}页  当前第{3}页";
        public DataGridPager()
        {
            InitializeComponent();
            lbPagerInfo.Text = string.Format(defaultInfo, "0", "0", "0", "0");
        }
        public int CurrentPageIndex
        {
            get;
            set;
        }
        public void SetPagerInfo(int TotalCount, int RowCount, bool Compute)
        {
            if (Compute)
            {
                this.TotalCount = TotalCount;
                this.PageCount = TotalCount / RowCount;
                if (TotalCount % RowCount > 0)
                {
                    PageCount++;
                }
            }
            lbPagerInfo.Text = string.Format(defaultInfo, TotalCount, RowCount, PageCount, CurrentPageIndex + 1);
            txtPageIndex.Text = (CurrentPageIndex + 1) + "";
            toolFirst.Enabled = true;
            toolPrev.Enabled = true;
            toolLast.Enabled = true;
            toolNext.Enabled = true;

            if (CurrentPageIndex == 0)
            {
                toolFirst.Enabled = false;
                toolPrev.Enabled = false;
            }
            if (CurrentPageIndex == PageCount - 1)
            {
                toolLast.Enabled = false;
                toolNext.Enabled = false;
            }
        }
        public void SetPagerInfo(int TotalCount, int RowCount)
        {
            SetPagerInfo(TotalCount, RowCount, true);
        }
        private int TotalCount = 0;
        private int PageCount = 0;
        private IFormDataPager dataPager = null;
        private void DataGridPager_Load(object sender, EventArgs e)
        {
            dataPager = this.FindForm() as IFormDataPager;
            this.CurrentPageIndex = 0;
        }

        private void toolFirst_Click(object sender, EventArgs e)
        {
            if (dataPager != null)
            {
                if (dataPager.TempSerachData == null)
                {
                    dataPager.TempSerachData = dataPager.SerachEntity.Clone();
                }
                this.CurrentPageIndex = 0;
                dataPager.TempSerachData.RowCount = CurrentPageIndex;
                SerachData serachData = dataPager.TempSerachData.Clone();
                dataPager.OnBindData(ref serachData, false);
                this.TotalCount = serachData.TotalCount;
                SetPagerInfo(serachData.TotalCount, TryParse.StrToInt(dataPager.TempSerachData.TopCount), false);
            }
        }

        private void toolPrev_Click(object sender, EventArgs e)
        {
            if (dataPager != null)
            {
                if (dataPager.TempSerachData == null)
                {
                    dataPager.TempSerachData = dataPager.SerachEntity.Clone();
                }
                CurrentPageIndex--;
                dataPager.TempSerachData.RowCount = CurrentPageIndex * TryParse.StrToInt(dataPager.TempSerachData.TopCount);
                SerachData serachData = dataPager.TempSerachData.Clone();
                dataPager.OnBindData(ref serachData, false);
                SetPagerInfo(serachData.TotalCount, TryParse.StrToInt(serachData.TopCount), false);
            }
        }

        private void toolNext_Click(object sender, EventArgs e)
        {
            if (dataPager != null)
            {
                if (dataPager.TempSerachData == null)
                {
                    dataPager.TempSerachData = dataPager.SerachEntity.Clone();
                }
                CurrentPageIndex++;
                dataPager.TempSerachData.RowCount = CurrentPageIndex * TryParse.StrToInt(dataPager.TempSerachData.TopCount);
                SerachData serachData = dataPager.TempSerachData.Clone();
                dataPager.OnBindData(ref serachData, false);
                this.TotalCount = serachData.TotalCount;
                SetPagerInfo(serachData.TotalCount, TryParse.StrToInt(dataPager.TempSerachData.TopCount), false);
            }
        }

        private void toolLast_Click(object sender, EventArgs e)
        {
            if (dataPager != null)
            {
                if (dataPager.TempSerachData == null)
                {
                    dataPager.TempSerachData = dataPager.SerachEntity.Clone();
                }
                CurrentPageIndex = PageCount - 1;
                dataPager.TempSerachData.RowCount = CurrentPageIndex * TryParse.StrToInt(dataPager.TempSerachData.TopCount);
                SerachData serachData = dataPager.TempSerachData.Clone();
                dataPager.OnBindData(ref serachData, false);
                this.TotalCount = serachData.TotalCount;
                SetPagerInfo(this.TotalCount, TryParse.StrToInt(dataPager.TempSerachData.TopCount), false);
            }
        }

        private void toolGo_Click(object sender, EventArgs e)
        {
            int PageIndex = TryParse.StrToInt(txtPageIndex.Text, 0);
            if (PageIndex <= 0 || PageIndex > this.PageCount)
            {
                MessageBoxHelper.ShowError("非法页数,请填写正确的页数");
                return;
            }
            if (dataPager != null)
            {
                if (dataPager.TempSerachData == null)
                {
                    dataPager.TempSerachData = dataPager.SerachEntity.Clone();
                }
                CurrentPageIndex = TryParse.StrToInt(txtPageIndex.Text) - 1;
                dataPager.TempSerachData.RowCount = CurrentPageIndex * TryParse.StrToInt(dataPager.TempSerachData.TopCount);
                SerachData serachData = dataPager.TempSerachData.Clone();
                dataPager.OnBindData(ref serachData, false);
                this.TotalCount = serachData.TotalCount;
                SetPagerInfo(this.TotalCount, TryParse.StrToInt(dataPager.TempSerachData.TopCount), false);
            }
        }

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DataGridPager));
            this.lbPagerInfo = new System.Windows.Forms.Label();
            this.pnlRight = new System.Windows.Forms.Panel();
            this.toolPager = new System.Windows.Forms.ToolStrip();
            this.toolFirst = new System.Windows.Forms.ToolStripButton();
            this.toolPrev = new System.Windows.Forms.ToolStripButton();
            this.toolNext = new System.Windows.Forms.ToolStripButton();
            this.toolLast = new System.Windows.Forms.ToolStripButton();
            this.txtPageIndex = new System.Windows.Forms.ToolStripTextBox();
            this.toolGo = new System.Windows.Forms.ToolStripButton();
            this.pnlRight.SuspendLayout();
            this.toolPager.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbPagerInfo
            // 
            this.lbPagerInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbPagerInfo.Location = new System.Drawing.Point(0, 0);
            this.lbPagerInfo.Name = "lbPagerInfo";
            this.lbPagerInfo.Size = new System.Drawing.Size(760, 25);
            this.lbPagerInfo.TabIndex = 15;
            this.lbPagerInfo.Text = "共有646项   每页40项  共有17页  当前第1页";
            this.lbPagerInfo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pnlRight
            // 
            this.pnlRight.Controls.Add(this.toolPager);
            this.pnlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlRight.Location = new System.Drawing.Point(570, 0);
            this.pnlRight.Name = "pnlRight";
            this.pnlRight.Size = new System.Drawing.Size(190, 25);
            this.pnlRight.TabIndex = 19;
            // 
            // toolPager
            // 
            this.toolPager.AutoSize = false;
            this.toolPager.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(212)))), ((int)(((byte)(208)))), ((int)(((byte)(200)))));
            this.toolPager.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolPager.GripMargin = new System.Windows.Forms.Padding(0);
            this.toolPager.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolFirst,
            this.toolPrev,
            this.toolNext,
            this.toolLast,
            this.txtPageIndex,
            this.toolGo});
            this.toolPager.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.toolPager.Location = new System.Drawing.Point(0, 0);
            this.toolPager.Name = "toolPager";
            this.toolPager.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.toolPager.Size = new System.Drawing.Size(190, 25);
            this.toolPager.TabIndex = 16;
            // 
            // toolFirst
            // 
            this.toolFirst.AutoSize = false;
            this.toolFirst.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.toolFirst.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolFirst.Image = WinFormLib.Properties.Resources.imgFirst_min;
            this.toolFirst.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolFirst.Name = "toolFirst";
            this.toolFirst.Size = new System.Drawing.Size(23, 23);
            this.toolFirst.ToolTipText = "首页";
            this.toolFirst.Click += new System.EventHandler(this.toolFirst_Click);
            // 
            // toolPrev
            // 
            this.toolPrev.AutoSize = false;
            this.toolPrev.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.toolPrev.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolPrev.Image = WinFormLib.Properties.Resources.imgPrev_min;
            this.toolPrev.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolPrev.Name = "toolPrev";
            this.toolPrev.Size = new System.Drawing.Size(23, 23);
            this.toolPrev.ToolTipText = "上一页";
            this.toolPrev.Click += new System.EventHandler(this.toolPrev_Click);
            // 
            // toolNext
            // 
            this.toolNext.AutoSize = false;
            this.toolNext.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.toolNext.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolNext.Image = WinFormLib.Properties.Resources.imgNext_min;
            this.toolNext.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolNext.Name = "toolNext";
            this.toolNext.Size = new System.Drawing.Size(23, 23);
            this.toolNext.ToolTipText = "下一页";
            this.toolNext.Click += new System.EventHandler(this.toolNext_Click);
            // 
            // toolLast
            // 
            this.toolLast.AutoSize = false;
            this.toolLast.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.toolLast.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolLast.Image = WinFormLib.Properties.Resources.imgLast_min;
            this.toolLast.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolLast.Name = "toolLast";
            this.toolLast.Size = new System.Drawing.Size(23, 23);
            this.toolLast.Text = "toolStripButton4";
            this.toolLast.ToolTipText = "尾页";
            this.toolLast.Click += new System.EventHandler(this.toolLast_Click);
            // 
            // txtPageIndex
            // 
            this.txtPageIndex.Name = "txtPageIndex";
            this.txtPageIndex.Size = new System.Drawing.Size(70, 21);
            // 
            // toolGo
            // 
            this.toolGo.AutoSize = false;
            this.toolGo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolGo.Image = WinFormLib.Properties.Resources.BtnQuery_N;
            this.toolGo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolGo.Name = "toolGo";
            this.toolGo.Size = new System.Drawing.Size(23, 23);
            this.toolGo.Text = "toolStripButton5";
            this.toolGo.ToolTipText = "查询";
            this.toolGo.Click += new System.EventHandler(this.toolGo_Click);
            // 
            // DataGridPager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnlRight);
            this.Controls.Add(this.lbPagerInfo);
            this.Name = "DataGridPager";
            this.Size = new System.Drawing.Size(760, 25);
            this.Load += new System.EventHandler(this.DataGridPager_Load);
            this.pnlRight.ResumeLayout(false);
            this.toolPager.ResumeLayout(false);
            this.toolPager.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lbPagerInfo;
        private System.Windows.Forms.Panel pnlRight;
        private System.Windows.Forms.ToolStrip toolPager;
        private System.Windows.Forms.ToolStripButton toolFirst;
        private System.Windows.Forms.ToolStripButton toolPrev;
        private System.Windows.Forms.ToolStripButton toolNext;
        private System.Windows.Forms.ToolStripButton toolLast;
        private System.Windows.Forms.ToolStripTextBox txtPageIndex;
        private System.Windows.Forms.ToolStripButton toolGo;
    }
}
