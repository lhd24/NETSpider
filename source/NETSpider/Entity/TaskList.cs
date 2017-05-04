using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace NETSpider.Entity
{
    [Serializable]
    public class TaskList : List<TaskItem>
    {

    }


    [Serializable]
    public class TaskCategroyList : List<TaskCategroy>
    {
        public Int64 MaxTaskID
        {
            get
            {
                Int64 taskID = 0;
                foreach (var item in this)
                {
                    if (item.MaxTaskID > taskID)
                    {
                        taskID = item.MaxTaskID;
                    }
                }
                return taskID;
            }
        }
        public TaskCategroy this[string key]
        {
            get
            {
                return this.Where(q => q.CategroyName.Value == key).FirstOrDefault();
            }
        }
    }

    [Serializable]
    public class TaskRunItemList : List<TaskRunItem>
    {

    }
    [Serializable]
    public class TaskRunTickItemList : List<TaskRunTickItem>
    {

    }
}
