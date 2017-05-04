using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NETSpider.Entity;
using WinFormLib.Components;
using NETSpider.Controls;
using System.Drawing;
using System.Data;
using WinFormLib.Core;

namespace NETSpider.Gather
{
    public class cGatherTaskTabPageManage
    {
        cGatherTaskManage cGaterWeb;
        System.Windows.Forms.TabControl tabTaskRunLog;
        DataGridView dgvRunTaskList;
        RichTextBoxLog txtLog;
        TaskRunItem taskEntity;
        public TaskRunItem TaskEntity
        {
            get
            {
                return taskEntity;
            }
        }
        public string Name { get; set; }
        public EnumGloabParas.EnumThreadState GetGaterherState()
        {
            if (taskEntity != null)
                return taskEntity.GaterherState;
            return EnumGloabParas.EnumThreadState.Normal;
        }

        ~cGatherTaskTabPageManage()
        {
            if (fileLogger != null)
            {
                fileLogger.Dispose();
                fileLogger = null;
            }
            if (cGaterWeb != null)
            {
                cGaterWeb.Dispose();
                cGaterWeb = null;
            }
        }
        private DataTable m_DataTable;
        private int taskPlanCompleted = 0;
        DMSFrame.Loggers.FileLogger fileLogger = null;
        public cGatherTaskTabPageManage(System.Windows.Forms.TabControl tabTaskRunLog, TaskRunItem _taskEntity)
        {
            this.taskEntity = _taskEntity;
            if (taskEntity == null)
            {
                throw new Exception(taskEntity.TaskTempName + "查找不到");
            }
            this.tabTaskRunLog = tabTaskRunLog;
            this.Name = taskEntity.TaskTempName.Value;
            Int64 taskID = taskEntity.TaskID;
            string taskName = taskEntity.TaskName.Value;
            if (taskEntity.AutoLog)
            {
                string logPath = taskEntity.DownFilePath.Value + @"\" + taskEntity.TaskTempName.Value + ".log";
                fileLogger = new DMSFrame.Loggers.FileLogger(Program.GetConfigPath(logPath));
            }
            string taskFilePath = @"Run\" + taskEntity.TaskTempName + ".xml";
            cGaterWeb = new cGatherTaskManage(taskEntity);
            cGaterWeb.e_Log += new OnGatherLog(delegate(cGatherEventArgs evt)
            {
                string message = (int)evt.MessageType + evt.ThreadName + ":" + evt.Message;
                if (taskEntity.AutoLog && fileLogger != null)
                {
                    lock (fileLogger)
                    {
                        fileLogger.Log(message);
                    }
                }

                if (txtLog != null)
                {
                    cControlInvoked.UpdateText(txtLog, message + "\r\n");
                    Application.DoEvents();
                }

            });


            cGaterWeb.e_TotalCount += new OnGatherTotalCount(delegate(cGatherCompletedEventArgs evt)
            {
                taskEntity.TrueCount = evt.TrueCount;
                taskEntity.TotalCount = evt.TotalCount;
                taskEntity.ErrorCount = evt.ErrorCount;
                taskEntity.GaterherState = evt.GaterherState;
            });

            cGaterWeb.e_GatherManagereCompleteCount += new OnGatherManagereCompleteCount(delegate(cGatherCompletedEventArgs evt)
            {
                #region MyRegion
                taskEntity.TrueCount = evt.TrueCount;
                taskEntity.TotalCount = evt.TotalCount;
                taskEntity.ErrorCount = evt.ErrorCount;
                taskEntity.GaterherState = evt.GaterherState;
                #endregion
            });
            cGaterWeb.e_ThreadAllCompleted += new OnGatherThreadAllCompleted(delegate(cGatherUrlItemEventArgs evt)
            {
                #region MyRegion
                if (txtLog != null)
                {
                    cControlInvoked.UpdateText(txtLog, string.Format("{0}共采集到{1}条,已采集{2}条，错误{3}条！\r\n", (int)EnumGloabParas.EnumMessageType.WARNING, evt.TotalCount, evt.TrueCount, evt.ErrorCount));
                    cControlInvoked.UpdateText(txtLog, string.Format("{0}thread all colsed！\r\n", (int)EnumGloabParas.EnumMessageType.WARNING));
                }
                int totalCount = evt.RunWebUrls.Count + evt.RunFileUrls.Count;

                taskEntity.GaterherState = totalCount == 0 ? EnumGloabParas.EnumThreadState.SpiderCompleted : EnumGloabParas.EnumThreadState.Suspended;
                if (cGaterWeb != null)
                {
                    cGaterWeb.GaterherState = taskEntity.GaterherState;
                }
                taskEntity.TrueCount = evt.TrueCount;
                taskEntity.TotalCount = evt.TotalCount;
                taskEntity.ErrorCount = evt.ErrorCount;
                if (dgvRunTaskList.DataSource != null)
                {
                    SaveData();
                }
                SaveTaskFile(taskFilePath, evt);
                if (taskEntity.GaterherState == EnumGloabParas.EnumThreadState.SpiderCompleted)
                {
                    if (taskEntity.ExcuteType == EnumGloabParas.EnumExcuteType.GetAndPublish)
                    {
                        #region MyRegion
                        if (taskEntity.TriggerType == EnumGloabParas.EnumTriggerType.GetData)
                        {
                            taskPlanCompleted = 0;
                            if (e_OnGatherRunPlanItemCompleted != null)
                            {
                                e_OnGatherRunPlanItemCompleted(new cGatherPublishCompletedEventArgs()
                                {
                                    DataSource = m_DataTable,
                                    TaskEntity = taskEntity,
                                });
                            }
                        }
                        else if (taskEntity.TriggerType == EnumGloabParas.EnumTriggerType.Publish)
                        {
                            if (e_OnGatherPublishCompleted != null)
                            {
                                e_OnGatherPublishCompleted(new cGatherPublishCompletedEventArgs()
                                {
                                    DataSource = m_DataTable,
                                    TaskEntity = taskEntity,
                                });
                            }
                        }
                        else
                        {
                            if (e_OnGatherPublishCompleted != null)
                            {
                                e_OnGatherPublishCompleted(new cGatherPublishCompletedEventArgs()
                                {
                                    DataSource = m_DataTable,
                                    TaskEntity = taskEntity,
                                });
                            }
                        } 
                        #endregion
                    }
                    else
                    {
                        if (e_GatherNotityCompleted != null)
                        {
                            e_GatherNotityCompleted(new cGatherPublishCompletedEventArgs()
                            {
                                DataSource = m_DataTable,
                                TaskEntity = taskEntity,
                            });
                        }
                    }
                }
                #endregion
            });
            cGaterWeb.e_OnGatherDataCompleted += new OnGatherDataCompleted(delegate(cGatherDataEventArgs evt)
            {
                dgvRunTaskList.Invoke(new MethodInvoker(delegate()
                {
                    if (m_DataTable != null)
                    {
                        m_DataTable.Merge(evt.dataTable);
                        dgvRunTaskList.DataSource = m_DataTable;
                        AutoNum--;
                        if (AutoNum < 0)
                        {
                            AutoNum = StaticConst.AutoSaveNum;
                            SaveData();
                            SaveTaskFile(taskFilePath, evt);
                        }
                    }
                    else
                    {
                        m_DataTable = evt.dataTable;
                        dgvRunTaskList.AutoGenerateColumns = true;
                        dgvRunTaskList.DataSource = m_DataTable;
                    }
                    dgvRunTaskList.FirstDisplayedScrollingRowIndex = dgvRunTaskList.RowCount - 1;
                }));

            });
        }
        public void AddTab()
        {
            if (taskEntity != null)
            {
                this.AddTab(taskEntity.TaskID, taskEntity.TaskName.Value, taskEntity.TaskTempName.Value);
            }
        }
        private void SaveTaskFile(string taskFilePath, cGatherUrlItemEventArgs evt)
        {
            taskEntity.GatherUrlItemCompleteList = evt.RunCompleteWebUrls;
            taskEntity.GatherUrlItemTempList = evt.RunWebUrls;
            taskEntity.TrueCount = evt.TrueCount;
            taskEntity.TotalCount = evt.TotalCount;
            taskEntity.ErrorCount = evt.ErrorCount;
            taskEntity.GatherFileItemTempList = evt.RunFileUrls;
            taskEntity.GatherFileItemCompleteList = evt.RunCompleteFileUrls;
            int totalCount = evt.RunWebUrls.Count + evt.RunFileUrls.Count;
            taskEntity.GaterherState = totalCount > 0 ? EnumGloabParas.EnumThreadState.Suspended : EnumGloabParas.EnumThreadState.SpiderCompleted;
            string errMsg = string.Empty;
            XmlHelper.Save2File<TaskRunItem>(taskEntity, Program.GetConfigPath(taskFilePath), ref errMsg);
            if (!string.IsNullOrEmpty(errMsg))
            {
                WinFormLib.Core.MessageBoxHelper.ShowError(errMsg);
            }
        }
        private void SaveData()
        {
            if (m_DataTable != null)
            {
                lock (m_DataTable)
                {
                    string filePath = taskEntity.DownFilePath.Value;
                    if (!string.IsNullOrEmpty(filePath))
                    {
                        if (!System.IO.Directory.Exists(filePath))
                        {
                            System.IO.Directory.CreateDirectory(filePath);
                        }
                        filePath += @"\" + taskEntity.TaskTempName.Value + ".xml";
                        m_DataTable.TableName = taskEntity.TaskName.Value;
                        DataSet dataSet = new DataSet();
                        dataSet.Tables.Add(m_DataTable);
                        dataSet.WriteXml(filePath);
                        dataSet.Tables.Clear();
                    }
                }

            }
        }
        private int AutoNum = StaticConst.AutoSaveNum;
        public void Start()
        {
            if (cGaterWeb != null)
            {
                this.AddTab();
                this.tabTaskRunLog.SelectTab(taskEntity.TaskTempName.Value);
                if (taskEntity.GaterherState != EnumGloabParas.EnumThreadState.Run)
                {
                    ReadDataSource();
                    cGaterWeb.ThreadNum = taskEntity.ThreadNum;
                    cGaterWeb.GaterherState = taskEntity.GaterherState;//这里可能还是暂停状态                    
                    cGaterWeb.Start();
                    taskEntity.GaterherState = EnumGloabParas.EnumThreadState.Run;
                }
            }
        }
        private void ReadDataSource()
        {
            if (taskEntity.GaterherState == EnumGloabParas.EnumThreadState.Suspended)
            {
                string filePath = taskEntity.DownFilePath.Value;
                if (!string.IsNullOrEmpty(filePath))
                {
                    filePath += @"\" + taskEntity.TaskTempName.Value + ".xml";
                    if (System.IO.File.Exists(filePath))
                    {
                        DataSet dataSet = new DataSet();
                        dataSet.ReadXml(filePath);
                        m_DataTable = dataSet.Tables[0];
                        dgvRunTaskList.DataSource = m_DataTable;
                        dataSet.Tables.Clear();
                        dgvRunTaskList.FirstDisplayedScrollingRowIndex = dgvRunTaskList.RowCount - 1;
                    }
                }
            }
        }
        public void Stop()
        {
            if (cGaterWeb != null && taskEntity.GaterherState == EnumGloabParas.EnumThreadState.Run)
            {
                cGaterWeb.Stop();
            }
        }
        public void Reset(TaskRunItem _taskEntity)
        {
            this.cGaterWeb.LoadTask(taskEntity);
        }
        public void Reset()
        {
            if (cGaterWeb != null)
            {
                string taskFilePath = @"Run\" + taskEntity.TaskTempName + ".xml";
                taskEntity.TrueCount = 0;
                taskEntity.GatherUrlItemCompleteList = new List<cGatherUrlItem>();
                taskEntity.GatherUrlItemTempList = new List<cGatherUrlItem>();
                taskEntity.GatherFileItemCompleteList = new List<cGatherUrlBaseItem>();
                taskEntity.GatherFileItemTempList = new List<cGatherUrlBaseItem>();
                taskEntity.TrueCount = 0;
                taskEntity.TotalCount = 0;
                taskEntity.ErrorCount = 0;
                taskEntity.GaterherState = EnumGloabParas.EnumThreadState.Normal;
                this.cGaterWeb.LoadTask(taskEntity);
                string errMsg = string.Empty;
                XmlHelper.Save2File<TaskRunItem>(taskEntity, Program.GetConfigPath(taskFilePath), ref errMsg);
                if (!string.IsNullOrEmpty(errMsg))
                {
                    WinFormLib.Core.MessageBoxHelper.ShowError(errMsg);
                    return;
                }
                if (this.tabTaskRunLog.TabPages.ContainsKey(taskEntity.TaskTempName.Value))
                {
                    this.tabTaskRunLog.SelectTab(taskEntity.TaskTempName.Value);
                    dgvRunTaskList.Invoke(new MethodInvoker(delegate()
                    {
                        if (m_DataTable != null)
                        {
                            m_DataTable.Clear();
                            dgvRunTaskList.DataSource = m_DataTable;
                        }
                    }));
                    txtLog.Invoke(new MethodInvoker(delegate()
                    {
                        this.txtLog.Clear();
                    }));
                }
                
            }
        }
        public void Delete()
        {
            if (cGaterWeb != null && taskEntity != null && taskEntity.GaterherState == EnumGloabParas.EnumThreadState.Normal)
            {
                string errMsg = string.Empty;
                TaskRunTickItemList runItemList = XmlHelper.LoadFromXml<TaskRunTickItemList>(Program.GetConfigPath("run.xml"), ref errMsg);
                if (!string.IsNullOrEmpty(errMsg))
                {
                    WinFormLib.Core.MessageBoxHelper.ShowError(errMsg);
                }
                TaskRunTickItem item = runItemList.Where(q => q.TaskTempName.Value == taskEntity.TaskTempName.Value).FirstOrDefault();
                if (item != null)
                {
                    runItemList.Remove(item);
                    XmlHelper.Save2File<TaskRunTickItemList>(runItemList, Program.GetConfigPath("run.xml"), ref errMsg);
                    if (!string.IsNullOrEmpty(errMsg))
                    {
                        WinFormLib.Core.MessageBoxHelper.ShowError(errMsg);
                        return;
                    }
                    string taskFilePath = @"Run\" + taskEntity.TaskTempName + ".xml";
                    System.IO.File.Delete(Program.GetConfigPath(taskFilePath));

                    foreach (TabPage tabePage in this.tabTaskRunLog.TabPages)
                    {
                        if (tabePage.Name == taskEntity.TaskTempName.Value)
                        {
                            this.tabTaskRunLog.TabPages.Remove(tabePage);
                            break;
                        }
                    }
                    cGaterWeb = null;
                }
            }
        }
        private void AddTab(Int64 taskID, string taskName, string taskTempName)
        {
            if (tabTaskRunLog.TabPages.ContainsKey(taskTempName))
            {
                this.tabTaskRunLog.SelectedTab = tabTaskRunLog.TabPages[taskTempName];
                return;
            }
            TabPage tabPage = new TabPage();
            tabPage.Name = taskTempName;
            tabPage.Tag = taskTempName;
            tabPage.Text = taskName;

            SplitContainer sc = new SplitContainer();
            sc.Name = "sCon" + taskID.ToString();
            sc.Orientation = Orientation.Horizontal;
            sc.Dock = DockStyle.Fill;

            dgvRunTaskList = new DataGridView();
            dgvRunTaskList.AllowUserToAddRows = false;
            dgvRunTaskList.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            dgvRunTaskList.Dock = DockStyle.Fill;

            ContextMenuStrip menu = new ContextMenuStrip();
            menu.Items.Add("读取数据");
            menu.Items.Add("导出文本数据");
            menu.Items.Add("导出Excel");
            menu.ItemClicked += new ToolStripItemClickedEventHandler(delegate(object sender, ToolStripItemClickedEventArgs e)
            {
                if (e.ClickedItem.Text == "读取数据")
                {
                    if (taskEntity.GaterherState != EnumGloabParas.EnumThreadState.Run)
                    {
                        ReadDataSource();
                    }
                    return;
                }
                if (taskEntity.GaterherState != EnumGloabParas.EnumThreadState.Suspended)
                {
                    if (e_ExportData != null)
                    {
                        e_ExportData(e.ClickedItem.Text == "导出文本数据" ? 1 : 2, m_DataTable);
                    }
                }
                else
                {
                    MessageBoxHelper.ShowError("请先暂停任务后才能进导出！");
                }
            });
            dgvRunTaskList.ContextMenuStrip = menu;
            sc.Panel1.Controls.Add(dgvRunTaskList);
            txtLog = new RichTextBoxLog();
            txtLog.Name = "tLog" + taskID.ToString();
            txtLog.ReadOnly = true;
            txtLog.BorderStyle = BorderStyle.FixedSingle;
            txtLog.BackColor = Color.White;
            txtLog.DetectUrls = false;
            txtLog.WordWrap = false;
            txtLog.Dock = DockStyle.Fill;
            sc.Panel2.Controls.Add(txtLog);
            tabPage.Controls.Add(sc);
            tabTaskRunLog.TabPages.Add(tabPage);
            this.tabTaskRunLog.SelectedTab = tabPage;
        }

