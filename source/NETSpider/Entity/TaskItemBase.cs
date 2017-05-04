using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WinFormLib.Components;

namespace NETSpider.Entity
{
    public class TaskItemBase : TaskBase
    {
        public Int64 TaskID { get; set; }//ID
        public CDataItem CategroyName { get; set; }//分类名称
        public CDataItem TaskName { get; set; }//任务名称    
        public static DataGridViewTextBoxColumn GetDataColumn(string name, string dataName, string text)
        {
            DataGridViewTextBoxColumn textBoxColumn = new DataGridViewTextBoxColumn()
            {
                Name = name,
                DataPropertyName = dataName,
                HeaderText = text,
            };
            return textBoxColumn;
        }
    }
}
