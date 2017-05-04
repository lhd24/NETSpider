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
using System.IO;
using NETSpider.Controls;

namespace NETSpider
{
    public partial class frmTask : Form
    {
        TaskItem taskItemEntity;
        private List<TaskItemLevelUrl> taskItemLevelUrlList = new List<TaskItemLevelUrl>();
        private string GetFilePath()
        {
            if (taskItemEntity == null)
            {
                return string.Empty;
            }
            return @"categroy\" + taskItemEntity.CategroyName + @"\" + taskItemEntity.TaskName + ".xml";
        }
        #region MainForm
        /// <summary>
        ///  2 代表编辑
        /// </summary>
        private int FormState = 0;
        public frmTask(string categoryName, string taskName)
            : this(categoryName, taskName, null)
        {
        }
        public frmTask(string categoryName)
            : this(categoryName, "", null)
        {

        }
        public frmTask(string categoryName, string taskName, TaskRunItem taskRunItemEntity)
        {
            InitializeComponent();
            #region MyRegion
            CheckForIllegalCrossThreadCalls = false;
            EnumHelper.InitItemList(categoryName, this.cbTaskCategory);
            EnumHelper.InitItemList(typeof(EnumGloabParas.EnumTaskType), this.cbTaskType);
            EnumHelper.InitItemList(typeof(EnumGloabParas.EnumExcuteType), this.cbExcuteType);
            EnumHelper.InitItemList(typeof(EnumGloabParas.EnumEncodeType), this.cbWebEncode);
            EnumHelper.InitItemList(typeof(EnumGloabParas.EnumEncodeType), this.cbUrlEncode);
            EnumHelper.InitItemList(typeof(EnumGloabParas.EnumEncodeType), this.cbPubEncode);
            EnumHelper.InitItemList(typeof(EnumGloabParas.EnumDataFileType), this.cbDataFileType);
            EnumHelper.InitItemList(typeof(EnumGloabParas.EnumDataTextType), this.cbDataTextType);
            EnumHelper.InitItemList(typeof(EnumGloabParas.EnumLimitSign), this.cbLimitSign);
            EnumHelper.InitItemList(typeof(EnumGloabParas.EnumExportLimit), this.cbExportLimit);
            if (cbPubDataTable.Items.Count > 0)
                cbPubDataTable.SelectedIndex = 0;

            if (taskRunItemEntity == null)
            {
                if (!string.IsNullOrEmpty(taskName))
                {
                    string errMsg = string.Empty;
                    taskItemEntity = XmlHelper.LoadFromXml<TaskItem>(Program.GetConfigPath(@"categroy\" + categoryName + @"\" + taskName + ".xml"), ref errMsg);
                    if (!string.IsNullOrEmpty(errMsg))
                    {
                        WinFormLib.Core.MessageBoxHelper.ShowError(errMsg);
                        return;
                    }
                    FormState = 1;
                }
                else
                {
                    FormState = 0;
                    taskItemEntity = new TaskItem();
                }
            }
            else
            {
                taskItemEntity = StaticConst.CloneObject<TaskRunItem, TaskItem>(taskRunItemEntity);
                btnCancel.Text = "返回";
            }
            #endregion
            #region MyRegion
            this.lbTaskName.DataViewValue = taskItemEntity.TaskName.Value;
            this.lbRemark.DataViewValue = taskItemEntity.Remark.Value;
            this.lbLoginUrl.DataViewValue = taskItemEntity.LoginUrl.Value;
            this.lbDownFilePath.DataViewValue = taskItemEntity.DownFilePath.Value;
            this.lbWebCookie.DataViewValue = taskItemEntity.WebCookie.Value;
            this.cbExcuteType.SelectedValue = taskItemEntity.ExcuteType.ToString();
            this.cbTaskType.SelectedValue = taskItemEntity.TaskType.ToString();
            this.cbWebEncode.SelectedValue = taskItemEntity.WebEncode.ToString();
            this.cbUrlEncode.SelectedValue = taskItemEntity.UrlEncode.ToString();
            this.numThreadNum.Value = taskItemEntity.ThreadNum;
            this.lbTempUrl.DataViewValue = taskItemEntity.TempUrl.Value;
            this.lbLastStartPos.DataViewValue = taskItemEntity.LastStartPos.Value;
            this.lbLastEndPos.Text = taskItemEntity.LastEndPos.Value;
            this.chkAutoErrorLog.Checked = taskItemEntity.AutoErrorLog;
            this.chkAutoLog.Checked = taskItemEntity.AutoLog;
            this.chkTryAgainFlag.Checked = taskItemEntity.TryAgainFlag;
            this.numTryAgainCount.Value = taskItemEntity.TryAgainCount;
            this.txtTaskID.Text = taskItemEntity.TaskID.ToString();
            this.dgvUrlList.ReBind(TaskItemUrl.GetDataTable(taskItemEntity.UrlList));
            this.dgvUrlList.ClearSelection();

            this.dgvColumnItemList.ReBind(TaskColumnItem.GetDataTable(taskItemEntity.ColumnItemList));
            this.dgvColumnItemList.ClearSelection();
            if (taskItemEntity.TriggerType == EnumGloabParas.EnumTriggerType.GetData)
            {
                raGetData.Checked = true;
            }
            else if (taskItemEntity.TriggerType == EnumGloabParas.EnumTriggerType.Publish)
            {
                raPublish.Checked = true;
            }
            taskPlanItemList = taskItemEntity.TaskPlanItemList;
            this.dgvTaskPlan.ReBind(TaskPlanItem.GetDataTable(taskPlanItemList), false);
            this.dgvTaskPlan.ClearSelection();
            #endregion

            #region MyRegion
            if (taskItemEntity.ExcuteType == EnumGloabParas.EnumExcuteType.GetAndPublish)
            {
                switch (taskItemEntity.ConnectionType)
                {
                    case EnumGloabParas.EnumConnectionType.None:
                        break;
                    case EnumGloabParas.EnumConnectionType.ExportTxt:
                    case EnumGloabParas.EnumConnectionType.ExportExcel:
                        lbExportFileName.DataViewValue = taskItemEntity.ConnectionString.Value;
                        if (taskItemEntity.ConnectionType == EnumGloabParas.EnumConnectionType.ExportTxt)
                        {
                            raExportTxt.Checked = true;
                        }
                        else
                        {
                            raExportExcel.Checked = true;
                        }
                        break;
                    case EnumGloabParas.EnumConnectionType.ExportAccess:
                    case EnumGloabParas.EnumConnectionType.ExportMSSQL:
                    case EnumGloabParas.EnumConnectionType.ExportMySql:
                        lbConnectionString.DataViewValue = taskItemEntity.ConnectionString.Value;
                        if (!string.IsNullOrEmpty(taskItemEntity.ConnectionString.Value))
                        {
                            cbPubDataTable.Items.Clear();
                            cbPubDataTable.Items.Add("None");
                            List<string> items = ConectionTables.GetTables(taskItemEntity.ConnectionType, taskItemEntity.ConnectionString.Value);
                            foreach (string item in items)
                            {
                                cbPubDataTable.Items.Add(item);
                            }
                            cbPubDataTable.SelectedItem = taskItemEntity.PubDataTable.Value;
                        }
                        lbPubSql.DataViewValue = taskItemEntity.PubSql.Value;
                        if (taskItemEntity.ConnectionType == EnumGloabParas.EnumConnectionType.ExportAccess)
                        {
                            raExportAccess.Checked = true;
                        }
                        else if (taskItemEntity.ConnectionType == EnumGloabParas.EnumConnectionType.ExportMSSQL)
                        {
                            raExportMSSQL.Checked = true;
                        }
                        else
                        {
                            raExportMySql.Checked = true;
                        }
                        break;
                    case EnumGloabParas.EnumConnectionType.ExportWeb:
                        lbWebSiteAdr.DataViewValue = taskItemEntity.ConnectionString.Value;
                        cbPubEncode.SelectedValue = taskItemEntity.PubEncode.ToString();
                        lbPubWebCookie.DataViewValue = taskItemEntity.PubWebCookie.Value;
                        raExportWeb.Checked = true;
                        break;
                }

            }
            #endregion

            if (taskRunItemEntity != null)
            {
                this.btnApply.Enabled = false;
                this.btnSubmit.Enabled = false;
            }
        }


        private void frmTask_Load(object sender, EventArgs e)
        {
            int count = cnArgs.Items.Count;
            if (count == 6)
            {
                string errMsg = string.Empty;
                DictList dictList = XmlHelper.LoadFromXml<DictList>(Program.GetConfigPath(@"dict.xml"), ref errMsg);
                if (string.IsNullOrEmpty(errMsg))
                {
                    foreach (var item in dictList)
                    {
                        ToolStripMenuItem toolDict = new ToolStripMenuItem();
                        toolDict.Name = item.CategoryName.Value;
                        toolDict.Size = new System.Drawing.Size(178, 22);
                        toolDict.Text = "字典" + item.CategoryName.Value;
                        toolDict.Tag = item.CategoryName.Value;
                        toolDict.Click += new EventHandler(delegate(object menu, EventArgs evt)
                        {
                            ToolStripMenuItem menuItem = menu as ToolStripMenuItem;
                            if (menuItem != null)
                            {
                                lbMainUrl.DataViewValue += "{dict:" + menuItem.Tag.ToString() + "}";
                            }
                        });
                        cnArgs.Items.Add(toolDict);
                    }
                }
            }
        }
        private void btnApply_Click(object sender, EventArgs e)
        {
            SaveTask();
        }
        private void btnSubmit_Click(object sender, EventArgs e)
        {
            if (SaveTask())
            {
                this.Close();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmTask_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void btnDirBrowser_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(lbDownFilePath.Value))
            {
                folderBrowserDialog1.SelectedPath = lbDownFilePath.Value;
            }
            else
            {
                folderBrowserDialog1.SelectedPath = Directory.GetCurrentDirectory();
            }
            DialogResult result = folderBrowserDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                lbDownFilePath.DataViewValue = folderBrowserDialog1.SelectedPath;
            }
        }

        private void btnGratherTest0_Click(object sender, EventArgs e)
        {
            this.tabControl1.SelectedTab = this.tabPage4;

        }
        private bool SaveTask()
        {
            taskItemEntity.CategroyName = cbTaskCategory.Value.ToString();
            taskItemEntity.DownFilePath = lbDownFilePath.Value;
            taskItemEntity.DownFileQueue = chkDownFileQueue.Checked;
            taskItemEntity.ExcuteType = (EnumGloabParas.EnumExcuteType)Enum.Parse(typeof(EnumGloabParas.EnumExcuteType), cbExcuteType.SelectedValue.ToString());
            taskItemEntity.TaskType = (EnumGloabParas.EnumTaskType)Enum.Parse(typeof(EnumGloabParas.EnumTaskType), cbTaskType.SelectedValue.ToString());
            string fileTempPath = string.Empty;
            if (this.FormState == 1)
            {
                fileTempPath = GetFilePath();
            }
            taskItemEntity.TaskName = lbTaskName.Value;
            taskItemEntity.IsAjax = false;
            taskItemEntity.LoginFlag = chkLoginFlag.Checked;
            taskItemEntity.LoginUrl = lbLoginUrl.Value;
            taskItemEntity.TaskID = TryParse.StrToInt(txtTaskID.Text);
            taskItemEntity.ThreadNum = TryParse.StrToInt(numThreadNum.Value);
            taskItemEntity.UrlCount = 1;
            taskItemEntity.WebEncode = (EnumGloabParas.EnumEncodeType)Enum.Parse(typeof(EnumGloabParas.EnumEncodeType), cbWebEncode.SelectedValue.ToString());
            taskItemEntity.Remark = lbRemark.Value;
            taskItemEntity.Version = StaticConst.Version;
            taskItemEntity.WaitMinutes = TryParse.StrToInt(numWaitMinutes.Value);
            taskItemEntity.WaitUrlCount = TryParse.StrToInt(numWaitUrlCount.Value);
            taskItemEntity.WebCookie = lbWebCookie.Value;
            taskItemEntity.TempUrl = lbTempUrl.Value;
            taskItemEntity.LastStartPos = CDataItem.Instance(lbLastStartPos.Value);
            taskItemEntity.LastEndPos = CDataItem.Instance(lbLastEndPos.Text);
            taskItemEntity.AutoErrorLog = chkAutoErrorLog.Checked;
            taskItemEntity.AutoLog = chkAutoLog.Checked;
            taskItemEntity.TryAgainCount = TryParse.StrToInt(numTryAgainCount.Value);
            taskItemEntity.TryAgainFlag = chkTryAgainFlag.Checked;
            taskItemEntity.ConnectionType = EnumGloabParas.EnumConnectionType.None;
            taskItemEntity.ConnectionString = CDataItem.Instance("");
            taskItemEntity.PubWebCookie = CDataItem.Instance("");
            taskItemEntity.PubDataTable = CDataItem.Instance("");
            taskItemEntity.PubSql = CDataItem.Instance("");
            taskItemEntity.PubEncode = EnumGloabParas.EnumEncodeType.AUTO;
            taskItemEntity.TaskPlanItemList = taskPlanItemList;
            taskItemEntity.TriggerType = EnumGloabParas.EnumTriggerType.None;
            if (raGetData.Checked)
            {
                taskItemEntity.TriggerType = EnumGloabParas.EnumTriggerType.GetData;
            }
            else if (raPublish.Checked)
            {
                taskItemEntity.TriggerType = EnumGloabParas.EnumTriggerType.Publish;
            }
            if (taskItemEntity.ExcuteType == EnumGloabParas.EnumExcuteType.GetAndPublish)
            {
                if (raExportTxt.Checked)
                {
                    taskItemEntity.ConnectionType = EnumGloabParas.EnumConnectionType.ExportTxt;
                    taskItemEntity.ConnectionString = lbExportFileName.Value;
                }
                else if (raExportExcel.Checked)
                {
                    taskItemEntity.ConnectionType = EnumGloabParas.EnumConnectionType.ExportExcel;
                    taskItemEntity.ConnectionString = lbExportFileName.Value;
                }
                else if (raExportAccess.Checked)
                {
                    taskItemEntity.ConnectionType = EnumGloabParas.EnumConnectionType.ExportAccess;
                    taskItemEntity.ConnectionString = lbConnectionString.Value;
                    if (cbPubDataTable.SelectedItem != null)
                        taskItemEntity.PubDataTable = cbPubDataTable.SelectedItem.ToString();
                    taskItemEntity.PubSql = lbPubSql.Value;
                }
                else if (raExportMSSQL.Checked)
                {
                    taskItemEntity.ConnectionType = EnumGloabParas.EnumConnectionType.ExportMSSQL;
                    taskItemEntity.ConnectionString = lbConnectionString.Value;
                    if (cbPubDataTable.SelectedItem != null)
                        taskItemEntity.PubDataTable = cbPubDataTable.SelectedItem.ToString();
                    taskItemEntity.PubSql = lbPubSql.Value;
                }
                else if (raExportMySql.Checked)
                {
                    taskItemEntity.ConnectionType = EnumGloabParas.EnumConnectionType.ExportMySql;
                    taskItemEntity.ConnectionString = lbConnectionString.Value;
                    if (cbPubDataTable.SelectedItem != null)
                        taskItemEntity.PubDataTable = cbPubDataTable.SelectedItem.ToString();
                    taskItemEntity.PubSql = lbPubSql.Value;
                }
                else if (raExportWeb.Checked)
                {
                    taskItemEntity.ConnectionType = EnumGloabParas.EnumConnectionType.ExportWeb;
                    taskItemEntity.ConnectionString = lbWebSiteAdr.Value;
                    taskItemEntity.PubWebCookie = lbPubWebCookie.Value;
                    taskItemEntity.PubEncode = (EnumGloabParas.EnumEncodeType)Enum.Parse(typeof(EnumGloabParas.EnumEncodeType), cbPubEncode.SelectedValue.ToString());
                }

            }
            string errMsg = string.Empty;

            if (taskItemEntity.UrlList.Count == 0)
            {
                if (MessageBoxHelper.ShowQuestion("没有可以采集的网址,确定保存吗?") != System.Windows.Forms.DialogResult.OK)
                {
                    return false;
                }
            }
            string categoryName = ((ItemValue)cbTaskCategory.SelectedItem).text;
            TaskCategroyList categoryList = XmlHelper.LoadFromXml<TaskCategroyList>(Program.GetConfigPath("category.xml"), ref errMsg);
            if (!string.IsNullOrEmpty(errMsg))
            {
                WinFormLib.Core.MessageBoxHelper.ShowError(errMsg);
                return false;
            }
            TaskCategroy category = categoryList.Where(q => q.CategroyName.Value == categoryName).FirstOrDefault();
            if (category != null)
            {
                if (this.FormState == 1)
                {
                    TaskItemBase task = category.TaskItemList.Where(q => q.TaskID == taskItemEntity.TaskID).FirstOrDefault();
                    if (task != null)
                    {
                        task.TaskName = taskItemEntity.TaskName;
                        task.CategroyName = taskItemEntity.CategroyName;
                        task.Version = StaticConst.Version;
                    }
                    else
                    {
                        MessageBoxHelper.ShowError("查找失败!");
                        return false;
                    }
                }
                else
                {
                    long taskId = categoryList.MaxTaskID;
                    TaskItemBase task = new TaskItemBase()
                    {
                        TaskName = taskItemEntity.TaskName,
                        CategroyName = taskItemEntity.CategroyName,
                        TaskID = taskId + 1,
                        Version = StaticConst.Version,
                    };
                    taskItemEntity.TaskID = taskId + 1;
                    txtTaskID.Text = taskItemEntity.TaskID.ToString();
                    category.TaskItemList.Add(task);
                }
                XmlHelper.Save2File<TaskCategroyList>(categoryList, Program.GetConfigPath("category.xml"), ref errMsg);
                if (!string.IsNullOrEmpty(errMsg))
                {
                    WinFormLib.Core.MessageBoxHelper.ShowError(errMsg);
                    return false;
                }
            }

            string filePath = GetFilePath();
            if (!string.IsNullOrEmpty(filePath))
            {
                if (this.FormState == 1 && filePath != fileTempPath)
                {
                    XmlHelper.DeleteFile(Program.GetConfigPath(fileTempPath));
                }
                FormState = 1;
                XmlHelper.Save2File<TaskItem>(taskItemEntity, Program.GetConfigPath(filePath), ref errMsg);
                if (!string.IsNullOrEmpty(errMsg))
                {
                    WinFormLib.Core.MessageBoxHelper.ShowError(errMsg);
                    return false;
                }
            }
            else
            {
                MessageBoxHelper.Show("文件路径出错了!");
                return false;
            }
            MessageBoxHelper.Show("保存任务成功!");
            return true;
        }
        #endregion

        #region Controls
        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.tabControl1.SelectedTab == this.tabPage2)
            {
                dgvUrlList.ClearSelection();
            }
            else if (this.tabControl1.SelectedTab == this.tabPage3)
            {
                dgvColumnItemList.ClearSelection();
            }
        }

