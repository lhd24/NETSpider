using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NETSpider.Entity;
using NETSpider.Gather;
using WinFormLib.Core;
using WinFormLib.Components;
using NETSpider.Controls;
using System.Threading;
using System.Collections;
namespace NETSpider
{
    public partial class frmMain : Form
    {
        private enum EnumFormRunState
        {
            Run = 1,
            Plan = 2,
            Task = 3,
        }
        private List<TaskPlan> _taskPlanList;
        private TaskCategroyList _taskCategroyList;
        private TaskRunItemList _runItemList;
        private TaskList _taskList = null;
        private bool resetRowIndex = true;
        private Dictionary<int, cGatherTaskTabPageManage> dicList = new Dictionary<int, cGatherTaskTabPageManage>();
        private object syncRoot = new object();
        private IDataTablePublish exportFile = new DataTable2File();
        private bool m_RunStartFlag = false;
        private EnumFormRunState CurrentState
        {
            get;
            set;
        }

        public frmMain()
        {
            InitializeComponent();
            this.CurrentState = EnumFormRunState.Run;
            this.SetStyle(ControlStyles.DoubleBuffer | ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint, true);
            this.UpdateStyles();

            //加载托盘图标
            this.nfySpider.Visible = true;
            this.nfySpider.ShowBalloonTip(2, "已经启动", "已经启动", ToolTipIcon.Info);
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            this.BindRunTask(EnumGloabParas.EnumThreadState.Normal);
        }
        public void FormInit()
        {
            this.LoadRunItemList(true);
            this.LoadTaskCategroyList(true);
            this.InitTaskList();
            tvMain.Nodes["tvTaskRun"].Expand();
            tvMain.Nodes["tvTasks"].Expand();
            tvMain.Nodes["tvTaskPlan"].Expand();
            exportFile.e_ExportStartingEvent += new ExportStartingEvent(exportFile_e_ExportStartingEvent);
            exportFile.e_ExportProgressingEvent += new ExportProgressingEvent(exportFile_e_ExportProgressingEvent);
            exportFile.e_ExportEndedEvent += new ExportEndedEvent(exportFile_e_ExportEndedEvent);
            toolStripProgressBar1.Minimum = 0;
            toolStripProgressBar1.Maximum = 100;
            timer1.Start();
        }

