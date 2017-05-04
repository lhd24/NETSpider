using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NETSpider.Gather;
using System.Threading;
using WinFormLib.Core;
using NETSpider.Entity;

namespace NETSpider
{
    public partial class frmViewData : Form
    {
        private string TaskTempName { get; set; }
        public frmViewData(string downFilePath, string taskTempName)
        {
            InitializeComponent();
            string filePath = downFilePath;
            this.TaskTempName = taskTempName;
            if (!string.IsNullOrEmpty(filePath))
            {
                filePath += @"\" + taskTempName + ".xml";
                if (System.IO.File.Exists(filePath))
                {
                    DataSet dataSet = new DataSet();
                    dataSet.ReadXml(filePath);
                    m_DataTable = dataSet.Tables[0];
                    dgvList.DataSource = m_DataTable;
                    dataSet.Tables.Clear();
                    toolStripStatusLabel1.Text = "当前数据(" + m_DataTable.Rows.Count + ")：";
                }
            }
            exportFile.e_ExportStartingEvent += new ExportStartingEvent(exportFile_e_ExportStartingEvent);
            exportFile.e_ExportProgressingEvent += new ExportProgressingEvent(exportFile_e_ExportProgressingEvent);
            exportFile.e_ExportEndedEvent += new ExportEndedEvent(exportFile_e_ExportEndedEvent);
            toolStripProgressBar1.Minimum = 0;
            toolStripProgressBar1.Maximum = 100;
            tsPopDouble.Visible = false;
        }

        public frmViewData(string taskTempName)
        {
            InitializeComponent();
            string errMsg = string.Empty;
            string taskFilePath = @"Run\" + taskTempName + ".xml";
            TaskRunItem taskEntity = XmlHelper.LoadFromXml<TaskRunItem>(Program.GetConfigPath(taskFilePath), ref errMsg);
            if (!string.IsNullOrEmpty(errMsg))
            {
                WinFormLib.Core.MessageBoxHelper.ShowError(errMsg);
                return;
            }
            this.TaskTempName = taskTempName;
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("Url");
            dataTable.Columns.Add("GaterherFlag");
            foreach (var item in taskEntity.GatherUrlItemCompleteList)
            {
                DataRow dr = dataTable.NewRow();
                dr["Url"] = item.Url;
                dr["GaterherFlag"] = item.GaterherFlag.ToString();
                dataTable.Rows.Add(dr);
            }
            dgvList.DataSource = dataTable;
            int totalCount = taskEntity.GatherUrlItemCompleteList.Count;
            int errCount = taskEntity.GatherUrlItemCompleteList.Where(q => q.GaterherFlag == EnumGloabParas.EnumUrlGaterherState.Error).Count();
            toolStripStatusLabel1.Text = "当前数据(" + errCount + "/" + totalCount + ")：";
            tsExportExcel.Visible = false;
            tsExportTxt.Visible = false;
        }
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
                    exportFile.ThreadState = EnumGloabParas.EnumThreadState.SpiderCompleted;
                    toolStripProgressBar1.Value = 0;
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
                    exportFile.ThreadState = EnumGloabParas.EnumThreadState.Run;
                    toolStripProgressBar1.Value = 0;
                }));
            }
        }
        private DataTable2File exportFile = new DataTable2File();
        private Thread exportFileThread = null;
        private DataTable m_DataTable = null;
        private void cnTask_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            string text = e.ClickedItem.Text;
            if (text == "去除重复数据")
            {
                string taskFilePath = @"Run\" + this.TaskTempName + ".xml";
                string errMsg = string.Empty;
                TaskRunItem taskEntity = XmlHelper.LoadFromXml<TaskRunItem>(Program.GetConfigPath(taskFilePath), ref errMsg);
                if (!string.IsNullOrEmpty(errMsg))
                {
                    WinFormLib.Core.MessageBoxHelper.ShowError(errMsg);
                    return;
                }
                taskEntity.GatherUrlItemCompleteList = taskEntity.GatherUrlItemCompleteList.Distinct(new cGatherUrlItemComparer()).ToList();

                XmlHelper.Save2File<TaskRunItem>(taskEntity,Program.GetConfigPath(taskFilePath), ref errMsg);
                if (!string.IsNullOrEmpty(errMsg))
                {
                    WinFormLib.Core.MessageBoxHelper.ShowError(errMsg);
                    return;
                }
                MessageBoxHelper.Show("去除重复数据成功,请重新打开运行记录!");
                return;
            }
            if (exportFile.ThreadState == EnumGloabParas.EnumThreadState.Run)
            {
                MessageBoxHelper.ShowError("file is exporting");
                return;
            }
            if (m_DataTable != null)
            {
                int dataType = e.ClickedItem.Text == "导出文本数据" ? 1 : 2;
                if (exportFileThread != null)
                {
                    exportFileThread.Abort();
                    exportFileThread = null;
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
                else
                {
                    return;
                }
                DialogResult result = saveFileDialog1.ShowDialog();
                if (result == System.Windows.Forms.DialogResult.OK)
                {
                    exportFile.m_DataTable = m_DataTable;
                    exportFile.ConnectionString = saveFileDialog1.FileName;
                    if (dataType == 1)
                    {
                        exportFileThread = new Thread(new ThreadStart(exportFile.ExportToTxt));
                        exportFileThread.Start();
                    }
                    else
                    {

                        exportFileThread = new Thread(new ThreadStart(exportFile.ExportToExcel));
                        exportFileThread.Start();
                    }
                }
            }
        }
    }
}
