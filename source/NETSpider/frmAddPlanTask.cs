using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WinFormLib.Core;
using NETSpider.Controls;
using NETSpider.Entity;

namespace NETSpider
{
    public partial class frmAddPlanTask : Form
    {
        public frmAddPlanTask()
        {
            InitializeComponent();
            ShowTaskType(this, new EventArgs());
        }

        private void button12_Click(object sender, EventArgs e)
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
            List<string> items = ConectionTables.GetProcedures(connectionType, ConnectionString);
            foreach (string item in items)
            {
                cbPubDataTable.Items.Add(item);
            }
            if (cbPubDataTable.Items.Count > 0)
                cbPubDataTable.SelectedIndex = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog1.FileName = "";
            openFileDialog1.Filter = "*.*|*.*";
            openFileDialog1.InitialDirectory = Program.GetProgramPath("");
            openFileDialog1.Title = "选择可执行程序";
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                lbFileName.DataViewValue = openFileDialog1.FileName;
            }
        }
        private void ShowTaskType(object sender, EventArgs e)
        {
            if (raSpiderTask.Checked)
            {
                pnTask.Visible = true;
                pnlDataBase.Visible = false;
                pnlPrograms.Visible = false;
            }
            else if (raDataTask.Checked)
            {
                pnTask.Visible = false;
                pnlDataBase.Visible = true;
                pnlPrograms.Visible = false;
            }
            else if (raOtherTask.Checked)
            {
                pnTask.Visible = false;
                pnlDataBase.Visible = false;
                pnlPrograms.Visible = true;
            }
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {

            TaskPlanItem entity = new TaskPlanItem() { TaskItemType = 0 };
            if (raSpiderTask.Checked)
            {
                if (dgvTask.SelectedRows.Count > 0)
                {
                    entity.CategroyName = comCategorys.SelectedItem.ToString();
                    entity.TaskID = TryParse.StrToInt(dgvTask[colTaskID.Index, dgvTask.SelectedRows[0].Index].Value);
                    entity.TaskName = CDataItem.Instance(dgvTask[colTaskName.Index, dgvTask.SelectedRows[0].Index].Value.ToString());
                    entity.TaskType = EnumGloabParas.EnumPlanTaskType.Task;
                    entity.TaskArgs = "";
                }
                else
                {
                    WinFormLib.Core.MessageBoxHelper.ShowError("请选择执行任务!");
                    return;
                }
            }
            else if (raDataTask.Checked)
            {
                if (cbPubDataTable.SelectedItem != null || string.IsNullOrEmpty(cbPubDataTable.Text))
                {
                    entity.CategroyName = CDataItem.Instance("");
                    entity.TaskID = 0;
                    entity.TaskName = string.IsNullOrEmpty(cbPubDataTable.Text) ? cbPubDataTable.SelectedItem.ToString() : cbPubDataTable.Text;
                    entity.TaskType = EnumGloabParas.EnumPlanTaskType.DataBase;
                    entity.TaskArgs = lbConnectionString.Value;
                    entity.TaskItemType = cbPubDataTable.SelectedItem != null ? 1 : 0;
                }
                else
                {
                    WinFormLib.Core.MessageBoxHelper.ShowError("请设置查询或存储过程");
                    return;
                }
            }
            else if (raOtherTask.Checked)
            {

                entity.CategroyName = CDataItem.Instance("");
                entity.TaskID = 0;
                entity.TaskName = lbFileName.Value;
                entity.TaskType = EnumGloabParas.EnumPlanTaskType.Custom;
                entity.TaskArgs = lbFileArgs.Value;
                if (string.IsNullOrEmpty(entity.TaskName.Value))
                {
                    WinFormLib.Core.MessageBoxHelper.ShowError("请设置执行程序路径");
                    return;
                }
            }
            this.DialogResult = DialogResult.OK;
            if (e_OnReturnTaskPlanItem != null)
            {
                e_OnReturnTaskPlanItem(entity);
            }
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
        private TaskList taskList = new TaskList();
        private void frmAddPlanTask_Load(object sender, EventArgs e)
        {
            string errMsg = string.Empty;
            TaskCategroyList _taskCategroyList = XmlHelper.LoadFromXml<TaskCategroyList>(Program.GetConfigPath("category.xml"), ref errMsg);
            if (!string.IsNullOrEmpty(errMsg))
            {
                WinFormLib.Core.MessageBoxHelper.ShowError(errMsg);
                return;
            }
            comCategorys.Items.Clear();
            foreach (var item in _taskCategroyList)
            {
                comCategorys.Items.Add(item.CategroyName.Value);
                foreach (var category in item.TaskItemList)
                {
                    string taskFilePath = @"categroy\" + category.CategroyName.Value + @"\" + category.TaskName.Value + ".xml";
                    TaskItem entity = XmlHelper.LoadFromXml<TaskItem>(Program.GetConfigPath(taskFilePath), ref errMsg);
                    if (!string.IsNullOrEmpty(errMsg))
                    {
                        WinFormLib.Core.MessageBoxHelper.ShowError(errMsg);
                        return;
                    }
                    taskList.Add(entity);
                }
            }
            if (comCategorys.Items.Count > 0)
            {
                comCategorys.SelectedIndex = 0;
            }
        }

        private void comCategorys_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (taskList != null)
            {
                List<TaskItem> resultList = taskList.Where(q => q.CategroyName.Value == comCategorys.SelectedItem.ToString()).ToList();
                this.dgvTask.RemoveBind();
                this.dgvTask.ReBind(TaskItem.GetDataTable(resultList), false);
                this.dgvTask.ClearSelection();
            }
        }

        public event OnReturnTaskPlanItem e_OnReturnTaskPlanItem;

        private void dgvTask_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            btnSubmit_Click(sender, e);
        }
    }
}