        #region exportFile
        void exportFile_e_ExportEndedEvent(object sender, cExportEndedEventArgs e)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new MethodInvoker(delegate()
                {
                    if (e.IsCompleted)
                    {
                        MessageBoxHelper.Show(e.Message);
                    }
                    else
                    {
                        MessageBoxHelper.ShowError(e.Message);
                    }
                    toolStripProgressBar1.Value = 0;
                    toolStripStatusLabel3.Text = "当前正在导出：";
                }));
            }
        }

        void exportFile_e_ExportProgressingEvent(object sender, ExportProgressingArgs e)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new MethodInvoker(delegate()
                {
                    toolStripProgressBar1.Value = e.ExportPercent;
                }));
            }

        }

        void exportFile_e_ExportStartingEvent(object sender, cExportStartEventArgs e)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new MethodInvoker(delegate()
                {
                    toolStripProgressBar1.Value = 0;
                    toolStripStatusLabel3.Text = "正在导出(" + e.TotalCount + ")：";
                }));
            }
        }
        #endregion


        private void InitTaskList()
        {
            tvMain.Nodes["tvTasks"].Nodes.Clear();
            if (_taskCategroyList != null)
            {
                foreach (TaskCategroy item in _taskCategroyList)
                {
                    InitTaskCategory(item, tvMain.Nodes["tvTasks"]);
                }
            }
        }

        private void InitTaskCategory(TaskCategroy item, TreeNode parentNode)
        {
            TreeNode newNode = new TreeNode()
            {
                Name = "category_" + item.CategroyName.Value,
                Text = item.CategroyName.Value,
                Tag = item.TaskItemList,
                ImageIndex = 15,
                SelectedImageIndex = 16,
            };
            parentNode.Nodes.Add(newNode);
        }


        #region Tree
        private void tvMain_AfterLabelEdit(object sender, NodeLabelEditEventArgs e)
        {

            if (string.IsNullOrEmpty(e.Label))
            {
                e.CancelEdit = true;
                tvMain.LabelEdit = true;
                e.Node.BeginEdit();
                return;
            }
            if (e.Label == "data" || e.Label == "run")
            {
                e.CancelEdit = true;
                tvMain.LabelEdit = true;
                e.Node.BeginEdit();
                return;
            }
            if (_taskCategroyList != null)
            {
                string errMsg = string.Empty;
                if (_taskCategroyList.Where(q => q.CategroyName.Value == e.Label).FirstOrDefault() != null)
                {
                    e.CancelEdit = true;
                    tvMain.LabelEdit = true;
                    e.Node.BeginEdit();
                    return;
                }
                e.Node.Name = "category_" + e.Label;
                _taskCategroyList.Add(new TaskCategroy()
                {
                    CategroyID = _taskCategroyList.Count + 1,
                    CategroyName = e.Label,
                    TaskItemList = new List<TaskItemBase>(),
                    Version = StaticConst.Version,
                });
                XmlHelper.Save2File<TaskCategroyList>(_taskCategroyList, Program.GetConfigPath("category.xml"), ref errMsg);
                if (!string.IsNullOrEmpty(errMsg))
                {
                    WinFormLib.Core.MessageBoxHelper.ShowError(errMsg);
                    return;
                }
                if (!System.IO.Directory.Exists(Program.GetConfigPath(@"categroy\" + e.Label)))
                {
                    System.IO.Directory.CreateDirectory(Program.GetConfigPath(@"categroy\" + e.Label));
                }
                tvMain.LabelEdit = false;
            }
        }
        private void tvMain_AfterSelect(object sender, TreeViewEventArgs e)
        {
            //this.toolNew.Enabled = false;
            //this.toolEditTask.Enabled = false;
            if (e.Node != null)
            {
                if (e.Node.FullPath.IndexOf("任务区") != -1)
                {
                    this.CurrentState = EnumFormRunState.Task;
                    if (e.Node.FullPath.IndexOf("任务区\\") != -1)
                    {
                        this.toolNewTask.Enabled = true;
                    }
                    while (this.tabTaskRunLog.TabPages.Count > 1)
                    {
                        this.tabTaskRunLog.TabPages.RemoveAt(1);
                    }
                }
                else if (e.Node.FullPath.IndexOf("任务运行区") != -1)
                {
                    this.CurrentState = EnumFormRunState.Run;

                }
                else if (e.Node.FullPath.IndexOf("任务计划区") != -1)
                {
                    this.CurrentState = EnumFormRunState.Plan;


                }
                else
                {
                    throw new Exception("未知tree");
                }
                ReloadDataGridView(e.Node);
            }
            Application.DoEvents();
        }

        #endregion
        private void ReloadDataGridView(TreeNode node)
        {
            if (this.CurrentState == EnumFormRunState.Task && node.Name.IndexOf("category_") != -1)
            {
                BindTask(node);
            }
            else if (this.CurrentState == EnumFormRunState.Run)
            {
                if (node.FullPath == @"任务运行区\已经完成")
                {
                    while (this.tabTaskRunLog.TabPages.Count > 1)
                    {
                        this.tabTaskRunLog.TabPages.RemoveAt(1);
                    }
                    BindRunTask(EnumGloabParas.EnumThreadState.SpiderCompleted);
                }
                else
                {
                    foreach (cGatherTaskTabPageManage tabPageManager in tabPageManageList)
                    {
                        tabPageManager.AddTab();
                    }
                    BindRunTask(EnumGloabParas.EnumThreadState.Normal);
                }

            }
            else if (this.CurrentState == EnumFormRunState.Plan)
            {
                if (node.FullPath == @"任务计划区\任务计划")
                {
                    BindTaskPlan();
                }
                else if (node.FullPath == @"任务计划区\任务历史")
                {

                }
            }
        }

        private void LoadTaskCategroyList(bool reload)
        {
            if (reload || _taskCategroyList == null)
            {
                string errMsg = string.Empty;
                _taskCategroyList = XmlHelper.LoadFromXml<TaskCategroyList>(Program.GetConfigPath("category.xml"), ref errMsg);
                if (!string.IsNullOrEmpty(errMsg))
                {
                    WinFormLib.Core.MessageBoxHelper.ShowError(errMsg);
                    return;
                }
            }
        }

        private TaskRunTickItemList _runTickItemList;

        private void LoadRunTickItemList(bool reload)
        {
            if (reload || _runTickItemList == null)
            {
                string errMsg = string.Empty;
                _runTickItemList = XmlHelper.LoadFromXml<TaskRunTickItemList>(Program.GetConfigPath("run.xml"), ref errMsg);
                if (!string.IsNullOrEmpty(errMsg))
                {
                    WinFormLib.Core.MessageBoxHelper.ShowError(errMsg);
                    return;
                }
            }
        }

        private void LoadRunItemList(bool reload)
        {
            if (reload || _runTickItemList == null)
            {
                LoadRunTickItemList(reload);
            }
            if (_runTickItemList != null)
            {
                _runItemList = new TaskRunItemList();
                foreach (var item in _runTickItemList)
                {
                    string filePath = @"run\" + item.TaskTempName + ".xml";
                    string errMsg = string.Empty;
                    TaskRunItem runEntity = XmlHelper.LoadFromXml<TaskRunItem>(Program.GetConfigPath(filePath), ref errMsg);
                    if (!string.IsNullOrEmpty(errMsg))
                    {
                        WinFormLib.Core.MessageBoxHelper.ShowError(errMsg);
                        return;
                    }
                    _runItemList.Add(runEntity);
                }
            }
        }
        private void BindRunTask(EnumGloabParas.EnumThreadState gaterherState)
        {
            string tagName = "taskRuning";
            TaskRunItemList resultList = new TaskRunItemList();
            if (gaterherState == EnumGloabParas.EnumThreadState.SpiderCompleted)
            {
                tagName = "taskCompleted";
                List<TaskRunItem> _resultList = _runItemList.Where(q => q.GaterherState == EnumGloabParas.EnumThreadState.Completed).ToList();
                resultList.AddRange(_resultList);
            }
            else
            {
                List<TaskRunItem> _resultList = _runItemList.Where(q => q.GaterherState != EnumGloabParas.EnumThreadState.Completed).ToList();
                resultList.AddRange(_resultList);
            }
            foreach (var item in resultList)
            {
                if (item.GaterherState != EnumGloabParas.EnumThreadState.Completed)
                {
                    cGatherTaskTabPageManage tabPageManager = tabPageManageList.Where(q => q.Name == item.TaskTempName.Value).FirstOrDefault();
                    if (tabPageManager == null)
                    {
                        tabPageManager = new cGatherTaskTabPageManage(tabTaskRunLog, item);
                        tabPageManager.e_ExportData += new OnExportData(tabPageManager_e_ExportData);
                        tabPageManager.e_GatherNotityCompleted += new OnGatherPublishCompleted(tabPageManager_e_GatherNotityCompleted);
                        tabPageManager.e_OnGatherRunPlanItemCompleted += new OnGatherPublishCompleted(tabPageManager_e_OnGatherRunPlanItemCompleted);
                        tabPageManager.e_OnGatherPublishCompleted += new OnGatherPublishCompleted(tabPageManager_e_OnGatherPublishCompleted);
                        tabPageManageList.Add(tabPageManager);
                    }
                }
            }
            if (this.dgvList.IsHandleCreated)
            {
                DataTable dataTable = TaskRunItem.GetRunDataTable(resultList);
                this.dgvList.RemoveBind();
                TaskRunItem.InitRunDataGrid(this.dgvList, tagName);
                this.dgvList.ReBind(dataTable, false);
                this.dgvList.ClearSelection();
            }
        }
        private bool ReadTaskPlan()
        {
            string errMsg = string.Empty;
            TaskBasePlanList planList = XmlHelper.LoadFromXml<TaskBasePlanList>(Program.GetConfigPath("plan.xml"), ref errMsg);
            if (!string.IsNullOrEmpty(errMsg))
            {
                MessageBoxHelper.ShowError(errMsg);
                return false;
            }
            _taskPlanList = new List<TaskPlan>();
            foreach (var item in planList)
            {
                TaskPlan entity = XmlHelper.LoadFromXml<TaskPlan>(Program.GetConfigPath(@"plan\" + item.PlanName.Value + ".xml"), ref errMsg);
                _taskPlanList.Add(entity);
            }
            return true;
        }
        private void BindTaskPlan()
        {
            if (_taskPlanList == null)
            {
                if (!ReadTaskPlan()) { return; }
            }
            this.dgvList.RemoveBind();
            if (this.dgvList.Tag.ToString() != "taskPlanList")
            {
                TaskPlan.InitDataGrid(this.dgvList, "taskPlanList");
            }
            this.dgvList.ReBind(TaskPlan.GetDataTable(_taskPlanList), false);
            this.dgvList.ClearSelection();
        }

        #region tabPageManager
        private void tabPageManager_e_GatherNotityCompleted(cGatherPublishCompletedEventArgs e)
        {

            //保存任务到XML中
            TaskRunItem runItem = _runItemList.Where(q => q.TaskTempName.Value == e.TaskEntity.TaskTempName.Value).FirstOrDefault();
            if (runItem == null)
            {
                return;
            }
            runItem.GatherFileItemCompleteList = e.TaskEntity.GatherFileItemCompleteList;
            runItem.GatherFileItemTempList = e.TaskEntity.GatherFileItemTempList;
            runItem.GatherUrlItemCompleteList = e.TaskEntity.GatherUrlItemCompleteList;
            runItem.GatherUrlItemTempList = e.TaskEntity.GatherUrlItemTempList;
            runItem.ErrorCount = e.TaskEntity.ErrorCount;
            runItem.TotalCount = e.TaskEntity.TotalCount;
            runItem.TrueCount = e.TaskEntity.TrueCount;
            runItem.GaterherState = EnumGloabParas.EnumThreadState.Completed;
            string errMsg = string.Empty;
            XmlHelper.Save2File<TaskRunItem>(runItem, Program.GetConfigPath(@"run\" + runItem.TaskTempName.Value + ".xml"), ref errMsg);
            if (!string.IsNullOrEmpty(errMsg))
            {
                WinFormLib.Core.MessageBoxHelper.ShowError(errMsg);
                return;
            }
            if (!string.IsNullOrEmpty(e.TaskEntity.ParentTaskTempName.Value))
            {
                cGatherTaskTabPageManage tabPageManager0 = tabPageManageList.Where(q => q.Name == e.TaskEntity.ParentTaskTempName.Value).FirstOrDefault();
                if (tabPageManager0 != null)
                {
                    tabPageManager0.OnChildTaskCompleted();
                }
            }
            //移除TAB
            string taskTempName = e.TaskEntity.TaskTempName.Value;
            cGatherTaskTabPageManage tabPageManager = tabPageManageList.Where(q => q.Name == taskTempName).FirstOrDefault();
            if (tabPageManager != null)
            {
                tabTaskRunLog.Invoke(new MethodInvoker(delegate()
                {
                    if (this.tabTaskRunLog.TabPages.ContainsKey(taskTempName))
                    {
                        this.tabTaskRunLog.TabPages.RemoveByKey(taskTempName);
                    }
                    tabPageManager.e_ExportData -= new OnExportData(tabPageManager_e_ExportData);
                    tabPageManager.e_GatherNotityCompleted -= new OnGatherPublishCompleted(tabPageManager_e_GatherNotityCompleted);
                    tabPageManager.e_OnGatherRunPlanItemCompleted -= new OnGatherPublishCompleted(tabPageManager_e_OnGatherRunPlanItemCompleted);
                    tabPageManager.e_OnGatherPublishCompleted -= new OnGatherPublishCompleted(tabPageManager_e_OnGatherPublishCompleted);
                    tabPageManageList.Remove(tabPageManager);
                    tabPageManager = null;
                    this.LoadRunItemList(true);
                    if (this.CurrentState == EnumFormRunState.Run)
                    {
                        TreeNode node = tvMain.SelectedNode;
                        if (node != null && node.FullPath == @"任务运行区\已经完成")
                        {
                            BindRunTask(EnumGloabParas.EnumThreadState.Completed);
                        }
                        else
                        {
                            BindRunTask(EnumGloabParas.EnumThreadState.Normal);
                        }

                    }
                }));
            }
            else
            {
                MessageBoxHelper.ShowError("tabPageManager is null");
            }
        }
        private void tabPageManager_e_OnGatherRunPlanItemCompleted(cGatherPublishCompletedEventArgs e)
        {
            RunPlanItem(e.TaskEntity.TaskPlanItemList, e.TaskEntity.TaskTempName.Value);
        }
        private void tabPageManager_e_OnGatherPublishCompleted(cGatherPublishCompletedEventArgs e)
        {
            txtSysLog.AppendText("正在发布数据中....." + e.TaskEntity.TaskTempName.Value + "\n");
            frmPublish frmPub = new frmPublish(e.TaskEntity, e.DataSource);
            frmPub.ShowDialog();
            if (e.TaskEntity.TriggerType == EnumGloabParas.EnumTriggerType.Publish)
            {
                tabPageManager_e_OnGatherRunPlanItemCompleted(e);
            }
            else
            {
                tabPageManager_e_GatherNotityCompleted(e);
            }
        }
        private void tabPageManager_e_ExportData(int dataType, DataTable dataTable)
        {
            if (exportFile.ThreadState == EnumGloabParas.EnumThreadState.Run)
            {
                MessageBoxHelper.ShowError("file is exporting");
                return;
            }
            if (dataTable == null)
            {
                return;
            }
            if (dataType == 1)
            {
                saveFileDialog1.Filter = "txt|*.txt";
                saveFileDialog1.Title = "导出为文本文件";
            }
            else if (dataType == 2)
            {
                saveFileDialog1.Filter = "97-2003 Excel工作簿|*.xls";
                saveFileDialog1.Title = "导出为Excel文件";
            }
            DialogResult result = saveFileDialog1.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                exportFile.Abort();
                exportFile.m_DataTable = dataTable;
                exportFile.ConnectionString = saveFileDialog1.FileName;

                if (dataType == 1)
                {
                    exportFile.ConnectionType = EnumGloabParas.EnumConnectionType.ExportTxt;
                }
                else
                {
                    exportFile.ConnectionType = EnumGloabParas.EnumConnectionType.ExportExcel;
                }
                exportFile.Start();
            }
        }

        #endregion

        private void RunPlanItem(List<TaskPlanItem> taskPlanItemList, string parentTaskTempName)
        {
            foreach (var item in taskPlanItemList)
            {
                if (item.TaskType == EnumGloabParas.EnumPlanTaskType.Task)
                {
                    TaskRunItem runEntity = null;
                    bool result = StartTask(item.CategroyName.Value, item.TaskName.Value, false, false, parentTaskTempName, ref runEntity);
                    if (result)
                    {
                        if ((this.CurrentState == EnumFormRunState.Run) && dgvList.Tag.ToString() == "taskRuning")
                        {
                            BindRunTask(EnumGloabParas.EnumThreadState.Run);
                        }
                        StartTaskRun(runEntity);
                        this.resetRowIndex = true;
                        txtSysLog.AppendText("运行子任务中\n");
                    }
                }
            }
        }
        private DataGridViewRow GetRunDataGridRow(string taskTempName)
        {
            DataGridViewRow dataGridRow = null;
            if (dgvList.Tag.ToString() == "taskRuning")
            {
                foreach (DataGridViewRow item in dgvList.Rows)
                {
                    object value = dgvList["TaskTempName", item.Index].Value;
                    if (value != null && value.ToString() == taskTempName)
                    {
                        dataGridRow = item;
                        break;
                    }
                }
            }
            return dataGridRow;
        }

        private void BindTask(TreeNode node)
        {
            if (_taskList == null)
            {
                this.LoadTaskList();
            }
            List<TaskItem> itemList = _taskList.Where(q => q.CategroyName.Value == node.Text).ToList();
            this.dgvList.RemoveBind();
            TaskItem.InitDataGrid(this.dgvList, "taskList");
            this.dgvList.ReBind(TaskItem.GetDataTable(itemList), false);
            this.dgvList.ClearSelection();
        }
        private bool LoadTaskList()
        {
            _taskList = new TaskList();
            foreach (var categroy in _taskCategroyList)
            {
                string filePath = Program.GetConfigPath(@"categroy\" + categroy.CategroyName.Value);
                if (!System.IO.Directory.Exists(filePath))
                {
                    System.IO.Directory.CreateDirectory(Program.GetConfigPath(filePath));
                }
                List<TaskItemBase> taskItemList = categroy.TaskItemList;

                foreach (var item in taskItemList)
                {
                    string taskFilePath = @"categroy\" + categroy.CategroyName.Value + @"\" + item.TaskName.Value + ".xml";
                    string errMsg = string.Empty;
                    TaskItem entity = XmlHelper.LoadFromXml<TaskItem>(Program.GetConfigPath(taskFilePath), ref errMsg);
                    if (!string.IsNullOrEmpty(errMsg))
                    {
                        WinFormLib.Core.MessageBoxHelper.ShowError(errMsg);
                        return false;
                    }
                    _taskList.Add(entity);
                }
            }
            return true;
        }
        List<cGatherTaskTabPageManage> tabPageManageList = new List<cGatherTaskTabPageManage>();
        private void dgvList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            cnEditTask_Click(sender, e);
        }
        private void cnNewCategory_Click(object sender, EventArgs e)
        {
            string name = "newCategory" + _taskCategroyList.Count;
            TreeNode newNode = tvMain.Nodes["tvTasks"].Nodes.Add("newCategory" + _taskCategroyList.Count);
            newNode.Name = "category_" + name;
            newNode.ImageIndex = 15;
            newNode.SelectedImageIndex = 16;
            newNode.Tag = new List<TaskItemBase>();
            tvMain.Nodes["tvTasks"].Expand();
            tvMain.LabelEdit = true;
            newNode.BeginEdit();
        }
        private void cnDelCategory_Click(object sender, EventArgs e)
        {
            if (this.CurrentState == EnumFormRunState.Task)
            {
                if (tvMain.SelectedNode.FullPath.StartsWith("任务区\\"))
                {
                    if (MessageBoxHelper.ShowQuestion("确定删除当前分类吗?删除分类下面的任务也会删除!") != System.Windows.Forms.DialogResult.OK)
                    {
                        return;
                    }
                    string categoryName = tvMain.SelectedNode.Text;
                    TaskCategroy categoryEntity = _taskCategroyList.Where(q => q.CategroyName.Value == categoryName).FirstOrDefault();
                    if (categoryEntity != null)
                    {
                        foreach (var item in categoryEntity.TaskItemList)
                        {
                            string taskFilePath = @"categroy\" + categoryName + @"\" + item.TaskName.Value + ".xml";
                            TaskItem taskEntity = _taskList.Where(q => q.TaskName.Value == item.TaskName.Value).FirstOrDefault();
                            if (taskEntity != null)
                            {
                                if (System.IO.File.Exists(Program.GetConfigPath(taskFilePath)))
                                {
                                    System.IO.File.Delete(Program.GetConfigPath(taskFilePath));
                                }
                                _taskList.Remove(taskEntity);
                            }
                        }
                        _taskCategroyList.Remove(categoryEntity);
                        string errMsg = string.Empty;
                        XmlHelper.Save2File<TaskCategroyList>(_taskCategroyList, Program.GetConfigPath("category.xml"), ref errMsg);
                        if (!string.IsNullOrEmpty(errMsg))
                        {
                            MessageBoxHelper.ShowError(errMsg);
                            return;
                        }
                        tvMain.SelectedNode.Remove();
                        if (System.IO.Directory.Exists(Program.GetConfigPath(@"categroy\" + categoryName)))
                        {
                            System.IO.Directory.Delete(Program.GetConfigPath(@"categroy\" + categoryName));
                        }
                        MessageBoxHelper.Show("删除分类信息成功!");
                    }
                }
            }

        }
        private void dgvList_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
        {
            if (this.CurrentState == EnumFormRunState.Task)
            {
                this.toolEditTask.Enabled = true;
            }
            else if (this.CurrentState == EnumFormRunState.Run)
            {
                if (dgvList.SelectedRows.Count > 0)
                {
                    string taskTempName = TryParse.ToString(this.dgvList[dgvList.ColumnCount - 1, dgvList.SelectedRows[0].Index].Value);
                    if (this.tabTaskRunLog.TabPages.ContainsKey(taskTempName))
                    {
                        this.tabTaskRunLog.SelectTab(taskTempName);
                    }
                }
            }
        }
        #region 右键操作
        private bool StartTask(string categoryName, string taskName, bool alerted, bool checkQueue, string parentTaskTempName, ref TaskRunItem runEntity)
        {
            string errMsg = string.Empty;
            string taskFilePath = @"categroy\" + categoryName + @"\" + taskName + ".xml";
            TaskItem taskEntity = XmlHelper.LoadFromXml<TaskItem>(Program.GetConfigPath(taskFilePath), ref errMsg);
            if (!string.IsNullOrEmpty(errMsg))
            {
                if (alerted)
                {
                    WinFormLib.Core.MessageBoxHelper.ShowError(errMsg);
                }
                return false;
            }
            if (taskEntity != null && taskEntity.TaskID > 0)
            {
                string taskTempName = taskName + DateTime.Now.Ticks.ToString();
                TaskRunTickItem entity = new TaskRunTickItem()
                {
                    TaskTempName = CDataItem.Instance(taskTempName),
                    TaskName = CDataItem.Instance(taskName),
                    CategroyName = CDataItem.Instance(categoryName),
                    TaskID = taskEntity.TaskID,
                };
                if (checkQueue)
                {
                    if (_runItemList.Where(q => q.TaskName.Value == entity.TaskName.Value && q.GaterherState != EnumGloabParas.EnumThreadState.SpiderCompleted).FirstOrDefault() != null)
                    {
                        if (MessageBoxHelper.ShowQuestion("当前任务已经在队列中,确定新的任务吗?") != System.Windows.Forms.DialogResult.OK)
                        {
                            return false;
                        }
                    }
                }
                _runTickItemList.Add(entity);
                runEntity = StaticConst.CloneObject<TaskItem, TaskRunItem>(taskEntity);
                runEntity.ParentTaskTempName = CDataItem.Instance(parentTaskTempName);
                runEntity.Load(taskTempName);
                if (!System.IO.Directory.Exists(Program.GetConfigPath("run")))
                {
                    System.IO.Directory.CreateDirectory(Program.GetConfigPath("run"));
                }
                XmlHelper.Save2File<TaskRunItem>(runEntity, Program.GetConfigPath(@"run\" + taskTempName + ".xml"), ref errMsg);
                if (!string.IsNullOrEmpty(errMsg))
                {
                    if (alerted)
                    {
                        WinFormLib.Core.MessageBoxHelper.ShowError(errMsg);
                    }
                    return false;
                }
                _runItemList.Add(runEntity);
                XmlHelper.Save2File<TaskRunTickItemList>(_runTickItemList, Program.GetConfigPath("run.xml"), ref errMsg);
                if (!string.IsNullOrEmpty(errMsg))
                {
                    if (alerted)
                    {
                        WinFormLib.Core.MessageBoxHelper.ShowError(errMsg);
                    }
                    return false;
                }
            }
            
            return true;
        }
        private void cnStartTask_Click(object sender, EventArgs e)
        {
            if (this.CurrentState == EnumFormRunState.Task)
            {
                if (dgvList.SelectedRows.Count > 0)
                {
                    #region Run

                    string categoryName = TryParse.ToString(this.dgvList["CategroyName", dgvList.SelectedRows[0].Index].Value);
                    string taskName = TryParse.ToString(this.dgvList["TaskName", dgvList.SelectedRows[0].Index].Value);
                    TaskRunItem runEntity = null;
                    if (StartTask(categoryName, taskName, true, true, "", ref runEntity))
                    {
                        tvMain.SelectedNode = tvMain.Nodes["tvTaskRun"].Nodes["tvOnlineRun"];
                        if (tvMain.SelectedNode != null)
                        {
                            tvMain_AfterSelect(this, new TreeViewEventArgs(tvMain.SelectedNode, TreeViewAction.ByMouse));
                        }
                    }
                    #endregion
                }
            }
            else if (this.CurrentState == EnumFormRunState.Run)
            {
                if (dgvList.SelectedRows.Count > 0)
                {
                    string taskTempName = TryParse.ToString(this.dgvList[dgvList.ColumnCount - 1, dgvList.SelectedRows[0].Index].Value);
                    TaskRunItem runEntity = _runItemList.Where(q => q.TaskTempName.Value == taskTempName).FirstOrDefault();
                    if (runEntity != null)
                    {
                        StartTaskRun(runEntity);
                    }
                }
            }
        }

        private void StartTaskRun(TaskRunItem runEntity)
        {
            cGatherTaskTabPageManage tabPageManager = tabPageManageList.Where(q => q.Name == runEntity.TaskTempName.Value).FirstOrDefault();
            if (tabPageManager != null)
            {
                tabPageManager.Start();
                m_RunStartFlag = true;
            }
            else
            {
                tabPageManager = new cGatherTaskTabPageManage(tabTaskRunLog, runEntity);
                tabPageManager.e_ExportData += new OnExportData(tabPageManager_e_ExportData);
                tabPageManager.e_GatherNotityCompleted += new OnGatherPublishCompleted(tabPageManager_e_GatherNotityCompleted);
                tabPageManager.e_OnGatherRunPlanItemCompleted += new OnGatherPublishCompleted(tabPageManager_e_OnGatherRunPlanItemCompleted);
                tabPageManager.e_OnGatherPublishCompleted += new OnGatherPublishCompleted(tabPageManager_e_OnGatherPublishCompleted);
                tabPageManager.Start();
                m_RunStartFlag = true;
                tabPageManageList.Add(tabPageManager);
            }
        }

        private void cnReStartTask_Click(object sender, EventArgs e)
        {
            if (this.CurrentState == EnumFormRunState.Task)
            {

            }
            else if (this.CurrentState == EnumFormRunState.Run)
            {
                if (dgvList.SelectedRows.Count > 0)
                {
                    string taskTempName = TryParse.ToString(this.dgvList[dgvList.ColumnCount - 1, dgvList.SelectedRows[0].Index].Value);
                    string errMsg = string.Empty;
                    string taskFilePath = Program.GetConfigPath(@"run\" + taskTempName + ".xml");
                    TaskRunItem runEntity = XmlHelper.LoadFromXml<TaskRunItem>(taskFilePath, ref errMsg);
                    if (!string.IsNullOrEmpty(errMsg))
                    {
                        return;
                    }
                    if (_runItemList.Where(q => q.TaskName.Value == runEntity.TaskName.Value
                        && (q.GaterherState != EnumGloabParas.EnumThreadState.SpiderCompleted && q.GaterherState != EnumGloabParas.EnumThreadState.Suspended)).FirstOrDefault() != null)
                    {
                        MessageBoxHelper.ShowWarning("当前任务已经在队列中...");
                        return;
                    }
                    bool resultFlag = false;
                    List<cGatherUrlItem> resultList = runEntity.GatherUrlItemCompleteList.Where(q => q.GaterherFlag != EnumGloabParas.EnumUrlGaterherState.Completed).ToList();
                    foreach (var item in resultList)
                    {
                        resultFlag = true;
                        if (item.GaterherFlag == EnumGloabParas.EnumUrlGaterherState.FirstError)
                        {

                            item.GaterherFlag = EnumGloabParas.EnumUrlGaterherState.First;
                        }
                        else
                        {
                            item.GaterherFlag = EnumGloabParas.EnumUrlGaterherState.Run;
                        }
                        runEntity.GatherUrlItemTempList.Add(item);

                    }

                    if (resultFlag)
                    {
                        runEntity.GatherUrlItemCompleteList.RemoveAll(q => q.GaterherFlag != EnumGloabParas.EnumUrlGaterherState.Completed);
                        runEntity.GaterherState = EnumGloabParas.EnumThreadState.Suspended;
                        runEntity.ErrorCount = 0;
                        runEntity.TotalCount =
                              runEntity.GatherFileItemCompleteList.Count
                            + runEntity.GatherFileItemTempList.Count
                            + runEntity.GatherUrlItemCompleteList.Count
                            + runEntity.GatherUrlItemTempList.Count;
                        runEntity.TrueCount = runEntity.GatherFileItemCompleteList.Count
                            + runEntity.GatherUrlItemCompleteList.Count;

                        XmlHelper.Save2File(runEntity, taskFilePath, ref errMsg);
                        if (!string.IsNullOrEmpty(errMsg))
                        {
                            return;
                        }
                        cGatherTaskTabPageManage tabPageManager = tabPageManageList.Where(q => q.Name == taskTempName).FirstOrDefault();
                        if (tabPageManager != null)
                        {
                            tabPageManager.Reset(runEntity);
                        }

                    }
                    this.LoadRunItemList(true);
                    TreeNode node = tvMain.Nodes["tvTaskRun"].Nodes["tvOnlineRun"];
                    if (tvMain.SelectedNode != node)
                    {
                        tvMain.SelectedNode = node;
                        this.BindRunTask(EnumGloabParas.EnumThreadState.Normal);
                    }
                }
            }
        }

        private void cnPauseTask_Click(object sender, EventArgs e)
        {
            if (this.CurrentState == EnumFormRunState.Task)
            {

            }
            else if (this.CurrentState == EnumFormRunState.Run)
            {
                if (dgvList.SelectedRows.Count > 0)
                {
                    foreach (DataGridViewRow item in dgvList.SelectedRows)
                    {
                        string taskTempName = TryParse.ToString(this.dgvList["TaskTempName", item.Index].Value);
                        cGatherTaskTabPageManage tabPageManager = tabPageManageList.Where(q => q.Name == taskTempName).FirstOrDefault();
                        if (tabPageManager != null)
                        {
                            tabPageManager.Stop();
                        }
                    }
                }
            }
        }
        private void cnResetTask_Click(object sender, EventArgs e)
        {
            if (this.CurrentState == EnumFormRunState.Run)
            {
                if (MessageBoxHelper.ShowQuestion("确定重置当前数据吗？重置会清除当前已采集的数据！") != DialogResult.OK)
                {
                    return;
                }
                if (dgvList.SelectedRows.Count > 0)
                {
                    string taskTempName = TryParse.ToString(this.dgvList[dgvList.ColumnCount - 1, dgvList.SelectedRows[0].Index].Value);
                    if (dgvList.Tag.ToString() == "taskRuning")
                    {
                        cGatherTaskTabPageManage tabPageManager = tabPageManageList.Where(q => q.Name == taskTempName).FirstOrDefault();
                        if (tabPageManager != null)
                        {
                            tabPageManager.Reset();
                            string taskFilePath = @"run\" + taskTempName + ".xml";
                            string errMsg = string.Empty;
                            TaskRunItem taskEntity = XmlHelper.LoadFromXml<TaskRunItem>(Program.GetConfigPath(taskFilePath), ref errMsg);
                            if (!string.IsNullOrEmpty(errMsg))
                            {
                                WinFormLib.Core.MessageBoxHelper.ShowError(errMsg);
                                return;
                            }
                            this.LoadRunItemList(true);
                            TreeNode node = tvMain.Nodes["tvTaskRun"].Nodes["tvOnlineRun"];
                            if (tvMain.SelectedNode != node)
                            {
                                tvMain.SelectedNode = node;
                                this.BindRunTask(EnumGloabParas.EnumThreadState.Normal);
                            }
                            else
                            {
                                dgvList.SelectedRows[0].Cells["ErrorCount"].Value = taskEntity.ErrorCount;
                                dgvList.SelectedRows[0].Cells["TrueCount"].Value = taskEntity.TrueCount;
                                dgvList.SelectedRows[0].Cells["TotalCount"].Value = taskEntity.TotalCount;
                                dgvList.SelectedRows[0].Cells["ProgessValue"].Value = TaskRunItem.GetProgessValue(taskEntity.TrueCount, taskEntity.ErrorCount, taskEntity.TotalCount);
                                dgvList.SelectedRows[0].Cells["GaterherState"].Value = taskEntity.GaterherState.ToString();
                            }
                        }
                    }
                    else if (dgvList.Tag.ToString() == "taskCompleted")
                    {
                        string taskFilePath = @"run\" + taskTempName + ".xml";
                        string errMsg = string.Empty;
                        TaskRunItem taskEntity = XmlHelper.LoadFromXml<TaskRunItem>(Program.GetConfigPath(taskFilePath), ref errMsg);
                        if (!string.IsNullOrEmpty(errMsg))
                        {
                            WinFormLib.Core.MessageBoxHelper.ShowError(errMsg);
                            return;
                        }
                        taskEntity.GaterherState = EnumGloabParas.EnumThreadState.Normal;
                        taskEntity.GatherUrlItemCompleteList = new List<cGatherUrlItem>();
                        taskEntity.GatherUrlItemTempList = new List<cGatherUrlItem>();
                        taskEntity.TrueCount = taskEntity.TotalCount = taskEntity.ErrorCount = 0;
                        XmlHelper.Save2File(taskEntity, Program.GetConfigPath(taskFilePath), ref errMsg);
                        if (!string.IsNullOrEmpty(errMsg))
                        {
                            WinFormLib.Core.MessageBoxHelper.ShowError(errMsg);
                            return;
                        }
                        this.LoadRunItemList(true);
                        TreeNode node = tvMain.Nodes["tvTaskRun"].Nodes["tvOnlineRun"];
                        if (tvMain.SelectedNode != node)
                        {

                            tvMain.SelectedNode = node;
                            this.BindRunTask(EnumGloabParas.EnumThreadState.Normal);
                        }
                    }

                }
            }
        }

        private void cnTask_Opening(object sender, CancelEventArgs e)
        {
            cnDeleteTask.Enabled = false;
            cnEditTask.Enabled = false;
            cnStartTask.Enabled = false;
            cnPauseTask.Enabled = false;
            cnResetTask.Enabled = false;
            cnCopyTask.Enabled = false;
            cnViewTask.Enabled = false;
            cnViewRunTask.Enabled = false;
            cnReStartTask.Enabled = false;
            cnPublishTask.Enabled = false;
            if (this.CurrentState == EnumFormRunState.Task)
            {
                if (dgvList.SelectedRows.Count > 0)
                {
                    cnStartTask.Enabled = true;
                    cnCopyTask.Enabled = true;
                    cnEditTask.Enabled = true;
                    cnDeleteTask.Enabled = true;
                }
            }
            else if (this.CurrentState == EnumFormRunState.Run)
            {
                if (dgvList.SelectedRows.Count > 0)
                {
                    if (dgvList.Tag.ToString() == "taskRuning")
                    {
                        DataGridViewRow item = dgvList.SelectedRows[0];
                        string taskTempName = TryParse.ToString(this.dgvList[dgvList.ColumnCount - 1, item.Index].Value);
                        EnumGloabParas.EnumThreadState GaterherState = EnumGloabParas.EnumThreadState.Normal;

                        cGatherTaskTabPageManage tabPageManager = tabPageManageList.Where(q => q.Name == taskTempName).FirstOrDefault();
                        if (tabPageManager != null)
                        {
                            GaterherState = tabPageManager.GetGaterherState();
                        }
                        if (GaterherState == EnumGloabParas.EnumThreadState.Normal)
                        {
                            cnStartTask.Enabled = true;
                            cnDeleteTask.Enabled = true;
                        }
                        else if (GaterherState == EnumGloabParas.EnumThreadState.Run)
                        {
                            cnPauseTask.Enabled = true;
                        }
                        else if (GaterherState == EnumGloabParas.EnumThreadState.Suspended)
                        {
                            cnStartTask.Enabled = true;
                            cnResetTask.Enabled = true;
                            cnViewTask.Enabled = true;
                            cnReStartTask.Enabled = true;
                            cnViewRunTask.Enabled = true;
                        }
                        else if (GaterherState == EnumGloabParas.EnumThreadState.SpiderCompleted)
                        {
                            cnResetTask.Enabled = true;
                            cnViewTask.Enabled = true;
                        }
                    }
                    if (dgvList.Tag.ToString() == "taskCompleted")
                    {
                        cnResetTask.Enabled = true;
                        cnDeleteTask.Enabled = true;
                        cnViewTask.Enabled = true;
                        cnViewRunTask.Enabled = true;
                        cnReStartTask.Enabled = true;
                        cnPublishTask.Enabled = true;
                    }

                }
            }
        }
        private void cnEditTask_Click(object sender, EventArgs e)
        {
            if (this.CurrentState == EnumFormRunState.Task)
            {
                if (this.dgvList.SelectedRows.Count > 0)
                {
                    string categoryName = TryParse.ToString(this.dgvList[1, this.dgvList.CurrentRow.Index].Value);
                    string taskName = TryParse.ToString(this.dgvList[2, this.dgvList.CurrentRow.Index].Value);
                    frmTask taskForm = new frmTask(categoryName, taskName);
                    taskForm.ShowDialog(this);
                    this.LoadTaskCategroyList(true);

                    TreeNode newNode = tvMain.Nodes["tvTasks"].Nodes["category_" + categoryName];
                    newNode.Tag = _taskCategroyList[categoryName].TaskItemList;
                    TreeNode node = tvMain.SelectedNode;
                    ReloadDataGridView(node);
                }
            }
            else if (this.CurrentState == EnumFormRunState.Run)
            {
                if (dgvList.Tag.ToString() == "taskRuning" || dgvList.Tag.ToString() == "taskCompleted")
                {
                    if (MessageBoxHelper.ShowQuestion("当前任务在运行中,不能修改任务,确定打开吗?") == System.Windows.Forms.DialogResult.OK)
                    {
                        string taskTempName = TryParse.ToString(this.dgvList[dgvList.ColumnCount - 1, this.dgvList.CurrentRow.Index].Value);
                        TaskRunItem entity = _runItemList.Where(q => q.TaskTempName.Value == taskTempName).FirstOrDefault();
                        if (entity != null)
                        {
                            frmTask taskForm = new frmTask(entity.CategroyName.Value, entity.TaskName.Value, entity);
                            taskForm.ShowDialog(this);
                        }
                    }
                }

            }
            else if (this.CurrentState == EnumFormRunState.Plan)
            {
                if (dgvList.Tag.ToString() == "taskPlanList")
                {
                    string taskPlanName = TryParse.ToString(this.dgvList[1, this.dgvList.CurrentRow.Index].Value);
                    frmPlan frmPlan0 = new frmPlan(taskPlanName);
                    frmPlan0.ShowDialog(this);
                    if (!ReadTaskPlan())
                    {
                        return;
                    }
                    BindTaskPlan();
                }
            }
        }
        private void cnDeleteTask_Click(object sender, EventArgs e)
        {

            if (this.CurrentState == EnumFormRunState.Task)
            {
                string categoryName = TryParse.ToString(this.dgvList[1, this.dgvList.CurrentRow.Index].Value);
                string taskName = TryParse.ToString(this.dgvList[2, this.dgvList.CurrentRow.Index].Value);
                string taskFilePath = @"categroy\" + categoryName + @"\" + taskName + ".xml";
                string errMsg = string.Empty;
                if (MessageBoxHelper.ShowQuestion("确定删除任务" + taskName + "吗?删除任务不可恢复!") != System.Windows.Forms.DialogResult.OK)
                {
                    return;
                }
                TaskItem entity = XmlHelper.LoadFromXml<TaskItem>(Program.GetConfigPath(taskFilePath), ref errMsg);
                if (!string.IsNullOrEmpty(errMsg))
                {
                    WinFormLib.Core.MessageBoxHelper.ShowError(errMsg);
                    return;
                }
                TaskCategroy categoryEntity = _taskCategroyList.Where(q => q.CategroyName.Value == categoryName).FirstOrDefault();
                TaskItemBase taskBase = categoryEntity.TaskItemList.Where(q => q.TaskID == entity.TaskID).FirstOrDefault();
                if (taskBase != null)
                {
                    categoryEntity.TaskItemList.Remove(taskBase);
                    XmlHelper.Save2File<TaskCategroyList>(_taskCategroyList, Program.GetConfigPath("category.xml"), ref errMsg);
                    if (!string.IsNullOrEmpty(errMsg))
                    {
                        WinFormLib.Core.MessageBoxHelper.ShowError(errMsg);
                        return;
                    }
                    XmlHelper.DeleteFile(Program.GetConfigPath(taskFilePath));
                    this.LoadTaskCategroyList(true);
                    this.InitTaskList();
                    this.LoadTaskList();
                    tvMain.SelectedNode = tvMain.Nodes["tvTasks"].Nodes["category_" + categoryName];
                    if (tvMain.SelectedNode != null)
                    {
                        ReloadDataGridView(tvMain.SelectedNode);
                    }
                }
            }
            else if (this.CurrentState == EnumFormRunState.Run)
            {
                if (dgvList.SelectedRows.Count > 0)
                {
                    string taskTempName = TryParse.ToString(this.dgvList["TaskTempName", dgvList.SelectedRows[0].Index].Value);
                    if (MessageBoxHelper.ShowQuestion("确定删除任务" + taskTempName + "吗?删除任务不可恢复!") != System.Windows.Forms.DialogResult.OK)
                    {
                        return;
                    }
                    if (dgvList.Tag.ToString() == "taskRuning")
                    {
                        cGatherTaskTabPageManage tabPageManager = tabPageManageList.Where(q => q.Name == taskTempName).FirstOrDefault();
                        if (tabPageManager != null)
                        {
                            tabPageManager.Delete();
                            tabPageManager.e_ExportData -= new OnExportData(tabPageManager_e_ExportData);
                            tabPageManager.e_GatherNotityCompleted -= new OnGatherPublishCompleted(tabPageManager_e_GatherNotityCompleted);
                            tabPageManager.e_OnGatherPublishCompleted -= new OnGatherPublishCompleted(tabPageManager_e_OnGatherPublishCompleted);
                            tabPageManageList.Remove(tabPageManager);
                            tabPageManager = null;
                            this.LoadRunItemList(true);
                            dgvList.Rows.Remove(dgvList.SelectedRows[0]);
                        }
                    }
                    else if (dgvList.Tag.ToString() == "taskCompleted")
                    {
                        TaskRunTickItem tickItem = _runTickItemList.Where(q => q.TaskTempName.Value == taskTempName).FirstOrDefault();
                        if (tickItem != null)
                        {
                            string errMsg = string.Empty;
                            _runTickItemList.Remove(tickItem);
                            XmlHelper.Save2File<TaskRunTickItemList>(_runTickItemList, Program.GetConfigPath("run.xml"), ref errMsg);
                            if (!string.IsNullOrEmpty(errMsg))
                            {
                                WinFormLib.Core.MessageBoxHelper.ShowError(errMsg);
                                return;
                            }
                            string taskFilePath = @"run\" + taskTempName + ".xml";
                            System.IO.File.Delete(Program.GetConfigPath(taskFilePath));
                            this.LoadRunItemList(true);
                            dgvList.Rows.Remove(dgvList.SelectedRows[0]);
                        }
                    }
                    dgvList.ClearSelection();
                }
            }
        }
        private void cnCopyTask_Click(object sender, EventArgs e)
        {
            if (this.CurrentState == EnumFormRunState.Task)
            {
                if (dgvList.SelectedRows.Count > 0)
                {
                    foreach (DataGridViewRow item in dgvList.SelectedRows)
                    {
                        string categoryName = TryParse.ToString(this.dgvList["CategroyName", item.Index].Value);
                        string taskName = TryParse.ToString(this.dgvList["TaskName", item.Index].Value);
                        string taskFilePath = "categroy\\" + categoryName + @"\" + taskName + ".xml";
                        string errMsg = string.Empty;
                        TaskItem taskEntity = XmlHelper.LoadFromXml<TaskItem>(Program.GetConfigPath(taskFilePath), ref errMsg);
                        if (!string.IsNullOrEmpty(errMsg))
                        {
                            WinFormLib.Core.MessageBoxHelper.ShowError(errMsg);
                            return;
                        }
                        TaskCategroy category = _taskCategroyList.Where(q => q.CategroyName.Value == categoryName).FirstOrDefault();
                        if (category != null)
                        {
                            long taskId = _taskCategroyList.Select(q => q.TaskItemList.Select(p => p.TaskID).Max()).FirstOrDefault();
                            while (true)
                            {
                                taskEntity.TaskName += "复制";
                                TaskItemBase task = category.TaskItemList.Where(q => q.TaskName.Value == taskEntity.TaskName.Value).FirstOrDefault();
                                if (task == null)
                                    break;
                            }
                            TaskItemBase newTask = new TaskItemBase()
                            {
                                TaskName = taskEntity.TaskName,
                                CategroyName = taskEntity.CategroyName,
                                TaskID = taskId + 1,
                                Version = StaticConst.Version,
                            };
                            taskEntity.TaskID = taskId + 1;
                            category.TaskItemList.Add(newTask);
                            XmlHelper.Save2File<TaskCategroyList>(_taskCategroyList, Program.GetConfigPath("category.xml"), ref errMsg);
                            if (!string.IsNullOrEmpty(errMsg))
                            {
                                WinFormLib.Core.MessageBoxHelper.ShowError(errMsg);
                                return;
                            }
                            string filePath = @"categroy\" + taskEntity.CategroyName + @"\" + taskEntity.TaskName + ".xml";
                            XmlHelper.Save2File<TaskItem>(taskEntity, Program.GetConfigPath(filePath), ref errMsg);
                            if (!string.IsNullOrEmpty(errMsg))
                            {
                                WinFormLib.Core.MessageBoxHelper.ShowError(errMsg);
                                return;
                            }
                        }
                    }
                }
            }
        }
        private void cnViewTask_Click(object sender, EventArgs e)
        {
            if (this.dgvList.SelectedRows.Count > 0)
            {
                if (this.CurrentState == EnumFormRunState.Run)
                {
                    DataGridViewRow item = dgvList.SelectedRows[0];
                    string taskTempName = TryParse.ToString(this.dgvList[dgvList.ColumnCount - 1, item.Index].Value);
                    if (dgvList.Tag.ToString() == "taskRuning")
                    {
                        TaskRunItem entity = _runItemList.Where(q => q.TaskTempName.Value == taskTempName).FirstOrDefault();
                        if (entity != null)
                        {
                            frmViewData viewForm = new frmViewData(entity.DownFilePath.Value, entity.TaskTempName.Value);
                            viewForm.ShowDialog();
                        }
                    }
                    else if (dgvList.Tag.ToString() == "taskCompleted")
                    {
                        TaskRunItem entity = _runItemList.Where(q => q.TaskTempName.Value == taskTempName).FirstOrDefault();
                        if (entity != null)
                        {
                            frmViewData viewForm = new frmViewData(entity.DownFilePath.Value, entity.TaskTempName.Value);
                            viewForm.ShowDialog();
                        }
                    }
                }
            }
        }
        private void cnViewRunTask_Click(object sender, EventArgs e)
        {
            if (this.dgvList.SelectedRows.Count > 0)
            {
                if (this.CurrentState == EnumFormRunState.Run)
                {
                    DataGridViewRow item = dgvList.SelectedRows[0];
                    string taskTempName = TryParse.ToString(this.dgvList[dgvList.ColumnCount - 1, item.Index].Value);
                    if (dgvList.Tag.ToString() == "taskRuning")
                    {
                        TaskRunItem entity = _runItemList.Where(q => q.TaskTempName.Value == taskTempName).FirstOrDefault();
                        if (entity != null)
                        {
                            frmViewData viewForm = new frmViewData(entity.TaskTempName.Value);
                            viewForm.ShowDialog();
                        }
                    }
                    else if (dgvList.Tag.ToString() == "taskCompleted")
                    {
                        TaskRunItem entity = _runItemList.Where(q => q.TaskTempName.Value == taskTempName).FirstOrDefault();
                        if (entity != null)
                        {
                            frmViewData viewForm = new frmViewData(entity.TaskTempName.Value);
                            viewForm.ShowDialog();
                        }
                    }
                }
            }
        }
        private void cnPublishTask_Click(object sender, EventArgs e)
        {
            if (this.dgvList.SelectedRows.Count > 0)
            {
                if (this.CurrentState == EnumFormRunState.Run)
                {
                    DataGridViewRow item = dgvList.SelectedRows[0];
                    string taskTempName = TryParse.ToString(this.dgvList[dgvList.ColumnCount - 1, item.Index].Value);
                    if (dgvList.Tag.ToString() == "taskCompleted")
                    {
                        TaskRunItem entity = _runItemList.Where(q => q.TaskTempName.Value == taskTempName).FirstOrDefault();
                        if (entity != null)
                        {
                            DataTable dataTable = new DataTable();
                            string filePath = entity.DownFilePath.Value;
                            if (!string.IsNullOrEmpty(filePath))
                            {
                                filePath += @"\" + taskTempName + ".xml";
                                if (System.IO.File.Exists(filePath))
                                {
                                    DataSet dataSet = new DataSet();
                                    dataSet.ReadXml(filePath);
                                    dataTable = dataSet.Tables[0];
                                    dataSet.Tables.Clear();
                                    frmPublish frmPub = new frmPublish(entity, dataTable);
                                    frmPub.ShowDialog();

                                }
                            }

                        }
                    }
                }
            }
        }
        #endregion

        #region Tools

        private void toolNewTask_Click(object sender, EventArgs e)
        {
            if (this.CurrentState == EnumFormRunState.Task)
            {
                string categoryName = this.tvMain.SelectedNode.Text;
                if (!string.IsNullOrEmpty(categoryName))
                {
                    if (tvMain.SelectedNode.FullPath.IndexOf("任务区\\") != -1)
                    {
                        frmTask taskForm = new frmTask(categoryName, "");
                        taskForm.ShowDialog(this);
                        this.LoadTaskCategroyList(true);
                        this.LoadTaskList();
                        this.InitTaskList();
                        tvMain.SelectedNode = tvMain.Nodes["tvTasks"].Nodes["category_" + categoryName];
                        if (tvMain.SelectedNode != null)
                        {
                            ReloadDataGridView(tvMain.SelectedNode);
                        }
                    }
                    else
                    {
                        MessageBoxHelper.ShowError("请先指定添加任务的分类！");
                    }
                }
            }
        }

        private void toolNewPlan_Click(object sender, EventArgs e)
        {
            frmPlan frmPlan1 = new frmPlan();
            frmPlan1.ShowDialog(this);
            if (!ReadTaskPlan()) { return; }
            if (this.CurrentState == EnumFormRunState.Plan)
            {
                if (tvMain.SelectedNode != null && tvMain.SelectedNode.FullPath == @"任务计划区\任务计划")
                {
                    BindTaskPlan();
                }
            }
        }

        private void toolNewCategory_Click(object sender, EventArgs e)
        {

        }

        private void toolEditTask_Click(object sender, EventArgs e)
        {
            cnEditTask_Click(sender, e);
        }
        private void toolExit_Click(object sender, EventArgs e)
        {
            MainExit();
        }

        private void MainExit()
        {
            if (MessageBoxHelper.ShowQuestion("确定退出当前程序吗?") == System.Windows.Forms.DialogResult.OK)
            {
                this.Close();
                Application.Exit();
            }
        }
        #endregion

        private void toolDictTool_Click(object sender, EventArgs e)
        {
            frmDict frmDict1 = new frmDict();
            frmDict1.ShowDialog();
        }

        private void toolExitNotify_Click(object sender, EventArgs e)
        {
            MainExit();
        }

        /// <summary>
        /// 定时刷新datagridview数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer1_Tick(object sender, EventArgs e)
        {
            lock (syncRoot)
            {
                if (dgvList.Tag.ToString() == "taskRuning" && m_RunStartFlag == true)
                {
                    if (resetRowIndex)
                    {
                        dicList = new Dictionary<int, cGatherTaskTabPageManage>();
                        foreach (var item in tabPageManageList)
                        {
                            if (item.TaskEntity != null)
                            {
                                DataGridViewRow row = GetRunDataGridRow(item.TaskEntity.TaskTempName.Value);
                                if (row != null)
                                    dicList.Add(row.Index, item);
                            }
                        }
                        resetRowIndex = false;
                    }
                    m_RunStartFlag = false;
                    foreach (var item in dicList)
                    {
                        if (dgvList.Rows.Count > item.Key && dgvList["TaskTempName", item.Key].Value.ToString() == item.Value.TaskEntity.TaskTempName.Value)
                        {
                            dgvList["TrueCount", item.Key].Value = item.Value.TaskEntity.TrueCount;
                            dgvList["ErrorCount", item.Key].Value = item.Value.TaskEntity.ErrorCount;
                            dgvList["GaterherState", item.Key].Value = item.Value.TaskEntity.GaterherState;
                            dgvList["TotalCount", item.Key].Value = item.Value.TaskEntity.TotalCount;
                            dgvList["ProgessValue", item.Key].Value = TaskRunItem.GetProgessValue(item.Value.TaskEntity.TrueCount, item.Value.TaskEntity.ErrorCount, item.Value.TaskEntity.TotalCount);
                            if (item.Value.TaskEntity.GaterherState == EnumGloabParas.EnumThreadState.Run || item.Value.TaskEntity.GaterherState == EnumGloabParas.EnumThreadState.SpiderCompleted)
                            {
                                m_RunStartFlag = true;
                            }
                        }
                        else
                        {
                            resetRowIndex = true;
                            break;
                        }
                    }


                }
                else
                {
                    resetRowIndex = true;
                }
            }
        }

        private void toolEncodeTool_Click(object sender, EventArgs e)
        {
            frmEncode frmEncode0 = new frmEncode();
            frmEncode0.ShowDialog();
        }

        private void toolSysInfo_Click(object sender, EventArgs e)
        {
            frmAbout frmAbout0 = new frmAbout();
            frmAbout0.ShowDialog(this);
        }


    }
}
