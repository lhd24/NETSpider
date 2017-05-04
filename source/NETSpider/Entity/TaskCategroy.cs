using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NETSpider.Entity
{

    public class TaskCategroy : TaskBase
    {
        public int CategroyID { get; set; }
        public CDataItem CategroyName { get; set; }
        /// <summary>
        /// 子类
        /// </summary>
        public List<TaskItemBase> TaskItemList { get; set; }

        public Int64 MaxTaskID
        {
            get
            {
                if (this.TaskItemList == null || this.TaskItemList.Count == 0)
                {
                    return 0;
                }
                return TaskItemList.Select(q => q.TaskID).Max();
            }
        }
    }
}
