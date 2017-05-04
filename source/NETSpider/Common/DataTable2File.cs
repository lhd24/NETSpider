using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WinFormLib.Core;
using System.Data;
using NETSpider.Gather;
using System.Text.RegularExpressions;

namespace NETSpider
{

    public interface IDataTablePublish
    {
        DataTable m_DataTable { get; set; }
        //filename
        string ConnectionString { get; set; }
        string CommandText { get; set; }
        EnumGloabParas.EnumConnectionType ConnectionType { get; set; }
        EnumGloabParas.EnumThreadState ThreadState { get; set; }
        event ExportStartingEvent e_ExportStartingEvent;
        event ExportProgressingEvent e_ExportProgressingEvent;
        event ExportEndedEvent e_ExportEndedEvent;
        void Start();
        void Abort();
    }
    public class DataTable2File : IDataTablePublish
    {
        public DataTable m_DataTable { get; set; }
        public string ConnectionString { get; set; }
        public string CommandText { get; set; }
        public EnumGloabParas.EnumThreadState ThreadState { get; set; }
        public EnumGloabParas.EnumConnectionType ConnectionType
        {
            get;
            set;
        }
        public void ExportToTxt()
        {
            if (false == ValidExport())
            {
                return;
            }
            int totalCount = m_DataTable.Rows.Count;
            string errMsg = string.Empty;
            int rowRead = 0;
            int percent = 0;
            bool isCompleted = false;
            if (e_ExportStartingEvent != null)
            {
                e_ExportStartingEvent(this, new cExportStartEventArgs()
                {
                    TotalCount = totalCount
                });
            }
            try
            {
                //写入标题
                using (System.IO.StreamWriter sw = new System.IO.StreamWriter(this.ConnectionString, false, Encoding.UTF8))
                {
                    for (int i = 0; i < m_DataTable.Rows.Count; i++)
                    {
                        foreach (DataColumn item in m_DataTable.Columns)
                        {
                            string value = m_DataTable.Rows[i][item].ToString();
                            value = Regex.Replace(value, @"\s", "");
                            sw.Write(value + "\t");
                        }
                        sw.WriteLine();
                        rowRead++;
                        string m = ((100m * rowRead) / totalCount).ToString();
                        percent = TryParse.StrToInt(m);
                        if (e_ExportProgressingEvent != null)
                        {
                            e_ExportProgressingEvent(this, new ExportProgressingArgs()
                            {
                                ExportCount = rowRead,
                                ExportPercent = percent,
                                TotalCount = totalCount,
                            });
                        }
                    }
                    sw.Close();
                    isCompleted = true;
                }
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
                isCompleted = false;
            }
            finally
            {
                if (e_ExportEndedEvent != null)
                {
                    e_ExportEndedEvent(this, new cExportEndedEventArgs()
                    {
                        IsCompleted = isCompleted,
                        TotalCount = totalCount,
                        Message = errMsg,
                    });
                }
            }
        }

