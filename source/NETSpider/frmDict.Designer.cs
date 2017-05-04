namespace NETSpider
{
    partial class frmDict
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
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("参数字典", 14, 15);
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDict));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripSplitButton();
            this.toolNewCategory = new System.Windows.Forms.ToolStripMenuItem();
            this.toolNewWord = new System.Windows.Forms.ToolStripMenuItem();
            this.toolExit = new System.Windows.Forms.ToolStripButton();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tvCategory = new System.Windows.Forms.TreeView();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.lsvWord = new System.Windows.Forms.ListView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1,
            this.toolExit});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(642, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolNewCategory,
            this.toolNewWord});
            this.toolStripButton1.Image = global::NETSpider.Properties.Resources.file;
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(64, 22);
            this.toolStripButton1.Text = "新建";
            // 
            // toolNewCategory
            // 
            this.toolNewCategory.Name = "toolNewCategory";
            this.toolNewCategory.Size = new System.Drawing.Size(148, 22);
            this.toolNewCategory.Text = "新建分类";
            this.toolNewCategory.Click += new System.EventHandler(this.toolNewCategory_Click);
            // 
            // toolNewWord
            // 
            this.toolNewWord.Name = "toolNewWord";
            this.toolNewWord.Size = new System.Drawing.Size(148, 22);
            this.toolNewWord.Text = "新建字典健值";
            this.toolNewWord.Click += new System.EventHandler(this.toolNewWord_Click);
            // 
            // toolExit
            // 
            this.toolExit.Image = global::NETSpider.Properties.Resources.file;
            this.toolExit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolExit.Name = "toolExit";
            this.toolExit.Size = new System.Drawing.Size(52, 22);
            this.toolExit.Text = "退出";
            this.toolExit.Click += new System.EventHandler(this.toolExit_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 25);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.tvCategory);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.lsvWord);
            this.splitContainer1.Size = new System.Drawing.Size(642, 407);
            this.splitContainer1.SplitterDistance = 154;
            this.splitContainer1.TabIndex = 1;
            // 
            // tvCategory
            // 
            this.tvCategory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvCategory.ImageIndex = 14;
            this.tvCategory.ImageList = this.imageList1;
            this.tvCategory.Location = new System.Drawing.Point(0, 0);
            this.tvCategory.Name = "tvCategory";
            treeNode1.ImageIndex = 14;
            treeNode1.Name = "tvDict";
            treeNode1.SelectedImageIndex = 15;
            treeNode1.Text = "参数字典";
            this.tvCategory.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode1});
            this.tvCategory.SelectedImageIndex = 0;
            this.tvCategory.Size = new System.Drawing.Size(154, 407);
            this.tvCategory.TabIndex = 0;
            this.tvCategory.AfterLabelEdit += new System.Windows.Forms.NodeLabelEditEventHandler(this.tvCategory_AfterLabelEdit);
            this.tvCategory.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvCategory_AfterSelect);
            this.tvCategory.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.tvCategory_MouseDoubleClick);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "A01.gif");
            this.imageList1.Images.SetKeyName(1, "A02.gif");
            this.imageList1.Images.SetKeyName(2, "A03.gif");
            this.imageList1.Images.SetKeyName(3, "A04.gif");
            this.imageList1.Images.SetKeyName(4, "A07.gif");
            this.imageList1.Images.SetKeyName(5, "A08.gif");
            this.imageList1.Images.SetKeyName(6, "A09.gif");
            this.imageList1.Images.SetKeyName(7, "A10.gif");
            this.imageList1.Images.SetKeyName(8, "A30.gif");
            this.imageList1.Images.SetKeyName(9, "A32.gif");
            this.imageList1.Images.SetKeyName(10, "A33.gif");
            this.imageList1.Images.SetKeyName(11, "A331.gif");
            this.imageList1.Images.SetKeyName(12, "agree.gif");
            this.imageList1.Images.SetKeyName(13, "file.gif");
            this.imageList1.Images.SetKeyName(14, "folder.gif");
            this.imageList1.Images.SetKeyName(15, "folderopen.gif");
            this.imageList1.Images.SetKeyName(16, "Help.gif");
            this.imageList1.Images.SetKeyName(17, "Logo.ico");
            this.imageList1.Images.SetKeyName(18, "noti.ico");
            this.imageList1.Images.SetKeyName(19, "right.gif");
            // 
            // lsvWord
            // 
            this.lsvWord.ContextMenuStrip = this.contextMenuStrip1;
            this.lsvWord.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lsvWord.HideSelection = false;
            this.lsvWord.Location = new System.Drawing.Point(0, 0);
            this.lsvWord.MultiSelect = false;
            this.lsvWord.Name = "lsvWord";
            this.lsvWord.Size = new System.Drawing.Size(484, 407);
            this.lsvWord.TabIndex = 4;
            this.lsvWord.UseCompatibleStateImageBehavior = false;
            this.lsvWord.View = System.Windows.Forms.View.List;
            this.lsvWord.AfterLabelEdit += new System.Windows.Forms.LabelEditEventHandler(this.lsvWord_AfterLabelEdit);
            this.lsvWord.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lsvWord_MouseDoubleClick);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.toolStripMenuItem2});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(149, 48);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(148, 22);
            this.toolStripMenuItem1.Text = "新建字典健值";
            this.toolStripMenuItem1.Click += new System.EventHandler(this.toolStripMenuItem1_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(148, 22);
            this.toolStripMenuItem2.Text = "编辑字典健值";
            this.toolStripMenuItem2.Click += new System.EventHandler(this.toolStripMenuItem2_Click);
            // 
            // frmDict
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(642, 432);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.toolStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmDict";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "字典管理";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmDict_FormClosing);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolExit;
        private System.Windows.Forms.ToolStripSplitButton toolStripButton1;
        private System.Windows.Forms.ToolStripMenuItem toolNewCategory;
        private System.Windows.Forms.ToolStripMenuItem toolNewWord;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TreeView tvCategory;
        private System.Windows.Forms.ListView lsvWord;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;

    }
}