        private void chkNextPageFlag_CheckedChanged(object sender, EventArgs e)
        {
            if (chkNextPageFlag.Checked)
            {
                lbNextPageText.Enabled = true;
                lbNextPageText.DataViewValue = "";
            }
            else
            {
                lbNextPageText.Enabled = false;
                lbNextPageText.DataViewValue = "";
            }
        }

        private void chkNavigateFlag_CheckedChanged(object sender, EventArgs e)
        {
            if (chkNavigateFlag.Checked)
            {
                gbNavigateFlag.Enabled = true;
                dgvTaskItemLevelUrlList.DataSource = TaskItemLevelUrl.GetDataTable(taskItemLevelUrlList);
                dgvTaskItemLevelUrlList.Refresh();
            }
            else
            {
                gbNavigateFlag.Enabled = false;
                dgvTaskItemLevelUrlList.DataSource = TaskItemLevelUrl.GetDataTable(new List<TaskItemLevelUrl>());
                dgvTaskItemLevelUrlList.Refresh();
            }
        }
        #endregion


        #region TaskItemUrl
        private void btnAddTaskItemUrl_Click(object sender, EventArgs e)
        {
            TaskItemUrl entity = new TaskItemUrl()
            {
                LevelCount = taskItemLevelUrlList.Count,
                LevelUrlList = taskItemLevelUrlList,
                MainUrl = CDataItem.Instance(lbMainUrl.Value),
                NavigateFlag = chkNavigateFlag.Checked,
                NextPageFlag = chkNextPageFlag.Checked,
                UrlCount = 0,
                NextPageText = CDataItem.Instance(lbNextPageText.Value),
                StartPos = CDataItem.Instance(lbStartPos.Value),
                EndPos = CDataItem.Instance(txtEndPos.Text.Trim()),
            };

            taskItemEntity.UrlList.Add(entity);
            taskItemLevelUrlList = new List<TaskItemLevelUrl>();
            dgvTaskItemLevelUrlList.DataSource = TaskItemLevelUrl.GetDataTable(taskItemLevelUrlList);
            dgvTaskItemLevelUrlList.Refresh();
            this.dgvUrlList.ReBind(TaskItemUrl.GetDataTable(taskItemEntity.UrlList));
            this.dgvUrlList.ClearSelection();
        }