        private bool ValidExport()
        {
            if (string.IsNullOrEmpty(this.ConnectionString))
            {
                if (e_ExportEndedEvent != null)
                {
                    e_ExportEndedEvent(this, new cExportEndedEventArgs()
                    {
                        IsCompleted = false,
                        TotalCount = 0,
                        Message = "保存文件不能为空",
                    });
                }
                return false;
            }
            if (m_DataTable == null || m_DataTable.Rows.Count == 0 || m_DataTable.Columns.Count == 0)
            {
                if (e_ExportEndedEvent != null)
                {
                    e_ExportEndedEvent(this, new cExportEndedEventArgs()
                    {
                        IsCompleted = false,
                        TotalCount = 0,
                        Message = "表格无数据",
                    });
                }
                return false;
            }
            return true;
        }
        /// <summary>
        /// 将DataTable导出为Excel文件(.xls) 
        /// </summary>
        /// <param name="dt">要导出的DataTable</param>
        public void ExportToExcel()
        {
            if (false == ValidExport())
            {
                return;
            }
            this.ThreadState = EnumGloabParas.EnumThreadState.Run;
            Microsoft.Office.Interop.Excel.Application xlApp = null;
            Microsoft.Office.Interop.Excel.Workbooks workbooks = null;
            int rowTempRead = 0;
            int sheetCount = 1;
            int rangeColumn = 0;
            object rangeValue = "";
            int r = 0;
            bool isComplete = false;
            string errMsg = string.Empty;
            int totalCount = 0;
            try
            {
                xlApp = new Microsoft.Office.Interop.Excel.Application();
                if (xlApp == null)
                {
                    if (e_ExportEndedEvent != null)
                    {
                        e_ExportEndedEvent(this, new cExportEndedEventArgs()
                        {
                            IsCompleted = false,
                            TotalCount = 0,
                            Message = "无法创建Excel对象，可能您的电脑未安装Excel",
                        });
                    }
                    return;
                }

                workbooks = xlApp.Workbooks;
                Microsoft.Office.Interop.Excel.Workbook workbook = workbooks.Add(Microsoft.Office.Interop.Excel.XlWBATemplate.xlWBATWorksheet);
                Microsoft.Office.Interop.Excel.Worksheet worksheet = (Microsoft.Office.Interop.Excel.Worksheet)workbook.Worksheets[1];//取得sheet1
                //Microsoft.Office.Interop.Excel.Range range = null;
                totalCount = m_DataTable.Rows.Count;
                int rowRead = 0;
                int percent = 0;
                if (e_ExportStartingEvent != null)
                {
                    e_ExportStartingEvent(this, new cExportStartEventArgs() { TotalCount = totalCount });
                }
                //写入标题
                WriteTitle(worksheet);

                //写入内容
                for (r = 0; r < m_DataTable.DefaultView.Count; r++, rangeColumn++)
                {
                    if (rowTempRead > 60000)
                    {
                        worksheet = (Microsoft.Office.Interop.Excel.Worksheet)workbook.Worksheets.Add(System.Type.Missing, System.Type.Missing, System.Type.Missing, System.Type.Missing);
                        WriteTitle(worksheet);
                        sheetCount++;
                        rowTempRead = 0;
                        rangeColumn = 0;
                    }
                    for (int i = 0; i < m_DataTable.Columns.Count; i++)
                    {
                        rangeValue = m_DataTable.DefaultView[r][i];
                        rangeValue = "'" + rangeValue;
                        worksheet.Cells[rangeColumn + 2, i + 1] = rangeValue.ToString();
                        //range = (Microsoft.Office.Interop.Excel.Range)worksheet.Cells[r + 2, i + 1];
                        //range.Font.Size = 9;//字体大小
                        //加边框
                        //range.BorderAround(Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous, Microsoft.Office.Interop.Excel.XlBorderWeight.xlThin, Microsoft.Office.Interop.Excel.XlColorIndex.xlColorIndexAutomatic, null);
                        //range.EntireColumn.AutoFit();//自动调整列宽
                    }
                    rowRead++;
                    rowTempRead++;
                    string m = ((100m * rowRead) / totalCount).ToString();
                    percent = TryParse.StrToInt(m);
                    if (e_ExportProgressingEvent != null)
                    {
                        e_ExportProgressingEvent(this, new ExportProgressingArgs()
                        {
                            ExportCount = rowRead,
                            ExportPercent = percent,
                            TotalCount = totalCount,
                        });
                    }
                }
                //range.Borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlInsideHorizontal].Weight = Microsoft.Office.Interop.Excel.XlBorderWeight.xlThin;
                //if (m_DataTable.Columns.Count > 1)
                //{
                //    range.Borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlInsideVertical].Weight = Microsoft.Office.Interop.Excel.XlBorderWeight.xlThin;
                //}
                workbook.Saved = true;
                workbook.SaveCopyAs(ConnectionString);
                isComplete = true;
            }
            catch (Exception ex)
            {
                errMsg = ex.Message + "rows:" + r + "rangeValue:" + rangeValue;
                DMSFrame.Loggers.LoggerManager.FileLogger.LogWithTime(errMsg);
            }
            finally
            {
                if (workbooks != null)
                {
                    workbooks.Close();
                }
                if (xlApp != null)
                {
                    xlApp.Workbooks.Close();
                    xlApp.Quit();
                    int generation = System.GC.GetGeneration(xlApp);
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(xlApp);
                    xlApp = null;
                    System.GC.Collect(generation);
                }
                GC.Collect();//强行销毁
                #region 强行杀死最近打开的Excel进程
                KillExcel();
                if (e_ExportEndedEvent != null)
                {
                    e_ExportEndedEvent(this, new cExportEndedEventArgs()
                    {
                        IsCompleted = isComplete,
                        TotalCount = totalCount,
                        Message = errMsg,
                    });
                }
                #endregion
            }
        }

        private static void KillExcel()
        {
            try
            {
                System.Diagnostics.Process[] excelProc = System.Diagnostics.Process.GetProcessesByName("EXCEL");
                System.DateTime startTime = new DateTime();
                int m, killId = 0;
                for (m = 0; m < excelProc.Length; m++)
                {
                    if (startTime < excelProc[m].StartTime)
                    {
                        startTime = excelProc[m].StartTime;
                        killId = m;
                    }
                }
                if (excelProc[killId].HasExited == false)
                {
                    excelProc[killId].Kill();
                }
            }
            catch (Exception ex)
            {
                DMSFrame.Loggers.LoggerManager.FileLogger.LogWithTime(ex.Message + ex.Source + ex.StackTrace);
            }
        }

        private void WriteTitle(Microsoft.Office.Interop.Excel.Worksheet worksheet)
        {
            for (int i = 0; i < m_DataTable.Columns.Count; i++)
            {
                worksheet.Cells[1, i + 1] = m_DataTable.Columns[i].ColumnName;
                //range = (Microsoft.Office.Interop.Excel.Range)worksheet.Cells[1, i + 1];
                //range.Font.Bold = true;//粗体
                //range.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;//居中
                //加边框
                //range.BorderAround(Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous, Microsoft.Office.Interop.Excel.XlBorderWeight.xlThin, Microsoft.Office.Interop.Excel.XlColorIndex.xlColorIndexAutomatic, null);
            }
        }
        private System.Threading.Thread threadItem;

        public event ExportStartingEvent e_ExportStartingEvent;
        public event ExportProgressingEvent e_ExportProgressingEvent;
        public event ExportEndedEvent e_ExportEndedEvent;


        public void Start()
        {
            if (this.ConnectionType == EnumGloabParas.EnumConnectionType.ExportExcel)
            {
                threadItem = new System.Threading.Thread(new System.Threading.ThreadStart(this.ExportToExcel));
                threadItem.Start();
            }
            else if (this.ConnectionType == EnumGloabParas.EnumConnectionType.ExportTxt)
            {
                threadItem = new System.Threading.Thread(new System.Threading.ThreadStart(this.ExportToTxt));
                threadItem.Start();
            }
        }

        public void Abort()
        {
            if (threadItem != null)
            {
                threadItem.Abort();
                threadItem = null;
            }
        }



    }
}
