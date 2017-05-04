using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WinFormLib.Components;
using System.Data;

namespace NETSpider.Entity
{
    [Serializable]
    public class TaskItem : TaskItemBase
    {
        public static void InitDataGrid(System.Windows.Forms.DataGridView grid, string tagName)
        {
            grid.Tag = tagName;
            grid.Columns.Clear();
            DataGridViewTextBoxColumn colTaskID = new DataGridViewTextBoxColumn();
            colTaskID.HeaderText = "TaskID";
            colTaskID.Name = "TaskID";
            colTaskID.DataPropertyName = "TaskID";
            grid.Columns.Add(colTaskID);

            DataGridViewTextBoxColumn colCategroyName = new DataGridViewTextBoxColumn();
            colCategroyName.HeaderText = "分类";
            colCategroyName.Name = "CategroyName";
            colCategroyName.DataPropertyName = "CategroyName";
            grid.Columns.Add(colCategroyName);

            DataGridViewTextBoxColumn colTaskName = new DataGridViewTextBoxColumn();
            colTaskName.HeaderText = "名称";
            colTaskName.Name = "TaskName";
            colTaskName.DataPropertyName = "TaskName";
            colTaskName.Width = 200;
            grid.Columns.Add(colTaskName);

            DataGridViewTextBoxColumn colTaskType = new DataGridViewTextBoxColumn();
            colTaskType.HeaderText = "任务类型";
            colTaskType.Name = "TaskType";
            colTaskType.DataPropertyName = "TaskType";
            grid.Columns.Add(colTaskType);

            DataGridViewTextBoxColumn colExcuteType = new DataGridViewTextBoxColumn();
            colExcuteType.HeaderText = "执行类型";
            colExcuteType.Name = "ExcuteType";
            colExcuteType.DataPropertyName = "ExcuteType";
            grid.Columns.Add(colExcuteType);

            DataGridViewTextBoxColumn colUrlCount = new DataGridViewTextBoxColumn();
            colUrlCount.HeaderText = "网址数量";
            colUrlCount.Name = "UrlCount";
            colUrlCount.DataPropertyName = "UrlCount";
            grid.Columns.Add(colUrlCount);

            DataGridViewTextBoxColumn colLoginFlag = new DataGridViewTextBoxColumn();
            colLoginFlag.HeaderText = "是否需要登录";
            colLoginFlag.Name = "LoginFlag";
            colLoginFlag.DataPropertyName = "LoginFlag";
            grid.Columns.Add(colLoginFlag);
        }

        public static DataTable GetDataTable(List<TaskItem> dataList)
        {
            DataTable dataTable = new DataTable() { };
            dataTable.Columns.Add("TaskID");
            dataTable.Columns.Add("CategroyName");
            dataTable.Columns.Add("TaskName");
            dataTable.Columns.Add("TaskType");
            dataTable.Columns.Add("ExcuteType");
            dataTable.Columns.Add("UrlCount");
            dataTable.Columns.Add("LoginFlag");
            foreach (TaskItem item in dataList)
            {
                DataRow dr = dataTable.NewRow();
                dr["TaskID"] = item.TaskID;
                dr["CategroyName"] = item.CategroyName.Value;
                dr["TaskName"] = item.TaskName;
                dr["TaskType"] = EnumHelper.GetEnumDesc(typeof(EnumGloabParas.EnumTaskType), item.TaskType.ToString());
                dr["ExcuteType"] = EnumHelper.GetEnumDesc(typeof(EnumGloabParas.EnumExcuteType), item.ExcuteType.ToString());
                dr["UrlCount"] = item.UrlCount;
                dr["LoginFlag"] = item.LoginFlag ? "是" : "否";
                dataTable.Rows.Add(dr);
            }
            return dataTable;
        }
        public TaskItem()
        {
            this.TaskID = 0;
            this.TaskName = CDataItem.Instance("");
            this.ExcuteType = EnumGloabParas.EnumExcuteType.GetOnly;
            this.TaskType = EnumGloabParas.EnumTaskType.Normal;
            this.WaitMinutes = this.WaitUrlCount = this.UrlCount = 0;
            this.DownFilePath = CDataItem.Instance(Program.GetConfigPath("data"));
            this.LoginFlag = false;
            this.IsAjax = false;
            this.LoginUrl = CDataItem.Instance("");
            this.WebCookie = CDataItem.Instance("");
            this.WebEncode = EnumGloabParas.EnumEncodeType.AUTO;
            this.UrlEncode = EnumGloabParas.EnumEncodeType.AUTO;
            this.UrlList = new List<TaskItemUrl>();
            this.ColumnItemList = new List<TaskColumnItem>();
            this.ThreadNum = 3;
            this.TryAgainCount = 3;
            this.TryAgainFlag = true;
            this.TempUrl = CDataItem.Instance("");
            this.Remark = CDataItem.Instance("");
            this.LastStartPos = CDataItem.Instance("");
            this.LastEndPos = CDataItem.Instance("");
            this.AutoLog = false;
            this.TaskPlanItemList = new List<TaskPlanItem>();
        }

        public CDataItem DownFilePath { get; set; }//下载文件夹路径

        public bool LoginFlag { get; set; }//是否要登录
        public CDataItem LoginUrl { get; set; }//登录URL
        public CDataItem WebCookie { get; set; }//cookie
        public EnumGloabParas.EnumEncodeType WebEncode { get; set; }//网站编码
        public EnumGloabParas.EnumEncodeType UrlEncode { get; set; }//网址编码
        public CDataItem Remark { get; set; }
        public int WaitMinutes { get; set; }//等待时间(分)
        public int WaitUrlCount { get; set; }//读取网址数量   
        public int UrlCount { get; set; }//
        public bool DownFileQueue { get; set; }//是否进入到下载队列
        public EnumGloabParas.EnumTaskType TaskType { get; set; }//任务类型
        public EnumGloabParas.EnumExcuteType ExcuteType { get; set; }//执行类型
        public bool IsAjax { get; set; }//是否Ajax获取数据
        public int ThreadNum { get; set; }//线程数量
        public int TryAgainCount { get; set; }
        public bool TryAgainFlag { get; set; }
        public bool AutoLog { get; set; }//自动保存采集日志
        public bool AutoErrorLog { get; set; }//自动保存出错信息(采集错误)到日志
        public CDataItem TempUrl { get; set; }//测试URL
        public CDataItem LastStartPos { get; set; }
        public CDataItem LastEndPos { get; set; }

        /// <summary>
        /// 下级导航
        /// </summary>
        public List<TaskItemUrl> UrlList { get; set; }//

        /// <summary>
        /// 获取数据
        /// </summary>
        public List<TaskColumnItem> ColumnItemList { get; set; }

        public EnumGloabParas.EnumConnectionType ConnectionType { get; set; }
        public CDataItem ConnectionString { get; set; }//连接字符串名称，EXCEL文件名称，ACCESS文件名称
        public CDataItem PubWebCookie { get; set; }
        public CDataItem PubDataTable { get; set; }
        public CDataItem PubSql { get; set; }
        public EnumGloabParas.EnumEncodeType PubEncode { get; set; }
        public EnumGloabParas.EnumTriggerType TriggerType { get; set; }
        public List<TaskPlanItem> TaskPlanItemList { get; set; }
    }
}