        private void dgvUrlList_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
        {
            if (dgvUrlList.SelectedRows.Count > 0)
            {
                object MainUrl = dgvUrlList[0, dgvUrlList.SelectedRows[0].Index].Value;
                TaskItemUrl entity = taskItemEntity.UrlList.Where(q => q.MainUrl.Value == TryParse.ToString(MainUrl)).FirstOrDefault();
                if (entity != null)
                {
                    lbMainUrl.DataViewValue = entity.MainUrl.Value;
                    chkNavigateFlag.Checked = entity.NavigateFlag;
                    chkNextPageFlag.Checked = entity.NextPageFlag;
                    lbNextPageText.DataViewValue = entity.NextPageText.Value;
                    lbStartPos.DataViewValue = entity.StartPos.Value;
                    txtEndPos.Text = entity.EndPos.Value;
                    taskItemLevelUrlList = entity.LevelUrlList;
                    dgvTaskItemLevelUrlList.DataSource = TaskItemLevelUrl.GetDataTable(taskItemLevelUrlList);
                    dgvTaskItemLevelUrlList.Refresh();
                }
            }
            else
            {
                lbMainUrl.DataViewValue = "";
                chkNavigateFlag.Checked = false;
                chkNextPageFlag.Checked = false;
                lbNextPageText.DataViewValue = "";
                lbStartPos.DataViewValue = "";
                txtEndPos.Text = "";
                taskItemLevelUrlList = new List<TaskItemLevelUrl>();
                dgvTaskItemLevelUrlList.DataSource = TaskItemLevelUrl.GetDataTable(taskItemLevelUrlList);
                dgvTaskItemLevelUrlList.Refresh();
            }
        }

