using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data;

namespace NETSpider.Entity
{
    public class TaskBasePlan
    {
        public Int64 TaskPlanID { get; set; }
        public CDataItem PlanName { get; set; }
    }

    public class TaskBasePlanList : List<TaskBasePlan>
    {

    }
    public class TaskPlan : TaskBasePlan
    {
        public static DataTable GetDataTable(List<TaskPlan> dataList)
        {
            DataTable dataTable = new DataTable() { };
            dataTable.Columns.Add("TaskPlanID");
            dataTable.Columns.Add("PlanName");
            dataTable.Columns.Add("StartDate");
            dataTable.Columns.Add("StartTime");
            dataTable.Columns.Add("PlanExcuteType");
            dataTable.Columns.Add("ExpireType");
            foreach (TaskPlan item in dataList)
            {
                DataRow dr = dataTable.NewRow();
                dr["TaskPlanID"] = item.TaskPlanID;
                dr["PlanName"] = item.PlanName.Value;
                dr["StartDate"] = item.StartDate.ToString("yyyy-MM-dd");
                dr["StartTime"] = item.StartTime.ToString("HH:mm:ss");
                dr["PlanExcuteType"] = EnumHelper.GetEnumDesc(typeof(EnumGloabParas.EnumPlanExcuteType), item.PlanExcuteType.ToString());
                dr["ExpireType"] = EnumHelper.GetEnumDesc(typeof(EnumGloabParas.EnumExpireType), item.ExpireType.ToString());
                dataTable.Rows.Add(dr);
            }
            return dataTable;
        }
        public static void InitDataGrid(System.Windows.Forms.DataGridView grid, string tagName)
        {
            grid.Tag = tagName;
            grid.Columns.Clear();
            DataGridViewTextBoxColumn colTaskPlanID = new DataGridViewTextBoxColumn();
            colTaskPlanID.HeaderText = "TaskPlanID";
            colTaskPlanID.Name = "TaskPlanID";
            colTaskPlanID.DataPropertyName = "TaskPlanID";
            grid.Columns.Add(colTaskPlanID);

            DataGridViewTextBoxColumn colPlanName = new DataGridViewTextBoxColumn();
            colPlanName.HeaderText = "计划名称";
            colPlanName.Name = "PlanName";
            colPlanName.DataPropertyName = "PlanName";
            colPlanName.Width = 400;
            grid.Columns.Add(colPlanName);
            DataGridViewTextBoxColumn colStartDate = new DataGridViewTextBoxColumn();
            colStartDate.HeaderText = "开始日期";
            colStartDate.Name = "StartDate";
            colStartDate.DataPropertyName = "StartDate";
            grid.Columns.Add(colStartDate);

            DataGridViewTextBoxColumn colStartTime = new DataGridViewTextBoxColumn();
            colStartTime.HeaderText = "开始时间";
            colStartTime.Name = "StartTime";
            colStartTime.DataPropertyName = "StartTime";
            grid.Columns.Add(colStartTime);

            DataGridViewTextBoxColumn colPlanExcuteType = new DataGridViewTextBoxColumn();
            colPlanExcuteType.HeaderText = "执行类型";
            colPlanExcuteType.Name = "PlanExcuteType";
            colPlanExcuteType.DataPropertyName = "PlanExcuteType";
            grid.Columns.Add(colPlanExcuteType);

        }
        public TaskPlan()
        {
            this.TaskPlanID = 0;
            this.PlanName = CDataItem.Instance("");
            this.Remark = CDataItem.Instance("");
            this.ExpireFlag = false;
            this.ExpireTime = StaticConst.DateMin;
            this.ExpireType = EnumGloabParas.EnumExpireType.None;
            this.ExpireCount = 0;
            this.PlanExcuteString = CDataItem.Instance("");
            this.PlanExcuteType = EnumGloabParas.EnumPlanExcuteType.Day;
            this.PlanItemList = new List<TaskPlanItem>();
            this.StartDate = DateTime.Now;
            this.StartTime = DateTime.Now;//默认执行时间
        }

        public CDataItem Remark { get; set; }
        public bool ExpireFlag { get; set; }
        public EnumGloabParas.EnumExpireType ExpireType { get; set; }
        public int ExpireCount { get; set; }
        public DateTime ExpireTime { get; set; }
        public EnumGloabParas.EnumPlanExcuteType PlanExcuteType { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime StartTime { get; set; }
        public CDataItem PlanExcuteString { get; set; }
        public List<TaskPlanItem> PlanItemList { get; set; }
    }

    public class TaskPlanItem
    {

        public static DataTable GetDataTable(List<TaskPlanItem> dataList)
        {
            DataTable dataTable = new DataTable() { };
            dataTable.Columns.Add("TaskPlanItemID");
            dataTable.Columns.Add("TaskID");
            dataTable.Columns.Add("CategroyName");
            dataTable.Columns.Add("TaskName");
            dataTable.Columns.Add("TaskArgs");
            dataTable.Columns.Add("TaskType");
            foreach (TaskPlanItem item in dataList)
            {
                DataRow dr = dataTable.NewRow();
                dr["TaskPlanItemID"] = item.TaskPlanItemID;
                dr["TaskID"] = item.TaskID;
                dr["CategroyName"] = item.CategroyName.Value;
                dr["TaskName"] = item.TaskName.Value;
                dr["TaskArgs"] = item.TaskArgs.Value;
                dr["TaskType"] = EnumHelper.GetEnumDesc(typeof(EnumGloabParas.EnumPlanTaskType), item.TaskType.ToString());
                dataTable.Rows.Add(dr);
            }
            return dataTable;
        }

        public int TaskPlanItemID { get; set; }

        public Int64 TaskID { get; set; }
        public EnumGloabParas.EnumPlanTaskType TaskType { get; set; }
        /// <summary>
        /// 1=存储过程,0 查询语句
        /// </summary>
        public int TaskItemType { get; set; }

        /// <summary>
        /// categroyName
        /// </summary>
        public CDataItem CategroyName { get; set; }
        /// <summary>
        /// name and connectionstring
        /// </summary>
        public CDataItem TaskName { get; set; }
        /// <summary>
        /// args and connection sql
        /// </summary>
        public CDataItem TaskArgs { get; set; }


    }
}
