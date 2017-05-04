using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace NETSpider.Entity
{
    public class TaskColumnItem
    {
        public static DataTable GetDataTable(List<TaskColumnItem> dataList)
        {
            DataTable dataTable = new DataTable() { };
            dataTable.Columns.Add("DataTextType");
            dataTable.Columns.Add("DataFileType");
            dataTable.Columns.Add("StartPos");
            dataTable.Columns.Add("EndPos");
            dataTable.Columns.Add("LimitSign");
            dataTable.Columns.Add("LimitSignText");
            dataTable.Columns.Add("ExportLimit");
            dataTable.Columns.Add("ExportLimitText");
            foreach (TaskColumnItem item in dataList)
            {
                DataRow dr = dataTable.NewRow();
                dr["DataTextType"] = item.DataTextType.Value;
                dr["DataFileType"] = EnumHelper.GetEnumDesc(typeof(EnumGloabParas.EnumDataFileType), item.DataFileType.ToString());
                dr["StartPos"] = item.StartPos.Value;
                dr["EndPos"] = item.EndPos.Value;
                dr["LimitSign"] = EnumHelper.GetEnumDesc(typeof(EnumGloabParas.EnumLimitSign), item.LimitSign.ToString());
                dr["LimitSignText"] = item.LimitSignText.Value;
                dr["ExportLimit"] = EnumHelper.GetEnumDesc(typeof(EnumGloabParas.EnumExportLimit), item.ExportLimit.ToString());
                dr["ExportLimitText"] = item.ExportLimitText.Value;
                dataTable.Rows.Add(dr);
            }
            return dataTable;
        }

        public TaskColumnItem()
        {

        }
        /// <summary>
        /// 
        /// </summary>
        public EnumGloabParas.EnumDataFileType DataFileType { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public CDataItem DataTextType { get; set; }

        /// <summary>
        /// /
        /// </summary>
        public CDataItem StartPos { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public CDataItem EndPos { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public EnumGloabParas.EnumLimitSign LimitSign { get; set; }
        /// <summary>
        /// /
        /// </summary>
        public CDataItem LimitSignText { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public EnumGloabParas.EnumExportLimit ExportLimit { get; set; }

        /// <summary>
        /// /
        /// </summary>
        public CDataItem ExportLimitText { get; set; }

        /// <summary>
        /// 始终去除空格 
        /// </summary>
        public bool LimitSignSpaceFlag { get; set; }
        /// <summary>
        /// 始终去除空格
        /// </summary>
        public bool ExportLimitSpaceFlag { get; set; }


    }
}
