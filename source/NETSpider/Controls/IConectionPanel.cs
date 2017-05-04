using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OleDb;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;
using NETSpider.Entity;


namespace NETSpider.Controls
{
    public delegate void OnReturnDataSource(EnumGloabParas.EnumConnectionType connectionType, string ConnectionString);
    public delegate void OnReturnTaskPlanItem(TaskPlanItem planItem);
    public interface IConectionPanel
    {
        bool TestCon();
        string GetConectionString();
    }
    public class ConectionTables
    {
        public static List<string> GetTables(EnumGloabParas.EnumConnectionType connectionType, string sql)
        {
            List<string> items = new List<string>();
            DbConnection conn = null;
            DataTable tb = null;
            try
            {

                switch (connectionType)
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
                        return items;
                }
                conn.Open();
                tb = conn.GetSchema("Tables");
                DataView dv = tb.DefaultView;
                dv.Sort = "TABLE_NAME Asc";
                DataTable dt2 = dv.ToTable();
                foreach (DataRow r in dt2.Rows)
                {
                    if (connectionType == EnumGloabParas.EnumConnectionType.ExportAccess)
                    {
                        if (r[3].ToString() != "TABLE")
                        {
                            continue;
                        }
                    }
                    if (connectionType == EnumGloabParas.EnumConnectionType.ExportMSSQL)
                    {
                        if (r[3].ToString() != "BASE TABLE")
                        {
                            continue;
                        }
                    }
                    items.Add(r[2].ToString());
                }
            }
            catch (Exception ex)
            {
                WinFormLib.Core.MessageBoxHelper.ShowError(ex.Message);
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }

            return items;
        }
        public static List<string> GetProcedures(EnumGloabParas.EnumConnectionType connectionType, string sql)
        {
            List<string> items = new List<string>();
            DbConnection conn = null;
            DataTable tb = null;
            try
            {

                switch (connectionType)
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
                        return items;
                }
                conn.Open();
                if (connectionType == EnumGloabParas.EnumConnectionType.ExportMSSQL || connectionType == EnumGloabParas.EnumConnectionType.ExportMySql)
                {
                    tb = conn.GetSchema("Procedures");
                    //DataView dv = tb.DefaultView;
                    //dv.Sort = "TABLE_NAME Asc";
                    //DataTable dt2 = dv.ToTable();
                    foreach (DataRow r in tb.Rows)
                    {
                        switch (connectionType)
                        {
                            case EnumGloabParas.EnumConnectionType.ExportMSSQL:
                                items.Add(r[5].ToString());
                                break;
                            case EnumGloabParas.EnumConnectionType.ExportAccess:
                                items.Add(r[5].ToString());
                                break;
                        }
                    }
                }
                else if (connectionType == EnumGloabParas.EnumConnectionType.ExportAccess)
                {
                    string[] Restrictions = new string[4];
                    Restrictions[3] = "VIEW";
                    tb = conn.GetSchema("Tables", Restrictions);
                    foreach (DataRow r in tb.Rows)
                    {
                        items.Add(r[2].ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                WinFormLib.Core.MessageBoxHelper.ShowError(ex.Message);
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }

            return items;
        }
    }
}
