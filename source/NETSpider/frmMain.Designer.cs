namespace NETSpider
{
    partial class frmMain
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
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("正在运行", 0, 0);
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("已经完成", 7, 7);
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("任务运行区", 0, 0, new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode2});
            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("任务计划", 11, 11);
            System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("任务历史", 10, 10);
            System.Windows.Forms.TreeNode treeNode6 = new System.Windows.Forms.TreeNode("任务计划区", 13, 13, new System.Windows.Forms.TreeNode[] {
            treeNode4,
            treeNode5});
            System.Windows.Forms.TreeNode treeNode7 = new System.Windows.Forms.TreeNode("任务区", 4, 4);
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.tvMain = new System.Windows.Forms.TreeView();
            this.cnTree = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cnNewCategory = new System.Windows.Forms.ToolStripMenuItem();
            this.cnDelCategory = new System.Windows.Forms.ToolStripMenuItem();
            this.imageList2 = new System.Windows.Forms.ImageList(this.components);
            this.txtSysLog = new System.Windows.Forms.RichTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.dgvList = new WinFormLib.Components.DataGridViewer();
            this.cnTask = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cnStartTask = new System.Windows.Forms.ToolStripMenuItem();
            this.cnEditTask = new System.Windows.Forms.ToolStripMenuItem();
            this.cnResetTask = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.cnPauseTask = new System.Windows.Forms.ToolStripMenuItem();
            this.cnDeleteTask = new System.Windows.Forms.ToolStripMenuItem();
            this.cnCopyTask = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.cnViewTask = new System.Windows.Forms.ToolStripMenuItem();
            this.cnViewRunTask = new System.Windows.Forms.ToolStripMenuItem();
            this.cnReStartTask = new System.Windows.Forms.ToolStripMenuItem();
            this.cnPublishTask = new System.Windows.Forms.ToolStripMenuItem();
            this.tabTaskRunLog = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.richSysLog = new System.Windows.Forms.RichTextBox();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripSplitButton1 = new System.Windows.Forms.ToolStripSplitButton();
            this.toolUpdateWin = new System.Windows.Forms.ToolStripMenuItem();
            this.toolEncodeTool = new System.Windows.Forms.ToolStripMenuItem();
            this.toolDictTool = new System.Windows.Forms.ToolStripMenuItem();
            this.toolMinBrower = new System.Windows.Forms.ToolStripMenuItem();
            this.toolSysInfo = new System.Windows.Forms.ToolStripMenuItem();
            this.toolNew = new System.Windows.Forms.ToolStripSplitButton();
            this.toolNewTask = new System.Windows.Forms.ToolStripMenuItem();
            this.toolNewPlan = new System.Windows.Forms.ToolStripMenuItem();
            this.toolNewCategory = new System.Windows.Forms.ToolStripMenuItem();
            this.toolEditTask = new System.Windows.Forms.ToolStripButton();
            this.toolExit = new System.Windows.Forms.ToolStripButton();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.publishProgressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.toolStripStatusLabel3 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripProgressBar1 = new System.Windows.Forms.ToolStripProgressBar();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.nfySpider = new System.Windows.Forms.NotifyIcon(this.components);
            this.cnNotify = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolSendEmail = new System.Windows.Forms.ToolStripMenuItem();
            this.toolExitNotify = new System.Windows.Forms.ToolStripMenuItem();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.cnTree.SuspendLayout();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvList)).BeginInit();
            this.cnTask.SuspendLayout();
            this.tabTaskRunLog.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.cnNotify.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 25);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.splitContainer2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer3);
            this.splitContainer1.Size = new System.Drawing.Size(920, 491);
            this.splitContainer1.SplitterDistance = 220;
            this.splitContainer1.TabIndex = 0;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.tvMain);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.txtSysLog);
            this.splitContainer2.Panel2.Controls.Add(this.label1);
            this.splitContainer2.Size = new System.Drawing.Size(220, 491);
            this.splitContainer2.SplitterDistance = 305;
            this.splitContainer2.TabIndex = 0;
            // 
            // tvMain
            // 
            this.tvMain.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tvMain.ContextMenuStrip = this.cnTree;
            this.tvMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvMain.ImageIndex = 14;
            this.tvMain.ImageList = this.imageList2;
            this.tvMain.Location = new System.Drawing.Point(0, 0);
            this.tvMain.Name = "tvMain";
            treeNode1.ImageIndex = 0;
            treeNode1.Name = "tvOnlineRun";
            treeNode1.SelectedImageIndex = 0;
            treeNode1.Text = "正在运行";
            treeNode2.ImageIndex = 7;
            treeNode2.Name = "tvFinshedRun";
            treeNode2.SelectedImageIndex = 7;
            treeNode2.Text = "已经完成";
            treeNode3.ImageIndex = 0;
            treeNode3.Name = "tvTaskRun";
            treeNode3.SelectedImageIndex = 0;
            treeNode3.Text = "任务运行区";
            treeNode4.ImageIndex = 11;
            treeNode4.Name = "tvTaskPlan";
            treeNode4.SelectedImageIndex = 11;
            treeNode4.Text = "任务计划";
            treeNode5.ImageIndex = 10;
            treeNode5.Name = "tvTaskPlanHistory";
            treeNode5.SelectedImageIndex = 10;
            treeNode5.Text = "任务历史";
            treeNode6.ImageIndex = 13;
            treeNode6.Name = "tvTaskPlan";
            treeNode6.SelectedImageIndex = 13;
            treeNode6.Text = "任务计划区";
            treeNode7.ImageIndex = 4;
            treeNode7.Name = "tvTasks";
            treeNode7.SelectedImageIndex = 4;
            treeNode7.Text = "任务区";
            this.tvMain.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode3,
            treeNode6,
            treeNode7});
            this.tvMain.SelectedImageIndex = 0;
            this.tvMain.ShowNodeToolTips = true;
            this.tvMain.Size = new System.Drawing.Size(220, 305);
            this.tvMain.TabIndex = 0;
            this.tvMain.AfterLabelEdit += new System.Windows.Forms.NodeLabelEditEventHandler(this.tvMain_AfterLabelEdit);
            this.tvMain.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvMain_AfterSelect);
            // 
            // cnTree
            // 
            this.cnTree.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cnNewCategory,
            this.cnDelCategory});
            this.cnTree.Name = "cnTree";
            this.cnTree.Size = new System.Drawing.Size(125, 48);
            // 
            // cnNewCategory
            // 
            this.cnNewCategory.Name = "cnNewCategory";
            this.cnNewCategory.Size = new System.Drawing.Size(124, 22);
            this.cnNewCategory.Text = "新建分类";
            this.cnNewCategory.Click += new System.EventHandler(this.cnNewCategory_Click);
            // 
            // cnDelCategory
            // 
            this.cnDelCategory.Name = "cnDelCategory";
            this.cnDelCategory.Size = new System.Drawing.Size(124, 22);
            this.cnDelCategory.Text = "删除分类";
            this.cnDelCategory.Click += new System.EventHandler(this.cnDelCategory_Click);
            // 
            // imageList2
            // 
            this.imageList2.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList2.ImageStream")));
            this.imageList2.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList2.Images.SetKeyName(0, "task");
            this.imageList2.Images.SetKeyName(1, "run");
            this.imageList2.Images.SetKeyName(2, "export");
            this.imageList2.Images.SetKeyName(3, "OK");
            this.imageList2.Images.SetKeyName(4, "tree");
            this.imageList2.Images.SetKeyName(5, "started");
            this.imageList2.Images.SetKeyName(6, "pause");
            this.imageList2.Images.SetKeyName(7, "stop");
            this.imageList2.Images.SetKeyName(8, "logo");
            this.imageList2.Images.SetKeyName(9, "error");
            this.imageList2.Images.SetKeyName(10, "taskplan");
            this.imageList2.Images.SetKeyName(11, "planrunning");
            this.imageList2.Images.SetKeyName(12, "PlanCompleted");
            this.imageList2.Images.SetKeyName(13, "disabledplan");
            this.imageList2.Images.SetKeyName(14, "log");
            this.imageList2.Images.SetKeyName(15, "folder.gif");
            this.imageList2.Images.SetKeyName(16, "folderopen.gif");
            // 
            // txtSysLog
            // 
            this.txtSysLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtSysLog.Location = new System.Drawing.Point(0, 20);
            this.txtSysLog.Name = "txtSysLog";
            this.txtSysLog.Size = new System.Drawing.Size(220, 162);
            this.txtSysLog.TabIndex = 3;
            this.txtSysLog.Text = "";
            // 
            // label1
            // 
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(220, 20);
            this.label1.TabIndex = 2;
            this.label1.Text = "系统日志";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // splitContainer3
            // 
            this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer3.Location = new System.Drawing.Point(0, 0);
            this.splitContainer3.Name = "splitContainer3";
            this.splitContainer3.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.Controls.Add(this.dgvList);
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.Controls.Add(this.tabTaskRunLog);
            this.splitContainer3.Size = new System.Drawing.Size(696, 491);
            this.splitContainer3.SplitterDistance = 231;
            this.splitContainer3.TabIndex = 0;
            // 
            // dgvList
            // 
            this.dgvList.AllowUserToAddRows = false;
            this.dgvList.AllowUserToDeleteRows = false;
            this.dgvList.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(206)))), ((int)(((byte)(249)))), ((int)(((byte)(231)))));
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(183)))), ((int)(((byte)(220)))), ((int)(((byte)(242)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.Black;
            this.dgvList.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvList.AutoColumnWidth = true;
            this.dgvList.BackgroundColor = System.Drawing.Color.DarkGray;
            this.dgvList.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.dgvList.ContextMenuStrip = this.cnTask;
            this.dgvList.CurrentPageIndex = 0;
            this.dgvList.DataGridPager = null;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(183)))), ((int)(((byte)(220)))), ((int)(((byte)(242)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvList.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvList.EnableHeadersVisualStyles = false;
            this.dgvList.EnterIsTab = false;
            this.dgvList.Location = new System.Drawing.Point(0, 0);
            this.dgvList.Margin = new System.Windows.Forms.Padding(0);
            this.dgvList.MenuStripShowFlag = false;
            this.dgvList.Name = "dgvList";
            this.dgvList.ReadOnly = true;
            this.dgvList.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
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
            this.dgvList.RowTemplate.Height = 25;
            this.dgvList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvList.Size = new System.Drawing.Size(696, 231);
            this.dgvList.TabIndex = 0;
            this.dgvList.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvList_CellDoubleClick);
            this.dgvList.RowStateChanged += new System.Windows.Forms.DataGridViewRowStateChangedEventHandler(this.dgvList_RowStateChanged);
            // 
            // cnTask
            // 
            this.cnTask.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cnStartTask,
            this.cnEditTask,
            this.cnResetTask,
            this.toolStripSeparator1,
            this.cnPauseTask,
            this.cnDeleteTask,
            this.cnCopyTask,
            this.toolStripSeparator2,
            this.cnViewTask,
            this.cnViewRunTask,
            this.cnReStartTask,
            this.cnPublishTask});
            this.cnTask.Name = "cnTask";
            this.cnTask.Size = new System.Drawing.Size(157, 236);
            this.cnTask.Text = "开始任务";
            this.cnTask.Opening += new System.ComponentModel.CancelEventHandler(this.cnTask_Opening);
            // 
            // cnStartTask
            // 
            this.cnStartTask.Image = global::NETSpider.Properties.Resources.A01;
            this.cnStartTask.Name = "cnStartTask";
            this.cnStartTask.Size = new System.Drawing.Size(156, 22);
            this.cnStartTask.Text = "开始任务";
            this.cnStartTask.Click += new System.EventHandler(this.cnStartTask_Click);
            // 
            // cnEditTask
            // 
            this.cnEditTask.Image = global::NETSpider.Properties.Resources.A02;
            this.cnEditTask.Name = "cnEditTask";
            this.cnEditTask.Size = new System.Drawing.Size(156, 22);
            this.cnEditTask.Text = "编辑任务";
            this.cnEditTask.Click += new System.EventHandler(this.cnEditTask_Click);
            // 
            // cnResetTask
            // 
            this.cnResetTask.Image = global::NETSpider.Properties.Resources.A02;
            this.cnResetTask.Name = "cnResetTask";
            this.cnResetTask.Size = new System.Drawing.Size(156, 22);
            this.cnResetTask.Text = "重置任务";
            this.cnResetTask.Click += new System.EventHandler(this.cnResetTask_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(153, 6);
            // 
            // cnPauseTask
            // 
            this.cnPauseTask.Image = global::NETSpider.Properties.Resources.A03;
            this.cnPauseTask.Name = "cnPauseTask";
            this.cnPauseTask.Size = new System.Drawing.Size(156, 22);
            this.cnPauseTask.Text = "暂停任务";
            this.cnPauseTask.Click += new System.EventHandler(this.cnPauseTask_Click);
            // 
            // cnDeleteTask
            // 
            this.cnDeleteTask.Image = global::NETSpider.Properties.Resources.A03;
            this.cnDeleteTask.Name = "cnDeleteTask";
            this.cnDeleteTask.Size = new System.Drawing.Size(156, 22);
            this.cnDeleteTask.Text = "删除任务";
            this.cnDeleteTask.Click += new System.EventHandler(this.cnDeleteTask_Click);
            // 
            // cnCopyTask
            // 
            this.cnCopyTask.Image = global::NETSpider.Properties.Resources.A03;
            this.cnCopyTask.Name = "cnCopyTask";
            this.cnCopyTask.Size = new System.Drawing.Size(156, 22);
            this.cnCopyTask.Text = "复制任务";
            this.cnCopyTask.Click += new System.EventHandler(this.cnCopyTask_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(153, 6);
            // 
            // cnViewTask
            // 
            this.cnViewTask.Image = global::NETSpider.Properties.Resources.A30;
            this.cnViewTask.Name = "cnViewTask";
            this.cnViewTask.Size = new System.Drawing.Size(156, 22);
            this.cnViewTask.Text = "查看数据";
            this.cnViewTask.Click += new System.EventHandler(this.cnViewTask_Click);
            // 
            // cnViewRunTask
            // 
            this.cnViewRunTask.Image = global::NETSpider.Properties.Resources.A30;
            this.cnViewRunTask.Name = "cnViewRunTask";
            this.cnViewRunTask.Size = new System.Drawing.Size(156, 22);
            this.cnViewRunTask.Text = "查看运行记录";
            this.cnViewRunTask.Click += new System.EventHandler(this.cnViewRunTask_Click);
            // 
            // cnReStartTask
            // 
            this.cnReStartTask.Image = global::NETSpider.Properties.Resources.A01;
            this.cnReStartTask.Name = "cnReStartTask";
            this.cnReStartTask.Size = new System.Drawing.Size(156, 22);
            this.cnReStartTask.Text = "重新开始(错误)";
            this.cnReStartTask.Click += new System.EventHandler(this.cnReStartTask_Click);
            // 
            // cnPublishTask
            // 
            this.cnPublishTask.Image = global::NETSpider.Properties.Resources.file;
            this.cnPublishTask.Name = "cnPublishTask";
            this.cnPublishTask.Size = new System.Drawing.Size(156, 22);
            this.cnPublishTask.Text = "发布数据";
            this.cnPublishTask.Click += new System.EventHandler(this.cnPublishTask_Click);
            // 
            // tabTaskRunLog
            // 
            this.tabTaskRunLog.Appearance = System.Windows.Forms.TabAppearance.FlatButtons;
            this.tabTaskRunLog.Controls.Add(this.tabPage1);
            this.tabTaskRunLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabTaskRunLog.Location = new System.Drawing.Point(0, 0);
            this.tabTaskRunLog.Name = "tabTaskRunLog";
            this.tabTaskRunLog.SelectedIndex = 0;
            this.tabTaskRunLog.Size = new System.Drawing.Size(696, 256);
            this.tabTaskRunLog.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.richSysLog);
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(688, 227);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "系统日志";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // richSysLog
            // 
            this.richSysLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richSysLog.Location = new System.Drawing.Point(3, 3);
            this.richSysLog.Name = "richSysLog";
            this.richSysLog.Size = new System.Drawing.Size(682, 221);
            this.richSysLog.TabIndex = 0;
            this.richSysLog.Text = "";
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripSplitButton1,
            this.toolNew,
            this.toolEditTask,
            this.toolExit});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(920, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripSplitButton1
            // 
            this.toolStripSplitButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolUpdateWin,
            this.toolEncodeTool,
            this.toolDictTool,
            this.toolMinBrower,
            this.toolSysInfo});
            this.toolStripSplitButton1.Image = global::NETSpider.Properties.Resources.right;
            this.toolStripSplitButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripSplitButton1.Name = "toolStripSplitButton1";
            this.toolStripSplitButton1.Size = new System.Drawing.Size(64, 22);
            this.toolStripSplitButton1.Text = "工具";
            // 
            // toolUpdateWin
            // 
            this.toolUpdateWin.Name = "toolUpdateWin";
            this.toolUpdateWin.Size = new System.Drawing.Size(153, 22);
            this.toolUpdateWin.Text = "任务升级";
            // 
            // toolEncodeTool
            // 
            this.toolEncodeTool.Name = "toolEncodeTool";
            this.toolEncodeTool.Size = new System.Drawing.Size(153, 22);
            this.toolEncodeTool.Text = "网址解码/编码";
            this.toolEncodeTool.Click += new System.EventHandler(this.toolEncodeTool_Click);
            // 
            // toolDictTool
            // 
            this.toolDictTool.Name = "toolDictTool";
            this.toolDictTool.Size = new System.Drawing.Size(153, 22);
            this.toolDictTool.Text = "维护参数字典";
            this.toolDictTool.Click += new System.EventHandler(this.toolDictTool_Click);
            // 
            // toolMinBrower
            // 
            this.toolMinBrower.Name = "toolMinBrower";
            this.toolMinBrower.Size = new System.Drawing.Size(153, 22);
            this.toolMinBrower.Text = "Mini浏览器";
            // 
            // toolSysInfo
            // 
            this.toolSysInfo.Name = "toolSysInfo";
            this.toolSysInfo.Size = new System.Drawing.Size(153, 22);
            this.toolSysInfo.Text = "查看系统信息";
            this.toolSysInfo.Click += new System.EventHandler(this.toolSysInfo_Click);
            // 
            // toolNew
            // 
            this.toolNew.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolNewTask,
            this.toolNewPlan,
            this.toolNewCategory});
            this.toolNew.Image = global::NETSpider.Properties.Resources.file;
            this.toolNew.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolNew.Name = "toolNew";
            this.toolNew.Size = new System.Drawing.Size(64, 22);
            this.toolNew.Text = "新建";
            // 
            // toolNewTask
            // 
            this.toolNewTask.Name = "toolNewTask";
            this.toolNewTask.Size = new System.Drawing.Size(148, 22);
            this.toolNewTask.Text = "新建任务";
            this.toolNewTask.Click += new System.EventHandler(this.toolNewTask_Click);
            // 
            // toolNewPlan
            // 
            this.toolNewPlan.Name = "toolNewPlan";
            this.toolNewPlan.Size = new System.Drawing.Size(148, 22);
            this.toolNewPlan.Text = "新建任务计划";
            this.toolNewPlan.Click += new System.EventHandler(this.toolNewPlan_Click);
            // 
            // toolNewCategory
            // 
            this.toolNewCategory.Name = "toolNewCategory";
            this.toolNewCategory.Size = new System.Drawing.Size(148, 22);
            this.toolNewCategory.Text = "新建任务分类";
            this.toolNewCategory.Click += new System.EventHandler(this.toolNewCategory_Click);
            // 
            // toolEditTask
            // 
            this.toolEditTask.Image = global::NETSpider.Properties.Resources.file;
            this.toolEditTask.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolEditTask.Name = "toolEditTask";
            this.toolEditTask.Size = new System.Drawing.Size(76, 22);
            this.toolEditTask.Text = "编辑任务";
            this.toolEditTask.Click += new System.EventHandler(this.toolEditTask_Click);
            // 
            // toolExit
            // 
            this.toolExit.Image = global::NETSpider.Properties.Resources.A03;
            this.toolExit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolExit.Name = "toolExit";
            this.toolExit.Size = new System.Drawing.Size(52, 22);
            this.toolExit.Text = "退出";
            this.toolExit.Click += new System.EventHandler(this.toolExit_Click);
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
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.toolStripStatusLabel2,
            this.publishProgressBar,
            this.toolStripStatusLabel3,
            this.toolStripProgressBar1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 516);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(920, 25);
            this.statusStrip1.SizingGrip = false;
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Image = global::NETSpider.Properties.Resources.A08;
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(48, 20);
            this.toolStripStatusLabel1.Text = "在线";
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.AutoSize = false;
            this.toolStripStatusLabel2.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top)
                        | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right)
                        | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(200, 20);
            // 
            // publishProgressBar
            // 
            this.publishProgressBar.AutoSize = false;
            this.publishProgressBar.Name = "publishProgressBar";
            this.publishProgressBar.Size = new System.Drawing.Size(150, 19);
            // 
            // toolStripStatusLabel3
            // 
            this.toolStripStatusLabel3.Name = "toolStripStatusLabel3";
            this.toolStripStatusLabel3.Size = new System.Drawing.Size(92, 20);
            this.toolStripStatusLabel3.Text = "当前正在导出：";
            // 
            // toolStripProgressBar1
            // 
            this.toolStripProgressBar1.AutoSize = false;
            this.toolStripProgressBar1.Name = "toolStripProgressBar1";
            this.toolStripProgressBar1.Size = new System.Drawing.Size(420, 19);
            // 
            // nfySpider
            // 
            this.nfySpider.ContextMenuStrip = this.cnNotify;
            this.nfySpider.Icon = ((System.Drawing.Icon)(resources.GetObject("nfySpider.Icon")));
            this.nfySpider.Text = "NETSpider";
            this.nfySpider.Visible = true;
            // 
            // cnNotify
            // 
            this.cnNotify.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolSendEmail,
            this.toolExitNotify});
            this.cnNotify.Name = "cnNotify";
            this.cnNotify.Size = new System.Drawing.Size(132, 48);
            // 
            // toolSendEmail
            // 
            this.toolSendEmail.Name = "toolSendEmail";
            this.toolSendEmail.Size = new System.Drawing.Size(131, 22);
            this.toolSendEmail.Text = "发送Email";
            // 
            // toolExitNotify
            // 
            this.toolExitNotify.Name = "toolExitNotify";
            this.toolExitNotify.Size = new System.Drawing.Size(131, 22);
            this.toolExitNotify.Text = "退出";
            this.toolExitNotify.Click += new System.EventHandler(this.toolExitNotify_Click);
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(920, 541);
            this.ContextMenuStrip = this.cnTask;
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.statusStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "NETSpider 运行主窗口";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.ResumeLayout(false);
            this.cnTree.ResumeLayout(false);
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel2.ResumeLayout(false);
            this.splitContainer3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvList)).EndInit();
            this.cnTask.ResumeLayout(false);
            this.tabTaskRunLog.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.cnNotify.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.SplitContainer splitContainer3;
        private System.Windows.Forms.TabControl tabTaskRunLog;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TreeView tvMain;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ImageList imageList2;
        private WinFormLib.Components.DataGridViewer dgvList;
        private System.Windows.Forms.ContextMenuStrip cnTask;
        private System.Windows.Forms.ToolStripMenuItem cnStartTask;
        private System.Windows.Forms.ToolStripMenuItem cnResetTask;
        private System.Windows.Forms.ToolStripMenuItem cnEditTask;
        private System.Windows.Forms.ToolStripMenuItem cnPauseTask;
        private System.Windows.Forms.ToolStripMenuItem cnDeleteTask;
        private System.Windows.Forms.ContextMenuStrip cnTree;
        private System.Windows.Forms.ToolStripMenuItem cnNewCategory;
        private System.Windows.Forms.RichTextBox richSysLog;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.ToolStripProgressBar publishProgressBar;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel3;
        private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolStripButton toolEditTask;
        private System.Windows.Forms.ToolStripMenuItem cnCopyTask;
        private System.Windows.Forms.RichTextBox txtSysLog;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.ToolStripMenuItem cnViewTask;
        private System.Windows.Forms.ToolStripMenuItem cnViewRunTask;
        private System.Windows.Forms.ToolStripMenuItem cnReStartTask;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem cnPublishTask;
        private System.Windows.Forms.ToolStripSplitButton toolStripSplitButton1;
        private System.Windows.Forms.ToolStripMenuItem toolUpdateWin;
        private System.Windows.Forms.ToolStripMenuItem toolEncodeTool;
        private System.Windows.Forms.ToolStripMenuItem toolDictTool;
        private System.Windows.Forms.ToolStripMenuItem toolMinBrower;
        private System.Windows.Forms.ToolStripMenuItem toolSysInfo;
        private System.Windows.Forms.ToolStripSplitButton toolNew;
        private System.Windows.Forms.ToolStripMenuItem toolNewTask;
        private System.Windows.Forms.ToolStripMenuItem toolNewPlan;
        private System.Windows.Forms.ToolStripMenuItem toolNewCategory;
        private System.Windows.Forms.ToolStripButton toolExit;
        private System.Windows.Forms.NotifyIcon nfySpider;
        private System.Windows.Forms.ContextMenuStrip cnNotify;
        private System.Windows.Forms.ToolStripMenuItem toolSendEmail;
        private System.Windows.Forms.ToolStripMenuItem toolExitNotify;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.ToolStripMenuItem cnDelCategory;
    }
}