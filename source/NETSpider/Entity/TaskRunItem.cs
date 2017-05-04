using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NETSpider.Gather;
using System.Data;
using System.Windows.Forms;
using NETSpider.Controls;

namespace NETSpider.Entity
{
    public class TaskRunItem : TaskItem
    {
        public static int GetProgessValue(int trueCount, int errCount, int totalCount)
        {
            int progress = trueCount + errCount;
            if (totalCount > 0)
            {
                decimal value = (progress * 100m) / (totalCount * 100);
                value *= 100;
                if (value > 0)
                {
                    string str = value.ToString();
                    if (str.IndexOf(".") != -1)
                    {
                        str = str.Substring(0, str.IndexOf("."));
                    }
                    int.TryParse(str, out progress);
                    return progress;
                }
            }
            return 0;
        }
        public static void InitRunDataGrid(System.Windows.Forms.DataGridView grid, string tagName)
        {
            #region MyRegion
            if (grid.InvokeRequired)
            {
                grid.Invoke(new MethodInvoker(delegate()
                {
                    InitRunDataGrid(grid, tagName);
                }));
                return;
            }
            grid.Tag = tagName;
            grid.Columns.Clear();
            DataGridViewTextBoxColumn colGaterherState = new DataGridViewTextBoxColumn();
            colGaterherState.HeaderText = "状态";
            colGaterherState.Name = "GaterherState";
            colGaterherState.DataPropertyName = "GaterherState";
            grid.Columns.Add(colGaterherState);


            DataGridViewTextBoxColumn colTaskName = new DataGridViewTextBoxColumn();
            colTaskName.HeaderText = "任务名称";
            colTaskName.Name = "TaskName";
            colTaskName.DataPropertyName = "TaskName";
            colTaskName.Width = 200;
            grid.Columns.Add(colTaskName);

            DataGridViewTextBoxColumn colTaskType = new DataGridViewTextBoxColumn();
            colTaskType.HeaderText = "任务类型";
            colTaskType.Name = "TaskType";
            colTaskType.DataPropertyName = "TaskType";
            grid.Columns.Add(colTaskType);

            DataGridViewTextBoxColumn colLoginFlag = new DataGridViewTextBoxColumn();
            colLoginFlag.HeaderText = "是否登录";
            colLoginFlag.DataPropertyName = "LoginFlag";
            colLoginFlag.Name = "LoginFlag";
            grid.Columns.Add(colLoginFlag);

            DataGridViewTextBoxColumn colTrueCount = new DataGridViewTextBoxColumn();
            colTrueCount.HeaderText = "完成页面";
            colTrueCount.DataPropertyName = "TrueCount";
            colTrueCount.Name = "TrueCount";
            grid.Columns.Add(colTrueCount);

            DataGridViewTextBoxColumn colErrorCount = new DataGridViewTextBoxColumn();
            colErrorCount.HeaderText = "错误数量";
            colErrorCount.DataPropertyName = "ErrorCount";
            colErrorCount.Name = "ErrorCount";
            grid.Columns.Add(colErrorCount);

            DataGridViewTextBoxColumn colTotalCount = new DataGridViewTextBoxColumn();
            colTotalCount.HeaderText = "采集页面";
            colTotalCount.DataPropertyName = "TotalCount";
            colTotalCount.Name = "TotalCount";
            grid.Columns.Add(colTotalCount);

            DataGridViewProgressBarColumn colProgessValue = new DataGridViewProgressBarColumn();
            colProgessValue.HeaderText = "当前进度";
            colProgessValue.Maximum = 100;
            colProgessValue.Mimimum = 0;
            colProgessValue.Width = 120;
            colProgessValue.DataPropertyName = "ProgessValue";
            colProgessValue.ValueType = typeof(int);
            colProgessValue.Name = "ProgessValue";
            grid.Columns.Add(colProgessValue);

            DataGridViewTextBoxColumn colExcuteType = new DataGridViewTextBoxColumn();
            colExcuteType.HeaderText = "执行类型";
            colExcuteType.DataPropertyName = "ExcuteType";
            colExcuteType.Name = "ExcuteType";
            grid.Columns.Add(colExcuteType);

            DataGridViewTextBoxColumn colThreadNum = new DataGridViewTextBoxColumn();
            colThreadNum.HeaderText = "线程数";
            colThreadNum.DataPropertyName = "ThreadNum";
            colThreadNum.Name = "ThreadNum";
            grid.Columns.Add(colThreadNum);

            DataGridViewTextBoxColumn colTaskTempName = new DataGridViewTextBoxColumn();
            colTaskTempName.HeaderText = "TaskTempName";
            colTaskTempName.DataPropertyName = "TaskTempName";
            colTaskTempName.Name = "TaskTempName";
            colTaskTempName.Visible = false;
            grid.Columns.Add(colTaskTempName);
            #endregion
        }


