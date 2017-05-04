using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NETSpider.Entity;

namespace NETSpider.Gather
{

    public class cGatherEventArgs : EventArgs
    {
        public EnumGloabParas.EnumMessageType MessageType { get; set; }
        public string Message { get; set; }
        public object MessageObject { get; set; }
        public string ThreadName { get; set; }
    }

    public class cGatherUrlItemEventArgs : EventArgs
    {
        /// <summary>
        /// 当前正在运行的urls
        /// </summary>
        public List<cGatherUrlItem> RunWebUrls { get; set; }
        /// <summary>
        /// 当前已经运行完成的urls
        /// </summary>
        public List<cGatherUrlItem> RunCompleteWebUrls { get; set; }

        /// <summary>
        /// 当前正在运行的urls
        /// </summary>
        public List<cGatherUrlBaseItem> RunFileUrls { get; set; }
        /// <summary>
        /// 当前已经运行完成的urls
        /// </summary>
        public List<cGatherUrlBaseItem> RunCompleteFileUrls { get; set; }
        public int TrueCount { get; set; }
        public int ErrorCount { get; set; }
        public int TotalCount { get; set; }
    }

    public class cGatherCompletedEventArgs : EventArgs
    {
        public int TrueCount { get; set; }
        public int ErrorCount { get; set; }
        public int TotalCount { get; set; }
        public EnumGloabParas.EnumThreadState GaterherState { get; set; }
    }

    public class cGatherPublishCompletedEventArgs : EventArgs
    {
        public System.Data.DataTable DataSource { get; set; }
        public TaskRunItem TaskEntity { get; set; }
    }

    public class cGatherDataEventArgs : cGatherUrlItemEventArgs
    {
        public Int64 TaskID { get; set; }
        public System.Data.DataTable dataTable { get; set; }

    }

    public class cExportEndedEventArgs : EventArgs
    {
        public bool IsCompleted { get; set; }
        public int TotalCount { get; set; }
        public string Message { get; set; }
    }
    public class cExportStartEventArgs : EventArgs
    {
        public int TotalCount { get; set; }
    }
    public class ExportProgressingArgs : EventArgs
    {
        public int TotalCount { get; set; }
        public int ExportCount { get; set; }
        public int ExportPercent { get; set; }
    }
    public class cGatherCompleteCountEventArgs : EventArgs
    {
        public string Url { get; set; }
        public int Level { get; set; }
        public List<string> LevelUrlList { get; set; }
        public string NextPageText { get; set; }
        public string StartPos { get; set; }
        public string EndPos { get; set; }
        public EnumGloabParas.EnumUrlGaterherState GaterherFlag { get; set; }
        public EnumGloabParas.EnumThreadCompleteType CompleteType { get; set; }
    }
    public delegate void OnGatherLog(cGatherEventArgs e);
    public delegate void OnGatherTotalCount(cGatherCompletedEventArgs count);
    public delegate void OnGatherCompleteCount(cGatherCompleteCountEventArgs e);
    public delegate void OnGatherThreadAllCompleted(cGatherUrlItemEventArgs e);
    //public delegate void OnGatherGetMainUrlCompleted(cGatherUrlItemEventArgs e);
    public delegate void OnGatherManagereCompleteCount(cGatherCompletedEventArgs e);
    
    /// <summary>
    /// 发布数据
    /// </summary>
    /// <param name="e"></param>
    public delegate void OnGatherPublishCompleted(cGatherPublishCompletedEventArgs e);
    public delegate void OnGatherNotityCompleted(string name);
    public delegate void OnGatherDataCompleted(cGatherDataEventArgs e);

    public delegate void OnExportData(int dataType, System.Data.DataTable dataTable);



    public delegate void ExportStartingEvent(object sender, cExportStartEventArgs e);
    public delegate void ExportProgressingEvent(object sender, ExportProgressingArgs e);
    public delegate void ExportEndedEvent(object sender, cExportEndedEventArgs e);
}
