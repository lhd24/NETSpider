using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using WinFormLib.Core;

namespace NETSpider.Controls
{
    public partial class MSPanel : UserControl, IConectionPanel
    {
        public MSPanel()
        {
            InitializeComponent();
        }



        public bool TestCon()
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = GetConectionString("master");
            try
            {
                conn.Open();
            }
            catch (Exception ex)
            {
                MessageBoxHelper.ShowError(ex.Message);
                return false;
            }
            conn.Close();
            MessageBoxHelper.Show("测试连接成功！");
            return true;
        }
        public string GetConectionString()
        {
            if (this.comSqlServerData.SelectedItem == null)
            {
                return string.Empty;
            }
            return GetConectionString(this.comSqlServerData.SelectedItem.ToString());
        }
        private string GetConectionString(string catalog)
        {
            string connectionstring = "";

            connectionstring = "Data Source=" + this.lbSqlserver.Value + ";initial catalog=" + catalog + ";";

            if (this.radioButton1.Checked == true)
                connectionstring += "Integrated Security=True;";
            else if (this.radioButton2.Checked == true)
                connectionstring += "user id=" + this.lbSqlServerUser.Value + ";password=" + this.lbSqlServerPwd.Value;
            return connectionstring;
        }

        private void comSqlServerData_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Changed = true;
        }

        private void comSqlServerData_DropDown(object sender, EventArgs e)
        {
            if (this.comSqlServerData.Items.Count != 0)
            {
                return;
            }
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = GetConectionString("master");
            try
            {
                conn.Open();
            }
            catch (System.Exception ex)
            {
                MessageBoxHelper.ShowError(ex.Message);
                return;
            }
            this.comSqlServerData.Items.Clear();
            DataTable tb = conn.GetSchema("Databases");
            foreach (DataRow r in tb.Rows)
            {
                this.comSqlServerData.Items.Add(r[0].ToString());
            }
        }
    }
}
