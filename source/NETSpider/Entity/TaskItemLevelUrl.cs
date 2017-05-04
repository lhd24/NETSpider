using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace NETSpider.Entity
{
    public class TaskItemLevelUrl
    {
        public static DataTable GetDataTable(List<TaskItemLevelUrl> dataList)
        {
            DataTable dataTable = new DataTable() { };
            dataTable.Columns.Add("LevelID");
            dataTable.Columns.Add("LevelUrl");
            foreach (TaskItemLevelUrl item in dataList)
            {
                DataRow dr = dataTable.NewRow();
                dr["LevelID"] = item.LevelID;
                dr["LevelUrl"] = item.LevelUrl;
                dataTable.Rows.Add(dr);
            }
            return dataTable;
        }
        public int LevelID { get; set; }
        public string LevelUrl { get; set; }//
    }
}
