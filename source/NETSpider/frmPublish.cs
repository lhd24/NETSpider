using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NETSpider.Entity;
using WinFormLib.Core;

namespace NETSpider
{
    public partial class frmPublish : Form
    {
        private TaskRunItem TaskEntity = null;
        private DataTable DataSource = null;
        private EnumGloabParas.EnumThreadState threadState = EnumGloabParas.EnumThreadState.Normal;
        private IDataTablePublish dataPublish = null;
        ~frmPublish()
        {
        }
        public frmPublish(TaskRunItem _taskEntity, DataTable _dataSource)
        {
            InitializeComponent();
            this.TaskEntity = _taskEntity;
            this.DataSource = _dataSource;
            this.pubProgressBar.Minimum = 0;
            this.pubProgressBar.Maximum = 100;
            this.SetStyle(ControlStyles.DoubleBuffer | ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint, true);
            this.UpdateStyles();
        }

        private void frmPublish_Load(object sender, EventArgs e)
        {
            pubProgressBar.Maximum = 100;
            pubProgressBar.Minimum = 0;
            pubProgressBar.Value = 0;
            this.threadState = EnumGloabParas.EnumThreadState.Run;
            LoadTask();
            //timer1.Start();
        }
        private void LoadTask()
        {
            if (this.DataSource == null || this.TaskEntity == null)
            {
                this.threadState = EnumGloabParas.EnumThreadState.SpiderCompleted;
                this.label1.Text = "数据为空,可以已经删除.";
                return;
            }
            string connectionString = this.TaskEntity.ConnectionString.Value;
            if (string.IsNullOrEmpty(connectionString))
            {
                this.label1.Text = "发布文件为空,请重新设置采集任务.";
                this.threadState = EnumGloabParas.EnumThreadState.SpiderCompleted;
                return;
            }
            if (this.TaskEntity.ConnectionType == EnumGloabParas.EnumConnectionType.ExportTxt || this.TaskEntity.ConnectionType == EnumGloabParas.EnumConnectionType.ExportExcel)
            {
                #region MyRegion
                dataPublish = new DataTable2File();
                dataPublish.m_DataTable = this.DataSource;
                dataPublish.ConnectionString = connectionString;
                dataPublish.CommandText = "";
                dataPublish.ConnectionType = this.TaskEntity.ConnectionType;
                dataPublish.e_ExportStartingEvent += new Gather.ExportStartingEvent(data2File_e_ExportStartingEvent);
                dataPublish.e_ExportProgressingEvent += new Gather.ExportProgressingEvent(data2File_e_ExportProgressingEvent);
                dataPublish.e_ExportEndedEvent += new Gather.ExportEndedEvent(data2File_e_ExportEndedEvent);
                dataPublish.Start();
                #endregion
            }
            else if (this.TaskEntity.ConnectionType == EnumGloabParas.EnumConnectionType.ExportMSSQL || this.TaskEntity.ConnectionType == EnumGloabParas.EnumConnectionType.ExportMySql || this.TaskEntity.ConnectionType == EnumGloabParas.EnumConnectionType.ExportAccess)
            {
                dataPublish = new DataTable2Data();
                dataPublish.m_DataTable = this.DataSource;
                dataPublish.ConnectionString = connectionString;
                dataPublish.CommandText = this.TaskEntity.PubSql.Value;
                dataPublish.ConnectionType = this.TaskEntity.ConnectionType;
                dataPublish.e_ExportStartingEvent += new Gather.ExportStartingEvent(data2Data_e_ExportStartingEvent);
                dataPublish.e_ExportProgressingEvent += new Gather.ExportProgressingEvent(data2Data_e_ExportProgressingEvent);
                dataPublish.e_ExportEndedEvent += new Gather.ExportEndedEvent(data2Data_e_ExportEndedEvent);
                dataPublish.Start();
            }
            else
            {
                System.Threading.Thread.Sleep(2000);
                this.Close();
            }
        }

        void data2Data_e_ExportEndedEvent(object sender, Gather.cExportEndedEventArgs e)
        {
            this.threadState = EnumGloabParas.EnumThreadState.SpiderCompleted;
            if (this.InvokeRequired)
            {
                this.Invoke(new MethodInvoker(delegate()
                {
                    this.Close();
                }));
            }
            else
            {
                this.Close();
            }
        }

        void data2Data_e_ExportProgressingEvent(object sender, Gather.ExportProgressingArgs e)
        {
            SetProgressValue(e.ExportPercent);
        }

        void data2Data_e_ExportStartingEvent(object sender, Gather.cExportStartEventArgs e)
        {
            SetProgressValue(0);
        }
        private delegate void ReportProcessInfo(int iPercent);
        private void SetProgressValue(int iPercent)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new ReportProcessInfo(SetProgressValue), iPercent);
            }
            else
            {
                this.pubProgressBar.Value = iPercent;
            }

        }

        void data2File_e_ExportEndedEvent(object sender, Gather.cExportEndedEventArgs e)
        {
            this.threadState = EnumGloabParas.EnumThreadState.SpiderCompleted;
            if (this.InvokeRequired)
            {
                this.Invoke(new MethodInvoker(delegate()
                {
                    this.Close();
                }));
            }
            else
            {
                this.Close();
            }
        }
        void data2File_e_ExportProgressingEvent(object sender, Gather.ExportProgressingArgs e)
        {
            SetProgressValue(e.ExportPercent);
        }

        void data2File_e_ExportStartingEvent(object sender, Gather.cExportStartEventArgs e)
        {
            SetProgressValue(0);
        }

        private void frmPublish_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (threadState == EnumGloabParas.EnumThreadState.Run)
            {
                DialogResult result = MessageBoxHelper.ShowQuestion("正在运行导入,确定关闭吗?");
                if (result != DialogResult.OK)
                {
                    e.Cancel = true;
                    return;
                }
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