        private void btnEditTaskItemUrl_Click(object sender, EventArgs e)
        {
            if (dgvUrlList.SelectedRows.Count > 0)
            {
                object MainUrl = dgvUrlList[0, dgvUrlList.SelectedRows[0].Index].Value;
                TaskItemUrl entity = taskItemEntity.UrlList.Where(q => q.MainUrl.Value == TryParse.ToString(MainUrl)).FirstOrDefault();
                if (entity != null)
                {
                    entity.MainUrl = CDataItem.Instance(lbMainUrl.DataViewValue);
                    entity.NextPageText = CDataItem.Instance(lbNextPageText.DataViewValue);
                    entity.NavigateFlag = chkNavigateFlag.Checked;
                    entity.NextPageFlag = chkNextPageFlag.Checked;
                    entity.NextPageText = chkNextPageFlag.Checked ? lbNextPageText.Value : "";
                    entity.UrlCount = 0;
                    entity.StartPos = lbStartPos.Value;
                    entity.EndPos = txtEndPos.Text.Trim();
                    entity.LevelCount = chkNavigateFlag.Checked ? taskItemLevelUrlList.Count : 0;
                    entity.LevelUrlList = chkNavigateFlag.Checked ? taskItemLevelUrlList : new List<TaskItemLevelUrl>();
                    dgvUrlList.ReBind(TaskItemUrl.GetDataTable(taskItemEntity.UrlList));
                    dgvUrlList.Refresh();

                    taskItemLevelUrlList = new List<TaskItemLevelUrl>();
                    dgvTaskItemLevelUrlList.DataSource = TaskItemLevelUrl.GetDataTable(taskItemLevelUrlList);
                    dgvTaskItemLevelUrlList.Refresh();
                    dgvUrlList.ClearSelection();
                }
            }
        }

        private void btnDelTaskItemUrl_Click(object sender, EventArgs e)
        {
            if (dgvUrlList.SelectedRows.Count > 0)
            {
                object MainUrl = dgvUrlList[0, dgvUrlList.SelectedRows[0].Index].Value;
                TaskItemUrl entity = taskItemEntity.UrlList.Where(q => q.MainUrl.Value == TryParse.ToString(MainUrl)).FirstOrDefault();
                if (entity != null)
                {
                    taskItemEntity.UrlList.Remove(entity);
                    dgvUrlList.ReBind(TaskItemUrl.GetDataTable(taskItemEntity.UrlList));
                    dgvUrlList.Refresh();
                }
            }
        }

        #endregion

