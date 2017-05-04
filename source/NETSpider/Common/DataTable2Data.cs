using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using NETSpider.Gather;
using WinFormLib.Core;
using System.Data.Common;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;
using System.Data.OleDb;
namespace NETSpider
{
    public class DataTable2Data : IDataTablePublish
    {
        public DataTable m_DataTable { get; set; }
        public string ConnectionString { get; set; }
        public string CommandText { get; set; }
        public EnumGloabParas.EnumThreadState ThreadState { get; set; }

        public void ExportMSSQL()
        {
            ExcuteExport();
        }
        public void ExportMySql()
        {
            ExcuteExport();
        }

        public void ExportAccess()
        {
            ExcuteExport();
        }
        private DbConnection GetConnection(string sql)
        {
            DbConnection conn = null;
            switch (this.ConnectionType)
            {
                case EnumGloabParas.EnumConnectionType.ExportMSSQL:
                    conn = new SqlConnection(sql);
                    break;
                case EnumGloabParas.EnumConnectionType.ExportMySql:
                    conn = new MySqlConnection(sql);
                    break;
                case EnumGloabParas.EnumConnectionType.ExportAccess:
                    conn = new OleDbConnection(sql);
                    break;
                default:
                    break;
            }
            return conn;
        }
        private void ExcuteExport()
        {
            this.ThreadState = EnumGloabParas.EnumThreadState.Run;
            int totalCount = this.m_DataTable.Rows.Count;
            int rowRead = 0;
            bool IsCompleted = false;
            string errMsg = string.Empty;
            DbConnection conn = GetConnection(this.ConnectionString);
            if (conn == null)
            {
                this.ThreadState = EnumGloabParas.EnumThreadState.SpiderCompleted;
                errMsg = "无法获取到数据库连接";
                ExportCompleted(totalCount, errMsg, IsCompleted);
                return;
            }

            if (e_ExportStartingEvent != null)
            {
                e_ExportStartingEvent(this, new cExportStartEventArgs()
                {
                    TotalCount = totalCount,
                });
            }
            try
            {
                conn.Open();
                foreach (DataRow item in this.m_DataTable.Rows)
                {

                    string commandText = this.CommandText;
                    foreach (DataColumn column in m_DataTable.Columns)
                    {
                        string replaceStr = "{" + column.ColumnName + "}";
                        if (commandText.IndexOf(replaceStr) != -1)
                        {
                            string rowValue = TryParse.ToString(item[column.ColumnName]);
                            rowValue = rowValue.Replace("'", "''");
                            commandText = commandText.Replace(replaceStr, rowValue);
                        }
                    }

                    DbCommand command = conn.CreateCommand();
                    command.CommandText = commandText;
                    int queryCount = command.ExecuteNonQuery();
                    rowRead++;
                    string m = ((100m * rowRead) / totalCount).ToString();
                    int percent = TryParse.StrToInt(m);
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
                this.ThreadState = EnumGloabParas.EnumThreadState.SpiderCompleted;
                IsCompleted = true;
            }
            catch (Exception ex)
            {
                this.ThreadState = EnumGloabParas.EnumThreadState.SpiderCompleted;
                IsCompleted = false;
                errMsg = ex.Message + ex.Source;
            }
            finally
            {
                if (conn != null && conn.State != ConnectionState.Closed)
                {
                    conn.Close();
                }
                ExportCompleted(totalCount, errMsg, IsCompleted);
            }
        }

        private void ExportCompleted(int totalCount, string errMsg, bool IsCompleted)
        {
            if (e_ExportEndedEvent != null)
            {
                e_ExportEndedEvent(this, new cExportEndedEventArgs()
                {
                    IsCompleted = IsCompleted,
                    Message = errMsg,
                    TotalCount = totalCount,
                });
            }
        }
        public event ExportStartingEvent e_ExportStartingEvent;
        public event ExportProgressingEvent e_ExportProgressingEvent;
        public event ExportEndedEvent e_ExportEndedEvent;


        public EnumGloabParas.EnumConnectionType ConnectionType
        {
            get;
            set;
        }
        private System.Threading.Thread threadItem;
        public void Start()
        {
            if (this.ConnectionType == EnumGloabParas.EnumConnectionType.ExportMSSQL)
            {
                threadItem = new System.Threading.Thread(new System.Threading.ThreadStart(this.ExportMSSQL));
                threadItem.Start();
            }
            else if (this.ConnectionType == EnumGloabParas.EnumConnectionType.ExportMySql)
            {
                threadItem = new System.Threading.Thread(new System.Threading.ThreadStart(this.ExportMySql));
                threadItem.Start();
            }
            else if (this.ConnectionType == EnumGloabParas.EnumConnectionType.ExportAccess)
            {
                threadItem = new System.Threading.Thread(new System.Threading.ThreadStart(this.ExportAccess));
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