        public event OnExportData e_ExportData;

        public void OnChildTaskCompleted()
        {
            taskPlanCompleted++;
            if (taskPlanCompleted == taskEntity.TaskPlanItemList.Count)
            {
                if (taskEntity.TriggerType == EnumGloabParas.EnumTriggerType.GetData)
                {
                    if (e_OnGatherPublishCompleted != null)
                    {
                        e_OnGatherPublishCompleted(new cGatherPublishCompletedEventArgs()
                        {
                            DataSource = m_DataTable,
                            TaskEntity = taskEntity,
                        });
                    }
                }
                else
                {
                    if (e_GatherNotityCompleted != null)
                    {
                        e_GatherNotityCompleted(new cGatherPublishCompletedEventArgs()
                        {
                            DataSource = m_DataTable,
                            TaskEntity = taskEntity,
                        });
                    }
                }
            }
        }
        /// <summary>
        /// 所有任务结束事件
        /// </summary>
        public event OnGatherPublishCompleted e_GatherNotityCompleted;
        /// <summary>
        /// 任务发布事件
        /// </summary>
        public event OnGatherPublishCompleted e_OnGatherPublishCompleted;
        /// <summary>
        /// 任务计划子任务事件
        /// </summary>
        public event OnGatherPublishCompleted e_OnGatherRunPlanItemCompleted;
    }
}