        public static DataTable GetRunDataTable(List<TaskRunItem> dataList)
        {
            DataTable dataTable = new DataTable() { };
            dataTable.Columns.Add("GaterherState");
            dataTable.Columns.Add("TaskName");
            dataTable.Columns.Add("TaskType");
            dataTable.Columns.Add("LoginFlag");
            dataTable.Columns.Add("TrueCount");
            dataTable.Columns.Add("ErrorCount");
            dataTable.Columns.Add("TotalCount");
            dataTable.Columns.Add("ProgessValue", typeof(int));
            dataTable.Columns.Add("ExcuteType");
            dataTable.Columns.Add("ThreadNum");
            dataTable.Columns.Add("TaskTempName");
            foreach (TaskRunItem item in dataList)
            {
                DataRow dr = dataTable.NewRow();
                dr["GaterherState"] = item.GaterherState.ToString();
                dr["TaskName"] = item.TaskName.Value;
                dr["TaskType"] = EnumHelper.GetEnumDesc(typeof(EnumGloabParas.EnumTaskType), item.TaskType.ToString());
                dr["LoginFlag"] = item.LoginFlag ? "是" : "否";
                dr["TrueCount"] = item.TrueCount;
                dr["ErrorCount"] = item.ErrorCount;
                dr["TotalCount"] = item.TotalCount;
                dr["ProgessValue"] = TaskRunItem.GetProgessValue(item.TrueCount, item.ErrorCount, item.TotalCount);
                dr["ExcuteType"] = EnumHelper.GetEnumDesc(typeof(EnumGloabParas.EnumExcuteType), item.ExcuteType.ToString());//
                dr["ThreadNum"] = item.ThreadNum;
                dr["TaskTempName"] = item.TaskTempName.Value;
                dataTable.Rows.Add(dr);
            }
            return dataTable;
        }
        public void Load(string taskTempName)
        {
            this.TaskTempName = taskTempName;
            this.TrueCount = this.ErrorCount = this.TotalCount = 0;
            this.GaterherState = EnumGloabParas.EnumThreadState.Normal;
            this.GatherUrlItemCompleteList = new List<cGatherUrlItem>();
            this.GatherUrlItemTempList = new List<cGatherUrlItem>();
        }
        public CDataItem TaskTempName { get; set; }
        public int TrueCount { get; set; }
        public int ErrorCount { get; set; }
        public int TotalCount { get; set; }
        public CDataItem ParentTaskTempName { get; set; }
        public EnumGloabParas.EnumThreadState GaterherState { get; set; }
        public List<cGatherUrlItem> GatherUrlItemCompleteList { get; set; }
        public List<cGatherUrlItem> GatherUrlItemTempList { get; set; }
        public List<cGatherUrlBaseItem> GatherFileItemCompleteList { get; set; }
        public List<cGatherUrlBaseItem> GatherFileItemTempList { get; set; }

    }
    public class TaskRunTickItem
    {
        public CDataItem TaskTempName { get; set; }
        public Int64 TaskID { get; set; }//ID
        public CDataItem CategroyName { get; set; }//分类名称
        public CDataItem TaskName { get; set; }//任务名称    
    }
}
