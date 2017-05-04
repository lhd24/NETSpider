using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WinFormLib.Core
{
    public enum DataCellType
    {
        None = 0,
        Text = 1,
        SQL = 2,
        /// <summary>
        /// 字符转日期
        /// </summary>
        CharToDate = 3,
        /// <summary>
        /// 日期转字符
        /// </summary>
        DateToChar = 4,
    }
    public enum DialogType
    {
        DialogViewer = 0,
        DataDialogViewer = 1,
        Employee = 2,
        Info = 3,
        Remark = 4,
        XLS = 5,
        Default = 6,
    }
}
