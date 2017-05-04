using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NETSpider.Entity;

namespace NETSpider
{
    public partial class frmPlan : Form
    {
        public frmPlan()
            : this("")
        {
        }

        private TaskPlan taskPlanEntity;


        public frmPlan(string planName)
        {
            InitializeComponent();
            string errMsg = string.Empty;
            string dirPath = Program.GetConfigPath(@"plan");
            if (!System.IO.Directory.Exists(dirPath))
            {
                System.IO.Directory.CreateDirectory(dirPath);
            }
            if (!string.IsNullOrEmpty(planName))
            {
                string filePath = Program.GetConfigPath(@"plan\" + planName + ".xml");
                taskPlanEntity = XmlHelper.LoadFromXml<TaskPlan>(filePath, ref errMsg);
                if (!string.IsNullOrEmpty(errMsg))
                {
                    WinFormLib.Core.MessageBoxHelper.ShowError(errMsg);
                }
            }
            else
            {
                taskPlanEntity = new TaskPlan();
            }
            lbTaskPlanName.DataViewValue = taskPlanEntity.PlanName.Value;
            lbRemark.DataViewValue = taskPlanEntity.Remark.Value;
            if (taskPlanEntity.ExpireFlag)
            {
                chkExpireFlag.Checked = true;
                if (taskPlanEntity.ExpireType == EnumGloabParas.EnumExpireType.Count)
                {
                    raNumber.Checked = true;
                    numExpireCount.Value = taskPlanEntity.ExpireCount;
                }
                else
                {
                    raDateTime.Checked = true;
                    dateExpireTime.Value = taskPlanEntity.ExpireTime;
                }
            }
            else
            {
                chkExpireFlag.Checked = false;
            }
            dateStartDate.Value = taskPlanEntity.StartDate;
            if (taskPlanEntity.PlanExcuteType == EnumGloabParas.EnumPlanExcuteType.One)
            {
                raOneTime.Checked = true;
                oneStartTime.Value = taskPlanEntity.StartTime;
            }
            else if (taskPlanEntity.PlanExcuteType == EnumGloabParas.EnumPlanExcuteType.Day)
            {
                raDay.Checked = true;
                dayStartTime.Value = taskPlanEntity.StartTime;
                string[] str = taskPlanEntity.PlanExcuteString.Value.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                if (str.Length == 2)
                {
                    DateTime[] dateTime = Array.ConvertAll<string, DateTime>(str, item => DMSFrame.TryParse.StrToDate(item));
                    dayStartTime1.Value = dateTime[0];
                    dayStartTime2.Value = dateTime[1];
                }
            }
            else if (taskPlanEntity.PlanExcuteType == EnumGloabParas.EnumPlanExcuteType.Weekly)
            {
                raWeekly.Checked = true;
                weeklyStartTime.Value = taskPlanEntity.StartTime;
                string[] str = taskPlanEntity.PlanExcuteString.Value.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                if (str.Length == 7)
                {
                    int index = 0;
                    foreach (var item in str)
                    {
                        CheckBox chkWeekly = pnlWeekly.Controls["chkWeekly" + index] as CheckBox;
                        if (chkWeekly != null)
                        {
                            if (item == "1")
                            {
                                chkWeekly.Checked = true;
                            }
                            else
                            {
                                chkWeekly.Checked = false;
                            }
                        }
                        index++;
                    }
                }
            }
            else if (taskPlanEntity.PlanExcuteType == EnumGloabParas.EnumPlanExcuteType.Custom)
            {
                raCustom.Checked = true;
                cusStartTime.Value = taskPlanEntity.StartTime;
                cusInterval.Value = DMSFrame.TryParse.StrToDecimal(taskPlanEntity.PlanExcuteString);
            }
            ShowExcuteTypePanel();
            taskPlanItemList = taskPlanEntity.PlanItemList;
            if (taskPlanItemList.Count > 0)
            {
                //this.dgvTaskPlan.RemoveBind();
                this.dgvTaskPlan.ReBind(TaskPlanItem.GetDataTable(taskPlanItemList), false);
                this.dgvTaskPlan.ClearSelection();
            }
        }
        #region  ShowExcuteTypePanel
        private void ShowExcuteTypePanel()
        {
            pnlOne.Visible = false;
            pnlWeekly.Visible = false;
            pnlCustom.Visible = false;
            pnlDay.Visible = false;
            if (raOneTime.Checked)
            {
                pnlOne.Visible = true;

            }
            else if (raDay.Checked)
            {
                pnlDay.Visible = true;

            }
            else if (raWeekly.Checked)
            {
                pnlWeekly.Visible = true;
            }
            else if (raCustom.Checked)
            {
                pnlCustom.Visible = true;
            }
        }
        private void raOneTime_CheckedChanged(object sender, EventArgs e)
        {
            ShowExcuteTypePanel();
        }

        private void raDay_CheckedChanged(object sender, EventArgs e)
        {
            ShowExcuteTypePanel();
        }

        private void raWeekly_CheckedChanged(object sender, EventArgs e)
        {
            ShowExcuteTypePanel();
        }

        private void raCustom_CheckedChanged(object sender, EventArgs e)
        {
            ShowExcuteTypePanel();
        }
        #endregion
        private void btnApply_Click(object sender, EventArgs e)
        {
            SaveTaskPlan();
        }
        private void btnSubmit_Click(object sender, EventArgs e)
        {
            if (SaveTaskPlan())
            {
                this.Close();
            }
        }

        private bool SaveTaskPlan()
        {
            taskPlanEntity.PlanName = CDataItem.Instance(lbTaskPlanName.Value);
            if (string.IsNullOrEmpty(taskPlanEntity.PlanName.Value))
            {
                WinFormLib.Core.MessageBoxHelper.ShowError("任务名称不能为空!");
                return false;
            }
            taskPlanEntity.PlanItemList = taskPlanItemList;
            if (taskPlanEntity.PlanItemList.Count == 0)
            {
                if (WinFormLib.Core.MessageBoxHelper.ShowQuestion("没有任何任务信息!") != System.Windows.Forms.DialogResult.OK)
                {
                    return false;
                }
            }
            taskPlanEntity.ExpireCount = 1;
            taskPlanEntity.ExpireFlag = chkExpireFlag.Checked;
            taskPlanEntity.ExpireTime = DateTime.MinValue;
            taskPlanEntity.ExpireType = EnumGloabParas.EnumExpireType.None;
            if (taskPlanEntity.ExpireFlag)
            {
                if (raNumber.Checked)
                {
                    taskPlanEntity.ExpireType = EnumGloabParas.EnumExpireType.Count;
                    taskPlanEntity.ExpireCount = DMSFrame.TryParse.StrToInt(numExpireCount.Value);
                }
                else
                {
                    taskPlanEntity.ExpireType = EnumGloabParas.EnumExpireType.Time;
                    taskPlanEntity.ExpireTime = dateExpireTime.Value;
                }
            }
            taskPlanEntity.PlanExcuteString = CDataItem.Instance("");
            taskPlanEntity.StartDate = dateStartDate.Value;
            if (raOneTime.Checked)
            {
                taskPlanEntity.PlanExcuteType = EnumGloabParas.EnumPlanExcuteType.One;
                taskPlanEntity.StartTime = oneStartTime.Value;
            }
            else if (raDay.Checked)
            {
                taskPlanEntity.PlanExcuteType = EnumGloabParas.EnumPlanExcuteType.Day;
                taskPlanEntity.StartTime = dayStartTime.Value;
                taskPlanEntity.PlanExcuteString = dayStartTime1.Value.ToString() + "," + dayStartTime2.Value.ToString();
            }
            else if (raWeekly.Checked)
            {
                taskPlanEntity.PlanExcuteType = EnumGloabParas.EnumPlanExcuteType.Weekly;
                taskPlanEntity.StartTime = weeklyStartTime.Value;
                string str = "";
                for (int index = 0; index < 7; index++)
                {
                    CheckBox chkWeekly = pnlWeekly.Controls["chkWeekly" + index] as CheckBox;
                    if (chkWeekly != null)
                    {
                        str += chkWeekly.Checked ? ",1" : ",0";
                    }
                }
                taskPlanEntity.PlanExcuteString = str;
            }
            else if (raCustom.Checked)
            {
                taskPlanEntity.PlanExcuteType = EnumGloabParas.EnumPlanExcuteType.One;
                taskPlanEntity.StartTime = cusStartTime.Value;
                taskPlanEntity.PlanExcuteString = CDataItem.Instance(cusInterval.Value.ToString());
            }
            taskPlanEntity.Remark = CDataItem.Instance(lbRemark.Value);


            string errMsg = string.Empty;
            string filePath = Program.GetConfigPath(@"plan\" + taskPlanEntity.PlanName.Value + ".xml");
            TaskBasePlanList planList = XmlHelper.LoadFromXml<TaskBasePlanList>(Program.GetConfigPath("plan.xml"), ref errMsg);
            if (!string.IsNullOrEmpty(errMsg))
            {
                WinFormLib.Core.MessageBoxHelper.ShowError(errMsg);
                return false;
            }
            if (taskPlanEntity.TaskPlanID == 0)
            {
                if (planList.Count > 0)
                {
                    taskPlanEntity.TaskPlanID = planList.Select(q => q.TaskPlanID).Max() + 1;
                }
                else
                {
                    taskPlanEntity.TaskPlanID = 1;
                }
                planList.Add(new TaskBasePlan() { TaskPlanID = taskPlanEntity.TaskPlanID, PlanName = taskPlanEntity.PlanName });
                XmlHelper.Save2File<TaskBasePlanList>(planList, Program.GetConfigPath("plan.xml"), ref errMsg);
                if (!string.IsNullOrEmpty(errMsg))
                {
                    WinFormLib.Core.MessageBoxHelper.ShowError(errMsg);
                    return false;
                }
            }

            XmlHelper.Save2File<TaskPlan>(taskPlanEntity, filePath, ref errMsg);
            if (!string.IsNullOrEmpty(errMsg))
            {
                WinFormLib.Core.MessageBoxHelper.ShowError(errMsg);
                return false;
            }
            WinFormLib.Core.MessageBoxHelper.Show("保存成功!");
            return true;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void chkExpireFlag_CheckedChanged(object sender, EventArgs e)
        {
            groupBox4.Enabled = false;
            groupBox5.Enabled = false;
            if (chkExpireFlag.Checked)
            {
                raDateTime.Enabled = raNumber.Enabled = true;
                if (raNumber.Checked)
                {
                    groupBox4.Enabled = true;
                }
                else
                {
                    groupBox5.Enabled = true;
                }
            }
            else
            {
                raDateTime.Enabled = raNumber.Enabled = false;
            }
        }

        private void raDateTime_CheckedChanged(object sender, EventArgs e)
        {
            if (raDateTime.Checked)
            {
                groupBox4.Enabled = false;
                groupBox5.Enabled = true;
            }
            else
            {
                groupBox4.Enabled = true;
                groupBox5.Enabled = false;
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            frmAddPlanTask frmAddPlan = new frmAddPlanTask();
            frmAddPlan.e_OnReturnTaskPlanItem += new NETSpider.Controls.OnReturnTaskPlanItem(frmAddPlan_e_OnReturnTaskPlanItem);
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

        private void btnRemove_Click(object sender, EventArgs e)
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
    }
}
