using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace NETSpider.Entity
{
    /// <summary>
    /// 
    /// </summary>
    public class TaskItemUrl
    {
        public static DataTable GetDataTable(List<TaskItemUrl> dataList)
        {
            DataTable dataTable = new DataTable() { };
            dataTable.Columns.Add("MainUrl");
            dataTable.Columns.Add("NavigateFlag");
            dataTable.Columns.Add("LevelCount");
            dataTable.Columns.Add("NextPageText");
            dataTable.Columns.Add("UrlCount");
            foreach (TaskItemUrl item in dataList)
            {
                DataRow dr = dataTable.NewRow();
                dr["MainUrl"] = item.MainUrl.Value;
                dr["NavigateFlag"] = item.NavigateFlag ? "是" : "否";
                dr["LevelCount"] = item.LevelCount;
                dr["NextPageText"] = item.NextPageText;
                dr["UrlCount"] = item.UrlCount;
                dataTable.Rows.Add(dr);
            }
            return dataTable;
        }
        public TaskItemUrl()
        {
            this.LevelUrlList = new List<TaskItemLevelUrl>();
            this.MainUrl = "";
            this.NextPageFlag = this.NavigateFlag = false;
            this.LevelCount = 0;
            this.NextPageText = CDataItem.Instance("");
            this.StartPos = CDataItem.Instance("");
            this.EndPos = CDataItem.Instance("");
        }
        public CDataItem MainUrl { get; set; }//网站主URL
        public int UrlCount { get; set; }
        public bool NextPageFlag { get; set; }//自动读取下一页标识
        public CDataItem NextPageText { get; set; }
        public int? LevelCount { get; set; }//网站总层级
        public bool NavigateFlag { get; set; }//是否需要导航
        public List<TaskItemLevelUrl> LevelUrlList { get; set; }//层级导航
        public CDataItem StartPos { get; set; }
        public CDataItem EndPos { get; set; }
    }
}