        #region TaskItemLevelUrlList
        private void btnAddLevelUrl_Click(object sender, EventArgs e)
        {

            taskItemLevelUrlList.Add(new TaskItemLevelUrl() { LevelID = taskItemLevelUrlList.Count + 1, LevelUrl = txtLevelUrl.Text });
            dgvTaskItemLevelUrlList.DataSource = TaskItemLevelUrl.GetDataTable(taskItemLevelUrlList);
            dgvTaskItemLevelUrlList.Refresh();
            txtLevelUrl.Text = "";
        }
        private void btnDelLevelUrl_Click(object sender, EventArgs e)
        {
            if (dgvTaskItemLevelUrlList.SelectedRows.Count > 0)
            {
                object LevelID = dgvTaskItemLevelUrlList[0, dgvTaskItemLevelUrlList.SelectedRows[0].Index].Value;
                TaskItemLevelUrl entity = taskItemLevelUrlList.Where(q => q.LevelID == TryParse.StrToInt(LevelID)).FirstOrDefault();
                if (entity != null)
                {
                    taskItemLevelUrlList.Remove(entity);
                    dgvTaskItemLevelUrlList.DataSource = TaskItemLevelUrl.GetDataTable(taskItemLevelUrlList);
                    dgvTaskItemLevelUrlList.Refresh();
                }
            }
        }
        private void dgvTaskItemLevelUrlList_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            object LevelID = dgvTaskItemLevelUrlList[0, e.RowIndex].Value;
            TaskItemLevelUrl entity = taskItemLevelUrlList.Where(q => q.LevelID == TryParse.StrToInt(LevelID)).FirstOrDefault();
            if (entity != null)
            {
                entity.LevelUrl = TryParse.ToString(dgvTaskItemLevelUrlList[1, e.RowIndex].Value);
            }
        }

        #endregion


        #region toolNums_Click
        private void toolNums_Click(object sender, EventArgs e)
        {
            lbMainUrl.DataViewValue += "{1,100,1}";
        }
        private void toolLetter1_Click(object sender, EventArgs e)
        {
            lbMainUrl.DataViewValue += "{A-Z}";
        }

        private void toolLetter0_Click(object sender, EventArgs e)
        {
            lbMainUrl.DataViewValue += "{a-z}";
        }

        private void toolPlusNums_Click(object sender, EventArgs e)
        {
            lbMainUrl.DataViewValue += "{100,1,-1}";
        }
        #endregion




        #region cGatherTaskManage　操作

        /// <summary>
        /// 获取测试网址
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGetUrl_Click(object sender, EventArgs e)
        {
            cGatherGetTempData cTempData = new cGatherGetTempData();
            cTempData.runWebUrls = new List<cGatherUrlItem>();
            cTempData.GatherUrlItemSingleFlag = true;
            cTempData.ThreadGetMainUrlsWork(taskItemEntity);
            lbRemark.DataViewValue = "";
            if (cTempData.runWebUrls.Count > 0)
            {
                foreach (var item in cTempData.runWebUrls)
                {
                    if (item.GaterherFlag == EnumGloabParas.EnumUrlGaterherState.Run)
                    {
                        lbTempUrl.DataViewValue = item.Url;
                        break;
                    }
                    GetUrl(cTempData, item);
                }
            }
        }

        private void GetUrl(cGatherGetTempData cTempData, cGatherUrlItem item)
        {
            string html = string.Empty;
            int tryCount = 3;
            while (true)
            {
                try
                {
                    html = cTempData.GetHtml(item.Url, taskItemEntity.WebCookie.Value, taskItemEntity.WebEncode, item.StartPos, item.EndPos, taskItemEntity.IsAjax);
                }
                catch (Exception)
                {
                    if (tryCount > 0)
                    {
                        tryCount--;
                        continue;
                    }
                }
                break;
            }
            if (item.LevelUrlList.Count > item.Level)
            {
                string nextUrl = item.LevelUrlList[item.Level];
                List<string> levelUrls = cTempData.GetNextLevelUrl(item.Url, html, nextUrl);
                foreach (var levelUrl in levelUrls)
                {
                    #region MyRegion
                    if (item.LevelUrlList.Count > item.Level + 1)
                    {
                        cGatherUrlItem nextUrlItem = new cGatherUrlItem()
                        {
                            GaterherFlag = EnumGloabParas.EnumUrlGaterherState.First,
                            LevelUrlList = item.LevelUrlList,
                            NextPageText = "",//下一页标识只针对一级页面有效的
                            Url = levelUrl,
                            Level = item.Level + 1,
                            StartPos = "",
                            EndPos = "",
                        };
                        cTempData.runWebUrls.Add(nextUrlItem);
                        GetUrl(cTempData, nextUrlItem);
                        break;
                    }
                    else
                    {
                        lbTempUrl.DataViewValue = levelUrl;
                        break;
                    }
                    #endregion
                }
            }
        }

        /// <summary>
        /// 获取测试网页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGetCode_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(lbTempUrl.Value))
            {
                try
                {
                    string html = new cGatherTaskThreadBase().GetHtml(lbTempUrl.Value, taskItemEntity.WebCookie.Value, taskItemEntity.WebEncode, "", "", taskItemEntity.IsAjax);
                    string tmpPath = Path.GetTempPath();
                    string m_FileName = "~" + DateTime.Now.ToFileTime().ToString() + ".txt";
                    m_FileName = tmpPath + "\\" + m_FileName;
                    using (FileStream myStream = File.Open(m_FileName, FileMode.Create, FileAccess.Write, FileShare.Write))
                    {
                        StreamWriter sw = new StreamWriter(myStream, System.Text.Encoding.GetEncoding("gb2312"));
                        sw.Write(html);
                        sw.Close();
                        myStream.Close();
                        System.Diagnostics.Process.Start(m_FileName);
                    }
                }
                catch (Exception ex)
                {
                    MessageBoxHelper.ShowError(ex.Message);
                }
            }
        }
        /// <summary>
        /// 获取测试网页,并读取数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGatherTest_Click(object sender, EventArgs e)
        {
            dgvTestData.AutoGenerateColumns = true;
            btnGatherTest.Enabled = false;
            cGatherGetTempData cTempData = new cGatherGetTempData();
            string errMsg = string.Empty;
            DataTable dataTable = cTempData.GetDataTable(null, taskItemEntity.ColumnItemList, new cGatherUrlItem()
            {
                Url = lbTempUrl.Value,
                LevelUrlList = new List<string>(),
                EndPos = taskItemEntity.LastEndPos.Value,
                StartPos = taskItemEntity.LastStartPos.Value,
                GaterherFlag = EnumGloabParas.EnumUrlGaterherState.Run,
                Level = 0,
                NextPageText = "",
            }, taskItemEntity.WebCookie.Value, taskItemEntity.WebEncode, taskItemEntity.IsAjax, ref errMsg);
            if (dataTable != null && string.IsNullOrEmpty(errMsg))
            {
                dgvTestData.Invoke(new MethodInvoker(delegate()
                {
                    dgvTestData.AutoGenerateColumns = true;
                    dgvTestData.DataSource = dataTable;
                }));
            }
            else
            {
                MessageBoxHelper.ShowError(errMsg);
            }
            btnGatherTest.Enabled = true;
        }

        void cGatherWeb_e_OnGatherDataCompleted(cGatherDataEventArgs evt)
        {
            dgvTestData.Invoke(new MethodInvoker(delegate()
            {
                if (dgvTestData.DataSource != null)
                {
                    DataTable tempData = dgvTestData.DataSource as DataTable;
                    tempData.Merge(evt.dataTable);
                    dgvTestData.DataSource = tempData;
                }
                else
                {
                    dgvTestData.AutoGenerateColumns = true;
                    dgvTestData.DataSource = evt.dataTable;
                }
            }));

        }
        #endregion

        #region btnAddColumnItem_Click
        private void btnAddColumnItem_Click(object sender, EventArgs e)
        {
            if (taskItemEntity.ColumnItemList == null)
            {
                taskItemEntity.ColumnItemList = new List<TaskColumnItem>();
            }
            TaskColumnItem entity = new TaskColumnItem()
            {
                DataFileType = (EnumGloabParas.EnumDataFileType)Enum.Parse(typeof(EnumGloabParas.EnumDataFileType), cbDataFileType.SelectedValue.ToString()),
                DataTextType = CDataItem.Instance(cbDataTextType.Text.Trim()),
                StartPos = lbColumnItemStartPos.Value,
                EndPos = lbColumnItemEndPos.Value,
                ExportLimit = (EnumGloabParas.EnumExportLimit)Enum.Parse(typeof(EnumGloabParas.EnumExportLimit), cbExportLimit.SelectedValue.ToString()),
                LimitSign = (EnumGloabParas.EnumLimitSign)Enum.Parse(typeof(EnumGloabParas.EnumLimitSign), cbLimitSign.SelectedValue.ToString()),
                ExportLimitText = CDataItem.Instance(""),
                LimitSignText = CDataItem.Instance(""),
            };
            if (taskItemEntity.ColumnItemList.Where(q => q.DataTextType.Value == entity.DataTextType.Value).FirstOrDefault() != null)
            {
                MessageBoxHelper.Show("已存在标题，不能再次添加！");
                return;
            }
            if (entity.LimitSign == EnumGloabParas.EnumLimitSign.LimitSign7)
            {
                entity.LimitSignText = lbLimitSignText.Value;
            }
            if (entity.ExportLimit == EnumGloabParas.EnumExportLimit.ExportLimit6
                || entity.ExportLimit == EnumGloabParas.EnumExportLimit.ExportLimit7
                || entity.ExportLimit == EnumGloabParas.EnumExportLimit.ExportLimit8
                || entity.ExportLimit == EnumGloabParas.EnumExportLimit.ExportLimit9)
            {
                entity.ExportLimitText = lbExportLimitText.Value;
            }
            entity.ExportLimitSpaceFlag = chkExportLimit.Checked;
            entity.LimitSignSpaceFlag = chkLimitSign.Checked;

            taskItemEntity.ColumnItemList.Add(entity);
            this.dgvColumnItemList.ReBind(TaskColumnItem.GetDataTable(taskItemEntity.ColumnItemList));
            this.dgvColumnItemList.Refresh();
            this.dgvColumnItemList.ClearSelection();
        }

        private void btnEditColumnItem_Click(object sender, EventArgs e)
        {
            if (dgvColumnItemList.SelectedRows.Count > 0)
            {
                object DataTextType = dgvColumnItemList[0, dgvColumnItemList.SelectedRows[0].Index].Value;
                TaskColumnItem entity = taskItemEntity.ColumnItemList.Where(q => q.DataTextType.Value == TryParse.ToString(DataTextType)).FirstOrDefault();
                if (entity != null)
                {
                    entity.DataFileType = (EnumGloabParas.EnumDataFileType)Enum.Parse(typeof(EnumGloabParas.EnumDataFileType), cbDataFileType.SelectedValue.ToString());
                    entity.DataTextType = CDataItem.Instance(cbDataTextType.Text.Trim());
                    entity.StartPos = lbColumnItemStartPos.Value;
                    entity.EndPos = lbColumnItemEndPos.Value;
                    entity.ExportLimit = (EnumGloabParas.EnumExportLimit)Enum.Parse(typeof(EnumGloabParas.EnumExportLimit), cbExportLimit.SelectedValue.ToString());
                    entity.LimitSign = (EnumGloabParas.EnumLimitSign)Enum.Parse(typeof(EnumGloabParas.EnumLimitSign), cbLimitSign.SelectedValue.ToString());
                    entity.ExportLimitText = CDataItem.Instance("");
                    entity.LimitSignText = CDataItem.Instance("");
                    if (entity.LimitSign == EnumGloabParas.EnumLimitSign.LimitSign7)
                    {
                        entity.LimitSignText = lbLimitSignText.Value;
                    }
                    if (entity.ExportLimit == EnumGloabParas.EnumExportLimit.ExportLimit6
                        || entity.ExportLimit == EnumGloabParas.EnumExportLimit.ExportLimit7
                        || entity.ExportLimit == EnumGloabParas.EnumExportLimit.ExportLimit8
                        || entity.ExportLimit == EnumGloabParas.EnumExportLimit.ExportLimit9)
                    {
                        entity.ExportLimitText = lbExportLimitText.Value;
                    }
                    entity.ExportLimitSpaceFlag = chkExportLimit.Checked;
                    entity.LimitSignSpaceFlag = chkLimitSign.Checked;
                    this.dgvColumnItemList.ReBind(TaskColumnItem.GetDataTable(taskItemEntity.ColumnItemList));
                    this.dgvColumnItemList.Refresh();
                    this.dgvColumnItemList.ClearSelection();
                }
            }
        }

        private void btnDelColumnItem_Click(object sender, EventArgs e)
        {
            object DataTextType = dgvColumnItemList[0, dgvColumnItemList.SelectedRows[0].Index].Value;
            TaskColumnItem entity = taskItemEntity.ColumnItemList.Where(q => q.DataTextType.Value == TryParse.ToString(DataTextType)).FirstOrDefault();
            if (entity != null)
            {
                taskItemEntity.ColumnItemList.Remove(entity);
                this.dgvColumnItemList.ReBind(TaskColumnItem.GetDataTable(taskItemEntity.ColumnItemList));
                this.dgvColumnItemList.Refresh();
                this.dgvColumnItemList.ClearSelection();
            }
        }

        private void btnUpColumnItem_Click(object sender, EventArgs e)
        {
            if (dgvColumnItemList.SelectedRows.Count > 0)
            {
                int RowIndex = dgvColumnItemList.SelectedRows[0].Index;
                if (RowIndex > 0)
                {
                    object DataTextType = dgvColumnItemList[0, RowIndex].Value;
                    TaskColumnItem entity = taskItemEntity.ColumnItemList.Where(q => q.DataTextType.Value == TryParse.ToString(DataTextType)).FirstOrDefault();
                    if (entity != null)
                    {
                        taskItemEntity.ColumnItemList.Remove(entity);
                        taskItemEntity.ColumnItemList.Insert(RowIndex - 1, entity);
                        this.dgvColumnItemList.ReBind(TaskColumnItem.GetDataTable(taskItemEntity.ColumnItemList));
                        this.dgvColumnItemList.Refresh();
                        this.dgvColumnItemList.Rows[RowIndex - 1].Selected = true;
                        //this.dgvColumnItemList.ClearSelection();
                    }
                }
            }
        }

        private void btnDownColumnItem_Click(object sender, EventArgs e)
        {
            int RowIndex = dgvColumnItemList.SelectedRows[0].Index;
            if (RowIndex < taskItemEntity.ColumnItemList.Count - 1)
            {
                object DataTextType = dgvColumnItemList[0, RowIndex].Value;
                TaskColumnItem entity = taskItemEntity.ColumnItemList.Where(q => q.DataTextType.Value == TryParse.ToString(DataTextType)).FirstOrDefault();
                if (entity != null)
                {
                    taskItemEntity.ColumnItemList.Remove(entity);
                    taskItemEntity.ColumnItemList.Insert(RowIndex + 1, entity);
                    this.dgvColumnItemList.ReBind(TaskColumnItem.GetDataTable(taskItemEntity.ColumnItemList));
                    this.dgvColumnItemList.Refresh();
                    this.dgvColumnItemList.Rows[RowIndex + 1].Selected = true;
                }
            }
        }
        private void cbLimitSign_SelectedValueChanged(object sender, EventArgs e)
        {
            if (cbLimitSign.SelectedValue != null)
            {
                EnumGloabParas.EnumLimitSign limitSign = (EnumGloabParas.EnumLimitSign)Enum.Parse(typeof(EnumGloabParas.EnumLimitSign), cbLimitSign.SelectedValue.ToString());
                if (limitSign == EnumGloabParas.EnumLimitSign.LimitSign7)
                {
                    lbLimitSignText.Enabled = true;
                }
                else
                {
                    lbLimitSignText.Enabled = false;
                }
            }
            else
            {
                lbLimitSignText.Enabled = false;
            }
        }

        private void cbExportLimit_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbExportLimit.SelectedValue != null)
            {
                EnumGloabParas.EnumExportLimit exportLimit = (EnumGloabParas.EnumExportLimit)Enum.Parse(typeof(EnumGloabParas.EnumExportLimit), cbExportLimit.SelectedValue.ToString());

                if (exportLimit == EnumGloabParas.EnumExportLimit.ExportLimit6
                    || exportLimit == EnumGloabParas.EnumExportLimit.ExportLimit7
                    || exportLimit == EnumGloabParas.EnumExportLimit.ExportLimit8
                    || exportLimit == EnumGloabParas.EnumExportLimit.ExportLimit9)
                {
                    lbExportLimitText.Enabled = true;
                }
                else
                {
                    lbExportLimitText.Enabled = false;
                }
            }
            else
            {
                lbExportLimitText.Enabled = false;
            }
        }
        private void dgvColumnItemList_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
        {
            if (dgvColumnItemList.SelectedRows.Count > 0)
            {
                object DataTextType = dgvColumnItemList[0, dgvColumnItemList.SelectedRows[0].Index].Value;
                TaskColumnItem entity = taskItemEntity.ColumnItemList.Where(q => q.DataTextType.Value == TryParse.ToString(DataTextType)).FirstOrDefault();
                if (entity != null)
                {
                    lbColumnItemEndPos.DataViewValue = entity.EndPos.Value;
                    lbColumnItemStartPos.DataViewValue = entity.StartPos.Value;
                    cbDataFileType.SelectedValue = entity.DataFileType.ToString();
                    cbDataTextType.Text = entity.DataTextType.Value;
                    cbExportLimit.SelectedValue = entity.ExportLimit.ToString();
                    cbLimitSign.SelectedValue = entity.LimitSign.ToString();
                    lbLimitSignText.DataViewValue = entity.LimitSignText.Value;
                    lbExportLimitText.DataViewValue = entity.ExportLimitText.Value;
                    chkLimitSign.Checked = entity.LimitSignSpaceFlag;
                    chkExportLimit.Checked = entity.ExportLimitSpaceFlag;
                }
            }
            else
            {
                lbColumnItemEndPos.DataViewValue = "";
                lbColumnItemStartPos.DataViewValue = "";
                cbDataFileType.SelectedIndex = 0;
                cbDataTextType.Text = "";
                cbExportLimit.SelectedIndex = 0;
                cbLimitSign.SelectedIndex = 0;
                lbLimitSignText.DataViewValue = "";
                lbExportLimitText.DataViewValue = "";
                chkLimitSign.Checked = true;
                chkExportLimit.Checked = true;
            }
        }
        #endregion

        private void GetCookie(string strCookie)
        {
            this.lbWebCookie.DataViewValue = strCookie;
        }

        private void btnGetCookie_Click(object sender, EventArgs e)
        {
            frmBrowser wftm = new frmBrowser();
            wftm.getFlag = 0;
            wftm.rCookie = new frmBrowser.ReturnCookie(GetCookie);
            wftm.ShowDialog();
            wftm.Dispose();
        }

        private void btnAddNavRule_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.lbMainUrl.Value) || lbMainUrl.Value.IndexOf("http://") == -1)
            {
                return;
            }
            frmAddNavRules frmAddNavRule = new frmAddNavRules(lbMainUrl.Value);
            if (frmAddNavRule.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                txtLevelUrl.Text = frmAddNavRule.ResultValue;
            }
            frmAddNavRule.Dispose();
        }

        private void cbExcuteType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbExcuteType.SelectedIndex == 0)
            {
                pnlPublish.Enabled = false;
            }
            else
            {
                pnlPublish.Enabled = true;
            }
        }

        private void raExportTxt_CheckedChanged(object sender, EventArgs e)
        {
            if (raExportTxt.Checked == true)
            {
                lbExportFileName.DataViewValue = "";
                this.raExportAccess.Checked = false;
                this.raExportMSSQL.Checked = false;
                this.raExportMySql.Checked = false;
                this.raExportWeb.Checked = false;
            }
        }
        private void raExportExcel_CheckedChanged(object sender, EventArgs e)
        {
            if (raExportExcel.Checked == true)
            {
                lbExportFileName.DataViewValue = "";
                this.raExportAccess.Checked = false;
                this.raExportMSSQL.Checked = false;
                this.raExportMySql.Checked = false;
                this.raExportWeb.Checked = false;
            }
        }

        private void raExportAccess_CheckedChanged(object sender, EventArgs e)
        {
            if (raExportAccess.Checked == true)
            {
                lbExportFileName.DataViewValue = "";
                this.raExportExcel.Checked = false;
                this.raExportTxt.Checked = false;
                this.raExportWeb.Checked = false;
            }
        }



        private void raExportMSSQL_CheckedChanged(object sender, EventArgs e)
        {
            if (raExportMSSQL.Checked == true)
            {
                lbExportFileName.DataViewValue = "";
                this.raExportExcel.Checked = false;
                this.raExportTxt.Checked = false;
                this.raExportWeb.Checked = false;
            }
        }

        private void raExportMySql_CheckedChanged(object sender, EventArgs e)
        {
            if (raExportMySql.Checked == true)
            {
                lbExportFileName.DataViewValue = "";
                this.raExportExcel.Checked = false;
                this.raExportTxt.Checked = false;
                this.raExportWeb.Checked = false;
            }
        }

        private void raExportWeb_CheckedChanged(object sender, EventArgs e)
        {
            if (raExportWeb.Checked == true)
            {
                lbExportFileName.DataViewValue = "";
                this.raExportAccess.Checked = false;
                this.raExportMSSQL.Checked = false;
                this.raExportMySql.Checked = false;
                this.raExportTxt.Checked = false;
                this.raExportExcel.Checked = false;
            }
        }

        private void btnSaveFile_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            if (raExportTxt.Checked == true)
            {
                saveFileDialog1.Filter = "txt|*.txt";
                saveFileDialog1.Title = "导出为文本文件";
            }
            else if (raExportExcel.Checked == true)
            {
                saveFileDialog1.Filter = "97-2003 Excel工作簿|*.xls";
                saveFileDialog1.Title = "导出为Excel文件";
            }
            else
            {
                MessageBoxHelper.ShowError("请选择设置导出类型!");
                return;
            }
            DialogResult result = saveFileDialog1.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                lbExportFileName.DataViewValue = saveFileDialog1.FileName;
            }
        }

        private void btnNextTest_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(lbMainUrl.Value) || string.IsNullOrEmpty(lbNextPageText.Value))
            {
                return;
            }
            cGatherGetTempData tempData = new cGatherGetTempData();
            try
            {
                string html = tempData.GetHtml(lbMainUrl.Value, lbWebCookie.Value, (EnumGloabParas.EnumEncodeType)Enum.Parse(typeof(EnumGloabParas.EnumEncodeType), cbWebEncode.SelectedValue.ToString()), lbStartPos.Value, txtEndPos.Text.Trim(), false);
                if (!string.IsNullOrEmpty(html))
                {
                    string url = tempData.GetNextPage(lbMainUrl.Value, html, lbNextPageText.Value);
                    if (!string.IsNullOrEmpty(url))
                    {
                        url = tempData.GetNextUrl(url, lbMainUrl.Value);
                        MessageBoxHelper.Show("获取到网址" + url);
                        return;
                    }
                    else
                    {
                        MessageBoxHelper.ShowError("亲,获取不到网址信息哦！");
                        return;
                    }
                }
                MessageBoxHelper.ShowError("亲,网页内容都获取不到哦！");
                return;
            }
            catch (Exception ex)
            {
                MessageBoxHelper.ShowError(ex.Message);
            }
        }

        private void btnDataConfig_Click(object sender, EventArgs e)
        {
            EnumGloabParas.EnumConnectionType connectionType = EnumGloabParas.EnumConnectionType.None;
            if (raExportMSSQL.Checked)
            {
                connectionType = EnumGloabParas.EnumConnectionType.ExportMSSQL;
            }
            else if (raExportMySql.Checked)
            {
                connectionType = EnumGloabParas.EnumConnectionType.ExportMySql;
            }
            else if (raExportAccess.Checked)
            {
                connectionType = EnumGloabParas.EnumConnectionType.ExportAccess;
            }
            else
            {
                MessageBoxHelper.ShowError("请设置数据库类型！");
                return;
            }
            frmDataConfig frmData = new frmDataConfig(connectionType);
            frmData.e_OnReturnDataSource += new NETSpider.Controls.OnReturnDataSource(frmData_e_OnReturnDataSource);
            frmData.ShowDialog();
        }

        void frmData_e_OnReturnDataSource(EnumGloabParas.EnumConnectionType connectionType, string ConnectionString)
        {
            lbConnectionString.DataViewValue = ConnectionString;
            cbPubDataTable.Items.Clear();
            List<string> items = ConectionTables.GetTables(connectionType, ConnectionString);
            cbPubDataTable.Items.Add("None");
            foreach (string item in items)
            {
                cbPubDataTable.Items.Add(item);
            }
            cbPubDataTable.SelectedIndex = 0;
        }

        private void btnConnetion_Click(object sender, EventArgs e)
        {
            string connectionString = lbConnectionString.Value;
            EnumGloabParas.EnumConnectionType connectionType = EnumGloabParas.EnumConnectionType.None;
            if (raExportAccess.Checked)
            {
                connectionType = EnumGloabParas.EnumConnectionType.ExportAccess;

            }
            else if (raExportMSSQL.Checked)
            {
                connectionType = EnumGloabParas.EnumConnectionType.ExportMSSQL;
            }
            else if (raExportMySql.Checked)
            {
                connectionType = EnumGloabParas.EnumConnectionType.ExportMySql;
            }
            if (connectionType != EnumGloabParas.EnumConnectionType.None && !string.IsNullOrEmpty(connectionString))
            {
                frmData_e_OnReturnDataSource(connectionType, connectionString);
            }
        }

        private void btnDict_Click(object sender, EventArgs e)
        {
            frmDict frmDict1 = new frmDict();
            frmDict1.ShowDialog();
        }

        private void btnAddTaskPlan_Click(object sender, EventArgs e)
        {
            frmAddPlanTask frmAddPlan = new frmAddPlanTask();
            frmAddPlan.e_OnReturnTaskPlanItem += new OnReturnTaskPlanItem(frmAddPlan_e_OnReturnTaskPlanItem);
            DialogResult result = frmAddPlan.ShowDialog(this);
        }
        List<TaskPlanItem> taskPlanItemList = new List<TaskPlanItem>();
        void frmAddPlan_e_OnReturnTaskPlanItem(TaskPlanItem planItem)
        {
            planItem.TaskPlanItemID = taskPlanItemList.Count + 1;
            taskPlanItemList.Add(planItem);
            this.dgvTaskPlan.RemoveBind();
            this.dgvTaskPlan.ReBind(TaskPlanItem.GetDataTable(taskPlanItemList), false);
            this.dgvTaskPlan.ClearSelection();
        }

        private void btnDelTaskPlan_Click(object sender, EventArgs e)
        {
            if (dgvTaskPlan.SelectedRows.Count > 0)
            {
                int taskPlanItemID = DMSFrame.TryParse.StrToInt(dgvTaskPlan[colTaskPlanItemID.Index, dgvTaskPlan.SelectedRows[0].Index].Value);
                if (taskPlanItemID > 0)
                {
                    TaskPlanItem entity = taskPlanItemList.Where(q => q.TaskPlanItemID == taskPlanItemID).FirstOrDefault();
                    if (entity != null)
                    {
                        taskPlanItemList.Remove(entity);
                        this.dgvTaskPlan.RemoveBind();
                        this.dgvTaskPlan.ReBind(TaskPlanItem.GetDataTable(taskPlanItemList), false);
                        this.dgvTaskPlan.ClearSelection();
                    }
                }
            }
        }

        private void btnUrlEncode_Click(object sender, EventArgs e)
        {
            frmEncode frmEncode0 = new frmEncode();
            frmEncode0.ShowDialog();
        }

    }
}
